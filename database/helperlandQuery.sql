--for database creation
CREATE DATABASE helperland;
use helperland;
--for customer table
CREATE TABLE Customer(
	Custid int NOT NULL PRIMARY KEY IDENTITY(1,1),
	FirstName nvarchar(10) NOT NULL,
	LastName nvarchar(10) NOT NULL,
	Email nvarchar(15) NOT NULL Unique ,
	Mobile nvarchar(10)	NOT NULL,
	Password nvarchar(10) NOT NULL,
	Dob	Date Null,
	Language nvarchar(10) Null,
	RoleId	int	NOT NULL
);

--customer address
CREATE TABLE CustAdress(
AdressId int NOT NULL PRIMARY KEY IDENTITY(1,1),
Adress	nvarchar(50) NOT NULL,	
CustId int,
City nvarchar(10) NOT NULL,	
PostalCode int NOT NULL,
Mobile nvarchar(10)	NOT NULL,	
);

--for service provider
CREATE TABLE ServiceProvider(
SpId int NOT NULL PRIMARY KEY IDENTITY(1,1),
FirstName nvarchar(10) NOT NULL,
LastName nvarchar(10) NOT NULL,
Email nvarchar(15) NOT NULL	UNIQUE,
Mobile nvarchar(10)	NOT NULL,
Password nvarchar(10) NOT NULL,	
Dob Date,
Nationality	nvarchar(10),
Gender nvarchar(10)	NOT NULL,
Avtar Image	NOT NULL,
Adress nvarchar(50)	NOT NULL,
PostalCode int NOT NULL,
City nvarchar(10) NOT NULL,
RoleId int NOT NULL,	
);	

--for service booking
CREATE TABLE BookService(
ServiceId int NOT NULL PRIMARY KEY	IDENTITY(1,1),
CustId	int,	
SpId int,		
PostalCode	int	NOT NULL,	
ServiceDate	date NOT NULL,	
ServiceTime	time NOT NULL,	
StayHour	int	NOT NULL,	
InsideCabinets Bit,		
InsideFridge Bit,	
InsideOven	Bit,	
Laundry	Bit,		
Windows	Bit,	
Comments nvarchar(50),
PetAtHome Bit,	
Adress	nvarchar(50) NOT NULL,	
Payment	int	NOT NULL,	
Status	nvarchar(5)	NOT NULL,	
);

--for favourite service provider
CREATE TABLE Favourite_SP(
FavId	int	NOT NULL PRIMARY KEY IDENTITY(1,1),
CustId	int,
SpId	int	
);

--for service provider rating
CREATE TABLE SpRating(
RatingId int	PRIMARY KEY NOT NULL  IDENTITY(1,1),
CustId	int,
SpId	int,	
OnTimeRating int,		
FriendlyRating	int,		
QualityRating	int,		
Feeback	nvarchar(50),		
);

--for block customer
CREATE TABLE BlockCust(
BlockId	int	PRIMARY KEY NOT NULL  IDENTITY(1,1),
CustId	int,
SpId	int,	
);

--for role identity
CREATE TABLE RoleDetail(
RoleId	int	PRIMARY KEY NOT NULL  IDENTITY(1,1),
Role	nvarchar(5)	NOT NULL,
);

--for admin detail
CREATE TABLE AdminDetail(
AdminId		int	PRIMARY KEY		NOT NULL  IDENTITY(1,1),
Username	nvarchar(10) Unique		NOT NULL ,
Password	nvarchar(10) NOT NULL, 
);

--add foreign key for all table

ALTER TABLE Customer
ADD CONSTRAINT FK_roleid
FOREIGN KEY (RoleId) REFERENCES RoleDetail(RoleId);

ALTER TABLE CustAdress
ADD CONSTRAINT FK_custadress
FOREIGN KEY (CustId) REFERENCES Customer(CustId);

ALTER TABLE ServiceProvider
ADD CONSTRAINT FK_role
FOREIGN KEY (RoleId) REFERENCES RoleDetail(RoleId);

ALTER TABLE BookService 
ADD CONSTRAINT FK_custid
FOREIGN KEY (CustId) REFERENCES Customer(CustId);  

ALTER TABLE BookService
ADD CONSTRAINT FK_spid
FOREIGN KEY (SpId) REFERENCES ServiceProvider(SpId); 

ALTER TABLE Favourite_SP
ADD CONSTRAINT FK_custidforfavsp
FOREIGN KEY (CustId) REFERENCES Customer(CustId); 

ALTER TABLE Favourite_SP
ADD CONSTRAINT FK_spidforfavsp
FOREIGN KEY (SpId) REFERENCES ServiceProvider(SpId); 

ALTER TABLE SpRating
ADD CONSTRAINT FK_custidforsprating
FOREIGN KEY (CustId) REFERENCES Customer(CustId); 

ALTER TABLE SpRating
ADD CONSTRAINT FK_spidforsprating
FOREIGN KEY (SpId) REFERENCES ServiceProvider(SpId); 

ALTER TABLE BlockCust
ADD CONSTRAINT FK_custidforblockcust
FOREIGN KEY (CustId) REFERENCES Customer(CustId); 

ALTER TABLE BlockCust
ADD CONSTRAINT FK_spidforblockcust
FOREIGN KEY (SpId) REFERENCES ServiceProvider(SpId); 
