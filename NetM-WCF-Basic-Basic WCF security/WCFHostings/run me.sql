--table roles
USE [Northwind]
GO

/****** Object:  Table [dbo].[Roles]    Script Date: 04/28/2016 10:45:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Roles](
	[RoleId] [bigint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role_Id] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

INSERT INTO [Northwind].[dbo].[Roles]
           ([RoleId]
           ,[Name])
     VALUES
           (1, 'M')
GO


INSERT INTO [Northwind].[dbo].[Roles]
           ([RoleId]
           ,[Name])
     VALUES
           (1, 'O')
GO


INSERT INTO [Northwind].[dbo].[Roles]
           ([RoleId]
           ,[Name])
     VALUES
           (1, 'G')
GO


--table UserRoles
USE [Northwind]
GO

/****** Object:  Table [dbo].[UserRoles]    Script Date: 04/28/2016 10:46:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserRoles](
	[UserRoleId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
 CONSTRAINT [PK_Id] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO

ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Role]
GO

ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_User]
GO

INSERT INTO [Northwind].[dbo].[UserRoles]
           ([UserId]
           ,[RoleId])
     VALUES
           (1, 2)
GO

INSERT INTO [Northwind].[dbo].[UserRoles]
           ([UserId]
           ,[RoleId])
     VALUES
           (2, 3)
GO

INSERT INTO [Northwind].[dbo].[UserRoles]
           ([UserId]
           ,[RoleId])
     VALUES
           (4, 3)
GO

INSERT INTO [Northwind].[dbo].[UserRoles]
           ([UserId]
           ,[RoleId])
     VALUES
           (3, 1)
GO


--table Users
USE [Northwind]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 04/28/2016 10:47:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NULL,
 CONSTRAINT [PK_User_Id] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

INSERT INTO [Northwind].[dbo].[Users]
           ([Name]
           ,[Password])
     VALUES
           ('Вася', '2092545D92E30FF37C855876D41C6E08') --pass1
GO

INSERT INTO [Northwind].[dbo].[Users]
           ([Name]
           ,[Password])
     VALUES
           ('Петя', '418C4F74FB3BF109473D169656886D7C') --pass2
GO

INSERT INTO [Northwind].[dbo].[Users]
           ([Name]
           ,[Password])
     VALUES
           ('Коля', '6F3A3B04F3E8FC8D83275BB1FAB647F6') --pass3
GO

INSERT INTO [Northwind].[dbo].[Users]
           ([Name]
           ,[Password])
     VALUES
           ('Толя', '2BF0011540B7F03F673D563795862A3B') --pass4
GO

INSERT INTO [Northwind].[dbo].[Users]
           ([Name]
           ,[Password])
     VALUES
           ('Женя', 'FE88C549FF1A87EC08DFF49452504197') --pass5
GO

INSERT INTO [Northwind].[dbo].[Users]
           ([Name]
           ,[Password])
     VALUES
           ('Ваня', 'B0ADF3BD1C66361617BD8508886B4D24') --pass6
GO


--auth procedure
USE [Northwind]
GO
/****** Object:  StoredProcedure [dbo].[IsUserExist]    Script Date: 4/27/2016 11:11:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[IsUserExist] 
	@name NVARCHAR(50),
	@password NVARCHAR(50)
AS
BEGIN
    declare @success INT
    declare @cnt INT

    --if success = 0, then ok, else error
    --1 - some error
    --2 - user is not exist
    --3 - wrong password
    set @success = 1

    select @cnt = count(*) from Northwind.dbo.users 
    where Name = @name

    if @cnt = 0 
    begin
        set @success = 2
        select @success
        return 
    end

	select @cnt = count(*) from Northwind.dbo.users  
    where Name = @name
    and Password = CONVERT(VARCHAR(32), HashBytes('MD5', @password), 2)
    
    if @cnt = 0
        set @success = 3
    else
        set @success = 0

    select @success
END