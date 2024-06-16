-- This script contains the stored procedures for the Cancellation Module of the Hotel Booking System.

-- Get Cancellation Policies
-- This stored procedure retrieves active cancellation policies for display purposes.
CREATE
	OR
ALTER PROCEDURE spGetCancellationPolicies 
	@Status BIT OUTPUT
	,-- Output parameter for status (1 = Success, 0 = Failure)
	@Message NVARCHAR(255) OUTPUT -- Output parameter for messages
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		SELECT PolicyID
			,Description
			,CancellationChargePercentage
			,MinimumCharge
			,EffectiveFromDate
			,EffectiveToDate
		FROM CancellationPolicies
		WHERE EffectiveFromDate <= GETDATE()
			AND EffectiveToDate >= GETDATE();

		SET @Status = 1;-- Success
		SET @Message = 'Policies retrieved successfully.';
	END TRY

	BEGIN CATCH
		SET @Status = 0;-- Failure
		SET @Message = ERROR_MESSAGE();
	END CATCH
END;
GO

-- Calculate Cancellation Charges
-- Calculates the cancellation charges based on the policies.
CREATE
	OR

ALTER PROCEDURE spCalculateCancellationCharges 
	 @ReservationID INT
	,@RoomsCancelled RoomIDTableType READONLY
	,@TotalCost DECIMAL(10, 2) OUTPUT
	,@CancellationCharge DECIMAL(10, 2) OUTPUT
	,@CancellationPercentage DECIMAL(10, 2) OUTPUT
	,@PolicyDescription NVARCHAR(255) OUTPUT
	,@Status BIT OUTPUT
	,@Message NVARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @CheckInDate DATE;
	DECLARE @TotalRoomsCount INT
		,@CancelledRoomsCount INT;

	BEGIN TRY
		-- Fetch check-in date
		SELECT @CheckInDate = CheckInDate
		FROM Reservations
		WHERE ReservationID = @ReservationID;

		IF @CheckInDate IS NULL
		BEGIN
			SET @Status = 0;-- Failure
			SET @Message = 'No reservation found with the given ID.';

			RETURN;
		END

		-- Determine if the cancellation is full or partial
		SELECT @TotalRoomsCount = COUNT(*)
		FROM ReservationRooms
		WHERE ReservationID = @ReservationID;

		SELECT @CancelledRoomsCount = COUNT(*)
		FROM @RoomsCancelled;

		IF @CancelledRoomsCount = @TotalRoomsCount
		BEGIN
			-- Full cancellation: Calculate based on total reservation cost
			SELECT @TotalCost = SUM(TotalAmount)
			FROM Payments
			WHERE ReservationID = @ReservationID;
		END
		ELSE
		BEGIN
			-- Partial cancellation: Calculate based on specific rooms' costs from PaymentDetails
			SELECT @TotalCost = SUM(pd.Amount)
			FROM PaymentDetails pd
			INNER JOIN ReservationRooms rr ON pd.ReservationRoomID = rr.ReservationRoomID
			INNER JOIN @RoomsCancelled rc ON rr.RoomID = rc.RoomID
			WHERE rr.ReservationID = @ReservationID;
		END

		-- Check if total cost was calculated
		IF @TotalCost IS NULL
		BEGIN
			SET @Status = 0;-- Failure
			SET @Message = 'Failed to calculate total costs.';

			RETURN;
		END

		-- Fetch the appropriate cancellation policy based on the check-in date
		SELECT TOP 1 @CancellationPercentage = CancellationChargePercentage
			,@PolicyDescription = Description
		FROM CancellationPolicies
		WHERE EffectiveFromDate <= @CheckInDate
			AND EffectiveToDate >= @CheckInDate
		ORDER BY EffectiveFromDate DESC;-- In case of overlapping policies, the most recent one is used
			-- Calculate the cancellation charge

		SET @CancellationCharge = @TotalCost * (@CancellationPercentage / 100);
		SET @Status = 1;-- Success
		SET @Message = 'Calculation successful';
	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK;

		SET @Status = 0;-- Failure
		SET @Message = ERROR_MESSAGE();
	END CATCH
END;
GO

-- Create Cancellation Request
-- This Stored Procedure creates a cancellation request after validating the provided information.
CREATE
	OR

