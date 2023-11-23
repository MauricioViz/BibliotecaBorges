CREATE DATABASE BibliotecaBD
USE BibliotecaBD

select * from TB_USUARIO
select * from sys.key_constraints

/* Llamar tablas*/
select * from tb_autor
select * from TB_AUTOR_LIBRO
select * from tb_editorial
select * from TB_EDITORIAL_LIBRO
select * from tb_libro
select * from tb_prestamo
select * from tb_prestamo_libro
select * from tb_usuario

go
create procedure carrito
AS 
BEGIN
select l.idLibro, l.tituloLibro, l.generoLibro, pl.cantidad
from TB_LIBRO l inner join TB_PRESTAMO_LIBRO pl
on l.idLibro = pl.idLibro 
end
exec carrito

CREATE TABLE TB_AUTOR
(
idAutor char(5) not null PRIMARY KEY,
nomAutor varchar(20) not null,
nacionalidad varchar(20) not null,
fechaNacimiento varchar(11)
)



CREATE TABLE TB_EDITORIAL
(
idEditorial char(5) not null PRIMARY KEY,
nomEditorial varchar(20) not null,
dirEditorial varchar(40) not null,
tlfEditorial varchar(9) not null
)

CREATE TABLE TB_LIBRO
(
idLibro char(5) not null PRIMARY KEY,
tituloLibro varchar(30) not null,
generoLibro varchar(15) not null,
fecPublic varchar(11),
estLibro varchar(10) not null
)

CREATE TABLE TB_USUARIO
(
idUsuario char(5) not null PRIMARY KEY,
nomUsuario varchar(20) not null,
tlfUsuario varchar(9),
correoUsuario varchar(30) not null,
tipoUsuario varchar(10) not null,
passUsuario varchar (20) not null
)

CREATE TABLE TB_PRESTAMO
(
idPrestamo char(5) not null PRIMARY KEY,
idUsuario char(5) not null REFERENCES TB_USUARIO,
fecPrestamo varchar(11) not null,
fecDevolucion varchar(11) not null,
estPrestamo varchar(10) not null,
multaPrestamo float not null
)



CREATE TABLE TB_EDITORIAL_LIBRO
(
idRel char(5) not null PRIMARY KEY, /* Testing */
idEditorial char(5) not null REFERENCES TB_EDITORIAL,
idLibro char(5) not null REFERENCES TB_LIBRO

) 


CREATE TABLE TB_AUTOR_LIBRO
(
idRel char(5) not null PRIMARY KEY, /* Testing */
idAutor char(5) not null REFERENCES TB_AUTOR,
idLibro char(5) not null REFERENCES TB_LIBRO

) 


CREATE TABLE TB_PRESTAMO_LIBRO
(
idRel char(5) not null PRIMARY KEY, /* Testing */
idPrestamo char(5) not null REFERENCES TB_PRESTAMO,
idLibro char(5) not null REFERENCES TB_LIBRO,
cantidad int not null
)




INSERT INTO TB_AUTOR (idAutor, nomAutor, nacionalidad, fechaNacimiento) VALUES
('1', 'William Shakespeare', 'Inglés', '1564-04-26'),
('2', 'Jane Austen', 'Inglés', '1775-12-16'),
('3', 'Fyodor Dostoevsky', 'Ruso', '1821-11-11'),
('4', 'Gabriel García', 'Colombiano', '1927-03-06'),
('5', 'Haruki Murakami', 'Japonés', '1949-01-12'),
('6', 'George Orwell', 'Inglés', '1903-06-25'),
('7', 'Agatha Christie', 'Inglés', '1890-09-15'),
('8', 'Toni Morrison', 'Estadounidense', '1931-02-18'),
('9', 'J.K. Rowling', 'Británico', '1965-07-31'),
('10', 'Leo Tolstoy', 'Ruso', '1828-09-09');


