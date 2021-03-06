USE [netForum]
GO
/****** Object:  StoredProcedure [dbo].[get_also_learner_activities]    Script Date: 1/22/2018 1:50:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =====================================================
-- Author:		Megan Matile
-- Create date: 12/12/2017
-- Description:	Returns Also/Blso activities by learner
-- =====================================================
CREATE PROCEDURE [dbo].[get_also_learner_activities]
(
		@CustomerKey	UNIQUEIDENTIFIER
		 
)
AS
BEGIN
	SET NOCOUNT ON;
	

	SELECT
			  m31_key					AS ActivityKey
			, m31_number				AS ActivityNumber
			, m31_title					AS ActivityTitle
			, m31_begin_date			AS ActivityBeginDate
			, m31_end_date				AS ActivityEndDate
			, m01_activity_sub_type		AS ActivityCourseType
			, m09_city					AS ActivityCity
			, m09_sta_code				AS ActivityState
			, cst_name_cp				AS ActivitySponsorName
			, m01_cst_key_submitter
			, m31_course_director_cst_key
			, m31_course_coordinator_cst_key
	FROM
			dbo.client_aafp_m31_cme_activity WITH (NOLOCK)
	 INNER JOIN
			dbo.client_aafp_m01_cme_application WITH (NOLOCK) ON client_aafp_m01_cme_application.m01_key = client_aafp_m31_cme_activity.m31_m01_key
	 INNER JOIN
			dbo.client_aafp_m09_cme_session WITH (NOLOCK) ON client_aafp_m09_cme_session.m09_m31_key = client_aafp_m31_cme_activity.m31_key
	 INNER JOIN 
			dbo.client_aafp_m02_cme_workflow_step ON client_aafp_m02_cme_workflow_step.m02_key = client_aafp_m01_cme_application.m01_m02_key
	 LEFT OUTER JOIN 
			dbo.co_customer ON co_customer.cst_key = client_aafp_m31_cme_activity.m31_cst_key_provider
	WHERE
			m31_begin_date >= DATEADD(month, -6, GETDATE())
	 AND	(m01_cst_key_submitter = @CustomerKey OR m31_course_director_cst_key = @CustomerKey OR m31_course_coordinator_cst_key = @CustomerKey)
	 AND	m01_activity_sub_type IN ('ALSO Provider', 'ALSO Instructor', 'ALSO Refresher', 'BLSO Provider')
	 AND	m01_delete_flag = 0
	 AND	m01_submission_date IS NOT NULL
	 AND	m02_key != 'C86DDF8D-FE32-42A7-8E83-EB1F29208385'
	 ORDER BY
			m01_cst_key_submitter DESC

END
