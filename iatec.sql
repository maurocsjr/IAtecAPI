CREATE DATABASE  IF NOT EXISTS `iatec`;
USE `iatec`;

DROP TABLE IF EXISTS `pessoas`;

CREATE TABLE `pessoas` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(100) NOT NULL,
  `sobrenome` varchar(250) NOT NULL,
  `data_nascimento` datetime NOT NULL,
  `cpf` varchar(11) NOT NULL,
  `email` varchar(255) NOT NULL,
  `sexo` varchar(1) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


LOCK TABLES `pessoas` WRITE;
INSERT INTO `pessoas` VALUES (2,'Calebe','Silva','1998-02-01 00:00:00','00988452354','calebe.silva@iatec.com','M'),(3,'Beatriz','Carvalho','1998-02-04 00:00:00','13589655283','beatriz.silva@adventistas.org','F'),(4,'Katia','Regina Carvalho','1971-03-01 00:00:00','10888471297','katia.regina@uol.com.br','F'),(5,'Mauro','Carvalho','1998-08-19 00:00:00','39524261057','mauro.junior@adventistas.org','M');
UNLOCK TABLES;


DROP TABLE IF EXISTS `telefones`;

CREATE TABLE `telefones` (
  `id` int NOT NULL AUTO_INCREMENT,
  `id_pessoa` int NOT NULL,
  `telefone` varchar(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `id_pessoa_telefones_fk_idx` (`id_pessoa`),
  CONSTRAINT `id_pessoa_telefones_fk` FOREIGN KEY (`id_pessoa`) REFERENCES `pessoas` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

LOCK TABLES `telefones` WRITE;
INSERT INTO `telefones` VALUES (2,2,'62999522121'),(5,3,'62555423259'),(6,4,'41996142552'),(7,5,'62985356666'),(8,5,'6240127777'),(10,2,'6240127700');
UNLOCK TABLES;