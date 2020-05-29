USE [master]
GO
CREATE DATABASE [ValuesDB]
GO
ALTER DATABASE [ValuesDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ValuesDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ValuesDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ValuesDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ValuesDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ValuesDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ValuesDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ValuesDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ValuesDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ValuesDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ValuesDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ValuesDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ValuesDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ValuesDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ValuesDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ValuesDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ValuesDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ValuesDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ValuesDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ValuesDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ValuesDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ValuesDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ValuesDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ValuesDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ValuesDB] SET RECOVERY FULL 
GO
ALTER DATABASE [ValuesDB] SET  MULTI_USER 
GO
ALTER DATABASE [ValuesDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ValuesDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ValuesDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ValuesDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ValuesDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ValuesDB', N'ON'
GO
ALTER DATABASE [ValuesDB] SET QUERY_STORE = OFF
GO
USE [ValuesDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [ValuesDB]
GO
/****** Object:  Table [dbo].[Values]    Script Date: 05-May-19 7:19:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Values](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value1] [int] NOT NULL,
	[Value2] [nvarchar](500) NULL,
 CONSTRAINT [PK_Values] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[DeleteValue]    Script Date: 05-May-19 7:19:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteValue]
	-- Add the parameters for the stored procedure here
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	delete [values]
	where Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllValues]    Script Date: 05-May-19 7:19:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllValues]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select Id, Value1, Value2
	from [Values]
END
GO
/****** Object:  StoredProcedure [dbo].[GetValueById]    Script Date: 05-May-19 7:19:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetValueById] 
	-- Add the parameters for the stored procedure here
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Id, Value1, Value2
	from [Values]
	where Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[InsertValue]    Script Date: 05-May-19 7:19:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertValue] 
	-- Add the parameters for the stored procedure here
	@Value1 int,
	@Value2 nvarchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	insert into [Values] (Value1, Value2)
	values (@Value1, @Value2)
END
GO
USE [master]
GO
ALTER DATABASE [ValuesDB] SET  READ_WRITE 
GO