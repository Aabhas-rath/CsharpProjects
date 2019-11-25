CREATE TABLE [dbo].[postmetadata] (
    [postId]          INT  IDENTITY (1, 1) NOT NULL,
    [CreatedOn]       DATE NULL,
    [noOfViews]       INT  NULL,
    [noOfLikes]       INT  NULL,
    [hasPics]         BIT  DEFAULT ((0)) NULL,
    [hasTags]         BIT  DEFAULT ((0)) NULL,
    [noOfPics]        INT  DEFAULT ((0)) NULL,
    [noOfTags]        INT  DEFAULT ((0)) NULL,
    [isSponsoredPost] BIT  DEFAULT ((0)) NULL,
    [authorId]        INT  NOT NULL,
    PRIMARY KEY CLUSTERED ([postId] ASC),
    CONSTRAINT [FK_postmetadata_LoginInfo] FOREIGN KEY ([authorId]) REFERENCES [dbo].[LoginInfo] ([AdminId]),
    UNIQUE NONCLUSTERED ([authorId] ASC)
);

