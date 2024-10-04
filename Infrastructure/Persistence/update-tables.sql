ALTER TABLE users
ADD COLUMN status ENUM('ACTIVE', 'INACTIVE', 'PENDING') NOT NULL DEFAULT 'ACTIVE';

CREATE TABLE material (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    imageUrl VARCHAR(255),
    color VARCHAR(7) NOT NULL
);

INSERT INTO material (name, color) VALUES
('Latas', '#EF9D38'),
('Papel y cartón', '#1935F4'),
('Plástico PET', '#FEFB54'),
('Plástico otros', '#FEFB54'),
('Envases y botellas de vidrio', '#3A7D25'),
('Multicapa', '#4D4D4D');


CREATE TABLE role (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    description VARCHAR(255) NOT NULL
);

INSERT INTO role (name, description) VALUES
('A', 'usuario de tipo admin'),
('E', 'usuario gestor de recicladores o acopiadores'),
('U', 'usuario de tipo acopiador');


CREATE TABLE departamentos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(255) NOT NULL
);

CREATE TABLE municipios (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(255) NOT NULL,
    departamento_id INT,
    FOREIGN KEY (departamento_id) REFERENCES departamentos(id) ON DELETE CASCADE
);

-- Insertar departamentos
INSERT INTO departamentos (nombre) VALUES ('Alta Verapaz');
INSERT INTO departamentos (nombre) VALUES ('Baja Verapaz');
INSERT INTO departamentos (nombre) VALUES ('Chimaltenango');
INSERT INTO departamentos (nombre) VALUES ('Chiquimula');
INSERT INTO departamentos (nombre) VALUES ('El Progreso');
INSERT INTO departamentos (nombre) VALUES ('Escuintla');
INSERT INTO departamentos (nombre) VALUES ('Guatemala');
INSERT INTO departamentos (nombre) VALUES ('Huehuetenango');
INSERT INTO departamentos (nombre) VALUES ('Izabal');
INSERT INTO departamentos (nombre) VALUES ('Jalapa');
INSERT INTO departamentos (nombre) VALUES ('Jutiapa');
INSERT INTO departamentos (nombre) VALUES ('Petén');
INSERT INTO departamentos (nombre) VALUES ('Quetzaltenango');
INSERT INTO departamentos (nombre) VALUES ('Quiché');
INSERT INTO departamentos (nombre) VALUES ('Retalhuleu');
INSERT INTO departamentos (nombre) VALUES ('Sacatepéquez');
INSERT INTO departamentos (nombre) VALUES ('San Marcos');
INSERT INTO departamentos (nombre) VALUES ('Santa Rosa');
INSERT INTO departamentos (nombre) VALUES ('Sololá');
INSERT INTO departamentos (nombre) VALUES ('Suchitepéquez');
INSERT INTO departamentos (nombre) VALUES ('Totonicapán');
INSERT INTO departamentos (nombre) VALUES ('Zacapa');



-- Insertar municipios de Alta Verapaz
INSERT INTO municipios (nombre, departamento_id) VALUES ('Cahabón', 1);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Chahal', 1);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Chisec', 1);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Cobán', 1);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Fray Bartolomé de las Casas', 1);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Lanquín', 1);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Panzós', 1);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Raxruha', 1);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Cristóbal Verapaz', 1);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Juan Chamelco', 1);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Pedro Carchá', 1);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Santa Cruz Verapaz', 1);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Senahú', 1);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Tactic', 1);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Tamahú', 1);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Tucurú', 1);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Santa Catarina La Tinta', 1);

-- Insertar municipios de Baja Verapaz
INSERT INTO municipios (nombre, departamento_id) VALUES ('Cubulco', 2);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Granados', 2);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Purulhá', 2);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Rabinal', 2);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Salamá', 2);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Jerónimo', 2);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Miguel Chicaj', 2);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Santa Cruz El Chol', 2);


