USE [netForum]
GO

/****** Object:  Table [dbo].[client_aafp_b33_cme_activity_status]    Script Date: 12/7/2017 8:13:14 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[client_aafp_b33_cme_military_branch](
	[b33_key] [dbo].[av_key] ROWGUIDCOL  NOT NULL CONSTRAINT [DF_client_aafp_b33_cme_military_branch_b33_key]  DEFAULT (NEWID()),
	[b33_add_user] [dbo].[av_user] NOT NULL CONSTRAINT [DF_client_aafp_b33_cme_military_branch_add_user]  DEFAULT (SUSER_SNAME()),
	[b33_add_date] [dbo].[av_date] NOT NULL CONSTRAINT [DF_client_aafp_b33_cme_military_branch_add_date]  DEFAULT (GETDATE()),
	[b33_change_user] [dbo].[av_user] NULL,
	[b33_change_date] [dbo].[av_date] NULL,
	[b33_delete_flag] [dbo].[av_delete_flag] NOT NULL CONSTRAINT [DF_client_aafp_b33_cme_military_branch_b33_delete_flag]  DEFAULT ((0)),
	[b33_entity_key] [dbo].[av_key] NULL,
	[b33_type] [NVARCHAR](100) NOT NULL,
 CONSTRAINT [PK_client_aafp_b33_cme_activity_status] PRIMARY KEY CLUSTERED 
(
	[b33_key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


