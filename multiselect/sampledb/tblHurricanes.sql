CREATE TABLE [dbo].[tblHurricanes]
(
	[Id] INT NOT NULL,
	[Name] VARCHAR(50) NOT NULL,
	[LandfallDate] DATE NOT NULL,
	[SaffirSimpsonCat] INT NOT NULL,
	CONSTRAINT PK_tblHurricanes PRIMARY KEY(Id)
)

GO
