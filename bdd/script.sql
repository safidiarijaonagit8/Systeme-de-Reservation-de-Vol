USE [GestionReservationContext-e908598f-5301-4fd2-99d4-ce9d3add8b05]
GO
/****** Object:  Table [dbo].[Vol]    Script Date: 31/10/2023 11:35:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vol](
	[DateHeureVol] [datetime] NOT NULL,
	[DureeVol] [int] NOT NULL,
	[LieuDepart] [varchar](50) NOT NULL,
	[LieuArrivee] [varchar](50) NOT NULL,
	[IdAvion] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Vol] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Avion]    Script Date: 31/10/2023 11:35:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Avion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Modele] [nvarchar](max) NULL,
	[NbrSiegeAffaire] [int] NOT NULL,
	[NbrSiegeEconomique] [int] NOT NULL,
 CONSTRAINT [PK_Avion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetailsPlaceVol]    Script Date: 31/10/2023 11:35:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetailsPlaceVol](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idVol] [int] NULL,
	[nbrPlaceAffaireReserver] [int] NULL,
	[nbrPlaceEconomieReserver] [int] NULL,
 CONSTRAINT [PK_DetailsPlaceVol] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[DetailsVols]    Script Date: 31/10/2023 11:35:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[DetailsVols] as
select v.Id,v.DateHeureVol,v.DureeVol,v.LieuDepart,v.LieuArrivee,
a.Modele,a.NbrSiegeAffaire,a.NbrSiegeEconomique,d.nbrPlaceAffaireReserver,d.nbrPlaceEconomieReserver
from Vol v join Avion a on v.IdAvion=a.Id full outer join DetailsPlaceVol d on v.Id = d.idVol
GO
/****** Object:  Table [dbo].[Client]    Script Date: 31/10/2023 11:35:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[identifiant] [varchar](50) NOT NULL,
	[mdp] [varchar](150) NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservation]    Script Date: 31/10/2023 11:35:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservation](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idClient] [int] NOT NULL,
	[idVol] [int] NOT NULL,
	[nbrPlaceAffaireResa] [int] NOT NULL,
	[nbrPlaceEcoResa] [int] NOT NULL,
 CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[DetailsReservations]    Script Date: 31/10/2023 11:35:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[DetailsReservations] as 
select r.id,r.idClient,c.identifiant,v.DateHeureVol,v.DureeVol,v.LieuDepart,v.LieuArrivee,a.Modele,r.nbrPlaceAffaireResa,
r.nbrPlaceEcoResa 
from Reservation r join Client c on r.idClient = c.id join Vol v on r.idVol = v.Id join Avion a 
on v.IdAvion = a.Id
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 31/10/2023 11:35:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Administrateur]    Script Date: 31/10/2023 11:35:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Administrateur](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[identifiant] [varchar](50) NOT NULL,
	[mdp] [varchar](150) NOT NULL,
 CONSTRAINT [PK_Administrateur] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DetailsPlaceVol]  WITH CHECK ADD  CONSTRAINT [FK_DetailsPlaceVol_Vol] FOREIGN KEY([idVol])
REFERENCES [dbo].[Vol] ([Id])
GO
ALTER TABLE [dbo].[DetailsPlaceVol] CHECK CONSTRAINT [FK_DetailsPlaceVol_Vol]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Client] FOREIGN KEY([idClient])
REFERENCES [dbo].[Client] ([id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Client]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Vol] FOREIGN KEY([idVol])
REFERENCES [dbo].[Vol] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Vol]
GO
ALTER TABLE [dbo].[Vol]  WITH CHECK ADD  CONSTRAINT [IdAvion] FOREIGN KEY([IdAvion])
REFERENCES [dbo].[Avion] ([Id])
GO
ALTER TABLE [dbo].[Vol] CHECK CONSTRAINT [IdAvion]
GO
