/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
DELETE FROM tblHurricanes
WHERE 1 = 1;

DELETE FROM lktCounties
WHERE 1 = 1;

INSERT INTO tblHurricanes(Id, [Name], SaffirSimpsonCat, LandfallDate) VALUES
(1, 'Andrew',	5,	'19920824'),
(2, 'Opal', 	3,	'19951004'),
(3, 'Charley',	4,	'20040813'),
(4, 'Ivan',		3,	'20040916'),
(5, 'Jeanne',	3,	'20040926'),
(6, 'Dennis',	3,	'20050710'),
(7, 'Wilma',	3,	'20051024'),
(8, 'Irma',		4,	'20170910'),
(9, 'Michael',	5,	'20181010');

INSERT INTO lktCounties(Id, [Name]) VALUES
(1,'Clay'),
(2,'Taylor'),
(3,'Santa Rosa'),
(4,'Lafayette'),
(5,'Monroe'),
(6,'Gilchrist'),
(7,'Bay'),
(8,'Lee'),
(9,'Walton'),
(10,'Marion'),
(11,'Seminole'),
(12,'Madison'),
(13,'Calhoun'),
(14,'Escambia'),
(15,'Baldwin'),
(16,'Decatur'),
(17,'Sumter'),
(18,'Levy'),
(19,'Harrison'),
(20,'Franklin'),
(21,'Highlands'),
(22,'Broward'),
(23,'Polk'),
(24,'Bradford'),
(25,'Manatee'),
(26,'DeSoto'),
(27,'Washington'),
(28,'Martin'),
(29,'Flagler'),
(30,'Clinch'),
(31,'Covington'),
(32,'Hillsborough'),
(33,'Brevard'),
(34,'Nassau'),
(35,'Camden'),
(36,'Liberty'),
(37,'Lake'),
(38,'Collier'),
(39,'Glades'),
(40,'Alachua'),
(41,'Pasco'),
(42,'Baker'),
(43,'Putnam'),
(44,'St. Johns'),
(45,'Volusia'),
(46,'Duval'),
(47,'Hendry'),
(48,'Dixie'),
(49,'Orange'),
(50,'Columbia'),
(51,'Charlton'),
(52,'Echols'),
(53,'Indian River'),
(54,'McIntosh'),
(55,'Pinellas'),
(56,'Leon'),
(57,'Gadsden'),
(58,'Geneva'),
(59,'Ware'),
(60,'Citrus'),
(61,'Thomas'),
(62,'Wakulla'),
(63,'Sarasota'),
(64,'Jackson'),
(65,'Hardee'),
(66,'Jefferson'),
(67,'Palm Beach'),
(68,'Houston'),
(69,'Charlotte'),
(70,'Hernando'),
(71,'Osceola'),
(72,'Gulf'),
(73,'Dare'),
(74,'Okaloosa'),
(75,'Hamilton'),
(76,'Suwannee'),
(77,'St. Lucie'),
(78,'Union'),
(79,'Okeechobee'),
(80,'Holmes'),
(81,'Lafourche'),
(82,'Miami-Dade');