ALTER PROCEDURE spCreateCancellationRequest 
	 @UserID INT
	,@ReservationID INT
	,@RoomsCancelled RoomIDTableType READONLY
	,-- Table-valued parameter
	@CancellationReason NVARCHAR(MAX)
	,@Status BIT OUTPUT
	,-- Output parameter for operation status
	@Message NVARCHAR(255) OUTPUT
	,-- Output parameter for operation message
	@CancellationRequestID INT OUTPUT -- Output parameter to store the newly created CancellationRequestID
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;-- Automatically roll-back the transaction on error.

	DECLARE @CancellationType NVARCHAR(50);
	DECLARE @TotalRooms INT
		,@CancelledRoomsCount INT
		,@RemainingRoomsCount INT;
	DECLARE @ExistingStatus NVARCHAR(50);
	DECLARE @CheckInDate DATE
		,@CheckOutDate DATE;

	-- Retrieve reservation details
	SELECT @ExistingStatus = STATUS
		,@CheckInDate = CheckInDate
		,@CheckOutDate = CheckOutDate
	FROM Reservations
	WHERE ReservationID = @ReservationID;

	-- Validation for reservation status and dates
	IF @ExistingStatus = 'Cancelled'
		OR GETDATE() >= @CheckInDate
	BEGIN
		SET @Status = 0;-- Failure
		SET @Message = 'Cancellation not allowed. Reservation already fully cancelled or past check-in date.';

		RETURN;
	END

	-- Prevent cancellation of already cancelled or pending cancellation rooms
	IF EXISTS (
			SELECT 1
			FROM CancellationDetails cd
			JOIN CancellationRequests cr ON cd.CancellationRequestID = cr.CancellationRequestID
			JOIN ReservationRooms rr ON cd.ReservationRoomID = rr.ReservationRoomID
			JOIN @RoomsCancelled rc ON rr.RoomID = rc.RoomID
			WHERE cr.ReservationID = @ReservationID
				AND cr.STATUS IN (
					'Approved'
					,'Pending'
					)
			)
	BEGIN
		SET @Status = 0;-- Failure
		SET @Message = 'One or more rooms have already been cancelled or cancellation is pending.';

		RETURN;
	END

	SELECT @TotalRooms = COUNT(*)
	FROM ReservationRooms
	WHERE ReservationID = @ReservationID;

	SELECT @CancelledRoomsCount = COUNT(*)
	FROM CancellationDetails cd
	JOIN CancellationRequests cr ON cd.CancellationRequestID = cr.CancellationRequestID
	WHERE cr.ReservationID = @ReservationID
		AND cr.STATUS IN ('Approved');

	-- Calculate remaining rooms that are not yet cancelled
	SET @RemainingRoomsCount = @TotalRooms - @CancelledRoomsCount;

	-- Determine the type of cancellation based on remaining rooms to be cancelled
	IF (
			@RemainingRoomsCount = (
				SELECT COUNT(*)
				FROM @RoomsCancelled
				)
			)
		SET @CancellationType = 'Full'
	ELSE
		SET @CancellationType = 'Partial';

	BEGIN TRY
		BEGIN TRANSACTION

		-- Insert into CancellationRequests
		INSERT INTO CancellationRequests (
			ReservationID
			,UserID
			,CancellationType
			,RequestedOn
			,STATUS
			,CancellationReason
			)
		VALUES (
			@ReservationID
			,@UserID
			,@CancellationType
			,GETDATE()
			,'Pending'
			,@CancellationReason
			);

		SET @CancellationRequestID = SCOPE_IDENTITY();

		-- Insert into CancellationDetails for rooms not yet cancelled
		INSERT INTO CancellationDetails (
			CancellationRequestID
			,ReservationRoomID
			)
		SELECT @CancellationRequestID
			,rr.ReservationRoomID
		FROM ReservationRooms rr
		JOIN @RoomsCancelled rc ON rr.RoomID = rc.RoomID
		WHERE rr.ReservationID = @ReservationID;

		COMMIT TRANSACTION;

		SET @Status = 1;-- Success
		SET @Message = 'Cancellation request created successfully.';
	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;

		SET @Status = 0;-- Failure
		SET @Message = ERROR_MESSAGE();
	END CATCH
END;
GO

-- Get All Cancellations
-- This procedure fetches all cancellations based on the optional status filter.
CREATE
	OR
