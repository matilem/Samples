USE [netForumDev]
GO

/****** Object:  StoredProcedure [dbo].[client_aafp_event_get_fees_by_customer]    Script Date: 3/18/2016 2:46:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Jason Walker
-- Create date: 02/05/2016
-- Description:	Copy of ev_registrant_select_event_fees procedure with customizations.
-- =============================================
CREATE PROCEDURE [dbo].[client_aafp_event_get_fees_by_customer] 
(
	  @CustomerKey		UNIQUEIDENTIFIER
	, @EventKey			UNIQUEIDENTIFIER
	, @RegistrationDate	DATETIME
)

AS

SET NOCOUNT ON

DECLARE @NumberOfRegistrants INT = 1

CREATE TABLE #attributes 
(
	  cst_key UNIQUEIDENTIFIER NULL
	, cst_type NVARCHAR(20)
	, cst_member_flag INT NULL
	, cst_attribute_1 NVARCHAR(510) NULL
	, cst_attribute_2 NVARCHAR(510) NULL
	, mbr_mbt_key UNIQUEIDENTIFIER NULL
	, mbr_mbs_key UNIQUEIDENTIFIER NULL
	, ogt_key UNIQUEIDENTIFIER NULL
	, org_nonprofit_flag INT NULL
	, int_key UNIQUEIDENTIFIER NULL
	, adr_city NVARCHAR(40) NULL
	, adr_state NVARCHAR(40) NULL
	, adr_county NVARCHAR(40) NULL
	, adr_post_code NVARCHAR(20) NULL
	, cty_code NVARCHAR(60) NULL
	, cty_rgn_key UNIQUEIDENTIFIER NULL
	, cmc_cmt_key UNIQUEIDENTIFIER
	, cmc_start_date DATETIME NULL
	, cmc_end_date DATETIME NULL
	, mld_mls_key UNIQUEIDENTIFIER
	, mld_start_date DATETIME NULL
	, mld_end_date DATETIME NULL
)

CREATE TABLE #results
(
	  results_key INT IDENTITY
	, prd_key UNIQUEIDENTIFIER NULL
	, prc_display_name NVARCHAR(250) NULL
	, prd_short_description NVARCHAR(400) NULL
	, prc_Key UNIQUEIDENTIFIER NULL
	, ptp_key UNIQUEIDENTIFIER NULL
	, ptp_online_code NVARCHAR(50) NULL
	, prc_price MONEY NULL
	, prc_available INT NULL
	, prc_membertypeapplies INT NULL
	, prc_status INT NULL
	, member_type NVARCHAR(50) NULL
	, efc_code NVARCHAR(50) NULL
	, pat_prc_key UNIQUEIDENTIFIER NULL
	, pat_cst_type NVARCHAR(20) NULL
	, pat_member_flag INT NULL
	, pat_mbt_key UNIQUEIDENTIFIER NULL
	, pat_mbs_key UNIQUEIDENTIFIER NULL
	, pat_src_key UNIQUEIDENTIFIER NULL
	, pat_ogt_key UNIQUEIDENTIFIER NULL
	, pat_int_key UNIQUEIDENTIFIER NULL
	, pat_org_nonprofit_flag INT NULL
	, pat_start_date DATETIME NULL
	, pat_end_date DATETIME NULL
	, pat_rgt_key UNIQUEIDENTIFIER NULL
	, pat_fee_class NVARCHAR(10) NULL
	, pat_default_flag INT NULL
	, pat_delete_flag INT NULL
	, pat_county NVARCHAR(40) NULL
	, pat_state NVARCHAR(40) NULL
	, pat_city NVARCHAR(40) NULL
	, pat_zip_code_start NVARCHAR(10) NULL
	, pat_zip_code_end NVARCHAR(10) NULL
	, pat_country NVARCHAR(60) NULL
	, pat_rgn_key UNIQUEIDENTIFIER NULL
	, prd_sell_online INT NULL
	, prc_sell_online INT NULL
)

DECLARE @temp_table TABLE 
(
    prc_prd_key UNIQUEIDENTIFIER NULL
  , prc_key UNIQUEIDENTIFIER NULL
  , pat_start_date DATETIME NULL
  , pat_end_date DATETIME NULL
  , prc_price MONEY NULL    
)

DECLARE @temp_table_min TABLE 
(
   tmp_prc_key UNIQUEIDENTIFIER NULL 
)

	
INSERT INTO #results 
(
	  prd_key
	, prc_display_name
	, prd_short_description
	, prc_Key
	, ptp_key
	, ptp_online_code
	, prc_price
	, prc_available
	, prc_membertypeapplies
	, prc_status
	, member_type
	, efc_code
	, pat_prc_key
	, pat_cst_type
	, pat_member_flag
	, pat_mbt_key
	, pat_mbs_key
	, pat_src_key
	, pat_ogt_key
	, pat_int_key
	, pat_org_nonprofit_flag
	, pat_start_date
	, pat_end_date
	, pat_rgt_key
	, pat_fee_class
	, pat_default_flag
	, pat_delete_flag
	, prc_sell_online
	, prd_sell_online
)
SELECT DISTINCT
		  prd_key
		, prc_display_name
		, prd_short_description
		, prc_key
		, ptp_key 
		, ptp_online_code
		, CONVERT(MONEY, ISNULL(prc_price, 0))
		, 0
		, 1 
		, NULL
		, ISNULL(mbt_code, '[Any]')
		, efc_code 
		, pat_prc_key 
		, pat_cst_type 
		, pat_member_flag 
		, pat_mbt_key 
		, pat_mbs_key 
		, pat_src_key 
		, pat_ogt_key
		, pat_int_key
		, pat_org_nonprofit_flag
		, pat_start_date 
		, pat_end_date 
		, pat_rgt_key 
		, pat_fee_class 
		, pat_default_flag 
		, pat_delete_flag
		, prc_sell_online 
		, prd_sell_online
FROM
		oe_price_attribute WITH (NOLOCK) 
 INNER JOIN 
		oe_price WITH (NOLOCK) ON prc_key = pat_prc_key 
			AND prc_delete_flag = 0  
 INNER JOIN 
		oe_product WITH (NOLOCK) ON prd_key = prc_prd_key 
			AND prd_delete_flag = 0 
 INNER JOIN 
		ev_event_fee WITH (NOLOCK) ON fee_prd_key = prd_key 
			AND fee_delete_flag = 0 
			AND fee_evt_key = @EventKey
 INNER JOIN 
		oe_product_type WITH (NOLOCK) ON ptp_key = prd_ptp_key 
 LEFT JOIN  
		mb_member_type WITH (NOLOCK) ON pat_mbt_key = mbt_key 
 LEFT JOIN 
		ev_event_fee_category WITH (NOLOCK) ON efc_key = fee_efc_key  
WHERE 
		@NumberOfRegistrants BETWEEN ISNULL(prc_qty_min, 0) AND ISNULL(prc_qty_max, 99999) 
 AND	@RegistrationDate BETWEEN ISNULL(pat_start_date, CONVERT(DATETIME, '1/1/1900')) AND ISNULL(pat_end_date, CONVERT(DATETIME, '12/31/2078')) 
 AND	@RegistrationDate BETWEEN ISNULL(prc_start_date, CONVERT(DATETIME, '1/1/1900')) AND ISNULL(prc_end_date, CONVERT(DATETIME, '12/31/2078')) 
	
		
INSERT INTO #attributes
(
	cst_key
	, cst_type 
	, cst_member_flag 
	, cst_attribute_1 
	, cst_attribute_2 
	, mbr_mbt_key
	, mbr_mbs_key 
	, ogt_key 
	, org_nonprofit_flag 
	, int_key 
	, adr_city 
	, adr_state 
	, adr_county 
	, adr_post_code 
	, cty_code 
	, cty_rgn_key 
	, cmc_cmt_key 
	, cmc_start_date 
	, cmc_end_date 
	, mld_mls_key 
	, mld_start_date 
	, mld_end_date
)
EXEC pc_customer_pricing_attributes_select @CustomerKey 
		
	
-- filter the product list based on customer attributes 
INSERT INTO @temp_table 
SELECT
		  prd_key
		, r.prc_Key
		, pat_start_date
		, pat_end_date 
		, r.prc_price                                     		
FROM  
		#results r  
 INNER JOIN 
		#attributes a ON a.cst_key = @CustomerKey 
			AND 
			( r.pat_default_flag = 1 
			OR (
				dbo.StringCompareNull(a.cst_type, r.pat_cst_type) = 1 
				AND dbo.GuidCompareNull(a.mbr_mbt_key, r.pat_mbt_key) = 1 
				AND dbo.GuidCompareNull(a.mbr_mbs_key, r.pat_mbs_key) = 1 
				AND dbo.IntCompareNull(a.cst_member_flag, r.pat_member_flag) = 1 
				AND dbo.IntCompareNull(a.org_nonprofit_flag, r.pat_org_nonprofit_flag) = 1 
				AND dbo.GuidCompareNull(a.int_key, r.pat_int_key) = 1 
				AND dbo.GuidCompareNull(a.ogt_key, r.pat_ogt_key) = 1
				AND dbo.StringCompareNull(a.adr_city, r.pat_city) = 1 
				AND dbo.StringCompareNull(a.adr_county, r.pat_county) = 1 
				AND dbo.StringCompareNull(a.cty_code, r.pat_country) = 1 
				AND dbo.StringCompareNull(a.adr_state, r.pat_state) = 1 
				AND dbo.GuidCompareNull(a.cty_rgn_key, r.pat_rgn_key) = 1 
				AND dbo.StringCompareNull(a.adr_county, r.pat_county) = 1 
				AND (r.pat_zip_code_start IS NULL OR ISNULL(a.adr_post_code, '00000') >= ISNULL(r.pat_zip_code_start, '00000'))
				AND (r.pat_zip_code_end IS NULL OR ISNULL(a.adr_post_code, '00000') <= ISNULL(r.pat_zip_code_end, '99999')) 					
				)
			) 
	
	

IF NOT EXISTS 
(
	SELECT 
		tmp_prc_key 
	FROM 
		@temp_table_min
) 
BEGIN
	INSERT INTO @temp_table_min
	SELECT DISTINCT 
		prc_key 
	FROM 
		@temp_table  
END
		
--populate the final result set based on @temp_table_min 
SELECT DISTINCT
		  prd_key
		, prc_display_name
		, prc_Key
		, prc_price
		, NULL AS ses_key
FROM 
		#results r 
 INNER JOIN 
		@temp_table_min t ON r.prc_key = t.tmp_prc_key 
WHERE 
		results_key IN
		(	
			SELECT TOP 1 
					results_key
			FROM 
					#results s
			 INNER JOIN 
					@temp_table_min t ON s.prc_key = t.tmp_prc_key 
			WHERE 
					r.prc_key = s.prc_key
		)
ORDER BY prc_display_name



GO