-- Insertar municipios de Chimaltenango
INSERT INTO municipios (nombre, departamento_id) VALUES ('Acatenango', 3);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Chimaltenango', 3);
INSERT INTO municipios (nombre, departamento_id) VALUES ('El Tejar', 3);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Parramos', 3);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Patzicía', 3);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Patzún', 3);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Pochuta', 3);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Andrés Itzapa', 3);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San José Poaquil', 3);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Juan Comalapa', 3);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Martín Jilotepeque', 3);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Santa Apolonia', 3);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Santa Cruz Balanyá', 3);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Tecpán Guatemala', 3);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Yepocapa', 3);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Zaragoza', 3);


-- Insertar municipios de Chiquimula
INSERT INTO municipios (nombre, departamento_id) VALUES ('Camotán', 4);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Chiquimula', 4);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Concepción Las Minas', 4);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Esquipulas', 4);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Ipala', 4);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Jocotán', 4);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Olopa', 4);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Quezaltepeque', 4);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Jacinto', 4);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San José La Arada', 4);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Juan Ermita', 4);

-- Insertar municipios de El Progreso
INSERT INTO municipios (nombre, departamento_id) VALUES ('El Jícaro', 5);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Guastatoya', 5);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Morazán', 5);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Agustín Acasaguastlán', 5);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Antonio La Paz', 5);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Cristóbal Acasaguastlán', 5);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Sanarate', 5);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Sansare', 5);

-- Insertar municipios de Escuintla
INSERT INTO municipios (nombre, departamento_id) VALUES ('Escuintla', 6);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Guanagazapa', 6);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Iztapa', 6);
INSERT INTO municipios (nombre, departamento_id) VALUES ('La Democracia', 6);
INSERT INTO municipios (nombre, departamento_id) VALUES ('La Gomera', 6);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Masagua', 6);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Nueva Concepción', 6);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Palín', 6);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San José', 6);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Vicente Pacaya', 6);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Santa Lucía Cotzumalguapa', 6);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Siquinalá', 6);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Tiquisate', 6);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Sipacate', 6);


-- Insertar municipios de Guatemala
INSERT INTO municipios (nombre, departamento_id) VALUES ('Amatitlán', 7);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Chinautla', 7);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Chuarrancho', 7);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Fraijanes', 7);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Ciudad de Guatemala', 7);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Mixco', 7);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Palencia', 7);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Petapa', 7);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San José del Golfo', 7);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San José Pinula', 7);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Juan Sacatepéquez', 7);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Pedro Ayampuc', 7);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Pedro Sacatepéquez', 7);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Raymundo', 7);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Santa Catarina Pinula', 7);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Villa Canales', 7);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Villa Nueva', 7);

-- Insertar municipios de Huehuetenango
INSERT INTO municipios (nombre, departamento_id) VALUES ('Aguacatán', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Chiantla', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Colotenango', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Concepción Huista', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Cuilco', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Huehuetenango', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Ixtahuacán', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Jacaltenango', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('La Democracia', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('La Libertad', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Malacatán', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Malacatancito', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Nentón', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Antonio Huista', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Gaspar Ixchil', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Juan Atitán', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Juan Ixcoy', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Mateo Ixtatán', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Miguel Acatán', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Pedro Necta', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Rafael La Independencia', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Rafael Petzal', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Sebastián Coatán', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Sebastián Huehuetenango', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Santa Ana Huista', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Santa Bárbara', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Santa Cruz Barillas', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Santa Eulalia', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Santiago Chimaltenango', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Soloma', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Tectitán', 8);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Todos Santos Cuchumatan', 8);

-- Insertar municipios de Izabal
INSERT INTO municipios (nombre, departamento_id)
-- Insertar municipios de Izabal
INSERT INTO municipios (nombre, departamento_id) VALUES ('El Estor', 9);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Livingston', 9);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Los Amates', 9);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Morales', 9);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Puerto Barrios', 9);


