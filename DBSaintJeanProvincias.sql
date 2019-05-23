/* Creacion de Base de Datos */
USE master
go
if exists (select * from sys.sysdatabases where name= 'DBSaintJeanProvincias')
drop database DBSaintJeanProvincias
go
create database DBSaintJeanProvincias
go
if exists (select * from sys.syslogins where name = 'LogSaintJeanProvincias')
drop login LogSaintJeanProvincias
go
create login LogSaintJeanProvincias with password = 'SaintJeanProvincias123'
go
use DBSaintJeanProvincias
go
create user usuario from login LogSaintJeanProvincias
go
grant exec to usuario
go
/* Creacion de Tablas */
create table Roles (
ID int identity (1,1) primary key,
Nombre varchar (20) unique not null 
)
go
create table Usuarios(
ID int identity (1,1) primary key,
Nombre varchar (40) unique not null,
Clave varchar (40) not null,
IDRol int foreign key references Roles(ID),
constraint ukUsuario unique (Nombre, Clave)
)
go
create table Provincias(
ID int identity (1,1) primary key,
Nombre varchar (40) unique not null,
CantSenadores int,
CantDiputados int,
Capital varchar (40),
Superficie int,
Poblacion int,
Nota int,
IDGobernador int)
go
create table Gobernadores(
ID int identity (1,1) primary key,
Nombre varchar (40),
Apellido varchar (40),
PeriodoGobierno varchar (30),
Historial varchar (max),
IDProvincia int foreign key references Provincias(ID) unique not null
)
go
create table Alumnos(
ID int identity (1,1) primary key,
Nombre varchar(50) not null,
DNI int unique not null,
IDProvincia int foreign key references Provincias(ID) not null
)
go
create table PuntosDeInteres(
ID int identity (1,1) primary key,
Nombre varchar (60) not null,
Descripcion varchar (max),
IDProvincia int foreign key references Provincias(ID) not null,
constraint ukPI unique (Nombre, IDProvincia)
)
go
create table ProductosRegionales(
ID int identity (1,1) primary key,
Nombre varchar (60) not null,
Descripcion varchar(max),
IDProvincia int foreign key references Provincias(ID) not null,
constraint ukPR unique (Nombre, IDProvincia)
)
go
/* Creacion de StoreProcedures */
create procedure Roles_List as
select * from Roles order by Nombre 
go
create procedure Roles_Find(@ID int) as
select * from Roles where ID = @ID
go
create procedure Usuarios_Update(@ID int, @Clave varchar (40)) as
update Usuarios set Clave = @Clave where ID = @ID
go
create procedure Usuarios_List as
select * from Usuarios order by Nombre
go
create procedure Usuarios_Find (@ID int) as
select * from Usuarios where ID = @ID
go
create procedure Usuarios_Login(@Nombre varchar (40), @Clave varchar (40)) as
select * from Usuarios where Nombre = @Nombre and Clave = @Clave
go
create procedure Gobernadores_Update(@ID int, @Nombre varchar (40), @Apellido varchar (40), @PeriodoGobierno varchar (30), @Historial varchar (max)) as
update Gobernadores set Nombre = @Nombre, Apellido = @Apellido, PeriodoGobierno = @PeriodoGobierno, Historial = @Historial where ID = @ID
go
create procedure Gobernadores_List as
select * from Gobernadores order by Nombre
go
create procedure Gobernadores_Find(@IDProvincia int) as
select * from Gobernadores where IDProvincia = @IDProvincia
go
create procedure Provincias_Update (@ID int, @Nombre varchar (40), @CantSenadores int, @CantDiputados int,
								   @Capital varchar (40), @Superficie int, @Poblacion int, @Nota int) as
update Provincias set Nombre = @Nombre, CantSenadores = @CantSenadores, CantDiputados = @CantDiputados,
					 Capital = @Capital, Superficie = @Superficie, Poblacion = @Poblacion, Nota = @Nota
				  where ID = @ID
