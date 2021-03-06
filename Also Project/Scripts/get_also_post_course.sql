USE [netForum]
GO
/****** Object:  StoredProcedure [dbo].[get_also_post_course]    Script Date: 2/12/2018 1:28:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ======================================================================
-- Author:		Megan Matile
-- Create date: 12/12/2017
-- Description:	Returns activity post-course information
-- ======================================================================
CREATE PROCEDURE [dbo].[get_also_post_course]
(
	 @ActivityNumber	NVARCHAR(20)	
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		  
		  b29_key							AS LearnerKey 
		, cst_key							AS CustomerKey
		, cst_id							AS CustomerId
		, ind_first_name					AS FirstName
		, ind_last_name						AS LastName
		, b29_b30_key						AS OccupationKey
		, b29_passed_flag					AS PassedFlag
		, b29_failed_flag					AS FailedFlag
		, b29_no_show_flag					AS NoShowFlag
	FROM	
		dbo.ac_invoice_detail
		INNER JOIN dbo.ac_invoice ON ac_invoice.inv_key = ac_invoice_detail.ivd_inv_key
		INNER JOIN dbo.co_customer ON co_customer.cst_key = ac_invoice.inv_cst_key
		INNER JOIN dbo.co_individual ON co_individual.ind_cst_key = co_customer.cst_key
		INNER JOIN dbo.oe_price ON oe_price.prc_key = ac_invoice_detail.ivd_prc_key AND oe_price.prc_prd_key = ac_invoice_detail.ivd_prc_prd_key AND oe_price.prc_prd_ptp_key = ac_invoice_detail.ivd_prc_prd_ptp_key
		LEFT JOIN dbo.client_aafp_b29_also_learner ON client_aafp_b29_also_learner.b29_cst_key = co_customer.cst_key
	WHERE				
		prc_code = @ActivityNumber
END