-- bd_des104_d2
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 02-09-2026 a las 08:17:15
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

CREATE DATABASE IF NOT EXISTS bd_des104_d2;
USE bd_des104_d2;

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `bd_des104_d2`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `eventos`
--

CREATE TABLE `eventos` (
  `id_evento` int(11) NOT NULL,
  `nombre` varchar(100) NOT NULL CHECK (char_length(`nombre`) between 5 and 100),
  `fecha` date NOT NULL,
  `lugar` varchar(100) NOT NULL CHECK (char_length(`lugar`) between 5 and 100),
  `fecha_creacion` timestamp NOT NULL DEFAULT current_timestamp()
) ;

--
-- Volcado de datos para la tabla `eventos`
--

INSERT INTO `eventos` (`id_evento`, `nombre`, `fecha`, `lugar`, `fecha_creacion`) VALUES
(1, 'Conferencia de Tecnología 2026', '2026-10-15', 'Centro Comercial las Palmas piso 3', '2026-09-02 06:13:45'),
(2, 'Taller de Desarrollo Web', '2026-11-05', 'Hotel Crown', '2026-09-02 06:13:45'),
(3, 'Evento Navideño', '2026-12-20', 'Cifco San Salvador', '2026-09-02 06:13:45'),
(4, 'Seminario de Marketing Digital', '2027-01-18', 'Hotel Sheraton', '2026-09-02 06:13:45');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `organizadores`
--

CREATE TABLE `organizadores` (
  `id_organizador` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL CHECK (char_length(`nombre`) between 3 and 50),
  `cargo` varchar(50) NOT NULL CHECK (char_length(`cargo`) between 3 and 50),
  `id_evento` int(11) NOT NULL,
  `fecha_creacion` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Volcado de datos para la tabla `organizadores`
--

INSERT INTO `organizadores` (`id_organizador`, `nombre`, `cargo`, `id_evento`, `fecha_creacion`) VALUES
(1, 'María García', 'Coordinadora General', 1, '2026-09-02 06:13:45'),
(2, 'Carlos Rodríguez', 'Director Técnico', 1, '2026-09-02 06:13:45'),
(3, 'Laura Martínez', 'Responsable de Logística', 2, '2026-09-02 06:13:45'),
(4, 'Pedro Sánchez', 'Coordinador de Talleres', 3, '2026-09-02 06:13:45'),
(5, 'Ana Fernández', 'Directora de Marketing', 4, '2026-09-02 06:13:45'),
(6, 'Jorge López', 'Coordinador de Ponentes', 4, '2026-09-02 06:13:45');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `participantes`
--

CREATE TABLE `participantes` (
  `id_participante` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL CHECK (char_length(`nombre`) between 3 and 50),
  `email` varchar(100) NOT NULL CHECK (`email` like '%@%'),
  `id_evento` int(11) NOT NULL,
  `fecha_inscripcion` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Volcado de datos para la tabla `participantes`
--

INSERT INTO `participantes` (`id_participante`, `nombre`, `email`, `id_evento`, `fecha_inscripcion`) VALUES
(1, 'Juan Pérez', 'juan.perez@empresa.com', 1, '2026-09-02 06:13:45'),
(2, 'Marta Ruiz', 'marta.ruiz@empresa.es', 1, '2026-09-02 06:13:45'),
(3, 'Luis Gómez', 'luis.gomez@empresa.com', 1, '2026-09-02 06:13:45'),
(4, 'Sofía Martín', 'sofia.martin@empresa.com', 1, '2026-09-02 06:13:45'),
(5, 'Alberto Díaz', 'alberto.diaz@taller.com', 2, '2026-09-02 06:13:45'),
(6, 'Elena Torres', 'elena.torres@taller.com', 2, '2026-09-02 06:13:45'),
(7, 'Miguel Ángel', 'miguel.angel@taller.es', 2, '2026-09-02 06:13:45'),
(8, 'Cristina Ruiz', 'cristina.ruiz@navi.com', 3, '2026-09-02 06:13:45'),
(9, 'Fernando Garrido', 'fernando.garrido@navi.com', 3, '2026-09-02 06:13:45'),
(10, 'Isabel Herrera', 'isabel.herrera@navi.com', 3, '2026-09-02 06:13:45'),
(11, 'David López', 'david.lopez@navi.es', 3, '2026-09-02 06:13:45'),
(12, 'Patricia Delgado', 'patricia.delgado@semma.com', 4, '2026-09-02 06:13:45'),
(13, 'Roberto Núñez', 'roberto.nunez@semma.com', 4, '2026-09-02 06:13:45');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `eventos`
--
ALTER TABLE `eventos`
  ADD PRIMARY KEY (`id_evento`);

--
-- Indices de la tabla `organizadores`
--
ALTER TABLE `organizadores`
  ADD PRIMARY KEY (`id_organizador`),
  ADD KEY `id_evento` (`id_evento`);

--
-- Indices de la tabla `participantes`
--
ALTER TABLE `participantes`
  ADD PRIMARY KEY (`id_participante`),
  ADD UNIQUE KEY `uk_email_evento` (`email`,`id_evento`),
  ADD KEY `id_evento` (`id_evento`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `eventos`
--
ALTER TABLE `eventos` AUTO_INCREMENT = 5;

--
-- AUTO_INCREMENT de la tabla `organizadores`
--
ALTER TABLE `organizadores` AUTO_INCREMENT = 7;

--
-- AUTO_INCREMENT de la tabla `participantes`
--
ALTER TABLE `participantes` AUTO_INCREMENT = 14;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `organizadores`
--
ALTER TABLE `organizadores`
  ADD CONSTRAINT `organizadores_ibfk_1` FOREIGN KEY (`id_evento`) REFERENCES `eventos` (`id_evento`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `participantes`
--
ALTER TABLE `participantes`
  ADD CONSTRAINT `participantes_ibfk_1` FOREIGN KEY (`id_evento`) REFERENCES `eventos` (`id_evento`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
