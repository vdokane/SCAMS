--Make sure to get the object codes
/****** Script for SelectTopNRows command from SSMS  ******/
SELECT 'INSERT INTO [FullOrganizationCodesStaging] VALUES( ''' + [L1] + ''',''' +  [L2]  + ''',''' + [L3]  + ''',''' + [L4] + ''',''' + [L5]  + ''',''' + isnull([ShortTitle],'')  + ''',''' + isnull([LongTitle],'')  
+ ''',''' + isnull([StatusCode],'')  + ''','''  + isnull([Note],'') + ''')'
  FROM [TRACKER].[dbo].[OrganizationCodes]


select * from [OrgStaging]

--select * from FLAICodesStaging
--select distinct EO from FLAICodesStaging
select * from FLAICodesStaging order by flairaccountcode
 --86

select FLAIRAccountCode, substring(FLAIRAccountCode,28,2) from FLAICodesStaging where substring(FLAIRAccountCode,28,2) != '00'




 where eo='CR' order by Category --one to one relation, all same flair code
select * from FLAICodesStaging where eo='2d' order by Category --category has one tht has the weird dash in it. It has a different flair code

select distinct Category from FLAICodesStaging where eo='A6' order by Category --040000, 001500, 001800

select  distinct EO from FLAICodesStaging where Category= '040000' --17
select  distinct EO from FLAICodesStaging where Category= '001500' --4
select  distinct EO from FLAICodesStaging where Category= '001800' --17
select  distinct EO from FLAICodesStaging where Category= '220030' --3

/*
Title file

Expansion files
	Expansion set = SI and Object CODe pk
		Set indicator - 01, AA dependent association 1A
		object code
		GL code
		category (000125) dependent on OBJECT - set by leg or governors
		year
	Expansion option 
		org code & the EO PK for Expansion option
			EO has versions

Expansion set file has 1:* relation to expansion option file - no, all one entity.


	


Expansion set



object code, you use set indicator to get the category
set indi

A6
R1
4E
MI
SS
AW
SG
5E
D1
6E
GJ
1S
CE
CO
3E
3V
SB
PS
FR
CR
HS
3H
2S
CW
4G
RI
SE
MM
D3
AF
3G
JQ
AT
R6
3C
CT
AR
RV
CC
5D
16
ZZ
D6
AC
  
A2
5C
SM
8D
IP
SC
7E
F3
AM
CK
FG
HR
GP
4D
GD
CP
9E
CA
LP
VC
CD
F2
R5
GQ
9D
JA
3K
40
1B
2C
15
CI
AB
F1
2E
1Q
R2
CV
8E
SF
