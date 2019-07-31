USE [netForumDev]
GO

/****** Object:  StoredProcedure [dbo].[client_aafp_event_get_session_fees_by_customer]    Script Date: 3/18/2016 2:46:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Jason Walker
-- Create date: 02/05/2016
-- Description:	Copy of ev_registrant_select_session_fees procedure with customizations.
-- =============================================
CREATE PROCEDURE [dbo].[client_aafp_event_get_session_fees_by_customer]
(
	  @CustomerKey		UNIQUEIDENTIFIER
	, @EventKey			UNIQUEIDENTIFIER
	, @RegistrationDate DATETIME	
)
	
AS

SET NOCOUNT ON		

DECLARE @num_registrants INT = 1

DECLARE @attributes TABLE 
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
	, cmc_cmt_key UNIQUEIDENTIFIER NULL
	, cmc_start_date DATETIME NULL
	, cmc_end_date DATETIME NULL
	, mld_mls_key UNIQUEIDENTIFIER NULL
	, mld_start_date DATETIME NULL
	, mld_end_date DATETIME NULL	
)

CREATE TABLE #results 
(
	prd_key uniqueidentifier null, 
	prc_display_name nvarchar(250) null, 
	prd_short_description nvarchar(400) null, 
	prc_Key uniqueidentifier null, 
	ptp_key uniqueidentifier null,
	ptp_online_code nvarchar(50) null,
	prc_price money null,
	prc_available int null,
	prc_membertypeapplies int null,
	prc_status int null,
	[Member Type] nvarchar(50) null,
	ses_key uniqueidentifier null,
	ses_ticketed int null,
	[Capacity] int null,
	[_available] int null,
	[_reserved] int null,
	[_ticketed] int null,
	[_returned] int null,
	[Available] int null,
	ses_wait_list_flag int null, 
	ses_starts datetime null, 
	ses_ends datetime null, 
	[Starts] nvarchar(30) null,
	[Ends] nvarchar(30) null,
	[Comments] nvarchar(50) null,
	pat_prc_key uniqueidentifier null,
	pat_cst_type nvarchar(20) null,
	pat_member_flag int null,
	pat_mbt_key uniqueidentifier null,
	pat_mbs_key uniqueidentifier null,
	pat_src_key uniqueidentifier null,
	pat_ogt_key uniqueidentifier null,
	pat_int_key uniqueidentifier null,
	pat_org_nonprofit_flag int null,
	pat_start_date datetime null,
	pat_end_date datetime null,
	pat_rgt_key uniqueidentifier null,
	pat_fee_class nvarchar(10) null,
	pat_default_flag int null,
	pat_delete_flag int null,
	pat_county nvarchar(40) null,
	pat_state nvarchar(40) null,
	pat_city nvarchar(40) null,
	pat_zip_code_start nvarchar(10) null,
	pat_zip_code_end nvarchar(10) null,
	pat_country nvarchar(60) null,
	pat_rgn_key uniqueidentifier null,
	pat_attribute_1  nvarchar(510) null,
	pat_attribute_2  nvarchar(510) null,
	prd_sell_online int null,
	prc_sell_online int null,
	ses_registration_required tinyint null,
	cur_key uniqueidentifier null,	-- MC
	ses_code nvarchar(20),
	pat_mls_key uniqueidentifier null,
	pat_cmt_key uniqueidentifier null
)

DECLARE @temp_table TABLE 
(
  prc_prd_key uniqueidentifier null,
  prc_key uniqueidentifier null,
  pat_start_date datetime null,
  pat_end_date datetime null,
  prc_price money null    
)

DECLARE @temp_table_min TABLE 
(
   tmp_prc_key uniqueidentifier null 
)
	
INSERT INTO #results (
	prd_key,
	prc_display_name,
	prd_short_description,
	prc_Key,
	ptp_key,
	ptp_online_code,
	prc_price,
	prc_available,
	prc_membertypeapplies,
	prc_status,
	[Member Type],
	ses_key,
	ses_ticketed,
	[Capacity],
	ses_wait_list_flag,
	ses_starts,
	ses_ends,
	[Starts],
	[Ends],
	pat_prc_key,
	pat_cst_type,
	pat_member_flag,
	pat_mbt_key,
	pat_mbs_key,
	pat_src_key,
	pat_ogt_key,
	pat_int_key,
	pat_org_nonprofit_flag,
	pat_start_date,
	pat_end_date,
	pat_rgt_key,
	pat_fee_class,
	pat_default_flag,
	pat_delete_flag,
	pat_county,
	pat_state,
	pat_city,
	pat_zip_code_start,
	pat_zip_code_end,
	pat_country,
	pat_rgn_key,
	pat_attribute_1,
	pat_attribute_2,
	prc_sell_online,
	prd_sell_online,
	ses_registration_required,
	cur_key,
	ses_code,
	pat_mls_key,
	pat_cmt_key 
)
SELECT DISTINCT 
		  prc_prd_key
		, prc_display_name 
		, prd_short_description 
		, prc_key 
		, prc_prd_ptp_key 
		, ptp_online_code 
		, CONVERT(MONEY, ISNULL(prc_price, 0))
		, 0 
		, 1 
		, NULL 
		, ISNULL(mbt_code, '[Any]')
		, sfe_ses_key 
		, ses_ticketed 
		, ses_capacity
		, ISNULL(ses_wait_list_flag, 0)
		, CONVERT(DATETIME, CONVERT(VARCHAR, ses_start_date, 101) + CASE WHEN ses_start_time IS NULL THEN '' ELSE ' ' + ses_start_time END)
		, CONVERT(DATETIME, CONVERT(VARCHAR, ses_end_date, 101) + CASE WHEN ses_end_time IS NULL THEN '' ELSE ' ' + ses_end_time END)
		, CONVERT(VARCHAR, ses_start_date, 101) + CASE WHEN ses_start_time IS NULL THEN '' ELSE ' ' + ses_start_time END
		, CONVERT(VARCHAR, ses_end_date, 101) + CASE WHEN ses_end_time IS NULL THEN '' ELSE ' ' + ses_end_time END
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
		, pat_county 
		, pat_state 
		, pat_city 
		, pat_zip_code_start 
		, pat_zip_code_end 
		, pat_country 
		, pat_rgn_key 
		, pat_attribute_1 
		, pat_attribute_2 
		, prd_sell_online 
		, prc_sell_online 
		, ses_registration_required 
		, prc_cur_key 
		, ses_code 
		, pat_mls_key 
		, pat_cmt_key 
FROM  
		oe_price_attribute WITH (NOLOCK) 
 INNER JOIN 
		oe_price prc WITH (NOLOCK) ON prc_key = pat_prc_key  
			AND pat_delete_flag = 0 
			AND prc_delete_flag = 0 
 INNER JOIN 
		oe_product prd WITH (NOLOCK) ON prd_key = prc_prd_key  
			AND prd_delete_flag = 0 
 INNER JOIN 
		oe_product_type WITH (NOLOCK) ON ptp_key = prd_ptp_key  
 INNER JOIN 
		ev_session_fee WITH (NOLOCK) ON sfe_prd_key = prd_key  
			AND sfe_delete_flag = 0   
 INNER JOIN 
		ev_session WITH (NOLOCK) ON ses_key = sfe_ses_key 
			AND ses_delete_flag = 0 
			AND ses_evt_key = @EventKey
 LEFT OUTER JOIN 
		mb_member_type WITH (NOLOCK) ON pat_mbt_key = mbt_key 	
WHERE 	
		prc_delete_flag = 0 
 AND	pat_delete_flag = 0 
 AND	prd_delete_flag = 0 
 AND	ses_delete_flag = 0 
 AND	(ses_end_date IS NULL OR ses_end_date >= @RegistrationDate)
 AND	@num_registrants BETWEEN ISNULL(prc_qty_min, 0) AND ISNULL(prc_qty_max, 99999) 
 AND	@RegistrationDate BETWEEN ISNULL(pat_start_date, '1/1/1900') AND ISNULL(pat_end_date, '12/31/2078') 
 AND	@RegistrationDate BETWEEN ISNULL(prc_start_date, '1/1/1900') AND ISNULL(prc_end_date, '12/31/2078') 
 AND	(sfe_seats IS NULL OR sfe_seats = 1) 
	
IF EXISTS
(
	SELECT 
			* 
	FROM 
			ev_session_fee WITH (NOLOCK)
	 INNER JOIN 
			#results ON prd_key = sfe_prd_key 
				AND sfe_delete_flag = 0
)
AND EXISTS
(
	SELECT 
			* 
	FROM 
			ev_registrant_session WITH (NOLOCK) 
	 INNER JOIN 
			#results ON ses_key = rgs_ses_key 
				AND rgs_delete_flag = 0 
				AND rgs_cancel_date IS NULL
				AND Capacity IS NOT NULL
) 
BEGIN

	-- reserved
    UPDATE  #results
    SET     [_reserved] = 
		( 
			SELECT  
					COUNT(*)
            FROM    
					ev_registrant_session WITH (NOLOCK)
            WHERE   rgs_ses_key = ses.ses_key
             AND rgs_ivd_key IS NULL
             AND rgs_delete_flag = 0
             AND rgs_cancel_date IS NULL
        )
    FROM    
			#results ses
    WHERE   
			ses.[Capacity] IS NOT NULL;		
			
	-- H. Stechl 02/05/2008
	-- bug # 8426 - ivd is created regardless...			
	if exists(select * from ev_registrant_session s 
		join #results r	
			on r.ses_key=s.rgs_ses_key 
				and s.rgs_delete_flag=0 
				and s.rgs_cancel_date is null
				and r.Capacity is not null)
				--and r.ses_ticketed=1) 
	begin

		-- ticketed
		update #results
		set [_ticketed]=(select isnull(sum(ivd_qty),0) from 
				ev_registrant_session (nolock) 
			JOIN ac_invoice_detail  (NOLOCK) 
				on rgs_ses_key=ses.ses_key
					and rgs_ivd_key is not null 
					and rgs_delete_flag=0 
					and rgs_ivd_key is not null and rgs_ivd_key=ivd_key 
					and ivd_void_flag=0 
					and ivd_ajd_key is null 
					and ivd_delete_flag=0
				where rgs_key not in (select rxg_rgs_key from ev_registrant_x_grouping(nolock) 
                                where rxg_delete_flag=0 and rxg_cancel_date is null and rxg_rgs_key is not null))+
			(select isnull(count(*),0)
				from ev_registrant_session (nolock)
				join ev_registrant_x_grouping(nolock) on rgs_ses_key=ses.ses_key and rxg_rgs_key=rgs_key
				where rgs_cancel_date is null
				and rxg_cancel_date is null 
				and rgs_delete_flag = 0 
				and (rgs_on_wait_list_flag = 0 or rgs_on_wait_list_flag is null and rxg_delete_flag=0))         
		from #results ses 
		where ses.[Capacity] is not null
	end
			
	-- returned
    UPDATE  #results
    SET     [_returned] = 
		( 
			SELECT  
					SUM(ret_qty)
            FROM		
					ev_registrant_session WITH (NOLOCK)
             INNER JOIN 
					ac_invoice_detail WITH (NOLOCK) ON rgs_ses_key = ses.ses_key
                        AND rgs_ivd_key IS NOT NULL
                        AND rgs_delete_flag = 0
                        AND rgs_ivd_key = ivd_key
                        AND ivd_void_flag = 0
                        AND ivd_ajd_key IS NULL
                        AND ivd_delete_flag = 0
             INNER JOIN 
					ac_return WITH (NOLOCK) ON ret_ivd_key = ivd_key
                        AND ret_delete_flag = 0
        )
    FROM    
			#results ses
    WHERE   
			ses.[Capacity] IS NOT NULL;

	UPDATE  #results
	SET     [_reserved] = 0
	WHERE   [_reserved] IS NULL;
	UPDATE  #results
	SET     [_ticketed] = 0
	WHERE   [_ticketed] IS NULL;
	UPDATE  #results
	SET     [_returned] = 0
	WHERE   [_returned] IS NULL;
	UPDATE  #results
	SET     [_available] = [Capacity] - [_reserved] - [_ticketed] + [_returned]
	WHERE   [Capacity] IS NOT NULL;
END
	
INSERT INTO @attributes
(
	cst_key,
	cst_type,
	cst_member_flag,
	cst_attribute_1,
	cst_attribute_2,
	mbr_mbt_key,
	mbr_mbs_key,
	ogt_key,
	org_nonprofit_flag,
	int_key,
	adr_city,
	adr_state,
	adr_county,
	adr_post_code, 
	cty_code,
	cty_rgn_key,
		
	cmc_cmt_key,		
	cmc_start_date,	
	cmc_end_date,	
	mld_mls_key,
	mld_start_date,	
	mld_end_date
)
EXEC dbo.[pc_customer_pricing_attributes_select] @CustomerKey 

--ES 7/30/08 2008.01 - FIN-06
--if any attribute flips the member flag, flip all member flags so individual is no longer eligible for non-member prices
UPDATE  @attributes
SET     cst_member_flag = 
			( 
				SELECT  
					MAX(cst_member_flag)
				FROM    
					@attributes
            );


-- filter the product list based on customer attributes 
INSERT INTO @temp_table 
SELECT prd_key,prc_key,pat_start_date,pat_end_date, prc_price                                     		
FROM #results r  
	JOIN @attributes a 
		ON a.cst_key=@CustomerKey
			AND (r.pat_default_flag = 1 OR 
			(
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
			AND dbo.StringCompareNull(a.cst_attribute_1, r.pat_attribute_1) = 1
			AND dbo.StringCompareNull(a.cst_attribute_2, r.pat_attribute_2) = 1
			AND dbo.GuidCompareNull(a.cmc_cmt_key, r.pat_cmt_key) = 1
			AND dbo.GuidCompareNull(a.mld_mls_key, r.pat_mls_key) = 1
			AND (r.pat_cmt_key IS NULL OR (dbo.DateRangeCompareCommitteMailing( r.pat_start_date, r.pat_end_date, a.cmc_start_date, a.cmc_end_date, GETDATE()) = 1)) 
			AND (r.pat_mls_key IS NULL  OR(dbo.DateRangeCompareCommitteMailing( r.pat_start_date, r.pat_end_date, a.mld_start_date, a.mld_end_date, GETDATE()) = 1))		

			))

 
-- get the entire list if prd_key is not passed.
IF NOT EXISTS (SELECT tmp_prc_key FROM @temp_table_min) BEGIN
  INSERT INTO @temp_table_min
  SELECT DISTINCT prc_key FROM @temp_table  
END

--populate the final result set based on @temp_table_min 
SELECT DISTINCT
		  prd_key
		, prc_display_name 
		, prc_Key
		, prc_price
		, ses_key
FROM 
		#results r  
 INNER JOIN 
		@temp_table_min t ON r.prc_key = t.tmp_prc_key
 LEFT OUTER JOIN 
		ac_currency c WITH (NOLOCK) ON r.cur_key = c.cur_key


GO


