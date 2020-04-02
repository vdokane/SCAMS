USE [SCAMS]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Agency]') AND type in (N'U'))
BEGIN
        DROP TABLE [dbo].Agency
END


CREATE TABLE [Agency] (
	AgencyID INT IDENTITY(1,1) NOT NULL,
	AgencyName varchar(150) NOT NULL,
	OLO varchar(6) NOT NULL,
	--Code varchar(2) NOT NULL, --//Isn't this redundent?
	CONSTRAINT [PK_AgencyID] PRIMARY KEY CLUSTERED 
	(
		AgencyID ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Agency]') AND type in (N'U'))
BEGIN

	INSERT INTO [Agency] VALUES ('Legislature','110000')
	INSERT INTO [Agency] VALUES ('Justice Administrative Commission','210000')
	INSERT INTO [Agency] VALUES ('State Courts System','220000')
	INSERT INTO [Agency] VALUES ('Executive Office of the Governor','310000')
	INSERT INTO [Agency] VALUES ('Department of the Lottery','360000')
	INSERT INTO [Agency] VALUES ('Department of Environmental Protection','370000')
	INSERT INTO [Agency] VALUES ('Department of Economic Opportunity','400000')
	INSERT INTO [Agency] VALUES ('Office of the Attorney General (Department of Legal Affairs)','410000')
	INSERT INTO [Agency] VALUES ('Department of Agriculture and Consumer Services','420000')
	INSERT INTO [Agency] VALUES ('Department of Financial Services','430000')
	INSERT INTO [Agency] VALUES ('Division of Accounting and Auditing','439000')
	INSERT INTO [Agency] VALUES ('Department of State','450000')
	INSERT INTO [Agency] VALUES ('Education System','480000')
	INSERT INTO [Agency] VALUES ('Agency for Persons with Disabilities','489000')
	INSERT INTO [Agency] VALUES ('Department of Veterans'' Affairs','500000')
	INSERT INTO [Agency] VALUES ('Department of Community Affairs','520000')
	INSERT INTO [Agency] VALUES ('Department of Transportation','550000')
	INSERT INTO [Agency] VALUES ('Department of Citrus','570000')

	/*
	INSERT INTO [Agency] VALUES ('Department of Business and Professional Regulation','xx')
	INSERT INTO [Agency] VALUES ('Agency for Health Care Administration','xx')
	INSERT INTO [Agency] VALUES ('Fish and Wildlife Conservation Commission','xx')
	INSERT INTO [Agency] VALUES ('Department of Corrections','xx')
	INSERT INTO [Agency] VALUES ('Department of Revenue','xx')
	INSERT INTO [Agency] VALUES ('Department of Juvenile Justice','xx')
	INSERT INTO [Agency] VALUES ('Department of Law Enforcement','xx')
	INSERT INTO [Agency] VALUES ('Department of Military Affairs','xx')
	INSERT INTO [Agency] VALUES ('Department of Management Services','xx')
	INSERT INTO [Agency] VALUES ('State Board of Administration of Florida','xx')
	INSERT INTO [Agency] VALUES ('Florida Commission on Offender Review','xx')
	INSERT INTO [Agency] VALUES ('Department of Children and Families','xx')
	INSERT INTO [Agency] VALUES ('Prison Rehabilitative Industries and Diversified Enterprises, Inc.','xx')
	INSERT INTO [Agency] VALUES ('Department of Elder Affairs','xx') --coimmunity?
	INSERT INTO [Agency] VALUES ('Department of Highway Safety and Motor Vehicles','xx')
	INSERT INTO [Agency] VALUES ('Department of Health','xx')
	INSERT INTO [Agency] VALUES ('Public Service Commission','xx')
	*/
END
--select * from agency