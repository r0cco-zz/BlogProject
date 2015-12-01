use master
go

drop database PetShopBlog
go

create database PetShopBlog
go

use PetShopBlog
go

create table Posts (
	PostID int identity (1, 1) primary key not null,
	CategoryID int foreign key references Categories (CategoryID) not null,
	PostTitle nvarchar(100),
	PostDate date,
	PostContent nvarchar(2000),
	Author nvarchar(50),
	PostStatus int
)
go

create table Categories (
	CategoryID int identity (1, 1) primary key not null,
	CategoryName nvarchar(50)
)
go

create table Tags (
	TagID int identity (1, 1) primary key not null,
	TagName nvarchar(50)
)
go

create table Roles (
	RoleID int identity (1, 1) primary key not null,
	UserID int foreign key references Users (UserID) not null,
	RoleValue int
)
go

create table StaticPages (
	StaticPageID int identity (1, 1) primary key not null,
	StaticPageTitle nvarchar(50),
	StaticPageContent nvarchar(2000)
)
go

create table Users (
	UserID int identity (1, 1) primary key not null,
	UserName nvarchar(50),
	FirstName nvarchar(20),
	LastName nvarchar(20),
	UserPassword nvarchar(20)
)
go

create table PostsTags (
	PostID int foreign key references Posts (PostID) not null,
	TagID int foreign key references Tags (TagID) not null,
	constraint pk_PostsTags primary key (PostID, TagID)
)
go

create procedure [dbo].[AddNewPost](
	@CategoryID int,
	@PostTitle nvarchar(100),
	@PostDate date,
	@PostContent nvarchar(2000),
	@Author nvarchar(50),
	@PostStatus int,
	@PostID int output
	)
	as
begin
	insert into Posts (CategoryID, PostTitle, PostDate, PostContent, Author, PostStatus)
	values (@CategoryID, @PostTitle, @PostDate, @PostContent, @Author, @PostStatus)

	set @PostID = SCOPE_IDENTITY();
end

go
