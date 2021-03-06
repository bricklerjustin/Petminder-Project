USE [master]
GO
/****** Object:  Database [PetminderDB]    Script Date: 11/7/2020 15:26:09 ******/
CREATE DATABASE [PetminderDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PetminderDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PetminderDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PetminderDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PetminderDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PetminderDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PetminderDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PetminderDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PetminderDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PetminderDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PetminderDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PetminderDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [PetminderDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PetminderDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PetminderDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PetminderDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PetminderDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PetminderDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PetminderDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PetminderDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PetminderDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PetminderDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PetminderDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PetminderDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PetminderDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PetminderDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PetminderDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PetminderDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PetminderDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PetminderDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PetminderDB] SET  MULTI_USER 
GO
ALTER DATABASE [PetminderDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PetminderDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PetminderDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PetminderDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PetminderDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PetminderDB] SET QUERY_STORE = OFF
GO
USE [PetminderDB]
GO
/****** Object:  Table [dbo].[accounts]    Script Date: 11/7/2020 15:26:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[accounts](
	[id] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NULL,
	[api_key] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK__accounts__46A222CD0939533E] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[filedata]    Script Date: 11/7/2020 15:26:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[filedata](
	[id] [uniqueidentifier] NOT NULL,
	[data] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_filedata] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[files]    Script Date: 11/7/2020 15:26:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[files](
	[id] [uniqueidentifier] NOT NULL,
	[account_id] [uniqueidentifier] NOT NULL,
	[pet_id] [uniqueidentifier] NOT NULL,
	[type] [nvarchar](50) NOT NULL,
	[name] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_files] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pets]    Script Date: 11/7/2020 15:26:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pets](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[weight] [nvarchar](255) NULL,
	[age] [int] NULL,
	[type] [nvarchar](255) NULL,
	[breed] [nvarchar](255) NULL,
	[account_id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK__pets__3213E83FE95348DF] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[reminders]    Script Date: 11/7/2020 15:26:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[reminders](
	[name] [nvarchar](500) NOT NULL,
	[id] [uniqueidentifier] NOT NULL,
	[message] [nvarchar](500) NOT NULL,
	[complete] [bit] NOT NULL,
	[repeat] [bit] NOT NULL,
	[pet_id] [uniqueidentifier] NOT NULL,
	[account_id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_reminders] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 11/7/2020 15:26:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [uniqueidentifier] NOT NULL,
	[username] [nvarchar](250) NOT NULL,
	[password_hash] [nvarchar](max) NOT NULL,
	[api_key] [nvarchar](max) NOT NULL,
	[google_auth] [nvarchar](max) NULL,
	[account_id] [uniqueidentifier] NOT NULL,
	[email] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[accounts] ADD  CONSTRAINT [DF__accounts__accoun__2B3F6F97]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[accounts] ADD  CONSTRAINT [DF_accounts_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[files] ADD  CONSTRAINT [DF_files_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[pets] ADD  CONSTRAINT [DF__pets__id__276EDEB3]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[files]  WITH CHECK ADD  CONSTRAINT [FK_files_pets] FOREIGN KEY([pet_id])
REFERENCES [dbo].[pets] ([id])
GO
ALTER TABLE [dbo].[files] CHECK CONSTRAINT [FK_files_pets]
GO
ALTER TABLE [dbo].[pets]  WITH CHECK ADD  CONSTRAINT [FK_pets_pets] FOREIGN KEY([account_id])
REFERENCES [dbo].[accounts] ([id])
GO
ALTER TABLE [dbo].[pets] CHECK CONSTRAINT [FK_pets_pets]
GO
ALTER TABLE [dbo].[reminders]  WITH CHECK ADD  CONSTRAINT [FK_reminders_pets] FOREIGN KEY([pet_id])
REFERENCES [dbo].[pets] ([id])
GO
ALTER TABLE [dbo].[reminders] CHECK CONSTRAINT [FK_reminders_pets]
GO
USE [master]
GO
ALTER DATABASE [PetminderDB] SET  READ_WRITE 
GO
