USE [netForumDev]
GO

/****** Object:  View [dbo].[vw_client_aafp_event_invoice_detail]    Script Date: 3/18/2016 2:59:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vw_client_aafp_event_invoice_detail]
AS
SELECT        dbo.ac_invoice_detail.ivd_key, dbo.ac_invoice_detail.ivd_inv_key, dbo.ac_invoice_detail.ivd_qty, dbo.ac_invoice_detail.ivd_price, dbo.ac_invoice_detail.ivd_prc_key, dbo.ac_invoice_detail.ivd_prc_prd_key, 
                         dbo.oe_price.prc_display_name, dbo.oe_product.prd_name, dbo.ac_invoice_detail.ivd_type, dbo.ac_invoice.inv_code, dbo.oe_product.prd_code, dbo.oe_price.prc_end_date
FROM            dbo.ac_invoice_detail WITH (NOLOCK) INNER JOIN
						dbo.ac_invoice WITH (NOLOCK) ON ac_invoice.inv_key = ac_invoice_detail.ivd_inv_key INNER JOIN	
                         dbo.oe_price WITH (NOLOCK) ON dbo.oe_price.prc_key = dbo.ac_invoice_detail.ivd_prc_key INNER JOIN
                         dbo.oe_product WITH (NOLOCK) ON dbo.oe_product.prd_key = dbo.oe_price.prc_prd_key INNER JOIN
                         dbo.oe_product_type WITH (NOLOCK) ON dbo.oe_product_type.ptp_key = dbo.oe_product.prd_ptp_key
WHERE        (dbo.ac_invoice_detail.ivd_delete_flag = 0) AND (dbo.oe_product_type.ptp_key IN ('64B77FB4-DA34-4034-ADA8-F1DA796D8720', 'CB7F7AA9-9AA2-42E6-87C6-E36E1AB32AFD','52405A5C-6393-460D-BD35-2BBB2AA79E33'))


GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ac_invoice_detail"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 287
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "oe_price"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 286
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "oe_product"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 290
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "oe_product_type"
            Begin Extent = 
               Top = 402
               Left = 38
               Bottom = 532
               Right = 288
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_client_aafp_event_invoice_detail'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_client_aafp_event_invoice_detail'
GO


