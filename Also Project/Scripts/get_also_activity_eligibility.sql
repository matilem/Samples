USE [netForum]
GO
/****** Object:  StoredProcedure [dbo].[get_also_activity_credit]    Script Date: 2/7/2018 2:49:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- ======================================================================
-- Author:		Megan Matile
-- Create date: 12/12/2017
-- Description:	Returns also activity session
-- ======================================================================
CREATE PROCEDURE [dbo].[get_also_activity_eligibility]
(
	 @SessionKey	UNIQUEIDENTIFIER,
	 @CustomerKey	UNIQUEIDENTIFIER	
)
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @Credits	INT
	DECLARE @Eligible	INT
	SET		@Credits = 0
	SET		@Eligible = 0

	SELECT		  
		  @Credits = COUNT(m09_key)
	FROM	
		dbo.client_aafp_m41_cme_reported_credit
		INNER JOIN
			dbo.client_aafp_m09_cme_session ON client_aafp_m09_cme_session.m09_key = client_aafp_m41_cme_reported_credit.m41_m09_key
		INNER JOIN 
			dbo.co_customer ON co_customer.cst_key = client_aafp_m41_cme_reported_credit.m41_cst_key
	WHERE	cst_key = @CustomerKey			
	AND		m09_key = @SessionKey

	IF(@Credits > 0)
	BEGIN
		SET @Eligible = 1
	END

	SELECT @Eligible AS IsEligible
END





















