USE [netForum]
GO
/****** Object:  StoredProcedure [dbo].[get_also_activities_for_staff]    Script Date: 1/5/2018 7:58:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- ==============================================================
-- Author:		Megan Matile
-- Create date: 12/12/2017
-- Description:	Returns all Also/Blso activities that 
--				have pre-course or post-course work submitted
-- ==============================================================
CREATE PROCEDURE [dbo].[get_also_activities_for_staff]

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
			, b28_pre_course_submitted_flag		AS ActivityPreCourseSubmitted
			, b28_pre_course_approved_flag		AS ActivityPreCourseApproved
			, b28_post_course_submitted_flag	AS ActivityPostCourseSubmitted
			, b28_post_course_completed_flag	AS ActivityPostCourseCompleted			
	FROM
			dbo.client_aafp_m31_cme_activity WITH (NOLOCK)
	 INNER JOIN
			dbo.client_aafp_m01_cme_application WITH (NOLOCK) ON client_aafp_m01_cme_application.m01_key = client_aafp_m31_cme_activity.m31_m01_key
	 INNER JOIN
			dbo.client_aafp_m09_cme_session WITH (NOLOCK) ON client_aafp_m09_cme_session.m09_m31_key = client_aafp_m31_cme_activity.m31_key
	 LEFT OUTER JOIN
			dbo.client_aafp_b28_also_course WITH (NOLOCK) ON client_aafp_b28_also_course.b28_m31_key = client_aafp_m31_cme_activity.m31_key
	 INNER JOIN 
			dbo.client_aafp_m02_cme_workflow_step ON client_aafp_m02_cme_workflow_step.m02_key = client_aafp_m01_cme_application.m01_m02_key
	 LEFT OUTER JOIN 
			dbo.co_customer ON co_customer.cst_key = client_aafp_m31_cme_activity.m31_cst_key_provider
	WHERE
			m31_begin_date >= DATEADD(month, -6, GETDATE())
	 AND	m01_activity_sub_type IN ('ALSO Provider', 'ALSO Instructor', 'ALSO Refresher', 'BLSO Provider')
	 AND	m01_delete_flag = 0
	 AND	m01_submission_date IS NOT NULL
	 AND	m02_key != 'C86DDF8D-FE32-42A7-8E83-EB1F29208385'
	 --AND	(b28_pre_course_submitted_flag = 1 OR b28_post_course_submitted_flag = 1)
	 ORDER BY 
			m31_begin_date
	
END
