Use [master]
GO

-- ================================================================================================ --
-- CREATE SYSTEM LOGIN                                                                              --
-- ================================================================================================ --
IF  NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'AcademyLinkUser')
	CREATE LOGIN [AcademyLinkUser] WITH PASSWORD=N'WebAPIPassword', DEFAULT_DATABASE=[Test], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

-- ================================================================================================
-- 
-- ================================================================================================

IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'AcademyLink' )
BEGIN
    CREATE DATABASE [AcademyLink]   
END
Go

Use [AcademyLink]
Go

-- ================================================================================================ 
-- CREATE db user                                                                                   
-- ================================================================================================ 
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'AcademyLinkUser')
	CREATE USER [AcademyLinkUser] FOR LOGIN [AcademyLinkUser] WITH DEFAULT_SCHEMA=[dbo]
GO

-- ================================================================================================
-- CREATE SCHEMAS 
-- ================================================================================================

-- ================================================================================================
-- CREATE SCHEMAS FOR USER MANAGMENT 
-- ================================================================================================
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'umUser')
BEGIN
	EXEC('CREATE SCHEMA [umUser]');
END
Go

---- ================================================================================================
---- CREATE SCHEMAS FOR WALLET MODULE
---- ================================================================================================
--IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'wallet')
--BEGIN
--	EXEC('CREATE SCHEMA [wallet]');
--END
--Go

---- ================================================================================================
--IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'walletHist')
--BEGIN
--	EXEC('CREATE SCHEMA [walletHist]');
--END
--Go

-- ============================================================================================================================================ 
-- Drops                                                                                  
-- ============================================================================================================================================ 

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StudensEnrolledCourses]') AND type in (N'U'))
	DROP TABLE [dbo].[StudensEnrolledCourses]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Students]') AND type in (N'U'))
	DROP TABLE [dbo].[Students]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Courses]') AND type in (N'U'))
	DROP TABLE [dbo].[Courses]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[umUser].[Users]') AND type in (N'U'))
	DROP TABLE [umUser].[Users]
GO

