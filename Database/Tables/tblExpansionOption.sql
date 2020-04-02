USE [SCAMS]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExpansionOption]') AND type in (N'U'))
BEGIN
        DROP TABLE [dbo].ExpansionOption
END


CREATE TABLE [ExpansionOption] (
	ExpansionOptionID INT IDENTITY(1,1) NOT NULL,
	EO varchar(2) NOT NULL,
	CONSTRAINT [PK_ExpansionOptionID] PRIMARY KEY CLUSTERED 
	(
		ExpansionOptionID ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExpansionOption]') AND type in (N'U'))
	AND EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EOStaging]') AND type in (N'U'))
BEGIN
        INSERT INTO ExpansionOption 
		SELECT EO FROM EOStaging
END

--select * from ExpansionOption
