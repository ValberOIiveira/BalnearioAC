-- Inserindo tipos de usuários
INSERT INTO user_type (name) VALUES 
('Admin'),
('Employee'),
('Visitor');

-- Inserindo usuários base
INSERT INTO users (name, cpf, email, phone, age, passwd, id_user_type) VALUES
('João Admin', '111.111.111-11', 'admin@resort.com', '11999999999', '1980-01-01', 'adminpass', 1),
('Maria Funcionária', '222.222.222-22', 'func@resort.com', '11988888888', '1990-05-10', 'funcpass', 2),
('Carlos Visitante', '333.333.333-33', 'visitante@resort.com', '11977777777', '2000-07-15', 'visitpass', 3);

-- Inserindo funcionário vinculado ao usuário
INSERT INTO employees (id_user, role, salary, admission_date) VALUES
(2, 'Atendente', 3000.00, '2022-03-01');

-- Inserindo visitante vinculado ao usuário
INSERT INTO visitors (name, cpf, age, id_user) VALUES
('Carlos Visitante', '333.333.333-33', '2000-07-15', 3);

-- Inserindo quiosques
INSERT INTO kiosks (capacity, price, status) VALUES
(4, 150.00, 'Disponível'),
(6, 200.00, 'Disponível');

-- Inserindo produtos
INSERT INTO products (name, price, qtd) VALUES
('Coca-Cola Lata', 5.00, 100),
('Água Mineral', 3.00, 200),
('Churrasco Completo', 50.00, 20);
