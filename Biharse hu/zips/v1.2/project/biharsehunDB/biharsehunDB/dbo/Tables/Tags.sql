CREATE TABLE [dbo].[Tags] (
    [tagId]      INT           IDENTITY (1, 1) NOT NULL,
    [CreatedOn]  DATE          NULL,
    [isNewTag]   BIT           DEFAULT ((0)) NULL,
    [tagCounter] INT           NULL,
    [Content]    NVARCHAR (25) NULL,
    PRIMARY KEY CLUSTERED ([tagId] ASC)
);