-- ============================================================================================================================================ 
--                                                                                   
-- ============================================================================================================================================ 
CREATE TABLE [umUser].[Users](
	[UserID]    [int] IDENTITY(1, 1) NOT NULL,
	[UserName]  [nvarchar](50)	   NOT NULL,
	[Password]  [varbinary](50)        NULL,
    [FullName]	[nvarchar](150)        NULL,
	[Mail]      [nvarchar](50)		   NULL,
	[Phone]     [nvarchar](50)         NULL,
	[D_Modify]  [datetime2]         NOT NULL,
	[Version] int				   NOT NULL,
	CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
	(
		[UserID] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [umUser].[Users] ADD  CONSTRAINT [DF_Users_D_Modify]  DEFAULT (getdate()) FOR [D_Modify]
ALTER TABLE [umUser].[Users] ADD  CONSTRAINT [DF_Users_Version]   DEFAULT ((0)) FOR [Version]
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_UMUsers_Name] ON [umUser].[Users] 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [umUser].[Users] ADD CONSTRAINT uc_UsersUserName UNIQUE (UserName)
Go

SET IDENTITY_INSERT [umUser].[Users] ON

INSERT INTO [umUser].[Users] ([UserID], [UserName],[Password],[FullName],[Mail])
     VALUES (1, 'Hurka', 0x4D7956617262696E61727944617461, 'Hurka Marinov', 'MHurka.gmail.com')
Go
SET IDENTITY_INSERT [umUser].[Users] OFF
Go

-- ============================================================================================================================================ 
--                                                                                   
-- ============================================================================================================================================ 
CREATE TABLE [dbo].[Students](
	[StudentID]               [int] IDENTITY(1, 1)  NOT NULL,
	[FirstName]               [nvarchar](50)	    NOT NULL,
	[LastName]                [nvarchar](50)	    NOT NULL,
	[Email]                   [nvarchar](255)	    NOT NULL,
	[Phone]                   [nvarchar](20)	    NOT NULL,
	--[UserID]		          [int]				    NOT NULL,
	--[D_Modify]			      [datetime2]                NULL,
	--[Version]                 [int]				    NOT NULL,
	CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
	(
		[StudentID] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO

--ALTER TABLE [dbo].[Students] ADD  CONSTRAINT [DF_Students_D_Modify]  DEFAULT (getdate()) FOR [D_Modify]
--ALTER TABLE [dbo].[Students] ADD  CONSTRAINT [DF_Students_Version]   DEFAULT ((0)) FOR [Version]
--ALTER TABLE [dbo].[Students] ADD  CONSTRAINT [DF_Students_UserID]   DEFAULT ((1)) FOR [UserID]
--GO

--ALTER TABLE [dbo].[Students]  WITH CHECK ADD CONSTRAINT [FK_Students_User_UserID] FOREIGN KEY([UserID]) 
--	REFERENCES [umUser].[Users] ([UserID])
--GO
--ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_User_UserID]
--GO

-- ============================================================================================================================================ 
--                                                                                   
-- ============================================================================================================================================ 
CREATE TABLE [dbo].[Courses](
	[CourseID]                [int] IDENTITY(1, 1)  NOT NULL,
	[Name]                    [nvarchar](50)	    NOT NULL,
	[Description]             [nvarchar](max)	    NOT NULL,
	[SeatsAvailable]          [int]                 NOT NULL,  
	[DateFrom]				  [datetime2]           NOT NULL,
	[DateTo]				  [datetime2]            NOT NULL,
	[IsAvailable]             [int]                 NOT NULL,
	--[UserID]		          [int]				    NOT NULL,
	--[D_Modify]				  [datetime2]             NULL,
	--[Version]                 [int]				    NOT NULL,
	CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED 
	(
		[CourseID] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO

--ALTER TABLE [dbo].[Courses] ADD  CONSTRAINT [DF_Courses_D_Modify]  DEFAULT (getdate()) FOR [D_Modify]
--ALTER TABLE [dbo].[Courses] ADD  CONSTRAINT [DF_Courses_Version]   DEFAULT ((0)) FOR [Version]
--GO


CREATE UNIQUE NONCLUSTERED INDEX [IX_Courses_Name] ON [dbo].[Courses] 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Courses] ADD CONSTRAINT uc_CoursesName UNIQUE ([Name])
Go

--ALTER TABLE [dbo].[Courses] ADD  CONSTRAINT [DF_Courses_UserID]   DEFAULT ((1)) FOR [UserID]
--GO
--ALTER TABLE [dbo].[Courses]  WITH CHECK ADD CONSTRAINT [FK_Courses_User_UserID] FOREIGN KEY([UserID]) 
--	REFERENCES [umUser].[Users] ([UserID])
--GO
--ALTER TABLE [dbo].[Courses] CHECK CONSTRAINT [FK_Courses_User_UserID]
--GO

-- ============================================================================================================================================ 
--                                                                                   
-- ============================================================================================================================================ 
CREATE TABLE [dbo].[StudensEnrolledCourses](
	[EnrollmentId]                [int] IDENTITY(1, 1) NOT NULL,
	[StudentId]	                  [int]				  NOT NULL,
	[CourseId]                    [int]				  NOT NULL,
	[EnrollmentDate]              [datetime2]          NOT NULL,
	[Progress]                    [decimal](5, 2)     NOT NULL,
	[Status]                      [nvarchar](50)	  NOT NULL,
	--[UserID]		              [int]				  NOT NULL,
	--[D_Modify]				      [datetime2]          NOT NULL,
	--[Version]                     [int]				  NOT NULL,
	CONSTRAINT [PK_StudensEnrolledCourses] PRIMARY KEY CLUSTERED 
	(
		[EnrollmentId] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO

--ALTER TABLE [dbo].[StudensEnrolledCourses] ADD  CONSTRAINT [DF_StudensEnrolledCourses_D_Modify]  DEFAULT (getdate()) FOR [D_Modify]
--ALTER TABLE [dbo].[StudensEnrolledCourses] ADD  CONSTRAINT [DF_StudensEnrolledCourses_Version]   DEFAULT ((0)) FOR [Version]
--GO


ALTER TABLE [dbo].[StudensEnrolledCourses] WITH CHECK ADD CONSTRAINT [FK_StudensEnrolledCourses_Students_StudentID] FOREIGN KEY([StudentID]) 
	REFERENCES [dbo].[Students] ([StudentID])
	ON DELETE CASCADE;
GO
ALTER TABLE [dbo].[StudensEnrolledCourses] CHECK CONSTRAINT [FK_StudensEnrolledCourses_Students_StudentID]
GO


ALTER TABLE [dbo].[StudensEnrolledCourses] WITH CHECK ADD CONSTRAINT [FK_StudensEnrolledCourses_Courses_CourseID] FOREIGN KEY([CourseID]) 
	REFERENCES [dbo].[Courses] ([CourseID])
	ON DELETE CASCADE;
GO
ALTER TABLE [dbo].[StudensEnrolledCourses] CHECK CONSTRAINT [FK_StudensEnrolledCourses_Courses_CourseID]
GO

--ALTER TABLE [dbo].[StudensEnrolledCourses] ADD  CONSTRAINT [DF_StudensEnrolledCourses_UserID]   DEFAULT ((1)) FOR [UserID]
--GO
--ALTER TABLE [dbo].[StudensEnrolledCourses]  WITH CHECK ADD CONSTRAINT [FK_StudensEnrolledCourses_User_UserID] FOREIGN KEY([UserID]) 
--	REFERENCES [umUser].[Users] ([UserID])
--GO
--ALTER TABLE [dbo].[StudensEnrolledCourses] CHECK CONSTRAINT [FK_StudensEnrolledCourses_User_UserID]
--GO
---------------------------------------------- GRANT PERMISSIONS -----------------------------------------------------------------------


GRANT SELECT ON [dbo].[StudensEnrolledCourses] TO [AcademyLinkUser];  
GO  
GRANT insert ON [dbo].[StudensEnrolledCourses] TO [AcademyLinkUser];  
GO  
GRANT update ON [dbo].[StudensEnrolledCourses] TO [AcademyLinkUser];  
GO  
GRANT delete ON [dbo].[StudensEnrolledCourses] TO [AcademyLinkUser];  
GO  
GRANT SELECT ON [dbo].[Courses] TO [AcademyLinkUser];  
GO  
GRANT insert ON [dbo].[Courses] TO [AcademyLinkUser];  
GO  
GRANT update ON [dbo].[Courses] TO [AcademyLinkUser];  
GO  
GRANT delete ON [dbo].[Courses] TO [AcademyLinkUser];  
GO  
GRANT SELECT ON [dbo].[Students] TO [AcademyLinkUser];  
GO  
GRANT insert ON [dbo].[Students] TO [AcademyLinkUser];  
GO  
GRANT update ON [dbo].[Students] TO [AcademyLinkUser];  
GO  
GRANT delete ON [dbo].[Students] TO [AcademyLinkUser];  
GO  


---- Creation of DUMMY users that are going to create, delete his  wallets

USE [AcademyLink]
GO

insert into [dbo].[Courses] ([Name],[Description],[SeatsAvailable],[DateFrom],[DateTo],[IsAvailable])
         values('C# Fundamentals', 'Some description', 1, getdate(), (getdate() + 30), 1)

insert into [dbo].[Courses] ([Name],[Description],[SeatsAvailable],[DateFrom],[DateTo],[IsAvailable])
         values('Java Fundamentals', 'Some description', 1, getdate(), (getdate() + 30), 1)

insert into [dbo].[Courses] ([Name],[Description],[SeatsAvailable],[DateFrom],[DateTo],[IsAvailable])
         values('ASP.NET Core Fundamentals', 'Some description', 1, getdate(), (getdate() + 30), 1)

INSERT INTO [dbo].[Students]([FirstName],[LastName],[Email],[Phone])
     VALUES('Georgy', 'Petrov', 'g.petrov@gmail.com', '+359887665544')

INSERT INTO [dbo].[Students]([FirstName],[LastName],[Email],[Phone])
    VALUES('Martin', 'Marinov', 'm.marinov@gmail.com', '+359887665533')

INSERT INTO [dbo].[Students]([FirstName],[LastName],[Email],[Phone])
    VALUES('Gergana', 'Minkova', 'g.minkova@gmail.com', '+359887775533')

USE [AcademyLink]
GO

declare @StudentID int = (select top 1 StudentID from Students),
	    @CourseID int = (select top 1 CourseID from Courses)

INSERT INTO [dbo].[StudensEnrolledCourses] ([StudentId],[CourseId],[EnrollmentDate],[Progress],[Status])
     VALUES (@StudentID, @CourseID, getdate(), 0.00, 'Enrolled')

SELECT @StudentID  = (select top 1 StudentID from Students where StudentID != @StudentID)
SELECT @CourseID  = (select top 1 CourseID from Courses where CourseID != @CourseID)

INSERT INTO [dbo].[StudensEnrolledCourses]
           ([StudentId],[CourseId],[EnrollmentDate],[Progress],[Status])
     VALUES
           (@StudentID, @CourseID, getdate(), 0.00, 'Enrolled')

INSERT INTO [dbo].[StudensEnrolledCourses]
           ([StudentId],[CourseId],[EnrollmentDate],[Progress],[Status])
     VALUES
           (2, 5, getdate()+2, 0.00, 'Enrolled')

INSERT INTO [dbo].[StudensEnrolledCourses]
           ([StudentId],[CourseId],[EnrollmentDate],[Progress],[Status])
     VALUES
           (2, 3, getdate()+20, 0.00, 'Enrolled')
GO

select * from Students
select * from Courses
select * from StudensEnrolledCourses
