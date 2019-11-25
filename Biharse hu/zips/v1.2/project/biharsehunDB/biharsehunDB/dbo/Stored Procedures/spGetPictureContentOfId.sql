CREATE PROCEDURE [dbo].[spGetPictureContentOfId]
	@id int = 0
AS
IF @id=0 
	begin
		return 1
	End
Else
	Begin
		SELECT *
		FROM PictureContent(NOLOCK)
		WHERE PicContentId=@id;
	RETURN 0
	End