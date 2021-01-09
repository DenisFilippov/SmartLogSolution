USE [master]
GO
/****** Object:  Database [smartLog]    Script Date: 09.01.2021 21:44:55 ******/
CREATE DATABASE [smartLog]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'smartLog', FILENAME = N'E:\Projects\Db\SmartLog\smartLog.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'smartLog_log', FILENAME = N'E:\Projects\Db\SmartLog\smartLog_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [smartLog] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [smartLog].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [smartLog] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [smartLog] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [smartLog] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [smartLog] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [smartLog] SET ARITHABORT OFF 
GO
ALTER DATABASE [smartLog] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [smartLog] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [smartLog] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [smartLog] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [smartLog] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [smartLog] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [smartLog] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [smartLog] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [smartLog] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [smartLog] SET  DISABLE_BROKER 
GO
ALTER DATABASE [smartLog] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [smartLog] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [smartLog] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [smartLog] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [smartLog] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [smartLog] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [smartLog] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [smartLog] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [smartLog] SET  MULTI_USER 
GO
ALTER DATABASE [smartLog] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [smartLog] SET DB_CHAINING OFF 
GO
ALTER DATABASE [smartLog] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [smartLog] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [smartLog] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [smartLog] SET QUERY_STORE = OFF
GO
USE [smartLog]
GO
USE [smartLog]
GO
/****** Object:  Sequence [dbo].[seq_custom_attributes]    Script Date: 09.01.2021 21:44:55 ******/
CREATE SEQUENCE [dbo].[seq_custom_attributes] 
 AS [bigint]
 START WITH 0
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE 
GO
USE [smartLog]
GO
/****** Object:  Sequence [dbo].[seq_logs]    Script Date: 09.01.2021 21:44:55 ******/
CREATE SEQUENCE [dbo].[seq_logs] 
 AS [bigint]
 START WITH 0
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE 
GO
/****** Object:  Table [dbo].[custom_attributes]    Script Date: 09.01.2021 21:44:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[custom_attributes](
	[custom_attributes_id] [bigint] NOT NULL,
	[logs_id] [bigint] NOT NULL,
	[name] [nvarchar](256) NOT NULL,
	[value] [nvarchar](max) NULL,
 CONSTRAINT [PK_custom_attributes] PRIMARY KEY CLUSTERED 
(
	[custom_attributes_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[log_types]    Script Date: 09.01.2021 21:44:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[log_types](
	[log_types_id] [smallint] NOT NULL,
	[name] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_log_types] PRIMARY KEY CLUSTERED 
(
	[log_types_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UNQ_log_types] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[logs]    Script Date: 09.01.2021 21:44:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[logs](
	[logs_id] [bigint] NOT NULL,
	[log_guid] [uniqueidentifier] NOT NULL,
	[parent] [bigint] NOT NULL,
	[method_name] [nvarchar](256) NOT NULL,
	[type] [smallint] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[message] [nvarchar](max) NULL,
 CONSTRAINT [PK_logs] PRIMARY KEY CLUSTERED 
(
	[logs_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [INDX_custom_attributes_logs_id]    Script Date: 09.01.2021 21:44:55 ******/
CREATE NONCLUSTERED INDEX [INDX_custom_attributes_logs_id] ON [dbo].[custom_attributes]
(
	[logs_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [INDX_logs_log_guid]    Script Date: 09.01.2021 21:44:55 ******/
CREATE NONCLUSTERED INDEX [INDX_logs_log_guid] ON [dbo].[logs]
(
	[log_guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [INDX_logs_parent]    Script Date: 09.01.2021 21:44:55 ******/
CREATE NONCLUSTERED INDEX [INDX_logs_parent] ON [dbo].[logs]
(
	[parent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[custom_attributes] ADD  DEFAULT (NEXT VALUE FOR [seq_custom_attributes]) FOR [custom_attributes_id]
GO
ALTER TABLE [dbo].[logs] ADD  CONSTRAINT [DF__logs__logs_id__45F365D3]  DEFAULT (NEXT VALUE FOR [seq_logs]) FOR [logs_id]
GO
ALTER TABLE [dbo].[logs] ADD  CONSTRAINT [DF_logs_parent]  DEFAULT ((0)) FOR [parent]
GO
ALTER TABLE [dbo].[logs] ADD  CONSTRAINT [DF_logs_type]  DEFAULT ((1)) FOR [type]
GO
ALTER TABLE [dbo].[logs] ADD  CONSTRAINT [DF_logs_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[custom_attributes]  WITH CHECK ADD  CONSTRAINT [FK_custom_attributes_logs] FOREIGN KEY([logs_id])
REFERENCES [dbo].[logs] ([logs_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[custom_attributes] CHECK CONSTRAINT [FK_custom_attributes_logs]
GO
USE [master]
GO
ALTER DATABASE [smartLog] SET  READ_WRITE 
GO
