USE master
GO

CREATE DATABASE SnackShopDB
 ON  PRIMARY 
( NAME = N'SnackShopDB', FILENAME = N'H:\DEV\My DataBase\CNW_FastFood\SnackShopDB.mdf' , SIZE = 100 , MAXSIZE = 2GB, FILEGROWTH = 10 )
 LOG ON 
( NAME = N'SnackShopDB_log', FILENAME = N'H:\DEV\My DataBase\CNW_FastFood\SnackShopDB_log.ldf' , SIZE = 200 , MAXSIZE = UNLIMITED , FILEGROWTH = 20)
GO

USE [SnackShopDB]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 6/16/2020 5:08:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[id_bill] [int] IDENTITY(1,1) NOT NULL,
	[subtotal] [decimal](12, 0) NULL,
	[total] [decimal](12, 0) NULL,
	[creatDate] [datetime] NULL,
	[id_customer] [int] NULL,
	[discountCode] [varchar](20) NULL,
	[discount] [decimal](12, 0) NULL,
	[status] [tinyint] NOT NULL,
 CONSTRAINT [PK_Bill] PRIMARY KEY CLUSTERED 
(
	[id_bill] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillDetail]    Script Date: 6/16/2020 5:08:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillDetail](
	[id_bill] [int] NOT NULL,
	[id_product] [int] NOT NULL,
	[price] [decimal](10, 0) NULL,
	[amount] [int] NULL,
	[intoMoney] [decimal](12, 0) NULL,
	[discriptionProductDetail] [nvarchar](max) NULL,
 CONSTRAINT [PK_BillDetail] PRIMARY KEY CLUSTERED 
(
	[id_bill] ASC,
	[id_product] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 6/16/2020 5:08:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[id_cart] [int] IDENTITY(1,1) NOT NULL,
	[subtotal] [decimal](12, 0) NULL,
	[total] [decimal](12, 0) NULL,
	[id_discountCode] [varchar](20) NULL,
	[id_customer] [int] NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[id_cart] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartDetail]    Script Date: 6/16/2020 5:08:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartDetail](
	[id_customer] [int] NOT NULL,
	[id_product] [int] NOT NULL,
	[price] [decimal](10, 0) NULL,
	[amount] [int] NULL,
	[intoMoney] [decimal](12, 0) NULL,
	[discriptionProductDetail] [nvarchar](max) NULL,
 CONSTRAINT [PK_CartDetail_1] PRIMARY KEY CLUSTERED 
(
	[id_customer] ASC,
	[id_product] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 6/16/2020 5:08:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[id_category] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](200) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[id_category] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Credential]    Script Date: 6/16/2020 5:08:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Credential](
	[id_userGroup] [varchar](20) NOT NULL,
	[id_role] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Credential] PRIMARY KEY CLUSTERED 
