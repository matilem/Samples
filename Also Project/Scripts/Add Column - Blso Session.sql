ALTER TABLE client_aafp_m09_cme_session ADD m09_blso_flag NVARCHAR(50) NULL;
EXEC ('sp_addextendedproperty [MS_Description],[File Type],[user],dbo,[table],[client_aafp_m09_cme_session],[column],[m09_blso_flag]');
