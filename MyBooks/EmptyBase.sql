/*
SQLyog Ultimate v12.14 (64 bit)
MySQL - 5.7.18 : Database - my_books
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`my_books` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `my_books`;

/*Table structure for table `bk_aa_test` */

DROP TABLE IF EXISTS `bk_aa_test`;

CREATE TABLE `bk_aa_test` (
  `t_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `t_name` varchar(45) NOT NULL,
  `t_val` int(10) unsigned NOT NULL,
  PRIMARY KEY (`t_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_account` */

DROP TABLE IF EXISTS `bk_account`;

CREATE TABLE `bk_account` (
  `a_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `a_date` datetime NOT NULL,
  `a_point` int(10) unsigned NOT NULL DEFAULT '2',
  `a_way` int(10) unsigned NOT NULL DEFAULT '1',
  `a_business` int(10) unsigned NOT NULL DEFAULT '1',
  `a_cat` int(10) unsigned NOT NULL DEFAULT '1',
  `a_prc` decimal(15,5) NOT NULL DEFAULT '0.00000',
  `a_cnt` int(10) unsigned NOT NULL DEFAULT '1',
  `a_com` varchar(255) NOT NULL DEFAULT 'итого',
  `a_who` int(10) unsigned NOT NULL DEFAULT '1',
  `a_agent` int(10) unsigned NOT NULL DEFAULT '0',
  `a_from` int(10) unsigned NOT NULL DEFAULT '0',
  `a_file` varchar(45) NOT NULL DEFAULT 'kass_book',
  PRIMARY KEY (`a_id`),
  KEY `a_point` (`a_point`,`a_way`,`a_who`,`a_agent`)
) ENGINE=InnoDB AUTO_INCREMENT=33963 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_agent` */

DROP TABLE IF EXISTS `bk_agent`;

CREATE TABLE `bk_agent` (
  `ag_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ag_name` varchar(45) NOT NULL,
  PRIMARY KEY (`ag_id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_article_group` */

DROP TABLE IF EXISTS `bk_article_group`;

CREATE TABLE `bk_article_group` (
  `ag_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ag_parent` int(10) unsigned NOT NULL DEFAULT '4',
  `ag_name` varchar(45) NOT NULL,
  `ag_order` int(10) unsigned NOT NULL DEFAULT '1',
  `ag_art` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`ag_id`)
) ENGINE=InnoDB AUTO_INCREMENT=65 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_articles` */

DROP TABLE IF EXISTS `bk_articles`;

