USE [master]
GO
/****** Object:  Database [EMetro]    Script Date: 7/9/2021 10:15:46 AM ******/
CREATE DATABASE [EMetro]
GO
USE [EMetro]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 7/9/2021 10:15:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](200) NULL,
	[websiteAddress] [nvarchar](200) NULL,
	[addressCompany] [nvarchar](100) NULL,
	[phone] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Line]    Script Date: 7/9/2021 10:15:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Line](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idCompany] [int] NOT NULL,
	[lineName] [nvarchar](100) NULL,
	[lineType] [nvarchar](50) NULL,
	[lineStartId] [int] NOT NULL,
	[lineEndId] [int] NOT NULL,
	[stops] [nvarchar](100) NULL,
	[openTime] [nvarchar](50) NULL,
	[closeTime] [nvarchar](50) NULL,
	[awTime] [nvarchar](50) NULL,
	[statusLine] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Station]    Script Date: 7/9/2021 10:15:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Station](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[stationName] [nvarchar](max) NULL,
	[statusStatus] [nvarchar](100) NULL,
	[locationDescription] [nvarchar](max) NULL,
	[map] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 7/9/2021 10:15:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ticket](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tType] [nvarchar](50) NULL,
	[idRoute] [int] NOT NULL,
	[registerDate] [date] NULL,
	[expiryDate] [date] NULL,
	[price] [int] NULL,
	[isUsed] [bit] NULL,
	[isSell] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 7/9/2021 10:15:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userName] [nvarchar](100) NULL,
	[userPass] [nvarchar](max) NULL,
	[idCompany] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Company] ON 

INSERT [dbo].[Company] ([id], [name], [websiteAddress], [addressCompany], [phone]) VALUES (1, N'HoangLongk', N'long.com', N'141 Nguyễn Thái Bình, Quận 1, TP HCM', N'0572165544')
INSERT [dbo].[Company] ([id], [name], [websiteAddress], [addressCompany], [phone]) VALUES (2, N'Nhân coop', N'nhancp.com', N'159 Phạm Ngũ Lão, Quận 1, TP HCM', N'0578169242')
INSERT [dbo].[Company] ([id], [name], [websiteAddress], [addressCompany], [phone]) VALUES (3, N'The Express Metro', N'express.metro.com', N'685 Âu Cơ, Quận Tân Phú, TP HCM', N'0539188251')
INSERT [dbo].[Company] ([id], [name], [websiteAddress], [addressCompany], [phone]) VALUES (4, N'NLines', N'nlines.com', N'01 Song Hành, Quận 2, TP HCM', N'0571929373')
INSERT [dbo].[Company] ([id], [name], [websiteAddress], [addressCompany], [phone]) VALUES (5, N'Giang Nguyễn coop', N'giangyn.com', N'86 Cao Thắng, Quận 3, TP HCM', N'0571929982')
INSERT [dbo].[Company] ([id], [name], [websiteAddress], [addressCompany], [phone]) VALUES (6, N'Hồ Ngọc Ánh', N'hhng.com', N'43 Hoa Hồng, Quận Phú Nhuận, TP HCM', N'0538841573')
SET IDENTITY_INSERT [dbo].[Company] OFF
GO
SET IDENTITY_INSERT [dbo].[Line] ON 

