USE [NESS]
GO
/****** Object:  Table [dbo].[tbPacientes]    Script Date: 11/04/2020 12:23:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbPacientes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_tbPaciente] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbDatasDisponiveis]    Script Date: 11/04/2020 12:23:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbDatasDisponiveis](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Data] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_tbDatasDisponiveis] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbConsultas]    Script Date: 11/04/2020 12:23:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbConsultas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdPaciente] [int] NOT NULL,
	[IdData] [int] NOT NULL,
 CONSTRAINT [PK_tbConsulta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwConsultas]    Script Date: 11/04/2020 12:23:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwConsultas]
AS
SELECT        C.Id, C.IdPaciente, C.IdData, P.Nome, D.Data
FROM            dbo.tbPacientes AS P INNER JOIN
                         dbo.tbConsultas AS C ON P.Id = C.IdPaciente INNER JOIN
                         dbo.tbDatasDisponiveis AS D ON D.Id = C.IdData
GO
/****** Object:  View [dbo].[vwDisponiveis]    Script Date: 11/04/2020 12:23:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwDisponiveis]
AS
SELECT        D.Id, D.Data
FROM            dbo.tbDatasDisponiveis AS D LEFT OUTER JOIN
                         dbo.tbConsultas AS C ON D.Id = C.IdData
WHERE        (C.IdData IS NULL)
GO
ALTER TABLE [dbo].[tbConsultas]  WITH CHECK ADD  CONSTRAINT [FK_tbConsulta_tbDatasDisponiveis] FOREIGN KEY([IdData])
REFERENCES [dbo].[tbDatasDisponiveis] ([Id])
GO
ALTER TABLE [dbo].[tbConsultas] CHECK CONSTRAINT [FK_tbConsulta_tbDatasDisponiveis]
GO
ALTER TABLE [dbo].[tbConsultas]  WITH CHECK ADD  CONSTRAINT [FK_tbConsulta_tbPaciente] FOREIGN KEY([IdPaciente])
REFERENCES [dbo].[tbPacientes] ([Id])
GO
ALTER TABLE [dbo].[tbConsultas] CHECK CONSTRAINT [FK_tbConsulta_tbPaciente]
GO
/****** Object:  StoredProcedure [dbo].[prDelConsulta]    Script Date: 11/04/2020 12:23:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Autor:		Julierme A. C. Santos
-- Data de Criação: 10/04/2020
-- Descrição:	Deleta uma consulta
-- =============================================

CREATE PROCEDURE [dbo].[prDelConsulta]	
	@id int
AS
BEGIN	
	SET NOCOUNT ON;
   
	DELETE FROM tbConsultas WHERE Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[prDelData]    Script Date: 11/04/2020 12:23:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		Julierme A. C. Santos
-- Data de Criação: 10/04/2020
-- Descrição:	Deleta uma data disponível
-- =============================================

CREATE PROCEDURE [dbo].[prDelData]	
	@id int
AS
BEGIN	
	SET NOCOUNT ON;
   
	DELETE FROM tbDatasDisponiveis WHERE Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[prDelPaciente]    Script Date: 11/04/2020 12:23:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Julierme A. C. Santos
-- Data de Criação: 10/04/2020
-- Descrição:	Deleta um paciente
-- =============================================

CREATE PROCEDURE [dbo].[prDelPaciente]	
	@id int
AS
BEGIN	
	SET NOCOUNT ON;
   
	DELETE FROM tbPacientes WHERE Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[prInsConsulta]    Script Date: 11/04/2020 12:23:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Autor:		Julierme A. C. Santos
-- Data de Criação: 10/04/2020
-- Descrição:	Insere uma consulta
-- =============================================

CREATE PROCEDURE [dbo].[prInsConsulta]
	@idPaciente int,
	@idData int
AS
BEGIN	
	SET NOCOUNT ON;
   
	INSERT INTO tbConsultas ( IdData, IdPaciente ) VALUES ( @idData, @idPaciente ) SELECT SCOPE_IDENTITY() AS Id;
END
GO
/****** Object:  StoredProcedure [dbo].[prInsData]    Script Date: 11/04/2020 12:23:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Autor:		Julierme A. C. Santos
-- Data de Criação: 10/04/2020
-- Descrição:	Insere uma data disponível
-- =============================================

CREATE PROCEDURE [dbo].[prInsData]
	@data smalldatetime
AS
BEGIN	
	SET NOCOUNT ON;
   
	INSERT INTO tbDatasDisponiveis ( [Data] ) VALUES ( @data ) SELECT SCOPE_IDENTITY() AS Id;
END
GO
/****** Object:  StoredProcedure [dbo].[prInsPaciente]    Script Date: 11/04/2020 12:23:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Autor:		Julierme A. C. Santos
-- Data de Criação: 10/04/2020
-- Descrição:	Insere um paciente
-- =============================================

CREATE PROCEDURE [dbo].[prInsPaciente]
	@nome nvarchar(100)
AS
BEGIN	
	SET NOCOUNT ON;
   
	INSERT INTO tbPacientes ( Nome ) VALUES ( @nome ) SELECT SCOPE_IDENTITY() AS Id;
END
GO
/****** Object:  StoredProcedure [dbo].[prSelConsulta]    Script Date: 11/04/2020 12:23:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Autor:		Julierme A. C. Santos
-- Data de Criação: 10/04/2020
-- Descrição:	Consulta as consulta
-- =============================================

CREATE PROCEDURE [dbo].[prSelConsulta]	
	@id int = 0
AS
BEGIN	
	SET NOCOUNT ON;

	IF @id = 0   
		SELECT * FROM vwConsultas
	ELSE
		SELECT * FROM vwConsultas WHERE Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[prSelData]    Script Date: 11/04/2020 12:23:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Autor:		Julierme A. C. Santos
-- Data de Criação: 10/04/2020
-- Descrição:	Consulta as datas disponíveis
-- =============================================

CREATE PROCEDURE [dbo].[prSelData]	
	@id int = 0
AS
BEGIN	
	SET NOCOUNT ON;

	IF @id = 0   
		SELECT * FROM tbDatasDisponiveis
	ELSE
		SELECT * FROM tbDatasDisponiveis WHERE Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[prSelDisponiveis]    Script Date: 11/04/2020 12:23:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Autor:		Julierme A. C. Santos
-- Data de Criação: 10/04/2020
-- Descrição:	Consulta as datas sem uso
-- =============================================

CREATE PROCEDURE [dbo].[prSelDisponiveis]	
AS
BEGIN	
	SET NOCOUNT ON;

	SELECT * FROM vwDisponiveis
END
GO
/****** Object:  StoredProcedure [dbo].[prSelPaciente]    Script Date: 11/04/2020 12:23:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		Julierme A. C. Santos
-- Data de Criação: 10/04/2020
-- Descrição:	Consulta os pacientes
-- =============================================

CREATE PROCEDURE [dbo].[prSelPaciente]	
	@id int = 0
AS
BEGIN	
	SET NOCOUNT ON;

	IF @id = 0   
		SELECT * FROM tbPacientes
	ELSE
		SELECT * FROM tbPacientes WHERE Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[prUpdConsulta]    Script Date: 11/04/2020 12:23:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Autor:		Julierme A. C. Santos
-- Data de Criação: 10/04/2020
-- Descrição:	Atualiza uma consulta
-- =============================================

CREATE PROCEDURE [dbo].[prUpdConsulta]	
	@id int,
	@idPaciente int,
	@idData int
AS
BEGIN	
	SET NOCOUNT ON;
   
	UPDATE tbConsultas
	SET IdData = @idData, 
		IdPaciente = @idPaciente
	WHERE Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[prUpdData]    Script Date: 11/04/2020 12:23:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Autor:		Julierme A. C. Santos
-- Data de Criação: 10/04/2020
-- Descrição:	Atualiza uma data disponível
-- =============================================

CREATE PROCEDURE [dbo].[prUpdData]	
	@id int,
	@data datetime
AS
BEGIN	
	SET NOCOUNT ON;
   
	UPDATE tbDatasDisponiveis
	SET [Data] = @data	
	WHERE Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[prUpdPaciente]    Script Date: 11/04/2020 12:23:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		Julierme A. C. Santos
-- Data de Criação: 10/04/2020
-- Descrição:	Atualiza um paciente
-- =============================================

CREATE PROCEDURE [dbo].[prUpdPaciente]	
	@id int,
	@nome nvarchar(100)
AS
BEGIN	
	SET NOCOUNT ON;
   
	UPDATE tbPacientes
	SET Nome = @nome	
	WHERE Id = @id
END
GO