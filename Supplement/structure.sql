CREATE DATABASE `auditdb` DEFAULT CHARSET=utf8;
USE auditdb;

--
-- Foreign key checks OFF 
--

SET foreign_key_checks = 0; 


CREATE TABLE `reasons` (
  `_id` tinyint NOT NULL AUTO_INCREMENT,
  `name` varchar(94) DEFAULT NULL,
  PRIMARY KEY (`_id`)
) ENGINE=InnoDB, AUTO_INCREMENT = 0;

INSERT INTO `reasons` (name) VALUES
('Основание проверки не выбрано'),
('Плановая проверка'),
('По заданию (постановлению, распоряжению) Правительства РТ'),
('По обращению правоохранительных органов'),
('По обращениям граждан, включая обращения должностных лиц и бюджетных учреждений и организаций');

--
-- Table structure for table `inspector`
--

CREATE TABLE `inspectors` (
  `_id` int(11) NOT NULL AUTO_INCREMENT,
  `surname` varchar(45) DEFAULT NULL,
  `name` varchar(45) DEFAULT NULL,
  `patronymic` varchar(45) DEFAULT NULL,
  `pwdhash` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`_id`)
) ENGINE=InnoDB;

--
-- Table structure for table `inspection`
--

CREATE TABLE `inspections` (
  `_id` int(11) NOT NULL AUTO_INCREMENT,
  `protocol` VARCHAR(16) DEFAULT NULL,
  `name` text,
  `_date` date NOT NULL,
  `reason` tinyint,
  `director` int(11),
  `period_begin` date NOT NULL,
  `period_end` date NOT NULL,
  `_comment` text,
  `modif_inspector` int(11) NOT NULL,
  `modif_datetime` datetime NOT NULL,
  PRIMARY KEY (`_id`),
  UNIQUE KEY `protocol_UNIQUE` (`protocol`),
  KEY `fk_inspections_director` (`director`),
  KEY `fk_inspections_modif_inspector` (`modif_inspector`),
  KEY `fk_inspections_reason` (`reason`),
  CONSTRAINT `fk_inspections_director` FOREIGN KEY (`director`) REFERENCES `inspectors` (`_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_inspections_modif_inspector` FOREIGN KEY (`modif_inspector`) REFERENCES `inspectors` (`_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_inspections_reason` FOREIGN KEY (`reason`) REFERENCES `reasons` (`_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB;

--
-- Table structure for table `violation`
--

CREATE TABLE `violations` (
  `inspection` int(11) NOT NULL,
  `budget_lvl` tinyint NOT NULL,
  `_year` year(4) NOT NULL,
  `d1` DOUBLE DEFAULT NULL,
  `d2` DOUBLE DEFAULT NULL,
  `d3` DOUBLE DEFAULT NULL,
  `d4` DOUBLE DEFAULT NULL,
  `d5` DOUBLE DEFAULT NULL,
  `d6` DOUBLE DEFAULT NULL,
  `d7` DOUBLE DEFAULT NULL,
  `d8` DOUBLE DEFAULT NULL,
  `d9` DOUBLE DEFAULT NULL,
  `d10` DOUBLE DEFAULT NULL,
  `d11` DOUBLE DEFAULT NULL,
  `d12` DOUBLE DEFAULT NULL,
  `d13` DOUBLE DEFAULT NULL,
  `d14` DOUBLE DEFAULT NULL,
  `d15` DOUBLE DEFAULT NULL,
  `d16` DOUBLE DEFAULT NULL,
  PRIMARY KEY (`inspection`,`budget_lvl`,`_year`),
  KEY `fk_violations_inspection` (`inspection`),
  CONSTRAINT `fk_violations_inspection` FOREIGN KEY (`inspection`) REFERENCES `inspections` (`_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB;

--
-- Table structure for table `elimination`
--

CREATE TABLE `eliminations` (
  `inspection` int(11) NOT NULL,
  `budget_lvl` tinyint NOT NULL,
  `d1` DOUBLE DEFAULT NULL,
  `d2` DOUBLE DEFAULT NULL,
  `d3` DOUBLE DEFAULT NULL,
  `d4` DOUBLE DEFAULT NULL,
  `d5` DOUBLE DEFAULT NULL,
  `d6` DOUBLE DEFAULT NULL,
  `d7` DOUBLE DEFAULT NULL,
  PRIMARY KEY (`inspection`,`budget_lvl`),
  KEY `fk_eliminations_inspection` (`inspection`),
  CONSTRAINT `fk_eliminations_inspection` FOREIGN KEY (`inspection`) REFERENCES `inspections` (`_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB;

--
-- Table structure for table `inspection_group`
--

CREATE TABLE `inspection_group` (
  `inspection` int(11) NOT NULL,
  `inspector` int(11) NOT NULL,
  PRIMARY KEY (`inspection`,`inspector`),
  KEY `fk_inspection_group_inspection` (`inspection`),
  KEY `fk_inspection_group_inspector` (`inspector`),
  CONSTRAINT `fk_inspection_group_inspection` FOREIGN KEY (`inspection`) REFERENCES `inspections` (`_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_inspection_group_inspector` FOREIGN KEY (`inspector`) REFERENCES `inspectors` (`_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB;


--
-- Foreign key checks ON 
--

SET foreign_key_checks = 1; 






















