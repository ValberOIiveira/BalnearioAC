INSERT INTO user_type (name) VALUES ('Administrador'), ('Funcionário'), ('Visitante'), ('Usuário');

INSERT INTO users (name, cpf, email, phone, age, passwd, id_user_type)
VALUES 
('João da Silva', '123.451.789-00', 'a@a.com', '11999999999', '1990-01-01', '1', 3);


INSERT INTO employees (id_user, role, salary, admission_date)
VALUES (1, 'Atendente', 2500.00, '2022-02-01');

INSERT INTO visitors (name, cpf, age, id_user)
VALUES ('Maria Visitante', '987.654.321-00', '1995-05-15', 2);

INSERT INTO kiosks (capacity, price, status) VALUES (6, 100.00, 'Disponível');

INSERT INTO reservations (start_date, end_date, id_visitor, id_kiosk)
VALUES ('2025-04-10', '2025-04-12', 1, 1);

INSERT INTO products (name, price, qtd) VALUES ('Água Mineral', 3.50, 100);

INSERT INTO sales (id_employee, sale_date, total_value)
VALUES (1, '2025-04-09', 7.00);

INSERT INTO items_sale (id_sale, id_product, qtd)
VALUES (1, 1, 2);
