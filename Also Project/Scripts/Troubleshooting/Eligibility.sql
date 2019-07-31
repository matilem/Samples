DECLARE	@ActivityNumber	NVARCHAR(20)
DECLARE @ActivityType	NVARCHAR(20)
DECLARE @CustomerKey	UNIQUEIDENTIFIER
DECLARE @CustomerId		INT		
DECLARE @Credits	INT
DECLARE @Eligible	INT

SET	@ActivityNumber = '80103'--'77397'
SET	@CustomerKey = '446c8024-2633-4364-9c83-b50cd58d2666'
SET	@CustomerId = 7222145
SET	@Credits = 0
SET	@Eligible = 0
SET	@ActivityType = 'ALSO Provider'


--SELECT	*
--FROM	dbo.client_aafp_m31_cme_activity
--WHERE	m31_number = 80799


----7222145/80799
--SELECT	cst_id,cst_name_cp,*
--FROM	dbo.co_customer
--WHERE	cst_id = @CustomerId
	
	--SELECT		  
	--	  b29_key							AS LearnerKey 
	--	, cst_key							AS CustomerKey
	--	, cst_id							AS CustomerId
	--	, ind_first_name					AS FirstName
	--	, ind_last_name						AS LastName
	--	, b29_b30_key						AS OccupationKey
	--	, b29_passed_flag					AS PassedFlag
	--	, b29_failed_flag					AS FailedFlag
	--	, b29_no_show_flag					AS NoShowFlag
	--FROM	
	--	dbo.ac_invoice_detail
	--	INNER JOIN dbo.ac_invoice ON ac_invoice.inv_key = ac_invoice_detail.ivd_inv_key
	--	INNER JOIN dbo.co_customer ON co_customer.cst_key = ac_invoice.inv_cst_key
	--	INNER JOIN dbo.co_individual ON co_individual.ind_cst_key = co_customer.cst_key
	--	INNER JOIN dbo.oe_price ON oe_price.prc_key = ac_invoice_detail.ivd_prc_key AND oe_price.prc_prd_key = ac_invoice_detail.ivd_prc_prd_key AND oe_price.prc_prd_ptp_key = ac_invoice_detail.ivd_prc_prd_ptp_key
	--	LEFT OUTER JOIN dbo.client_aafp_b29_also_learner ON client_aafp_b29_also_learner.b29_cst_key = co_customer.cst_key
	--WHERE				
	--	prc_code = @ActivityNumber
		

--SELECT	*
--FROM	dbo.co_customer
--WHERE	cst_id = '8152493'

SELECT	prd_name,inv_cst_key,*
FROM	dbo.client_aafp_m41_cme_reported_credit
		INNER JOIN dbo.client_aafp_m09_cme_session ON client_aafp_m09_cme_session.m09_key = client_aafp_m41_cme_reported_credit.m41_m09_key
		INNER JOIN dbo.client_aafp_m36_cme_session_x_oe_product ON client_aafp_m36_cme_session_x_oe_product.m36_m09_key = client_aafp_m09_cme_session.m09_key
		INNER JOIN dbo.oe_product ON oe_product.prd_key = client_aafp_m36_cme_session_x_oe_product.m36_prd_key
		INNER JOIN dbo.oe_product_ext ON oe_product_ext.prd_key_ext = oe_product.prd_key
		INNER JOIN	dbo.ac_invoice_detail ON ivd_cst_key = @CustomerKey
		INNER JOIN dbo.ac_invoice ON ac_invoice.inv_key = ac_invoice_detail.ivd_inv_key
		INNER JOIN dbo.client_aafp_m32_cme_credit_type ON client_aafp_m32_cme_credit_type.m32_key = client_aafp_m41_cme_reported_credit.m41_m32_key
WHERE	m41_cst_key = @CustomerKey
AND		inv_add_date > DATEADD(year,-1,GETDATE())
AND		prd_ethos_lms_item_flag_ext = 1
AND		m32_title = @ActivityType



