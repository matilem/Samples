/*
Run this script on:

        NF2011-DB.webad.aafp.org.netForum    -  This database will be modified

to synchronize it with:

        TESTINGDB.webad.aafp.org.netForum

You are recommended to back up your database before running this script

Script created by SQL Compare version 11.6.11 from Red Gate Software Ltd at 12/4/2017 10:49:28 AM

*/
SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
BEGIN TRANSACTION
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Altering [dbo].[client_aafp_m31_cme_activity]'
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
IF COL_LENGTH(N'[dbo].[client_aafp_m31_cme_activity]', N'm31_course_coordinator_name') IS NOT NULL
ALTER TABLE [dbo].[client_aafp_m31_cme_activity] DROP COLUMN [m31_course_coordinator_name]
IF COL_LENGTH(N'[dbo].[client_aafp_m31_cme_activity]', N'm31_course_coordinator_phone') IS NOT NULL
ALTER TABLE [dbo].[client_aafp_m31_cme_activity] DROP COLUMN [m31_course_coordinator_phone]
IF COL_LENGTH(N'[dbo].[client_aafp_m31_cme_activity]', N'm31_course_coordinator_email') IS NOT NULL
ALTER TABLE [dbo].[client_aafp_m31_cme_activity] DROP COLUMN [m31_course_coordinator_email]
IF COL_LENGTH(N'[dbo].[client_aafp_m31_cme_activity]', N'm31_director_id') IS NOT NULL
ALTER TABLE [dbo].[client_aafp_m31_cme_activity] DROP COLUMN [m31_director_id]
IF COL_LENGTH(N'[dbo].[client_aafp_m31_cme_activity]', N'm31_coordinator_id') IS NOT NULL
ALTER TABLE [dbo].[client_aafp_m31_cme_activity] DROP COLUMN [m31_coordinator_id]
IF COL_LENGTH(N'[dbo].[client_aafp_m31_cme_activity]', N'm31_same_as_director_check_flag') IS NOT NULL
ALTER TABLE [dbo].[client_aafp_m31_cme_activity] DROP COLUMN [m31_same_as_director_check_flag]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
COMMIT TRANSACTION
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
DECLARE @Success AS BIT
SET @Success = 1
SET NOEXEC OFF
IF (@Success = 1) PRINT 'The database update succeeded'
ELSE BEGIN
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
	PRINT 'The database update failed'
END
GO
