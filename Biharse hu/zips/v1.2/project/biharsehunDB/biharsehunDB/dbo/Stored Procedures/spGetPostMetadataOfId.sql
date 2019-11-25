CREATE PROCEDURE [dbo].[spGetPostMetadataOfId]
	@id int = 0
AS
IF @id=0 
	begin
		return 1
	End
Else
	Begin
		SELECT *
		FROM PostMetadata(NOLOCK)
		WHERE postId=@id;
	RETURN 0
	End