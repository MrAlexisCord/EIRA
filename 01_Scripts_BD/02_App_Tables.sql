USE EIRA
GO
CREATE TABLE APP_CONFIGURATION_LOAD_INFORMATION
( PROJECT_ID		VARCHAR(128) NOT NULL
, FIELD_ID			VARCHAR(128) NOT NULL
, ORDER_NUMBER		INT DEFAULT 1 NOT NULL
, IS_ACTIVE         BIT DEFAULT 1 NOT NULL
, CREATION_AT		DATETIME DEFAULT (GETDATE()) NOT NULL
, USER_AT			VARCHAR(128) NOT NULL
, UPDATED_AT		DATETIME DEFAULT (GETDATE()) NOT NULL
, UPDATE_USER		VARCHAR(128) NOT NULL
, CONSTRAINT PK_APP_CONFIGURATION_LOAD_INFORMATION PRIMARY KEY (PROJECT_ID, FIELD_ID)
, CONSTRAINT FK_APP_CONFIGURATION_LOAD_INFORMATION_PROJECT_ID FOREIGN KEY (PROJECT_ID) REFERENCES BAS_PROJECT (PROJECT_ID)
, CONSTRAINT FK_APP_CONFIGURATION_LOAD_INFORMATION_FIELD_ID FOREIGN KEY (FIELD_ID) REFERENCES BAS_FIELD (FIELD_ID)
);
GO
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Configuración de los Campos por Proyecto para Carga de Información', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_LOAD_INFORMATION';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Identificador Único del Proyecto', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_LOAD_INFORMATION', 
  @level2type = N'Column', @level2name = N'PROJECT_ID';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Identificador Único del Campo', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_LOAD_INFORMATION', 
  @level2type = N'Column', @level2name = N'FIELD_ID';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Orden', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_LOAD_INFORMATION', 
  @level2type = N'Column', @level2name = N'ORDER_NUMBER';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'¿Esta Activo? (1-True, 0-False)', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_LOAD_INFORMATION', 
  @level2type = N'Column', @level2name = N'IS_ACTIVE';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Fecha de Creación del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_LOAD_INFORMATION', 
  @level2type = N'Column', @level2name = N'CREATION_AT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Usuario que Realizó la Creación del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_LOAD_INFORMATION', 
  @level2type = N'Column', @level2name = N'USER_AT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Fecha de la Última Actualización del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_LOAD_INFORMATION', 
  @level2type = N'Column', @level2name = N'UPDATED_AT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Usuario que Realizó la Última Actualización del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_LOAD_INFORMATION', 
  @level2type = N'Column', @level2name = N'UPDATE_USER';
GO
CREATE TABLE APP_CONFIGURATION_FOLLOW_UP_REPORT
( PROJECT_ID		VARCHAR(128) NOT NULL
, FIELD_ID			VARCHAR(128) NOT NULL
, ORDER_NUMBER		INT DEFAULT 1 NOT NULL
, IS_ACTIVE         BIT DEFAULT 1 NOT NULL
, CREATION_AT		DATETIME DEFAULT (GETDATE()) NOT NULL
, USER_AT			VARCHAR(128) NOT NULL
, UPDATED_AT		DATETIME DEFAULT (GETDATE()) NOT NULL
, UPDATE_USER		VARCHAR(128) NOT NULL
, CONSTRAINT PK_APP_CONFIGURATION_FOLLOW_UP_REPORT PRIMARY KEY (PROJECT_ID, FIELD_ID)
, CONSTRAINT FK_APP_CONFIGURATION_FOLLOW_UP_REPORT_PROJECT_ID FOREIGN KEY (PROJECT_ID) REFERENCES BAS_PROJECT (PROJECT_ID)
, CONSTRAINT FK_APP_CONFIGURATION_FOLLOW_UP_REPORT_FIELD_ID FOREIGN KEY (FIELD_ID) REFERENCES BAS_FIELD (FIELD_ID)
);
GO
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Configuración de los Campos por Proyecto para el Reporte de Seguimiento', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_FOLLOW_UP_REPORT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Identificador Único del Proyecto', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_FOLLOW_UP_REPORT', 
  @level2type = N'Column', @level2name = N'PROJECT_ID';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Identificador Único del Campo', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_FOLLOW_UP_REPORT', 
  @level2type = N'Column', @level2name = N'FIELD_ID';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Orden', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_FOLLOW_UP_REPORT', 
  @level2type = N'Column', @level2name = N'ORDER_NUMBER';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'¿Esta Activo? (1-True, 0-False)', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_FOLLOW_UP_REPORT', 
  @level2type = N'Column', @level2name = N'IS_ACTIVE';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Fecha de Creación del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_FOLLOW_UP_REPORT', 
  @level2type = N'Column', @level2name = N'CREATION_AT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Usuario que Realizó la Creación del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_FOLLOW_UP_REPORT', 
  @level2type = N'Column', @level2name = N'USER_AT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Fecha de la Última Actualización del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_FOLLOW_UP_REPORT', 
  @level2type = N'Column', @level2name = N'UPDATED_AT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Usuario que Realizó la Última Actualización del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_FOLLOW_UP_REPORT', 
  @level2type = N'Column', @level2name = N'UPDATE_USER';
