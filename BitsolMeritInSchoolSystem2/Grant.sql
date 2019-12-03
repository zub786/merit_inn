IF NOT EXISTS (SELECT name FROM sys.server_principals WHERE name = 'IIS APPPOOL\TheMeritSchool')
BEGIN
    CREATE LOGIN [IIS APPPOOL\DefaultAppPool] 
      FROM WINDOWS WITH DEFAULT_DATABASE=[master], 
      DEFAULT_LANGUAGE=[us_english]
END
GO
CREATE USER [ContosoUniversityUser22] 
  FOR LOGIN [IIS APPPOOL\DefaultAppPool]
GO
EXEC sp_addrolemember 'db_owner', 'ContosoUniversityUser22'
GO