INSERT INTO tb_libro (idLibro, tituloLibro, generoLibro, fecPublic, estLibro) VALUES
('1', 'Hamlet', 'Tragedia', '1600-01-01', 'Disponible'),
('2', 'Pride and Prejudice', 'Novela', '1813-01-28', 'Disponible'),
('3', 'Crime and Punishment', 'Novela', '1866-01-01', 'Disponible'),
('4', 'One Hundred Years of Solitude', 'Realismo mágico', '1967-05-30', 'Disponible'),
('5', 'Norwegian Wood', 'Novela', '1987-08-20', 'Disponible'),
('6', '1984', 'Ciencia ficción', '1949-06-08', 'Disponible'),
('7', 'Murder on the Orient Express', 'Misterio', '1934-01-01', 'Disponible'),
('8', 'Beloved', 'Novela', '1987-09-02', 'Disponible'),
('9', 'Harry Potter', 'Fantasía', '1997-06-26', 'Disponible'),
('10', 'War and Peace', 'Novela', '1869-01-01', 'Disponible'),
('11', 'Romeo and Juliet', 'Tragedia', '1597-01-01', 'Disponible'),
('12', 'Sense and Sensibility', 'Novela', '1811-01-01', 'Disponible'),
('13', 'The Brothers Karamazov', 'Novela', '1880-01-01', 'Disponible'),
('14', 'Love in the Time of Cholera', 'Realismo mágico', '1985-03-30', 'Disponible'),
('15', 'Kafka on the Shore', 'Novela', '2002-01-01', 'Disponible'),
('16', 'Animal Farm', 'Sátira política', '1945-08-17', 'Disponible'),
('17', 'The Murder of Roger Ackroyd', 'Misterio', '1926-01-01', 'Disponible'),
('18', 'Song of Solomon', 'Novela', '1977-01-01', 'Disponible'),
('19', 'Anna Karenina', 'Novela', '1877-01-01', 'Disponible');

select * from tb_libro


INSERT INTO tb_autor_libro (idRel, idAutor, idLibro) VALUES
('1','1', '1'),
('2','2', '2'),
('3','3', '3'),
('4','4', '4'),
('5','5', '5'),
('6','6', '6'),
('7','7', '7'),
('8','8', '8'),
('9','9', '9'),
('10','10', '10'),
('11','1', '11'),
('12','2', '12'),
('13','3', '13'),
('14','4', '14'),
('15','5', '15'),
('16','6', '16'),
('17','7', '17'),
('18','8', '18'),
('19','9', '19');

INSERT INTO tb_autor_libro (idRel, idAutor, idLibro) VALUES
('20','2', '1');




INSERT INTO tb_editorial (idEditorial, nomEditorial, dirEditorial, tlfEditorial) VALUES
('1', 'Editorial Shakes', '123 Main Street, Londres', '123456789'),
('2', 'Editorial Austen', '456 Elm Street, Bath', '987654321'),
('3', 'Editorial Dostoevsky', '789 Oak Street, San Petersburgo', '555555555'),
('4', 'Editorial Márquez', '101 Calle Real, Bogotá', '777777777'),
('5', 'Editorial Murakami', '555 Sakura Avenue, Tokio', '888888888'),
('6', 'Editorial Orwell', '1984 Surveillance Lane, Londres', '999999999'),
('7', 'Editorial Christie', '7 Orient Express Road, Inglaterra', '111111111'),
('8', 'Editorial Morrison', '100 Beloved Place, Ohio', '222222222'),
('9', 'Editorial Rowling', '11 Hogwarts Street, Londres', '333333333'),
('10', 'Editorial Tolstoy', '1869 War and Peace Road, Moscú', '444444444');



