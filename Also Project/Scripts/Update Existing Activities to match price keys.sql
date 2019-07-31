--All discounts created for also/blso

INSERT INTO dbo.client_aafp_b28_also_course
        ( b28_add_user ,
          b28_add_date ,
          b28_delete_flag ,
          b28_entity_key ,
          b28_m31_key ,
          b28_prc_key 
        )
--VALUES  ( 'sa' , -- b28_add_user - av_user
--          GETDATE() , -- b28_add_date - av_date
--          0 , -- b28_delete_flag - av_delete_flag
--          NULL , -- b28_entity_key - av_key
--          NULL , -- b28_m31_key - av_key
--          NULL  -- b28_prc_key - av_key
--        )


SELECT	'sa', GETDATE(), 0, '00000000-0000-0000-0000-000000000000', m31_key, prc_key
FROM	dbo.oe_price
		INNER JOIN dbo.client_aafp_m31_cme_activity ON CAST(m31_number AS NVARCHAR(20)) = oe_price.prc_code
		LEFT OUTER JOIN dbo.client_aafp_b28_also_course ON client_aafp_b28_also_course.b28_m31_key = client_aafp_m31_cme_activity.m31_key
WHERE	prc_code IN (
SELECT	CAST(m31_number AS NVARCHAR(20))	
FROM
			dbo.client_aafp_m31_cme_activity WITH (NOLOCK)
	 INNER JOIN
			dbo.client_aafp_m01_cme_application WITH (NOLOCK) ON client_aafp_m01_cme_application.m01_key = client_aafp_m31_cme_activity.m31_m01_key

	WHERE
			m31_begin_date >= DATEADD(month, -6, GETDATE())
	 AND	m01_activity_sub_type IN ('ALSO Provider', 'ALSO Instructor', 'ALSO Refresher', 'BLSO Provider')
	 --AND	m01_delete_flag = 0
	 --AND	m01_submission_date IS NOT NULL
	 --AND	m02_key != 'C86DDF8D-FE32-42A7-8E83-EB1F29208385'
)
AND b28_key IS NULL



SELECT	*
FROM	dbo.client_aafp_b28_also_course