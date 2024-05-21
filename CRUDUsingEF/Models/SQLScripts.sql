create database B22APIDB
go
use B22APIDB
go
create table Product
(
Id int primary key identity,
Name varchar(50),
Price decimal,
AvailableQuantity int
)
go
insert into Product values ('Mens Shirt', 599, 10)
go
select * from Product

go

go
select * from Product

create table Category(
Id int primary key identity,
Name varchar(200)
)


insert into Category values ('Mens Wear'), ('Kids Wear')

go

alter table product
add CategoryId int foreign key references Category(Id)

go

update Product set CategoryId = 1 where Id = 4

select * from Product
select * from Category


create table Users
(
UserId int primary key identity,
FirstName varchar(50),
LastName varchar(50),
Email varchar(200),
Password varchar(30),
AddedDate datetime2
)