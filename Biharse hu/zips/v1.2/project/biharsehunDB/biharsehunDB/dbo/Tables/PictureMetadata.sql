CREATE TABLE [dbo].[PictureMetadata] (
    [PicId]             INT          IDENTITY (1, 1) NOT NULL,
    [CreatedOn]         DATE         NOT NULL,
    [fileSize]          INT          NULL,
    [Height]            INT          NULL,
    [Width]             INT          NULL,
    [Format]            NVARCHAR (8) NULL,
    [picturePriority]   INT          NULL,
    [isPostBackground]  BIT          DEFAULT ((0)) NULL,
    [isPostMainPicture] BIT          DEFAULT ((0)) NULL,
    [AssociatedPostId]  INT          NULL,
    PRIMARY KEY CLUSTERED ([PicId] ASC),
    CONSTRAINT [FK_PictureMetadata_postmetadata] FOREIGN KEY ([AssociatedPostId]) REFERENCES [dbo].[postmetadata] ([postId])
);

