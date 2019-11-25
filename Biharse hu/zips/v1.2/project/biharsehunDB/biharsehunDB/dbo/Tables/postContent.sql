CREATE TABLE [dbo].[postContent] (
    [postContentId] INT            IDENTITY (1, 1) NOT NULL,
    [postId]        INT            NOT NULL,
    [Heading]       NVARCHAR (500) NULL,
    [Content]       NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([postContentId] ASC),
    CONSTRAINT [FK_postContent_postmetadata] FOREIGN KEY ([postId]) REFERENCES [dbo].[postmetadata] ([postId]),
    UNIQUE NONCLUSTERED ([postId] ASC)
);

