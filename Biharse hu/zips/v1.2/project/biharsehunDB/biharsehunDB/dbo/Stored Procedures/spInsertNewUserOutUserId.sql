CREATE PROCEDURE [dbo].[spInsertNewUserOutUserId]
	@username nvarchar(15),
	@password nvarchar(15),
	@emailid nvarchar(30),
	@name nvarchar(20),
	@displayname nvarchar(15),
	@createdon date,
	@noofposts int,
	@nooftags int,
	@dateofbirth date,
	@isauthor bit,
	@isadmin bit,
	@adminid int,
	@userid int out
AS
	INSERT INTO LoginInfo(username,[password],emailId)
	VALUES (@username,@password,@emailid);
	SET @adminid=@@IDENTITY;
	INSERT INTO UserInfo(Name,DisplayName,CreatedOn,NoOfPosts,NoOfTags,DateOfBirth,isAuthor,isAdmin,AdminId)
	VALUES (@name,@displayname,@createdon,@noofposts,@nooftags,@dateofbirth,@isauthor,@isadmin,@adminid);
	Set @userid=@@Identity;
RETURN 0