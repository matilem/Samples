USE [netForum]
GO
/****** Object:  StoredProcedure [dbo].[get_cme_teaching_credits_count]    Script Date: 3/11/2018 4:19:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- ======================================================================
-- Author:		Megan Matile
-- Create date: 12/12/2017
-- Description:	Returns also activity 
-- ======================================================================
CREATE PROCEDURE [dbo].[get_advisory_faculty_recommendation]
(
	 @CustomerKey	UNIQUEIDENTIFIER
)
AS
BEGIN
	SET NOCOUNT ON;
	
SELECT CASE WHEN EXISTS (
    SELECT	*
    FROM	dbo.client_aafp_b31_also_course_instructor
    WHERE	b31_cst_key = @CustomerKey
)
THEN CAST(1 AS BIT)
ELSE CAST(0 AS BIT) END
END





