GO
CREATE TABLE APP_CONFIGURATION_GLOBAL_REPORT
( PROJECT_ID		VARCHAR(128) NOT NULL
, FIELD_ID			VARCHAR(128) NOT NULL
, ORDER_NUMBER		INT DEFAULT 1 NOT NULL
, IS_ACTIVE         BIT DEFAULT 1 NOT NULL
, CREATION_AT		DATETIME DEFAULT (GETDATE()) NOT NULL
, USER_AT			VARCHAR(128) NOT NULL
, UPDATED_AT		DATETIME DEFAULT (GETDATE()) NOT NULL
, UPDATE_USER		VARCHAR(128) NOT NULL
, CONSTRAINT PK_APP_CONFIGURATION_GLOBAL_REPORT PRIMARY KEY (PROJECT_ID, FIELD_ID)
, CONSTRAINT FK_APP_CONFIGURATION_GLOBAL_REPORT_PROJECT_ID FOREIGN KEY (PROJECT_ID) REFERENCES BAS_PROJECT (PROJECT_ID)
, CONSTRAINT FK_APP_CONFIGURATION_GLOBAL_REPORT_FIELD_ID FOREIGN KEY (FIELD_ID) REFERENCES BAS_FIELD (FIELD_ID)
);
GO
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Configuración de los Campos por Proyecto para el Reporte Global', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_GLOBAL_REPORT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Identificador Único del Proyecto', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_GLOBAL_REPORT', 
  @level2type = N'Column', @level2name = N'PROJECT_ID';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Identificador Único del Campo', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_GLOBAL_REPORT', 
  @level2type = N'Column', @level2name = N'FIELD_ID';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Orden', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_GLOBAL_REPORT', 
  @level2type = N'Column', @level2name = N'ORDER_NUMBER';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'¿Esta Activo? (1-True, 0-False)', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_GLOBAL_REPORT', 
  @level2type = N'Column', @level2name = N'IS_ACTIVE';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Fecha de Creación del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_GLOBAL_REPORT', 
  @level2type = N'Column', @level2name = N'CREATION_AT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Usuario que Realizó la Creación del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_GLOBAL_REPORT', 
  @level2type = N'Column', @level2name = N'USER_AT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Fecha de la Última Actualización del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_GLOBAL_REPORT', 
  @level2type = N'Column', @level2name = N'UPDATED_AT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Usuario que Realizó la Última Actualización del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_GLOBAL_REPORT', 
  @level2type = N'Column', @level2name = N'UPDATE_USER';
GO
CREATE TABLE APP_CONFIGURATION_ISSUE_TYPE
( PROJECT_ID		VARCHAR(128) NOT NULL
, ISSUE_TYPE_ID		INT NOT NULL
, FIELD_VALUE_NAME	VARCHAR(128) NOT NULL
, IS_ACTIVE         BIT DEFAULT 1 NOT NULL
, CREATION_AT		DATETIME DEFAULT (GETDATE()) NOT NULL
, USER_AT			VARCHAR(128) NOT NULL
, UPDATED_AT		DATETIME DEFAULT (GETDATE()) NOT NULL
, UPDATE_USER		VARCHAR(128) NOT NULL
, CONSTRAINT PK_APP_CONFIGURATION_ISSUE_TYPE PRIMARY KEY (PROJECT_ID, ISSUE_TYPE_ID)
, CONSTRAINT FK_APP_CONFIGURATION_ISSUE_TYPE_PROJECT_ID FOREIGN KEY (PROJECT_ID) REFERENCES BAS_PROJECT (PROJECT_ID)
, CONSTRAINT FK_APP_CONFIGURATION_ISSUE_TYPE_ISSUE_TYPE_ID FOREIGN KEY (ISSUE_TYPE_ID) REFERENCES BAS_ISSUE_TYPE (ISSUE_TYPE_ID)
);
GO
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Configuración de Issue Type por Proyecto', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_ISSUE_TYPE';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Identificador Único del Proyecto', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_ISSUE_TYPE', 
  @level2type = N'Column', @level2name = N'PROJECT_ID';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Identificador Único del Tipo de Incidente', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_ISSUE_TYPE', 
  @level2type = N'Column', @level2name = N'ISSUE_TYPE_ID';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Nombre del campo de Tipo de Incidente', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_ISSUE_TYPE', 
  @level2type = N'Column', @level2name = N'FIELD_VALUE_NAME'
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'¿Esta Activo? (1-True, 0-False)', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_ISSUE_TYPE', 
  @level2type = N'Column', @level2name = N'IS_ACTIVE';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Fecha de Creación del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_ISSUE_TYPE', 
  @level2type = N'Column', @level2name = N'CREATION_AT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Usuario que Realizó la Creación del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_ISSUE_TYPE', 
  @level2type = N'Column', @level2name = N'USER_AT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Fecha de la Última Actualización del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_ISSUE_TYPE', 
  @level2type = N'Column', @level2name = N'UPDATED_AT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Usuario que Realizó la Última Actualización del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'APP_CONFIGURATION_ISSUE_TYPE', 
  @level2type = N'Column', @level2name = N'UPDATE_USER';
GO