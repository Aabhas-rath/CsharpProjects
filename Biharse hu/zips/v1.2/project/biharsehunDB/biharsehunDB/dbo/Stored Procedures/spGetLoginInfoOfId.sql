CREATE PROCEDURE [dbo].[spGetLoginInfoOfId]
	@id int = 0
AS
IF @id=0 
	begin
		return 1
	End
Else
	Begin
		SELECT *
		FROM LoginInfo(NOLOCK)
		WHERE AdminId=@id;
	RETURN 0
	End