-- Insertar municipios de Jalapa
INSERT INTO municipios (nombre, departamento_id) VALUES ('Jalapa', 10);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Mataquescuintla', 10);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Monjas', 10);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Carlos Alzatate', 10);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Luis Jilotepeque', 10);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Pedro Pinula', 10);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Manuel Chaparrón', 10);

-- Insertar municipios de Jutiapa
INSERT INTO municipios (nombre, departamento_id) VALUES ('Agua Blanca', 11);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Asunción Mita', 11);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Atescatempa', 11);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Comapa', 11);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Conguaco', 11);
INSERT INTO municipios (nombre, departamento_id) VALUES ('El Adelanto', 11);
INSERT INTO municipios (nombre, departamento_id) VALUES ('El Progreso', 11);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Jalpatagua', 11);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Jerez', 11);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Jutiapa', 11);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Moyuta', 11);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Pasaco', 11);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Quezada', 11);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San José Acatempa', 11);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Santa Catarina Mita', 11);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Yupiltepeque', 11);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Zapotitlán', 11);

-- Insertar municipios de Petén
INSERT INTO municipios (nombre, departamento_id) VALUES ('Dolores', 12);
INSERT INTO municipios (nombre, departamento_id) VALUES ('El Chal', 12);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Flores', 12);
INSERT INTO municipios (nombre, departamento_id) VALUES ('La Libertad', 12);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Melchor de Mencos', 12);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Poptún', 12);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Andrés', 12);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Benito', 12);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Francisco', 12);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San José', 12);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Luis', 12);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Santa Ana', 12);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Sayaxché', 12);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Las Cruces', 12);

-- Insertar municipios de Quetzaltenango
INSERT INTO municipios (nombre, departamento_id) VALUES ('Almolonga', 13);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Cabricán', 13);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Cajolá', 13);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Cantel', 13);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Coatepeque', 13);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Colomba', 13);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Concepción Chiquirichapa', 13);
INSERT INTO municipios (nombre, departamento_id) VALUES ('El Palmar', 13);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Flores Costa Cuca', 13);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Génova', 13);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Huitán', 13);
INSERT INTO municipios (nombre, departamento_id) VALUES ('La Esperanza', 13);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Olintepeque', 13);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Ostuncalco', 13);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Palestina de Los Altos', 13);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Quetzaltenango', 13);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Salcajá', 13);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Carlos Sija', 13);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Francisco La Unión', 13);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Martín Sacatepéquez', 13);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Mateo', 13);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Miguel Sigüilá', 13);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Sibilia', 13);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Zunil', 13);


-- Insertar municipios de Quiché
INSERT INTO municipios (nombre, departamento_id) VALUES ('Canillá', 14);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Chajul', 14);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Chicamán', 14);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Chiché', 14);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Chichicastenango', 14);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Chinique', 14);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Cunén', 14);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Ixcán', 14);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Joyabaj', 14);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Nebaj', 14);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Pachalum', 14);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Patzité', 14);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Sacapulas', 14);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Andrés Sajcabajá', 14);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Antonio Ilotenango', 14);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Bartolomé Jocotenango', 14);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Juan Cotzal', 14);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Pedro Jocopilas', 14);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Santa Cruz del Quiché', 14);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Uspantán', 14);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Zacualpa', 14);

-- Insertar municipios de Retalhuleu
INSERT INTO municipios (nombre, departamento_id) VALUES ('Champerico', 15);
INSERT INTO municipios (nombre, departamento_id) VALUES ('El Asintal', 15);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Nuevo San Carlos', 15);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Retalhuleu', 15);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Andrés Villa Seca', 15);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Felipe', 15);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Martín Zapotitlán', 15);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Sebastián', 15);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Santa Cruz Muluá', 15);

-- Insertar municipios de Sacatepéquez
INSERT INTO municipios (nombre, departamento_id) VALUES ('Alotenango', 16);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Antigua', 16);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Ciudad Vieja', 16);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Bartolomé Milpas Altas', 16);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Lucas Sacatepéquez', 16);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Miguel Dueñas', 16);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Sumpango', 16);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Tecpán Guatemala', 16);