CREATE TABLE `bk_articles` (
  `a_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `a_title` varchar(45) NOT NULL DEFAULT '',
  `a_date` datetime NOT NULL DEFAULT '2015-02-05 22:00:00',
  `a_text` text NOT NULL,
  `a_param` int(10) unsigned NOT NULL DEFAULT '0',
  `a_int` int(10) unsigned NOT NULL DEFAULT '0',
  `a_page_title` varchar(100) NOT NULL DEFAULT '1',
  `a_page_desc` varchar(255) NOT NULL DEFAULT 'Web-page default description',
  PRIMARY KEY (`a_id`)
) ENGINE=InnoDB AUTO_INCREMENT=66 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_audit` */

DROP TABLE IF EXISTS `bk_audit`;

CREATE TABLE `bk_audit` (
  `a_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `a_date` datetime NOT NULL,
  `a_point` int(10) unsigned NOT NULL,
  `a_way` int(10) unsigned NOT NULL,
  `a_inc_prc` decimal(10,2) NOT NULL DEFAULT '0.00',
  `a_inc_cnt` int(10) unsigned NOT NULL DEFAULT '0',
  `a_inc_s` varchar(255) NOT NULL DEFAULT '',
  `a_out` decimal(10,2) NOT NULL DEFAULT '0.00',
  `a_out_s` varchar(255) NOT NULL DEFAULT '',
  `a_by` int(10) unsigned NOT NULL DEFAULT '1',
  `a_agent` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`a_id`),
  KEY `a_point` (`a_point`,`a_way`,`a_by`,`a_agent`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `bk_bill` */

DROP TABLE IF EXISTS `bk_bill`;

CREATE TABLE `bk_bill` (
  `bill_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `bill_point` int(10) unsigned NOT NULL DEFAULT '1',
  `bill_num` int(10) unsigned NOT NULL,
  `bill_date` datetime NOT NULL,
  `bill_cash` decimal(20,2) NOT NULL,
  `bill_rcpt` varchar(45) DEFAULT NULL,
  `bill_payer` varchar(45) DEFAULT NULL,
  `bill_reason` varchar(45) DEFAULT NULL,
  `bill_cond` varchar(45) DEFAULT NULL,
  `bill_user` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`bill_id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=57 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_business` */

DROP TABLE IF EXISTS `bk_business`;

CREATE TABLE `bk_business` (
  `b_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `b_name` varchar(25) NOT NULL,
  PRIMARY KEY (`b_id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_carrier` */

DROP TABLE IF EXISTS `bk_carrier`;

CREATE TABLE `bk_carrier` (
  `car_iid` int(10) unsigned NOT NULL,
  `car_density` int(10) unsigned NOT NULL,
  `car_fmt` int(10) unsigned NOT NULL,
  `car_surface` int(10) unsigned NOT NULL,
  PRIMARY KEY (`car_iid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `bk_cat` */

DROP TABLE IF EXISTS `bk_cat`;

CREATE TABLE `bk_cat` (
  `ct_id` int(11) NOT NULL AUTO_INCREMENT,
  `ct_com` int(11) NOT NULL DEFAULT '1',
  `ct_item` int(11) NOT NULL DEFAULT '1',
  `ct_code` varchar(455) NOT NULL DEFAULT '-',
  `ct_price` double NOT NULL DEFAULT '0',
  `ct_unit` int(10) unsigned NOT NULL DEFAULT '1',
  `ct_updated` date NOT NULL,
  `ct_art` varchar(455) NOT NULL,
  PRIMARY KEY (`ct_id`)
) ENGINE=InnoDB AUTO_INCREMENT=128 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_category` */

DROP TABLE IF EXISTS `bk_category`;

CREATE TABLE `bk_category` (
  `c_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `c_way` int(10) unsigned NOT NULL DEFAULT '2',
  `c_name` varchar(45) NOT NULL,
  PRIMARY KEY (`c_id`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_company` */

DROP TABLE IF EXISTS `bk_company`;

CREATE TABLE `bk_company` (
  `c_id` int(11) NOT NULL AUTO_INCREMENT,
  `c_name` varchar(45) NOT NULL,
  `c_short` varchar(16) NOT NULL,
  `c_role` int(11) NOT NULL COMMENT 'x1-supplier, x2-customer, x100-mine',
  `c_mfo` varchar(6) NOT NULL,
  `c_edrpou` varchar(15) NOT NULL,
  `c_cert` varchar(10) NOT NULL,
  `c_account_n` varchar(45) NOT NULL,
  `c_adr` varchar(55) NOT NULL,
  `c_coeff` decimal(10,4) NOT NULL DEFAULT '1.0000',
  PRIMARY KEY (`c_id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_density` */

DROP TABLE IF EXISTS `bk_density`;

CREATE TABLE `bk_density` (
  `id_den` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `den_name` varchar(45) NOT NULL,
  PRIMARY KEY (`id_den`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_device` */

DROP TABLE IF EXISTS `bk_device`;

CREATE TABLE `bk_device` (
  `d_id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `d_name` varchar(55) NOT NULL,
  `d_short` varchar(10) NOT NULL,
  `d_type` int(11) unsigned NOT NULL,
  `d_ip` varchar(16) NOT NULL DEFAULT '127.0.0.1',
  PRIMARY KEY (`d_id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_device_history` */

DROP TABLE IF EXISTS `bk_device_history`;

CREATE TABLE `bk_device_history` (
  `dh_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `dh_device` int(10) unsigned NOT NULL,
  `dh_dt` datetime NOT NULL,
  `dh_total` int(10) unsigned NOT NULL,
  PRIMARY KEY (`dh_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `bk_device_history_fmt` */

DROP TABLE IF EXISTS `bk_device_history_fmt`;

CREATE TABLE `bk_device_history_fmt` (
  `dh_fid` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `dh_sid` int(10) unsigned NOT NULL,
  `dh_total` int(10) unsigned NOT NULL,
  PRIMARY KEY (`dh_fid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `bk_devtype` */

DROP TABLE IF EXISTS `bk_devtype`;

CREATE TABLE `bk_devtype` (
  `dt_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `dt_name` varchar(55) NOT NULL,
  `dt_total` varchar(55) NOT NULL COMMENT 'TotalCounter SNMP Id',
  PRIMARY KEY (`dt_id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_devtype_snmp` */

DROP TABLE IF EXISTS `bk_devtype_snmp`;

CREATE TABLE `bk_devtype_snmp` (
  `dt_sid` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `dt_type` int(10) unsigned NOT NULL,
  `dt_fmt` int(10) unsigned NOT NULL,
  `dt_snmp` varchar(55) NOT NULL,
  `dt_color` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT '0-Any, 1-b/w, 2-Color',
  PRIMARY KEY (`dt_sid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `bk_formats` */

DROP TABLE IF EXISTS `bk_formats`;

CREATE TABLE `bk_formats` (
  `id_fmt` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `fmt_name` varchar(45) NOT NULL,
  `fmt_width` int(10) unsigned NOT NULL DEFAULT '0',
  `fmt_height` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`id_fmt`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_ico` */

DROP TABLE IF EXISTS `bk_ico`;

CREATE TABLE `bk_ico` (
  `ico_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ico_file` blob NOT NULL,
  PRIMARY KEY (`ico_id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_item_devices` */

DROP TABLE IF EXISTS `bk_item_devices`;

CREATE TABLE `bk_item_devices` (
  `item_id` int(10) unsigned NOT NULL,
  `dev_id` int(10) unsigned NOT NULL,
  `param` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`item_id`,`dev_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `bk_item_group` */

DROP TABLE IF EXISTS `bk_item_group`;

CREATE TABLE `bk_item_group` (
  `ig_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ig_name` varchar(55) NOT NULL COMMENT 'Группа товаров',
  `ig_bus` int(10) unsigned NOT NULL COMMENT 'bk_business',
  PRIMARY KEY (`ig_id`)
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_item_printer` */

DROP TABLE IF EXISTS `bk_item_printer`;

CREATE TABLE `bk_item_printer` (
  `ip_id` int(11) NOT NULL AUTO_INCREMENT,
  `ip_item` int(11) NOT NULL,
  `ip_printer` int(11) NOT NULL,
  PRIMARY KEY (`ip_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `bk_items` */

DROP TABLE IF EXISTS `bk_items`;

CREATE TABLE `bk_items` (
  `i_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `i_name` varchar(255) NOT NULL,
  `i_short` varchar(255) NOT NULL,
  `i_name_ua` varchar(255) NOT NULL COMMENT '-',
  `i_tab` int(10) unsigned NOT NULL DEFAULT '0',
  `i_ico` int(10) unsigned NOT NULL DEFAULT '0',
  `i_param` int(10) unsigned NOT NULL DEFAULT '1',
  `i_pos` int(10) unsigned NOT NULL DEFAULT '0',
  `i_unit` int(10) unsigned NOT NULL DEFAULT '1' COMMENT 'base_unit',
  `i_min_unit` int(10) unsigned NOT NULL DEFAULT '1' COMMENT 'minimal_unit',
  `i_fmt` int(10) unsigned NOT NULL DEFAULT '0',
  `i_desc` text,
  `i_art` varchar(45) DEFAULT NULL,
  `i_man` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'Manufacturer',
  `i_group` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'bk_item_group',
  `i_device` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`i_id`)
) ENGINE=InnoDB AUTO_INCREMENT=303 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_iunits` */

DROP TABLE IF EXISTS `bk_iunits`;

CREATE TABLE `bk_iunits` (
  `iu_item` int(10) unsigned NOT NULL,
  `iu_unit` int(10) unsigned NOT NULL,
  `iu_rate` int(10) unsigned NOT NULL,
  `iu_rate_unit` int(10) unsigned NOT NULL,
  `iu_min` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`iu_item`,`iu_unit`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `bk_list` */

DROP TABLE IF EXISTS `bk_list`;

CREATE TABLE `bk_list` (
  `bill` int(10) unsigned NOT NULL DEFAULT '0',
  `item` int(10) unsigned NOT NULL DEFAULT '0',
  `prc` decimal(20,2) NOT NULL DEFAULT '0.00',
  `cnt` decimal(20,2) NOT NULL DEFAULT '0.00',
  `unit` int(10) unsigned NOT NULL DEFAULT '1',
  `car` int(10) unsigned NOT NULL DEFAULT '0',
  `device` int(10) unsigned NOT NULL DEFAULT '0',
  `var` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`bill`,`item`,`unit`,`car`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `bk_point` */

DROP TABLE IF EXISTS `bk_point`;

CREATE TABLE `bk_point` (
  `pt_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `pt_name` varchar(45) NOT NULL,
  `pt_adr` varchar(45) NOT NULL DEFAULT ' ',
  PRIMARY KEY (`pt_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_price` */

DROP TABLE IF EXISTS `bk_price`;

CREATE TABLE `bk_price` (
  `p_item` int(10) unsigned NOT NULL,
  `p_point` int(10) unsigned NOT NULL DEFAULT '3',
  `p_unit` int(10) unsigned NOT NULL DEFAULT '1',
  `p_car` int(10) unsigned NOT NULL DEFAULT '0',
  `p_var` int(10) unsigned NOT NULL DEFAULT '0',
  `p_prc` decimal(20,2) NOT NULL DEFAULT '0.00',
  `p_car_cnt` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`p_item`,`p_point`,`p_unit`,`p_car`,`p_var`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `bk_printer` */

DROP TABLE IF EXISTS `bk_printer`;

CREATE TABLE `bk_printer` (
  `p_id` int(11) NOT NULL AUTO_INCREMENT,
  `p_name` varchar(50) NOT NULL,
  PRIMARY KEY (`p_id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_regular` */

DROP TABLE IF EXISTS `bk_regular`;

CREATE TABLE `bk_regular` (
  `rg_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `rg_cat` int(10) unsigned NOT NULL,
  `rg_param` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`rg_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_roles` */

DROP TABLE IF EXISTS `bk_roles`;

CREATE TABLE `bk_roles` (
  `r_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `r_name` varchar(45) NOT NULL,
  PRIMARY KEY (`r_id`)
) ENGINE=InnoDB AUTO_INCREMENT=102 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_slist` */

DROP TABLE IF EXISTS `bk_slist`;

CREATE TABLE `bk_slist` (
  `sl_sup` int(10) unsigned NOT NULL,
  `sl_item` int(10) unsigned NOT NULL,
  `sl_prc` double NOT NULL,
  `sl_cnt` int(10) NOT NULL,
  `sl_unit` int(10) unsigned NOT NULL,
  PRIMARY KEY (`sl_sup`,`sl_item`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `bk_string` */

DROP TABLE IF EXISTS `bk_string`;

CREATE TABLE `bk_string` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `key` varchar(20) NOT NULL DEFAULT '',
  `lang` int(11) NOT NULL DEFAULT '1',
  `val` varchar(255) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`),
  KEY `IX_Lang` (`lang`),
  KEY `IX_key` (`key`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_supply` */

DROP TABLE IF EXISTS `bk_supply`;

CREATE TABLE `bk_supply` (
  `s_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `s_com` int(10) unsigned NOT NULL,
  `s_date_order` date NOT NULL,
  `s_date_deliver` date NOT NULL,
  `s_number` varchar(45) NOT NULL,
  `s_status` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '0-Wait, 1-Ready',
  `s_point` int(10) NOT NULL DEFAULT '0',
  PRIMARY KEY (`s_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_surface` */

DROP TABLE IF EXISTS `bk_surface`;

CREATE TABLE `bk_surface` (
  `sf_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `sf_name` varchar(45) NOT NULL,
  `sf_short` varchar(10) NOT NULL,
  `sf_short_ua` varchar(45) NOT NULL,
  PRIMARY KEY (`sf_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_tab_type` */

DROP TABLE IF EXISTS `bk_tab_type`;

CREATE TABLE `bk_tab_type` (
  `t_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `t_col_cnt` int(10) unsigned NOT NULL,
  `t_names` varchar(255) NOT NULL,
  `t_width` varchar(255) NOT NULL,
  `t_carrier` int(10) unsigned NOT NULL DEFAULT '0',
  `t_device` int(10) unsigned NOT NULL DEFAULT '0',
  `t_price` int(10) unsigned NOT NULL DEFAULT '0',
  `t_count` int(10) unsigned NOT NULL DEFAULT '0',
  `t_unit` int(10) unsigned NOT NULL DEFAULT '0',
  `t_rest` int(10) unsigned NOT NULL DEFAULT '0',
  `t_note` int(10) unsigned NOT NULL DEFAULT '0',
  `t_variant` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`t_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_tabs` */

DROP TABLE IF EXISTS `bk_tabs`;

CREATE TABLE `bk_tabs` (
  `id_tab` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `t_tag` varchar(5) NOT NULL,
  `t_name` varchar(45) NOT NULL,
  `t_pos` int(10) unsigned NOT NULL,
  `t_type` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`id_tab`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_units` */

DROP TABLE IF EXISTS `bk_units`;

CREATE TABLE `bk_units` (
  `un_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `un_name` varchar(25) NOT NULL COMMENT 'For_1',
  `un_short` varchar(10) NOT NULL COMMENT 'Short',
  `un_name2` varchar(25) NOT NULL COMMENT 'For_2',
  `un_name5` varchar(25) NOT NULL COMMENT 'For_5',
  PRIMARY KEY (`un_id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_users` */

DROP TABLE IF EXISTS `bk_users`;

CREATE TABLE `bk_users` (
  `u_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `u_login` varchar(50) NOT NULL,
  `u_pass` varchar(64) NOT NULL DEFAULT 'not',
  `u_role` int(10) unsigned NOT NULL DEFAULT '0',
  `u_family` varchar(55) NOT NULL DEFAULT '',
  `u_name` varchar(55) DEFAULT NULL,
  `u_prenom` varchar(55) DEFAULT NULL,
  `u_com` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`u_id`),
  UNIQUE KEY `u_login_UNIQUE` (`u_login`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_variant` */

DROP TABLE IF EXISTS `bk_variant`;

CREATE TABLE `bk_variant` (
  `v_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `v_item` int(10) unsigned NOT NULL,
  `v_name` varchar(50) NOT NULL,
  `v_param` int(10) unsigned NOT NULL DEFAULT '0',
  `v_device` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`v_id`),
  KEY `IX_Item` (`v_item`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_variant_item` */

DROP TABLE IF EXISTS `bk_variant_item`;

CREATE TABLE `bk_variant_item` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `var_id` int(10) unsigned NOT NULL,
  `item_id` int(10) unsigned NOT NULL,
  `item_cnt` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Table structure for table `bk_ware` */

DROP TABLE IF EXISTS `bk_ware`;

CREATE TABLE `bk_ware` (
  `w_item` int(11) unsigned NOT NULL DEFAULT '0',
  `w_point` int(11) unsigned NOT NULL DEFAULT '0',
  `w_cnt` int(10) unsigned NOT NULL DEFAULT '0',
  UNIQUE KEY `UK_ItemPoint` (`w_item`,`w_point`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `bk_who` */

DROP TABLE IF EXISTS `bk_who`;

CREATE TABLE `bk_who` (
  `w_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `w_name` varchar(45) NOT NULL,
  PRIMARY KEY (`w_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;


/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