(
	[id_userGroup] ASC,
	[id_role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 6/16/2020 5:08:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[id_customer] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[phone] [char](10) NULL,
	[address] [nvarchar](200) NULL,
	[userName] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[subtotalCart] [decimal](12, 0) NULL,
	[totalCart] [decimal](12, 0) NULL,
	[id_discountCode] [varchar](20) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[id_customer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DiscountCode]    Script Date: 6/16/2020 5:08:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiscountCode](
	[id_discountCode] [varchar](20) NOT NULL,
	[discount] [decimal](12, 0) NULL,
 CONSTRAINT [PK_DiscountCode] PRIMARY KEY CLUSTERED 
(
	[id_discountCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 6/16/2020 5:08:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[id_product] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](200) NULL,
	[description] [nvarchar](max) NULL,
	[information] [nvarchar](max) NULL,
	[review] [nvarchar](max) NULL,
	[availability] [bit] NOT NULL,
	[price] [decimal](10, 0) NULL,
	[salePercent] [int] NULL,
	[salePrice] [decimal](10, 0) NULL,
	[rate] [float] NULL,
	[mainPhoto] [varchar](100) NULL,
	[photo1] [varchar](100) NULL,
	[photo2] [varchar](100) NULL,
	[photo3] [varchar](100) NULL,
	[photo4] [varchar](100) NULL,
	[updated] [date] NULL,
	[id_category] [int] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[id_product] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductDetail]    Script Date: 6/16/2020 5:08:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductDetail](
	[id_productDetail] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](200) NULL,
	[amount] [int] NULL,
	[availability] [bit] NOT NULL,
	[extraPrice] [decimal](10, 0) NULL,
	[id_product] [int] NULL,
 CONSTRAINT [PK_ProductDetail] PRIMARY KEY CLUSTERED 
(
	[id_productDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 6/16/2020 5:08:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[id_role] [varchar](50) NOT NULL,
	[name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[id_role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 6/16/2020 5:08:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id_user] [int] IDENTITY(1,1) NOT NULL,
	[userName] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[name] [nvarchar](50) NULL,
	[email] [varchar](50) NULL,
	[status] [bit] NULL,
	[createDate] [datetime] NULL,
	[id_userGroup] [varchar](20) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 6/16/2020 5:08:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroup](
	[id_userGroup] [varchar](20) NOT NULL,
	[name] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserGroup] PRIMARY KEY CLUSTERED 
(
	[id_userGroup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([id_user], [userName], [password], [name], [email], [status], [createDate], [id_userGroup]) VALUES (1, N'hieumta', N'hieu', N'Vũ Minh Hiếu', N'vuhieupro1999@gmail.com', NULL, CAST(N'2020-06-16T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[User] ([id_user], [userName], [password], [name], [email], [status], [createDate], [id_userGroup]) VALUES (3, N'hungmta', N'hung', N'Nguyễn Đức Hưng', N'nguyenduchung99hd@gmail.com', NULL, CAST(N'2020-06-16T00:00:00.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[Bill] ADD  CONSTRAINT [DF_Bill_subtotal]  DEFAULT ((0)) FOR [subtotal]
GO
ALTER TABLE [dbo].[Bill] ADD  CONSTRAINT [DF_Bill_total]  DEFAULT ((0)) FOR [total]
GO
ALTER TABLE [dbo].[Bill] ADD  CONSTRAINT [DF_Bill_discount]  DEFAULT ((0)) FOR [discount]
GO
ALTER TABLE [dbo].[Bill] ADD  CONSTRAINT [DF_Bill_status]  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[BillDetail] ADD  CONSTRAINT [DF_BillDetail_price]  DEFAULT ((0)) FOR [price]
GO
ALTER TABLE [dbo].[BillDetail] ADD  CONSTRAINT [DF_BillDetail_amount]  DEFAULT ((0)) FOR [amount]
GO
ALTER TABLE [dbo].[BillDetail] ADD  CONSTRAINT [DF_BillDetail_intoMoney]  DEFAULT ((0)) FOR [intoMoney]
GO
ALTER TABLE [dbo].[Cart] ADD  CONSTRAINT [DF_Cart_subtotal]  DEFAULT ((0)) FOR [subtotal]
GO
ALTER TABLE [dbo].[Cart] ADD  CONSTRAINT [DF_Cart_total]  DEFAULT ((0)) FOR [total]
GO
ALTER TABLE [dbo].[CartDetail] ADD  CONSTRAINT [DF_CartDetail_price]  DEFAULT ((0)) FOR [price]
GO
ALTER TABLE [dbo].[CartDetail] ADD  CONSTRAINT [DF_CartDetail_amount]  DEFAULT ((0)) FOR [amount]
GO
ALTER TABLE [dbo].[CartDetail] ADD  CONSTRAINT [DF_CartDetail_intoMoney]  DEFAULT ((0)) FOR [intoMoney]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_subtotalCart]  DEFAULT ((0)) FOR [subtotalCart]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_total]  DEFAULT ((0)) FOR [totalCart]
GO
ALTER TABLE [dbo].[DiscountCode] ADD  CONSTRAINT [DF_DiscountCode_discount]  DEFAULT ((0)) FOR [discount]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_availability]  DEFAULT ((1)) FOR [availability]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_price]  DEFAULT ((0)) FOR [price]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_salePercent]  DEFAULT ((0)) FOR [salePercent]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_salePrice]  DEFAULT ((0)) FOR [salePrice]
GO
ALTER TABLE [dbo].[ProductDetail] ADD  CONSTRAINT [DF_ProductDetail_amount]  DEFAULT ((0)) FOR [amount]
GO
ALTER TABLE [dbo].[ProductDetail] ADD  CONSTRAINT [DF_ProductDetail_availability]  DEFAULT ((1)) FOR [availability]
GO
ALTER TABLE [dbo].[ProductDetail] ADD  CONSTRAINT [DF_ProductDetail_extraPrice]  DEFAULT ((0)) FOR [extraPrice]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK_Bill_Customer] FOREIGN KEY([id_customer])
REFERENCES [dbo].[Customer] ([id_customer])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK_Bill_Customer]
GO
ALTER TABLE [dbo].[BillDetail]  WITH CHECK ADD  CONSTRAINT [FK_BillDetail_Bill] FOREIGN KEY([id_bill])
REFERENCES [dbo].[Bill] ([id_bill])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BillDetail] CHECK CONSTRAINT [FK_BillDetail_Bill]
GO
ALTER TABLE [dbo].[BillDetail]  WITH CHECK ADD  CONSTRAINT [FK_BillDetail_Product] FOREIGN KEY([id_product])
REFERENCES [dbo].[Product] ([id_product])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BillDetail] CHECK CONSTRAINT [FK_BillDetail_Product]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Customer] FOREIGN KEY([id_customer])
REFERENCES [dbo].[Customer] ([id_customer])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_Customer]
GO
ALTER TABLE [dbo].[CartDetail]  WITH CHECK ADD  CONSTRAINT [FK_CartDetail_Cart] FOREIGN KEY([id_customer])
REFERENCES [dbo].[Cart] ([id_cart])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartDetail] CHECK CONSTRAINT [FK_CartDetail_Cart]
GO
ALTER TABLE [dbo].[CartDetail]  WITH CHECK ADD  CONSTRAINT [FK_CartDetail_Product] FOREIGN KEY([id_product])
REFERENCES [dbo].[Product] ([id_product])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartDetail] CHECK CONSTRAINT [FK_CartDetail_Product]
GO
ALTER TABLE [dbo].[Credential]  WITH CHECK ADD  CONSTRAINT [FK_Credential_Role] FOREIGN KEY([id_role])
REFERENCES [dbo].[Role] ([id_role])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Credential] CHECK CONSTRAINT [FK_Credential_Role]
GO
ALTER TABLE [dbo].[Credential]  WITH CHECK ADD  CONSTRAINT [FK_Credential_UserGroup] FOREIGN KEY([id_userGroup])
REFERENCES [dbo].[UserGroup] ([id_userGroup])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Credential] CHECK CONSTRAINT [FK_Credential_UserGroup]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_DiscountCode] FOREIGN KEY([id_discountCode])
REFERENCES [dbo].[DiscountCode] ([id_discountCode])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_DiscountCode]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([id_category])
REFERENCES [dbo].[Category] ([id_category])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[ProductDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProductDetail_Product] FOREIGN KEY([id_product])
REFERENCES [dbo].[Product] ([id_product])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductDetail] CHECK CONSTRAINT [FK_ProductDetail_Product]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserGroup] FOREIGN KEY([id_userGroup])
REFERENCES [dbo].[UserGroup] ([id_userGroup])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserGroup]
GO