go
create procedure Provincias_List as
select * from Provincias order by Nombre
go
create procedure Provincias_Find (@ID int) as
select * from Provincias where ID = @ID
go
create procedure Alumnos_Insert (@Nombre varchar (50), @DNI int, @IDProvincia int) as
begin
insert Alumnos (Nombre, DNI, IDProvincia) values (@Nombre, @DNI, @IDProvincia)
select @@IDENTITY
end
go
create procedure Alumnos_Update (@ID int, @Nombre varchar (50), @DNI int, @IDProvincia int) as
update Alumnos set Nombre = @Nombre, DNI = @DNI, IDProvincia = @IDProvincia where ID = @ID
go
create procedure Alumnos_Delete (@ID int) as
delete Alumnos where ID = @ID
go
create procedure Alumnos_List as
select * from Alumnos order by Nombre
go
create procedure Alumnos_Find (@ID int) as
select * from Alumnos where ID = @ID
go
create procedure PuntosDeInteres_Insert (@Nombre varchar (60), @Descripcion varchar (max), @IDProvincia int) as
begin
insert PuntosDeInteres (Nombre, Descripcion, IDProvincia) values (@Nombre, @Descripcion, @IDProvincia)
select @@IDENTITY
end
go
create procedure PuntosDeInteres_Update (@ID int, @Nombre varchar (60), @Descripcion varchar (max), @IDProvincia int) as
update PuntosDeInteres set Nombre = @Nombre, Descripcion = @Descripcion, IDProvincia = @IDProvincia where ID = @ID
go
create procedure PuntosDeInteres_Delete (@ID int) as
delete PuntosDeInteres where ID = @ID
go
create procedure PuntosDeInteres_List (@IDProvincia int) as
select * from PuntosDeInteres where IDProvincia = @IDProvincia order by Nombre
go
create procedure PuntosDeInteres_Find (@ID int) as
select * from PuntosDeInteres where ID = @ID
go
create procedure ProductosRegionales_Insert (@Nombre varchar (60), @Descripcion varchar (max), @IDProvincia int) as
begin
insert ProductosRegionales (Nombre, Descripcion, IDProvincia) values (@Nombre, @Descripcion, @IDProvincia)
select @@IDENTITY
end
go
create procedure ProductosRegionales_Update (@ID int, @Nombre varchar (60), @Descripcion varchar (max), @IDProvincia int) as
update ProductosRegionales set Nombre = @Nombre, Descripcion = @Descripcion, IDProvincia = @IDProvincia where ID = @ID
go
create procedure ProductosRegionales_Delete (@ID int) as
delete ProductosRegionales where ID = @ID
go
create procedure ProductosRegionales_List (@IDProvincia int) as
select * from ProductosRegionales where IDProvincia = @IDProvincia order by Nombre
go
create procedure ProductosRegionales_Find (@ID int) as
select * from ProductosRegionales where ID = @ID
go
/* Carga de Datos Comunes (Hardcoding) */
insert Roles values ('Maestra')
insert Roles values ('Grupo')
insert Usuarios (Nombre, Clave, IDRol) values ('Buenos Aires', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('Ciudad Autonoma de Buenos Aires', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('Catamarca', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('Chaco', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('Chubut', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('Cordoba', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('Corrientes', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('Entre Rios', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('Formosa', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('Jujuy', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('La Pampa', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('La Rioja', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('Mendoza', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('Misiones', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('Neuquen', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('Rio Negro', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('Salta', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('San Juan', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('San Luis', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('Santa Cruz', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('Santa Fe', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('Santiago del Estero', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('Tierra del Fuego', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('Tucuman', '123', 2)
insert Usuarios (Nombre, Clave, IDRol) values ('Admin', 'Admin', 1)
insert Provincias (Nombre) values ('Buenos Aires')
insert Provincias (Nombre) values ('Ciudad Autonoma de Buenos Aires')
insert Provincias (Nombre) values ('Catamarca')
insert Provincias (Nombre) values ('Chaco')
insert Provincias (Nombre) values ('Chubut')
insert Provincias (Nombre) values ('Cordoba')
insert Provincias (Nombre) values ('Corrientes')
insert Provincias (Nombre) values ('Entre Rios')
insert Provincias (Nombre) values ('Formosa')
insert Provincias (Nombre) values ('Jujuy')
insert Provincias (Nombre) values ('La Pampa')
insert Provincias (Nombre) values ('La Rioja')
insert Provincias (Nombre) values ('Mendoza')
insert Provincias (Nombre) values ('Misiones')
insert Provincias (Nombre) values ('Neuquen')
insert Provincias (Nombre) values ('Rio Negro')
insert Provincias (Nombre) values ('Salta')
insert Provincias (Nombre) values ('San Juan')
insert Provincias (Nombre) values ('San Luis')
insert Provincias (Nombre) values ('Santa Cruz')
insert Provincias (Nombre) values ('Santa Fe')
insert Provincias (Nombre) values ('Santiago del Estero')
insert Provincias (Nombre) values ('Tierra del Fuego')
insert Provincias (Nombre) values ('Tucuman')
insert Gobernadores (IDProvincia) values (1)
insert Gobernadores (IDProvincia) values (2)
insert Gobernadores (IDProvincia) values (3)
insert Gobernadores (IDProvincia) values (4)
insert Gobernadores (IDProvincia) values (5)
insert Gobernadores (IDProvincia) values (6)
insert Gobernadores (IDProvincia) values (7)
insert Gobernadores (IDProvincia) values (8)
insert Gobernadores (IDProvincia) values (9)
insert Gobernadores (IDProvincia) values (10)
insert Gobernadores (IDProvincia) values (11)
insert Gobernadores (IDProvincia) values (12)
insert Gobernadores (IDProvincia) values (13)
insert Gobernadores (IDProvincia) values (14)
insert Gobernadores (IDProvincia) values (15)
insert Gobernadores (IDProvincia) values (16)
insert Gobernadores (IDProvincia) values (17)
insert Gobernadores (IDProvincia) values (18)
insert Gobernadores (IDProvincia) values (19)
insert Gobernadores (IDProvincia) values (20)
insert Gobernadores (IDProvincia) values (21)
insert Gobernadores (IDProvincia) values (22)
insert Gobernadores (IDProvincia) values (23)
insert Gobernadores (IDProvincia) values (24)
/* Asignación de Gobernadores a Provincias */
update Provincias set IDGobernador = 1 where ID = 1
update Provincias set IDGobernador = 2 where ID = 2
update Provincias set IDGobernador = 3 where ID = 3
update Provincias set IDGobernador = 4 where ID = 4
update Provincias set IDGobernador = 5 where ID = 5
update Provincias set IDGobernador = 6 where ID = 6
update Provincias set IDGobernador = 7 where ID = 7
update Provincias set IDGobernador = 8 where ID = 8
update Provincias set IDGobernador = 9 where ID = 9
update Provincias set IDGobernador = 10 where ID = 10
update Provincias set IDGobernador = 11 where ID = 11
update Provincias set IDGobernador = 12 where ID = 12
update Provincias set IDGobernador = 13 where ID = 13
update Provincias set IDGobernador = 14 where ID = 14
update Provincias set IDGobernador = 15 where ID = 15
update Provincias set IDGobernador = 16 where ID = 16
update Provincias set IDGobernador = 17 where ID = 17
update Provincias set IDGobernador = 18 where ID = 18
update Provincias set IDGobernador = 19 where ID = 19
update Provincias set IDGobernador = 20 where ID = 20
update Provincias set IDGobernador = 21 where ID = 21
update Provincias set IDGobernador = 22 where ID = 22
update Provincias set IDGobernador = 23 where ID = 23
update Provincias set IDGobernador = 24 where ID = 24