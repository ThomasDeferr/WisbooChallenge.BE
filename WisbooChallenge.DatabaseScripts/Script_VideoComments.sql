CREATE TABLE VideoComments (
    ID INT PRIMARY KEY IDENTITY NOT NULL,

    VideoMediaID INT NOT NULL,
    Content VARCHAR(100) NOT NULL,
    UploadDate DATETIME2 NOT NULL,

    TS DATETIME2 DEFAULT(GETDATE()) NOT NULL,
    
    CONSTRAINT FK_CommentVideo FOREIGN KEY (VideoMediaID) REFERENCES VideoMedias(ID)
);
GO
------------------------------------------------------------
CREATE OR ALTER PROCEDURE usp_VideoComments_GetAll
AS
BEGIN 
    SET NOCOUNT ON;

    SELECT  
        *
    FROM 
        VideoComments
    ORDER BY 
        ID;
END;
GO

CREATE OR ALTER PROCEDURE usp_VideoComments_GetAllByVideoMedia (
	@VideoMediaID INT
)
AS
BEGIN 
    SET NOCOUNT ON;

    SELECT  
        *
    FROM 
        VideoComments
    WHERE   
        VideoMediaID = @VideoMediaID
    ORDER BY 
        ID;
END;
GO

CREATE OR ALTER PROCEDURE usp_VideoComments_GetByID (
	@ID INT
)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT  
        *
    FROM 
        VideoComments
    WHERE 
        ID = @ID;
END;
GO

CREATE OR ALTER PROCEDURE usp_VideoComments_Insert (
    @VideoMediaID INT,
    @Content VARCHAR(100),
    @UploadDate DATETIME2,

    @ID INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO VideoComments (
        VideoMediaID,
        Content,
        UploadDate
    )
    VALUES (
        @VideoMediaID,
        @Content,
        @UploadDate
    );

    SET @ID = SCOPE_IDENTITY();
END;
GO

CREATE OR ALTER PROCEDURE usp_VideoComments_UpdateByID (
    @ID INT,

    @VideoMediaID INT,
    @Content VARCHAR(100),
    @UploadDate DATETIME2
)
AS 
BEGIN
    SET NOCOUNT ON;

    UPDATE VideoComments
    SET 
        VideoMediaID = @VideoMediaID,
        Content = @Content,
        UploadDate = @UploadDate,

        TS = GETDATE()
    WHERE
        ID = @ID;
END;
GO

CREATE OR ALTER PROCEDURE usp_VideoComments_Delete (
    @ID INT
)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM VideoComments WHERE ID = @ID;
END;
GO        