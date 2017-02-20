--                 
-- dont do this! the best way is XML.
--

USE auditdb;

CREATE TABLE `budget_levels` (
  `_id` tinyint NOT NULL AUTO_INCREMENT,
  `name` varchar(13) DEFAULT NULL,
  PRIMARY KEY (`_id`)
) ENGINE=MyISAM;


CREATE TABLE `violations_names` (
  `_id` tinyint NOT NULL AUTO_INCREMENT,
  `name` varchar(82) DEFAULT NULL,
  PRIMARY KEY (`_id`)
) ENGINE=MyISAM;

CREATE TABLE `eliminations_names` (
  `_id` tinyint NOT NULL AUTO_INCREMENT,
  `name` varchar(41) DEFAULT NULL,
  PRIMARY KEY (`_id`)
) ENGINE=MyISAM;

CREATE TABLE `total_info_data_names` (
  `_id` tinyint NOT NULL AUTO_INCREMENT,
  `name` varchar(32) DEFAULT NULL,
  PRIMARY KEY (`_id`)
) ENGINE=MyISAM;


INSERT INTO `budget_levels` (name) VALUES
('Федеральный'),
('Региональный'),
('Местный'),
('Прочее');

INSERT INTO `violations_names` (name) VALUES
('Недостача денежных средств'),
('Недостача ТМЦ'),
('Излишки денежных средств'),
('Излишки ТМЦ'),
('Нецелевые расходы'),
('Неэффективное использование бюджетных средств'),
('Неправильное списание, расходование денежных средств'),
('Неправильное списание, расходование материальных запасов, в том числе ГСМ'),
('Неправильная выплата (переплата) зарплаты, авансов, премий, отпускных, мат.помощи'),
('Недоплата зарплаты, премий, мат.помощи'),
('Недопоступление доходов'),
('Завышение СМР'),
('Прочие Финансовые нарушения'),
('Прочие Нефинансовые нарушения'),
('Нарушения Федерального закона №94-ФЗ'),
('Общий объем проверенных средств');

INSERT INTO `eliminations_names` (name) VALUES
('Устранено нарушений: зачет нецелевых'),
('Устранено нарушений: возмещено в бюджеты'),
('Внесены изменения в РЦП'),
('Неустранимые'),
('Не устранено'),
('Налож штрафы'),
('Уплачены штрафы');


INSERT INTO `total_info_data_names` (name) VALUES
('Общий объем проверенных средств'),
('Общая сумма нарушений'),
('В том числе, нецелевые'),
('Финансовые нарушения'),
('Прочие Нефинансовые нарушения'),
('Нарушения  94-ФЗ'),
('Устранено нарушений'),
('Неустранимые'),
('Не устранено'),
('Зачет нецелевых'),
('Возмещено в бюджет'),
('Наложено штрафов'),
('Уплачено штрафов');

