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

IF OBJECT_ID('dbo.FK_SearchSortPlayer_SearchSortId') IS NOT NULL AND OBJECT_ID(N'dbo.SearchSortPlayer') IS NOT NULL  
BEGIN
	ALTER TABLE dbo.SearchSortPlayer DROP CONSTRAINT FK_SearchSortPlayer_SearchSortId;
END

IF OBJECT_ID('dbo.FK_SearchSortPlayer_PlayerId') IS NOT NULL AND OBJECT_ID(N'dbo.SearchSortPlayer') IS NOT NULL  
BEGIN
	ALTER TABLE dbo.SearchSortPlayer DROP CONSTRAINT FK_SearchSortPlayer_PlayerId;
END

IF OBJECT_ID('dbo.FK_SearchSortTeam_SearchSortId') IS NOT NULL AND OBJECT_ID(N'dbo.SearchSortTeam') IS NOT NULL  
BEGIN
	ALTER TABLE dbo.SearchSortTeam DROP CONSTRAINT FK_SearchSortTeam_SearchSortId;
END

IF OBJECT_ID('dbo.FK_SearchSortTeam_TeamId') IS NOT NULL AND OBJECT_ID(N'dbo.SearchSortTeam') IS NOT NULL  
BEGIN
	ALTER TABLE dbo.SearchSortTeam DROP CONSTRAINT FK_SearchSortTeam_TeamId;
END

IF OBJECT_ID('dbo.FK_SearchSortSport_SearchSortId') IS NOT NULL AND OBJECT_ID(N'dbo.SearchSortSport') IS NOT NULL  
BEGIN
	ALTER TABLE dbo.SearchSortSport DROP CONSTRAINT FK_SearchSortSport_SearchSortId;
END

IF OBJECT_ID('dbo.FK_SearchSortSport_SportId') IS NOT NULL AND OBJECT_ID(N'dbo.SearchSortSport') IS NOT NULL  
BEGIN
	ALTER TABLE dbo.SearchSortSport DROP CONSTRAINT FK_SearchSortSport_SportId;
END

IF OBJECT_ID('dbo.FK_SearchSortYear_SearchSortId') IS NOT NULL AND OBJECT_ID(N'dbo.SearchSortYear') IS NOT NULL  
BEGIN
	ALTER TABLE dbo.SearchSortYear DROP CONSTRAINT FK_SearchSortYear_SearchSortId;
END

IF OBJECT_ID('dbo.FK_SearchSortYear_YearId') IS NOT NULL AND OBJECT_ID(N'dbo.SearchSortYear') IS NOT NULL  
BEGIN
	ALTER TABLE dbo.SearchSortYear DROP CONSTRAINT FK_SearchSortYear_YearId;
END

IF OBJECT_ID('dbo.FK_SearchSortSet_SearchSortId') IS NOT NULL AND OBJECT_ID(N'dbo.SearchSortSet') IS NOT NULL  
BEGIN
	ALTER TABLE dbo.SearchSortSet DROP CONSTRAINT FK_SearchSortSet_SearchSortId;
END

IF OBJECT_ID('dbo.FK_SearchSortSet_SetId') IS NOT NULL AND OBJECT_ID(N'dbo.SearchSortSet') IS NOT NULL  
BEGIN
	ALTER TABLE dbo.SearchSortSet DROP CONSTRAINT FK_SearchSortSet_SetId;
END

IF OBJECT_ID('dbo.FK_SearchSortGradeCompany_SearchSortId') IS NOT NULL AND OBJECT_ID(N'dbo.SearchSortGradeCompany') IS NOT NULL  
BEGIN
	ALTER TABLE dbo.SearchSortGradeCompany DROP CONSTRAINT FK_SearchSortGradeCompany_SearchSortId;
END

IF OBJECT_ID('dbo.FK_SearchSortGradeCompany_GradeCompanyId') IS NOT NULL AND OBJECT_ID(N'dbo.SearchSortGradeCompany') IS NOT NULL  
BEGIN
	ALTER TABLE dbo.SearchSortGradeCompany DROP CONSTRAINT FK_SearchSortGradeCompany_GradeCompanyId;
END

