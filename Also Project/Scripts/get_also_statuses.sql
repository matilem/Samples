USE [netForum]
GO
/****** Object:  StoredProcedure [dbo].[get_also_statuses]    Script Date: 3/5/2018 11:20:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- ======================================================================
-- Author:		Megan Matile
-- Create date: 12/12/2017
-- Description:	Returns also statuses 
-- ======================================================================
ALTER PROCEDURE [dbo].[get_also_statuses]
(
	 @CustomerKey	UNIQUEIDENTIFIER	
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT		  
		  a02_key									AS AlsoStatusKey
		, a02_add_user								AS AddUser
		, a02_add_date								AS AddDate
		, a02_change_user							AS ChangeUser
		, a02_change_date							AS ChangeDate
		, a02_start_date							AS StartDate
		, a02_expire_date							AS ExpirationDate
		, a02_approve_date							AS ApprovedDate
		, a01_type									AS AlsoStatusType
	FROM	dbo.client_aafp_a02_also_status
		INNER JOIN dbo.client_aafp_a01_also_status_type ON client_aafp_a01_also_status_type.a01_key = client_aafp_a02_also_status.a02_a01_key			
	WHERE				
			a02_cst_key = @CustomerKey
END

















