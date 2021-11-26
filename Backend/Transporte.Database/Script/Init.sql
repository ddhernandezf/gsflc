INSERT INTO [VehicleType] VALUES
('Trans pesado'),
('Trans liviano');

DECLARE @TransPesado TINYINT =	(SELECT Id FROM [VehicleType] WHERE [Name] = 'Trans pesado');
UPDATE [VehicleType] SET UseTrailer = 1 WHERE [Id] = @TransPesado;

DECLARE	@Pesado INT = (SELECT Id FROM [VehicleType] WHERE [Name] = 'Trans pesado');
INSERT INTO [Brand] VALUES
(@Pesado, 'Volvo'),
(@Pesado, 'Kenworth');

DECLARE	@Volvo TINYINT = (SELECT Id FROM [Brand] WHERE [Type] = @Pesado AND [Name] = 'Volvo'),
		@Kenworth TINYINT = (SELECT Id FROM [Brand] WHERE [Type] = @Pesado AND [Name] = 'Kenworth');
INSERT INTO [BrandModel] VALUES
(@Volvo, 'VNR'),
(@Volvo, 'VNL'),
(@Volvo, 'VAH'),
(@Kenworth, 'T680'),
(@Kenworth, 'T880'),
(@Kenworth, 'T800');

INSERT INTO [RegistrationType] VALUES
('C', 'Comercial'),
('P', 'Particular'),
('M', 'Motocicletas');

INSERT INTO [ServiceType] VALUES
('Cisterna'),
('Flete');

DECLARE @Cisterna AS TINYINT =	(SELECT Id FROM ServiceType WHERE [Name] = 'Cisterna');
INSERT INTO [Service] VALUES
(@Cisterna, 'Envío de combustible'),
(@Cisterna, 'Envío de agua');

INSERT INTO [ExpenseType] VALUES
('Reparaciones'),
('Servicios'),
('Repuestos');

DECLARE @Reparacion AS TINYINT	= (SELECT Id FROM ExpenseType WHERE [Name] = 'Reparaciones'),
		@Servicio AS TINYINT	= (SELECT Id FROM ExpenseType WHERE [Name] = 'Servicios'),
		@Repuesto AS TINYINT	= (SELECT Id FROM ExpenseType WHERE [Name] = 'Repuestos');
INSERT INTO [Expense] VALUES
(@Reparacion, 'Reparación de motor'),
(@Reparacion, 'Reparación tren delantero'),
(@Reparacion, 'Reparación sistema eléctrico'),
(@Servicio, 'Servicio mayor'),
(@Servicio, 'Servicio menor'),
(@Repuesto, 'Llantas'),
(@Repuesto, 'Bateria');

INSERT INTO [Role] VALUES
('Administración'),
('Gerencia'),
('Secretaria');

INSERT INTO [Action] VALUES
('catalog', 'Catalogos', 'inactivefolder', null, 1, null),
('operations', 'Operaciones', 'cart', null, 1, null),
('security', 'Seguridad', 'card', null, 1, null);

DECLARE	@Catalog AS TINYINT = (SELECT Id FROM [Action] WHERE [Name] = 'catalog'),
		@Operations AS TINYINT = (SELECT Id FROM [Action] WHERE [Name] = 'operations'),
		@Security AS TINYINT = (SELECT Id FROM [Action] WHERE [Name] = 'security');

INSERT INTO [Action] VALUES
('vehicle', 'Vehículos', 'car', null, 1, @Catalog),
('service', 'Servicios', 'money', null, 1, @Catalog),
('expense', 'Gastos', 'percent', null, 1, @Catalog);

DECLARE	@Vehicle AS TINYINT = (SELECT Id FROM [Action] WHERE [Name] = 'vehicle'),
		@Service AS TINYINT = (SELECT Id FROM [Action] WHERE [Name] = 'service'),
		@Expense AS TINYINT = (SELECT Id FROM [Action] WHERE [Name] = 'expense');

INSERT INTO [Action] VALUES
('transactions', 'Transacciones', 'globe', 'Operaciones/Transacciones', 0, @Operations),
('users', 'Usuarios', 'group', 'Catalogos/Seguridad/Usuarios', 0, @Security),
('plateType', 'Tipo matricula', 'info', 'Catalogos/Vehiculos/TipoMatricula', 0, @Vehicle),
('pilot', 'Pilotos', 'user', 'Catalogos/Vehiculos/Pilotos', 0, @Vehicle),
('vehicles', 'Unidades', 'car', 'Catalogos/Vehiculos/Unidades', 0, @Vehicle),
('trailer', 'Remolques', 'car', 'Catalogos/Vehiculos/Remolques', 0, @Vehicle),
('servType', 'Tipo servicio', 'car', 'Catalogos/Servicios/TipoServicio', 0, @Service),
('serv', 'Servicio', 'car', 'Catalogos/Servicios/Servicio', 0, @Service),
('expType', 'Tipo gasto', 'car', 'Catalogos/Gastos/TipoGasto', 0, @Expense),
('exp', 'Gasto', 'car', 'Catalogos/Gastos/Gasto', 0, @Expense);

