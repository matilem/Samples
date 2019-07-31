ALTER TABLE client_aafp_b32_also_attachment ADD b32_file_type NVARCHAR(50) NULL;
EXEC ('sp_addextendedproperty [MS_Description],[File Type],[user],dbo,[table],[client_aafp_b32_also_attachment],[column],[b32_file_type]');

ALTER TABLE client_aafp_b32_also_attachment ADD b32_file_location NVARCHAR(255) NULL;
EXEC ('sp_addextendedproperty [MS_Description],[File Location],[user],dbo,[table],[client_aafp_b32_also_attachment],[column],[b32_file_location]');