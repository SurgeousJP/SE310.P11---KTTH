USE [master]
GO
/****** Object:  Database [BookCatalog]    Script Date: 11/2/2024 10:51:37 AM ******/
CREATE DATABASE [BookCatalog]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BookCatalog', FILENAME = N'C:\Users\tuanb\BookCatalog.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BookCatalog_log', FILENAME = N'C:\Users\tuanb\BookCatalog_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BookCatalog] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BookCatalog].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BookCatalog] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BookCatalog] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BookCatalog] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BookCatalog] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BookCatalog] SET ARITHABORT OFF 
GO
ALTER DATABASE [BookCatalog] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BookCatalog] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BookCatalog] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BookCatalog] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BookCatalog] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BookCatalog] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BookCatalog] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BookCatalog] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BookCatalog] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BookCatalog] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BookCatalog] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BookCatalog] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BookCatalog] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BookCatalog] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BookCatalog] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BookCatalog] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BookCatalog] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BookCatalog] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BookCatalog] SET  MULTI_USER 
GO
ALTER DATABASE [BookCatalog] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BookCatalog] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BookCatalog] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BookCatalog] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BookCatalog] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BookCatalog] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BookCatalog] SET QUERY_STORE = OFF
GO
USE [BookCatalog]
GO
/****** Object:  Table [dbo].[books]    Script Date: 11/2/2024 10:51:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[books](
	[id] [int] NOT NULL,
	[language_code] [nvarchar](50) NULL,
	[average_rating] [tinyint] NULL,
	[description] [nvarchar](2850) NULL,
	[num_pages] [smallint] NULL,
	[publication_day] [tinyint] NULL,
	[publication_month] [tinyint] NULL,
	[publication_year] [smallint] NULL,
	[isbn13] [bigint] NULL,
	[url] [nvarchar](100) NULL,
	[image_url] [nvarchar](100) NULL,
	[ratings_count] [int] NULL,
	[title] [nvarchar](200) NULL,
	[title_without_series] [nvarchar](200) NULL,
	[price] [float] NULL,
	[availability] [smallint] NULL,
	[dimensions] [nvarchar](50) NULL,
	[discount_percentage] [float] NULL,
	[item_weight] [float] NULL,
	[format_id] [tinyint] NULL,
	[publisher_id] [tinyint] NULL,
	[author_name] [nvarchar](50) NULL,
	[book_id] [tinyint] NULL,
	[genre_id] [tinyint] NOT NULL,
 CONSTRAINT [PK_books] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[genres]    Script Date: 11/2/2024 10:51:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[genres](
	[id] [tinyint] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_genres] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Genres] FOREIGN KEY([genre_id])
REFERENCES [dbo].[genres] ([id])
GO
ALTER TABLE [dbo].[books] CHECK CONSTRAINT [FK_Books_Genres]
GO
USE [master]
GO
ALTER DATABASE [BookCatalog] SET  READ_WRITE 
GO

ALTER TABLE dbo.books
   ADD id INT IDENTITY
       CONSTRAINT PK_books PRIMARY KEY CLUSTERED

ALTER TABLE dbo.books
DROP COLUMN id

SET IDENTITY_INSERT books ON;