--SELECT	prd_code,prd_name, *
--FROM	dbo.ac_invoice
--		LEFT JOIN dbo.ac_invoice_detail ON ac_invoice_detail.ivd_inv_key = ac_invoice.inv_key
--		LEFT JOIN dbo.oe_product ON ivd_prc_prd_key = prd_key
--		LEFT JOIN dbo.oe_product_ext ON oe_product_ext.prd_key_ext = oe_product.prd_key
--		LEFT OUTER JOIN dbo.client_aafp_m36_cme_session_x_oe_product ON client_aafp_m36_cme_session_x_oe_product.m36_prd_key = oe_product.prd_key
--		LEFT OUTER JOIN dbo.client_aafp_m09_cme_session ON client_aafp_m09_cme_session.m09_key = client_aafp_m36_cme_session_x_oe_product.m36_m09_key
--		LEFT JOIN dbo.client_aafp_m41_cme_reported_credit ON client_aafp_m41_cme_reported_credit.m41_m09_key = client_aafp_m09_cme_session.m09_key
--WHERE	inv_cst_key = @CustomerKey
--AND		inv_add_date > DATEADD(year,-1,GETDATE())
--AND		prd_ethos_lms_item_flag_ext = 1

--SELECT	m41_m09_key, m41_m32_key,*
--FROM	dbo.client_aafp_m41_cme_reported_credit
--WHERE	m41_cst_key = @CustomerKey

SELECT		  
		  @Credits = COUNT(m09_key)
FROM	dbo.client_aafp_m41_cme_reported_credit
		INNER JOIN dbo.client_aafp_m09_cme_session ON client_aafp_m09_cme_session.m09_key = client_aafp_m41_cme_reported_credit.m41_m09_key
		INNER JOIN dbo.client_aafp_m36_cme_session_x_oe_product ON client_aafp_m36_cme_session_x_oe_product.m36_m09_key = client_aafp_m09_cme_session.m09_key
		INNER JOIN dbo.oe_product ON oe_product.prd_key = client_aafp_m36_cme_session_x_oe_product.m36_prd_key
		INNER JOIN dbo.oe_product_ext ON oe_product_ext.prd_key_ext = oe_product.prd_key
		INNER JOIN	dbo.ac_invoice_detail ON ivd_cst_key = @CustomerKey
		INNER JOIN dbo.ac_invoice ON ac_invoice.inv_key = ac_invoice_detail.ivd_inv_key
		INNER JOIN dbo.client_aafp_m32_cme_credit_type ON client_aafp_m32_cme_credit_type.m32_key = client_aafp_m41_cme_reported_credit.m41_m32_key
WHERE	m41_cst_key = @CustomerKey
AND		inv_add_date > DATEADD(year,-1,GETDATE())
AND		prd_ethos_lms_item_flag_ext = 1
AND		m32_title = @ActivityType

	IF(@Credits > 0)
	BEGIN
		SET @Eligible = 1
	END

	SELECT @Eligible AS IsEligible

--SELECT	*
--FROM	dbo.client_aafp_m09_cme_session
--		INNER JOIN dbo.client_aafp_m31_cme_activity ON client_aafp_m31_cme_activity.m31_key = client_aafp_m09_cme_session.m09_m31_key
--WHERE	m09_key = '7AC8A81B-62F9-4F82-9525-C79D55E3BD4E'

--SELECT	*
--FROM	dbo.client_aafp_m32_cme_credit_type
--WHERE	m32_key = 'DA005DE6-CBE4-48C5-BD33-2B1801424C78'


