
/****** Object:  Database [MyNursingFuture]    Script Date: 14/02/2018 1:39:38 PM ******/
CREATE DATABASE [MyNursingFuture]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MyNursingFuture', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\MyNursingFuture.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MyNursingFuture_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\MyNursingFuture_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [MyNursingFuture] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MyNursingFuture].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MyNursingFuture] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MyNursingFuture] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MyNursingFuture] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MyNursingFuture] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MyNursingFuture] SET ARITHABORT OFF 
GO
ALTER DATABASE [MyNursingFuture] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MyNursingFuture] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MyNursingFuture] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MyNursingFuture] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MyNursingFuture] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MyNursingFuture] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MyNursingFuture] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MyNursingFuture] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MyNursingFuture] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MyNursingFuture] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MyNursingFuture] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MyNursingFuture] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MyNursingFuture] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MyNursingFuture] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MyNursingFuture] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MyNursingFuture] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MyNursingFuture] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MyNursingFuture] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MyNursingFuture] SET  MULTI_USER 
GO
ALTER DATABASE [MyNursingFuture] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MyNursingFuture] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MyNursingFuture] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MyNursingFuture] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MyNursingFuture] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MyNursingFuture', N'ON'
GO
ALTER DATABASE [MyNursingFuture] SET QUERY_STORE = OFF
GO
USE [MyNursingFuture]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [MyNursingFuture]
GO
/****** Object:  User [IIS APPPOOL\DefaultAppPool]    Script Date: 14/02/2018 1:39:38 PM ******/
CREATE USER [IIS APPPOOL\DefaultAppPool] FOR LOGIN [IIS APPPOOL\DefaultAppPool] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [IIS APPPOOL\DefaultAppPool]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [IIS APPPOOL\DefaultAppPool]
GO
/****** Object:  UserDefinedFunction [dbo].[GetJSON]    Script Date: 14/02/2018 1:39:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[GetJSON] 
(	
	-- Add the parameters for the function here
	@json varchar(MAX),
	@jsonPath varchar(100)
	
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT * FROM OPENJSON(JSON_QUERY(@json, @jsonPath))
)

GO
/****** Object:  Table [dbo].[Actions]    Script Date: 14/02/2018 1:39:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Actions](
	[ActionId] [int] IDENTITY(1,1) NOT NULL,
	[Text] [varchar](5000) NULL,
	[Title] [varchar](500) NOT NULL,
	[Active] [bit] NOT NULL,
	[Type] [varchar](50) NULL,
 CONSTRAINT [PK_Actions] PRIMARY KEY CLUSTERED 
(
	[ActionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Administrators]    Script Date: 14/02/2018 1:39:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Administrators](
	[AdministratorId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](250) NOT NULL,
	[Password] [varchar](250) NOT NULL,
	[Hash] [varchar](31) NULL,
	[Sealed] [bit] NOT NULL,
	[Name] [varchar](250) NULL,
 CONSTRAINT [PK_Administrators] PRIMARY KEY CLUSTERED 
(
	[AdministratorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AnonUserQuizzes]    Script Date: 10/07/2018 5:01:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnonUserQuizzes](
	[UserQuizId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](250) NOT NULL,
	[Name] [varchar](250) NOT NULL,
	[QuizId] [int] NOT NULL,
	[DateVal] [datetime] NOT NULL,
	[Results] [varchar](max) NULL,
	[Completed] [bit] NOT NULL,
	[Date] [varchar](50) NULL,
	[Type] [varchar](50) NOT NULL,
	[NurseType] [varchar](10) NULL,
	[ActiveWorking] [varchar](10) NULL,
	[Area] [varchar](10) NULL,
	[Setting] [varchar](10) NULL,
	[Age] [varchar](10) NULL,
	[Country] [varchar](50) NULL,
	[Suburb] [varchar](250) NULL,
	[PostCode] [varchar](25) NULL,
	[State] [varchar](50) NULL,
	[PatientsTitle] [varchar](50) NULL,
	[Qualification] [varchar](10) NULL,
 CONSTRAINT [PK_AnonUsersQuizzes] PRIMARY KEY CLUSTERED 
(
	[UserQuizId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Answers]    Script Date: 14/02/2018 1:39:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answers](
	[AnswerId] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NOT NULL,
	[Text] [varchar](1000) NOT NULL,
	[Value] [money] NOT NULL,
	[MatchText] [varchar](500) NULL,
	[Active] [bit] NOT NULL,
	[Type] [varchar](50) NULL,
	[TextValue] [varchar](500) NULL,
 CONSTRAINT [PK_Answers] PRIMARY KEY CLUSTERED 
(
	[AnswerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Articles]    Script Date: 14/02/2018 1:39:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articles](
	[ArticleId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](250) NOT NULL,
	[Text] [varchar](max) NULL,
	[Type] [varchar](50) NOT NULL,
	[CategoryId] [int] NULL,
	[LinkId] [int] NULL,
	[Name] [varchar](250) NOT NULL,
	[Active] [bit] NOT NULL,
	[Published] [bit] NOT NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_Articles] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ArticlesUniqueName] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Aspects]    Script Date: 14/02/2018 1:39:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aspects](
	[AspectId] [int] IDENTITY(1,1) NOT NULL,
	[DomainId] [int] NOT NULL,
	[LinkId] [int] NOT NULL,
	[Title] [varchar](250) NOT NULL,
	[Text] [varchar](5000) NULL,
	[Examples] [varchar](5000) NULL,
	[OnlineResources] [varchar](5000) NULL,
	[FurtherEducation] [varchar](5000) NULL,
	[PeopleContact] [varchar](5000) NULL,
	[Levels] [varchar](5000) NULL,
	[Published] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
	[Position] [int] NOT NULL,
 CONSTRAINT [PK_Aspects] PRIMARY KEY CLUSTERED 
(
	[AspectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspectsActions]    Script Date: 14/02/2018 1:39:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspectsActions](
	[AspectId] [int] NOT NULL,
	[ActionId] [int] NOT NULL,
	[LevelAction] [int] NOT NULL,
	[Position] [int] NOT NULL,
 CONSTRAINT [PK_AspectsActions] PRIMARY KEY CLUSTERED 
(
	[AspectId] ASC,
	[ActionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categories]    Script Date: 14/02/2018 1:39:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Configurations]    Script Date: 14/02/2018 1:39:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Configurations](
	[ConfigurationId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Value] [varchar](1500) NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_Configurations] PRIMARY KEY CLUSTERED 
(
	[ConfigurationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ContentItems]    Script Date: 14/02/2018 1:39:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContentItems](
	[ContentItemId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NULL,
	[Text] [varchar](max) NULL,
	[Position] [int] NOT NULL,
	[SectionId] [int] NULL,
	[ArticleId] [int] NULL,
	[Title] [varchar](250) NULL,
	[Type] [varchar](50) NULL,
	[Image] [varchar](250) NULL,
	[Carousel] [varchar](2500) NULL,
	[Link] [varchar](250) NULL,
	[ButtonLink] [varchar](250) NULL,
	[Video] [varchar](1000) NULL,
	[TitleImage] [varchar](100) NULL,
	[Icon] [varchar](250) NULL,
 CONSTRAINT [PK_ContentItems] PRIMARY KEY CLUSTERED 
(
	[ContentItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Definitions]    Script Date: 14/02/2018 1:39:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Definitions](
	[DefinitionId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NOT NULL,
	[Text] [varchar](1000) NOT NULL,
 CONSTRAINT [PK_Definitions] PRIMARY KEY CLUSTERED 
(
	[DefinitionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Domains]    Script Date: 14/02/2018 1:39:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Domains](
	[DomainId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NOT NULL,
	[Text] [varchar](max) NULL,
	[LinkId] [int] NULL,
	[Image] [varchar](500) NULL,
	[Icon] [varchar](500) NULL,
	[Title] [varchar](250) NULL,
	[Framework] [varchar](250) NULL,
	[Active] [bit] NOT NULL,
	[Position] [int] NOT NULL,
 CONSTRAINT [PK_Domains] PRIMARY KEY CLUSTERED 
(
	[DomainId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_DomainUniqueName] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DomainsActions]    Script Date: 14/02/2018 1:39:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DomainsActions](
	[DomainId] [int] NOT NULL,
	[ActionId] [int] NOT NULL,
	[Position] [int] NOT NULL,
 CONSTRAINT [PK_DomainsActions_1] PRIMARY KEY CLUSTERED 
(
	[DomainId] ASC,
	[ActionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EndorsedLogos]    Script Date: 14/02/2018 1:39:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EndorsedLogos](
	[EndorsedLogoId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Image] [varchar](50) NULL,
 CONSTRAINT [PK_EndorsedLogo] PRIMARY KEY CLUSTERED 
(
	[EndorsedLogoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Images]    Script Date: 14/02/2018 1:39:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[ImageId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[ImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Links]    Script Date: 14/02/2018 1:39:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Links](
	[LinkId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[Href] [varchar](250) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Links] PRIMARY KEY CLUSTERED 
(
	[LinkId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LogChanges]    Script Date: 14/02/2018 1:39:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogChanges](
	[LogChangeId] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [varchar](50) NOT NULL,
	[Identifier] [int] NULL,
	[Username] [varchar](50) NOT NULL,
	[Name] [varchar](250) NULL,
	[Date] [datetime] NULL,
	[Operation] [varchar](250) NULL,
 CONSTRAINT [PK_LogChanges] PRIMARY KEY CLUSTERED 
(
	[LogChangeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Menus]    Script Date: 14/02/2018 1:39:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menus](
	[MenuId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](500) NOT NULL,
	[Href] [varchar](500) NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[Position] [int] NOT NULL,
	[Submenu] [bit] NULL,
	[Separator] [bit] NULL,
 CONSTRAINT [PK_Menues] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PostCards]    Script Date: 14/02/2018 1:39:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostCards](
	[PostCardId] [int] IDENTITY(1,1) NOT NULL,
	[Text] [varchar](250) NULL,
	[Image] [varchar](50) NULL,
 CONSTRAINT [PK_PostCards] PRIMARY KEY CLUSTERED 
(
	[PostCardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Questions]    Script Date: 14/02/2018 1:39:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questions](
	[QuestionId] [int] IDENTITY(1,1) NOT NULL,
	[QuizId] [int] NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[AspectId] [int] NULL,
	[Text] [varchar](500) NULL,
	[SubText] [varchar](500) NULL,
	[Active] [bit] NOT NULL,
	[Requirements] [varchar](1000) NULL,
	[Position] [int] NOT NULL,
	[Examples] [varchar](1000) NULL,
 CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Quizzes]    Script Date: 14/02/2018 1:39:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quizzes](
	[QuizId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NOT NULL,
	[Type] [varchar](50) NULL,
	[DomainId] [int] NULL,
	[Active] [bit] NOT NULL,
	[LastModify] [datetime] NULL,
 CONSTRAINT [PK_Quizzes] PRIMARY KEY CLUSTERED 
(
	[QuizId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Reasons]    Script Date: 14/02/2018 1:39:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reasons](
	[ReasonId] [int] IDENTITY(1,1) NOT NULL,
	[Ix] [int] NOT NULL,
	[Title] [varchar](250) NULL,
	[Text] [varchar](max) NULL,
	[TextPrev] [varchar](500) NULL,
 CONSTRAINT [PK_Reasons] PRIMARY KEY CLUSTERED 
(
	[ReasonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 14/02/2018 1:39:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NULL,
	[LinkId] [int] NULL,
	[WhatIs] [varchar](max) NULL,
	[WhatIsTheirRole] [varchar](max) NULL,
	[Accountabilities] [varchar](max) NULL,
	[Examples] [varchar](max) NULL,
	[FurtherInformation] [varchar](max) NULL,
	[Pathways] [varchar](max) NULL,
	[Title] [varchar](250) NULL,
	[LinkName] [varchar](100) NULL,
	[Published] [bit] NULL,
	[Active] [bit] NOT NULL,
	[Position] [int] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_RolesUniqueName] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sections]    Script Date: 14/02/2018 1:39:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sections](
	[SectionId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NOT NULL,
	[Title] [varchar](250) NOT NULL,
	[Sealed] [bit] NOT NULL,
	[Published] [bit] NULL,
	[LinkId] [int] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Sections] PRIMARY KEY CLUSTERED 
(
	[SectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_UniqueName] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sectors]    Script Date: 14/02/2018 1:39:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sectors](
	[SectorId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NOT NULL,
	[LinkId] [int] NULL,
	[Title] [varchar](250) NOT NULL,
	[Published] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Sectors] PRIMARY KEY CLUSTERED 
(
	[SectorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_SectorsUniqueName] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SectorsQuestions]    Script Date: 14/02/2018 1:39:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SectorsQuestions](
	[QuestionId] [int] NOT NULL,
	[SectorId] [int] NOT NULL,
	[Value] [money] NULL,
 CONSTRAINT [PK_SectorsQuestions] PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC,
	[SectorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SectorViews]    Script Date: 14/02/2018 1:39:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SectorViews](
	[SectorViewId] [int] IDENTITY(1,1) NOT NULL,
	[SectorId] [int] NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[Framework] [varchar](20) NOT NULL,
	[Intro] [varchar](5000) NULL,
	[Video] [varchar](250) NULL,
	[MoreStories] [varchar](250) NULL,
	[CareerPathways] [varchar](5000) NULL,
	[WorkEnvironments] [varchar](5000) NULL,
	[CareerOpportunities] [varchar](5000) NULL,
	[EducationOpportunities] [varchar](5000) NULL,
	[ContactText] [varchar](5000) NULL,
	[OnlineResources] [varchar](5000) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_SectorViews] PRIMARY KEY CLUSTERED 
(
	[SectorViewId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserDataQuestions]    Script Date: 14/02/2018 1:39:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDataQuestions](
	[UserDataQuestionId] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NOT NULL,
	[FieldName] [varchar](50) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 14/02/2018 1:39:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Email] [varchar](250) NOT NULL,
	[Password] [varchar](250) NOT NULL,
	[ApnaMemberId] [int] NULL,
	[ApnaUser] [bit] NOT NULL,
	[Hash] [varchar](31) NOT NULL,
	[CreateDate] [datetime] NULL,
	[ModifyDate] [datetime] NULL,
	[Active] [bit] NOT NULL,
	[NurseType] [varchar](50) NULL,
	[ActiveWorking] [varchar](50) NULL,
	[Area] [varchar](50) NULL,
	[Age] [varchar](50) NULL,
	[Country] [varchar](50) NULL,
	[Suburb] [varchar](50) NULL,
	[PostalCode] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[Patients] [varchar](1000) NULL,
	[PatientsTitle] [varchar](50) NULL,
	[Qualification] [varchar](50) NULL,
	[Setting] [varchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UsersQuizzes]    Script Date: 14/02/2018 1:39:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersQuizzes](
	[UserQuizId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[QuizId] [int] NOT NULL,
	[DateVal] [datetime] NOT NULL,
	[Results] [varchar](max) NULL,
	[Completed] [bit] NOT NULL,
	[Date] [varchar](50) NULL,
	[Type] [varchar](50) NOT NULL,
	[Survey] [varchar](250) NULL,
 CONSTRAINT [PK_UsersQuizzes] PRIMARY KEY CLUSTERED 
(
	[UserQuizId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Administrators]    Script Date: 14/02/2018 1:39:40 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Administrators] ON [dbo].[Administrators]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Actions] ADD  CONSTRAINT [DF_Actions_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Administrators] ADD  CONSTRAINT [DF_Administrators_Sealed]  DEFAULT ((0)) FOR [Sealed]
GO
ALTER TABLE [dbo].[Answers] ADD  CONSTRAINT [DF_Answers_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Articles] ADD  CONSTRAINT [DF_Articles_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Articles] ADD  CONSTRAINT [DF_Articles_Published]  DEFAULT ((0)) FOR [Published]
GO
ALTER TABLE [dbo].[Aspects] ADD  CONSTRAINT [DF_Aspects_Published]  DEFAULT ((0)) FOR [Published]
GO
ALTER TABLE [dbo].[Aspects] ADD  CONSTRAINT [DF_Aspects_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Aspects] ADD  DEFAULT ((0)) FOR [Position]
GO
ALTER TABLE [dbo].[Domains] ADD  CONSTRAINT [DF_Domains_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Domains] ADD  DEFAULT ((0)) FOR [Position]
GO
ALTER TABLE [dbo].[Questions] ADD  CONSTRAINT [DF_Questions_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Questions] ADD  CONSTRAINT [DF_Questions_Position]  DEFAULT ((0)) FOR [Position]
GO
ALTER TABLE [dbo].[Quizzes] ADD  CONSTRAINT [DF_Quizzes_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_Actiove]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Roles] ADD  DEFAULT ((0)) FOR [Position]
GO
ALTER TABLE [dbo].[Sections] ADD  CONSTRAINT [DF_Sections_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Sectors] ADD  CONSTRAINT [DF_Sectors_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[SectorViews] ADD  CONSTRAINT [DF_SectorViews_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_ApnaUser]  DEFAULT ((0)) FOR [ApnaUser]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Hash]  DEFAULT ((0)) FOR [Hash]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Answers]  WITH CHECK ADD  CONSTRAINT [FK_Answers_Questions] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Questions] ([QuestionId])
GO
ALTER TABLE [dbo].[Answers] CHECK CONSTRAINT [FK_Answers_Questions]
GO
ALTER TABLE [dbo].[Articles]  WITH CHECK ADD  CONSTRAINT [FK_Articles_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
GO
ALTER TABLE [dbo].[Articles] CHECK CONSTRAINT [FK_Articles_Categories]
GO
ALTER TABLE [dbo].[Aspects]  WITH CHECK ADD  CONSTRAINT [FK_Aspects_Domains] FOREIGN KEY([DomainId])
REFERENCES [dbo].[Domains] ([DomainId])
GO
ALTER TABLE [dbo].[Aspects] CHECK CONSTRAINT [FK_Aspects_Domains]
GO
ALTER TABLE [dbo].[AspectsActions]  WITH CHECK ADD  CONSTRAINT [FK_AspectsActions_Actions] FOREIGN KEY([ActionId])
REFERENCES [dbo].[Actions] ([ActionId])
GO
ALTER TABLE [dbo].[AspectsActions] CHECK CONSTRAINT [FK_AspectsActions_Actions]
GO
ALTER TABLE [dbo].[AspectsActions]  WITH CHECK ADD  CONSTRAINT [FK_AspectsActions_Aspects] FOREIGN KEY([AspectId])
REFERENCES [dbo].[Aspects] ([AspectId])
GO
ALTER TABLE [dbo].[AspectsActions] CHECK CONSTRAINT [FK_AspectsActions_Aspects]
GO
ALTER TABLE [dbo].[ContentItems]  WITH CHECK ADD  CONSTRAINT [FK_ContentItems_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([ArticleId])
GO
ALTER TABLE [dbo].[ContentItems] CHECK CONSTRAINT [FK_ContentItems_Articles]
GO
ALTER TABLE [dbo].[ContentItems]  WITH CHECK ADD  CONSTRAINT [FK_ContentItems_Sections] FOREIGN KEY([SectionId])
REFERENCES [dbo].[Sections] ([SectionId])
GO
ALTER TABLE [dbo].[ContentItems] CHECK CONSTRAINT [FK_ContentItems_Sections]
GO
ALTER TABLE [dbo].[DomainsActions]  WITH CHECK ADD  CONSTRAINT [FK_DomainsActions_Actions] FOREIGN KEY([ActionId])
REFERENCES [dbo].[Actions] ([ActionId])
GO
ALTER TABLE [dbo].[DomainsActions] CHECK CONSTRAINT [FK_DomainsActions_Actions]
GO
ALTER TABLE [dbo].[DomainsActions]  WITH CHECK ADD  CONSTRAINT [FK_DomainsActions_Domains] FOREIGN KEY([DomainId])
REFERENCES [dbo].[Domains] ([DomainId])
GO
ALTER TABLE [dbo].[DomainsActions] CHECK CONSTRAINT [FK_DomainsActions_Domains]
GO
ALTER TABLE [dbo].[Quizzes]  WITH CHECK ADD  CONSTRAINT [FK_Quizzes_Domains1] FOREIGN KEY([DomainId])
REFERENCES [dbo].[Domains] ([DomainId])
GO
ALTER TABLE [dbo].[Quizzes] CHECK CONSTRAINT [FK_Quizzes_Domains1]
GO
ALTER TABLE [dbo].[SectorsQuestions]  WITH CHECK ADD  CONSTRAINT [FK_SectorsQuestions_Questions] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Questions] ([QuestionId])
GO
ALTER TABLE [dbo].[SectorsQuestions] CHECK CONSTRAINT [FK_SectorsQuestions_Questions]
GO
ALTER TABLE [dbo].[SectorsQuestions]  WITH CHECK ADD  CONSTRAINT [FK_SectorsQuestions_Sectors] FOREIGN KEY([SectorId])
REFERENCES [dbo].[Sectors] ([SectorId])
GO
ALTER TABLE [dbo].[SectorsQuestions] CHECK CONSTRAINT [FK_SectorsQuestions_Sectors]
GO
ALTER TABLE [dbo].[SectorViews]  WITH CHECK ADD  CONSTRAINT [FK_SectorViews_Sectors] FOREIGN KEY([SectorId])
REFERENCES [dbo].[Sectors] ([SectorId])
GO
ALTER TABLE [dbo].[SectorViews] CHECK CONSTRAINT [FK_SectorViews_Sectors]
GO
ALTER TABLE [dbo].[UsersQuizzes]  WITH CHECK ADD  CONSTRAINT [FK_UsersQuizzes_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UsersQuizzes] CHECK CONSTRAINT [FK_UsersQuizzes_Users]
GO
/****** Object:  StoredProcedure [dbo].[GetExtract]    Script Date: 14/02/2018 1:39:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetExtract]
	-- Add the parameters for the stored procedure here
	@ExtractType int,
	@DateStart datetime,
	@DateEnd datetime
AS
BEGIN
	DECLARE @NurseTypeQuestionID INT
	SELECT @NurseTypeQuestionID = QuestionId FROM UserDataQuestions Where FieldName = 'NurseType'
	DECLARE @ActiveWorkingQuestionID INT
	SELECT @ActiveWorkingQuestionID = QuestionId FROM UserDataQuestions Where FieldName = 'ActiveWorking'
	DECLARE @AreaQuestionID INT
	SELECT @AreaQuestionID = QuestionId FROM UserDataQuestions Where FieldName = 'Area'
	DECLARE @SettingQuestionID INT
	SELECT @SettingQuestionID = QuestionId FROM UserDataQuestions Where FieldName = 'Setting'
	DECLARE @QualificationQuestionID INT
	SELECT @QualificationQuestionID = QuestionId FROM UserDataQuestions Where FieldName = 'Qualification'

	-- assessment
	IF (@ExtractType = 1)
	BEGIN
		SELECT 
			
			UserQuizId, 
			AssessmentDate = UsersQuizzes.Date,
			UsersQuizzes.Completed,
			DomainId = scores.[key],
			Domain = (SELECT Title FROM Domains WHERE DomainId = scores.[key]),
			
			
			[Level] = (CASE WHEN convert(float,scores.[value]) > 0.67 THEN 'ADVANCED' WHEN convert(float,scores.[value]) > 0.34 THEN 'INTERMEDIATE' WHEN convert(float,scores.[value]) > 0.1 THEN 'FOUNDATION' ELSE 'None' END),
			QuestionId = answers.[key],
			
			QuestionText = (SELECT Text From Questions WHERE QuestionId = answers.[key]),
			AnswerValue= Convert(float, answers.[value]),
			AnswerText = (SELECT [Text] FROM Answers a WHERE a.[QuestionId] = answers.[key] AND Convert(float, a.[Value]) = Convert(float, answers.[value])),
			Users.UserId, 
			Users.ApnaMemberId,
			UserName = Users.Name,
			Email, 
			UserCreateDate = Users.CreateDate, 
			 
			NurseType, 
			NurseTypeText = (SELECT [Text] FROM Answers WHERE QuestionId = @NurseTypeQuestionID AND Convert(float, [Value]) = Convert(float, NurseType)),
			ActiveWorking, 
			ActiveWorkingText = (SELECT [Text] FROM Answers WHERE QuestionId = @ActiveWorkingQuestionID AND Convert(float, [Value]) = Convert(varchar, ActiveWorking)),
			Area, 
			AreaText = (SELECT [Text] FROM Answers WHERE QuestionId = @AreaQuestionID AND Convert(float, [Value]) = Convert(varchar, Area)),
			Setting, 
			SettingText = (SELECT [Text] FROM Answers WHERE QuestionId = @SettingQuestionID AND Convert(float, [Value]) = Convert(varchar, Setting)),
			Age, Country, Suburb, Postalcode, State, 
			Patients, patientsTitle, Qualification

		FROM 
			Users INNER JOIN UsersQuizzes ON Users.UserId = UsersQuizzes.UserId
			CROSS APPLY GetJSON(Results, '$.results.score') AS scores
			CROSS APPLY GetJSON(Results, '$.answers') AS answers
			INNER JOIN Aspects ON Aspects.DomainId = scores.[key]
			INNER JOIN Questions ON Questions.AspectId = Aspects.AspectId AND Questions.QuestionId = answers.[key]
		WHERE 
			UsersQuizzes.type = 'ASSESSMENT'
			AND results IS NOT NULL
			AND UsersQuizzes.DateVal > @DateStart
			AND UsersQuizzes.DateVal < @DateEnd
		ORDER BY
			UserQuizId,
			Users.UserId,
			Aspects.DomainId,
			Questions.QuestionId

	END

	-- carrer quiz
	IF (@ExtractType = 2)
	BEGIN
		SELECT 
			UserQuizId, 
			QuizDate = UsersQuizzes.Date,
			UsersQuizzes.Completed,
			QuestionId = [key],
			QuestionText = (SELECT Text From Questions WHERE QuestionId = [key]),
			AnswerValue= [value],

			Users.UserId, 
			Users.ApnaMemberId,
			UserName = Users.Name,
			Email, 
			UserCreateDate = Users.CreateDate, 
			
			NurseType, 
			NurseTypeText = (SELECT [Text] FROM Answers WHERE QuestionId = @NurseTypeQuestionID AND Convert(float, [Value]) = Convert(float, NurseType)),
			ActiveWorking, 
			ActiveWorkingText = (SELECT [Text] FROM Answers WHERE QuestionId = @ActiveWorkingQuestionID AND Convert(float, [Value]) = Convert(varchar, ActiveWorking)),
			Area, 
			AreaText = (SELECT [Text] FROM Answers WHERE QuestionId = @AreaQuestionID AND Convert(float, [Value]) = Convert(varchar, Area)),
			Setting, 
			SettingText = (SELECT [Text] FROM Answers WHERE QuestionId = @SettingQuestionID AND Convert(float, [Value]) = Convert(varchar, Setting)),
			Age, Country, Suburb, Postalcode, State, 
			Patients, patientsTitle, Qualification

		FROM 
			Users INNER JOIN UsersQuizzes ON Users.UserId = UsersQuizzes.UserId
			INNER JOIN Quizzes ON Quizzes.QuizId = UsersQuizzes.QuizId AND Quizzes.Type='PATHWAY'
			CROSS APPLY GetJSON(Results, '$.answers')
		WHERE 
			UsersQuizzes.DateVal > @DateStart
			AND UsersQuizzes.DateVal < @DateEnd
		ORDER BY
			UserQuizId,
			Users.UserId,
			[key]
	END

	-- carrer quiz percentages
	IF (@ExtractType = 3)
	BEGIN
		SELECT 
			UserQuizId, 
			QuizDate = UsersQuizzes.Date,
			UsersQuizzes.Completed,
			SectorId = [key],
			Sector = (SELECT Title FROM Sectors WHERE SectorId = scores.[key]),
			Percentage = [value],

			Users.UserId, 
			Users.ApnaMemberId,
			UserName = Users.Name,
			Email, 
			UserCreateDate = Users.CreateDate, 
			
			NurseType, 
			NurseTypeText = (SELECT [Text] FROM Answers WHERE QuestionId = @NurseTypeQuestionID AND Convert(float, [Value]) = Convert(float, NurseType)),
			ActiveWorking, 
			ActiveWorkingText = (SELECT [Text] FROM Answers WHERE QuestionId = @ActiveWorkingQuestionID AND Convert(float, [Value]) = Convert(varchar, ActiveWorking)),
			Area, 
			AreaText = (SELECT [Text] FROM Answers WHERE QuestionId = @AreaQuestionID AND Convert(float, [Value]) = Convert(varchar, Area)),
			Setting, 
			SettingText = (SELECT [Text] FROM Answers WHERE QuestionId = @SettingQuestionID AND Convert(float, [Value]) = Convert(varchar, Setting)),
			Age, Country, Suburb, Postalcode, State, 
			Patients, patientsTitle, Qualification

		FROM 
			Users INNER JOIN UsersQuizzes ON Users.UserId = UsersQuizzes.UserId
			INNER JOIN Quizzes ON Quizzes.QuizId = UsersQuizzes.QuizId AND Quizzes.Type='PATHWAY'
			CROSS APPLY GetJSON(Results, '$.results.scorePercentages') AS scores
		WHERE 
			UsersQuizzes.DateVal > @DateStart
			AND UsersQuizzes.DateVal < @DateEnd
		ORDER BY
			UserQuizId,
			Users.UserId,
			[value] DESC
			
	END
		
END

GO
USE [master]
GO
ALTER DATABASE [MyNursingFuture] SET  READ_WRITE 
GO
