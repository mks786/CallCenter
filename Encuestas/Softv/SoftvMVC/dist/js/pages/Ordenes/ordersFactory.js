'use strict';

ordersApp.factory('ordersFactory', function ($http, $q) {
    var factory = {};
    var paths = {
        list: "Municipio/GetMunicipioList",
        detailOrder: "/Ordenes/getDetailOrder/",
        addPre: "/Ordenes/preGuardado/",
        update: "Municipio/UpdateRelEstMunL",
        remove: "Municipio/DeleteMunicipio",
        orders: "/Ordenes/getListOrders/",
        places: "/Conexion/Plazas/",
        counters: "/Ordenes/getCounters/",
        dataClient: "/Ordenes/getDatosClientes/",
        dataTecnicos: "/Ordenes/getDataTecnicos/",
        dataServicios: "/Ordenes/getServiciosContratados/",
        dataTrabajos: "/Ordenes/getTrabajos/",
        dataActivo: "/Ordenes/getServicioActivo/",
        allOrdes: "/Ordenes/getAllOrders/",
        saveDetailOrder: "/Ordenes/guardarDetalleOrden/",
        deleteDetailOrder: "/Ordenes/deleteDetailOrder/",
        cancelOrder: "/Ordenes/cancelOrder/",
        saveOrder: "/Ordenes/SaveOrder/",
        getCiudades: "/CIUDAD/GetCiudad/",
        getColonia: "/COLONIA/GetColoniaByCiudad/",
        getCalle: "/CALLE/GetCalleByColonia/",
        saveCambioDomicilio: "/Ordenes/saveCambioDomicilio/",
        getExtensiones: "/Ordenes/getExtensiones/",
        cancelExtensiones: "/Ordenes/saveExtensiones/",
        saveExtensiones: "/Ordenes/saveExtensiones/",
        getCablemodems: "/Ordenes/getCablemodem/",
        bajaPaquete: "/Ordenes/bajaPaquete/",
        consultarDetalleOrden: "/Ordenes/consultarDetalleOrden/",
        detalleCamdo: "/Ordenes/detalleCamdo/",
        detalleConet: "/Ordenes/detalleConet/",
        tieneCanexConex: "/Ordenes/tieneCanexConex/",
        motivosCancelacion: "/Ordenes/motivosCancelacion/",
        guardarMotivo: "/Ordenes/guardarMotivo/",
        getBitacoraDescarga: "/Ordenes/getBitacoraDescarga/",
        getArticulosDescarga: "/Ordenes/getArticulosDescarga/",
        consultarExistencia: "/Ordenes/consultarExistencia/",
        guardarMaterial: "/Ordenes/guardarMaterial/",
        detalleArticulosTabla: "/Ordenes/detalleArticulosTabla/",
        getSession: "/Ordenes/getSession/",
        eliminarMaterial: "/Ordenes/eliminarMaterial",
        eliminarTodosArticulos: "/Ordenes/eliminarTodosArticulos/",
        guardarDescargaMaterial: "/Ordenes/guardarDescargaMaterial/",
        eliminarArticulosTabla: "/Ordenes/eliminarArticulosTabla/",
        consultarArticulosTabla: "/Ordenes/consultarArticulosTabla/",
        addArticuloExtensiones: "/Ordenes/addArticuloExtensiones/",
        consultarArticulosTablaExtensiones: "/Ordenes/consultarArticulosTablaExtensiones/",
        eliminarMaterialExtensiones: "/Ordenes/eliminarMaterialExtensiones/",
        eliminarTodoMaterialExtensiones: "/Ordenes/eliminarTodoMaterialExtensiones/",
        consultarExtencionesArticulosDetalle: "/Ordenes/consultarExtencionesArticulosDetalle/",
        generarBPAQU: "/Ordenes/generarBPAQU/"
    };

    factory.getList = function () {
        var deferred = $q.defer();
        $http.get(paths.list).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.addArticuloExtensiones = function (idPlaza, Orden, Articulo, Tecnico, Almacen, Mii, Mfi, Mie, Mfe, Cantidad, Extension) {
        var deferred = $q.defer();
        $http.get(paths.addArticuloExtensiones, { params: { idPlaza: idPlaza, Orden: Orden, Almacen: Almacen, Articulo: Articulo, Tecnico: Tecnico, Cantidad: Cantidad, Mii: Mii, Mfi: Mfi, Mie: Mie, Mfe: Mfe, Extension: Extension } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.generarBPAQU = function (idPlaza,Contrato) {
        var deferred = $q.defer();
        $http.get(paths.generarBPAQU, { params: { idPlaza: idPlaza,Contrato: Contrato } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.getDataClient = function (idPlaza, Contrato) {
        var deferred = $q.defer();
        $http.get(paths.dataClient, { params: { idPlaza: idPlaza, Contrato: Contrato } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.eliminarTodosArticulos = function (idPlaza, Orden) {
        var deferred = $q.defer();
        $http.get(paths.eliminarTodosArticulos, { params: { idPlaza: idPlaza, Orden: Orden } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.consultarExtencionesArticulosDetalle = function (idPlaza, Orden) {
        var deferred = $q.defer();
        $http.get(paths.consultarExtencionesArticulosDetalle, { params: { idPlaza: idPlaza, Orden: Orden } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.eliminarTodoMaterialExtensiones = function (idPlaza, Orden) {
        var deferred = $q.defer();
        $http.get(paths.eliminarTodoMaterialExtensiones, { params: { idPlaza: idPlaza, Orden: Orden } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.eliminarMaterialExtensiones = function (idPlaza, ID) {
        var deferred = $q.defer();
        $http.get(paths.eliminarMaterialExtensiones, { params: { idPlaza: idPlaza, ID: ID } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.consultarArticulosTablaExtensiones = function (idPlaza, Orden) {
        var deferred = $q.defer();
        $http.get(paths.consultarArticulosTablaExtensiones, { params: { idPlaza: idPlaza, Orden: Orden } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.eliminarArticulosTabla = function (idPlaza, Orden) {
        var deferred = $q.defer();
        $http.get(paths.eliminarArticulosTabla, { params: { idPlaza: idPlaza, Orden: Orden } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };


    factory.consultarArticulosTabla = function (idPlaza, Orden) {
        var deferred = $q.defer();
        $http.get(paths.consultarArticulosTabla, { params: { idPlaza: idPlaza, Orden: Orden } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.getSession = function (idPlaza) {
        var deferred = $q.defer();
        $http.get(paths.getSession, { params: { idPlaza: idPlaza } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.detalleArticulosTabla = function (idPlaza, Orden, Session) {
        var deferred = $q.defer();
        $http.get(paths.detalleArticulosTabla, { params: { idPlaza: idPlaza, Orden: Orden, Session:Session } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.eliminarMaterial = function (idPlaza, ID) {
        var deferred = $q.defer();
        $http.get(paths.eliminarMaterial, { params: { idPlaza: idPlaza, ID: ID } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.guardarMotivo = function (idPlaza, Orden, Motivo) {
        var deferred = $q.defer();
        $http.get(paths.guardarMotivo, { params: { idPlaza: idPlaza, Orden: Orden, Motivo:Motivo } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.getArticulosDescarga = function (idPlaza, Tecnico, Clasificacion) {
        var deferred = $q.defer();
        $http.get(paths.getArticulosDescarga, { params: { idPlaza: idPlaza, Tecnico: Tecnico, Clasificacion: Clasificacion } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.getBitacoraDescarga = function (idPlaza, Orden) {
        var deferred = $q.defer();
        $http.get(paths.getBitacoraDescarga, { params: { idPlaza: idPlaza, Orden: Orden } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.consultarExistencia = function (idPlaza, Tecnico, Articulo, Cantidad) {
        var deferred = $q.defer();
        $http.get(paths.consultarExistencia, { params: { idPlaza: idPlaza, Tecnico: Tecnico, Articulo: Articulo, Cantidad: Cantidad } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.motivosCancelacion = function (idPlaza) {
        var deferred = $q.defer();
        $http.get(paths.motivosCancelacion, { params: { idPlaza: idPlaza } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.tieneCanexConex = function (idPlaza, Orden) {
        var deferred = $q.defer();
        $http.get(paths.tieneCanexConex, { params: { idPlaza: idPlaza, Orden: Orden } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.consultarDetalleOrden = function (idPlaza, Orden) {
        var deferred = $q.defer();
        $http.get(paths.consultarDetalleOrden, { params: { idPlaza: idPlaza, Orden: Orden } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.getCablemodems = function (idPlaza, Contrato) {
        var deferred = $q.defer();
        $http.get(paths.getCablemodems, { params: { idPlaza: idPlaza, Contrato: Contrato } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };
    
    factory.detalleConet = function (idPlaza, Clave, Orden, Contrato) {
        var deferred = $q.defer();
        $http.get(paths.detalleConet, { params: { idPlaza: idPlaza, Contrato: Contrato, Clave: Clave, Orden: Orden } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.detalleCamdo = function (idPlaza, Clave, Orden, Contrato) {
        var deferred = $q.defer();
        $http.get(paths.detalleCamdo, { params: { idPlaza: idPlaza, Contrato: Contrato, Clave: Clave, Orden: Orden } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.cancelExtensiones = function (idPlaza, Clave, Orden, Contrato, Extensiones) {
        var deferred = $q.defer();
        $http.get(paths.cancelExtensiones, { params: { idPlaza: idPlaza, Contrato: Contrato, Clave: Clave, Orden: Orden, Extensiones: Extensiones } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.bajaPaquete = function (Objeto) {
        var deferred = $q.defer();
        $http.post(paths.bajaPaquete, Objeto).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.guardarDescargaMaterial = function (Descarga) {
        var deferred = $q.defer();
        $http.post(paths.guardarDescargaMaterial, Descarga).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.saveExtensiones = function (idPlaza, Clave, Orden, Contrato, Extensiones) {
        var deferred = $q.defer();
        $http.get(paths.saveExtensiones, { params: { idPlaza: idPlaza, Contrato: Contrato, Clave: Clave, Orden: Orden, Extensiones: Extensiones } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.guardarMaterial = function (idPlaza, Orden, Almacen, ClaveArticulo, Articulo, Tecnico, cantidad, Mii, Mfi, Mei, Mef, Session) {
        console.log(idPlaza, Orden, Almacen, ClaveArticulo, Articulo, Tecnico, cantidad, Mii, Mfi, Mei, Mef, Session);
        var deferred = $q.defer();
        $http.get(paths.guardarMaterial, { params: { idPlaza: idPlaza, Orden: Orden, Almacen: Almacen, ClaveArticulo: ClaveArticulo, Articulo: Articulo, Tecnico: Tecnico, cantidad: cantidad, Mii: Mii, Mfi: Mfi, Mei: Mei, Mef: Mef, Session: Session } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.getExtensiones = function (idPlaza, Contrato) {
        var deferred = $q.defer();
        $http.get(paths.getExtensiones, { params: { idPlaza: idPlaza, Contrato: Contrato } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.getCalle = function (idPlaza, Colonia) {
        var deferred = $q.defer();
        $http.get(paths.getCalle, { params: { colonia: Colonia, plaza: idPlaza } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.getColonia = function (idPlaza, Ciudad) {
        var deferred = $q.defer();
        $http.get(paths.getColonia, { params: { idciudad: Ciudad, plaza: idPlaza } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.saveCambioDomicilio = function (idPlaza, Clave, Orden, Contrato, Ciudad, Colonia, Calle, Numero, Telefono, entreCalles, NumInt) {
        var deferred = $q.defer();
        $http.get(paths.saveCambioDomicilio, { params: { idPlaza: idPlaza, Clave: Clave, Orden: Orden, Contrato: Contrato, Ciudad: Ciudad, Colonia: Colonia, Calle: Calle, Numero: Numero, Telefono: Telefono, entreCalles: entreCalles, NumInt: NumInt } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.getCiudades = function (idPlaza) {
        var deferred = $q.defer();
        $http.get(paths.getCiudades, { params: { idConexion: idPlaza } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.cancelOrder = function (idPlaza, Orden) {
        var deferred = $q.defer();
        $http.get(paths.cancelOrder, { params: { idPlaza: idPlaza, Orden: Orden } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.deleteDetailOrder = function (idPlaza, Clave) {
        var deferred = $q.defer();
        $http.get(paths.deleteDetailOrder, { params: { idPlaza: idPlaza, Clave: Clave } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.getAllOrders = function (idPlaza, Orden) {
        var deferred = $q.defer();
        $http.get(paths.allOrdes, { params: { idPlaza: idPlaza, Orden: Orden } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.saveDetailOrder = function (idPlaza, Orden, Trabajo, Observaciones, Realiza, Servicio) {
        var deferred = $q.defer();
        $http.get(paths.saveDetailOrder, { params: { idPlaza: idPlaza, Orden: Orden, Observaciones: Observaciones, Trabajo: Trabajo, Realiza: Realiza, Servicio: Servicio } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.saveOrder = function (idPlaza, Obse, Orden, Usuario) {
        var deferred = $q.defer();
        $http.get(paths.saveOrder, { params: { idPlaza: idPlaza, Obse: Obse, Orden: Orden, Usuario: Usuario } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.addPre = function (idPlaza, Contrato, Observaciones) {
        var deferred = $q.defer();
        $http.get(paths.addPre, { params: { idPlaza: idPlaza, Contrato: Contrato, Observaciones: Observaciones } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.getDataJobs = function (idPlaza, Servicio) {
        var deferred = $q.defer();
        $http.get(paths.dataTrabajos, { params: { idPlaza: idPlaza, Servicio: Servicio } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.getDataActivo = function (idPlaza, Servicio, Contrato) {
        var deferred = $q.defer();
        $http.get(paths.dataActivo, { params: { idPlaza: idPlaza, Servicio: Servicio, Contrato: Contrato } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.getDataServices = function (idPlaza, Contrato) {
        var deferred = $q.defer();
        $http.get(paths.dataServicios, { params: { idPlaza: idPlaza, Contrato: Contrato } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.getDataTecnicos = function (idPlaza) {
        var deferred = $q.defer();
        $http.get(paths.dataTecnicos, { params: { idPlaza: idPlaza } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.getCounters = function (idPlaza) {
        var deferred = $q.defer();
        $http.get(paths.counters, { params: { idPlaza: idPlaza} }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.getDetailOrders = function (idPlaza,Contrato) {
        var deferred = $q.defer();
        $http.get(paths.detailOrder, { params: { idPlaza: idPlaza, Contrato:Contrato } }).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    factory.getListPlaces = function () {
        var deferred = $q.defer();
        $http.get(paths.places).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };


    return factory;
});