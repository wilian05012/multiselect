CREATE TABLE [dbo].[xtblHurricanCounties]
(
	[HurricaneId] INT NOT NULL,
	[CountyId] INT NOT NULL,
	CONSTRAINT PK_xtblHurricaneCounties PRIMARY KEY(HurricaneId, CountyId),
	CONSTRAINT FK_xtblHurricaneCounties_tblHurricanes FOREIGN KEY(HurricaneId) REFERENCES tblHurricanes(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT FK_xtblHurricaneCounties_tblCounties FOREIGN KEY(CountyId) REFERENCES lktCounties(Id) ON UPDATE CASCADE ON DELETE CASCADE
)
