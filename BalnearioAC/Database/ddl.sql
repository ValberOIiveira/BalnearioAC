CREATE DATABASE Resort;

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
    qtd INTEGER
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

CREATE TABLE report (
    id SERIAL PRIMARY KEY,
    type VARCHAR(255),
    generation_date DATE
);

CREATE TABLE log_reservations (
    id SERIAL PRIMARY KEY,
    id_reservation INTEGER,
    action VARCHAR(50), 
    action_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    performed_by INTEGER,
    FOREIGN KEY (id_reservation) REFERENCES reservations(id),
    FOREIGN KEY (performed_by) REFERENCES users(id)
);

CREATE TABLE log_sales (
    id SERIAL PRIMARY KEY,
    id_sale INTEGER,
    action VARCHAR(50), 
    action_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    performed_by INTEGER,
    details JSONB,
    FOREIGN KEY (id_sale) REFERENCES sales(id),
    FOREIGN KEY (performed_by) REFERENCES users(id)
);

CREATE TABLE log_user_activity (
    id SERIAL PRIMARY KEY,
    id_user INTEGER,
    activity_type VARCHAR(50),
    activity_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_user) REFERENCES users(id)
);

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
    FOREIGN KEY (user_id) REFERENCES users(id)
);

CREATE TABLE report_log_reservations (
    id SERIAL PRIMARY KEY,
    report_date DATE DEFAULT CURRENT_DATE,
    reservation_id INTEGER,
    action VARCHAR(50),
    action_date TIMESTAMP,
    performed_by INTEGER,
    FOREIGN KEY (reservation_id) REFERENCES reservations(id),
    FOREIGN KEY (performed_by) REFERENCES users(id)
);

CREATE TABLE report_log_sales (
    id SERIAL PRIMARY KEY,
    report_date DATE DEFAULT CURRENT_DATE,
    sale_id INTEGER,
    action VARCHAR(50),
    action_date TIMESTAMP,
    performed_by INTEGER,
    details JSONB,
    FOREIGN KEY (sale_id) REFERENCES sales(id),
    FOREIGN KEY (performed_by) REFERENCES users(id)
);
