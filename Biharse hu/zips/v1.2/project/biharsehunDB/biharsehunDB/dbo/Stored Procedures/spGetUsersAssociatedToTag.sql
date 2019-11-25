CREATE PROCEDURE [dbo].[spGetUsersAssociatedToTag]
	@id int = 0
AS
IF @id=0 
	begin
		return 1
	End
Else
	Begin
		SELECT *
		FROM UserInfo_Tags(nolock)
		WHERE TagId=@id;
	RETURN 0
	End