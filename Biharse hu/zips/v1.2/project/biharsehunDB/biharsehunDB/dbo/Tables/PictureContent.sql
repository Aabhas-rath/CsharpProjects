CREATE TABLE [dbo].[PictureContent] (
    [PicContentId]     INT            IDENTITY (1, 1) NOT NULL,
    [PicId]            INT            NULL,
    [Caption]          NVARCHAR (200) NULL,
    [Path]             NVARCHAR (50)  NULL,
    [PictureFileName]  NVARCHAR (20)  NULL,
    [PicThumbnailPath] NVARCHAR (50)  NULL,
    [takenBy]          VARCHAR (30)   NULL,
    PRIMARY KEY CLUSTERED ([PicContentId] ASC),
    CONSTRAINT [FK_PictureContent_PictureMetadata] FOREIGN KEY ([PicId]) REFERENCES [dbo].[PictureMetadata] ([PicId]),
    UNIQUE NONCLUSTERED ([PicId] ASC)
);

