-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.0.45-community-nt


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema books
--

CREATE DATABASE IF NOT EXISTS books;
USE books;

--
-- Definition of table `bk_bill`
--

DROP TABLE IF EXISTS `bk_bill`;
CREATE TABLE `bk_bill` (
  `bill_id` int(10) unsigned NOT NULL auto_increment,
  `bill_num` int(10) unsigned NOT NULL,
  `bill_date` datetime NOT NULL,
  `bill_cash` double NOT NULL,
  PRIMARY KEY  (`bill_id`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `bk_bill`
--

/*!40000 ALTER TABLE `bk_bill` DISABLE KEYS */;
INSERT INTO `bk_bill` (`bill_id`,`bill_num`,`bill_date`,`bill_cash`) VALUES 
 (1,1,'2012-03-17 17:08:32',22.22),
 (2,2,'2012-03-17 17:12:59',33.33),
 (3,3,'2012-03-17 17:14:14',44.44),
 (4,4,'2012-03-17 17:16:22',55.55),
 (5,1,'2012-03-18 17:19:07',10),
 (6,2,'2012-03-18 17:19:59',7),
 (7,3,'2012-03-18 19:57:16',10),
 (8,4,'2012-03-18 20:02:59',10),
 (9,5,'2012-03-18 20:12:56',20),
 (10,7,'2012-03-18 20:16:52',15),
 (11,8,'2012-03-18 20:17:11',15),
 (12,9,'2012-03-18 20:17:28',100),
 (13,10,'2012-03-18 20:18:01',55),
 (14,1,'2012-03-19 00:03:22',0),
 (15,2,'2012-03-19 00:53:34',200),
 (16,1,'2012-03-31 17:32:42',200),
 (17,2,'2012-03-31 17:33:30',3),
 (18,3,'2012-03-31 17:33:13',50),
 (19,1,'2012-04-01 13:05:56',0),
 (20,2,'2012-04-01 13:05:56',0),
 (21,3,'2012-04-01 13:05:56',0),
 (22,1,'2012-04-02 02:20:09',22);
/*!40000 ALTER TABLE `bk_bill` ENABLE KEYS */;


--
-- Definition of table `bk_density`
--

DROP TABLE IF EXISTS `bk_density`;
CREATE TABLE `bk_density` (
  `id_den` int(10) unsigned NOT NULL auto_increment,
  `den_name` varchar(45) NOT NULL,
  PRIMARY KEY  (`id_den`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `bk_density`
--

/*!40000 ALTER TABLE `bk_density` DISABLE KEYS */;
INSERT INTO `bk_density` (`id_den`,`den_name`) VALUES 
 (1,'80'),
 (2,'100');
/*!40000 ALTER TABLE `bk_density` ENABLE KEYS */;


--
-- Definition of table `bk_discount`
--

DROP TABLE IF EXISTS `bk_discount`;
CREATE TABLE `bk_discount` (
  `id_dsc` int(10) unsigned NOT NULL auto_increment,
  `dsc_name` varchar(45) NOT NULL,
  PRIMARY KEY  (`id_dsc`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `bk_discount`
--

/*!40000 ALTER TABLE `bk_discount` DISABLE KEYS */;
INSERT INTO `bk_discount` (`id_dsc`,`dsc_name`) VALUES 
 (1,'Печать 38коп.');
/*!40000 ALTER TABLE `bk_discount` ENABLE KEYS */;


--
-- Definition of table `bk_formats`
--

DROP TABLE IF EXISTS `bk_formats`;
CREATE TABLE `bk_formats` (
  `id_fmt` int(10) unsigned NOT NULL auto_increment,
  `fmt_name` varchar(45) NOT NULL,
  PRIMARY KEY  (`id_fmt`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `bk_formats`
--

/*!40000 ALTER TABLE `bk_formats` DISABLE KEYS */;
INSERT INTO `bk_formats` (`id_fmt`,`fmt_name`) VALUES 
 (1,'А4'),
 (2,'А3');
/*!40000 ALTER TABLE `bk_formats` ENABLE KEYS */;


--
-- Definition of table `bk_ico`
--

DROP TABLE IF EXISTS `bk_ico`;
CREATE TABLE `bk_ico` (
  `ico_id` int(10) unsigned NOT NULL auto_increment,
  `ico_file` blob NOT NULL,
  PRIMARY KEY  (`ico_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `bk_ico`
--

/*!40000 ALTER TABLE `bk_ico` DISABLE KEYS */;
INSERT INTO `bk_ico` (`ico_id`,`ico_file`) VALUES 
 (1,0x89504E470D0A1A0A0000000D4948445200000010000000100803000000282D0F5300000300504C54450000001E1E1E2929293535353E3E3E4040404343434D4D4D5353525757565E5E5E636363747474868685929292949494979796A8A8A7A9A9A8AAAAA9B8B8B8C2C2C2C8C8C7D6D6D5DADAD9DADADAE2E2E1E3E3E2E3E3E3E6E6E5EDEDECF0F0F0F1F1F0F4F4F4F7F7F6FAFAFAFCFCFCFEFEFE1616160000008C15703273C31E01303273A2A02D200000001E00001E1168A02D401E00000000001E01D418002C1E1168EAF8D01E01481E11681E00001E01481E1162E803481E014800000200003E0000E300001D1E5B28E80170000003E807700000800000638C15A80001D300001DFFFFFEEAA3C83264AF00004C1E103800001D1E1B600000000000008C15D81E1B300000011E1100EAF8D032657AEAE7A00000000000E80101688C14901E00008C15D82C9AA21D7430FFFFFE3273A23273D50000001E11681E11680000000000008C15A0C19A261E00000000001E11608C15E84885B71E00000000001E11680000000000131E10381EE1704885D500004C0000000000134A217A8C16448C160448CAEC4D2210FFFFFF8C16104A65A60000004A211C0000008C16308C16444C2B3C000000FFFFFF450FE600000C0000038C165C4072F600002000000000000000002800000040742E8C16B80000004075B24075400000000000000000001EFBF80000000000280000000000008C16A88C19788C195840759D0000000000000000000000280000001EFBF88C19780000001EFBF80522482E0000657469000000735F65000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000202462620000000174524E530040E6D8660000006249444154789C5DCF6B1A42401880D1B722118D12464C8C4BFB5FA2F8318F6FCE0E0E6055F4DB45CAF2D704272768A00FCF0761CFE3222892AB90709362EE1E520F9987DCC3EB2914946FA1A4AD848E59D7077A86C97C1C336DDD651CBEBB615C600518C80EB928CBD8280000000049454E44AE426082);
/*!40000 ALTER TABLE `bk_ico` ENABLE KEYS */;


--
-- Definition of table `bk_list`
--

DROP TABLE IF EXISTS `bk_list`;
CREATE TABLE `bk_list` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `bill` int(10) unsigned NOT NULL default '0',
  `item` int(10) unsigned NOT NULL default '0',
  `prc` double NOT NULL default '0',
  `cnt` int(10) unsigned NOT NULL default '0',
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=48 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `bk_list`
--

/*!40000 ALTER TABLE `bk_list` DISABLE KEYS */;
INSERT INTO `bk_list` (`id`,`bill`,`item`,`prc`,`cnt`) VALUES 
 (1,2,1,0.4,5),
 (2,3,1,0.4,5),
 (3,5,1,0.4,5),
 (4,5,5,1,5),
 (5,6,1,0.4,4),
 (6,6,5,1,4),
 (7,7,5,1,10),
 (8,8,10,0.4,20),
 (9,9,2,0.65,8),
 (10,9,10,0.4,25),
 (11,10,33,2,1),
 (12,10,36,1.4,1),
 (13,10,44,5.5,2),
 (14,11,19,15,1),
 (15,12,7,4.8,5),
 (16,12,30,10,5),
 (17,13,24,34,1),
 (18,12,38,1.2,5),
 (19,12,40,4,2),
 (20,14,1,0.4,10),
 (21,14,5,1,3),
 (22,14,3,1,1),
 (23,15,1,0.4,58),
 (24,15,2,0.65,14),
 (25,15,6,2.6,5),
 (26,15,38,1.2,12),
 (27,15,31,12,2),
 (28,15,19,15,1),
 (29,15,25,36,1),
 (30,16,3,1,34),
 (31,16,7,4.8,20),
 (32,17,10,0.4,7),
 (33,18,1,0.4,20),
 (34,18,29,8,2),
 (35,19,2,0.65,1),
 (36,19,1,0.4,30),
 (37,19,16,8,1),
 (38,19,31,12,1),
 (39,19,15,7,1),
 (40,20,1,0.4,30),
 (41,21,1,0.4,51),
 (42,21,17,10,1),
 (43,21,52,1.1,1),
 (44,21,49,1.5,1),
 (45,22,1,0.4,5),
 (46,22,2,0.65,5),
 (47,22,5,1,8);
/*!40000 ALTER TABLE `bk_list` ENABLE KEYS */;


--
-- Definition of table `bk_price`
--

DROP TABLE IF EXISTS `bk_price`;
CREATE TABLE `bk_price` (
  `prc_id` int(10) unsigned NOT NULL auto_increment,
  `prc_name` varchar(45) NOT NULL,
  `prc_short` varchar(20) NOT NULL,
  `prc_price` double NOT NULL default '0',
  `prc_cnt` int(10) unsigned NOT NULL default '0',
  `prc_type` int(10) unsigned NOT NULL default '0',
  `prc_ico` int(10) unsigned NOT NULL default '0',
  `prc_use` tinyint(1) NOT NULL default '1',
  PRIMARY KEY  (`prc_id`)
) ENGINE=InnoDB AUTO_INCREMENT=66 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `bk_price`
--

/*!40000 ALTER TABLE `bk_price` DISABLE KEYS */;
INSERT INTO `bk_price` (`prc_id`,`prc_name`,`prc_short`,`prc_price`,`prc_cnt`,`prc_type`,`prc_ico`,`prc_use`) VALUES 
 (1,'Черно-белая','Печ.ч/б',0.4,0,1,1,1),
 (2,'Черно-белая','Печ.ч/б',0.65,0,1,1,1),
 (3,'Черно-белая','Печ.ч/б',1,0,1,1,1),
 (4,'Черно-белая','Печ.ч/б',1.8,0,1,1,1),
 (5,'Струйная','Печ.стр.',1,0,1,3,1),
 (6,'Лазерная','Печ.лаз.',2.6,0,1,2,1),
 (7,'Лазерная','Печ.лаз.',4.8,0,1,2,1),
 (8,'Лазерная','Печ.лаз.',3.9,0,1,2,1),
 (9,'Лазерная','Печ.лаз.',6.8,0,1,2,1),
 (10,'Ксерокс','Ксер.',0.4,0,2,4,1),
 (11,'Ксерокс','Ксер.',0.65,0,2,4,1),
 (12,'Ксерокс','Ксер.',0.7,0,2,4,1),
 (13,'Ксерокс','Ксер.',1,0,2,4,1),
 (14,'Мягкий 6','Пер.мяг.6',6,0,3,5,1),
 (15,'Мягкий 8','Пер.мяг.8',7,0,3,5,1),
 (16,'Мягкий 10','Пер.мяг.10',8,0,3,5,1),
 (17,'Мягкий 12','Пер.мяг.12',10,0,3,5,1),
 (18,'Мягкий 14','Пер.мяг.14',12,0,3,5,1),
 (19,'Мягкий 16-22','Пер.мяг.16-22',15,0,3,5,1),
 (20,'Мягкий 28-38','Пер.мяг.28-38',17,0,3,5,1),
 (21,'Мягкий 51','Пер.мяг.51',20,0,3,5,1),
 (22,'Мягкий переделка','Пер.мяг.пер.',2,0,3,5,1),
 (23,'Мягкий','Пер.мяг.',6,0,3,5,1),
 (24,'Твердый 7-10','Тверд.7-10',34,0,3,5,1),
 (25,'Твердый 13-16','Тверд.13-16',36,0,3,5,1),
 (26,'Твердый 20-24','Тверд.20-24',38,0,3,5,1),
 (27,'Твердый 28-32','Тверд.28-32',40,0,3,5,1),
 (28,'Твердый переделка','Тверд.пер.',5,0,3,5,1),
 (29,'Глянцевая','Лам.глян.',8,0,4,6,1),
 (30,'Матовая','Лам.мат.',10,0,4,6,1),
 (31,'Глянцевая','Лам.глян.',12,0,4,6,1),
 (32,'Глянцевая','Лам.глян.',16,0,4,6,1),
 (33,'Бумага для записи серая','Бум.для зап.сер.',2,0,10,0,1),
 (34,'Бумага для записи цветная кл.','Бум.для зап.цв.',5,0,10,0,1),
 (35,'Зажим 32','Зажим 32',1.1,0,10,0,1),
 (36,'Зажим 41','Зажим 41',1.4,0,10,0,1),
 (37,'Зажим 51','Зажим 51',1.8,0,10,0,1),
 (38,'Карандаш','Карандаш',1.2,0,10,0,1),
 (39,'Клей карандаш','Клей каран.',2.3,0,10,0,1),
 (40,'Клей ПВА','Клей ПВА',4,0,10,0,1),
 (41,'Конверты','Конверты',1,0,10,0,1),
 (42,'Корректор','Корректор',4.5,0,10,0,1),
 (43,'Линейка','Линейка',3.5,0,10,0,1),
 (44,'Маркер для дисков','Маркер для диск.',5.5,0,10,0,1),
 (45,'Нож для бумаги','Нож для бум.',3,0,10,0,1),
 (46,'Ножницы большие','Ножн.больш.',12,0,10,0,1),
 (47,'Ножницы маленькие','Ножн.мал.',8,0,10,0,1),
 (48,'Папки для бумаг','Папки для бум.',1,0,10,0,1),
 (49,'Резинка','Резинка',1.5,0,10,0,1),
 (50,'Ручка гелевая','Ручка гел.',2.5,0,10,0,1),
 (51,'Ручка Матадор','Ручка Мат.',1.8,0,10,0,1),
 (52,'Ручка обычная','Ручка обыч.',1.1,0,10,0,1),
 (53,'Скобы для степлера 10','Скобы 10',1.7,0,10,0,1),
 (54,'Скобы для степлера 24','Скобы 24',2.7,0,10,0,1),
 (55,'Скотч большой','Скотч бол.',3,0,10,0,1),
 (56,'Скотч маленький','Скотч мал.',2,0,10,0,1),
 (57,'Скотч средний','Скотч сред.',10,0,10,0,1),
 (58,'Скрепки','Скрепки',2.7,0,10,0,1),
 (59,'Степлер','Степлер',9,0,10,0,1),
 (60,'Тетрадь 12л.','Тетрадь 12л.',2.7,0,10,0,1),
 (61,'Тетрадь 24л.','Тетрадь 24л.',2.7,0,10,0,1),
 (62,'Тетрадь 60л.','Тетрадь 60л.',5,0,10,0,1),
 (63,'Тетрадь 96л.','Тетрадь 96л.',6,0,10,0,1),
 (64,'Точилка металл.','Точилка мет.',3,0,10,0,1),
 (65,'Точилка цветная','Точилка цв.',3.5,0,10,0,1);
/*!40000 ALTER TABLE `bk_price` ENABLE KEYS */;


--
-- Definition of table `bk_service`
--

DROP TABLE IF EXISTS `bk_service`;
CREATE TABLE `bk_service` (
  `srv_id` int(10) unsigned NOT NULL default '0',
  `srv_printer` varchar(45) NOT NULL default 'prn',
  `srv_fmt` int(10) unsigned NOT NULL default '1',
  `srv_dual` tinyint(1) NOT NULL default '0',
  PRIMARY KEY  (`srv_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `bk_service`
--

/*!40000 ALTER TABLE `bk_service` DISABLE KEYS */;
INSERT INTO `bk_service` (`srv_id`,`srv_printer`,`srv_fmt`,`srv_dual`) VALUES 
 (1,'prn',1,0),
 (2,'prn',1,1),
 (3,'prn',2,0),
 (4,'prn',2,1),
 (5,'prn',1,0),
 (6,'prn',1,0),
 (7,'prn',1,1),
 (8,'prn',2,0),
 (9,'prn',2,1),
 (10,'xerox',1,0),
 (11,'xerox',1,1),
 (12,'xerox',2,0),
 (13,'xerox',2,1),
 (14,'пласт.',1,0),
 (15,'пласт.',1,0),
 (16,'пласт.',1,0),
 (17,'пласт.',1,0),
 (18,'пласт.',1,0),
 (19,'пласт.',1,0),
 (20,'пласт.',1,0),
 (21,'пласт.',1,0),
 (22,'пласт.',1,0),
 (23,'метал.',1,0),
 (24,'-',1,0),
 (25,'-',1,0),
 (26,'-',1,0),
 (27,'-',1,0),
 (28,'-',1,0),
 (29,'80мкм.',1,0),
 (30,'80мкм.',1,0),
 (31,'100мкм.',1,0),
 (32,'80мкм.',2,0);
/*!40000 ALTER TABLE `bk_service` ENABLE KEYS */;


--
-- Definition of table `bk_service_type`
--

DROP TABLE IF EXISTS `bk_service_type`;
CREATE TABLE `bk_service_type` (
  `id_type` int(10) unsigned NOT NULL auto_increment,
  `type_name` varchar(45) NOT NULL,
  PRIMARY KEY  (`id_type`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `bk_service_type`
--

/*!40000 ALTER TABLE `bk_service_type` DISABLE KEYS */;
INSERT INTO `bk_service_type` (`id_type`,`type_name`) VALUES 
 (1,'Печать'),
 (2,'Ксерокс'),
 (3,'Переплет'),
 (4,'Ламинация'),
 (5,'Сканирование'),
 (6,'Запись');
/*!40000 ALTER TABLE `bk_service_type` ENABLE KEYS */;




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
