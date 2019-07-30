- For Jsonp

Callback is javascript wrapper function name
http://localhost:61175/webapi/employees?callback=test
Header Accept:Application/Javascript

And to get JSON
Header Accept:Application/Json


//Sql table creation

USE [PracticeSQL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Employee_Master](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeName] [varchar](255) NULL,
	[EmployeeDOJ] [date] NULL,
	[EmployeeDesignation] [varchar](255) NULL,
	[EmployeeDepartment] [varchar](255) NULL,
	[Gender] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


Create Table Users(
Id int identity(1,1) primary key,
Username varchar(255) not null unique,
[Password] varchar(255) not null,
)

USE [PracticeSQL]
GO

INSERT INTO [dbo].[Users]
           ([Username]
           ,[Password])
     VALUES
           ('310193143',
           'Test123')
GO
INSERT INTO [dbo].[Users]
           ([Username]
           ,[Password])
     VALUES
           ('310193142',
           'Test123')
GO
INSERT INTO [dbo].[Users]
           ([Username]
           ,[Password])
     VALUES
           ('310193141',
           'Test123')
GO
INSERT INTO [dbo].[Users]
           ([Username]
           ,[Password])
     VALUES
           ('310193140',
           'Test123')
GO

