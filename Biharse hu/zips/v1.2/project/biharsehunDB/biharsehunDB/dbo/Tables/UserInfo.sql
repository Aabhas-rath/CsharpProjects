CREATE TABLE [dbo].[UserInfo] (
    [userId]      INT           IDENTITY (1, 1) NOT NULL,
    [CreatedOn]   DATE          NOT NULL,
    [Name]        VARCHAR (20)  NOT NULL,
    [DisplayName] NVARCHAR (15) NULL,
    [NoOfPosts]   INT           DEFAULT ((0)) NOT NULL,
    [NoOfTags]    INT           DEFAULT ((0)) NOT NULL,
    [DateOfBirth] DATE          NULL,
    [isAuthor]    BIT           DEFAULT ((0)) NOT NULL,
    [AdminId]     INT           NOT NULL,
    [isAdmin]     BIT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([userId] ASC),
    CONSTRAINT [FK_UserInfo_LoginInfo] FOREIGN KEY ([AdminId]) REFERENCES [dbo].[LoginInfo] ([AdminId]),
    UNIQUE NONCLUSTERED ([AdminId] ASC)
);

