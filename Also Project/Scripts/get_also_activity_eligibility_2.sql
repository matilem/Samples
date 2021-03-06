USE [netForum]
GO
/****** Object:  StoredProcedure [dbo].[get_also_activity_eligibility]    Script Date: 2/16/2018 12:33:41 PM ******/
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
	 @CustomerKey	UNIQUEIDENTIFIER,
	 @ActivityType	NVARCHAR(20)	
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
			dbo.client_aafp_m36_cme_session_x_oe_product ON client_aafp_m36_cme_session_x_oe_product.m36_m09_key = client_aafp_m09_cme_session.m09_key
		INNER JOIN 
			dbo.oe_product ON oe_product.prd_key = client_aafp_m36_cme_session_x_oe_product.m36_prd_key
		INNER JOIN 
			dbo.oe_product_ext ON oe_product_ext.prd_key_ext = oe_product.prd_key
		INNER JOIN	
			dbo.ac_invoice_detail ON ivd_cst_key = @CustomerKey
		INNER JOIN 
			dbo.ac_invoice ON ac_invoice.inv_key = ac_invoice_detail.ivd_inv_key
		INNER JOIN 
			dbo.client_aafp_m32_cme_credit_type ON client_aafp_m32_cme_credit_type.m32_key = client_aafp_m41_cme_reported_credit.m41_m32_key
	WHERE	m41_cst_key = @CustomerKey
	AND		inv_add_date > DATEADD(year,-1,GETDATE())
	AND		prd_ethos_lms_item_flag_ext = 1
	AND		m32_title = @ActivityType

	IF(@Credits > 0)
	BEGIN
		SET @Eligible = 1
	END

	SELECT @Eligible AS IsEligible
END





















