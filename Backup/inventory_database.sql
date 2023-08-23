/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50505
Source Host           : localhost:3306
Source Database       : inventory_databasenew

Target Server Type    : MYSQL
Target Server Version : 50505
File Encoding         : 65001

Date: 2023-07-14 08:24:53
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
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ----------------------------
-- Records of db_activitylogs
-- ----------------------------
INSERT INTO `db_activitylogs` VALUES ('1', '2023-07-13', '13:58:26', '16-12667', 'has logged in to the system with the USERNAME admin and USERTYPE Administrator');
INSERT INTO `db_activitylogs` VALUES ('2', '2023-07-13', '13:58:59', '16-12667', 'has logged in to the system with the USERNAME admin and USERTYPE Administrator');
INSERT INTO `db_activitylogs` VALUES ('3', '2023-07-13', '13:59:32', '16-12667', ' has added data into the system. Number of Pieces: 1 PIECE  Type of ICT Equipment: DESKTOP Hardware Type: Desktop');
INSERT INTO `db_activitylogs` VALUES ('4', '2023-07-13', '14:25:50', '16-12667', 'has logged in to the system with the USERNAME ADMIN and USERTYPE Administrator');
INSERT INTO `db_activitylogs` VALUES ('5', '2023-07-13', '14:33:56', '16-12667', 'has logged in to the system with the USERNAME admin and USERTYPE Administrator');
INSERT INTO `db_activitylogs` VALUES ('6', '2023-07-13', '14:39:02', '16-12667', 'has logged in to the system with the USERNAME admin and USERTYPE Administrator');
INSERT INTO `db_activitylogs` VALUES ('7', '2023-07-13', '14:41:39', '16-12667', 'has logged in to the system with the USERNAME admin and USERTYPE Administrator');
INSERT INTO `db_activitylogs` VALUES ('8', '2023-07-13', '14:42:09', '16-12667', 'has logged in to the system with the USERNAME admin and USERTYPE Administrator');
INSERT INTO `db_activitylogs` VALUES ('9', '2023-07-13', '14:43:34', '16-12667', 'has logged in to the system with the USERNAME admin and USERTYPE Administrator');
INSERT INTO `db_activitylogs` VALUES ('10', '2023-07-13', '14:46:37', '16-12667', 'has logged in to the system with the USERNAME admin and USERTYPE Administrator');
INSERT INTO `db_activitylogs` VALUES ('11', '2023-07-13', '14:48:32', '16-12667', 'has logged in to the system with the USERNAME admin and USERTYPE Administrator');
INSERT INTO `db_activitylogs` VALUES ('12', '2023-07-13', '15:01:20', '16-12667', 'has logged in to the system with the USERNAME admin and USERTYPE Administrator');
INSERT INTO `db_activitylogs` VALUES ('13', '2023-07-13', '15:04:20', '16-12667', 'has logged in to the system with the USERNAME admin and USERTYPE Administrator');
INSERT INTO `db_activitylogs` VALUES ('14', '2023-07-13', '15:49:40', '16-12667', 'has logged in to the system with the USERNAME admin and USERTYPE Administrator');
INSERT INTO `db_activitylogs` VALUES ('15', '2023-07-13', '15:53:32', '16-12667', 'has logged in to the system with the USERNAME admin and USERTYPE Administrator');
INSERT INTO `db_activitylogs` VALUES ('16', '2023-07-13', '15:54:20', '16-12667', 'has logged in to the system with the USERNAME admin and USERTYPE Administrator');
INSERT INTO `db_activitylogs` VALUES ('17', '2023-07-13', '15:58:31', '16-12667', 'has UPDATED INVENTORY with the usertype Administrator and with the username of 16-12667');
INSERT INTO `db_activitylogs` VALUES ('18', '2023-07-13', '15:58:54', '16-12667', 'has logged in to the system with the USERNAME ADMIN and USERTYPE Administrator');
INSERT INTO `db_activitylogs` VALUES ('19', '2023-07-13', '16:01:40', '16-12667', 'has logged in to the system with the USERNAME admin and USERTYPE Administrator');
INSERT INTO `db_activitylogs` VALUES ('20', '2023-07-13', '16:02:58', '16-12667', 'has logged in to the system with the USERNAME admin and USERTYPE Administrator');
INSERT INTO `db_activitylogs` VALUES ('21', '2023-07-13', '16:03:58', '16-12667', 'has UPDATED INVENTORY with the usertype Administrator and with the username of 16-12667');
INSERT INTO `db_activitylogs` VALUES ('22', '2023-07-13', '16:04:22', '16-12667', 'has UPDATED INVENTORY with the usertype Administrator and with the username of 16-12667');
INSERT INTO `db_activitylogs` VALUES ('23', '2023-07-13', '16:04:29', '16-12667', 'has UPDATED INVENTORY with the usertype Administrator and with the username of 16-12667');
INSERT INTO `db_activitylogs` VALUES ('24', '2023-07-13', '16:06:25', '16-12667', 'has logged in to the system with the USERNAME admin and USERTYPE Administrator');
INSERT INTO `db_activitylogs` VALUES ('25', '2023-07-13', '16:06:42', '16-12667', 'has UPDATED INVENTORY with the usertype Administrator and with the username of 16-12667');
INSERT INTO `db_activitylogs` VALUES ('26', '2023-07-13', '16:09:20', '16-12667', 'has logged in to the system with the USERNAME admin and USERTYPE Administrator');
INSERT INTO `db_activitylogs` VALUES ('27', '2023-07-13', '16:10:42', '16-12667', 'has logged in to the system with the USERNAME admin and USERTYPE Administrator');
INSERT INTO `db_activitylogs` VALUES ('28', '2023-07-13', '16:12:44', '16-12667', 'has logged in to the system with the USERNAME admin and USERTYPE Administrator');

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
  `SerialNumber` varchar(250) DEFAULT NULL,
  `PreviousOwner` varchar(250) DEFAULT NULL,
  `Model` varchar(250) DEFAULT NULL,
  `Brand` varchar(250) DEFAULT NULL,
  `PropertyNumber` varchar(250) DEFAULT NULL,
  `Cost` varchar(250) DEFAULT NULL,
  `YearAcquired` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ----------------------------
-- Records of db_inventory
-- ----------------------------
INSERT INTO `db_inventory` VALUES ('1', 'Lance Andrei U. Espina', 'RICTMS', 'RICTMS', '1 PIECE ', 'DESKTOP', 'Desktop', '4CE308C7D1', 'NONE', '280 G9 SF', 'HP', 'CRG-CO1-23-0014', '71101.00', '25/05/2023');

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
