--Questions for Julia:
--can you have an XXXXXX category?
--Can you have an empty category?
--Can you have a category with the -XX suffix? 

USE [SCAMS]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Category]') AND type in (N'U'))
BEGIN
        DROP TABLE [dbo].Category
END


CREATE TABLE [Category] (
	CategoryID INT IDENTITY(1,1) NOT NULL,
	CategoryCode varchar(6) NOT NULL,
	Title varchar(150) NOT NULL,
	CONSTRAINT [PK_CategoryID] PRIMARY KEY CLUSTERED 
	(
		CategoryID ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Category]') AND type in (N'U'))
BEGIN
	INSERT INTO [Category] VALUES ('001000','State Grants')
	INSERT INTO [Category] VALUES ('001010','State Grants - No Service Charge')
	INSERT INTO [Category] VALUES ('001100','Other Grants')
	INSERT INTO [Category] VALUES ('001101','Donations/Contributions Given to the State')
	INSERT INTO [Category] VALUES ('001110','Other Grants - No Service Charge')
	INSERT INTO [Category] VALUES ('001111','Deepwater Horizon')
	INSERT INTO [Category] VALUES ('001200','Fines, Forefeitures, Judgements, and Penal')
	INSERT INTO [Category] VALUES ('001201','Overweight Penalties')
	INSERT INTO [Category] VALUES ('001202','Penalties')
	INSERT INTO [Category] VALUES ('001203','Sale of COnfiscated/Forfeited Property')
	
	/*
	INSERT INTO [Category] VALUES ('xxxx','yyyy')
	INSERT INTO [Category] VALUES ('xxxx','yyyy')
		INSERT INTO [Category] VALUES ('xxxx','yyyy')
	INSERT INTO [Category] VALUES ('xxxx','yyyy')
	INSERT INTO [Category] VALUES ('xxxx','yyyy')
	*/

	
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CategoryStaging]') AND type in (N'U'))
	BEGIN
		INSERT INTO [Category] 
		SELECT cs.[CATEGORY],'' from CategoryStaging cs where len(cs.[CATEGORY]) = 6 and not exists(select 1 from [Category] where CategoryCode = cs.[CATEGORY])

	END



END

--select * from Category order by categoryCode