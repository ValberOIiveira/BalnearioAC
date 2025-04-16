-- Tipos de Usuário
INSERT INTO user_type (name) VALUES 
('Administrador'),
('Funcionário'),
('Visitante');

-- Usuários
INSERT INTO users (name, cpf, email, phone, age, passwd, id_user_type) VALUES
('João Admin', '123.456.789-00', 'admin@example.com', '11999999999', '1985-01-01', 'admin123', 1),
('Maria Funcionária', '987.654.321-00', 'funcionaria@example.com', '11988888888', '1990-05-10', 'func123', 2),
('Carlos Visitante', '111.222.333-44', 'visitante@example.com', '11977777777', '2000-08-15', 'visit123', 3);

-- Funcionário vinculado ao usuário Maria Funcionária
INSERT INTO employees (id_user, role, salary, admission_date) VALUES
(2, 'Atendente', 2500.00, '2023-01-15');

-- Visitante vinculado ao Carlos Visitante
INSERT INTO visitors (name, cpf, age, id_user) VALUES
('Carlos Visitante', '111.222.333-44', '2000-08-15', 3);

-- Quiosques
INSERT INTO kiosks (capacity, price, status) VALUES
(5, 150.00, 'Disponível'),
(8, 200.00, 'Disponível');

-- Produtos
INSERT INTO products (name, price, qtd, category) VALUES
('Coca-Cola Lata', 5.50, 100, 'Bebida'),
('Água Mineral', 3.00, 200, 'Bebida'),
('Churrasco Completo', 50.00, 20, 'Alimento');

-- Reserva exemplo
INSERT INTO reservations (start_date, end_date, id_visitor, id_kiosk) VALUES
('2025-04-20', '2025-04-22', 1, 1);

-- Venda exemplo
INSERT INTO sales (id_employee, sale_date, total_value) VALUES
(1, '2025-04-16', 61.00); -- será atualizada pelo valor dos produtos abaixo

-- Itens vendidos (e atualiza o estoque via trigger)
INSERT INTO items_sale (id_sale, id_product, qtd) VALUES
(1, 1, 5),  -- 5x Coca-Cola (5.50 x 5 = 27.50)
(1, 3, 1),  -- 1x Churrasco (50.00 x 1 = 50.00) → total ajustado depois para 77.50
(1, 2, 2);  -- 2x Água (3.00 x 2 = 6.00)

-- Ajusta o total da venda
UPDATE sales SET total_value = (
    SELECT SUM(p.price * i.qtd)
    FROM items_sale i
    JOIN products p ON p.id = i.id_product
    WHERE i.id_sale = 1
)
WHERE id = 1;
