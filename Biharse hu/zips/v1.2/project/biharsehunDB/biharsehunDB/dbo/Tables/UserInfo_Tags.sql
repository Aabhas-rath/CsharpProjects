CREATE TABLE [dbo].[UserInfo_Tags] (
    [tagId]  INT NOT NULL,
    [UserId] INT NOT NULL,
    CONSTRAINT [FK_UserInfo_Tags_Tags] FOREIGN KEY ([tagId]) REFERENCES [dbo].[Tags] ([tagId]),
    CONSTRAINT [FK_UserInfo_Tags_UserInfo] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserInfo] ([userId])
);

