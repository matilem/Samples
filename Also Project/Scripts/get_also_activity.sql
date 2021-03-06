USE [netForum]
GO
/****** Object:  StoredProcedure [dbo].[get_also_activity]    Script Date: 3/11/2018 2:58:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- ======================================================================
-- Author:		Megan Matile
-- Create date: 12/12/2017
-- Description:	Returns also activity 
-- ======================================================================
ALTER PROCEDURE [dbo].[get_also_activity]
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
		, m09_city							AS ActivityCity
		, m09_sta_code						AS ActivityState
		, cst_name_cp						AS ActivitySponsorName
		, m31_director_name					AS ActivityDirectorName
		, m31_course_coordinator_name		AS ActivityCoordinatorName
		, m09_key							AS ActivitySessionKey
		, m02_description					AS CMEApplicationStatus
	FROM	dbo.client_aafp_m31_cme_activity
			INNER JOIN 
				dbo.client_aafp_m01_cme_application ON client_aafp_m01_cme_application.m01_key = client_aafp_m31_cme_activity.m31_m01_key
			INNER JOIN 
				dbo.client_aafp_m09_cme_session ON client_aafp_m09_cme_session.m09_m31_key = client_aafp_m31_cme_activity.m31_key
			INNER JOIN 
				dbo.client_aafp_m02_cme_workflow_step ON client_aafp_m02_cme_workflow_step.m02_key = client_aafp_m01_cme_application.m01_m02_key
			LEFT OUTER JOIN 
				dbo.co_customer ON co_customer.cst_key = client_aafp_m31_cme_activity.m31_cst_key_provider				
	WHERE				
			m31_number = @ActivityNumber
	AND		m02_key != 'C86DDF8D-FE32-42A7-8E83-EB1F29208385'
END





















