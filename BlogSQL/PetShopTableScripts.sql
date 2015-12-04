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
	PostContent text,
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
	StaticPageContent text
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
	values ('Pet Health'),('Pet Wellness'),('Pet Products'),('Pet Fun'),('Getting to Know Your Pet'),('Specials')

	insert into Posts
	values
	(2,'Yet another Blog Post Title','2015-11-01','<p>Lorem ipsum dolor sit amet paws drool play slobber bird food. Window kisses purr kitty wag tail stick window catch water birds play dead feathers running chow field fur aquarium feathers. Feathers pet gate scratcher furry kitten food cockatiel cage vaccination ball tooth toys Mittens walk run fast pet supplies bird food finch throw.</p> <p>Cat picture!!</p> <p><img src="http://placekitten.com.s3.amazonaws.com/homepage-samples/200/139.jpg" alt="" /></p>','Nikola Tesla',1),
	(4,'SampleTitle4','2015-11-14','<p>Lorem ipsum dolor sit amet window leash polydactyl groom walk dog running turtle dog house walk bed. Nap heel chow park polydactyl meow lazy dog litter box fish. Slobber Tigger Fido harness house train window furry harness catch dog left paw fetch twine lazy cat walk Scooby snacks critters. Parrot right paw Spike sit field canary litter harness chew Tigger slobbery dog house food. Fido ball Scooby snacks foot good boy field Spike polydactyl roll over bed dragging cage ferret Scooby snacks play wag tail. Puppy gimme five litter field sit stick toy Scooby snacks commands.</p> <p>Toy pet food foot bird seed field drool chirp house train treats commands. Hamster Tigger cat stay dog house tooth carrier nest aquarium park nap stripes park string running pet Rover good boy. Hamster play groom pet catch Rover wag tail toy bird food. Aquatic left paw dinnertime vaccine brush cat furry whiskers brush walk wet nose gimme five run. Cage Tigger harness pet polydactyl fur bird behavior stay ferret left paw cage birds feathers brush paws roll over. Parrot roll over parakeet Spike meow brush ID tag bed lazy cat. Litter twine kisses purr cat vaccine pet cage tail furry harness house train dog.</p>','Author',1),
	(3,'SampleTitle3','2015-11-20','<h2>Lorem</h2><p>Ipsum dolor sit amet finch kitten leash vaccination.</p>','Author',1),
	(2,'Another Title','2015-11-24','<p>Lorem ipsum dolor sit amet mouse parrot id tag walk hamster bird food house train swimming ball bed tabby lazy dog tigger. Tigger yawn roll over Rover play head chew yawn cage running.</p> <p>Here is a picture of a kitten:</p> <p><img src="http://placekitten.com.s3.amazonaws.com/homepage-samples/408/287.jpg" alt="" /></p>','Author',1),
	(2,'SampleTitle2','2015-11-25','<p>Lorem ipsum dolor sit amet cage kibble speak.</p><p>Lick throw pet gate fluffy groom polydactyl</p><p>Kisses. Behavior dragging</p><p>Wag tail drool nap aquatic whiskers grooming run fast fluffy field purr Buddy bed swimming Mittens Buddy bird food throw pet supplies. Pet Supplies shake Snowball aquarium food roll over dragging stripes. Food stripes fur feeder wagging carrier leash. Ball dog house twine wag tail parakeet nest biscuit walk stripes Rover slobbery fish feeder yawn.</p>','Author',1),
	(1,'SampleTitle1','2015-12-01','<p>Lorem ipsum dolor sit amet smooshy</p><p>Feeder bird seed barky lazy cat wag tail catch bedding</p><p>Food dog house. String fur stripes house train kitty string. Fluffy leash walk nap pet purr dragging run feathers.</p>','Author',1),
	(3,'A Title Will Go Here','2015-12-02','<p>Lorem ipsum dolor sit amet tuxedo fluffy. Aquarium meow <strong>pet supplies vitamins</strong> bird seed feeder. Chew park lazy cat run fast stay strin<em>g chew vitamins toy turtle slo</em>bber cat park left paw pet <strong>supplies ferret nest w</strong>indow. <em>Maine Coon Cat lazy cat licks bark leash pet food dog house mouse speak hamster wagging aquarium</em> kitty throw kitten dragging Scooby snacks pet supplies.</p> <ul> <li>Feeder Snowball cage kibble crate</li> <li>cage fetch Mittens pet food kitty. Crate</li> <li>fleas bird food cat grooming crate lazy cat</li> <li>string biscuit toy meow ferret roll over Mittens string play dead</li> <li>canary lazy dog parakeet. Teeth groom roll over park kitten litter dragging speak groom bedding tongue.</li> </ul> <p>Cat picture incoming!!</p> <p><img src="http://placekitten.com.s3.amazonaws.com/homepage-samples/200/287.jpg" alt="" /></p> <p>Some more text</p>','Benjamin Franklin',1),
	(5,'Blog Post Title','2015-12-03','<p>Lorem ipsum dolor sit amet paws drool play slobber bird food. Window kisses purr kitty wag tail stick window catch water birds play dead feathers running chow field fur aquarium feathers. Feathers pet gate scratcher furry kitten food cockatiel cage vaccination ball tooth toys Mittens walk run fast pet supplies bird food finch throw.</p> <p>Cat picture!!</p> <p><img src="http://placekitten.com.s3.amazonaws.com/homepage-samples/200/139.jpg" alt="" /></p>','George Washington',1)
	
	insert into Tags
	values ('dogs'),('cats'),('ferrets'),('food'),('toys'),('health'),('fun'),('knowledge'),('happy'),('smiley'),('turtles'),('snakes')

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
	(3,4),
	(3,2),
	(3,6),
	(4,5),
	(4,10),
	(4,11),
	(4,12),
	(5,5),
	(5,6),
	(7,5),
	(7,7),
	(7,12),
	(8,1),
	(8,4),
	(8,8)

end
go

create procedure [dbo].[AddNewPost](
	@CategoryID int,
	@PostTitle nvarchar(100),
	@PostDate date,
	@PostContent text,
	@Author nvarchar(50),
	@PostStatus int,
	@PostID int output
	)
	as
begin
	insert into Posts (CategoryID, PostTitle, PostDate, PostContent, Author, PostStatus)
	values (@CategoryID, @PostTitle, @PostDate, @PostContent, @Author, @PostStatus)
set @PostID = SCOPE_IDENTITY()
end

go

create procedure [dbo].[AddNewTag]( 
@TagName nvarchar(50),
@TagID int output
)
as
begin
insert into Tags (TagName)
values (@TagName)

set @TagID = SCOPE_IDENTITY();
end

go

create procedure [dbo].[GetPostsByPR]
as
begin
select * from posts
where PostStatus = 0
end

go

create procedure [dbo].[AddNewStaticPage](
@StaticPageID int,
@StaticPageTitle nvarchar(50),
@StaticPageContent text
)
as
begin
insert into StaticPages (StaticPageTitle, StaticPageContent)
values(@StaticPageTitle, @StaticPageContent)

set @StaticPageID = SCOPE_IDENTITY();

end

go

create procedure [dbo].[GetAllPostsOrderedByDate] 
as
begin
select *
from Posts
where PostStatus = 1
order by PostID desc
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

create procedure [dbo].[AddNewPostTags] (@TagId int, @PostId int)
as
begin
insert into PostsTags (PostID, TagID)
values (@postid, @tagid)
end

go

create procedure [dbo].[GetAllTags] 
as
begin
select *
from Tags
end

go

create procedure [dbo].[updatePostContent] (@postid int, @postcontent text)
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