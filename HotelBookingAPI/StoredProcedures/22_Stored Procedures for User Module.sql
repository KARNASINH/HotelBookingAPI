-- This script contains the stored procedures for the User module of the Hotel Booking API.

-- Stored Procedures for Create the Room
CREATE
	OR

ALTER PROCEDURE spCreateRoom 
	 @RoomNumber NVARCHAR(10)
	,@RoomTypeID INT
	,@Price DECIMAL(10, 2)
	,@BedType NVARCHAR(50)
	,@ViewType NVARCHAR(50)
	,@Status NVARCHAR(50)
	,@IsActive BIT
	,@CreatedBy NVARCHAR(100)
	,@NewRoomID INT OUTPUT
	,@StatusCode INT OUTPUT
	,@Message NVARCHAR(255) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRANSACTION

		-- Check if the provided RoomTypeID exists in the RoomTypes table
		IF EXISTS (
				SELECT 1
				FROM RoomTypes
				WHERE RoomTypeID = @RoomTypeID
				)
		BEGIN
			-- Ensure the room number is unique
			IF NOT EXISTS (
					SELECT 1
					FROM Rooms
					WHERE RoomNumber = @RoomNumber
					)
			BEGIN
				INSERT INTO Rooms (
					RoomNumber
					,RoomTypeID
					,Price
					,BedType
					,ViewType
					,STATUS
					,IsActive
					,CreatedBy
					,CreatedDate
					)
				VALUES (
					@RoomNumber
					,@RoomTypeID
					,@Price
					,@BedType
					,@ViewType
					,@Status
					,@IsActive
					,@CreatedBy
					,GETDATE()
					)

				SET @NewRoomID = SCOPE_IDENTITY()
				SET @StatusCode = 0 -- Success
				SET @Message = 'Room created successfully.'
			END
			ELSE
			BEGIN
				SET @StatusCode = 1 -- Failure due to duplicate room number
				SET @Message = 'Room number already exists.'
			END
		END
		ELSE
		BEGIN
			SET @StatusCode = 3 -- Failure due to invalid RoomTypeID
			SET @Message = 'Invalid Room Type ID provided.'
		END

		COMMIT TRANSACTION
	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION

		SET @StatusCode = ERROR_NUMBER()
		SET @Message = ERROR_MESSAGE()
	END CATCH
END
GO

-- Stored Procedures for Update the Room
CREATE
	OR

ALTER PROCEDURE spUpdateRoom 
	 @RoomID INT
	,@RoomNumber NVARCHAR(10)
	,@RoomTypeID INT
	,@Price DECIMAL(10, 2)
	,@BedType NVARCHAR(50)
	,@ViewType NVARCHAR(50)
	,@Status NVARCHAR(50)
	,@IsActive BIT
	,@ModifiedBy NVARCHAR(100)
	,@StatusCode INT OUTPUT
	,@Message NVARCHAR(255) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRANSACTION

		-- Check if the RoomTypeID is valid and room number is unique for other rooms
		IF EXISTS (
				SELECT 1
				FROM RoomTypes
				WHERE RoomTypeID = @RoomTypeID
				)
			AND NOT EXISTS (
				SELECT 1
				FROM Rooms
				WHERE RoomNumber = @RoomNumber
					AND RoomID <> @RoomID
				)
		BEGIN
			-- Verify the room exists before updating
			IF EXISTS (
					SELECT 1
					FROM Rooms
					WHERE RoomID = @RoomID
					)
			BEGIN
				UPDATE Rooms
				SET RoomNumber = @RoomNumber
					,RoomTypeID = @RoomTypeID
					,Price = @Price
					,BedType = @BedType
					,ViewType = @ViewType
					,STATUS = @Status
					,IsActive = @IsActive
					,ModifiedBy = @ModifiedBy
					,ModifiedDate = GETDATE()
				WHERE RoomID = @RoomID

				SET @StatusCode = 0 -- Success
				SET @Message = 'Room updated successfully.'
			END
			ELSE
			BEGIN
				SET @StatusCode = 2 -- Failure due to room not found
				SET @Message = 'Room not found.'
			END
		END
		ELSE
		BEGIN
			SET @StatusCode = 1 -- Failure due to invalid RoomTypeID or duplicate room number
			SET @Message = 'Invalid Room Type ID or duplicate room number.'
		END

		COMMIT TRANSACTION
	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION

		SET @StatusCode = ERROR_NUMBER()
		SET @Message = ERROR_MESSAGE()
	END CATCH
END
GO

-- Stored Procedures for Soft-Delete the Room
CREATE
	OR

ALTER PROCEDURE spDeleteRoom 
	 @RoomID INT
	,@StatusCode INT OUTPUT
	,@Message NVARCHAR(255) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRANSACTION

		-- Ensure no active reservations exist for the room
		IF NOT EXISTS (
				SELECT 1
				FROM Reservations
				WHERE RoomID = @RoomID
					AND STATUS NOT IN (
						'Checked-out'
						,'Cancelled'
						)
				)
		BEGIN
			-- Verify the room exists and is currently active before deactivating
			IF EXISTS (
					SELECT 1
					FROM Rooms
					WHERE RoomID = @RoomID
						AND IsActive = 1
					)
			BEGIN
				-- Instead of deleting, we update the IsActive flag to false
				UPDATE Rooms
				SET IsActive = 0 -- Set IsActive to false to indicate the room is no longer active
				WHERE RoomID = @RoomID

				SET @StatusCode = 0 -- Success
				SET @Message = 'Room deactivated successfully.'
			END
			ELSE
			BEGIN
				SET @StatusCode = 2 -- Failure due to room not found or already deactivated
				SET @Message = 'Room not found or already deactivated.'
			END
		END
		ELSE
		BEGIN
			SET @StatusCode = 1 -- Failure due to active reservations
			SET @Message = 'Room cannot be deactivated, there are active reservations.'
		END

		COMMIT TRANSACTION
	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION

		SET @StatusCode = ERROR_NUMBER()
		SET @Message = ERROR_MESSAGE()
	END CATCH
END
GO

-- Stored Procedures for Get Room by Id
CREATE
	OR

ALTER PROCEDURE spGetRoomById 
		 @RoomID INT
AS
BEGIN
	SELECT RoomID
		,RoomNumber
		,RoomTypeID
		,Price
		,BedType
		,ViewType
		,STATUS
		,IsActive
	FROM Rooms
	WHERE RoomID = @RoomID
END
GO

-- Stored Procedures for Get All Rooms
-- Youcan add optional parameters to filter the results based on RoomTypeID and Status
CREATE
	OR

ALTER PROCEDURE spGetAllRoom 

	@RoomTypeID INT = NULL
	,-- Optional filter by Room Type
	@Status NVARCHAR(50) = NULL -- Optional filter by Status
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @SQL NVARCHAR(MAX)

	-- Start building the dynamic SQL query
	SET @SQL = 'SELECT RoomID, RoomNumber, RoomTypeID, Price, BedType, ViewType, Status, IsActive FROM Rooms WHERE 1=1'

	-- Append conditions based on the presence of optional parameters
	IF @RoomTypeID IS NOT NULL
		SET @SQL = @SQL + ' AND RoomTypeID = @RoomTypeID'

	IF @Status IS NOT NULL
		SET @SQL = @SQL + ' AND Status = @Status'

	-- Execute the dynamic SQL statement
	EXEC sp_executesql @SQL
		,N'@RoomTypeID INT, @Status NVARCHAR(50)'
		,@RoomTypeID
		,@Status
END
GO