INSERT INTO [User] VALUES ('douglas@mitigt.com', 'Letmein1.', 'Douglas Hernández', 0, 1);

DECLARE	@RoleAdmin AS TINYINT = (SELECT Id FROM [Role] WHERE [Name]= 'Administración'),
		@Dough AS TINYINT = (SELECT Id FROM [User] WHERE [Email] = 'douglas@mitigt.com');

INSERT INTO [RoleUser] VALUES (@Dough, @RoleAdmin);

DECLARE @RoleToAction TINYINT = (SELECT Id FROM [Role] WHERE [Name] = 'Administración');

INSERT	INTO [RoleAction]
SELECT	Id, @RoleToAction
  FROM	[Action]
 WHERE	IsGroup = 0;

INSERT INTO [AppKey] VALUES ('BE061626-212A-43FB-9FBA-3D03A1BEA0CA', 'Web Application');

INSERT INTO [TrailerType] VALUES
('Cisterna combustible'),
('Cisterna agua'),
('Palanagan');

DECLARE	@Plate AS VARCHAR(1)	= (SELECT Id FROM [RegistrationType] WHERE [Name] = 'Comercial'),
		@Cis AS TINYINT			= (SELECT Id FROM [TrailerType] WHERE [Name] = 'Cisterna combustible');

INSERT INTO [Trailer] VALUES
(@Cis, @Plate, '759DMM', 'Cisterna GRIS', 1);

INSERT INTO [Pilot] VALUES (1, '1986-12-23', 'Douglas Hernández');

DECLARE @VTYPE AS TINYINT = (SELECT [Id] FROM VehicleType WHERE [Name] = 'Trans pesado');
DECLARE @VREGT AS VARCHAR(1) = (SELECT [Id] FROM RegistrationType WHERE [Name] = 'Comercial');
DECLARE @VBRAND AS TINYINT = (SELECT [Id] FROM Brand WHERE [Type] = @VTYPE AND [Name] = 'Kenworth');
DECLARE @VMODEL AS TINYINT = (SELECT [Id] FROM BrandModel WHERE [Brand] = @VBRAND AND [Name] = 'T680');
DECLARE @VPILOT AS TINYINT = (SELECT [Id] FROM Pilot WHERE CompleteName = 'Douglas Hernández' AND BornDate = '1986-12-23');

DECLARE @VTRATY AS TINYINT = (SELECT [Id] FROM TrailerType WHERE [Name] = 'Cisterna combustible');
DECLARE @VTRAILER AS TINYINT = (SELECT [Id] FROM Trailer WHERE [Type] = @VTRATY AND [Registration] = '759DMM');

INSERT INTO [Vehicle] VALUES (@VTYPE, @VREGT, '758DMM', @VBRAND, @VMODEL, 2010, @VPILOT, @VTRAILER, 1);

DECLARE @UA_CATALOG TINYINT = (SELECT Id FROM [Action] WHERE [Name] = 'catalog');
INSERT INTO [Action] VALUES ('trailers', 'Remolques', 'box', null, 1, @UA_CATALOG);
DECLARE @UA_VEHICLE TINYINT = (SELECT Id FROM [Action] WHERE IsGroup = 1 AND Parent = @UA_CATALOG AND [Name] = 'vehicle'),
		@UA_TRAILER TINYINT = (SELECT Id FROM [Action] WHERE IsGroup = 1 AND Parent = @UA_CATALOG AND [Name] = 'trailers');
UPDATE	[Action] 
   SET	Parent = @UA_TRAILER,
		[Url] = 'Catalogos/Remolques/Remolque',
		Icon = 'box'
 WHERE	Parent = @UA_VEHICLE
   AND	[Name] = 'trailer';
INSERT INTO [Action] VALUES
('trailerType', 'Tipo Remolque', 'box', 'Catalogos/Remolques/TipoRemolque', 0, @UA_TRAILER),
('brand', 'Marca', 'box', 'Catalogos/Vehiculos/Marca', 0, @UA_VEHICLE),
('brandModel', 'Modelo', 'box', 'Catalogos/Vehiculos/Modelo', 0, @UA_VEHICLE),
('vehicleType', 'Tipo Vehículo', 'car', 'Catalogos/Vehiculos/TipoVehiculo', 0, @UA_VEHICLE);
UPDATE	[Action] 
   SET	[Url] = 'Catalogos/Vehiculos/Vehiculo',
		Icon = 'car',
		[Text] = 'Vehículo'
 WHERE	Parent = @UA_VEHICLE
   AND	[Name] = 'vehicles';