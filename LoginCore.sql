drop database LoginCore;
create database LoginCore;
use LoginCore;

create table Cliente(
Id int auto_increment primary key,
Nome varchar(50) not null,
Nascimento datetime not null,
Sexo char(1),
CPF varchar(11) not null,
Telefone varchar(14) not null,
Email varchar(50) not null,
Senha varchar(50) not null,
Situacao char(1) not null
);

create table Colaborador(
id int auto_increment primary key,
Nome varchar(50) not null,
Email varchar(50) not null,
Senha varchar(8) not null,
Tipo varchar(8) not null
);

select * from Colaborador;
select * from Colaborador where Email = "Josi@hotmail.com" and Senha = "Josi2024";
select * from Cliente;

insert into Colaborador(nome,Email,Senha,Tipo) values("Josisvaldo", "Josi@hotmail.com", "Josi2024", "Gerente");
insert into Colaborador(nome,Email,Senha,Tipo) values("Neymar", "ney@gmail.com", "ney2020", "Joagdor");

insert into Cliente(Nome,Nascimento,Sexo,CPF, Telefone,Email,Senha,Situacao) values("Miguel","2006-07-10","M","12345678910",12345678910123, "Miguel@hotmail.com", "Miguel2024", "A");
insert into Cliente(Nome,Nascimento,Sexo,CPF, Telefone,Email,Senha,Situacao) values("Miguel","2006-07-10","M","12345678910",12345678910123, "Miguel@hotmail.com", "Miguel2024", "A");
