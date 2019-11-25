CREATE TABLE [dbo].[LoginInfo] (
    [AdminId]  INT           IDENTITY (1, 1) NOT NULL,
    [username] NVARCHAR (15) NOT NULL,
    [password] NVARCHAR (15) NOT NULL,
    [emailId]  NVARCHAR (30) NULL,
    PRIMARY KEY CLUSTERED ([AdminId] ASC)
);

