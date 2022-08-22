USE [Coins]
GO

/****** Object:  Table [dbo].[TBEXECUTIONHISTORY]    Script Date: 27/05/2022 20:01:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBEXECUTIONHISTORY]') AND type in (N'U'))
DROP TABLE [dbo].[TBEXECUTIONHISTORY]
GO

/****** Object:  Table [dbo].[TBEXECUTIONHISTORY]    Script Date: 27/05/2022 20:01:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TBEXECUTIONHISTORY](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PlaceExecution] [varchar](60) NULL,
	[Description] [nvarchar](max) NULL,
	[DateExecution] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


USE [Coins]
GO

/****** Object:  Table [dbo].[TBMARKET]    Script Date: 27/05/2022 20:02:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBMARKET]') AND type in (N'U'))
DROP TABLE [dbo].[TBMARKET]
GO

/****** Object:  Table [dbo].[TBMARKET]    Script Date: 27/05/2022 20:02:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TBMARKET](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[BaseSymbol] [varchar](50) NULL,
	[Symbol] [varchar](50) NULL,
	[Broker] [varchar](50) NULL,
	[HighPrice] [varchar](50) NULL,
	[LowPrice] [varchar](50) NULL,
	[Volume] [varchar](50) NULL,
	[OpenTime] [varchar](50) NULL,
	[CloseTime] [varchar](50) NULL,
	[OpenPrice] [varchar](50) NULL,
	[ChangePercent] [varchar](50) NULL,
	[LastPrice] [varchar](80) NULL,
	[LastUpdate] [datetime] NULL,
	[CaptureDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
