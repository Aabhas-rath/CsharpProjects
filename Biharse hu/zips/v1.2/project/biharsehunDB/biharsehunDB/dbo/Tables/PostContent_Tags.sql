CREATE TABLE [dbo].[PostContent_Tags] (
    [tagId]  INT NOT NULL,
    [postId] INT NOT NULL,
    CONSTRAINT [FK_PostContent_Tags_postmetadata] FOREIGN KEY ([postId]) REFERENCES [dbo].[postmetadata] ([postId]),
    CONSTRAINT [FK_PostContent_Tags_Tags] FOREIGN KEY ([tagId]) REFERENCES [dbo].[Tags] ([tagId])
);

