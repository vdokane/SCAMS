USE [SCAMS]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Subsection]') AND type in (N'U'))
BEGIN
        DROP TABLE [dbo].Subsection
END


CREATE TABLE [Subsection] (
	SubsectionID INT IDENTITY(1,1) NOT NULL,
	SubsectionName varchar(50) NOT NULL,
	Code varchar(3) NOT NULL,
	FLAIRName varchar(50) NOT NULL,
	CONSTRAINT [PK_SubsectionID] PRIMARY KEY CLUSTERED 
	(
		SubsectionID ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
