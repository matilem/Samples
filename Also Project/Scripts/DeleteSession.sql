--SELECT	*
--FROM	dbo.client_aafp_m31_cme_activity
--WHERE	m31_number = 84461


--SELECT	*
--FROM	dbo.client_aafp_m09_cme_session
--WHERE	m09_m31_key = '67B64E70-F69C-47B4-9801-8113E0781B8D'

SELECT	m09_key, m09_m31_key, m09_m35_key_agenda, m09_m35_key_learning_objectives,*
FROM	dbo.client_aafp_m09_cme_session
WHERE	m09_m31_key = '9A9453AA-6FBF-4A59-8EAF-8BD485AEA783'

--SELECT	*
--FROM	dbo.client_aafp_m44_cme_session_x_core_competency
--WHERE	m44_m09_key = '0BD9AD99-1E4D-497E-B2CD-FEBA56D0C283' --Second session listed

SELECT *
FROM	dbo.client_aafp_m35_cme_attachment
WHERE	m35_key = 'D92FD346-E153-4116-8407-F2A93C33DC63'

--Change m35 key in m09 to be an existing m35_key
UPDATE dbo.client_aafp_m09_cme_session
SET		m09_m35_key_agenda = 'BD25BC4A-0316-437C-983E-B44E033B1137'
WHERE	m09_key = '0BD9AD99-1E4D-497E-B2CD-FEBA56D0C283'

DELETE	FROM dbo.client_aafp_m35_cme_attachment
WHERE	m35_key IN (
'D92FD346-E153-4116-8407-F2A93C33DC63'
)

DELETE	FROM dbo.client_aafp_m09_cme_session
WHERE	m09_key = '0BD9AD99-1E4D-497E-B2CD-FEBA56D0C283'