ALTER PROCEDURE spGetAllCancellations 
     @Status NVARCHAR(50) = NULL
	,@DateFrom DATETIME = NULL
	,@DateTo DATETIME = NULL
	,@StatusOut BIT OUTPUT
	,-- Output parameter for operation status
	@MessageOut NVARCHAR(255) OUTPUT -- Output parameter for operation message
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @SQL NVARCHAR(MAX)
		,@Params NVARCHAR(MAX);

	-- Initialize dynamic SQL query
	SET @SQL = N'SELECT CancellationRequestID, ReservationID, UserID, CancellationType, RequestedOn, Status FROM CancellationRequests WHERE 1=1';

	-- Append conditions dynamically based on the input parameters
	IF @Status IS NOT NULL
		SET @SQL += N' AND Status = @Status';

	IF @DateFrom IS NOT NULL
		SET @SQL += N' AND RequestedOn >= @DateFrom';

	IF @DateTo IS NOT NULL
		SET @SQL += N' AND RequestedOn <= @DateTo';
	-- Define parameters for dynamic SQL
	SET @Params = N'@Status NVARCHAR(50), @DateFrom DATETIME, @DateTo DATETIME';

	BEGIN TRY
		-- Execute dynamic SQL
		EXEC sp_executesql @SQL
			,@Params
			,@Status = @Status
			,@DateFrom = @DateFrom
			,@DateTo = @DateTo;

		-- If successful, set output parameters
		SET @StatusOut = 1;-- Success
		SET @MessageOut = 'Cancellations retrieved successfully.';
	END TRY

	BEGIN CATCH
		-- If an error occurs, set output parameters to indicate failure
		SET @StatusOut = 0;-- Failure
		SET @MessageOut = ERROR_MESSAGE();
	END CATCH
END;
GO

-- Review Cancellation Request
-- This procedure is used by an admin to review and either approve or reject a cancellation request.
CREATE
	OR

ALTER PROCEDURE spReviewCancellationRequest 
	 @CancellationRequestID INT
	,@AdminUserID INT
	,@ApprovalStatus NVARCHAR(50)
	,-- 'Approved' or 'Rejected'
	@Status BIT OUTPUT
	,-- Output parameter for operation status
	@Message NVARCHAR(255) OUTPUT -- Output parameter for operation message
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;-- Automatically roll-back the transaction on error.

	DECLARE @ReservationID INT
		,@CalcStatus BIT
		,@CalcMessage NVARCHAR(MAX);
	DECLARE @RoomsCancelled AS RoomIDTableType;
	DECLARE @CalcTotalCost DECIMAL(10, 2)
		,@CalcCancellationCharge DECIMAL(10, 2)
		,@CalcCancellationPercentage DECIMAL(10, 2)
		,@CalcPolicyDescription NVARCHAR(255);

	BEGIN TRY
		-- Validate the existence of the Cancellation Request
		IF NOT EXISTS (
				SELECT 1
				FROM CancellationRequests
				WHERE CancellationRequestID = @CancellationRequestID
				)
		BEGIN
			SET @Status = 0;-- Failure
			SET @Message = 'Cancellation request does not exist.';

			RETURN;
		END

		-- Validate the Admin User exists and is active
		IF NOT EXISTS (
				SELECT 1
				FROM Users
				WHERE UserID = @AdminUserID
					AND IsActive = 1
				)
		BEGIN
			SET @Status = 0;-- Failure
			SET @Message = 'Admin user does not exist or is not active.';

			RETURN;
		END

		-- Validate the Approval Status
		IF @ApprovalStatus NOT IN (
				'Approved'
				,'Rejected'
				)
		BEGIN
			SET @Status = 0;-- Failure
			SET @Message = 'Invalid approval status.';

			RETURN;
		END

		BEGIN TRANSACTION

		-- Update the Cancellation Requests
		UPDATE CancellationRequests
		SET STATUS = @ApprovalStatus
			,AdminReviewedByID = @AdminUserID
			,ReviewDate = GETDATE()
		WHERE CancellationRequestID = @CancellationRequestID;

		SELECT @ReservationID = ReservationID
		FROM CancellationRequests
		WHERE CancellationRequestID = @CancellationRequestID;

		IF @ApprovalStatus = 'Approved'
		BEGIN
			-- Fetch all rooms associated with the cancellation request
			INSERT INTO @RoomsCancelled (RoomID)
			SELECT rr.RoomID
			FROM CancellationDetails cd
			JOIN ReservationRooms rr ON cd.ReservationRoomID = rr.ReservationRoomID
			WHERE cd.CancellationRequestID = @CancellationRequestID;

			-- Call the calculation procedure
			EXEC spCalculateCancellationCharges @ReservationID = @ReservationID
				,@RoomsCancelled = @RoomsCancelled
				,@TotalCost = @CalcTotalCost OUTPUT
				,@CancellationCharge = @CalcCancellationCharge OUTPUT
				,@CancellationPercentage = @CalcCancellationPercentage OUTPUT
				,@PolicyDescription = @CalcPolicyDescription OUTPUT
				,@Status = @CalcStatus OUTPUT
				,@Message = @CalcMessage OUTPUT;

			IF @CalcStatus = 0 -- Check if the charge calculation was unsuccessful
			BEGIN
				SET @Status = 0;-- Failure
				SET @Message = 'Failed to calculate cancellation charges: ' + @CalcMessage;

				ROLLBACK TRANSACTION;

				RETURN;
			END

			-- Insert into CancellationCharges table
			INSERT INTO CancellationCharges (
				CancellationRequestID
				,TotalCost
				,CancellationCharge
				,CancellationPercentage
				,PolicyDescription
				)
			VALUES (
				@CancellationRequestID
				,@CalcTotalCost
				,@CalcCancellationCharge
				,@CalcCancellationPercentage
				,@CalcPolicyDescription
				);

			UPDATE Rooms
			SET STATUS = 'Available'
			WHERE RoomID IN (
					SELECT RoomID
					FROM @RoomsCancelled
					);

			UPDATE Reservations
			SET STATUS = CASE 
					WHEN (
							SELECT COUNT(*)
							FROM ReservationRooms
							WHERE ReservationID = @ReservationID
							) = (
							SELECT COUNT(*)
							FROM @RoomsCancelled
							)
						THEN 'Cancelled'
					ELSE 'Partially Cancelled'
					END
			WHERE ReservationID = @ReservationID;
		END

		COMMIT TRANSACTION;

		SET @Status = 1;-- Success
		SET @Message = 'Cancellation request handled successfully.';
	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;

		SET @Status = 0;-- Failure
		SET @Message = ERROR_MESSAGE();
	END CATCH
