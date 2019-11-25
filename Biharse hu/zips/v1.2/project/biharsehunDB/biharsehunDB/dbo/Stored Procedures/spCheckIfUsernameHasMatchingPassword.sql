CREATE PROCEDURE [dbo].[spCheckIfUsernameHasMatchingPassword]
	@username nvarchar(15),
	@password nvarchar(15)
AS
IF EXISTS 
	(SELECT username 
	FROM LoginInfo(NOLOCK)
	WHERE username=@username)
	BEGIN
	IF EXISTS (SELECT [password] FROM LoginInfo(NOLOCK) WHERE username=@username AND [password]= @password)
		BEGIN
		RETURN 0;
		END
	ELSE
		BEGIN
		RETURN 1;
		END
	END
ELSE
	BEGIN
	RETURN 2;
	END