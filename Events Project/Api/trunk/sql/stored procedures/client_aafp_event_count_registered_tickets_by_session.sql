USE [netForumDev]
GO

/****** Object:  StoredProcedure [dbo].[client_aafp_event_count_registered_tickets_by_session]    Script Date: 3/18/2016 2:53:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO









-- ====================================================================================
-- Author:		Jason Walker
-- Create date: 11/19/2015
-- Description:	Gets number of registrants for specific session
-- ====================================================================================

CREATE PROCEDURE [dbo].[client_aafp_event_count_registered_tickets_by_session]
          @SessionKey	UNIQUEIDENTIFIER
AS

BEGIN


SELECT 
		SUM(CAST(ISNULL(net_balance_quantity, 1) AS INT))
FROM
		ev_registrant_session WITH (NOLOCK) 
 LEFT JOIN 
		vw_ac_invoice_detail WITH (NOLOCK) ON net_ivd_key = rgs_ivd_key 
WHERE
		rgs_ses_key = @SessionKey 
 AND	rgs_cancel_date IS NULL  
 AND	rgs_delete_flag = 0 
 AND	(rgs_on_wait_list_flag = 0 OR rgs_on_wait_list_flag IS NULL)


END










GO