INSERT [dbo].[Line] ([id], [idCompany], [lineName], [lineType], [lineStartId], [lineEndId], [stops], [openTime], [closeTime], [awTime], [statusLine]) VALUES (1, 1, N'Tuyến 14', N'Regular', 9, 6, N'a', N'09h:10m', N'10h:00m', N'00h:10m', N'Active')
INSERT [dbo].[Line] ([id], [idCompany], [lineName], [lineType], [lineStartId], [lineEndId], [stops], [openTime], [closeTime], [awTime], [statusLine]) VALUES (2, 2, N'Tuyến 18', N'Express', 7, 6, N'a', N'09h:10m', N'10h:00m', N'00h:10m', N'Active')
INSERT [dbo].[Line] ([id], [idCompany], [lineName], [lineType], [lineStartId], [lineEndId], [stops], [openTime], [closeTime], [awTime], [statusLine]) VALUES (3, 3, N'Tuyến 9', N'Express', 3, 2, N'a', N'09h:10m', N'10h:00m', N'00h:10m', N'Active')
INSERT [dbo].[Line] ([id], [idCompany], [lineName], [lineType], [lineStartId], [lineEndId], [stops], [openTime], [closeTime], [awTime], [statusLine]) VALUES (4, 4, N'Tuyến 25', N'Regular', 1, 8, N'a', N'09h:10m', N'10h:00m', N'00h:10m', N'Active')
INSERT [dbo].[Line] ([id], [idCompany], [lineName], [lineType], [lineStartId], [lineEndId], [stops], [openTime], [closeTime], [awTime], [statusLine]) VALUES (5, 5, N'Tuyến 55', N'Regular', 5, 4, N'a', N'09h:10m', N'10h:00m', N'00h:10m', N'Active')
INSERT [dbo].[Line] ([id], [idCompany], [lineName], [lineType], [lineStartId], [lineEndId], [stops], [openTime], [closeTime], [awTime], [statusLine]) VALUES (6, 1, N'Tuyến 13', N'Regular', 1, 4, N'a', N'09h:10m', N'10h:00m', N'00h:10m', N'Active')
INSERT [dbo].[Line] ([id], [idCompany], [lineName], [lineType], [lineStartId], [lineEndId], [stops], [openTime], [closeTime], [awTime], [statusLine]) VALUES (7, 5, N'Tuyến 28', N'Regular', 3, 2, N'a', N'09h:10m', N'10h:00m', N'00h:10m', N'Active')
INSERT [dbo].[Line] ([id], [idCompany], [lineName], [lineType], [lineStartId], [lineEndId], [stops], [openTime], [closeTime], [awTime], [statusLine]) VALUES (8, 3, N'Tuyến 90', N'Regular', 7, 6, N'a', N'09h:10m', N'10h:00m', N'00h:10m', N'Active')
INSERT [dbo].[Line] ([id], [idCompany], [lineName], [lineType], [lineStartId], [lineEndId], [stops], [openTime], [closeTime], [awTime], [statusLine]) VALUES (9, 2, N'Tuyến 10', N'Regular', 2, 3, N'a', N'09h:10m', N'10h:00m', N'00h:10m', N'Inactive')
INSERT [dbo].[Line] ([id], [idCompany], [lineName], [lineType], [lineStartId], [lineEndId], [stops], [openTime], [closeTime], [awTime], [statusLine]) VALUES (10, 2, N'Tuyến 33', N'Regular', 4, 3, N'a', N'09h:10m', N'10h:00m', N'00h:10m', N'Active')
INSERT [dbo].[Line] ([id], [idCompany], [lineName], [lineType], [lineStartId], [lineEndId], [stops], [openTime], [closeTime], [awTime], [statusLine]) VALUES (11, 4, N'Tuyến 40', N'Regular', 1, 8, N'a', N'09h:10m', N'10h:00m', N'00h:10m', N'Active')
INSERT [dbo].[Line] ([id], [idCompany], [lineName], [lineType], [lineStartId], [lineEndId], [stops], [openTime], [closeTime], [awTime], [statusLine]) VALUES (12, 2, N'Tuyến 26', N'Regular', 9, 4, N'a', N'09h:10m', N'10h:00m', N'00h:10m', N'Active')
INSERT [dbo].[Line] ([id], [idCompany], [lineName], [lineType], [lineStartId], [lineEndId], [stops], [openTime], [closeTime], [awTime], [statusLine]) VALUES (13, 1, N'Tuyến 17', N'Regular', 8, 7, N'a', N'09h:10m', N'10h:00m', N'00h:10m', N'Active')
INSERT [dbo].[Line] ([id], [idCompany], [lineName], [lineType], [lineStartId], [lineEndId], [stops], [openTime], [closeTime], [awTime], [statusLine]) VALUES (14, 3, N'Tuyến 12', N'Regular', 6, 2, N'a', N'09h:10m', N'10h:00m', N'00h:10m', N'Active')
INSERT [dbo].[Line] ([id], [idCompany], [lineName], [lineType], [lineStartId], [lineEndId], [stops], [openTime], [closeTime], [awTime], [statusLine]) VALUES (15, 2, N'Tuyến 66', N'Express', 1, 8, NULL, N'06h:00m', N'23h:10m', N'00h:10m', N'Active')
INSERT [dbo].[Line] ([id], [idCompany], [lineName], [lineType], [lineStartId], [lineEndId], [stops], [openTime], [closeTime], [awTime], [statusLine]) VALUES (16, 6, N'Tuyến 99', N'Regular', 1, 7, NULL, N'07h:00m', N'22h:30m', N'00h:40m', N'Active')
INSERT [dbo].[Line] ([id], [idCompany], [lineName], [lineType], [lineStartId], [lineEndId], [stops], [openTime], [closeTime], [awTime], [statusLine]) VALUES (17, 6, N'Tuyến 21', N'Express', 7, 2, NULL, N'05h:30m', N'22h:45m', N'00h:30m', N'Inactive')
SET IDENTITY_INSERT [dbo].[Line] OFF
GO
SET IDENTITY_INSERT [dbo].[Station] ON 

