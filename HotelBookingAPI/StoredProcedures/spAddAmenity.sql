
-- Create the stored procedure to add a new amenity record to the database.
CREATE PROCEDURE [dbo].[spAddAmenity]

-- Define the input parameters.
@Name NVARCHAR(100),
@Description NVARCHAR(255),
@CreatedBy NVARCHAR(100),

-- Define the output parameters.
@AmenityID INT OUTPUT,
@Status BIT OUTPUT,
@Message NVARCHAR(255) OUTPUT

AS

-- Begins the stored procedure body.
BEGIN

-- Disable the row count affected message.
SET NOCOUNT ON;

-- Begin the try block to handle exceptions.
BEGIN TRY

    -- Begin a transaction to ensure data integrity.
	BEGIN TRANSACTION

	-- Checks if an amenity with the same name already exists to avoid duplication.
	IF EXISTS (SELECT 1 FROM Amenities WHERE Name = @Name)
		BEGIN
			-- Set the output parameters to indicate that the amenity already exists.
			SET @Status = 0;
			SET @Message = 'Amenity already exists.';
		END
	ELSE
		BEGIN
			-- Insert the new amenity record into the Amenities table.
			INSERT INTO Amenities (Name, Description, CreatedBy, CreatedDate, IsActive)
			VALUES (@Name, @Description, @CreatedBy, GETDATE(), 1);
			
			-- Retrieve the ID of the newly inserted amenity record.
			SET @AmenityID = SCOPE_IDENTITY();
			
			-- Set the output parameters to indicate that the amenity was added successfully.
			SET @Status = 1;
			SET @Message = 'Amenity added successfully.';
		END

	-- Commit the transaction to save the changes to the database.
	COMMIT TRANSACTION;

END TRY

-- Begin the catch block to handle exceptions.
BEGIN CATCH

	-- Rollback the transaction to undo any changes made during the try block.
	ROLLBACK TRANSACTION;

	-- Set the output parameters to indicate that an error occurred.
	SET @Status = 0;
	SET @Message = ERROR_MESSAGE();
END CATCH;

-- Ends the stored procedure body.
END;
GO