INSERT INTO tb_editorial_libro (idRel, idEditorial, idLibro) VALUES
('1','1', '1'),
('2','2', '2'),
('3','3', '3'),
('4','4', '4'),
('5','5', '5'),
('6','6', '6'),
('7','7', '7'),
('8','8', '8'),
('9','9', '9'),
('10','10', '10'),
('11','1', '11'),
('12','2', '12'),
('13','3', '13'),
('14','4', '14'),
('15','5', '15'),
('16','6', '16'),
('17','7', '17'),
('18','8', '18'),
('19','9', '19')



INSERT INTO tb_usuario (idUsuario, nomUsuario, tlfUsuario, correoUsuario, tipoUsuario, passUsuario) VALUES
('0','Administrador1','942331516','administrador@gmail.com','Empleado','password'),
('1', 'Juan Pérez', '987478514', 'juan@gmail.com', 'Cliente', 'juan.perez'),
('2', 'María López', '987654321', 'maria@gmail.com', 'Cliente','maria.lopez'),
('3', 'John Smith', '924458745', 'john@gmail.com', 'Cliente','john.smith'),
('4', 'Luisa Fernández', '956875789', 'luisa@gmail.com', 'Cliente','luisa.fernandez'),
('5', 'Ahmed Khan', '935478459', 'ahmed@gmail.com', 'Cliente','ahmed.khan'),
('6', 'Sophie Dubois', '997411589', 'sophie@gmail.com', 'Cliente','sophie.dubois'),
('7', 'Elena Petrov', '941546784', 'elena@gmail.com', 'Cliente','elena.petrov'),
('8', 'Ravi Gupta', '947845126', 'ravi@gmail.com', 'Cliente','ravi.gupta'),
('9', 'Anna Schmidt', '947845164', 'anna@gmail.com', 'Cliente','anna.schmidt'),
('10', 'Carlos Fernández', '947847458', 'carlos@gmail.com', 'Cliente','carlos.fernandez');




INSERT INTO tb_prestamo (idPrestamo, idUsuario, fecPrestamo, fecDevolucion, estPrestamo, multaPrestamo) VALUES
('1', '1', '2023-10-01', '2023-10-15', 'Prestado', '0.00'),
('2', '2', '2023-10-02', '2023-10-16', 'Prestado', '0.00'),
('3', '3', '2023-10-03', '2023-10-17', 'Prestado', '0.00'),
('4', '4', '2023-10-04', '2023-10-18', 'Prestado', '0.00'),
('5', '5', '2023-10-05', '2023-10-19', 'Prestado', '0.00'),
('6', '6', '2023-10-06', '2023-10-20', 'Prestado', '0.00'),
('7', '7', '2023-10-07', '2023-10-21', 'Prestado', '0.00'),
('8', '1', '2023-10-08', '2023-10-22', 'Prestado', '0.00'),
('9', '2', '2023-10-09', '2023-10-23', 'Prestado', '0.00'),
('10', '3', '2023-10-10', '2023-10-24', 'Prestado', '0.00'),
('11', '4', '2023-10-11', '2023-10-25', 'Prestado', '0.00'),
('12', '5', '2023-10-12', '2023-10-26', 'Prestado', '0.00'),
('13', '6', '2023-10-13', '2023-10-27', 'Prestado', '0.00'),
('14', '7', '2023-10-14', '2023-10-28', 'Prestado', '0.00'),
('15', '1', '2023-10-15', '2023-10-29', 'Prestado', '0.00');



INSERT INTO tb_prestamo_libro (idRel, idPrestamo, idLibro, cantidad) VALUES
('1','1', '1', 1),
('2','2', '2', 1),
('3','3', '3', 1),
('4','4', '4', 1),
('5','5', '5', 1),
('6','6', '6', 1),
('7','7', '7', 1),
('8','8', '8', 1),
('9','9', '9', 1),
('10','10', '10', 1),
('11','11', '1', 1),
('12','12', '2', 1),
('13','13', '3', 1),
('14','14', '4', 1),
('15','15', '5', 1);

select * from tb_prestamo_libro
