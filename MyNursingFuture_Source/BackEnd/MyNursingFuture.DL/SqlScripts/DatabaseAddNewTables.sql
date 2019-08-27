/****** Object:  Table [dbo].[Employers]    Script Date: 27/08/2019 1:39:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employers](
	[EmployerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Email] [varchar](250) NOT NULL,
	[Password] [varchar](250) NOT NULL,
	[ApnaMemberId] [int] NULL,
	[ApnaUser] [bit] NOT NULL,
	[Hash] [varchar](31) NOT NULL,
	[CreateDate] [datetime] NULL,
	[ModifyDate] [datetime] NULL,
	[Active] [bit] NOT NULL,
	[Area] [varchar](50) NULL,
	[Age] [varchar](50) NULL,
	[Country] [varchar](50) NULL,
	[Suburb] [varchar](50) NULL,
	[PostalCode] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[Setting] [varchar](50) NULL,
 CONSTRAINT [PK_Employers] PRIMARY KEY CLUSTERED 
(
	[EmployerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


/****** Object:  Table [dbo].[Jobs]    Script Date: 27/08/2019 1:39:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jobs](
	[JobId] [int] IDENTITY(1,1) NOT NULL,
	[EmployerId] [int] NOT NULL Foreign Key References[dbo].[Employers] ([EmployerId]) ,
	[Title] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifyDate] [datetime] NULL,
	[Active] [bit] NOT NULL,
	[Area] [varchar](50) NULL,
	[Age] [varchar](50) NULL,
	[Country] [varchar](50) NULL,
	[Suburb] [varchar](50) NULL,
	[PostalCode] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[MinSalary] [int] Null,
	[MaxSalary] [int] Null,
	[Published] [bit] Not NULL,
 CONSTRAINT [PK_Jobs] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


/****** Object:  Table [dbo].[InterestedMatches]    Script Date: 27/08/2019 1:39:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InterestedMatches](
	[MatchId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL Foreign Key References[dbo].[Users] ([UserId]) ,
	[Job] [int] NOT NULL Foreign Key References[dbo].[Jobs] ([JobId]) ,
	[CreateDate] [datetime] NULL,
	[Accepted] [bit] NOT NULL,
	[Feedback] [varchar](max) Null

 CONSTRAINT [PK_InterestedMatches] PRIMARY KEY CLUSTERED 
(
	[MatchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[NotificationId] [int] IDENTITY(1,1) NOT NULL,
	[MatchId] [int] NOT NULL Foreign Key References[dbo].[InterestedMatches] ([MatchId]), 
	[ContactDays] [int] Null,
	[NotificationTypeId] [int] Null,
	[ShortMessage] [varchar](max) Null
	

 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED 
(
	[NotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationTypes](
	[NotificationTypeId] [int] IDENTITY(1,1) NOT NULL,
	[NotificationType] [varchar](100) NOT NULL, 
	[DefaultMessage] [varchar](max) Null
	

 CONSTRAINT [PK_NotificationTypes] PRIMARY KEY CLUSTERED 
(
	[NotificationTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
