USE [netForum]
GO
/****** Object:  StoredProcedure [dbo].[get_also_course_history]    Script Date: 3/7/2018 10:22:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- ======================================================================
-- Author:		Megan Matile
-- Create date: 12/12/2017
-- Description:	Returns also status types 
-- ======================================================================
CREATE PROCEDURE [dbo].[get_also_status_types]

AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		  
		    a01_key		AS AlsoStatusTypeKey
		  , a01_type		AS AlsoStatusType
	FROM	dbo.client_aafp_a01_also_status_type			
END





















