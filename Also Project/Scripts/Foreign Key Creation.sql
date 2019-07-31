--Also Course Table
ALTER TABLE [dbo].[client_aafp_b28_also_course]  WITH CHECK 
ADD  CONSTRAINT [FK_client_aafp_b28_also_course_activity] 
FOREIGN KEY([b28_m31_key])
REFERENCES [dbo].[client_aafp_m31_cme_activity] ([m31_key])

--Discount Price Key
ALTER TABLE [dbo].[client_aafp_b28_also_course]  WITH CHECK 
ADD  CONSTRAINT [FK_client_aafp_b28_also_course_oe_price] 
FOREIGN KEY([b28_prc_key])
REFERENCES [dbo].[oe_price] ([prc_key])

--Military Key
ALTER TABLE [dbo].[client_aafp_b28_also_course]  WITH CHECK 
ADD  CONSTRAINT [FK_client_aafp_b28_also_course_client_aafp_m93_cme_military_branch] 
FOREIGN KEY([b28_m93_key])
REFERENCES [dbo].[client_aafp_m93_cme_military_branch] ([m93_key])

----Attachments
--ALTER TABLE [dbo].[client_aafp_b28_also_course]  WITH CHECK 
--ADD  CONSTRAINT [FK_client_aafp_b28_also_course_client_client_aafp_b32_also_attachment] 
--FOREIGN KEY([b28_b32_key])
--REFERENCES [dbo].[client_aafp_b32_also_attachment] ([b32_key])



--Also Learner
ALTER TABLE [dbo].[client_aafp_b29_also_learner]  WITH CHECK 
ADD  CONSTRAINT [FK_client_aafp_b29_also_particpant_also_course] 
FOREIGN KEY([b29_b28_key])
REFERENCES [dbo].[client_aafp_b28_also_course] ([b28_key])

ALTER TABLE [dbo].[client_aafp_b29_also_learner]  WITH CHECK 
ADD  CONSTRAINT [FK_client_aafp_b29_also_particpant_co_customer] 
FOREIGN KEY([b29_cst_key])
REFERENCES [dbo].[co_customer] ([cst_key])

ALTER TABLE [dbo].[client_aafp_b29_also_learner]  WITH CHECK 
ADD  CONSTRAINT [FK_client_aafp_b29_also_particpant_also_learner_occupation] 
FOREIGN KEY([b29_b30_key])
REFERENCES [dbo].[client_aafp_b30_also_learner_occupation] ([b30_key])



--Also Instructor
ALTER TABLE [dbo].[client_aafp_b31_also_course_instructor]  WITH CHECK 
ADD  CONSTRAINT [FK_client_aafp_b31_also_course_instructor_client_aafp_b28_also_course] 
FOREIGN KEY([b31_b28_course_key])
REFERENCES [dbo].[client_aafp_b28_also_course] ([b28_key])

ALTER TABLE [dbo].[client_aafp_b31_also_course_instructor]  WITH CHECK 
ADD  CONSTRAINT [FK_client_aafp_b31_also_course_instructor_co_customer] 
FOREIGN KEY([b31_cst_key])
REFERENCES [dbo].[co_customer] ([cst_key])

ALTER TABLE [dbo].[client_aafp_b31_also_course_instructor]  WITH CHECK 
ADD  CONSTRAINT [FK_client_aafp_b31_also_course_instructor_advisor_faculty_co_customer] 
FOREIGN KEY([b31_advisor_faculty_cst_key])
REFERENCES [dbo].[co_customer] ([cst_key])

ALTER TABLE [dbo].[client_aafp_b31_also_course_instructor]  WITH CHECK 
ADD  CONSTRAINT [FK_client_aafp_b31_also_course_instructor_also_status] 
FOREIGN KEY([b31_a02_key])
REFERENCES [dbo].[client_aafp_a02_also_status] ([a02_key])

ALTER TABLE [dbo].[client_aafp_b31_also_course_instructor]  WITH CHECK 
ADD  CONSTRAINT [FK_client_aafp_b31_also_course_instructor_activity] 
FOREIGN KEY([b31_m31_key])
REFERENCES [dbo].[client_aafp_m31_cme_activity] ([m31_key])

ALTER TABLE [dbo].[client_aafp_b31_also_course_instructor]  WITH CHECK 
ADD  CONSTRAINT [FK_client_aafp_b31_also_instructor_also_course] 
FOREIGN KEY([b31_b28_key])
REFERENCES [dbo].[client_aafp_b28_also_course] ([b28_key])

----Also Attachment
--ALTER TABLE [dbo].[cclient_aafp_b32_also_attachment]  WITH CHECK 
--ADD  CONSTRAINT [FK_client_client_aafp_b32_also_attachment_client_aafp_b28_also_course_client] 
--FOREIGN KEY([b32_b28_key])
--REFERENCES [dbo].[client_aafp_b28_also_course] ([b28_key])