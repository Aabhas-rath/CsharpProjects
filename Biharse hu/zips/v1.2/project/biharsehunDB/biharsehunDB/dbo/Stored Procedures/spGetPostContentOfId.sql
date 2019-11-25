CREATE PROCEDURE [dbo].[spGetPostContentOfId]
	@id int = 0
AS
IF @id=0 
	begin
		return 1
	End
Else
	Begin
		SELECT *
		FROM PostContent(NOLOCK)
		WHERE postContentId=@id;
	RETURN 0
	End