END;
GO

-- Get Cancellations for Refund
-- This procedure is used by an admin to fetch cancellations that are approved and either have no refund record
-- or need refund action (Pending or Failed, excluding Completed)
CREATE
	OR

ALTER PROCEDURE spGetCancellationsForRefund
AS
BEGIN
	SET NOCOUNT ON;

	SELECT cr.CancellationRequestID
		,cr.ReservationID
		,cr.UserID
		,cr.CancellationType
		,cr.RequestedOn
		,cr.STATUS
		,ISNULL(r.RefundID, 0) AS RefundID
		,-- Use 0 or another appropriate default value to indicate no refund has been initiated
		ISNULL(r.RefundStatus, 'Not Initiated') AS RefundStatus -- Use 'Not Initiated' or another appropriate status
	FROM CancellationRequests cr
	LEFT JOIN Refunds r ON cr.CancellationRequestID = r.CancellationRequestID
	WHERE cr.STATUS = 'Approved'
		AND (
			r.RefundStatus IS NULL
			OR r.RefundStatus IN (
				'Pending'
				,'Failed'
				)
			);
END;
GO

-- Process Refund
-- Processes refunds for approved cancellations.
CREATE
	OR

ALTER PROCEDURE spProcessRefund 
	 @CancellationRequestID INT
	,@ProcessedByUserID INT
	,@RefundMethodID INT
	,@RefundID INT OUTPUT
	,-- Output parameter for the newly created Refund ID
	@Status BIT OUTPUT
	,-- Output parameter for operation status
	@Message NVARCHAR(255) OUTPUT -- Output parameter for operation message
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;-- Automatically roll-back the transaction on error.

	DECLARE @PaymentID INT
		,@RefundAmount DECIMAL(10, 2)
		,@CancellationCharge DECIMAL(10, 2)
		,@NetRefundAmount DECIMAL(10, 2);

	BEGIN TRY
		BEGIN TRANSACTION

		-- Validate the existence of the CancellationRequestID and its approval status
		IF NOT EXISTS (
				SELECT 1
				FROM CancellationRequests
				WHERE CancellationRequestID = @CancellationRequestID
					AND STATUS = 'Approved'
				)
		BEGIN
			SET @Status = 0;-- Failure
			SET @Message = 'Invalid CancellationRequestID or the request has not been approved.';

			RETURN;
		END

		-- Retrieve the total amount and cancellation charge from the CancellationCharges table
		SELECT @PaymentID = p.PaymentID
			,@RefundAmount = cc.TotalCost
			,@CancellationCharge = cc.CancellationCharge
		FROM CancellationCharges cc
		JOIN Payments p ON p.ReservationID = (
				SELECT ReservationID
				FROM CancellationRequests
				WHERE CancellationRequestID = @CancellationRequestID
				)
		WHERE cc.CancellationRequestID = @CancellationRequestID;

		-- Calculate the net refund amount after deducting the cancellation charge
		SET @NetRefundAmount = @RefundAmount - @CancellationCharge;

		-- Insert into Refunds table, mark the Refund Status as Pending
		INSERT INTO Refunds (
			PaymentID
			,RefundAmount
			,RefundDate
			,RefundReason
			,RefundMethodID
			,ProcessedByUserID
			,RefundStatus
			,CancellationCharge
			,NetRefundAmount
			,CancellationRequestID
			)
		VALUES (
			@PaymentID
			,@NetRefundAmount
			,GETDATE()
			,'Cancellation Approved'
			,@RefundMethodID
			,@ProcessedByUserID
			,'Pending'
			,@CancellationCharge
			,@NetRefundAmount
			,@CancellationRequestID
			);

		-- Capture the newly created Refund ID
		SET @RefundID = SCOPE_IDENTITY();

		COMMIT TRANSACTION;

		SET @Status = 1;-- Success
		SET @Message = 'Refund processed successfully.';
	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;

		SET @Status = 0;-- Failure
		SET @Message = ERROR_MESSAGE();
	END CATCH
