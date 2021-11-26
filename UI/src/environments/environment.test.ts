export const environment = {
  production: false,
  appSettings: {
    appKey: 'BE061626-212A-43FB-9FBA-3D03A1BEA0CA',
    btoaKey: 'SESSIONID',
    userInfo: 'USERINFO',
    lockPassword: 'Letmein1.',
    session: {
      keepAlive: 900000,
      countDown: 15
    }
  },
  stringFormats: {
    currency: 'Q ###,###.00',
    date: 'dd/MM/yyyy'
  },
  api: {
    transport: {
      url: 'http://{0}:52393/',
      timeout: 900000,
      endpoint: {
        security: {
          user: {
            root: 'Seguridad/Usuario',
            action: 'Seguridad/Usuario/Acciones',
            authenticate: 'Seguridad/Usuario/Autenticar',
            delete: 'Seguridad/Usuario/Id/{0}',
            changePassword: 'Seguridad/Usuario/CambioPassword'
          },
          role: {
            root: 'Seguridad/Rol'
          }
        },
        general: {
          months: 'General/Meses',
          docType: 'General/TipoDocumento',
          transactionType: 'General/TipoTransaccion',
        },
        catalog: {
          vehicle: {
            root: 'Catalogo/Vehiculos/Vehiculo',
            getGrouped: 'Catalogo/Vehiculos/Vehiculo/Agrupado',
            getGroupedReport: 'Catalogo/Vehiculos/Vehiculo/Agrupado/Opcion/{0}',
            delete: 'Catalogo/Vehiculos/Vehiculo/Id/{0}',
            registrationType: {
              root: 'Catalogo/Vehiculos/Matricula',
              delete: 'Catalogo/Vehiculos/Matricula/Id/{0}'
            },
            type: {
              root: 'Catalogo/Vehiculos/Tipo',
              delete: 'Catalogo/Vehiculos/Tipo/Id/{0}'
            },
            brand: {
              root: 'Catalogo/Vehiculos/Marca',
              grouped: 'Catalogo/Vehiculos/Marca/Agrupado',
              delete: 'Catalogo/Vehiculos/Marca/Id/{0}'
            },
            brandModel: {
              root: 'Catalogo/Vehiculos/Marca/Modelo',
              grouped: 'Catalogo/Vehiculos/Marca/Modelo/Agrupado',
              delete: 'Catalogo/Vehiculos/Marca/Modelo/Id/{0}',
            },
            pilot: {
              root: 'Catalogo/Vehiculos/Piloto',
              delete: 'Catalogo/Vehiculos/Piloto/Id/{0}'
            }
          },
          service: {
            getGrouped: 'Catalogo/Servicios/Servicio/Agrupado',
            root: 'Catalogo/Servicios/Servicio',
            delete: 'Catalogo/Servicios/Servicio/Id/{0}',
            type: {
              root: 'Catalogo/Servicios/Tipo',
              delete: 'Catalogo/Servicios/Tipo/Id/{0}',
            }
          },
          expense: {
            getGrouped: 'Catalogo/Gastos/Gasto/Agrupado',
            root: 'Catalogo/Gastos/Gasto',
            delete: 'Catalogo/Gastos/Gasto/Id/{0}',
            type: {
              root: 'Catalogo/Gastos/Tipo',
              delete: 'Catalogo/Gastos/Tipo/Id/{0}',
            }
          }
        },
        operation: {
          transaction: {
            getFiltered: 'Operaciones/Transacciones/Anio/{0}/Mes/{1}/CodigoVehiculo/{2}',
            root: 'Operaciones/Transacciones',
            delete: 'Operaciones/Transacciones/Id/{0}'
          },
          transactionDetail: {
            getFiltered: 'Operaciones/TransaccionDetalle/Transaccion/{0}',
            root: 'Operaciones/TransaccionDetalle',
            delete: 'Operaciones/TransaccionDetalle/Id/{0}'
          }
        },
        report: {
          balance: 'Reportes/Balance/Opcion/{0}'
        }
      }
    },
    report: {
      url: 'http://{0}:55155/',
      timeout: 900000,
      endpoint: {
        balance: 'Balance/Tipo/{0}/Opcion/{1}'
      }
    }
  }
};