IF OBJECT_ID('dbo.FK_SearchSortLocation_SearchSortId') IS NOT NULL AND OBJECT_ID(N'dbo.SearchSortLocation') IS NOT NULL  
BEGIN
	ALTER TABLE dbo.SearchSortLocation DROP CONSTRAINT FK_SearchSortLocation_SearchSortId;
END

IF OBJECT_ID('dbo.FK_SearchSortLocation_LocationId') IS NOT NULL AND OBJECT_ID(N'dbo.SearchSortLocation') IS NOT NULL  
BEGIN
	ALTER TABLE dbo.SearchSortLocation DROP CONSTRAINT FK_SearchSortLocation_LocationId;
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
   
IF OBJECT_ID(N'dbo.[SearchSortPlayer]') IS NOT NULL  
   DROP TABLE [dbo].SearchSortPlayer;

IF OBJECT_ID(N'dbo.[SearchSortTeam]') IS NOT NULL  
   DROP TABLE [dbo].SearchSortTeam;

IF OBJECT_ID(N'dbo.[SearchSortSport]') IS NOT NULL  
   DROP TABLE [dbo].SearchSortSport;

IF OBJECT_ID(N'dbo.[SearchSortYear]') IS NOT NULL  
   DROP TABLE [dbo].SearchSortYear;

IF OBJECT_ID(N'dbo.[SearchSortSet]') IS NOT NULL  
   DROP TABLE [dbo].SearchSortSet;

IF OBJECT_ID(N'dbo.[SearchSortGradeCompany]') IS NOT NULL  
   DROP TABLE [dbo].SearchSortGradeCompany;

