ALTER TABLE client_aafp_b31_also_course_instructor ADD b31_b28_course_key av_key NULL;
EXEC ('sp_addextendedproperty [MS_Description],[Also Course Key],[user],dbo,[table],[client_aafp_b31_also_course_instructor],[column],[b31_b28_course_key]');

ALTER TABLE client_aafp_b31_also_course_instructor ADD b31_cst_key av_key NULL;
EXEC ('sp_addextendedproperty [MS_Description],[Instructor Cst Key],[user],dbo,[table],[client_aafp_b31_also_course_instructor],[column],[b31_cst_key]');

ALTER TABLE client_aafp_b31_also_course_instructor ADD b31_a02_key av_key NULL;
EXEC ('sp_addextendedproperty [MS_Description],[Also Status Key],[user],dbo,[table],[client_aafp_b31_also_course_instructor],[column],[b31_a02_key]');

ALTER TABLE client_aafp_b31_also_course_instructor ADD b31_advisor_faculty_cst_key av_key NULL;
EXEC ('sp_addextendedproperty [MS_Description],[Advisor Faculty Cst Key],[user],dbo,[table],[client_aafp_b31_also_course_instructor],[column],[b31_advisor_faculty_cst_key]');

ALTER TABLE client_aafp_b31_also_course_instructor ADD b31_recommend_flag av_flag NULL;
EXEC ('sp_addextendedproperty [MS_Description],[Instructor Recommend Flag],[user],dbo,[table],[client_aafp_b31_also_course_instructor],[column],[b31_recommend_flag]');