END;
GO

-- Update Refund Status
CREATE
	OR

ALTER PROCEDURE spUpdateRefundStatus 
	 @RefundID INT
	,@NewRefundStatus NVARCHAR(50)
	,@Status BIT OUTPUT
	,@Message NVARCHAR(255) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;-- Automatically roll-back the transaction on error.
		-- Define valid statuses, adjust these as necessary for your application

	DECLARE @ValidStatuses TABLE (STATUS NVARCHAR(50));

	INSERT INTO @ValidStatuses
	VALUES ('Pending')
		,('Processed')
		,('Completed')
		,('Failed');

	BEGIN TRY
		BEGIN TRANSACTION

		-- Check current status of the refund to avoid updating final states like 'Completed'
		DECLARE @CurrentStatus NVARCHAR(50);

		SELECT @CurrentStatus = RefundStatus
		FROM Refunds
		WHERE RefundID = @RefundID;

		IF @CurrentStatus IS NULL
		BEGIN
			SET @Status = 0;-- Failure
			SET @Message = 'Refund not found.';

			ROLLBACK TRANSACTION;

			RETURN;
		END

		IF @CurrentStatus = 'Completed'
		BEGIN
			SET @Status = 0;-- Failure
			SET @Message = 'Refund is already completed and cannot be updated.';

			ROLLBACK TRANSACTION;

			RETURN;
		END

		-- Validate the new refund status
		IF NOT EXISTS (
				SELECT 1
				FROM @ValidStatuses
				WHERE STATUS = @NewRefundStatus
				)
		BEGIN
			SET @Status = 0;-- Failure
			SET @Message = 'Invalid new refund status provided.';

			ROLLBACK TRANSACTION;

			RETURN;
		END

		-- Update the Refund Status if validations pass
		UPDATE Refunds
		SET RefundStatus = @NewRefundStatus
		WHERE RefundID = @RefundID;

		IF @@ROWCOUNT = 0
		BEGIN
			SET @Status = 0;-- Failure
			SET @Message = 'No refund found with the provided RefundID.';

			ROLLBACK TRANSACTION;

			RETURN;
		END

		COMMIT TRANSACTION;

		SET @Status = 1;-- Success
		SET @Message = 'Refund status updated successfully.';
	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;

		SET @Status = 0;-- Failure
		SET @Message = ERROR_MESSAGE();
	END CATCH
END;
GO


