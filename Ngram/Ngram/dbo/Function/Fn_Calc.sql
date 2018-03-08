CREATE FUNCTION [dbo].[Fn_Calc]
(
	@lookfor  nvarchar(500),
	@Mod int,
	@Min float
)
RETURNS @returntable TABLE
(
	Ordre Int,
	Score Float,
	Found  nvarchar(500)
)
AS
BEGIN
	INSERT @returntable
	 SELECT TOP (100) 
		ROW_NUMBER()over( ORDER BY [Score] DESC) [Ordre], [Found]
	from 
	(
		select
			(CONVERT(Float,[dbo].[fn_StringDistance](@lookfor, [Text], @Mod ))) as [Score] , [Text] as [Found]
			from [dbo].[Sample]
			where (CONVERT(Float,[dbo].[fn_StringDistance](@lookfor, [Text], @Mod ))>@Min) 
			
	) as result
	ORDER BY [Score] DESC
	RETURN
END
