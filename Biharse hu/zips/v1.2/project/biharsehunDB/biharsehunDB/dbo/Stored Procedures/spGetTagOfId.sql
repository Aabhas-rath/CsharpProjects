CREATE PROCEDURE [dbo].[spGetTagOfId]
	@id int = 0
AS
IF @id=0 
	begin
		return 1
	End
Else
	Begin
		SELECT *
		FROM Tags(NOLOCK)
		WHERE TagId=@id;
	RETURN 0
	End