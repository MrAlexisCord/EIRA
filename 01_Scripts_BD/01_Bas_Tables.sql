USE EIRA
GO
CREATE TABLE BAS_FIELD
( FIELD_ID			VARCHAR(128) NOT NULL
, FIELD_KEY			VARCHAR(128) NOT NULL
, NAME				VARCHAR(256) NOT NULL
, FIELD_TYPE		VARCHAR(128)
, IS_ACTIVE         BIT DEFAULT 1 NOT NULL
, CREATION_AT		DATETIME DEFAULT (GETDATE()) NOT NULL
, USER_AT			VARCHAR(128) NOT NULL
, UPDATED_AT		DATETIME DEFAULT (GETDATE()) NOT NULL
, UPDATE_USER		VARCHAR(128) NOT NULL
, CONSTRAINT PK_BAS_FIELD PRIMARY KEY (FIELD_ID));
GO
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Configuración de los campos', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_FIELD';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Identificador Único del Campo', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_FIELD', 
  @level2type = N'Column', @level2name = N'FIELD_ID';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Llave del Campo', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_FIELD', 
  @level2type = N'Column', @level2name = N'FIELD_KEY';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Nombre o Descripción del Campo', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_FIELD', 
  @level2type = N'Column', @level2name = N'NAME';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Tipo del Campo', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_FIELD', 
  @level2type = N'Column', @level2name = N'FIELD_TYPE';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'¿Esta Activo? (1-True, 0-False)', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_FIELD', 
  @level2type = N'Column', @level2name = N'IS_ACTIVE';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Fecha de Creación del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_FIELD', 
  @level2type = N'Column', @level2name = N'CREATION_AT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Usuario que Realizó la Creación del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_FIELD', 
  @level2type = N'Column', @level2name = N'USER_AT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Fecha de la Última Actualización del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_FIELD', 
  @level2type = N'Column', @level2name = N'UPDATED_AT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Usuario que Realizó la Última Actualización del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_FIELD', 
  @level2type = N'Column', @level2name = N'UPDATE_USER';
GO
CREATE TABLE BAS_PROJECT
( PROJECT_ID		VARCHAR(128) NOT NULL
, PROJECT_KEY		VARCHAR(128) NOT NULL
, NAME				VARCHAR(256) NOT NULL
, IS_ACTIVE         BIT DEFAULT 1 NOT NULL
, CREATION_AT		DATETIME DEFAULT (GETDATE()) NOT NULL
, USER_AT			VARCHAR(128) NOT NULL
, UPDATED_AT		DATETIME DEFAULT (GETDATE()) NOT NULL
, UPDATE_USER		VARCHAR(128) NOT NULL
, CONSTRAINT PK_BAS_PROJECT PRIMARY KEY (PROJECT_ID));
GO
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Configuración de los Proyectos', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_PROJECT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Identificador Único del Proyecto', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_PROJECT', 
  @level2type = N'Column', @level2name = N'PROJECT_ID';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Llave del Proyecto', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_PROJECT', 
  @level2type = N'Column', @level2name = N'PROJECT_KEY';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Nombre o Descripción del Proyecto', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_PROJECT', 
  @level2type = N'Column', @level2name = N'NAME';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'¿Esta Activo? (1-True, 0-False)', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_PROJECT', 
  @level2type = N'Column', @level2name = N'IS_ACTIVE';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Fecha de Creación del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_PROJECT', 
  @level2type = N'Column', @level2name = N'CREATION_AT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Usuario que Realizó la Creación del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_PROJECT', 
  @level2type = N'Column', @level2name = N'USER_AT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Fecha de la Última Actualización del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_PROJECT', 
  @level2type = N'Column', @level2name = N'UPDATED_AT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Usuario que Realizó la Última Actualización del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_PROJECT', 
  @level2type = N'Column', @level2name = N'UPDATE_USER';
GO
CREATE TABLE BAS_ISSUE_TYPE
( ISSUE_TYPE_ID		INT NOT NULL IDENTITY
, ISSUE_TYPE_NAME	VARCHAR(128) NOT NULL
, IS_ACTIVE         BIT DEFAULT 1 NOT NULL
, CREATION_AT		DATETIME DEFAULT (GETDATE()) NOT NULL
, USER_AT			VARCHAR(128) NOT NULL
, UPDATED_AT		DATETIME DEFAULT (GETDATE()) NOT NULL
, UPDATE_USER		VARCHAR(128) NOT NULL
, CONSTRAINT PK_BAS_ISSUE_TYPE PRIMARY KEY (ISSUE_TYPE_ID));
GO
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Configuración de los Proyectos', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_ISSUE_TYPE';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Identificador Único del Tipo de Incidente', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_ISSUE_TYPE', 
  @level2type = N'Column', @level2name = N'ISSUE_TYPE_ID';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Nombre o descripción del Tipo de Incidente', 
  @level0type = N'Schema',@level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_ISSUE_TYPE', 
  @level2type = N'Column', @level2name = N'ISSUE_TYPE_NAME';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'¿Esta Activo? (1-True, 0-False)', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_ISSUE_TYPE', 
  @level2type = N'Column', @level2name = N'IS_ACTIVE';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Fecha de Creación del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_ISSUE_TYPE', 
  @level2type = N'Column', @level2name = N'CREATION_AT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Usuario que Realizó la Creación del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_ISSUE_TYPE', 
  @level2type = N'Column', @level2name = N'USER_AT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Fecha de la Última Actualización del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_ISSUE_TYPE', 
  @level2type = N'Column', @level2name = N'UPDATED_AT';
EXEC sp_addextendedproperty 
  @name = N'MS_Description', @value = N'Usuario que Realizó la Última Actualización del Registro', 
  @level0type = N'Schema', @level0name = N'dbo', 
  @level1type = N'Table', @level1name = N'BAS_ISSUE_TYPE', 
  @level2type = N'Column', @level2name = N'UPDATE_USER';
GO