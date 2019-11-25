CREATE PROCEDURE [dbo].[spGetPictureMetadataOfId]
	@id int = 0
AS
IF @id=0 
	begin
		return 1
	End
Else
	Begin
		SELECT *
		FROM PictureMetadata(NOLOCK)
		WHERE PicId=@id;
	RETURN 0
	End