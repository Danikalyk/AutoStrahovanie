use [strahovanie]

insert into auth(worker_login, worker_password) values ('work1', 'work1')
insert into worker(worker, worker_surname, worker_name, worker_otchestvo, worker_adress, worker_phonenumber) values (1, '������', '����', '��������', '�������� ���� 2', '4843834833')
insert into dogovor_status (status_name) values (' ')
insert into dogovor_status (status_name) values ('���������')
insert into dogovor_status (status_name) values ('����')
insert into dogovor_status (status_name) values ('����������� �������')
insert into tariffs (tariff_name, tariff_coeff, tariff_prodolzhitelnost) values ('common', 50,3)
insert into tariffs (tariff_name, tariff_coeff, tariff_prodolzhitelnost) values ('standart', 30,6)
insert into tariffs (tariff_name, tariff_coeff, tariff_prodolzhitelnost) values ('premium', 15,12)
insert into tariffs (tariff_name, tariff_coeff, tariff_prodolzhitelnost) values ('legend', 5,24)