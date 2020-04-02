USE [SCAMS]
GO

SET ANSI_PADDING ON
GO


IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrganizationCode]') AND type in (N'U'))
BEGIN
        DROP TABLE [dbo].OrganizationCode
END


CREATE TABLE [OrganizationCode] (
	OrganizationCodeID INT IDENTITY(1,1) NOT NULL,
	AgencyID INT NOT NULL,
	DivisionID INT NOT NULL,
	BureauID INT NOT NULL,
	SectionID INT NOT NULL,
	SubsectionID INT NOT NULL,
	CONSTRAINT [PK_OrganizationCodeID] PRIMARY KEY CLUSTERED 
	(
		OrganizationCodeID ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



SET ANSI_PADDING OFF
GO


IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrganizationCode]') AND type in (N'U'))
BEGIN


	ALTER TABLE [dbo].OrganizationCode  WITH CHECK ADD  CONSTRAINT [FK_OrgCode_Agency] FOREIGN KEY(UserID)
	REFERENCES [dbo].Agency (AgencyID)
	ALTER TABLE [dbo].OrganizationCode CHECK CONSTRAINT [FK_OrgCode_Agency]

	ALTER TABLE [dbo].OrganizationCode  WITH CHECK ADD  CONSTRAINT [FK_OrgCode_Division] FOREIGN KEY(DivisionID)
	REFERENCES [dbo].Division (DivisionID)
	ALTER TABLE [dbo].OrganizationCode CHECK CONSTRAINT [FK_OrgCode_Division]

	ALTER TABLE [dbo].OrganizationCode  WITH CHECK ADD  CONSTRAINT [FK_OrgCode_Bureau] FOREIGN KEY(BureauID)
	REFERENCES [dbo].Bureau (BureauID)
	ALTER TABLE [dbo].OrganizationCode CHECK CONSTRAINT [FK_OrgCode_Bureau]

	ALTER TABLE [dbo].OrganizationCode  WITH CHECK ADD  CONSTRAINT [FK_OrgCode_Bureau] FOREIGN KEY(BureauID)
	REFERENCES [dbo].Bureau (BureauID)
	ALTER TABLE [dbo].OrganizationCode CHECK CONSTRAINT [FK_OrgCode_Bureau]

	ALTER TABLE [dbo].OrganizationCode  WITH CHECK ADD  CONSTRAINT [FK_OrgCode_Section] FOREIGN KEY(SectionID)
	REFERENCES [dbo].Section (SectionID)
	ALTER TABLE [dbo].OrganizationCode CHECK CONSTRAINT [FK_OrgCode_Section]

	ALTER TABLE [dbo].OrganizationCode  WITH CHECK ADD  CONSTRAINT [FK_OrgCode_SubSection] FOREIGN KEY(SubsectionID)
	REFERENCES [dbo].Subsection (SubsectionID)
	ALTER TABLE [dbo].OrganizationCode CHECK CONSTRAINT [FK_OrgCode_SubSection]

END
GO
