USE [netForum]
GO
/****** Object:  StoredProcedure [dbo].[get_purchased_cme_by_customer]    Script Date: 4/3/2017 7:51:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--exec get_purchased_cme_by_customer 'D918E875-CD32-4568-B487-ED12C09FADA1'

-- =============================================
-- Author:		Jason Walker
-- Create date: 03/10/2017
-- Description:	Returns all purchased CME for the specified customer
-- =============================================
ALTER PROCEDURE [dbo].[get_purchased_cme_by_customer]
(
	  @CustomerKey	UNIQUEIDENTIFIER
)
AS
BEGIN
	SET NOCOUNT ON;
	
	-- get all merchandise products
	SELECT DISTINCT
			  prd_name										AS Title
			, prd_key										AS ProductKey
			, doc_url										AS ProductImage
			, inv_trx_date									AS TransactionDate
			, prd_access_url_ext							AS AccessUrl
			, MIN(m31_end_date)								AS ExpirationDate
			, CASE
				WHEN m31_number_of_contact_credits IS NULL
				THEN SUM(m09_number_of_prescribed_credits)
				ELSE m31_number_of_contact_credits
			  END											AS CreditsAvailable
			, SUM(m41_prescribed_credits)					AS CreditsReported
	FROM
			ac_invoice WITH (NOLOCK)
	 INNER JOIN
			ac_invoice_detail WITH (NOLOCK) ON inv_key = ivd_inv_key
	 INNER JOIN
			oe_product WITH (NOLOCK) ON ivd_prc_prd_key = prd_key
	 INNER JOIN
			oe_product_ext WITH (NOLOCK) ON prd_key = prd_key_ext
	 INNER JOIN
			oe_product_type WITH (NOLOCK) ON prd_ptp_key = ptp_key	 
	 INNER JOIN
			client_aafp_m36_cme_session_x_oe_product WITH (NOLOCK) ON prd_key = m36_prd_key
	 INNER JOIN
			client_aafp_m09_cme_session WITH (NOLOCK) ON m36_m09_key = m09_key
	 INNER JOIN
			client_aafp_m31_cme_activity WITH (NOLOCK) ON m09_m31_key = m31_key
	 INNER JOIN
			client_aafp_m01_cme_application WITH (NOLOCK) ON m31_m01_key = m01_key
	 INNER JOIN
			client_aafp_m02_cme_workflow_step WITH (NOLOCK) ON m01_m02_key = m02_key
	 LEFT OUTER JOIN
			ac_return WITH (NOLOCK) ON ivd_key = ret_ivd_key
	 LEFT OUTER JOIN
			co_document WITH (NOLOCK) ON prd_thumbnail_doc_key = doc_key
	 LEFT OUTER JOIN
			client_aafp_m41_cme_reported_credit WITH (NOLOCK) ON m09_key = m41_m09_key
				AND inv_cst_key = m41_cst_key
	WHERE
			inv_cst_key = 'D918E875-CD32-4568-B487-ED12C09FADA1'
	 AND	m31_end_date > DateAdd(DAY, 1, GETDATE())
	 AND	m02_code = 'Approved'
	 AND	ptp_code = 'Merchandise'
	 AND	ivd_void_flag = 0
	 AND	ret_key IS NULL
	GROUP BY
			  prd_name
			, inv_trx_date
			, prd_access_url_ext
			, m31_number_of_contact_credits
			, prd_key
			, doc_url



	UNION

	
	-- get all event products
	SELECT DISTINCT
			  m31_title										AS Title
			, prd_key										AS ProductKey
			, ''										    AS ProductImage
			, reg_registration_date							AS TransactionDate
			, prd_access_url_ext							AS AccessUrl
			, MIN(m31_end_date)								AS ExpirationDate
			, CASE
				WHEN m31_number_of_contact_credits IS NULL
				THEN SUM(m09_number_of_prescribed_credits)
				ELSE m31_number_of_contact_credits
			  END											AS CreditsAvailable
			, SUM(m41_prescribed_credits)					AS CreditsReported
	FROM
			ev_registrant WITH (NOLOCK)
	 INNER JOIN
			ev_event WITH (NOLOCK) ON reg_evt_key = evt_key
	 INNER JOIN
			ev_event_fee WITH (NOLOCK) ON evt_key = fee_evt_key
	 INNER JOIN
			oe_product WITH (NOLOCK) ON fee_prd_key = prd_key
	 INNER JOIN
			oe_product_ext WITH (NOLOCK) ON prd_key = prd_key_ext
	 INNER JOIN
			client_aafp_m36_cme_session_x_oe_product WITH (NOLOCK) ON prd_key = m36_prd_key
	 INNER JOIN
			client_aafp_m09_cme_session WITH (NOLOCK) ON m36_m09_key = m09_key
	 INNER JOIN
			client_aafp_m31_cme_activity WITH (NOLOCK) ON m09_m31_key = m31_key
	 INNER JOIN
			client_aafp_m01_cme_application WITH (NOLOCK) ON m31_m01_key = m01_key
	 INNER JOIN
			client_aafp_m02_cme_workflow_step WITH (NOLOCK) ON m01_m02_key = m02_key
	 LEFT OUTER JOIN
			client_aafp_m41_cme_reported_credit WITH (NOLOCK) ON m09_key = m41_m09_key
				AND reg_cst_key = m41_cst_key
	WHERE
			reg_cst_key = 'D918E875-CD32-4568-B487-ED12C09FADA1'
	 AND	m31_end_date > DateAdd(DAY, 1, GETDATE())
	 AND	m02_code = 'Approved'
	 AND	reg_cancel_date IS NULL
	GROUP BY
			  m31_title
			, reg_registration_date
			, prd_access_url_ext
			, m31_number_of_contact_credits
			, prd_key
			
END


















