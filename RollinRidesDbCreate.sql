CREATE TABLE Advertisements(Id int identity(1,1) primary key,
Link varchar(5000), DisplayObjectUrl Varchar(1000), CompanyName varchar(500),
Location int default 1)

CREATE TABLE Automobile(Id int identity(1,1) primary key,
Make Varchar(100), Model Varchar(100), [Year] int,
Street1 Varchar(500), Street2 Varchar(500), 
City Varchar(500), [State] varchar(100), ZipCode  Varchar(50),
IsUsed int default 1, IsApproved int default 0,
PhoneNumber varchar(50), UserId int,
IsHighlight int default 0, CarfaxReportPath varchar(500),
Title Varchar(200), [Description] Varchar(2000),
ContactName Varchar(100), HasFinancing int default 0,
MinimumDownpayment decimal(18,2), Price decimal(18,2),
Color varchar(100), foreign key(UserId) references [User](Id))

CREATE TABLE [Image](Id int identity(1,1),
Url varchar(1000), IsMainImage int default 0,
[Type] int, AutomobileId int, foreign key(AutomobileId) references Automobile(Id))

Create Table Settings(CompanyLogoUrl varchar(2000), CompanyPhoneNumber Varchar(200),
CompanyFax Varchar(200), AboutUsDescription Varchar(MAX),
[Address] varchar(MAX), CouponOfTheMonthUrl varchar(2000),
HomePageMovieUrl Varchar(MAX), SiteMasterEmail Varchar(2000),
TermsAndConditions Varchar(MAX))

CREATE TABLE [User](Id int identity(1,1) primary key,
LastName Varchar(200), FirstName Varchar(200), CompanyName Varchar(500),
Username varchar(50) Not Null, [Password] Varchar(2000) Not Null, PhoneNumber Varchar(100),
Email varcahr(250) Not Null, Street1 Varchar(500), Street2 Varchar(500),
City Varchar(500), [State] varchar(100), ZipCode varchar(100), AccountType int not null,
DateJoined DateTime not null, Expires DateTime) 