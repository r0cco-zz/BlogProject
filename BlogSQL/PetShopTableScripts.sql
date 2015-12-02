use master
go

drop database PetShopBlog
go

create database PetShopBlog
go

use PetShopBlog
go

create table Categories (
	CategoryID int identity (1, 1) primary key not null,
	CategoryName nvarchar(50)
)
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
begin
	insert into Categories
	values ('Pet Health'),('Pet Wellness'),('Pet Products'),('Pet Fun'),('Getting to Know Your Pet')

	insert into Posts
	values (1,'SampleTitle1','2015-12-01','<p>Lorem ipsum dolor sit amet smooshy</p><p>Feeder bird seed barky lazy cat wag tail catch bedding</p><p>Food dog house. String fur stripes house train kitty string. Fluffy leash walk nap pet purr dragging run feathers.</p>','Author',1),
	(2,'SampleTitle2','2015-11-25','<p>Lorem ipsum dolor sit amet cage kibble speak.</p><p>Lick throw pet gate fluffy groom polydactyl</p><p>Kisses. Behavior dragging</p><p>Wag tail drool nap aquatic whiskers grooming run fast fluffy field purr Buddy bed swimming Mittens Buddy bird food throw pet supplies. Pet Supplies shake Snowball aquarium food roll over dragging stripes. Food stripes fur feeder wagging carrier leash. Ball dog house twine wag tail parakeet nest biscuit walk stripes Rover slobbery fish feeder yawn.</p>','Author',1),
	(3,'SampleTitle3','2015-11-20','<h2>Lorem</h2><p>Ipsum dolor sit amet finch kitten leash vaccination.</p>','Author',1)

	insert into Tags
	values ('Dogs'),('Cats'),('Ferrets'),('Food'),('Toys'),('Health'),('Fun'),('Knowledge'),('Happy'),('Smiley'),('Turtles'),('Snakes')

	insert into StaticPages
	values ('Title1','<p>Lorem ipsum dolor sit amet fleas purr pet food water dog mouse. Meow groom run purr speak collar throw dog swimming smooshy puppy carrier commands bark chirp carrier roll over chirp lick smooshy.</p>'),
	('Title2','<p>Lorem ipsum dolor sit amet furry meow foot speak biscuit feathers stay field water dog groom chew mittens harness bed. Fleas whiskers shake grooming gimme five run turtle furry. Bark barky Spike sit cage litter box dog treats tuxedo grooming warm furry. House Train bird food cat Buddy small animals litter box pet gate.</p>')

	insert into PostsTags
	values (1,1),
	(1,4),
	(1,5),
	(2,2),
	(2,3),
	(2,5),
	(2,10),
	(3,4)

end
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
order by PostDate desc
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

create procedure [dbo].[GetPostByID] (@postId int)
as
begin
select * from Posts p
where p.PostID = @postId
end

go

create procedure [dbo].[GetAllTagsOnAPostByPostID] (@postId int)
as
begin
select t.TagID, t.TagName
from Tags t
	inner join PostsTags pt on t.TagID = pt.TagID
where pt.PostID = @postId
end

go

create procedure [dbo].[GetCategoryByPostID] (@postId int)
as
begin
select c.CategoryName from Categories c
	inner join Posts p on p.CategoryID = c.CategoryID
where p.PostID = @postId
end