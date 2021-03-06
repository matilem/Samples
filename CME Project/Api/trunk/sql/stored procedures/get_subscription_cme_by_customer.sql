USE [netForum]
GO
/****** Object:  StoredProcedure [dbo].[get_subscription_cme_by_customer]    Script Date: 4/3/2017 7:49:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--exec get_subscription_cme_by_customer 'D918E875-CD32-4568-B487-ED12C09FADA1'

-- =============================================
-- Author:		Jason Walker
-- Create date: 03/10/2017
-- Description:	Returns all subscription-based CME for the specified customer
-- =============================================
ALTER PROCEDURE [dbo].[get_subscription_cme_by_customer]
(
	  @CustomerKey	UNIQUEIDENTIFIER
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT DISTINCT
			  p1.prd_name							AS Title
			, p1.prd_key							AS ProductKey
			, doc_url								AS ProductImage
			, SUM(m09_number_of_prescribed_credits) AS CreditsAvailable
			, SUM(m41_prescribed_credits)			AS CreditsReported
	FROM 
			dbo.su_fulfillment WITH (NOLOCK)
	 INNER JOIN
			dbo.ac_invoice_detail_term WITH (NOLOCK) ON sfl_trm_ivd_key = trm_ivd_key
				AND trm_delete_flag = 0
				AND trm_cancel_flag = 0
	 INNER JOIN 
			dbo.ac_invoice_detail WITH (NOLOCK) ON ivd_key = sfl_trm_ivd_key
				AND ivd_delete_flag = 0
				AND ivd_void_flag = 0
	 INNER JOIN 
			dbo.oe_product p WITH (NOLOCK) ON sfl_sui_prd_key = p.prd_key
				AND p.prd_delete_flag = 0
	 INNER JOIN 
			dbo.oe_product_ext WITH (NOLOCK) ON p.prd_key = prd_key_ext
	 INNER JOIN 
			dbo.co_customer WITH (NOLOCK) ON cst_key = ivd_cst_ship_key
	 INNER JOIN
			su_subscription_issue WITH (NOLOCK) ON sfl_sui_prd_key = sui_prd_key
	 INNER JOIN
			su_subscription WITH (NOLOCK) ON sui_sub_prd_key = sub_prd_key
	 INNER JOIN
			oe_product p1 WITH (NOLOCK) ON sub_prd_key = p1.prd_key
	 INNER JOIN
			client_aafp_m36_cme_session_x_oe_product WITH (NOLOCK) ON p.prd_key = m36_prd_key
	 INNER JOIN
			client_aafp_m09_cme_session WITH (NOLOCK) ON m36_m09_key = m09_key
	 INNER JOIN
			client_aafp_m31_cme_activity WITH (NOLOCK) ON m09_m31_key = m31_key
	 INNER JOIN
			client_aafp_m01_cme_application WITH (NOLOCK) ON m31_m01_key = m01_key
	 INNER JOIN
			client_aafp_m02_cme_workflow_step WITH (NOLOCK) ON m01_m02_key = m02_key
	 LEFT OUTER JOIN
			co_document WITH (NOLOCK) ON p1.prd_thumbnail_doc_key = doc_key
	 LEFT OUTER JOIN
			client_aafp_m41_cme_reported_credit WITH (NOLOCK) ON m09_key = m41_m09_key
				AND cst_key = m41_cst_key
	WHERE
			cst_key = @CustomerKey
	 AND	m09_end_date > DateAdd(DAY, 1, GETDATE())
	 AND	m02_code = 'Approved'
	GROUP BY
			  p1.prd_name
			, p1.prd_key
			, m31_number_of_prescribed_credits
			, doc_url


UNION


	 SELECT DISTINCT
			  p1.prd_name							AS Title
			, p1.prd_key							AS ProductKey
			, doc_url								AS ProductImage
			, SUM(m09_number_of_prescribed_credits) AS CreditsAvailable
			, SUM(m41_prescribed_credits)			AS CreditsReported
	 FROM 
			dbo.oe_fulfillment WITH (NOLOCK)
	 INNER JOIN
			dbo.ac_invoice_detail WITH (NOLOCK) ON ful_ivd_key = ivd_key
				AND ivd_delete_flag = 0
				AND ivd_void_flag = 0
	 INNER JOIN 
			dbo.oe_product p WITH (NOLOCK) ON ful_prd_key = p.prd_key
				AND p.prd_delete_flag = 0
	 INNER JOIN 
			dbo.oe_product_ext WITH (NOLOCK) ON p.prd_key = prd_key_ext
	 INNER JOIN 
			dbo.co_customer WITH (NOLOCK) ON cst_key = ivd_cst_ship_key
	 INNER JOIN
			su_subscription_issue WITH (NOLOCK) ON ful_prd_key = sui_prd_key
	 INNER JOIN
			su_subscription WITH (NOLOCK) ON sui_sub_prd_key = sub_prd_key
	 INNER JOIN
			oe_product p1 WITH (NOLOCK) ON sub_prd_key = p1.prd_key
	 INNER JOIN
			client_aafp_m36_cme_session_x_oe_product WITH (NOLOCK) ON p.prd_key = m36_prd_key
	 INNER JOIN
			client_aafp_m09_cme_session WITH (NOLOCK) ON m36_m09_key = m09_key
	 INNER JOIN
			client_aafp_m31_cme_activity WITH (NOLOCK) ON m09_m31_key = m31_key
	 INNER JOIN
			client_aafp_m01_cme_application WITH (NOLOCK) ON m31_m01_key = m01_key
	 INNER JOIN
			client_aafp_m02_cme_workflow_step WITH (NOLOCK) ON m01_m02_key = m02_key
	 LEFT OUTER JOIN
			co_document WITH (NOLOCK) ON p1.prd_thumbnail_doc_key = doc_key
	 LEFT OUTER JOIN
			client_aafp_m41_cme_reported_credit WITH (NOLOCK) ON m09_key = m41_m09_key
				AND cst_key = m41_cst_key
	WHERE
			cst_key = @CustomerKey
	 AND	m09_end_date > DateAdd(DAY, 1, GETDATE())
	 AND	m02_code = 'Approved'
	GROUP BY
			  p1.prd_name
			, p1.prd_key
			, m31_number_of_prescribed_credits
			, doc_url


	UNION


	SELECT
			'American Family Physician'					AS Subscription
			, '11111111-1111-1111-1111-111111111111'	AS ProductKey
			, './photos/afpcover-th.jpg'				AS ProductImage			
			, SUM(m09_number_of_prescribed_credits)		AS CreditsAvailable
			, SUM(m41_prescribed_credits)				AS CreditsReported
	FROM
			client_aafp_m09_cme_session WITH (NOLOCK)
	 INNER JOIN
			client_aafp_m31_cme_activity WITH (NOLOCK) ON m09_m31_key = m31_key
	 INNER JOIN
			client_aafp_m01_cme_application WITH (NOLOCK) ON m31_m01_key = m01_key
	 INNER JOIN
			client_aafp_m02_cme_workflow_step WITH (NOLOCK) ON m01_m02_key = m02_key
	 LEFT OUTER JOIN
			client_aafp_m41_cme_reported_credit WITH (NOLOCK) ON m09_key = m41_m09_key
				and m41_cst_key = @CustomerKey
	WHERE
			m31_title = 'American Family Physician'
	 AND	m09_end_date > DateAdd(DAY, 1, GETDATE())
	 AND	m02_code = 'Approved'
	

			
END


















