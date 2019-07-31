USE [netForumDev]
GO

/****** Object:  StoredProcedure [dbo].[client_aafp_event_get_events_by_customer]    Script Date: 3/18/2016 2:44:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






-- ====================================================================================
-- Author:		Jason Walker
-- Create date: 11/19/2015
-- Description:	Returns list of events for the list of customers provided as a comma delimited string. 
-- Includes pending registrations.
-- ====================================================================================

CREATE PROCEDURE [dbo].[client_aafp_event_get_events_by_customer]
        @CustomerKeys	NVARCHAR(MAX)
AS

BEGIN


SELECT DISTINCT
			evt_key
		  , evt_code
		  , evt_title
		  , evt_start_date
		  , reg_key AS registration_key
		  , reg_cst_key AS customer_key
		  , 0 AS is_pending
FROM
		ev_registrant WITH (NOLOCK)
 INNER JOIN
		ev_event WITH (NOLOCK) ON evt_key = reg_evt_key
WHERE
		reg_cst_key IN (SELECT value FROM dbo.aafp_split(',', @CustomerKeys))
 AND	reg_delete_flag = 0
 AND	reg_cancel_date IS NULL
 AND	evt_start_date > DATEADD(YEAR, -2, GETDATE())


UNION


SELECT DISTINCT
			evt_key
		  , evt_code
		  , evt_title
		  , evt_start_date
		  , e40_key AS registration_key
		  , e40_cst_key AS customer_key
		  , 1 AS is_pending
FROM
		client_aafp_e40_pending_registration WITH (NOLOCK)
 INNER JOIN
		ev_event WITH (NOLOCK) ON evt_key = e40_evt_key
WHERE
		e40_cst_key IN (SELECT value FROM dbo.aafp_split(',', @CustomerKeys))
 AND	e40_delete_flag = 0
 AND	e40_processed_flag = 0
 AND	evt_start_date > DATEADD(YEAR, -2, GETDATE())
ORDER BY 
		evt_start_date DESC 


END







GO


