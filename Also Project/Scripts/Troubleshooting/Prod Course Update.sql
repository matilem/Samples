--update client_aafp_m01_cme_application set m01_activity_sub_type = 'ALSO Provider' where m01_key = mykey
--update client_aafp_m01_cme_application set m01_activity_sub_type = 'BLSO Provider' where m01_key = mykey

SELECT	m31_m01_key,*
FROM	dbo.client_aafp_m31_cme_activity
WHERE	m31_number = 80674

--update client_aafp_m01_cme_application set m01_activity_sub_type = 'BLSO Provider' where m01_key IN ('1F673859-E050-4586-8BDD-C9B231CE0277', '5A032AD3-04CA-48BC-B88D-8A3BCD2225BC')


SELECT	m31_m01_key,*
FROM	dbo.client_aafp_m31_cme_activity
WHERE	m31_number = 80677

--update client_aafp_m01_cme_application set m01_activity_sub_type = 'ALSO Instructor' where m01_key IN ('7E6C4466-9FD0-409E-B60B-F545F881EC3C','699496E7-EF54-440F-8379-28CAE11D673C')


SELECT	m01_activity_sub_type,*
FROM	dbo.client_aafp_m01_cme_application
WHERE	m01_key IN ('1F673859-E050-4586-8BDD-C9B231CE0277', '5A032AD3-04CA-48BC-B88D-8A3BCD2225BC','7E6C4466-9FD0-409E-B60B-F545F881EC3C','699496E7-EF54-440F-8379-28CAE11D673C')