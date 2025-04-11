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
    id_user_type INTEGER NOT NULL REFERENCES user_type(id)
);

CREATE TABLE employees (
    id SERIAL PRIMARY KEY,
    id_user INTEGER REFERENCES users(id) ON DELETE CASCADE,
    role VARCHAR(255),
    salary DECIMAL(10,2),
    admission_date DATE
);

CREATE TABLE visitors (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    cpf VARCHAR(14) UNIQUE,
    age DATE,
    id_user INTEGER REFERENCES users(id) ON DELETE SET NULL
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
    id_visitor INTEGER NOT NULL REFERENCES visitors(id) ON DELETE CASCADE,
    id_kiosk INTEGER NOT NULL REFERENCES kiosk(id) ON DELETE SET NULL
);

CREATE TABLE products (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    price DECIMAL(10,2),
    qtd INTEGER
);

CREATE TABLE sales (
    id SERIAL PRIMARY KEY,
    id_employee INTEGER NOT NULL REFERENCES employees(id_user) ON DELETE SET NULL,
    sale_date DATE NOT NULL,
    total_value DECIMAL(10,2)
);

CREATE TABLE items_sale (
    id SERIAL PRIMARY KEY,
    id_sale INTEGER NOT NULL REFERENCES sales(id) ON DELETE CASCADE,
    id_product INTEGER NOT NULL REFERENCES products(id) ON DELETE SET NULL,
    qtd INTEGER NOT NULL
);

CREATE TABLE report (
    id SERIAL PRIMARY KEY,
    type VARCHAR(255),
    generation_date DATE
);

CREATE TABLE log_reservations (
    id SERIAL PRIMARY KEY,
    id_reservation INTEGER REFERENCES reservations(id),
    action VARCHAR(50), 
    action_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    performed_by INTEGER REFERENCES users(id), 
);

CREATE TABLE log_sales (
    id SERIAL PRIMARY KEY,
    id_sale INTEGER REFERENCES sales(id),
    action VARCHAR(50), 
    action_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    performed_by INTEGER REFERENCES users(id),
    details JSONB 
);

CREATE TABLE log_user_activity (
    id SERIAL PRIMARY KEY,
    id_user INTEGER REFERENCES users(id),
    activity_type VARCHAR(50),
    activity_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
);
