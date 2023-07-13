/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50505
Source Host           : localhost:3306
Source Database       : inventory_databasenew

Target Server Type    : MYSQL
Target Server Version : 50505
File Encoding         : 65001

Date: 2023-07-13 14:01:37
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `db_activitylogs`
-- ----------------------------
DROP TABLE IF EXISTS `db_activitylogs`;
CREATE TABLE `db_activitylogs` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `Date` date DEFAULT NULL,
  `Time` time DEFAULT NULL,
  `UserID` varchar(250) DEFAULT NULL,
  `Activity` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ----------------------------
-- Records of db_activitylogs
-- ----------------------------
INSERT INTO `db_activitylogs` VALUES ('1', '2023-07-13', '13:58:26', '16-12667', 'has logged in to the system with the USERNAME admin and USERTYPE Administrator');
INSERT INTO `db_activitylogs` VALUES ('2', '2023-07-13', '13:58:59', '16-12667', 'has logged in to the system with the USERNAME admin and USERTYPE Administrator');
INSERT INTO `db_activitylogs` VALUES ('3', '2023-07-13', '13:59:32', '16-12667', ' has added data into the system. Number of Pieces: 1 PIECE  Type of ICT Equipment: DESKTOP Hardware Type: Desktop');

-- ----------------------------
-- Table structure for `db_inventory`
-- ----------------------------
DROP TABLE IF EXISTS `db_inventory`;
CREATE TABLE `db_inventory` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `NameofStaff` varchar(250) DEFAULT NULL,
  `Section` varchar(250) DEFAULT NULL,
  `Division` varchar(250) DEFAULT NULL,
  `Piece` varchar(250) DEFAULT NULL,
  `TypeOfICTEquipment` varchar(250) DEFAULT NULL,
  `Type` varchar(250) DEFAULT NULL,
  `YearAcquired` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ----------------------------
-- Records of db_inventory
-- ----------------------------
INSERT INTO `db_inventory` VALUES ('1', 'Lance Andrei U. Espina', 'RICTMS', 'RICTMS', '1 PIECE ', 'DESKTOP', 'Desktop', '13/07/2023');

-- ----------------------------
-- Table structure for `db_register`
-- ----------------------------
DROP TABLE IF EXISTS `db_register`;
CREATE TABLE `db_register` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `OfficeID` varchar(250) DEFAULT NULL,
  `Name` varchar(250) DEFAULT NULL,
  `Usertype` varchar(250) DEFAULT NULL,
  `Username` varchar(250) DEFAULT NULL,
  `Password` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ----------------------------
-- Records of db_register
-- ----------------------------
INSERT INTO `db_register` VALUES ('1', '16-12667', 'Lance Andrei U. Espina', 'Administrator', 'admin', 'admin');

-- ----------------------------
-- Table structure for `db_statusbar`
-- ----------------------------
DROP TABLE IF EXISTS `db_statusbar`;
CREATE TABLE `db_statusbar` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `UserID` varchar(250) DEFAULT NULL,
  `Name` varchar(250) DEFAULT NULL,
  `Usertype` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ----------------------------
-- Records of db_statusbar
-- ----------------------------
INSERT INTO `db_statusbar` VALUES ('1', '16-12667', 'Lance Andrei U. Espina', 'Administrator');
