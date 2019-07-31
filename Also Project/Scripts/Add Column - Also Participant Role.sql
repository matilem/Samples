ALTER TABLE client_aafp_b30_also_learner_role ADD b30_role NVARCHAR(50) NOT NULL;
EXEC ('sp_addextendedproperty [MS_Description],[Also learner Role],[user],dbo,[table],[client_aafp_b30_also_learner_role],[column],[b30_role]');