-- Insertar municipios de San Marcos
INSERT INTO municipios (nombre, departamento_id) VALUES ('Ayutla', 17);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Catarina', 17);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Chiapas', 17);
INSERT INTO municipios (nombre, departamento_id) VALUES ('El Quetzal', 17);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Ixtahuacán', 17);
INSERT INTO municipios (nombre, departamento_id) VALUES ('La Reforma', 17);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Malacatán', 17);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Marquelia', 17);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Municipalidad San Marcos', 17);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Nuevo San Marcos', 17);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Antonio Sacatepéquez', 17);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Lorenzo', 17);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Marcos', 17);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Pablo', 17);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Pedro Sacatepéquez', 17);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Tejutla', 17);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Tierra Colorada', 17);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Villa Nueva', 17);


-- Insertar municipios de Santa Rosa
INSERT INTO municipios (nombre, departamento_id) VALUES ('Barberena', 18);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Cuyotenango', 18);
INSERT INTO municipios (nombre, departamento_id) VALUES ('El Santa Rosa', 18);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Juan Tecuaco', 18);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Rafael las Flores', 18);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Santa Rosa de Lima', 18);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Santa Rosa de Lima', 18);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Tierra Blanca', 18);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Villa Nueva', 18);

-- Insertar municipios de Solalá
INSERT INTO municipios (nombre, departamento_id) VALUES ('Chicacao', 19);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Concepción', 19);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Panajachel', 19);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Antonio Palopó', 19);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San José Chacayá', 19);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Lucas Tolimán', 19);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Santa Catarina Palopó', 19);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Santiago Atitlán', 19);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Solalá', 19);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Tzununa', 19);

-- Insertar municipios de Suchitepéquez
INSERT INTO municipios (nombre, departamento_id) VALUES ('Cuyotenango', 20);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Mazatenango', 20);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Bernardino', 20);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Francisco Zapotitlán', 20);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San José El Nuevo', 20);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Santa Bárbara', 20);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Santo Domingo', 20);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Zunilito', 20);

-- Insertar municipios de Totonicapán
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Cristóbal Totonicapán', 21);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Totonicapán', 21);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Francisco el Alto', 21);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Juan Atitlán', 21);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Tectitán', 21);

-- Insertar municipios de Zacapa
INSERT INTO municipios (nombre, departamento_id) VALUES ('Cabañas', 22);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Chiquimula', 22);
INSERT INTO municipios (nombre, departamento_id) VALUES ('El Jícaro', 22);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Gualán', 22);
INSERT INTO municipios (nombre, departamento_id) VALUES ('La Unión', 22);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Rio Hondo', 22);
INSERT INTO municipios (nombre, departamento_id) VALUES ('San Jorge', 22);
INSERT INTO municipios (nombre, departamento_id) VALUES ('Zacapa', 22);



CREATE TABLE questions (
    id INT AUTO_INCREMENT PRIMARY KEY,
    question TEXT NOT NULL,
    image_url VARCHAR(255),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);


CREATE TABLE answers (
    id INT AUTO_INCREMENT PRIMARY KEY,
    question_id INT NOT NULL,
    text TEXT NOT NULL,
    is_correct BOOLEAN NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (question_id) REFERENCES questions(id) ON DELETE CASCADE
);


INSERT INTO questions (question, image_url)
VALUES ('¿Cuál es la capital de Francia?', 'https://example.com/paris.jpg');

-- Obtener el ID de la pregunta recién insertada
SET @question_id = LAST_INSERT_ID();

-- Insertar respuestas para la pregunta
INSERT INTO answers (question_id, text, is_correct)
VALUES (@question_id, 'París', TRUE),
       (@question_id, 'Londres', FALSE),
       (@question_id, 'Berlín', FALSE),
       (@question_id, 'Madrid', FALSE);
