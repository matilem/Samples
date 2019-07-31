ALTER TABLE client_aafp_b29_also_learner ADD b29_b28_key av_key NOT NULL;
EXEC ('sp_addextendedproperty [MS_Description],[Also Course Key],[user],dbo,[table],[client_aafp_b29_also_learner],[column],[b29_b28_key]');

ALTER TABLE client_aafp_b29_also_learner ADD b29_cst_key av_key NOT NULL;
EXEC ('sp_addextendedproperty [MS_Description],[Also Cst Key],[user],dbo,[table],[client_aafp_b29_also_learner],[column],[b29_cst_key]');

ALTER TABLE client_aafp_b29_also_learner ADD b29_b30_key av_key NOT NULL;
EXEC ('sp_addextendedproperty [MS_Description],[Also Role Key],[user],dbo,[table],[client_aafp_b29_also_learner],[column],[b29_b30_key]');

ALTER TABLE client_aafp_b29_also_learner ADD b29_passed_flag av_flag NOT NULL;
EXEC ('sp_addextendedproperty [MS_Description],[Also Passed Flag],[user],dbo,[table],[client_aafp_b29_also_learner],[column],[b29_passed_flag]');

ALTER TABLE client_aafp_b29_also_learner ADD b29_failed_flag av_flag NOT NULL;
EXEC ('sp_addextendedproperty [MS_Description],[Also Failed Flag],[user],dbo,[table],[client_aafp_b29_also_learner],[column],[b29_failed_flag]');

ALTER TABLE client_aafp_b29_also_learner ADD b29_no_show_flag av_flag NOT NULL;
EXEC ('sp_addextendedproperty [MS_Description],[Also No Show Flag],[user],dbo,[table],[client_aafp_b29_also_learner],[column],[b29_no_show_flag]');