--SELECT dbo.oe_product.prd_key, dbo.oe_product.prd_add_date, dbo.oe_product.prd_add_user, dbo.oe_product.prd_change_date, dbo.oe_product.prd_change_user, dbo.oe_product.prd_delete_flag, dbo.oe_product.prd_entity_key, dbo.oe_product.prd_allow_backorder, dbo.oe_product.prd_code, dbo.oe_product.prd_description, dbo.oe_product.prd_short_description, 
--         dbo.oe_product.prd_download_url, dbo.oe_product.prd_email, dbo.oe_product.prd_fax, dbo.oe_product.prd_featured_product_flag, dbo.oe_product.prd_mail, dbo.oe_product.prd_microfiche, dbo.oe_product.prd_web, dbo.oe_product.prd_name, dbo.oe_product.prd_online_abstract, dbo.oe_product.prd_taxable_flag, dbo.oe_product.prd_track_inventory_flag, 
--         dbo.oe_product.prd_format, dbo.oe_product_ext.prd_friendly_url_name_ext, dbo.oe_product_ext.prd_image_doc_key_ext, dbo.oe_product.prd_thumbnail_doc_key, dbo.oe_product.prd_ptc_key, dbo.oe_product.prd_ptp_key, dbo.oe_product_ext.prd_access_url_ext, dbo.oe_product_ext.prd_access_days_ext, dbo.oe_product_ext.prd_ethos_lms_item_flag_ext, 
--         dbo.oe_product_ext.prd_pcmh_item_flag_ext, dbo.oe_product_ext.prd_login_required_add_to_cart_ext, dbo.oe_product_ext.prd_pcmh_user_count
--FROM  dbo.oe_product INNER JOIN
--         dbo.oe_product_ext ON dbo.oe_product.prd_key = dbo.oe_product_ext.prd_key_ext
--WHERE (dbo.oe_product.prd_delete_flag = 0) AND (dbo.oe_product.prd_sell_online = 1) AND (GETDATE() BETWEEN ISNULL(dbo.oe_product.prd_start_date, DATEADD(DAY, - 1, GETDATE())) AND ISNULL(dbo.oe_product.prd_end_date, DATEADD(DAY, 1, GETDATE()))) AND (GETDATE() BETWEEN ISNULL(dbo.oe_product.prd_post_to_web_date, DATEADD(DAY, - 1, GETDATE())) AND 
--         ISNULL(dbo.oe_product.prd_remove_from_web_date, DATEADD(DAY, 1, GETDATE())))

	--SELECT m31_number,*		  
	--	  --@Credits = COUNT(m09_key)
	--FROM	
	--	dbo.client_aafp_m41_cme_reported_credit
	--	INNER JOIN
	--		dbo.client_aafp_m09_cme_session ON client_aafp_m09_cme_session.m09_key = client_aafp_m41_cme_reported_credit.m41_m09_key
	--	INNER JOIN 
	--		dbo.co_customer ON co_customer.cst_key = client_aafp_m41_cme_reported_credit.m41_cst_key
	--		INNER JOIN dbo.client_aafp_m31_cme_activity ON client_aafp_m31_cme_activity.m31_key = client_aafp_m09_cme_session.m09_m31_key
	--WHERE	cst_key = @CustomerKey	


--SELECT	*
--FROM	dbo.client_aafp_m34_log
--WHERE	m34_date > GETDATE() - 2
--ORDER BY m34_date DESC

--The INSERT statement conflicted with the CHECK constraint "CK_oe_price_1". The conflict occurred in database "netForum", table "dbo.oe_price".
--The statement has been terminated.

--SELECT m31_begin_date, m31_end_date,*
--FROM	dbo.client_aafp_m31_cme_activity
--WHERE	m31_number = 79747


--DECLARE	 @ActivityNumber	NVARCHAR(20)	
--SET	@ActivityNumber = '80103'--'77397'
	
--	SELECT		  
--		  --b29_key							AS LearnerKey 
--		  cst_key							AS CustomerKey
--		, cst_id							AS CustomerId
--		, cst_name_cp
--		, prc_code
--		, prc_key
--		, prc_display_name
--		, prd_name
		
