CREATE DATABASE IF NOT EXISTS pos;
USE pos;
CREATE TABLE IF NOT EXISTS productos(codigo VARCHAR(20) NOT NULL UNIQUE, nombre VARCHAR(255) NOT NULL, precio FLOAT(6,2) UNSIGNED NOT NULL);
INSERT INTO productos (codigo, nombre, precio) VALUES
("0","Iphone 13","199.99"),
("1","Laptop","155.55"),
("2","Mouse","14.55"),
("3","CPU","500.00"),
("4","Monitor","468.58"),
("5","Monster","34.59"),
("6","Coca Cola","19.95"),
("7","Sabritones","65.66"),
("8","Tarjeta del Yugio","100.10"),
("9","Agua Purificada","13.99");