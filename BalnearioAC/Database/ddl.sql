-- CRIAÇÃO DO BANCO
CREATE DATABASE Resort;

-- TABELAS
CREATE TABLE user_type (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL
);

CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    cpf VARCHAR(14) UNIQUE NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    phone VARCHAR(20),
    age DATE,
    passwd VARCHAR(255) NOT NULL,
    id_user_type INTEGER NOT NULL,
    FOREIGN KEY (id_user_type) REFERENCES user_type(id)
);

CREATE TABLE employees (
    id SERIAL PRIMARY KEY,
    id_user INTEGER,
    role VARCHAR(255),
    salary DECIMAL(10,2),
    admission_date DATE,
    FOREIGN KEY (id_user) REFERENCES users(id) ON DELETE CASCADE
);

CREATE TABLE visitors (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    cpf VARCHAR(14) UNIQUE,
    age DATE,
    id_user INTEGER,
    FOREIGN KEY (id_user) REFERENCES users(id) ON DELETE SET NULL
);

CREATE TABLE kiosks (
    id SERIAL PRIMARY KEY,
    capacity INTEGER,
    price DECIMAL(10,2),
    status VARCHAR(255)
);

CREATE TABLE reservations (
    id SERIAL PRIMARY KEY,
    start_date DATE NOT NULL,
    end_date DATE NOT NULL,
    id_visitor INTEGER NOT NULL,
    id_kiosk INTEGER NOT NULL,
    FOREIGN KEY (id_visitor) REFERENCES visitors(id) ON DELETE CASCADE,
    FOREIGN KEY (id_kiosk) REFERENCES kiosks(id) ON DELETE SET NULL
);

CREATE TABLE products (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    price DECIMAL(10,2),
    qtd INTEGER,
    category VARCHAR(255)
);

CREATE TABLE sales (
    id SERIAL PRIMARY KEY,
    id_employee INTEGER NOT NULL,
    sale_date DATE NOT NULL,
    total_value DECIMAL(10,2),
    FOREIGN KEY (id_employee) REFERENCES employees(id) ON DELETE SET NULL
);

CREATE TABLE items_sale (
    id SERIAL PRIMARY KEY,
    id_sale INTEGER NOT NULL,
    id_product INTEGER NOT NULL,
    qtd INTEGER NOT NULL,
    FOREIGN KEY (id_sale) REFERENCES sales(id) ON DELETE CASCADE,
    FOREIGN KEY (id_product) REFERENCES products(id) ON DELETE SET NULL
);

-- LOGS E RELATÓRIOS
CREATE TABLE log_reservations (
    id SERIAL PRIMARY KEY,
    id_reservation INTEGER,
    action VARCHAR(50), 
    action_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    performed_by INTEGER,
    FOREIGN KEY (performed_by) REFERENCES users(id) ON DELETE SET NULL
);

CREATE TABLE log_sales (
    id SERIAL PRIMARY KEY,
    id_sale INTEGER,
    action VARCHAR(50), 
    action_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    performed_by INTEGER,
    details JSONB,
    FOREIGN KEY (id_sale) REFERENCES sales(id) ON DELETE SET NULL,
    FOREIGN KEY (performed_by) REFERENCES users(id) ON DELETE SET NULL
);

CREATE TABLE log_user_activity (
    id SERIAL PRIMARY KEY,
    id_user INTEGER,
    activity_type VARCHAR(50),
    activity_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_user) REFERENCES users(id) ON DELETE SET NULL
);

-- Tabelas de Relatórios
CREATE TABLE report_reservations (
    id SERIAL PRIMARY KEY,
    report_date DATE DEFAULT CURRENT_DATE,
    user_id INTEGER,
    kiosk_id INTEGER,
    start_date DATE,
    end_date DATE,
    FOREIGN KEY (kiosk_id) REFERENCES kiosks(id),
    FOREIGN KEY (user_id) REFERENCES users(id)
);

CREATE TABLE report_kiosk_occupancy (
    id SERIAL PRIMARY KEY,
    report_date DATE DEFAULT CURRENT_DATE,
    kiosk_id INTEGER,
    total_reservations INTEGER,
    occupied_days INTEGER,
    FOREIGN KEY (kiosk_id) REFERENCES kiosks(id)
);

CREATE TABLE report_sales_by_employee (
    id SERIAL PRIMARY KEY,
    report_date DATE DEFAULT CURRENT_DATE,
    employee_id INTEGER,
    employee_name VARCHAR(255),
    total_sales INTEGER,
    total_value DECIMAL(10,2),
    FOREIGN KEY (employee_id) REFERENCES employees(id)
);

CREATE TABLE report_products_sold (
    id SERIAL PRIMARY KEY,
    report_date DATE DEFAULT CURRENT_DATE,
    product_id INTEGER,
    product_name VARCHAR(255),
    total_quantity_sold INTEGER,
    total_revenue DECIMAL(10,2),
    FOREIGN KEY (product_id) REFERENCES products(id)
);

CREATE TABLE report_low_stock (
    id SERIAL PRIMARY KEY,
    report_date DATE DEFAULT CURRENT_DATE,
    product_id INTEGER,
    product_name VARCHAR(255),
    qtd INTEGER,
    FOREIGN KEY (product_id) REFERENCES products(id)
);

CREATE TABLE report_user_activity (
    id SERIAL PRIMARY KEY,
    report_date DATE DEFAULT CURRENT_DATE,
    user_id INTEGER,
    user_name VARCHAR(255),
    activity_type VARCHAR(50),
    activity_date TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE SET NULL
);

