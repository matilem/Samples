USE [netForum]

--SELECT	*
--FROM	dbo.co_customer
--WHERE	cst_id = '8152494'

DECLARE		@CustomerKey	UNIQUEIDENTIFIER
SET			@CustomerKey = 'D38F3C54-6F87-4084-9FFB-D2D65D685961'--'CE48C275-C85A-4AD0-A06C-2AAD2D4D4973'

	
	SELECT DISTINCT
			  p1.prd_name							AS Title
			, p1.prd_key							AS ProductKey
			, doc_url								AS ProductImage
			, SUM(m09_number_of_prescribed_credits) AS CreditsAvailable
			, SUM(m41_prescribed_credits)			AS CreditsReported
			, CASE
				WHEN p1.prd_name LIKE '%American Family Physician%' OR p1.prd_name LIKE '%AFP%'
				THEN 'http://www.aafp.org/journals/afp.html'
				WHEN p1.prd_name LIKE '%Family Practice Management%' OR p1.prd_name LIKE '%FPM%'
				THEN 'http://www.aafp.org/journals/fpm.html'
				WHEN p1.prd_name LIKE '%Combined%'
				THEN 'http://www.aafp.org/cme/subscriptions/combined.html'
				WHEN p1.prd_name LIKE '%FPE%' OR p1.prd_name LIKE '%FP Essentials%'
				THEN 'http://www.aafp.org/cme/subscriptions/fp-essentials.html'
				WHEN p1.prd_name LIKE '%FPA%' OR p1.prd_name LIKE '%FP Audio%'
				THEN 'http://www.aafp.org/cme/subscriptions/fp-audio/editions.html'
			  END									AS AccessUrl
			, CASE
				WHEN p1.prd_name LIKE '%American Family Physician%' OR p1.prd_name LIKE '%AFP%'
				THEN '/assessment/listing/048ef0fd-580c-4106-b871-1e2442c49db3'
				WHEN p1.prd_name LIKE '%Family Practice Management%' OR p1.prd_name LIKE '%FPM%'
				THEN '/assessment/listing/edb49c7c-72eb-46d1-a266-2b00ad6ea6ca'
				WHEN p1.prd_name LIKE '%Combined%'
				THEN 'http://www.aafp.org/cme/subscriptions/combined.html'
				WHEN p1.prd_name LIKE '%FPE%' OR p1.prd_name LIKE '%FP Essentials%'
				THEN '/assessment/listing/f4fd6d68-19ad-43d9-9f66-71dba400fc74'
				WHEN p1.prd_name LIKE '%FPA%' OR p1.prd_name LIKE '%FP Audio%'
				THEN '/assessment/listing/ed7fb3f4-83a7-4be8-850a-e8d0fe13fcd4'
			  END									AS ClaimCreditUrl
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
			oe_product_ext p1Ext WITH (NOLOCK) ON p1.prd_key = p1Ext.prd_key_ext
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
			co_document WITH (NOLOCK) ON p1Ext.prd_image_doc_key_ext = doc_key
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
			, CASE
				WHEN p1.prd_name LIKE '%American Family Physician%' OR p1.prd_name LIKE '%AFP%'
				THEN 'http://www.aafp.org/journals/afp.html'
				WHEN p1.prd_name LIKE '%Family Practice Management%' OR p1.prd_name LIKE '%FPM%'
				THEN 'http://www.aafp.org/journals/fpm.html'
				WHEN p1.prd_name LIKE '%Combined%'
				THEN 'http://www.aafp.org/cme/subscriptions/combined.html'
				WHEN p1.prd_name LIKE '%FPE%' OR p1.prd_name LIKE '%FP Essentials%'
				THEN 'http://www.aafp.org/cme/subscriptions/fp-essentials.html'
				WHEN p1.prd_name LIKE '%FPA%' OR p1.prd_name LIKE '%FP Audio%'
				THEN 'http://www.aafp.org/cme/subscriptions/fp-audio/editions.html'
			  END									AS AccessUrl
			, CASE
				WHEN p1.prd_name LIKE '%American Family Physician%' OR p1.prd_name LIKE '%AFP%'
				THEN '/assessment/listing/048ef0fd-580c-4106-b871-1e2442c49db3'
				WHEN p1.prd_name LIKE '%Family Practice Management%' OR p1.prd_name LIKE '%FPM%'
				THEN '/assessment/listing/edb49c7c-72eb-46d1-a266-2b00ad6ea6ca'
				WHEN p1.prd_name LIKE '%Combined%'
				THEN 'http://www.aafp.org/cme/subscriptions/combined.html'
				WHEN p1.prd_name LIKE '%FPE%' OR p1.prd_name LIKE '%FP Essentials%'
				THEN '/assessment/listing/f4fd6d68-19ad-43d9-9f66-71dba400fc74'
				WHEN p1.prd_name LIKE '%FPA%' OR p1.prd_name LIKE '%FP Audio%'
				THEN '/assessment/listing/ed7fb3f4-83a7-4be8-850a-e8d0fe13fcd4'
			  END									AS ClaimCreditUrl
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
			oe_product_ext p1Ext WITH (NOLOCK) ON p1.prd_key = p1Ext.prd_key_ext
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
			co_document WITH (NOLOCK) ON p1Ext.prd_image_doc_key_ext = doc_key
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
			, './uploads/afp20140515-cover.jpg'			AS ProductImage			
			, SUM(m09_number_of_prescribed_credits)		AS CreditsAvailable
			, SUM(m41_prescribed_credits)				AS CreditsReported
			, 'http://www.aafp.org/journals/afp.html'	AS AccessUrl
			, '/assessment/listing/048ef0fd-580c-4106-b871-1e2442c49db3'	AS ClaimCreditUrl
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
	 AND	m09_begin_date < GETDATE() -- added as part of DEV-626
	 AND	m09_end_date > DateAdd(DAY, 1, GETDATE())
	 AND	m02_code = 'Approved'