--		--, b29_b30_key						AS OccupationKey
--		--, b29_passed_flag					AS PassedFlag
--		--, b29_failed_flag					AS FailedFlag
--		--, b29_no_show_flag					AS NoShowFlag
--		,ac_invoice_detail.*
--	FROM	
--		dbo.ac_invoice_detail
--		INNER JOIN dbo.ac_invoice ON ac_invoice.inv_key = ac_invoice_detail.ivd_inv_key
--		INNER JOIN dbo.co_customer ON co_customer.cst_key = ac_invoice.inv_cst_key
--		INNER JOIN dbo.oe_price ON oe_price.prc_key = ac_invoice_detail.ivd_prc_key AND oe_price.prc_prd_key = ac_invoice_detail.ivd_prc_prd_key AND oe_price.prc_prd_ptp_key = ac_invoice_detail.ivd_prc_prd_ptp_key
--		INNER JOIN dbo.oe_product ON oe_product.prd_key = oe_price.prc_prd_key AND oe_product.prd_ptp_key = oe_price.prc_prd_ptp_key AND oe_product.prd_atc_key = oe_price.prc_prd_atc_key
--	WHERE				
--		--prc_code = @ActivityNumber
--		cst_key = 'F7A894E7-448A-413B-9519-BDA89B209299'
--		AND	prc_code = @ActivityNumber
		

----SELECT	*
----FROM	dbo.co_customer
----WHERE	cst_id = '8152493'

--DECLARE	 
--		@CustomerKey	UNIQUEIDENTIFIER	

--SET	@CustomerKey = 'F7A894E7-448A-413B-9519-BDA89B209299'
	

--	SELECT m31_number,m31_title, *		  
--		  --@Credits = COUNT(m09_key)
--	FROM	
--		dbo.client_aafp_m41_cme_reported_credit
--		INNER JOIN
--			dbo.client_aafp_m09_cme_session ON client_aafp_m09_cme_session.m09_key = client_aafp_m41_cme_reported_credit.m41_m09_key
--		INNER JOIN 
--			dbo.co_customer ON co_customer.cst_key = client_aafp_m41_cme_reported_credit.m41_cst_key
--			INNER JOIN dbo.client_aafp_m31_cme_activity ON client_aafp_m31_cme_activity.m31_key = client_aafp_m09_cme_session.m09_m31_key
--			INNER JOIN dbo.client_aafp_m01_cme_application ON m31_m01_key = m01_key
			
--	WHERE	cst_key = @CustomerKey			
--	--AND		m09_key = @SessionKey

--	SELECT	*
--	FROM	dbo.client_aafp_m32_cme_credit_type
--	WHERE	m32_key = 'DA005DE6-CBE4-48C5-BD33-2B1801424C78'

--	--IF(@Credits > 0)
--	--BEGIN
--	--	SET @Eligible = 1
--	--END

--	--SELECT @Eligible AS IsEligible


----SELECT	m41_m09_key,*
----FROM	dbo.client_aafp_m41_cme_reported_credit
----WHERE	m41_cst_key = 'F7A894E7-448A-413B-9519-BDA89B209299'

----SELECT	m09_m31_key, *
----FROM	dbo.client_aafp_m09_cme_session
----WHERE	m09_key = '7AC8A81B-62F9-4F82-9525-C79D55E3BD4E'

----SELECT	m31_m01_key, *
----FROM	dbo.client_aafp_m31_cme_activity
----WHERE	m31_key = '9B2A2E2A-3152-4318-BF43-AF808F86EE17'

----SELECT	*
----FROM	dbo.client_aafp_m01_cme_application
----WHERE	m01_key = '02D72FAB-2718-4C27-9D96-3634B79CEC41'


----SELECT	m31_number, m01_key, m09_key,*
----FROM	dbo.client_aafp_m31_cme_activity
----		INNER JOIN dbo.client_aafp_m09_cme_session ON client_aafp_m09_cme_session.m09_m31_key = client_aafp_m31_cme_activity.m31_key
----		INNER JOIN dbo.client_aafp_m01_cme_application ON m01_key = m31_m01_key
----WHERE	m31_number = '76385'

----SELECT	m31_number, m01_key, m09_key,*
----FROM	dbo.client_aafp_m31_cme_activity
----		INNER JOIN dbo.client_aafp_m09_cme_session ON client_aafp_m09_cme_session.m09_m31_key = client_aafp_m31_cme_activity.m31_key
----		INNER JOIN dbo.client_aafp_m01_cme_application ON m01_key = m31_m01_key
----WHERE	m31_number = '80103'


