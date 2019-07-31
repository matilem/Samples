USE [netForum]
GO
/****** Object:  StoredProcedure [dbo].[get_also_activity]    Script Date: 1/25/2018 10:28:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- ======================================================================
-- Author:		Megan Matile
-- Create date: 12/12/2017
-- Description:	Returns also discount 
-- ======================================================================
CREATE PROCEDURE [dbo].[get_also_discount]
(
	 @PriceCode	NVARCHAR(20)	
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		  
		  prc_key			AS	PriceKey
	FROM	dbo.oe_price
				
	WHERE				
			prc_code = @PriceCode
END





















