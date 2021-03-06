USE [netForum]
GO
/****** Object:  StoredProcedure [dbo].[get_cme_credit_types]    Script Date: 3/23/2017 3:15:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Jason Walker
-- Create date: 03/10/2017
-- Description:	Returns all cme credit types
-- =============================================
ALTER PROCEDURE [dbo].[get_cme_credit_types]
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT
			  m32_key						AS [Key]
			, m32_title						AS Title
			, m32_group_type				AS GroupType
			, m32_designation				AS Designation
			, m32_limit_type				AS LimitType
			, m32_max_credits_per_cycle     AS MaximumCreditsPerCycle
	FROM
			client_aafp_m32_cme_credit_type WITH (NOLOCK)
	WHERE
			m32_delete_flag = 0
END


















