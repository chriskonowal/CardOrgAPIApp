USE [CardOrg]
GO

/****** Object:  Table [dbo].[Years]    Script Date: 3/18/2022 5:51:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID('dbo.FK_Card_GradeCompanyId') IS NOT NULL AND OBJECT_ID(N'dbo.Card') IS NOT NULL  
BEGIN
	ALTER TABLE dbo.Card DROP CONSTRAINT FK_Card_GradeCompanyId;
END

IF OBJECT_ID('dbo.FK_Card_LocationId') IS NOT NULL AND OBJECT_ID(N'dbo.Card') IS NOT NULL  
BEGIN
	ALTER TABLE dbo.Card DROP CONSTRAINT FK_Card_LocationId;
END

IF OBJECT_ID('dbo.FK_Card_SportId') IS NOT NULL AND OBJECT_ID(N'dbo.Card') IS NOT NULL  
BEGIN
	ALTER TABLE dbo.Card DROP CONSTRAINT FK_Card_SportId;
END

IF OBJECT_ID('dbo.FK_Card_YearId') IS NOT NULL AND OBJECT_ID(N'dbo.Card') IS NOT NULL  
BEGIN
	ALTER TABLE dbo.Card DROP CONSTRAINT FK_Card_YearId;
END

IF OBJECT_ID('dbo.FK_Card_SetId') IS NOT NULL AND OBJECT_ID(N'dbo.Card') IS NOT NULL  
BEGIN
	ALTER TABLE dbo.Card DROP CONSTRAINT FK_Card_SetId;
END

IF OBJECT_ID('dbo.FK_PlayerCard_CardId') IS NOT NULL AND OBJECT_ID(N'dbo.PlayerCard') IS NOT NULL  
BEGIN
	ALTER TABLE dbo.PlayerCard DROP CONSTRAINT FK_PlayerCard_CardId;
END

IF OBJECT_ID('dbo.FK__PlayerCard_PlayerId') IS NOT NULL AND OBJECT_ID(N'dbo.PlayerCard') IS NOT NULL  
BEGIN
	ALTER TABLE dbo.PlayerCard DROP CONSTRAINT FK__PlayerCard_PlayerId;
END

IF OBJECT_ID('dbo.FK_TeamCards_CardId') IS NOT NULL AND OBJECT_ID(N'dbo.TeamCard') IS NOT NULL  
BEGIN
	ALTER TABLE dbo.TeamCard DROP CONSTRAINT FK_TeamCards_CardId;
END

IF OBJECT_ID('dbo.FK_TeamCards_TeamId') IS NOT NULL AND OBJECT_ID(N'dbo.TeamCard') IS NOT NULL  
BEGIN
	ALTER TABLE dbo.TeamCard DROP CONSTRAINT FK_TeamCards_TeamId;
END


IF OBJECT_ID(N'dbo.Year') IS NOT NULL  
   DROP TABLE [dbo].[Year];

IF OBJECT_ID(N'dbo.Team') IS NOT NULL  
   DROP TABLE [dbo].[Team];

IF OBJECT_ID(N'dbo.Sport') IS NOT NULL  
   DROP TABLE [dbo].[Sport];

IF OBJECT_ID(N'dbo.Set') IS NOT NULL  
   DROP TABLE [dbo].[Set];

IF OBJECT_ID(N'dbo.Player') IS NOT NULL  
   DROP TABLE [dbo].[Player];

IF OBJECT_ID(N'dbo.Location') IS NOT NULL  
   DROP TABLE [dbo].[Location];

IF OBJECT_ID(N'dbo.[GradeCompany]') IS NOT NULL  
   DROP TABLE [dbo].[GradeCompany];

IF OBJECT_ID(N'dbo.[Card]') IS NOT NULL  
   DROP TABLE [dbo].[Card];

IF OBJECT_ID(N'dbo.[PlayerCard]') IS NOT NULL  
   DROP TABLE [dbo].[PlayerCard];

IF OBJECT_ID(N'dbo.[TeamCard]') IS NOT NULL  
   DROP TABLE [dbo].TeamCard;

IF OBJECT_ID(N'dbo.[SearchSort]') IS NOT NULL  
   DROP TABLE [dbo].SearchSort;
   

CREATE TABLE [dbo].[Year](
	[YearId] [int] IDENTITY(1,1) NOT NULL,
	[BeginningYear] [int] NOT NULL,
	[EndingYear] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[YearId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Team](
	[TeamId] [int] IDENTITY(1,1) NOT NULL,
	[City] [nvarchar](254) NOT NULL,
	[Name] [nvarchar](254) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TeamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[Sport](
	[SportId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](254) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[Set](
	[SetId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](254) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[Player](
	[PlayerId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](254) NOT NULL,
	[LastName] [nvarchar](254) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PlayerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Location](
	[LocationId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](254) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[GradeCompany](
	[GradeCompanyId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](254) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GradeCompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[Card](
	[CardId] [int] IDENTITY(1,1) NOT NULL,
	[CardDescription] [nvarchar](254) NOT NULL,
	[CardNumber] [nvarchar](254) NOT NULL,
	[LowestBeckettPrice] [decimal](18, 2) NOT NULL,
	[HighestBeckettPrice] [decimal](18, 2) NOT NULL,
	[FrontCardMainImagePath] [nvarchar](254) NOT NULL,
	[FrontCardThumbnailImagePath] [nvarchar](254) NOT NULL,
	[BackCardMainImagePath] [nvarchar](254) NOT NULL,
	[BackCardThumbnailImagePath] [nvarchar](254) NOT NULL,
	[LowestCOMCPrice] [decimal](18, 2) NOT NULL,
	[EbayPrice] [decimal](18, 2) NOT NULL,
	[PricePaid] [decimal](18, 2) NOT NULL,
	[IsGraded] [bit] NOT NULL,
	[Copies] [int] NOT NULL,
	[SerialNumber] [int] NOT NULL,
	[Grade] [decimal](18, 1) NOT NULL,
	[IsRookie] [bit] NOT NULL,
	[IsAutograph] [bit] NOT NULL,
	[IsPatch] [bit] NOT NULL,
	[IsOnCardAutograph] [bit] NOT NULL,
	[IsGameWornJersey] [bit] NOT NULL,
	[SportId] [int] NULL,
	[YearId] [int] NULL,
	[SetId] [int] NULL,
	[GradeCompanyId] [int] NULL,
	[LocationId] [int] NULL,
	[TimeStamp] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Card]  WITH CHECK ADD FOREIGN KEY([CardId])
REFERENCES [dbo].[Card] ([CardId])
GO

ALTER TABLE [dbo].[Card]  WITH CHECK ADD  CONSTRAINT [FK_Card_GradeCompanyId] FOREIGN KEY([GradeCompanyId])
REFERENCES [dbo].[GradeCompany] ([GradeCompanyId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Card] CHECK CONSTRAINT [FK_Card_GradeCompanyId]
GO

ALTER TABLE [dbo].[Card]  WITH CHECK ADD  CONSTRAINT [FK_Card_LocationId] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([LocationId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Card] CHECK CONSTRAINT [FK_Card_LocationId]
GO

ALTER TABLE [dbo].[Card]  WITH CHECK ADD  CONSTRAINT [FK_Card_SetId] FOREIGN KEY([SetId])
REFERENCES [dbo].[Set] ([SetId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Card] CHECK CONSTRAINT [FK_Card_SetId]
GO

ALTER TABLE [dbo].[Card]  WITH CHECK ADD  CONSTRAINT [FK_Card_SportId] FOREIGN KEY([SportId])
REFERENCES [dbo].[Sport] ([SportId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Card] CHECK CONSTRAINT [FK_Card_SportId]
GO

ALTER TABLE [dbo].[Card]  WITH CHECK ADD  CONSTRAINT [FK_Card_YearId] FOREIGN KEY([YearId])
REFERENCES [dbo].[Year] ([YearId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Card] CHECK CONSTRAINT [FK_Card_YearId]
GO

CREATE TABLE [dbo].[PlayerCard](
	[PlayerCardId] [int] IDENTITY(1,1) NOT NULL,
	[PlayerId] [int] NULL,
	[CardId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PlayerCardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PlayerCard]  WITH CHECK ADD  CONSTRAINT [FK_PlayerCard_CardId] FOREIGN KEY([CardId])
REFERENCES [dbo].[Card] ([CardId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PlayerCard] CHECK CONSTRAINT [FK_PlayerCard_CardId]
GO

ALTER TABLE [dbo].[PlayerCard]  WITH CHECK ADD  CONSTRAINT [FK__PlayerCard_PlayerId] FOREIGN KEY([PlayerId])
REFERENCES [dbo].[Player] ([PlayerId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PlayerCard] CHECK CONSTRAINT [FK__PlayerCard_PlayerId]
GO

CREATE TABLE [dbo].[TeamCard](
	[TeamCardId] [int] IDENTITY(1,1) NOT NULL,
	[TeamId] [int] NOT NULL,
	[CardId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TeamCardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TeamCard]  WITH CHECK ADD  CONSTRAINT [FK_TeamCards_CardId] FOREIGN KEY([CardId])
REFERENCES [dbo].[Card] ([CardId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[TeamCard] CHECK CONSTRAINT [FK_TeamCards_CardId]
GO

ALTER TABLE [dbo].[TeamCard]  WITH CHECK ADD  CONSTRAINT [FK_TeamCards_TeamId] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Team] ([TeamId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[TeamCard] CHECK CONSTRAINT [FK_TeamCards_TeamId]
GO

CREATE TABLE [dbo].[SearchSort](
	[SearchSortId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[IsGraded] [bit] NOT NULL,
	[IsRookie] [bit] NOT NULL,
	[IsAutograph] [bit] NOT NULL,
	[IsPatch] [bit] NOT NULL,
	[IsOnCardAutograph] [bit] NOT NULL,
	[IsGameWornJersey] [bit] NOT NULL,
	[PlayerIds] [nvarchar](max) NULL,
	[TeamIds] [nvarchar](max) NULL,
	[SportIds] [nvarchar](max) NULL,
	[YearIds] [nvarchar](max) NULL,
	[SetIds] [nvarchar](max) NULL,
	[GradeCompanyIds] [nvarchar](max) NULL,
	[LocationIds] [nvarchar](max) NULL,
	[CardDescription] [nvarchar](max) NULL,
	[LowestBecketPriceLow] [decimal](18, 2) NOT NULL,
	[LowestBecketPriceHigh] [decimal](18, 2) NOT NULL,
	[HighestBecketPriceLow] [decimal](18, 2) NOT NULL,
	[HighestBecketPriceHigh] [decimal](18, 2) NOT NULL,
	[LowestCOMCPriceLow] [decimal](18, 2) NOT NULL,
	[LowestCOMCPriceHigh] [decimal](18, 2) NOT NULL,
	[EbayPriceLow] [decimal](18, 2) NOT NULL,
	[EbayPriceHigh] [decimal](18, 2) NOT NULL,
	[PricePaidLow] [decimal](18, 2) NOT NULL,
	[PricePaidHigh] [decimal](18, 2) NOT NULL,
	[GradeLow] [decimal](18, 2) NOT NULL,
	[GradeHigh] [decimal](18, 2) NOT NULL,
	[CopiesLow] [int] NOT NULL,
	[CopiesHigh] [int] NOT NULL,
	[SerialNumberLow] [int] NOT NULL,
	[SerialNumberHigh] [int] NOT NULL,
	[HasImage] [bit] NOT NULL,
	[TimeStampStart] [datetime] NULL,
	[TimeStampEnd] [datetime] NULL,
	[PlayerNameSort] [int] NOT NULL,
	[TeamSort] [int] NOT NULL,
	[CardDescriptionSort] [int] NOT NULL,
	[LowestBeckettPriceSort] [int] NOT NULL,
	[HighestBeckettPriceSort] [int] NOT NULL,
	[LowestCOMCPriceSort] [int] NOT NULL,
	[EbayPriceSort] [int] NOT NULL,
	[PricePaidSort] [int] NOT NULL,
	[HasImageSort] [int] NOT NULL,
	[IsGradedSort] [int] NOT NULL,
	[CopiesSort] [int] NOT NULL,
	[SerialNumberSort] [int] NOT NULL,
	[GradeSort] [int] NOT NULL,
	[IsRookieSort] [int] NOT NULL,
	[IsAutographSort] [int] NOT NULL,
	[IsPatchSort] [int] NOT NULL,
	[IsOnCardAutographSort] [int] NOT NULL,
	[IsGameWornJerseySort] [int] NOT NULL,
	[IsSerialNumbered] [bit] NOT NULL,
	[SportSort] [int] NOT NULL,
	[YearSort] [int] NOT NULL,
	[SetSort] [int] NOT NULL,
	[GradeCompanySort] [int] NOT NULL,
	[LocationSort] [int] NOT NULL,
	[TimeStampSort] [int] NOT NULL,
	[TimeStamp] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO