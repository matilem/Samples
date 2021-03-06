USE [netForum]
GO
/****** Object:  StoredProcedure [dbo].[get_also_product]    Script Date: 1/25/2018 10:58:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- ======================================================================
-- Author:		Megan Matile
-- Create date: 12/12/2017
-- Description:	Returns also discount product 
-- ======================================================================
CREATE PROCEDURE [dbo].[get_also_product]
(
	 @ProductCode	NVARCHAR(20)
	 	
)
AS
BEGIN
	SET NOCOUNT ON;	
	DECLARE	@DiscountKey UNIQUEIDENTIFIER
	SET		@DiscountKey = (SELECT dxp_dsc_prd_key FROM dbo.oe_product RIGHT JOIN dbo.oe_discount_product_x_product ON oe_discount_product_x_product.dxp_prd_key = oe_product.prd_key WHERE prd_code = @ProductCode)
	 	SELECT		  
		  prd_key						AS ProductKey
		, prd_name						AS ProductName
		, prd_ptp_key					AS ProductTypeKey
		, prd_atc_key					AS ProductCompanyKey
		, *

	FROM	dbo.oe_product	
	WHERE				
			prd_key =  @DiscountKey

END




















