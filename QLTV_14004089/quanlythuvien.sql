USE [master]
GO

/****** Object:  Database [quanlythuvien]    Script Date: 6/8/2017 6:20:15 PM ******/
CREATE DATABASE [quanlythuvien]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'quanlythuvien', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\quanlythuvien.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'quanlythuvien_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\quanlythuvien_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [quanlythuvien] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [quanlythuvien].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [quanlythuvien] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [quanlythuvien] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [quanlythuvien] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [quanlythuvien] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [quanlythuvien] SET ARITHABORT OFF 
GO

ALTER DATABASE [quanlythuvien] SET AUTO_CLOSE ON 
GO

ALTER DATABASE [quanlythuvien] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [quanlythuvien] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [quanlythuvien] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [quanlythuvien] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [quanlythuvien] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [quanlythuvien] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [quanlythuvien] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [quanlythuvien] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [quanlythuvien] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [quanlythuvien] SET  ENABLE_BROKER 
GO

ALTER DATABASE [quanlythuvien] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [quanlythuvien] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [quanlythuvien] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [quanlythuvien] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [quanlythuvien] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [quanlythuvien] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [quanlythuvien] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [quanlythuvien] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [quanlythuvien] SET  MULTI_USER 
GO

ALTER DATABASE [quanlythuvien] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [quanlythuvien] SET DB_CHAINING OFF 
GO

ALTER DATABASE [quanlythuvien] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [quanlythuvien] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [quanlythuvien] SET  READ_WRITE 
GO

