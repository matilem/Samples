USE [netForum]
GO
/****** Object:  StoredProcedure [dbo].[get_cme_teaching_credits_count]    Script Date: 3/7/2018 9:48:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- ======================================================================
-- Author:		Megan Matile
-- Create date: 12/12/2017
-- Description:	Returns also activity 
-- ======================================================================
ALTER PROCEDURE [dbo].[get_cme_teaching_credits_count]
(
	 @CustomerKey	UNIQUEIDENTIFIER,
	 @Year			INT	
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		  
		  COUNT(*)
	FROM	dbo.client_aafp_m41_cme_reported_credit			
	WHERE				
			m41_cst_key = @CustomerKey
	AND		m41_add_date >= DATEADD(yyyy, @Year, DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0)))
	AND		m41_m32_key = 'A3F70592-837E-4918-B643-0ED4D673858E'
END





















