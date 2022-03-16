create database car

create table users(
    userid UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    useremail VARCHAR(256) NOT NULL UNIQUE,
    userpassword VARCHAR(200) NOT NULL,
    username VARCHAR(50) NOT NULL,
    userphno VARCHAR(10) UNIQUE,
    userCity VARCHAR(20) NOT NULL,
    userRole INT DEFAULT 0
)

create table cars(
    carid UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    carmake VARCHAR(30) NOT NULL,
    carmodelname VARCHAR(30) NOT NULL,
    carshifttype VARCHAR(30) NOT NULL,
    carstatus VARCHAR(30) DEFAULT 'ACTIVE',
    cartype VARCHAR(30) NOT NULL,
    carcity VARCHAR(30) NOT NULL,
    carfuel VARCHAR(30) NOT NULL,
    userid UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES users(userid)
)

create TABLE purchases(
    purchaseid UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    carid UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES cars(carid),
    userid UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES users(userid),
    purchasedate DATETIME DEFAULT GETDATE()
)