CREATE TABLE report_log_reservations (
    id SERIAL PRIMARY KEY,
    report_date DATE DEFAULT CURRENT_DATE,
    reservation_id INTEGER,
    action VARCHAR(50),
    action_date TIMESTAMP,
    performed_by INTEGER,
    FOREIGN KEY (reservation_id) REFERENCES reservations(id) ON DELETE SET NULL,
    FOREIGN KEY (performed_by) REFERENCES users(id) ON DELETE SET NULL
);

CREATE TABLE report_log_sales (
    id SERIAL PRIMARY KEY,
    report_date DATE DEFAULT CURRENT_DATE,
    sale_id INTEGER,
    action VARCHAR(50),
    action_date TIMESTAMP,
    performed_by INTEGER,
    details JSONB,
    FOREIGN KEY (sale_id) REFERENCES sales(id) ON DELETE SET NULL,
    FOREIGN KEY (performed_by) REFERENCES users(id) ON DELETE SET NULL
);

-- TRIGGERS E FUNÇÕES

-- Log de Reservas
CREATE OR REPLACE FUNCTION log_reservation_action()
RETURNS TRIGGER AS $$
BEGIN
    IF TG_OP = 'INSERT' THEN
        INSERT INTO log_reservations (id_reservation, action, performed_by)
        VALUES (NEW.id, 'INSERT', NEW.id_visitor);
    ELSIF TG_OP = 'UPDATE' THEN
        INSERT INTO log_reservations (id_reservation, action, performed_by)
        VALUES (NEW.id, 'UPDATE', NEW.id_visitor);
    ELSIF TG_OP = 'DELETE' THEN
        INSERT INTO log_reservations (id_reservation, action, performed_by)
        VALUES (OLD.id, 'DELETE', OLD.id_visitor);
    END IF;
    RETURN NULL;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_log_reservation
AFTER INSERT OR UPDATE ON reservations
FOR EACH ROW EXECUTE FUNCTION log_reservation_action();

CREATE TRIGGER trg_log_reservation_delete
AFTER DELETE ON reservations
FOR EACH ROW EXECUTE FUNCTION log_reservation_action();

-- Log de Vendas
CREATE OR REPLACE FUNCTION log_sale_action()
RETURNS TRIGGER AS $$
BEGIN
    IF TG_OP = 'INSERT' THEN
        INSERT INTO log_sales (id_sale, action, performed_by, details)
        VALUES (NEW.id, 'INSERT', NEW.id_employee, NULL);
    ELSIF TG_OP = 'UPDATE' THEN
        INSERT INTO log_sales (id_sale, action, performed_by, details)
        VALUES (NEW.id, 'UPDATE', NEW.id_employee, NULL);
    ELSIF TG_OP = 'DELETE' THEN
        INSERT INTO log_sales (id_sale, action, performed_by, details)
        VALUES (OLD.id, 'DELETE', OLD.id_employee, NULL);
    END IF;
    RETURN NULL;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_log_sale
AFTER INSERT OR UPDATE OR DELETE ON sales
FOR EACH ROW EXECUTE FUNCTION log_sale_action();

-- Log de Atividade do Usuário
CREATE OR REPLACE FUNCTION log_user_activity_func()
RETURNS TRIGGER AS $$
BEGIN
    INSERT INTO log_user_activity (id_user, activity_type)
    VALUES (NEW.id, TG_OP);
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_log_user_activity
AFTER INSERT OR UPDATE ON users
FOR EACH ROW EXECUTE FUNCTION log_user_activity_func();

-- Atualiza status do quiosque ao reservar
CREATE OR REPLACE FUNCTION update_kiosk_status_on_reservation()
RETURNS TRIGGER AS $$
BEGIN
    UPDATE kiosks SET status = 'Ocupado' WHERE id = NEW.id_kiosk;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_update_kiosk_status
AFTER INSERT ON reservations
FOR EACH ROW EXECUTE FUNCTION update_kiosk_status_on_reservation();

-- Libera quiosque ao deletar reserva
CREATE OR REPLACE FUNCTION update_kiosk_status_on_reservation_delete()
RETURNS TRIGGER AS $$
BEGIN
    UPDATE kiosks SET status = 'Disponível' WHERE id = OLD.id_kiosk;
    RETURN OLD;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_update_kiosk_status_on_reservation_delete
AFTER DELETE ON reservations
FOR EACH ROW EXECUTE FUNCTION update_kiosk_status_on_reservation_delete();

-- Atualiza estoque após venda
CREATE OR REPLACE FUNCTION update_product_stock_on_sale()
RETURNS TRIGGER AS $$
BEGIN
    UPDATE products SET qtd = qtd - NEW.qtd
    WHERE id = NEW.id_product;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_update_product_stock_on_sale
AFTER INSERT ON items_sale
FOR EACH ROW EXECUTE FUNCTION update_product_stock_on_sale();

-- Atualiza data de modificação do funcionário
CREATE OR REPLACE FUNCTION update_employee_last_modified_date()
RETURNS TRIGGER AS $$
BEGIN
    IF NEW.role IS DISTINCT FROM OLD.role
        OR NEW.salary IS DISTINCT FROM OLD.salary THEN
        UPDATE employees SET admission_date = CURRENT_DATE WHERE id = NEW.id;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_update_employee_last_modified_date
AFTER UPDATE ON employees
FOR EACH ROW EXECUTE FUNCTION update_employee_last_modified_date();
