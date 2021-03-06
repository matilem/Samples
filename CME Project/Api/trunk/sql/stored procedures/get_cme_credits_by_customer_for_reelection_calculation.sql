USE [netForum]
GO
/****** Object:  StoredProcedure [dbo].[get_cme_credits_by_customer_for_reelection_calculation]    Script Date: 3/23/2017 8:39:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Jason Walker
-- Create date: 03/10/2017
-- Description:	Returns credits for specified user for re-election calculation
-- =============================================
CREATE PROCEDURE [dbo].[get_cme_credits_by_customer_for_reelection_calculation]
(
	  @CustomerKey	UNIQUEIDENTIFIER
	, @StartYear	INT
	, @EndYear		INT
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT
			  m41_key 
			, m41_prescribed_credits		AS PrescribedCredits
			, m41_elective_credits			AS ElectiveCredits
			, m31_key						AS ActivityKey
			, m31_fl_chapter_approved_flag	AS FloridaChapterApproved
			, m31_md_chapter_approved_flag	AS MarylandChapterApproved
			, m09_key						AS SessionKey
			, m01_activity_sub_type			AS ActivitySubType
			, m18_type						AS ActivityType
			, m32_key						AS CreditTypeKey
			, m32_title						AS CreditTypeTitle
			, m32_group_type				AS CreditTypeGroupType
			, m32_designation				AS CreditTypeDesignation
			, m32_limit_type				AS CreditTypeLimitType
	FROM
			client_aafp_m41_cme_reported_credit WITH (NOLOCK)
	 LEFT OUTER JOIN
			client_aafp_m32_cme_credit_type WITH (NOLOCK) ON m41_m32_key = m32_key
	 LEFT OUTER JOIN
			client_aafp_m09_cme_session WITH (NOLOCK) ON m41_m09_key = m09_key
	 LEFT OUTER JOIN
			client_aafp_m31_cme_activity WITH (NOLOCK) ON m09_m31_key = m31_key
	 LEFT OUTER JOIN
			client_aafp_m01_cme_application WITH (NOLOCK) ON m31_m01_key = m01_key
	 LEFT OUTER JOIN
			client_aafp_m02_cme_workflow_step WITH (NOLOCK) ON m01_m02_key = m02_key
	 LEFT OUTER JOIN
			client_aafp_m18_cme_activity_type WITH (NOLOCK) ON m31_m18_key = m18_key
	WHERE
			m41_cst_key = @CustomerKey
	 AND	(m09_delete_flag IS NULL OR m09_delete_flag = 0)
	 AND	(m09_approval_flag IS NULL OR m09_approval_flag = 1)
	 AND	(m31_delete_flag IS NULL OR m31_delete_flag = 0)
	 AND	(m02_code IS NULL OR m02_code = 'Approved')
	 AND	YEAR(m41_participation_begin_date) BETWEEN @StartYear AND @EndYear
END


















