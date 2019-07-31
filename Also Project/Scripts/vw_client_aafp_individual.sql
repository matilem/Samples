USE [netForum]
GO

/****** Object:  View [dbo].[vw_client_aafp_individual]    Script Date: 1/12/2018 8:30:19 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER VIEW [dbo].[vw_client_aafp_individual]
AS
SELECT        dbo.co_individual.ind_cst_key, dbo.co_individual.ind_first_name, dbo.co_individual.ind_last_name, dbo.co_individual.ind_mid_name, dbo.co_individual.ind_full_name_cp, dbo.co_customer.cst_key, 
                         dbo.co_customer.cst_type, dbo.co_customer.cst_member_flag, dbo.co_customer.cst_name_cp, dbo.co_customer.cst_ind_full_name_dn, dbo.co_customer.cst_id, dbo.co_individual.ind_designation, 
                         dbo.co_email.eml_key, dbo.co_email.eml_address, dbo.co_customer_x_address.cxa_key AS cxa_key_primary, dbo.co_individual.ind_delete_flag, dbo.co_customer_x_fax.cfx_key AS cfx_key_primary, 
                         dbo.co_customer_x_phone.cph_key AS cph_key_primary, dbo.co_customer.cst_ixo_title_dn, dbo.co_customer.cst_ixo_key, dbo.co_individual_x_organization.ixo_org_cst_key, dbo.co_organization.org_name, 
                         dbo.co_individual.ind_badge_name, dbo.co_customer.cst_web_login, dbo.co_individual.ind_int_code, dbo.co_individual.ind_prf_code, dbo.co_individual.ind_dob, dbo.co_individual.ind_gender, 
                         dbo.co_customer_ext.cst_twitter_handle_ext, dbo.co_individual.ind_grad_date, dbo.co_individual.ind_license_number, dbo.co_individual.ind_add_date, dbo.co_individual.ind_add_user, 
                         dbo.co_customer.cst_org_name_dn, dbo.co_individual_ext.ind_residency_start_date_ext, dbo.co_individual_ext.ind_v_residency_completion_date_ext, dbo.co_individual_ext.ind_v_residency_status_ext
FROM            dbo.co_individual WITH (NOLOCK) INNER JOIN
						dbo.co_individual_ext WITH (NOLOCK) ON dbo.co_individual_ext.ind_cst_key_ext = dbo.co_individual.ind_cst_key INNER JOIN
                         dbo.co_customer WITH (NOLOCK) ON dbo.co_customer.cst_key = dbo.co_individual.ind_cst_key INNER JOIN
                         dbo.co_customer_ext WITH (NOLOCK) ON dbo.co_customer.cst_key = dbo.co_customer_ext.cst_key_ext LEFT OUTER JOIN
                         dbo.co_email WITH (NOLOCK) ON dbo.co_email.eml_key = dbo.co_customer.cst_eml_key AND dbo.co_email.eml_delete_flag = 0 LEFT OUTER JOIN
                         dbo.co_individual_x_organization WITH (NOLOCK) ON dbo.co_customer.cst_ixo_key = dbo.co_individual_x_organization.ixo_key AND dbo.co_individual_x_organization.ixo_delete_flag = 0 LEFT OUTER JOIN
                         dbo.co_organization WITH (NOLOCK) ON dbo.co_individual_x_organization.ixo_org_cst_key = dbo.co_organization.org_cst_key LEFT OUTER JOIN
                         dbo.co_customer_x_phone WITH (NOLOCK) ON dbo.co_customer.cst_cph_key = dbo.co_customer_x_phone.cph_key LEFT OUTER JOIN
                         dbo.co_customer_x_fax WITH (NOLOCK) ON dbo.co_customer.cst_cfx_key = dbo.co_customer_x_fax.cfx_key LEFT OUTER JOIN
                         dbo.co_customer_x_address WITH (NOLOCK) ON dbo.co_customer.cst_cxa_key = dbo.co_customer_x_address.cxa_key
WHERE        (dbo.co_individual.ind_delete_flag = 0) AND (dbo.co_customer.cst_delete_flag = 0)

GO


