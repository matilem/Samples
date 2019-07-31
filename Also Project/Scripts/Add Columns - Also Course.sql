ALTER TABLE client_aafp_b28_also_course ADD b28_m31_key av_key NOT NULL;
EXEC ('sp_addextendedproperty [MS_Description],[Activity Key],[user],dbo,[table],[client_aafp_b28_also_course],[column],[b28_m31_key]');

ALTER TABLE client_aafp_b28_also_course ADD b28_prc_key av_key NOT NULL;
EXEC ('sp_addextendedproperty [MS_Description],[Price Key],[user],dbo,[table],[client_aafp_b28_also_course],[column],[b28_prc_key]');

ALTER TABLE client_aafp_b28_also_course ADD b28_m93_key av_key NOT NULL;
EXEC ('sp_addextendedproperty [MS_Description],[Military Key],[user],dbo,[table],[client_aafp_b28_also_course],[column],[b28_m93_key]');

--ALTER TABLE client_aafp_b28_also_course ADD b28_b32_key av_key NOT NULL;
--EXEC ('sp_addextendedproperty [MS_Description],[Attachment Key],[user],dbo,[table],[client_aafp_b28_also_course],[column],[b28_b32_key]');

ALTER TABLE client_aafp_b28_also_course ADD b28_pre_course_submitted_flag av_flag NULL;
EXEC ('sp_addextendedproperty [MS_Description],[Pre Course Submitted Flag],[user],dbo,[table],[client_aafp_b28_also_course],[column],[b28_pre_course_submitted_flag]');

ALTER TABLE client_aafp_b28_also_course ADD b28_pre_course_approved_flag av_flag NULL;
EXEC ('sp_addextendedproperty [MS_Description],[Pre Course Approved Flag],[user],dbo,[table],[client_aafp_b28_also_course],[column],[b28_pre_course_approved_flag]');

ALTER TABLE client_aafp_b28_also_course ADD b28_post_course_submitted_flag av_flag NULL;
EXEC ('sp_addextendedproperty [MS_Description],[Post Course Submitted Flag],[user],dbo,[table],[client_aafp_b28_also_course],[column],[b28_post_course_submitted_flag]');

ALTER TABLE client_aafp_b28_also_course ADD b28_post_course_completed_flag av_flag NULL;
EXEC ('sp_addextendedproperty [MS_Description],[Post Course Completed Flag],[user],dbo,[table],[client_aafp_b28_also_course],[column],[b28_post_course_completed_flag]');