create database MusicProject
GO
use "MusicProject"
GO
create table dosyagoster(
[ıd] int,
[Dosya_Yolu] nchar(255)
);
GO
create table kayitlar(
[ID] int primary key identity(1,1) not null,
[Name] nchar(20),
[Surname] nchar(20),
[EMail] nchar(20),
[Password] nchar(15)
);
GO
create table music(
[ID] int primary key identity(1,1) not null,
[Name] nchar(40),
[Mood Name] nchar(10),
[Tur Adi] nchar(10),
Artist nchar(20),
kaydeden char(30),
yol nchar(120)
);
GO
insert into kayitlar (Name,Surname,EMail,Password) values ('Admin','Admin','admin@hotmail.com','123456789')