--SELECT	m31_begin_date, m31_end_date,*
--FROM	dbo.client_aafp_m31_cme_activity
--WHERE	m31_number = 80165

--SELECT	*
--FROM	dbo.client_aafp_b28_also_course
--WHERE	b28_m31_key = '7C749968-94F3-4138-B85E-50952794F99E'



--SELECT	*
--FROM	dbo.oe_price
--ORDER BY prc_add_date DESC


--DECLARE	 @ProductCode	NVARCHAR(20)
--DECLARE	 @DiscountKey UNIQUEIDENTIFIER	
	
--	SET		@DiscountKey = (SELECT dxp_dsc_prd_key FROM dbo.oe_product RIGHT JOIN dbo.oe_discount_product_x_product ON oe_discount_product_x_product.dxp_prd_key = oe_product.prd_key WHERE prd_code = @ProductCode)
--	 	SELECT		  
--		  prd_key						AS ProductKey
--		, prd_name						AS ProductName
--		, prd_ptp_key					AS ProductTypeKey
--		, prd_atc_key					AS ProductCompanyKey
--		, *

--	FROM	dbo.oe_product	
--	WHERE				
--			prd_key =  @DiscountKey


--	SELECT	*
--	FROM	dbo.ac_gl_account
--			INNER JOIN dbo.oe_price ON oe_price.prc_gla_ar_key = ac_gl_account.gla_key
--	WHERE	gla_key = '64E77479-F5D1-4E5E-B304-D9C8CB008888'--also PROVIDER/blso
--	OR		gla_code = '102060303-230090'
--	--OR		prc_code = '77313'

--	SELECT	*
--	FROM	dbo.ac_gl_account
--	WHERE	gla_code = '102060303-230090'


--	SELECT	prc_*
--	FROM	dbo.oe_price
--	--WHERE	prc_code = '80072'
--	ORDER BY prc_add_date DESC

--	SELECT	*
--	FROM	dbo.oe_price_attribute
--	--WHERE	pat_prc_key = '308FC29C-2533-48C6-A4ED-672FD3F940CB'
--	ORDER BY pat_add_date DESC


	--DELETE	FROM dbo.oe_price
	--WHERE	prc_code = '80072'

	SELECT	*
	FROM	dbo.oe_price
	WHERE	prc_code = '80648'
	--ORDER BY prc_add_date DESC

	SELECT	*
	FROM	dbo.oe_price_attribute
	ORDER BY  pat_add_date DESC
	--WHERE	pat_prc_key = 'B2AED5EC-E898-42CC-999F-858E9BCF3CDA'
	--WHERE	pat_code = '80103'
	--ORDER BY pat_add_date DESC