INSERT [dbo].[Station] ([id], [stationName], [statusStatus], [locationDescription], [map]) VALUES (1, N'Trạm Dương Vương', N'Normal', N'395 Dương Vương, Bình Tân, Thành Phố Hồ Chí Minh', NULL)
INSERT [dbo].[Station] ([id], [stationName], [statusStatus], [locationDescription], [map]) VALUES (2, N'Trạm Miền Đông', N'Normal', N'39448 Xa lộ Hà Nội, Bình An, Quận 9, Thành phố Hồ Chí Minh', NULL)
INSERT [dbo].[Station] ([id], [stationName], [statusStatus], [locationDescription], [map]) VALUES (3, N'Trạm Đinh Bộ Lĩnh', N'Normal', N'Phường 26, Bình Thạnh, Thành phố Hồ Chí Minh', NULL)
INSERT [dbo].[Station] ([id], [stationName], [statusStatus], [locationDescription], [map]) VALUES (4, N'Trạm An Sương', N'Under Repair', N'QL22, Bà Điểm, Hóc Môn, Thành phố Hồ Chí Minh', NULL)
INSERT [dbo].[Station] ([id], [stationName], [statusStatus], [locationDescription], [map]) VALUES (5, N'Trạm Tân Phú', N'Decommissioned', N'34 Trường Chinh, Phường 14, Tân Phú, Thành phố Hồ Chí Minh', NULL)
INSERT [dbo].[Station] ([id], [stationName], [statusStatus], [locationDescription], [map]) VALUES (6, N'Trạm Quận 1', N'Normal', N'1 Phạm Ngũ Lão, Phường Bến Thành, Quận 1, Thành phố Hồ Chí', NULL)
INSERT [dbo].[Station] ([id], [stationName], [statusStatus], [locationDescription], [map]) VALUES (7, N'Trạm Suối Tiên', N'Normal', N'120 Xa lộ Hà Nội, Phường Tân Phú, Quận 9, Thành phố Hồ Chí Minh', NULL)
INSERT [dbo].[Station] ([id], [stationName], [statusStatus], [locationDescription], [map]) VALUES (8, N'Trạm Bến Thành', N'Normal', N'Suối Tiên, Phường Tân Phú, Quận 9, Thành phố Hồ Chí Minh', NULL)
INSERT [dbo].[Station] ([id], [stationName], [statusStatus], [locationDescription], [map]) VALUES (9, N'Trạm Đông Hòa', N'Under Repair', N'Đông Hòa, Dĩ An, Bình Dương', N'ne')
SET IDENTITY_INSERT [dbo].[Station] OFF
GO
SET IDENTITY_INSERT [dbo].[Ticket] ON 

INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (1, N'Regular', 1, CAST(N'2021-07-08' AS Date), NULL, 15000, 0, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (2, N'Regular', 1, NULL, NULL, 15000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (3, N'Regular', 1, NULL, NULL, 15000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (4, N'Regular', 1, NULL, NULL, 15000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (5, N'Regular', 1, NULL, NULL, 15000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (6, N'Monthly', 1, CAST(N'2021-07-08' AS Date), CAST(N'2021-08-07' AS Date), 20000, 1, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (7, N'Monthly', 1, CAST(N'2021-07-08' AS Date), CAST(N'2021-08-07' AS Date), 20000, 1, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (8, N'Monthly', 1, NULL, NULL, 20000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (9, N'Regular', 6, NULL, NULL, 13000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (10, N'Regular', 6, NULL, NULL, 13000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (11, N'Regular', 6, CAST(N'2021-07-08' AS Date), CAST(N'2021-07-08' AS Date), 13000, 1, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (12, N'Regular', 6, CAST(N'2021-07-08' AS Date), NULL, 13000, 0, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (13, N'Regular', 6, CAST(N'2021-07-08' AS Date), NULL, 13000, 0, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (14, N'Monthly', 6, CAST(N'2021-07-08' AS Date), CAST(N'2021-08-07' AS Date), 20000, 1, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (15, N'Monthly', 6, NULL, NULL, 20000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (16, N'Monthly', 6, NULL, NULL, 20000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (17, N'Regular', 13, CAST(N'2021-07-08' AS Date), NULL, 20000, 0, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (18, N'Monthly', 13, NULL, NULL, 18000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (19, N'Regular', 2, CAST(N'2021-07-08' AS Date), NULL, 30000, 0, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (20, N'Regular', 2, CAST(N'2021-07-08' AS Date), CAST(N'2021-07-08' AS Date), 30000, 1, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (21, N'Regular', 2, CAST(N'2021-07-08' AS Date), CAST(N'2021-07-08' AS Date), 30000, 1, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (22, N'Regular', 2, NULL, NULL, 30000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (23, N'Regular', 2, CAST(N'2021-07-08' AS Date), NULL, 30000, 0, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (24, N'Monthly', 2, CAST(N'2021-07-08' AS Date), CAST(N'2021-08-07' AS Date), 25000, 1, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (25, N'Monthly', 2, CAST(N'2021-07-08' AS Date), CAST(N'2021-08-07' AS Date), 25000, 1, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (26, N'Monthly', 2, CAST(N'2021-07-08' AS Date), CAST(N'2021-08-07' AS Date), 25000, 1, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (27, N'Monthly', 2, CAST(N'2021-07-08' AS Date), CAST(N'2021-08-07' AS Date), 25000, 1, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (28, N'Monthly', 2, NULL, NULL, 25000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (29, N'Regular', 3, NULL, NULL, 14000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (30, N'Regular', 3, NULL, NULL, 14000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (31, N'Regular', 3, NULL, NULL, 14000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (32, N'Regular', 3, NULL, NULL, 14000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (33, N'Regular', 11, NULL, NULL, 14000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (34, N'Regular', 11, NULL, NULL, 14000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (35, N'Regular', 11, NULL, NULL, 14000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (36, N'Regular', 11, NULL, NULL, 14000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (37, N'Regular', 11, NULL, NULL, 14000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (38, N'Regular', 5, NULL, NULL, 23000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (39, N'Regular', 5, NULL, NULL, 23000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (40, N'Regular', 5, CAST(N'2021-07-08' AS Date), CAST(N'2021-07-08' AS Date), 23000, 1, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (41, N'Regular', 5, NULL, NULL, 23000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (42, N'Regular', 5, CAST(N'2021-07-08' AS Date), NULL, 23000, 0, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (43, N'Regular', 5, NULL, NULL, 23000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (44, N'Monthly', 7, CAST(N'2021-07-08' AS Date), CAST(N'2021-08-07' AS Date), 18000, 1, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (45, N'Monthly', 7, CAST(N'2021-07-08' AS Date), CAST(N'2021-08-07' AS Date), 18000, 1, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (46, N'Monthly', 7, CAST(N'2021-07-08' AS Date), CAST(N'2021-08-07' AS Date), 18000, 1, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (47, N'Monthly', 7, NULL, NULL, 18000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (48, N'Monthly', 7, NULL, NULL, 18000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (49, N'Regular', 9, NULL, NULL, 18000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (50, N'Regular', 9, NULL, NULL, 18000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (51, N'Regular', 9, NULL, NULL, 18000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (52, N'Regular', 9, CAST(N'2021-07-08' AS Date), NULL, 13000, 0, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (53, N'Monthly', 12, CAST(N'2021-07-08' AS Date), CAST(N'2021-08-07' AS Date), 15000, 1, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (54, N'Monthly', 12, CAST(N'2021-07-08' AS Date), CAST(N'2021-08-07' AS Date), 15000, 1, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (55, N'Monthly', 12, NULL, NULL, 15000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (56, N'Regular', 10, NULL, NULL, 28000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (57, N'Regular', 10, CAST(N'2021-07-08' AS Date), NULL, 28000, 0, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (58, N'Regular', 2, CAST(N'2021-07-09' AS Date), NULL, 21000, 0, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (59, N'Regular', 2, CAST(N'2021-07-08' AS Date), NULL, 21000, 0, 1)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (60, N'Regular', 2, NULL, NULL, 21000, 0, 0)
INSERT [dbo].[Ticket] ([id], [tType], [idRoute], [registerDate], [expiryDate], [price], [isUsed], [isSell]) VALUES (61, N'Regular', 2, NULL, NULL, 21000, 0, 0)
SET IDENTITY_INSERT [dbo].[Ticket] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([id], [userName], [userPass], [idCompany]) VALUES (1, N'admin', N'db69fc039dcbd2962cb4d28f5891aae1', NULL)
INSERT [dbo].[Users] ([id], [userName], [userPass], [idCompany]) VALUES (2, N'user1', N'c3f38a9e7efe580f7b4ca6fec20ee2dd', 1)
INSERT [dbo].[Users] ([id], [userName], [userPass], [idCompany]) VALUES (3, N'user2', N'80e4ee2345dc141ce9d1ad65501a2fe4', 2)
INSERT [dbo].[Users] ([id], [userName], [userPass], [idCompany]) VALUES (4, N'user3', N'f6d0b31439f27bd83d3ff9da57e1dd96', 3)
INSERT [dbo].[Users] ([id], [userName], [userPass], [idCompany]) VALUES (5, N'user4', N'5b770739c874b9788e91f25789bed6e3', 4)
INSERT [dbo].[Users] ([id], [userName], [userPass], [idCompany]) VALUES (6, N'user5', N'1207db452370926c60941608d6196dab', 5)
INSERT [dbo].[Users] ([id], [userName], [userPass], [idCompany]) VALUES (7, N'user6', N'3335e8c4360687515824fd2c60fd6aca', 6)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Line]  WITH CHECK ADD FOREIGN KEY([idCompany])
REFERENCES [dbo].[Company] ([id])
GO
ALTER TABLE [dbo].[Line]  WITH CHECK ADD FOREIGN KEY([lineEndId])
REFERENCES [dbo].[Station] ([id])
GO
ALTER TABLE [dbo].[Line]  WITH CHECK ADD FOREIGN KEY([lineStartId])
REFERENCES [dbo].[Station] ([id])
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD FOREIGN KEY([idRoute])
REFERENCES [dbo].[Line] ([id])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([idCompany])
REFERENCES [dbo].[Company] ([id])
GO
USE [master]
GO
ALTER DATABASE [EMetro] SET  READ_WRITE 
GO
