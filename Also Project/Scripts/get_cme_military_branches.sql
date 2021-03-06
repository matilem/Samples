USE [netForum]
GO
/****** Object:  StoredProcedure [dbo].[get_cme_military_branches]    Script Date: 3/6/2018 9:18:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- ==============================================================
-- Author:		Megan Matile
-- Create date: 12/12/2017
-- Description:	Returns all cme military branches
-- ==============================================================
ALTER PROCEDURE [dbo].[get_cme_military_branches]

AS
BEGIN
	SET NOCOUNT ON;

	SELECT	
			  b33_key		AS MilitaryKey
			, b33_type		AS MilitaryType
			,  *
	FROM
			dbo.client_aafp_b33_cme_military_branch
	WHERE	b33_delete_flag = 0
END
