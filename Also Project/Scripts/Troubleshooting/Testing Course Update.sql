--update client_aafp_m01_cme_application set m01_activity_sub_type = 'ALSO Provider' where m01_key = mykey
--update client_aafp_m01_cme_application set m01_activity_sub_type = 'BLSO Provider' where m01_key = mykey

SELECT	m31_m01_key,*
FROM	dbo.client_aafp_m31_cme_activity
WHERE	m31_number = 83184

--update client_aafp_m01_cme_application set m01_activity_sub_type = 'BLSO Provider' where m01_key IN ('11BC1323-CF5D-4E42-9D77-2EC5B1BF4368', '881C50DC-5DB2-45FA-8679-62440D38DE34')


SELECT	m31_m01_key,*
FROM	dbo.client_aafp_m31_cme_activity
WHERE	m31_number = 83185

--update client_aafp_m01_cme_application set m01_activity_sub_type = 'ALSO Instructor' where m01_key IN ('77A578D1-D2B9-4D70-85DE-1A7DB806054F','C2E98581-9CAE-44EA-AFD6-43C9076A7E46')


SELECT	m01_activity_sub_type,*
FROM	dbo.client_aafp_m01_cme_application
WHERE	m01_key IN ('77A578D1-D2B9-4D70-85DE-1A7DB806054F','C2E98581-9CAE-44EA-AFD6-43C9076A7E46','11BC1323-CF5D-4E42-9D77-2EC5B1BF4368', '881C50DC-5DB2-45FA-8679-62440D38DE34')