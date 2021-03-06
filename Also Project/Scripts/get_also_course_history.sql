USE [netForum]
GO
/****** Object:  StoredProcedure [dbo].[get_also_course_history]    Script Date: 3/7/2018 10:01:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- ======================================================================
-- Author:		Megan Matile
-- Create date: 12/12/2017
-- Description:	Returns also activity 
-- ======================================================================
ALTER PROCEDURE [dbo].[get_also_course_history]
(
	 @CustomerKey	UNIQUEIDENTIFIER	
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		  
		    a03_key								AS AlsoStatusKey
		  , a03_add_user						AS AlsoStatusAddUser
		  , a03_add_date						AS AlsoStatusAddDate 
		  , a03_role							AS AlsoStatusRole
		  , m01_activity_sub_type				AS ActivityCourseType
	FROM	dbo.client_aafp_a03_also_course_history
			INNER JOIN dbo.client_aafp_m09_cme_session ON client_aafp_m09_cme_session.m09_key = client_aafp_a03_also_course_history.a03_m09_key
			INNER JOIN dbo.client_aafp_m31_cme_activity ON client_aafp_m31_cme_activity.m31_key = client_aafp_m09_cme_session.m09_m31_key
			INNER JOIN dbo.client_aafp_m01_cme_application ON client_aafp_m01_cme_application.m01_key = client_aafp_m31_cme_activity.m31_m01_key			
	WHERE				
			a03_cst_key = @CustomerKey
	AND		a03_add_date >= DATEADD(yyyy, -5, GETDATE())
END





















