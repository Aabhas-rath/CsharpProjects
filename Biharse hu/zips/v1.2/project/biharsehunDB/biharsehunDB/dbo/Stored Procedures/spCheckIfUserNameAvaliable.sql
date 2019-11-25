CREATE PROCEDURE [dbo].[spCheckIfUserNameAvaliable]
	@username nvarchar(15)
AS
	SELECT count(*) 
	FROM LoginInfo(NOLOCK)
	WHERE username=@username;
RETURN 0