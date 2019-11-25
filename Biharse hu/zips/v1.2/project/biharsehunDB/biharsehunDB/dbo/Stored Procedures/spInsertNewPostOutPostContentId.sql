CREATE PROCEDURE [dbo].[spInsertNewPostOutPostContentId]
	@Heading nvarchar(500),
	@Content nvarchar(MAX),
	@createdon date,
	@noofviews int,
	@nooflikes int,
	@noofpics int,
	@nooftags int,
	@haspics bit,
	@hastags bit,
	@issponsoredpost bit,
	
	@associatedAuthorid int,

	@postid int,
	@postcontentid int out
AS

	INSERT INTO postmetadata(CreatedOn,noOfViews,noOfLikes,hasPics,hasTags,noOfPics,noOfTags,isSponsoredPost,authorId)
	VALUES (@createdon,@noofviews,@nooflikes,@haspics,@hastags,@noofpics,@nooftags,@issponsoredpost,@associatedAuthorid);
	SET @postid=@@IDENTITY;
	INSERT INTO postContent(postId,Heading,Content) 
	VALUES (@postid,@Heading,@Content);
	Set @postcontentid=@@Identity;
	UPDATE UserInfo
	SET NoOfPosts=NoOfPosts+1
	WHERE userId=@associatedAuthorid;
	IF @hastags=1
	BEGIN
		INSERT INTO PostContent_Tags(postId)
		VALUES (@postid);
	END
RETURN 0