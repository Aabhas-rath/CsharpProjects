CREATE PROCEDURE [dbo].[spGetPostsAssociatedToTag]
	@id int = 0
AS
IF @id=0 
	begin
		return 1
	End
Else
	Begin
		SELECT *
		FROM PostContent_Tags(nolock)
		WHERE TagId=@id;
	RETURN 0
	End