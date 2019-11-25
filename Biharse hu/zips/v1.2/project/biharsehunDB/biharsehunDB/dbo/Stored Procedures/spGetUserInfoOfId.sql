CREATE PROCEDURE [dbo].[spGetUserInfoOfId]
	@id int = 0
AS
IF @id=0 
	begin
		return 1
	End
Else
	Begin
		SELECT *
		FROM UserInfo(NOLOCK)
		WHERE userId=@id;
	RETURN 0
	End