IF OBJECT_ID(N'dbo.[SearchSortLocation]') IS NOT NULL  
   DROP TABLE [dbo].SearchSortLocation;
   
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
	[CardDescription] [nvarchar](max) NULL,
	[LowestBeckettPriceLow] [decimal](18, 2) NOT NULL,
	[LowestBeckettPriceHigh] [decimal](18, 2) NOT NULL,
	[HighestBeckettPriceLow] [decimal](18, 2) NOT NULL,
	[HighestBeckettPriceHigh] [decimal](18, 2) NOT NULL,
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
	[TimeStamp] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SearchSortId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SearchSortPlayer](
	[SearchSortPlayerId] [int] IDENTITY(1,1) NOT NULL,
	[SearchSortId] [int] NOT NULL,
	[PlayerId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SearchSortPlayerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SearchSortPlayer]  WITH CHECK ADD  CONSTRAINT [FK_SearchSortPlayer_SearchSortId] FOREIGN KEY([SearchSortId])
REFERENCES [dbo].[SearchSort]([SearchSortId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SearchSortPlayer] CHECK CONSTRAINT [FK_SearchSortPlayer_SearchSortId]
GO

ALTER TABLE [dbo].[SearchSortPlayer]  WITH CHECK ADD  CONSTRAINT [FK_SearchSortPlayer_PlayerId] FOREIGN KEY(PlayerId)
REFERENCES [dbo].Player (PlayerId)
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SearchSortPlayer] CHECK CONSTRAINT [FK_SearchSortPlayer_PlayerId]
GO

CREATE TABLE [dbo].[SearchSortTeam](
	[SearchSortTeamId] [int] IDENTITY(1,1) NOT NULL,
	[SearchSortId] [int] NOT NULL,
	[TeamId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SearchSortTeamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SearchSortTeam]  WITH CHECK ADD  CONSTRAINT [FK_SearchSortTeam_SearchSortId] FOREIGN KEY([SearchSortId])
REFERENCES [dbo].[SearchSort]([SearchSortId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SearchSortTeam] CHECK CONSTRAINT [FK_SearchSortTeam_SearchSortId]
GO

ALTER TABLE [dbo].[SearchSortTeam]  WITH CHECK ADD  CONSTRAINT [FK_SearchSortTeam_TeamId] FOREIGN KEY(TeamId)
REFERENCES [dbo].Team (TeamId)
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SearchSortTeam] CHECK CONSTRAINT [FK_SearchSortTeam_TeamId]
GO

CREATE TABLE [dbo].SearchSortSport(
	[SearchSortSportId] [int] IDENTITY(1,1) NOT NULL,
	[SearchSortId] [int] NOT NULL,
	[SportId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SearchSortSportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].SearchSortSport  WITH CHECK ADD  CONSTRAINT [FK_SearchSortSport_SearchSortId] FOREIGN KEY([SearchSortId])
REFERENCES [dbo].SearchSort([SearchSortId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].SearchSortSport CHECK CONSTRAINT [FK_SearchSortSport_SearchSortId]
GO

ALTER TABLE [dbo].SearchSortSport  WITH CHECK ADD  CONSTRAINT [FK_SearchSortSport_SportId] FOREIGN KEY(SportId)
REFERENCES [dbo].Sport (SportId)
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].SearchSortSport CHECK CONSTRAINT [FK_SearchSortSport_SportId]
GO

CREATE TABLE [dbo].SearchSortYear(
	[SearchSortYearId] [int] IDENTITY(1,1) NOT NULL,
	[SearchSortId] [int] NOT NULL,
	[YearId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SearchSortYearId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].SearchSortYear  WITH CHECK ADD  CONSTRAINT [FK_SearchSortYear_SearchSortId] FOREIGN KEY([SearchSortId])
REFERENCES [dbo].SearchSort([SearchSortId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].SearchSortYear CHECK CONSTRAINT [FK_SearchSortYear_SearchSortId]
GO

ALTER TABLE [dbo].SearchSortYear  WITH CHECK ADD  CONSTRAINT [FK_SearchSortYear_YearId] FOREIGN KEY(YearId)
REFERENCES [dbo].Year (YearId)
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].SearchSortYear CHECK CONSTRAINT [FK_SearchSortYear_YearId]
GO

CREATE TABLE [dbo].SearchSortSet(
	[SearchSortSetId] [int] IDENTITY(1,1) NOT NULL,
	[SearchSortId] [int] NOT NULL,
	[SetId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SearchSortSetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].SearchSortSet  WITH CHECK ADD  CONSTRAINT [FK_SearchSortSet_SearchSortId] FOREIGN KEY([SearchSortId])
REFERENCES [dbo].SearchSort([SearchSortId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].SearchSortSet CHECK CONSTRAINT [FK_SearchSortSet_SearchSortId]
GO

ALTER TABLE [dbo].SearchSortSet  WITH CHECK ADD  CONSTRAINT [FK_SearchSortSet_SetId] FOREIGN KEY(SetId)
REFERENCES [dbo].[Set] (SetId)
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].SearchSortSet CHECK CONSTRAINT [FK_SearchSortSet_SetId]
GO


CREATE TABLE [dbo].SearchSortGradeCompany(
	[SearchSortGradeCompanyId] [int] IDENTITY(1,1) NOT NULL,
	[SearchSortId] [int] NOT NULL,
	[GradeCompanyId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SearchSortGradeCompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].SearchSortGradeCompany  WITH CHECK ADD  CONSTRAINT [FK_SearchSortGradeCompany_SearchSortId] FOREIGN KEY([SearchSortId])
REFERENCES [dbo].SearchSort([SearchSortId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].SearchSortGradeCompany CHECK CONSTRAINT [FK_SearchSortGradeCompany_SearchSortId]
GO

ALTER TABLE [dbo].SearchSortGradeCompany  WITH CHECK ADD  CONSTRAINT [FK_SearchSortGradeCompany_GradeCompanyId] FOREIGN KEY(GradeCompanyId)
REFERENCES [dbo].[GradeCompany] (GradeCompanyId)
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].SearchSortGradeCompany CHECK CONSTRAINT [FK_SearchSortGradeCompany_GradeCompanyId]
GO

CREATE TABLE [dbo].SearchSortLocation(
	[SearchSortLocationId] [int] IDENTITY(1,1) NOT NULL,
	[SearchSortId] [int] NOT NULL,
	[LocationId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SearchSortLocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].SearchSortLocation  WITH CHECK ADD  CONSTRAINT [FK_SearchSortLocation_SearchSortId] FOREIGN KEY([SearchSortId])
REFERENCES [dbo].SearchSort([SearchSortId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].SearchSortLocation CHECK CONSTRAINT [FK_SearchSortLocation_SearchSortId]
GO

ALTER TABLE [dbo].SearchSortLocation  WITH CHECK ADD  CONSTRAINT [FK_SearchSortLocation_LocationId] FOREIGN KEY(LocationId)
REFERENCES [dbo].[Location] (LocationId)
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].SearchSortLocation CHECK CONSTRAINT [FK_SearchSortLocation_LocationId]
GO