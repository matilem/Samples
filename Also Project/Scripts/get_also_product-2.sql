USE [netForum]
GO
/****** Object:  StoredProcedure [dbo].[get_also_activity]    Script Date: 1/24/2018 8:40:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- ======================================================================
-- Author:		Megan Matile
-- Create date: 12/12/2017
-- Description:	Returns also activity 
-- ======================================================================
CREATE PROCEDURE [dbo].[get_also_activity]
(
	 @ActivityNumber	INT	
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		  
		  m31_key							AS ActivityKey
		, m31_number						AS ActivityNumber
		, m31_title							AS ActivityTitle
		, m31_begin_date					AS ActivityBeginDate
		, m31_end_date						AS ActivityEndDate			
		, m01_activity_sub_type				AS ActivityCourseType
	FROM	dbo.client_aafp_m31_cme_activity
	INNER JOIN 
			dbo.client_aafp_m01_cme_application ON client_aafp_m01_cme_application.m01_key = client_aafp_m31_cme_activity.m31_m01_key
				
	WHERE				
			m31_number = @ActivityNumber
END





















