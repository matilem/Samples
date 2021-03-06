USE [netForum]
GO
/****** Object:  StoredProcedure [dbo].[get_also_course]    Script Date: 3/6/2018 9:15:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- ======================================================================
-- Author:		Megan Matile
-- Create date: 12/12/2017
-- Description:	Returns also course 
-- ======================================================================
ALTER PROCEDURE [dbo].[get_also_course]
(
	 @ActivityKey	UNIQUEIDENTIFIER	
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		  
		  b28_key									AS AlsoCourseKey
		, b28_m31_key								AS ActivityKey
		, b28_prc_key								AS PriceKey
		, b28_b33_key								AS MilitaryBranchKey
		, b33_type									AS MilitaryBranch	
		, b28_pre_course_submitted_flag				AS PreCourseSubmittedFlag
		, b28_pre_course_approved_flag				AS PreCourseApprovedFlag
		, b28_post_course_submitted_flag			AS PostCourseSubmittedFlag
		, b28_post_course_completed_flag			AS PostCourseCompletedFlag
	FROM	dbo.client_aafp_b28_also_course
		 INNER JOIN dbo.client_aafp_b33_cme_military_branch ON client_aafp_b33_cme_military_branch.b33_key = client_aafp_b28_also_course.b28_b33_key	
	WHERE				
			b28_m31_key = @ActivityKey
END

















