USE [netForum]
GO
/****** Object:  StoredProcedure [dbo].[get_also_post_course]    Script Date: 3/6/2018 6:26:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ======================================================================
-- Author:		Megan Matile
-- Create date: 12/12/2017
-- Description:	Returns activity instructor information
-- ======================================================================
CREATE PROCEDURE [dbo].[get_also_instructors]
(
	 @ActivityKey	UNIQUEIDENTIFIER
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		  
		    b31_key									AS InstructorKey
		  , b31_cst_key								AS CustomerKey
		  , cst_id									AS CustomerId
		  , ind_first_name							AS FirstName
		  , ind_last_name							AS LastName
		  , b31_advisory_faculty_recommended		AS AdvisoryFacultyRecommended
		  , b31_instructor_recommended				AS InstructorRecommended
FROM	
		 dbo.client_aafp_b31_also_course_instructor
		 INNER JOIN	dbo.co_individual ON b31_cst_key = co_individual.ind_cst_key
		 INNER JOIN dbo.co_customer ON co_customer.cst_key = client_aafp_b31_also_course_instructor.b31_cst_key
WHERE				
		b31_m31_key = @ActivityKey
END