USE [netForum]
GO
/****** Object:  StoredProcedure [dbo].[get_cme_military_branches]    Script Date: 12/18/2017 10:22:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- ==============================================================
-- Author:		Megan Matile
-- Create date: 12/12/2017
-- Description:	Returns all ALSO/BLSO learner occupations
-- ==============================================================
CREATE PROCEDURE [dbo].[get_also_learner_occupations]

AS
BEGIN
	SET NOCOUNT ON;

	SELECT	
			  b30_key		AS OccupationKey
			, b30_role		AS OccupationRole
	FROM
			dbo.client_aafp_b30_also_learner_occupation
	WHERE	b30_delete_flag = 0
END
