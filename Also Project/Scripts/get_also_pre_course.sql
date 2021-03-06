USE [netForum]
GO
/****** Object:  StoredProcedure [dbo].[get_also_pre_course]    Script Date: 2/16/2018 10:21:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- ======================================================================
-- Author:		Megan Matile
-- Create date: 12/12/2017
-- Description:	Returns activity pre-course information by Activity
-- ======================================================================
CREATE PROCEDURE [dbo].[get_also_pre_course]
(
	 @ActivityNumber	INT	
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		  
		  m31_m01_key						AS ApplicationKey
		, m31_course_director_cst_key		AS CourseDirectorKey
		, m31_director_id					AS CourseDirectorId
		, m31_director_name					AS CourseDirectorName
		, m31_director_email				AS CourseDirectorEmail
		, m31_director_phone				AS CourseDirectorPhone
		, m31_course_coordinator_cst_key	AS CourseCoordinatorKey
		, m31_coordinator_id				AS CourseCoordinatorId
		, m31_course_coordinator_name		AS CourseCoordinatorName
		, m31_course_coordinator_email		AS CourseCoordinatorEmail
		, m31_course_coordinator_phone		AS CourseCoordinatorPhone
		, m09_m35_key_agenda				AS AgendaKey
		, m35_file_location					AS ActivitySessionAgendaUrl
		, m09_city							AS ActivityCity
		, m09_sta_code						AS ActivityState
		, cst_name_cp						AS ActivitySponsorName
	FROM	dbo.client_aafp_m31_cme_activity
			INNER JOIN dbo.client_aafp_m01_cme_application ON client_aafp_m01_cme_application.m01_key = client_aafp_m31_cme_activity.m31_m01_key
			INNER JOIN dbo.client_aafp_m09_cme_session ON client_aafp_m09_cme_session.m09_m31_key = client_aafp_m31_cme_activity.m31_key
			INNER JOIN dbo.client_aafp_m35_cme_attachment ON client_aafp_m35_cme_attachment.m35_key = client_aafp_m09_cme_session.m09_m35_key_agenda
			INNER JOIN dbo.client_aafp_m02_cme_workflow_step ON client_aafp_m02_cme_workflow_step.m02_key = client_aafp_m01_cme_application.m01_m02_key
			LEFT OUTER JOIN dbo.co_customer ON co_customer.cst_key = client_aafp_m31_cme_activity.m31_cst_key_provider
	WHERE				
			m31_number = @ActivityNumber
	AND		m02_key != 'C86DDF8D-FE32-42A7-8E83-EB1F29208385'
END





















