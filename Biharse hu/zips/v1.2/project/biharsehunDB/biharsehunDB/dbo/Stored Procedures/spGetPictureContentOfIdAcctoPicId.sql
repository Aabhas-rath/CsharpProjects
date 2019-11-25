CREATE PROCEDURE [dbo].[spGetPictureContentOfIdAcctoPicId]
	@id int = 0
AS
IF @id=0 
	begin
		return 1
	End
Else
	Begin
		SELECT *
		FROM PictureContent(nolock)
		WHERE PicId=@id;
	RETURN 0
	End