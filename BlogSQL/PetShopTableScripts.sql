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



create table StaticPages (
	StaticPageID int identity (1, 1) primary key not null,
	StaticPageTitle nvarchar(50),
	StaticPageContent nvarchar(max)
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
	@PostContent nvarchar(max),
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

create procedure [dbo].[AddNewTag]( 
@TagName nvarchar(50)
)
as
begin
insert into Tags (TagName)
values (@TagName)

end

go

create procedure [dbo].[AddNewStaticPage](
@StaticPageTitle nvarchar(50),
@StaticPageContent nvarchar (max)
)
as
begin
insert into StaticPages (StaticPageTitle, StaticPageContent)
values(@StaticPageTitle, @StaticPageContent)

end

go

create procedure [dbo].[GetAllPostsOrderedByDate] 
as
begin
select *
from Posts
where PostStatus = 1
order by PostDate 
end

go

create procedure [dbo].[GetPostbyDate] (@postdate date)
as
begin
select *
from Posts where posts.PostDate = @postdate and posts.PostStatus = 1
end

go

create procedure [dbo].[GetPostsByCategory] (@categoryID int)
as
begin
select *
from posts where posts.CategoryID = @categoryID and posts.PostStatus = 1
end

go

create procedure [dbo].[GetPostsByTag] (@tagId int)
as
begin
select *
from Posts 
join PostsTags on PostsTags.PostID = Posts.PostID
join tags on tags.TagID = @tagId
where PostStatus =1
end

go

create procedure [dbo].[addnewpoststags] (@tagid int, @postsid int)
as
begin
insert into PostsTags (PostID, TagID)
values (@postsid, @tagid)
end

go

create procedure [dbo].[GetAllTags] 
as
begin
select *
from Tags
end

go

create procedure [dbo].[updatePostContent] (@postid int, @postcontent nvarchar(max))
as
begin
update Posts
set posts.PostContent = @postcontent
where posts.PostID = @postid
end

go


create procedure [dbo].[updatePostStatus] (@postid int)
as
begin
update posts
set posts.PostStatus = 1
where posts.PostID = @postid
end

go

create procedure [dbo].[softdeletepost] (@postId int)
as
begin
update posts
set posts.PostStatus = 2
where posts.PostID = @postId
end

go