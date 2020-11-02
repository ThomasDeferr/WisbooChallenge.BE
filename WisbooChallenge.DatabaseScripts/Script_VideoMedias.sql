CREATE TABLE VideoMedias (
    ID INT PRIMARY KEY IDENTITY NOT NULL,

    HashedID VARCHAR(25) NOT NULL,
    Title VARCHAR(100) NOT NULL,
    Color VARCHAR(7) NOT NULL,

    TS DATETIME2 DEFAULT(GETDATE()) NOT NULL,
    
    CONSTRAINT UK_VideoMediaHashed UNIQUE (HashedID)
);
GO
------------------------------------------------------------
CREATE OR ALTER PROCEDURE usp_VideoMedias_GetAll
AS
BEGIN 
    SET NOCOUNT ON;

    SELECT  
        *
    FROM 
        VideoMedias 
    ORDER BY 
        ID;
END;
GO

CREATE OR ALTER PROCEDURE usp_VideoMedias_GetByID (
	@ID INT
)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT  
        VideoMedias.*
    FROM 
        VideoMedias
    WHERE 
        VideoMedias.ID = @ID;
END;
GO

CREATE OR ALTER PROCEDURE usp_VideoMedias_GetByHashedID (
	@HashedID VARCHAR(25)
)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT  
        *
    FROM 
        VideoMedias
    WHERE 
        HashedID = @HashedID;
END;
GO

CREATE OR ALTER PROCEDURE usp_VideoMedias_Insert (
    @HashedID VARCHAR(25),
    @Title VARCHAR(100),
    @Color VARCHAR(7),

    @ID INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO VideoMedias (
        HashedID,
        Title,
        Color
    )
    VALUES (
        @HashedID,
        @Title,
        @Color
    );

    SET @ID = SCOPE_IDENTITY();
END;
GO

CREATE OR ALTER PROCEDURE usp_VideoMedias_UpdateByID (
    @ID INT,

    @HashedID VARCHAR(25),
    @Title VARCHAR(100),
    @Color VARCHAR(7)
)
AS 
BEGIN
    SET NOCOUNT ON;

    UPDATE VideoMedias
    SET 
        HashedID = @HashedID,
        Title = @Title,
        Color = @Color,

        TS = GETDATE()
    WHERE
        ID = @ID;
END;
GO

CREATE OR ALTER PROCEDURE usp_VideoMedias_Delete (
    @ID INT
)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM VideoMedias WHERE ID = @ID;
END;
GO        