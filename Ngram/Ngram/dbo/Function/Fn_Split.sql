CREATE FUNCTION [dbo].[Fn_Split]
(
	@stringToSplit nvarchar(max),
	@SlitChar char
)
RETURNS 
@tvTableD TABLE 
(
	 Code   nvarchar(max) 
)
AS
BEGIN
	

	While CHARINDEX(@SlitChar, @stringToSplit) > 0 or len(@stringToSplit)> 0 Begin

	  declare @pos int
	  declare @name  nvarchar(max)
	  SELECT @pos  = CHARINDEX(@SlitChar, @stringToSplit)
	  if(@pos = 0)  
		set @pos = len(@stringToSplit) 

	  SELECT @name = SUBSTRING(@stringToSplit, 1, @pos-1)

	INSERT INTO @tvTableD (Code) values (@name)  
	 SELECT @stringToSplit = SUBSTRING(@stringToSplit, @pos+1, LEN(@stringToSplit)-@pos)

	End

	return
END
