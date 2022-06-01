use [strahovanie]

create table auth(
	worker_id int primary key identity(1,1),
	worker_login varchar(15),
	worker_password varchar(15)
)

create table dogovor_status(
	status_id int primary key identity(1,1),
	status_name varchar(50)
)

create table client(
	client_id int primary key identity(1,1),
	client_name varchar(50),
	client_surname varchar(50),
	client_otchestvo varchar(50),
	client_adress varchar(200),
	client_phone varchar(20)
)

create table worker(
	worker int primary key references auth(worker_id),
	worker_name varchar(50),
	worker_surname varchar(50),
	worker_otchestvo varchar(50),
	worker_adress varchar(200),
	worker_phonenumber varchar(50)
)

create table automobile(
	auto_id int primary key identity(1,1),
	client_id int references client(client_id),
	auto_gosnumber int,
	auto_cost money,
	auto_marka varchar(100),
	auto_model varchar(100),
	auto_createdate date
)

create table autodocumentation(
	auto_id int primary key references automobile(auto_id),
	auto_prava varchar(50),
	auto_to varchar(50),
	auto_doverennost varchar(50)
)


create table tariffs(
	tariff_id int primary key identity(1,1),
	tariff_name varchar(50),
	tariff_coeff float,
	tariff_prodolzhitelnost int
)

create table dop_pred(
	dop_id int primary key identity(1,1),
	dop_name varchar(50),
	dop_cost money
)

create table dogovor(
	dogovor_id int primary key identity(1,1),
	worker_id int references worker(worker),
	client_id int references client(client_id),
	automobile_id int references automobile(auto_id),
	tariff int references tariffs(tariff_id),
	dop_pred_1 int references dop_pred(dop_id),
	dop_pred_2 int references dop_pred(dop_id),
	dop_pred_3 int references dop_pred(dop_id),
	ezhemes_platezh money,
	createdate date,
	okonchaniedate date,
	dogovor_status int references dogovor_status(status_id)
)
