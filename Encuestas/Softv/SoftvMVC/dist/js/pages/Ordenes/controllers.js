angular
    .module('ordersApp')
    .controller('showOrders', showOrders)
    .controller('ModalAddCtrl', ModalAddCtrl)
    .controller('ModalServiceCtrl', ModalServiceCtrl)
    .controller('ModalDomicilioCtrl', ModalDomicilioCtrl)
    .controller('ModalExtensionCtrl', ModalExtensionCtrl)
    .controller('ModalExtensionAddCtrl', ModalExtensionAddCtrl)
    .controller('ModalExtensionActualizarCtrl', ModalExtensionActualizarCtrl)
    .controller('ModalBajaPaqueteCtrl', ModalBajaPaqueteCtrl)
    .controller('ModalDomicilioNetCtrl', ModalDomicilioNetCtrl)
    .controller('ModalDesconexionPaqueteCtrl', ModalDesconexionPaqueteCtrl)
    .controller('ModalInstalacionPaqueteCtrl', ModalInstalacionPaqueteCtrl)
    .controller('ModalReconexionPaqueteCtrl', ModalReconexionPaqueteCtrl)
    .controller('ModalconsultarOrdenCtrl', ModalconsultarOrdenCtrl)
    .controller('ModalDomicilioDetalleCtrl', ModalDomicilioDetalleCtrl)
    .controller('ModalExtensionDetalleCtrl', ModalExtensionDetalleCtrl)
    .controller('MotivoCancelacionCtrl', MotivoCancelacionCtrl)
    .controller('ModalEjecutarOrdenCtrl', ModalEjecutarOrdenCtrl)
    .controller('ModalDescargaMaterialCtrl', ModalDescargaMaterialCtrl)
    .controller('ModalDescargaMaterialDetalleCtrl', ModalDescargaMaterialDetalleCtrl)
    .controller('descargaExtensionesCtrl', descargaExtensionesCtrl)
    .controller('descargaExtensionesDestalleCtrl', descargaExtensionesDestalleCtrl)
    .controller('ModalAsignacionCtrl', ModalAsignacionCtrl)
    .controller('bajaPaqueteDeatelleCtrl', bajaPaqueteDeatelleCtrl);

function showOrders(ordersFactory, $scope, $uibModal, $log, $rootScope) {
    var vm = this;
    vm.showPanels = true;
    vm.idPlaza;
    vm.showBtnNuevo = true;
    vm.showBtnBuscar = true;
    vm.showBtnEjecutar = true;
    vm.showBtnConsultar = true;
    vm.buscarOrden = buscarOrden;
    vm.changePlace = changePlace;
    vm.open = open;
    vm.openConsultar = openConsultar;
    vm.openEjecutar = openEjecutar;

    ordersFactory.getListPlaces().then(function (data) {
        data.unshift({
            "Plaza": "----------------",
            "IdConexion": 0
        });
        vm.places = data;
        vm.selectedPlace = vm.places[0];
    });
    
    
    function buscarOrden(id) {
        if(id == 1){
            LlenarTabla(vm.idPlaza, vm.conOrd, '', '', '', '', '', '');
            reset();
        } else if(id == 2) {
            LlenarTabla(vm.idPlaza, '', vm.status, '','', '', '', '');
            reset();
        } else if (id == 3) {
            LlenarTabla(vm.idPlaza, '', '', vm.nombreCliente, vm.apellidoPCliente, vm.apellidoMCliente, '', '');
            reset();
        }else if(id == 4){
            LlenarTabla(vm.idPlaza, '', '', '', '', '', vm.calle, vm.numero);
            reset();
        }
        $('.collapse').collapse('hide');
    }

    function changePlace() {
        reset();
        vm.idPlaza = vm.selectedPlace.IdConexion;
        if (vm.idPlaza != 0) {
            LlenarTabla(vm.idPlaza,'','','','','');
            ordersFactory.getCounters(vm.idPlaza).then(function (data) {
                vm.pendientes = data.pendientes;
                vm.proceso = data.proceso;
            });
            vm.showBtnBuscar = false;
            vm.showBtnNuevo = false;
        } else {
            vm.showPanels = true;
            vm.showBtnBuscar = true;
            vm.showBtnNuevo = true;
        }
    }
    

    $scope.getDetailsOrder = function (contrato, status) {
        if (vm.idPlaza == 0) {
            new PNotify({
                title: 'Selecciona una plaza',
                text: 'Debes seleccionar una plaza para poder ver el detalle de la órden.',
                icon: 'fa fa-info-circle',
                type: 'error',
                hide: true
            });
        } else {
            ordersFactory.getDetailOrders(vm.idPlaza, contrato)
                .then(function (data) {
                    vm.showBtnConsultar = false;
                    if (status == "P") {
                        vm.showBtnEjecutar = false;
                    } else {
                        vm.showBtnEjecutar = true;
                    }
                    vm.Clv_Orden = data.Clv_Orden;
                    vm.Status = data.Status;
                    vm.Nombre = data.Nombre;
                    vm.Contrato = data.Contrato;
                    vm.Calle = data.Calle;
                    vm.Numero = data.Numero;
                    vm.Descripciones = data.listDescripcion;
                });
        }     
    }

    function reset() {
        vm.Clv_Orden = "";
        vm.Status = "";
        vm.Nombre = "";
        vm.Contrato = "";
        vm.Calle = "";
        vm.Numero = "";
        vm.Descripcion = "";
        vm.conOrd = "";
        vm.status = "";
        vm.nombreCliente = "";
        vm.calle = "";
        vm.numero = "";
        vm.apellidoMCliente = "";
        vm.apellidoPCliente = "";
        vm.Descripciones = "";
    }

    function open(size) {
        vm.animationsEnabled = true;
        var modalInstance = $uibModal.open({
            animation: vm.animationsEnabled,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/dist/js/pages/Ordenes/views/modalAdd.tpl.html',
            controller: 'ModalAddCtrl',
            controllerAs: 'add',
            backdrop  : 'static',
            keyboard  : false,
            size: size,
            resolve: {
                plaza: function () {
                    return vm.idPlaza;
                }
            }
        });
    }
   
    function openConsultar(orden) {
        ordersFactory.consultarDetalleOrden(vm.idPlaza, orden)
            .then(function (data) {
                data.plaza = vm.idPlaza;
                vm.animationsEnabled = true;
                var modalInstance = $uibModal.open({
                    animation: vm.animationsEnabled,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: '/dist/js/pages/Ordenes/views/consultarOrden.tpl.html',
                    controller: 'ModalconsultarOrdenCtrl',
                    controllerAs: 'add',
                    backdrop: 'static',
                    keyboard: false,
                    size: 'lg',
                    resolve: {
                        detalle: function () {
                            return data;
                        }
                    }
            });
        });
    }

    function openEjecutar(orden) {
        getSession();
        ordersFactory.consultarDetalleOrden(vm.idPlaza, orden)
            .then(function (data) {
                data.plaza = vm.idPlaza;
                data.session = vm.session;
                vm.animationsEnabled = true;
                var modalInstance = $uibModal.open({
                    animation: vm.animationsEnabled,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: '/dist/js/pages/Ordenes/views/ejecutarOrden.tpl.html',
                    controller: 'ModalEjecutarOrdenCtrl',
                    controllerAs: 'add',
                    backdrop: 'static',
                    keyboard: false,
                    size: 'lg',
                    resolve: {
                        detalle: function () {
                            return data;
                        }
                    }
                 });
           });
    }

    function getSession() {
        ordersFactory.getSession(vm.idPlaza)
            .then(function (data) {
                vm.session = data;
        });
    }

}


function ModalAddCtrl($uibModal, $uibModalInstance, ordersFactory, plaza, $rootScope) {

    var vm = this;
    var d = new Date();
    vm.fecha = d.toLocaleDateString();
    vm.showAllOrders = showAllOrders;
    vm.deleteDetalle = deleteDetalle;
    vm.ok = ok;
    vm.buscarCliente = buscarCliente;
    vm.detalleExtra = detalleExtra;
    vm.open = open;
    vm.cancel = cancel;

    ordersFactory.getDataTecnicos(plaza)
        .then(function (data) {
            vm.tecnicos = data;  
            vm.selectedTecnico = data[0];
            vm.tecnicoVisible = true;
    });


    $rootScope.$on("CallParentMethod", function () {
        vm.showAllOrders();
    });

    function showAllOrders() {
        ordersFactory.getAllOrders(plaza, vm.noOrden)
            .then(function (data) {
                vm.DetailsOrders = data;
        });
    }


    function deleteDetalle(clave, index) {
        ordersFactory.deleteDetailOrder(plaza, clave)
            .then(function (data) {
                new PNotify({
                    title: 'Eliminación Exitosa',
                    text: 'Detalle de orden eliminada!',
                    type: 'success'
                });
                vm.DetailsOrders.splice(index, 1);
        });
    }

    function ok() {
        var flag = 0;
        for (var i = 0; i < vm.DetailsOrders.length; i++) {
            if (vm.DetailsOrders[i].descripcion == "BPAQU - BAJA DE PAQUETE DE INTERNET") {
                flag = 1;
                var items = {
                    plaza: plaza,
                    orden: vm.noOrden,
                    contrato: vm.contratoCliente,

                };
                vm.animationsEnabled = true;
                var modalInstance = $uibModal.open({
                    animation: vm.animationsEnabled,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: '/dist/js/pages/Ordenes/views/motivoCancelacion.tpl.html',
                    controller: 'MotivoCancelacionCtrl',
                    controllerAs: 'ctrl',
                    backdrop: 'static',
                    keyboard: false,
                    size: 'md',
                    resolve: {
                        items: function () {
                            return items;
                        }
                    }
                });
            }
        }
        if(flag == 0){
            guardarDetalle();
        }
       
    }

    $rootScope.$on("guardarDetalle", function () {
        guardarDetalle();
    });

    function guardarDetalle() {
        if (vm.noOrden == undefined || vm.noOrden == null || vm.noOrden == "") {
            new PNotify({
                title: 'Completa la Orden',
                text: 'Por favor completa todos los campos de la orden.',
                icon: 'fa fa-info-circle',
                type: 'warning',
                hide: true
            });
        } else if (vm.DetailsOrders.length == 0) {
            new PNotify({
                title: 'Completa la Orden',
                text: 'Por favor agrega el detalle de la orden.',
                icon: 'fa fa-info-circle',
                type: 'warning',
                hide: true
            });
        } else {
            ordersFactory.saveOrder(plaza, vm.observaciones, vm.noOrden, usuario).then(function (data) {
                new PNotify({
                    title: 'Orden Generada',
                    text: 'La orden se generó exitosamente!',
                    type: 'success'
                });
                $uibModalInstance.dismiss('cancel');
                LlenarTabla(plaza, '', '', '', '', '');
            });
        }
    }

    function buscarCliente() {
        if (vm.contratoCliente != undefined && vm.contratoCliente != "") {
            ordersFactory.getDataClient(plaza, vm.contratoCliente).then(function (data) {
                if (data.datos.Contrato == 0) {
                    new PNotify({
                        title: 'Contrato no encontrado',
                        text: 'El contrato ingresado no corresponde a ningun cliente.',
                        icon: 'fa fa-info-circle',
                        type: 'error',
                        hide: true
                    });
                }
                vm.contrato = data.datos.Contrato;
                vm.nombre = data.datos.Nombre;
                vm.ciudad = data.datos.Ciudad;
                vm.colonia = data.datos.Colonia;
                vm.calle = data.datos.Calle;
                vm.numero = data.datos.Numero;
                vm.serviciosCliente = data.servicios;
            });
        }
    }

    function detalleExtra(x) {
        if (x.accion == "Ext. Adicionales") {
            ordersFactory.detalleConet(plaza, x.clave, x.clv_orden, vm.contratoCliente)
                .then(function (data) {
                    if (x.descripcion == "CONEX - CONTRATACION DE TELEVISION ADICIONAL") {
                        vm.animationsEnabled = true;
                        var items = {
                            clave: x.clave,
                            orden: x.clv_orden,
                            contrato: vm.contratoCliente,
                            plaza: plaza,
                            extra: data.extra
                        }
                        var modalInstance = $uibModal.open({
                            animation: vm.animationsEnabled,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: '/dist/js/pages/Ordenes/views/agregarExtension.tpl.html',
                            controller: 'ModalExtensionAddCtrl',
                            controllerAs: 'ctrl',
                            backdrop: 'static',
                            keyboard: false,
                            size: 'md',
                            resolve: {
                                items: function () {
                                    return items;
                                }
                            }
                    });
                } else if (x.descripcion ==  "CANEX - Cancelación De Extensión") {
                    vm.animationsEnabled = true;
                    var items = {
                        clave: x.clave,
                        orden: x.clv_orden,
                        contrato: vm.contratoCliente,
                        plaza: plaza,
                        extra: data.extra
                    }
                    var modalInstance = $uibModal.open({
                        animation: vm.animationsEnabled,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: '/dist/js/pages/Ordenes/views/cancelarExtension.tpl.html',
                        controller: 'ModalExtensionCtrl',
                        controllerAs: 'ctrl',
                        backdrop: 'static',
                        keyboard: false,
                        size: 'md',
                        resolve: {
                            items: function () {
                                return items;
                            }
                        }
                    });
                }
               
            });
        } else if (x.accion == "Domicilio") {
            ordersFactory.detalleCamdo(plaza, x.clave, x.clv_orden, vm.contratoCliente)
                .then(function (data) {
                    vm.animationsEnabled = true;
                    var modalInstance = $uibModal.open({
                        animation: vm.animationsEnabled,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: '/dist/js/pages/Ordenes/views/domicilioDetalle.tpl.html',
                        controller: 'ModalDomicilioDetalleCtrl',
                        controllerAs: 'ctrl',
                        backdrop: 'static',
                        keyboard: false,
                        size: 'md',
                        resolve: {
                            data: function () {
                                return data;
                            }
                        }
                    });
            });
        } else if (x.accion == "Paquetes") {
            var data = {
                orden: x.clv_orden,
                clave: x.clave,
                plaza: plaza,
                contrato: vm.contratoCliente
            };
            vm.animationsEnabled = true;
            var modalInstance = $uibModal.open({
                animation: vm.animationsEnabled,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: '/dist/js/pages/Ordenes/views/bajaPaqueteDetalle.tpl.html',
                controller: 'bajaPaqueteDeatelleCtrl',
                controllerAs: 'ctrl',
                backdrop: 'static',
                keyboard: false,
                size: 'md',
                resolve: {
                    data: function () {
                        return data;
                    }
                }
            });
        }
    }

    function open(size) {
        if (vm.contratoCliente != undefined && vm.contratoCliente != null && vm.contratoCliente != "") {
            vm.items = {
                plaza: plaza,
                contrato: vm.contratoCliente,
                orden: vm.noOrden
            };
            if (vm.noOrden == undefined) {
                ordersFactory.addPre(plaza, vm.contratoCliente, vm.observaciones)
                    .then(function (data) {
                        vm.noOrden = data;
                        vm.items.orden = data;
                });
            }
            vm.animationsEnabled = true;
            var modalInstance = $uibModal.open({
                animation: vm.animationsEnabled,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: '/dist/js/pages/Ordenes/views/modalTrabajo.tpl.html',
                controller: 'ModalServiceCtrl',
                controllerAs: 'ctrl',
                backdrop: 'static',
                keyboard: false,
                size: size,
                resolve: {
                    items: function () {
                        return vm.items;
                    }
                }
            });
        } else {
            new PNotify({
                title: 'Selecciona una cliente',
                text: 'Debes seleccionar un cliente válido.',
                icon: 'fa fa-info-circle',
                type: 'error',
                hide: true
            });
        }
        
    }

    function cancel() {
        if (vm.noOrden == undefined || vm.noOrden == null || vm.noOrden == "") {
            $uibModalInstance.dismiss('cancel');
        } else {
            ordersFactory.cancelOrder(plaza, vm.noOrden)
                .then(function (data) {
                    new PNotify({
                        title: 'Orden Cancelada',
                        text: 'La orden ha sido cancelada.',
                        icon: 'fa fa-info-circle',
                        type: 'error',
                        hide: true
                    });
            });
            $uibModalInstance.dismiss('cancel');
        }
        
    };

}

function MotivoCancelacionCtrl($uibModal, $uibModalInstance, ordersFactory, items, $rootScope) {
    var vm = this;
    vm.ok = ok;
    vm.cancel = cancel;

    ordersFactory.motivosCancelacion(items.plaza)
        .then(function (data) {
            vm.motivos = data;
            vm.selectedMotivo = data[0];
    });
    
    function ok() {
        vm.cancel();
        ordersFactory.guardarMotivo(items.plaza, items.orden, vm.selectedMotivo.clv_motivo)
            .then(function (data) {
                $rootScope.$emit("guardarDetalle", {});
        });
    }
    
    function cancel() {
        $uibModalInstance.dismiss('cancel');
    };
}

function ModalServiceCtrl($uibModal, $uibModalInstance, ordersFactory, items, $rootScope) {
    
    var vm = this;
    vm.realizaTrabajo = true;
    vm.changeService = changeService;
    vm.cancel = cancel;
    vm.ok = ok;

    ordersFactory.getDataServices(items.plaza, items.contrato)
        .then(function (data) {
            vm.servicios = data;
            data.unshift({
                "clv_servicio": 0,
                "servicio": "--------------------------"
            });
            vm.selectedService = data[0];
    });
  
    function changeService() {
        if (vm.selectedService.clv_servicio != 0) {
            getTrabajos(vm.selectedService.clv_servicio);
        }
    }

    function getTrabajos(id) {
        ordersFactory.getDataJobs(items.plaza, id).then(function (data) {
            vm.trabajos = data;
            vm.selectedTrabajo = data[0];
        });
    }
    
    function cancel() {
        $uibModalInstance.dismiss('cancel');
    };

    function ok() {
        ordersFactory.getDataActivo(items.plaza, vm.selectedService.clv_servicio, items.contrato)
            .then(function (data) {
                if (data == 0) {
                    new PNotify({
                        title: 'Servicio Inactivo',
                        text: 'El cliente no cuenta con el servicio o esta dado de baja.',
                        icon: 'fa fa-info-circle',
                        type: 'error',
                        hide: true
                    });
                } else {
                    var trabajo = 0;
                    if (vm.realizaTrabajo == true) {
                        trabajo = 1;
                    } else {
                        trabajo = 0;
                    }
                    ordersFactory.saveDetailOrder(items.plaza, items.orden, vm.selectedTrabajo.clv_trabajo, vm.observaciones, trabajo, vm.selectedService.clv_servicio).then(function (data) {
                        items.clave = data;
                        $rootScope.$emit("CallParentMethod", {});
                        $uibModalInstance.dismiss('cancel');

                        if (vm.selectedTrabajo.descripcion == "CAMDO - CAMBIO DE DOMICILIO") {
                            vm.animationsEnabled = true;
                            var modalInstance = $uibModal.open({
                                animation: vm.animationsEnabled,
                                ariaLabelledBy: 'modal-title',
                                ariaDescribedBy: 'modal-body',
                                templateUrl: '/dist/js/pages/Ordenes/views/domicilio.tpl.html',
                                controller: 'ModalDomicilioCtrl',
                                controllerAs: 'ctrl',
                                backdrop: 'static',
                                keyboard: false,
                                size: 'md',
                                resolve: {
                                    items: function () {
                                        return items;
                                    }
                                }
                            });
                        } else if (vm.selectedTrabajo.descripcion == "CANEX - Cancelación De Extensión") {
                            vm.animationsEnabled = true;
                            var modalInstance = $uibModal.open({
                                animation: vm.animationsEnabled,
                                ariaLabelledBy: 'modal-title',
                                ariaDescribedBy: 'modal-body',
                                templateUrl: '/dist/js/pages/Ordenes/views/cancelarExtension.tpl.html',
                                controller: 'ModalExtensionCtrl',
                                controllerAs: 'ctrl',
                                backdrop: 'static',
                                keyboard: false,
                                size: 'md',
                                resolve: {
                                    items: function () {
                                        return items;
                                    }
                                }
                            });
                        } else if (vm.selectedTrabajo.descripcion == "CONEX - CONTRATACION DE TELEVISION ADICIONAL") {
                            vm.animationsEnabled = true;
                            var modalInstance = $uibModal.open({
                                animation: vm.animationsEnabled,
                                ariaLabelledBy: 'modal-title',
                                ariaDescribedBy: 'modal-body',
                                templateUrl: '/dist/js/pages/Ordenes/views/agregarExtension.tpl.html',
                                controller: 'ModalExtensionAddCtrl',
                                controllerAs: 'ctrl',
                                backdrop: 'static',
                                keyboard: false,
                                size: 'md',
                                resolve: {
                                    items: function () {
                                        return items;
                                    }
                                }
                            });
                        } else if (vm.selectedTrabajo.descripcion == "BPAQU - BAJA DE PAQUETE DE INTERNET") {
                            vm.animationsEnabled = true;
                            var modalInstance = $uibModal.open({
                                animation: vm.animationsEnabled,
                                ariaLabelledBy: 'modal-title',
                                ariaDescribedBy: 'modal-body',
                                templateUrl: '/dist/js/pages/Ordenes/views/bajaPaquete.tpl.html',
                                controller: 'ModalBajaPaqueteCtrl',
                                controllerAs: 'ctrl',
                                backdrop: 'static',
                                keyboard: false,
                                size: 'md',
                                resolve: {
                                    items: function () {
                                        return items;
                                    }
                                }
                            });
                        } else if (vm.selectedTrabajo.descripcion == "CANET - Cambio De Domicilio Servicio Internet") {
                            vm.animationsEnabled = true;
                            var modalInstance = $uibModal.open({
                                animation: vm.animationsEnabled,
                                ariaLabelledBy: 'modal-title',
                                ariaDescribedBy: 'modal-body',
                                templateUrl: '/dist/js/pages/Ordenes/views/domicilio.tpl.html',
                                controller: 'ModalDomicilioNetCtrl',
                                controllerAs: 'ctrl',
                                backdrop: 'static',
                                keyboard: false,
                                size: 'md',
                                resolve: {
                                    items: function () {
                                        return items;
                                    }
                                }
                            });
                        } else if (vm.selectedTrabajo.descripcion == "DPAQU - Desconexión De Paquete") {
                            vm.animationsEnabled = true;
                            var modalInstance = $uibModal.open({
                                animation: vm.animationsEnabled,
                                ariaLabelledBy: 'modal-title',
                                ariaDescribedBy: 'modal-body',
                                templateUrl: '/dist/js/pages/Ordenes/views/bajaPaquete.tpl.html',
                                controller: 'ModalDesconexionPaqueteCtrl',
                                controllerAs: 'ctrl',
                                backdrop: 'static',
                                keyboard: false,
                                size: 'md',
                                resolve: {
                                    items: function () {
                                        return items;
                                    }
                                }
                            });
                        } else if (vm.selectedTrabajo.descripcion == "IPAQU - Instalación De Paquete") {
                            vm.animationsEnabled = true;
                            var modalInstance = $uibModal.open({
                                animation: vm.animationsEnabled,
                                ariaLabelledBy: 'modal-title',
                                ariaDescribedBy: 'modal-body',
                                templateUrl: '/dist/js/pages/Ordenes/views/bajaPaquete.tpl.html',
                                controller: 'ModalInstalacionPaqueteCtrl',
                                controllerAs: 'ctrl',
                                backdrop: 'static',
                                keyboard: false,
                                size: 'md',
                                resolve: {
                                    items: function () {
                                        return items;
                                    }
                                }
                            });
                        } else if (vm.selectedTrabajo.descripcion == "RPAQU - Reconexión De Paquete") {
                            vm.animationsEnabled = true;
                            var modalInstance = $uibModal.open({
                                animation: vm.animationsEnabled,
                                ariaLabelledBy: 'modal-title',
                                ariaDescribedBy: 'modal-body',
                                templateUrl: '/dist/js/pages/Ordenes/views/bajaPaquete.tpl.html',
                                controller: 'ModalReconexionPaqueteCtrl',
                                controllerAs: 'ctrl',
                                backdrop: 'static',
                                keyboard: false,
                                size: 'md',
                                resolve: {
                                    items: function () {
                                        return items;
                                    }
                                }
                            });
                        } else if (vm.selectedTrabajo.descripcion == "ICABM - INSTALACION DE CABLEMODEM") {
                            vm.animationsEnabled = true;
                            var modalInstance = $uibModal.open({
                                animation: vm.animationsEnabled,
                                ariaLabelledBy: 'modal-title',
                                ariaDescribedBy: 'modal-body',
                                templateUrl: '/dist/js/pages/Ordenes/views/modalAsignacion.tpl.html',
                                controller: 'ModalAsignacionCtrl',
                                controllerAs: 'ctrl',
                                backdrop: 'static',
                                keyboard: false,
                                size: 'md',
                                resolve: {
                                    items: function () {
                                        return items;
                                    }
                                }
                            });
                        }
                    }); 
                }
        });
    }
}

function ModalDomicilioCtrl($uibModalInstance, ordersFactory, items, $rootScope) {
    var vm = this;
    vm.changeCiudad = changeCiudad;
    vm.ok = ok;
    vm.changeColonia = changeColonia;
    vm.cancel = cancel;

    ordersFactory.getCiudades(items.plaza)
        .then(function (data) {
            data.unshift({
                "Clv_Ciudad": 0,
                "Nombre": "--------------------------"
            });
            vm.ciudades = data;
            vm.selectedCiudad = data[0];
    });

    function changeCiudad() {
        if (vm.selectedCiudad.Clv_Ciudad != 0) {
            ordersFactory.getColonia(items.plaza, vm.selectedCiudad.Clv_Ciudad)
                .then(function (data) {
                    data.unshift({
                        "clv_colonia": 0,
                        "Nombre": "--------------------------"
                    });
                    vm.colonias = data;
                    vm.selectedColonia = data[0];
                    vm.calles = '';
            });
        }
    }

    function changeColonia() {
        if (vm.selectedColonia.clv_colonia != 0) {
            ordersFactory.getCalle(items.plaza, vm.selectedColonia.clv_colonia)
                .then(function (data) {
                    data.unshift({
                        "Clv_Calle": 0,
                        "Nombre": "--------------------------"
                    });
                    vm.calles = data;
                    vm.selectedCalle = data[0];
            });
        }
    }

    function ok() {
        if (vm.selectedCiudad.Clv_Ciudad == 0 || vm.selectedColonia.clv_colonia == 0 || vm.selectedCalle.Clv_Calle == 0 || vm.numero == undefined) {
            new PNotify({
                title: 'Error',
                text: 'Por favor llene los campos correctamente.',
                icon: 'fa fa-info-circle',
                type: 'error',
                hide: true
            });
        } else {
            ordersFactory.saveCambioDomicilio(items.plaza, items.clave, items.orden, items.contrato, vm.selectedCiudad.Clv_Ciudad, vm.selectedColonia.clv_colonia, vm.selectedCalle.Clv_Calle, vm.numero, vm.telefono, vm.entreCalles, vm.numeroInterior)
                .then(function (data) {
                    $uibModalInstance.dismiss('cancel'); 
            });
        }
    }

    function cancel() {
        ordersFactory.deleteDetailOrder(items.plaza, items.clave)
            .then(function (data) {
                $rootScope.$emit("CallParentMethod", {});
        });
        $uibModalInstance.dismiss('cancel');
    };
}

function ModalExtensionCtrl($uibModalInstance, ordersFactory, items, $rootScope) {

    var vm = this;
    vm.cancel = cancel;
    vm.ok = ok;

    ordersFactory.getExtensiones(items.plaza, items.contrato)
        .then(function (data) {
            vm.extInstaladas = data;
    });

    var can = 0;
    if(items.extra == undefined){
        vm.extCancelar = 1;
    } else {
        vm.extCancelar = items.extra;
        can = 1;
    }

    function cancel() {
        if (can == 0) {
            ordersFactory.deleteDetailOrder(items.plaza, items.clave)
                .then(function (data) {
                    $rootScope.$emit("CallParentMethod", {});
            });
        }  
        $uibModalInstance.dismiss('cancel');
    };

    function ok() {
        if (can = 0) {
            ordersFactory.tieneCanexConex(items.plaza, items.orden)
                .then(function (data) {
                    if (data.conex > 0 || data.canex > 0) {
                        if (data.conex > 0) {
                            new PNotify({
                                title: 'Error',
                                text: 'Existe una orden de cancelación de extensión por lo cual no se puede agregar este concepto.',
                                icon: 'fa fa-info-circle',
                                type: 'error',
                                hide: true
                            });
                        } else if (data.canex > 0) {
                            new PNotify({
                                title: 'Error',
                                text: 'Existe una orden de contratación de extensión por lo cual no se puede agregar este concepto.',
                                icon: 'fa fa-info-circle',
                                type: 'error',
                                hide: true
                            });
                        }

                        vm.cancel();
                    } else {
                        if (vm.extInstaladas < vm.extCancelar) {
                            new PNotify({
                                title: 'Error',
                                text: 'Seleccione un número de extensiones válido.',
                                icon: 'fa fa-info-circle',
                                type: 'error',
                                hide: true
                            });
                        } else if (vm.extInstaladas == 0) {
                            new PNotify({
                                title: 'Error',
                                text: 'El cliente no cuenta con extensiones.',
                                icon: 'fa fa-info-circle',
                                type: 'error',
                                hide: true
                            });
                        } else if (vm.extCancelar <= 0 || vm.extCancelar == undefined) {
                            new PNotify({
                                title: 'Error',
                                text: 'Seleccione un número de extensiones válido.',
                                icon: 'fa fa-info-circle',
                                type: 'error',
                                hide: true
                            });
                        } else {
                            ordersFactory.cancelExtensiones(items.plaza, items.clave, items.orden, items.contrato, vm.extCancelar).then(function (data) {
                                $uibModalInstance.dismiss('cancel');
                            });
                        }
                    }
            });
        } else {
            ordersFactory.cancelExtensiones(items.plaza, items.clave, items.orden, items.contrato, vm.extCancelar)
                .then(function (data) {
                    $uibModalInstance.dismiss('cancel');
            });
        }
               
    }
}

function ModalExtensionAddCtrl($uibModalInstance, ordersFactory, items, $rootScope) {
    var vm = this;
    var can = 0;
    vm.cancel = cancel;
    vm.ok = ok;


    if(items.extra == undefined){
        vm.extAgregar = 1;
    } else {
        vm.extAgregar = items.extra;
        can = 1;
    }

    function cancel() {
        if (can == 0) {
            ordersFactory.deleteDetailOrder(items.plaza, items.clave)
                .then(function (data) {
                    $rootScope.$emit("CallParentMethod", {});
            });
        } 
        $uibModalInstance.dismiss('cancel');
    };

    function ok() {
        if(can == 0){
            ordersFactory.tieneCanexConex(items.plaza, items.orden)
                .then(function (data) {
                    if (data.conex > 0 || data.canex > 0) {
                        if (data.conex > 0) {
                            new PNotify({
                                title: 'Error',
                                text: 'Existe una orden de cancelación de extensión por lo cual no se puede agregar este concepto.',
                                icon: 'fa fa-info-circle',
                                type: 'error',
                                hide: true
                            });
                        } else if (data.canex > 0) {
                            new PNotify({
                                title: 'Error',
                                text: 'Existe una orden de contratación de extensión por lo cual no se puede agregar este concepto.',
                                icon: 'fa fa-info-circle',
                                type: 'error',
                                hide: true
                            });
                        }

                        vm.cancel();
                    } else {
                        if (vm.extAgregar == 0 || vm.extAgregar == undefined) {
                            new PNotify({
                                title: 'Error',
                                text: 'Por favor agregue extensiones válidas.',
                                icon: 'fa fa-info-circle',
                                type: 'error',
                                hide: true
                            });
                        } else {
                            ordersFactory.saveExtensiones(items.plaza, items.clave, items.orden, items.contrato, vm.extAgregar).then(function (data) {
                                $uibModalInstance.dismiss('cancel');
                            });
                        }
                    }
            });
        } else {
            ordersFactory.saveExtensiones(items.plaza, items.clave, items.orden, items.contrato, vm.extAgregar)
                .then(function (data) {
                    $uibModalInstance.dismiss('cancel');
            });
        }
      
    }
}

function ModalBajaPaqueteCtrl($uibModalInstance, ordersFactory, items, $rootScope, $http) {
    var vm = this;
    vm.titulo = "Baja";
    vm.contratosNet = [];
    vm.saveContratoNet = saveContratoNet;
    vm.ok = ok;
    vm.cancel = cancel;

    ordersFactory.getCablemodems(items.plaza, items.contrato, items.orden, items.clave)
        .then(function (data) {
            vm.cablemodems = data;
    });

    function saveContratoNet(contrato) {
        if (contrato.selectedMac == true) {
            vm.contratosNet.push({
                ContratoNet: contrato.ContratoNet,
                Mac: contrato.Mac
            });
        }else{
            vm.contratosNet.forEach(function (element, index, array) {
                if (element.ContratoNet == contrato.ContratoNet) {
                    vm.contratosNet.splice(index, 1);
                }
            });
        }
        
    }

    function ok() {
        var objeto = {};
        objeto.idPlaza = items.plaza;
        objeto.Clave = items.clave;
        objeto.Orden = items.orden;
        objeto.Macs = vm.contratosNet;
        objeto.Status = "B";
        ordersFactory.bajaPaquete(objeto)
            .then(function (data) {
                $uibModalInstance.dismiss('cancel');
        });
    }

    function cancel() {
        ordersFactory.deleteDetailOrder(items.plaza, items.clave)
            .then(function (data) {
                $rootScope.$emit("CallParentMethod", {});
        });
        $uibModalInstance.dismiss('cancel');
    };
}

function ModalDomicilioNetCtrl($uibModalInstance, ordersFactory, items, $rootScope) {
    var vm = this;
    vm.changeCiudad = changeCiudad;
    vm.changeColonia = changeColonia;
    vm.ok = ok;
    vm.cancel = cancel;

    ordersFactory.getCiudades(items.plaza)
        .then(function (data) {
            data.unshift({
                "Clv_Ciudad": 0,
                "Nombre": "--------------------------"
            });
            vm.ciudades = data;
            vm.selectedCiudad = data[0];
    });

    function changeCiudad() {
        if (vm.selectedCiudad.Clv_Ciudad != 0) {
            ordersFactory.getColonia(items.plaza, vm.selectedCiudad.Clv_Ciudad)
                .then(function (data) {
                    data.unshift({
                        "clv_colonia": 0,
                        "Nombre": "--------------------------"
                    });
                    vm.colonias = data;
                    vm.selectedColonia = data[0];
                    vm.calles = '';
            });
        }
    }

    function changeColonia() {
        if (vm.selectedColonia.clv_colonia != 0) {
            ordersFactory.getCalle(items.plaza, vm.selectedColonia.clv_colonia)
                .then(function (data) {
                    data.unshift({
                        "Clv_Calle": 0,
                        "Nombre": "--------------------------"
                    });
                    vm.calles = data;
                    vm.selectedCalle = data[0];
            });
        }
    }

    function ok() {
        if (vm.selectedCiudad.Clv_Ciudad == 0 || vm.selectedColonia.clv_colonia == 0 || vm.selectedCalle.Clv_Calle == 0 || vm.numero == undefined) {
            new PNotify({
                title: 'Error',
                text: 'Por favor llene los campos correctamente.',
                icon: 'fa fa-info-circle',
                type: 'error',
                hide: true
            });
        } else {
            ordersFactory.saveCambioDomicilio(items.plaza, items.clave, items.orden, items.contrato, vm.selectedCiudad.Clv_Ciudad, vm.selectedColonia.clv_colonia, vm.selectedCalle.Clv_Calle, vm.numero, vm.telefono, vm.entreCalles, vm.numeroInterior)
                .then(function (data) {
                    $uibModalInstance.dismiss('cancel');
            });
        }
    }

    function cancel() {
        ordersFactory.deleteDetailOrder(items.plaza, items.clave)
            .then(function (data) {
                $rootScope.$emit("CallParentMethod", {});
        });
        $uibModalInstance.dismiss('cancel');
    };
}


function ModalDesconexionPaqueteCtrl($uibModalInstance, ordersFactory, items, $rootScope, $http) {
    var vm = this;
    vm.titutlo = "Desconexión";
    vm.contratosNet = [];
    vm.saveContratoNet = saveContratoNet;
    vm.ok = ok;
    vm.cancel = cancel;

    ordersFactory.getCablemodems(items.plaza, items.contrato)
        .then(function (data) {
            vm.cablemodems = data;
    });

    function saveContratoNet(contrato) {
        if (contrato.selectedMac == true) {
            vm.contratosNet.push({
                ContratoNet: contrato.ContratoNet,
                Mac: contrato.Mac
            });
        } else {
            vm.contratosNet.forEach(function (element, index, array) {
                if (element.ContratoNet == contrato.ContratoNet) {
                    vm.contratosNet.splice(index, 1);
                }
            });
        }

    }

    function ok() {
        var objeto = {};
        objeto.idPlaza = items.plaza;
        objeto.Clave = items.clave;
        objeto.Orden = items.orden;
        objeto.Macs = vm.contratosNet;
        objeto.Status = "S";
        ordersFactory.bajaPaquete(objeto)
            .then(function (data) {
                $uibModalInstance.dismiss('cancel');
        });
    }

    function cancel() {
        ordersFactory.deleteDetailOrder(items.plaza, items.clave)
            .then(function (data) {
                $rootScope.$emit("CallParentMethod", {});
        });
        $uibModalInstance.dismiss('cancel');
    };
}


function ModalInstalacionPaqueteCtrl($uibModalInstance, ordersFactory, items, $rootScope, $http) {
    var vm = this;
    vm.titutlo = "Instalación";
    vm.contratosNet = [];
    vm.saveContratoNet = saveContratoNet;
    vm.ok = ok;
    vm.cancel = cancel;

    ordersFactory.getCablemodems(items.plaza, items.contrato)
        .then(function (data) {
            vm.cablemodems = data;
    });

    function saveContratoNet(contrato) {
        if (contrato.selectedMac == true) {
            vm.contratosNet.push({
                ContratoNet: contrato.ContratoNet,
                Mac: contrato.Mac
            });
        } else {
            vm.contratosNet.forEach(function (element, index, array) {
                if (element.ContratoNet == contrato.ContratoNet) {
                    vm.contratosNet.splice(index, 1);
                }
            });
        }

    }

    function ok() {
        var objeto = {};
        objeto.idPlaza = items.plaza;
        objeto.Clave = items.clave;
        objeto.Orden = items.orden;
        objeto.Macs = vm.contratosNet;
        objeto.Status = "I";
        ordersFactory.bajaPaquete(objeto)
            .then(function (data) {
                $uibModalInstance.dismiss('cancel');
        });
    }

    function cancel() {
        ordersFactory.deleteDetailOrder(items.plaza, items.clave)
            .then(function (data) {
                $rootScope.$emit("CallParentMethod", {});
        });
        $uibModalInstance.dismiss('cancel');
    };
}


function ModalReconexionPaqueteCtrl($uibModalInstance, ordersFactory, items, $rootScope) {
    var vm = this;
    vm.titulo = "Reconexión";
    vm.contratosNet = [];
    vm.saveContratoNet = saveContratoNet;
    vm.ok = ok;
    vm.cancel = cancel;

    ordersFactory.getCablemodems(items.plaza, items.contrato)
        .then(function (data) {
            vm.cablemodems = data;
    });

    function saveContratoNet(contrato) {
        if (contrato.selectedMac == true) {
            vm.contratosNet.push({
                ContratoNet: contrato.ContratoNet,
                Mac: contrato.Mac
            });
        } else {
            vm.contratosNet.forEach(function (element, index, array) {
                if (element.ContratoNet == contrato.ContratoNet) {
                    vm.contratosNet.splice(index, 1);
                }
            });
        }

    }

    function ok() {
        var objeto = {};
        objeto.idPlaza = items.plaza;
        objeto.Clave = items.clave;
        objeto.Orden = items.orden;
        objeto.Macs = vm.contratosNet;
        objeto.Status = "I";
        ordersFactory.bajaPaquete(objeto)
            .then(function (data) {
                $uibModalInstance.dismiss('cancel');
        });
    }

    function cancel() {
        ordersFactory.deleteDetailOrder(items.plaza, items.clave)
            .then(function (data) {
                $rootScope.$emit("CallParentMethod", {});
        });
        $uibModalInstance.dismiss('cancel');
    };
}

function ModalconsultarOrdenCtrl($uibModal, $uibModalInstance, detalle, ordersFactory) {
    var vm = this;
    vm.contratoCliente = detalle.contrato;
    vm.noOrden = detalle.clv_orden;
    vm.mostrarDescarga = true;
    vm.mostrarDetallesDimicilio = false;
    vm.nombre = detalle.nombre;
    vm.calle = detalle.calle;
    vm.numero = detalle.numero;
    vm.colonia = detalle.colonia;
    vm.ciudad = detalle.ciudad;
    vm.serviciosCliente = detalle.servicios;
    vm.tecnicosSelect = detalle.tecnico;
    vm.fecha = detalle.solicitud;
    vm.fejecucion = detalle.ejecucion;
    vm.visita1 = detalle.visita1;
    vm.visita2 = detalle.visita2;
    vm.observaciones = detalle.observaciones;
    vm.DetailsOrders = detalle.detallesOrdenes;
    vm.genero = detalle.genero;
    vm.folio = detalle.folio;
    vm.taps = detalle.tap;
    vm.placa = detalle.placa;
    vm.detalleExtra = detalleExtra;
    vm.verDescarga = verDescarga;
    vm.cancel = cancel;


    if (detalle.status != "P") {
        vm.ejecuto = detalle.ejecuto;
    }

    if (detalle.status == "P") {;
        vm.pe = true;
    } else if (detalle.status == "E") {
        vm.mostrarDescarga = false;
        vm.ej = true;
    } else {
        vm.vis = true;
        vm.mostrarDescarga = false;
    }

    for (var i = 0; i < detalle.detallesOrdenes.length; i++) {
        if (detalle.detallesOrdenes[i].accion == "Domicilio") {
             vm.mostrarDetallesDimicilio = true;
        }
        if (detalle.detallesOrdenes[i].accion == "Ext. Adicionales") {
            detalle.adicionales = true;
        }
    }
   
    function detalleExtra(x) {
        if (x.accion == "Ext. Adicionales") {
            ordersFactory.detalleConet(detalle.plaza, x.clave, x.clv_orden, detalle.contrato)
                .then(function (data) {
                    if (x.descripcion == "CONEX - CONTRATACION DE TELEVISION ADICIONAL") {
                        data.titulo = "Agregar Extensiones";
                        data.label = "Extensiones por Agregar";
                    } else {
                        data.titulo = "Cancelar Extensiones";
                        data.label = "Extensiones por Cancelar";
                    }
                    vm.animationsEnabled = true;
                    var modalInstance = $uibModal.open({
                        animation: vm.animationsEnabled,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: '/dist/js/pages/Ordenes/views/extensionDetalle.tpl.html',
                        controller: 'ModalExtensionDetalleCtrl',
                        controllerAs: 'ctrl',
                        backdrop: 'static',
                        keyboard: false,
                        size: 'md',
                        resolve: {
                            data: function () {
                                return data;
                            }
                        }
                    });
            });
        } else if (x.accion == "Domicilio") {
            ordersFactory.detalleCamdo(detalle.plaza, x.clave, x.clv_orden, detalle.contrato)
                .then(function (data) {
                    vm.animationsEnabled = true;
                    var modalInstance = $uibModal.open({
                        animation: vm.animationsEnabled,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: '/dist/js/pages/Ordenes/views/domicilioDetalle.tpl.html',
                        controller: 'ModalDomicilioDetalleCtrl',
                        controllerAs: 'ctrl',
                        backdrop: 'static',
                        keyboard: false,
                        size: 'md',
                        resolve: {
                            data: function () {
                                return data;
                            }
                        }
                    });
            });
        }
    }

    function verDescarga() {
        vm.animationsEnabled = true;
        var modalInstance = $uibModal.open({
            animation: vm.animationsEnabled,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/dist/js/pages/Ordenes/views/descargaMaterialDetalle.tpl.html',
            controller: 'ModalDescargaMaterialDetalleCtrl',
            controllerAs: 'ctrl',
            backdrop: 'static',
            keyboard: false,
            size: 'lg',
            resolve: {
                data: function () {
                    return detalle;
                }
            }
        });
    }

    function cancel() {
        $uibModalInstance.dismiss('cancel');
    }
}

function ModalEjecutarOrdenCtrl($uibModal, $rootScope, $uibModalInstance, detalle, ordersFactory, $filter) {
    var vm = this;
    vm.contratoCliente = detalle.contrato;
    vm.noOrden = detalle.clv_orden;
    vm.tipo = true;
    vm.fechaEjecutar = false;
    vm.fechaVisita = true;
    vm.mostrarDetallesDimicilio = false;
    vm.aux = 0;
    vm.showFechas = showFechas;
    vm.hideTecnicos = false;
    vm.materialGuardado = false;
    vm.descargaMaterial = descargaMaterial;
    vm.nombre = detalle.nombre;
    vm.calle = detalle.calle;
    vm.numero = detalle.numero;
    vm.colonia = detalle.colonia;
    vm.ciudad = detalle.ciudad;
    vm.serviciosCliente = detalle.servicios;
    vm.tecnicosSelect = detalle.tecnico;
    vm.fecha = detalle.solicitud;
    vm.fejecucion = detalle.ejecucion;
    vm.visita1 = detalle.visita1;
    vm.visita2 = detalle.visita2;
    vm.observaciones = detalle.observaciones;
    vm.DetailsOrders = detalle.detallesOrdenes;
    vm.genero = detalle.genero;
    vm.folio = detalle.folio;
    vm.taps = detalle.tap;
    vm.placa = detalle.placa;
    vm.detalleExtra = detalleExtra;
    vm.cancel = cancel;
    vm.eliminarOrden = eliminarOrden;
    vm.ok = ok;

    if (detalle.session == null || detalle.session == 0 || detalle.session == undefined) {
        ordersFactory.getSession(detalle.plaza)
            .then(function (data) {
                detalle.session = data;
        });
    }
    
    for (var i = 0; i < detalle.detallesOrdenes.length; i++) {
        if(detalle.detallesOrdenes[i].accion != ""){
            vm.aux = 1;
        }
        if (detalle.detallesOrdenes[i].accion == "Domicilio") {
            vm.mostrarDetallesDimicilio = true;
        }
        if (detalle.detallesOrdenes[i].accion == "Ext. Adicionales") {
            detalle.adicionales = true;
            detalle.clave = detalle.detallesOrdenes[i].clave;
        }
    }

    function showFechas() {
        if (vm.tipo == true) {
            vm.fechaEjecutar = false;
            vm.fechaVisita = true;
            vm.visita1 = '';
            vm.visita2 = '';
        } else {
            vm.fechaEjecutar = true;
            vm.fechaVisita = false;
            vm.fejecucion = '';
        }
    }

    $rootScope.$on("HideTecnicos", function () {
        vm.hideTecnicos = true;
    });
    $rootScope.$on("banderaMaterial", function () {
        vm.materialGuardado = true;
    });

    function descargaMaterial() {
        detalle.tecnico = vm.selectedTecnico.clvTecnico;
        detalle.tecnicoNombre = vm.selectedTecnico.Nombre;
        detalle.materialGuardado = vm.materialGuardado;
        vm.animationsEnabled = true;
        var modalInstance = $uibModal.open({
            animation: vm.animationsEnabled,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/dist/js/pages/Ordenes/views/descargaMaterial.tpl.html',
            controller: 'ModalDescargaMaterialCtrl',
            controllerAs: 'ctrl',
            backdrop: 'static',
            keyboard: false,
            size: 'lg',
            resolve: {
                data: function () {
                    return detalle;
                }
            }
        });
    }

    ordersFactory.getDataTecnicos(detalle.plaza)
        .then(function (data) {
            vm.tecnicos = data;
            vm.tecnicos.splice(0, 1);
            vm.selectedTecnico = data[0];
    });

    if (detalle.status != "P") {
        vm.ejecuto = detalle.ejecuto;
    }

    function detalleExtra(x) {
        if (x.accion == "Ext. Adicionales") {
            ordersFactory.detalleConet(detalle.plaza, x.clave, x.clv_orden, detalle.contrato)
                .then(function (data) {
                    if (x.descripcion == "CONEX - CONTRATACION DE TELEVISION ADICIONAL") {
                        data.titulo = "Agregar Extensiones";
                        data.label = "Extensiones por Agregar";
                    } else {
                        data.titulo = "Cancelar Extensiones";
                        data.label = "Extensiones por Cancelar";
                    }
                    vm.animationsEnabled = true;
                    var modalInstance = $uibModal.open({
                        animation: vm.animationsEnabled,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: '/dist/js/pages/Ordenes/views/extensionDetalle.tpl.html',
                        controller: 'ModalExtensionDetalleCtrl',
                        controllerAs: 'ctrl',
                        backdrop: 'static',
                        keyboard: false,
                        size: 'md',
                        resolve: {
                            data: function () {
                                return data;
                            }
                        }
                    });
            });
        } else if (x.accion == "Domicilio") {
            ordersFactory.detalleCamdo(detalle.plaza, x.clave, x.clv_orden, detalle.contrato)
                .then(function (data) {
                    vm.animationsEnabled = true;
                    var modalInstance = $uibModal.open({
                        animation: vm.animationsEnabled,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: '/dist/js/pages/Ordenes/views/domicilioDetalle.tpl.html',
                        controller: 'ModalDomicilioDetalleCtrl',
                        controllerAs: 'ctrl',
                        backdrop: 'static',
                        keyboard: false,
                        size: 'md',
                        resolve: {
                            data: function () {
                                return data;
                            }
                        }
                    });
            });
        }
    }
   
    function cancel() {
        $uibModalInstance.dismiss('cancel');
        ordersFactory.eliminarTodosArticulos(detalle.plaza, detalle.clv_orden).then(function (data) { });
    }

    function eliminarOrden() {
        ordersFactory.EliminarOrden(detalle.plaza, vm.noOrden)
            .then(function (data) {
                $uibModalInstance.dismiss('cancel');
                LlenarTabla(detalle.plaza, '', '', '', '', '');
                new PNotify({
                    title: 'Orden Eliminada',
                    text: 'La orden #' + vm.noOrden + ' ha sido eliminada exitosamente.',
                    icon: 'fa fa-info-circle',
                    type: 'success',
                    hide: true
                });
        });       
    }

    function ok() {
        if (vm.tipo == true) {
            if (vm.fejecucion == '' || vm.fejecucion == "" || vm.fejecucion == null || vm.fejecucion == '01/01/1900 0:00:00') {
                new PNotify({
                    title: 'Fecha incorrecta',
                    text: 'Por favor seleccione una fecha.',
                    icon: 'fa fa-info-circle',
                    type: 'error',
                    hide: true
                });
            } else {
                var fecha = $filter('date')(vm.fejecucion, "dd/MM/yyyy");
                ordersFactory.ejecutarOrden(detalle.plaza, fecha, '', '', vm.observaciones, usuario, vm.noOrden, vm.aux, 1).then(function (data) {
                    $uibModalInstance.dismiss('cancel');
                    LlenarTabla(detalle.plaza, '', '', '', '', '');
                });
            }
        } else {
            if (vm.visita1 == '' || vm.visita1 == "" || vm.visita1 == null) {
                new PNotify({
                    title: 'Fecha incorrecta',
                    text: 'Por favor seleccione al menos una fecha de visita.',
                    icon: 'fa fa-info-circle',
                    type: 'error',
                    hide: true
                });              
            } else {
                var visita1 = $filter('date')(vm.visita1, "dd/MM/yyyy");
                var visita2 = '';
                if (vm.visita2 != '') {
                    visita2 = $filter('date')(vm.visita2, "dd/MM/yyyy");
                }
                ordersFactory.ejecutarOrden(detalle.plaza, '', visita1, visita2, vm.observaciones, usuario, vm.noOrden, vm.aux, 2)
                    .then(function (data) {
                        $uibModalInstance.dismiss('cancel');
                        LlenarTabla(detalle.plaza, '', '', '', '', '');
                        new PNotify({
                            title: 'Orden Ejecutada',
                            text: 'La orden fué ejecutada correctamente.',
                            icon: 'fa fa-info-circle',
                            type: 'success',
                            hide: true
                        });
                });
            }
        }
    }
}

function ModalDomicilioDetalleCtrl($uibModalInstance, data) {
    var vm = this;
    vm.ciudad = data.ciudad;
    vm.clv_ciudad = data.ciudad;
    vm.colonia = data.colonia;
    vm.clv_colonia = data.clv_colonia;
    vm.calle = data.calle;
    vm.clv_calle = data.clv_calle;
    vm.numero = data.numero;
    vm.telefono = data.telefono;
    vm.entreCalles = data.entreCalles;

    vm.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }
}

function ModalExtensionDetalleCtrl($uibModalInstance, data) {
    var vm = this;
    vm.extension = data.extra;
    vm.titulo = data.titulo;
    vm.label =  data.label;
    vm.cancel = cancel;

    function cancel() {
        $uibModalInstance.dismiss('cancel');
    }
}

function ModalDescargaMaterialCtrl($uibModal, $uibModalInstance, $rootScope, data, ordersFactory) {
    var vm = this;
    vm.openExtensiones = openExtensiones;
    vm.changeClasificacion = changeClasificacion;
    vm.eliminarArticulo = eliminarArticulo;
    vm.changeArticuloDescarga = changeArticuloDescarga;
    vm.agregarArticulo = agregarArticulo;
    vm.cancel = cancel;
    vm.ok = ok;
    vm.ordenGuardada = false;

    if (data.adicionales == undefined) {
        vm.showExtensiones = false;
    } else {
        vm.showExtensiones = data.adicionales;
    }
    
    function openExtensiones() {
        vm.animationsEnabled = true;
        data.esGuardar = true;
        var modalInstance = $uibModal.open({
            animation: vm.animationsEnabled,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/dist/js/pages/Ordenes/views/descargaMaterialExtensiones.tpl.html',
            controller: 'descargaExtensionesCtrl',
            controllerAs: 'ctrl',
            backdrop: 'static',
            keyboard: false,
            size: 'lg',
            resolve: {
                data: function () {
                    return data;
                }
            }
        });
    }

    ordersFactory.getBitacoraDescarga(data.plaza, data.clv_orden)
        .then(function (data) {
            vm.bitacora = data.bitacora;
            if (vm.bitacora == 0) {
                vm.bitacora = '';
            }
            vm.almacenes = data.almacenes;
            data.almacenes.unshift({
                "id": 0,
                "descripcion": "--------------------------"
            });
            vm.selectedAlmacen = data.almacenes[0];
            data.clasificaciones.unshift({
                "clv_tipo": 0,
                "concepto": "--------------------------"
            });
            vm.clasificaciones = data.clasificaciones;
            vm.selectedClasificacion = data.clasificaciones[0];
    });

    ordersFactory.detalleArticulosTabla(data.plaza, data.clv_orden, data.session)
        .then(function (data) {
            vm.articulosGuardados = data;
    });

    function changeClasificacion() {
        if (vm.selectedClasificacion.clv_tipo != 0) {         
            ordersFactory.getArticulosDescarga(data.plaza, data.tecnico, vm.selectedClasificacion.clv_tipo)
                .then(function (data) {
                    data.unshift({
                        "clave": 0,
                        "articulo": "--------------------------",
                    });
                    vm.articulos = data;
                    vm.selectedArticuloDescarga = data[0];
            });
        }
        
    }

    function eliminarArticulo(idArticulo) {
        ordersFactory.eliminarMaterial(data.plaza, idArticulo)
            .then(function (data) {
                actualizarTabla();
        });
    }

    function changeArticuloDescarga() {
        if (vm.selectedArticuloDescarga.articulo.toLowerCase().indexOf("cable") != -1 || vm.selectedArticuloDescarga.articulo.toLowerCase().indexOf("aire comprimido") != -1) {
            vm.showMetraje = true;
            vm.showCantidad = false;
        } else {
            vm.showCantidad = true;
            vm.showMetraje = false;
        }
    }
    
    function agregarArticulo() {
        if (vm.selectedAlmacen.id != 0) {
            if (vm.selectedClasificacion.clv_tipo != 0) {
                if (vm.selectedArticuloDescarga.clave != 0) {
                    var repetido = consultarRepetido(vm.selectedArticuloDescarga.articulo);
                    if(repetido == 0){
                        if (vm.selectedArticuloDescarga.articulo.toLowerCase().indexOf("cable") != -1 || vm.selectedArticuloDescarga.articulo.toLowerCase().indexOf("aire comprimido") != -1) {
                            if (vm.iinicial == undefined && vm.ifinal == undefined && vm.finicial == undefined && vm.ffinal == undefined) {
                                new PNotify({
                                    title: 'Error',
                                    text: 'Por favor llene los campos del metraje.',
                                    icon: 'fa fa-info-circle',
                                    type: 'error',
                                    hide: true
                                });
                            } else {
                                if (vm.iinicial < vm.ifinal && vm.finicial == 0 && vm.ffinal == 0) {
                                    if (vm.iinicial == 0 || vm.ifinal == 0) {
                                        new PNotify({
                                            title: 'Error',
                                            text: 'El rango de los metrajes interiores no pueden ir en cero.',
                                            icon: 'fa fa-info-circle',
                                            type: 'error',
                                            hide: true
                                        });
                                    } else {
                                        vm.cantidad_total = vm.ifinal - vm.iinicial;
                                        ordersFactory.consultarExistencia(data.plaza, data.tecnico, vm.selectedArticuloDescarga.id, vm.cantidad_total)
                                            .then(function (data) {
                                                if (data == 4) {
                                                    guardarDetalle(1);
                                                } else {
                                                    new PNotify({
                                                        title: 'Error',
                                                        text: 'El técnico no cuenta con el material suficiente.',
                                                        icon: 'fa fa-info-circle',
                                                        type: 'error',
                                                        hide: true
                                                    });
                                                }
                                        });
                                    }
                                } else if (vm.finicial < vm.ffinal && vm.iinicial == 0 && vm.ifinal == 0) {
                                    if (vm.finicial == 0 || vm.ffinal == 0) {
                                        new PNotify({
                                            title: 'Error',
                                            text: 'El rango de los metrajes interiores no pueden ir en cero.',
                                            icon: 'fa fa-info-circle',
                                            type: 'error',
                                            hide: true
                                        });
                                    } else {
                                        vm.cantidad_total = vm.ffinal - vm.finicial;
                                        ordersFactory.consultarExistencia(data.plaza, data.tecnico, vm.selectedArticuloDescarga.id, vm.cantidad_total)
                                            .then(function (data) {
                                                if (data == 4) {
                                                    guardarDetalle(1);
                                                } else {
                                                    new PNotify({
                                                        title: 'Error',
                                                        text: 'El técnico no cuenta con el material suficiente.',
                                                        icon: 'fa fa-info-circle',
                                                        type: 'error',
                                                        hide: true
                                                    });
                                                }
                                        });
                                    }
                                } else if (vm.ifinal <= vm.iinicial || vm.ffinal <= vm.finicial) {
                                    new PNotify({
                                        title: 'Error',
                                        text: 'El rango de los metrajes no se pueden interceptar.',
                                        icon: 'fa fa-info-circle',
                                        type: 'error',
                                        hide: true
                                    });
                                } else {
                                    if (vm.iinicial > vm.finicial) {
                                        if (vm.iinicial <= vm.ffinal) {
                                            new PNotify({
                                                title: 'Error',
                                                text: 'El rango de los metrajes no se pueden interceptar.',
                                                icon: 'fa fa-info-circle',
                                                type: 'error',
                                                hide: true
                                            });
                                        } else {
                                            var interior = vm.ifinal - vm.iinicial;
                                            var exterior = vm.ffinal - vm.finicial;

                                            vm.cantidad_total = interior + exterior;
                                            ordersFactory.consultarExistencia(data.plaza, data.tecnico, vm.selectedArticuloDescarga.id, vm.cantidad_total)
                                                .then(function (data) {
                                                    if (data == 4) {
                                                        guardarDetalle(1);
                                                    } else {
                                                        new PNotify({
                                                            title: 'Error',
                                                            text: 'El técnico no cuenta con el material suficiente.',
                                                            icon: 'fa fa-info-circle',
                                                            type: 'error',
                                                            hide: true
                                                        });
                                                    }
                                            });
                                        }
                                    } else {
                                        if (vm.finicial <= vm.ifinal) {
                                            new PNotify({
                                                title: 'Error',
                                                text: 'El rango de los metrajes no se pueden interceptar.',
                                                icon: 'fa fa-info-circle',
                                                type: 'error',
                                                hide: true
                                            });
                                        } else {
                                            var interior = vm.ifinal - vm.iinicial;
                                            var exterior = vm.ffinal - vm.finicial;
                                            vm.cantidad_total = interior + exterior;
                                            if (vm.ffinal == undefined || vm.finicial == undefined) {
                                                new PNotify({
                                                    title: 'Error',
                                                    text: 'Por favor llene todos los metrajes.',
                                                    icon: 'fa fa-info-circle',
                                                    type: 'error',
                                                    hide: true
                                                });
                                            } else {
                                                ordersFactory.consultarExistencia(data.plaza, data.tecnico, vm.selectedArticuloDescarga.id, vm.cantidad_total)
                                                    .then(function (data) {
                                                        if (data == 4) {
                                                            guardarDetalle(1);
                                                        } else {
                                                            new PNotify({
                                                                title: 'Error',
                                                                text: 'El técnico no cuenta con el material suficiente.',
                                                                icon: 'fa fa-info-circle',
                                                                type: 'error',
                                                                hide: true
                                                            });
                                                        }
                                                });
                                            }
                                        }
                                    }

                                }
                            }
                        } else {
                            if (vm.cantidad <= 0 || vm.cantidad == undefined) {
                                new PNotify({
                                    title: 'Error',
                                    text: 'La cantidad debe ser mayor a 0.',
                                    icon: 'fa fa-info-circle',
                                    type: 'error',
                                    hide: true
                                });
                            } else {
                                ordersFactory.consultarExistencia(data.plaza, data.tecnico, vm.selectedArticuloDescarga.id, vm.cantidad)
                                    .then(function (data) {
                                        if (data == 4) {
                                            guardarDetalle(2);
                                        } else {
                                            new PNotify({
                                                title: 'Error',
                                                text: 'El técnico no cuenta con el material suficiente.',
                                                icon: 'fa fa-info-circle',
                                                type: 'error',
                                                hide: true
                                            });
                                        }
                                });
                            }
                        }
                    }
                    
                } else {
                    completaCampos();
                }
            } else {
                completaCampos();
            }
        } else {
            completaCampos();
        }

        function guardarDetalle(flag) {
            if (flag == 2) {
                ordersFactory.guardarMaterial(data.plaza, data.clv_orden, vm.selectedAlmacen.descripcion, vm.selectedArticuloDescarga.noArticulo, vm.selectedArticuloDescarga.articulo, data.tecnicoNombre, vm.cantidad, 0, 0, 0, 0, data.session)
                    .then(function (data) {
                        actualizarTabla();
                });
            } else {
                ordersFactory.guardarMaterial(data.plaza, data.clv_orden, vm.selectedAlmacen.descripcion, vm.selectedArticuloDescarga.noArticulo, vm.selectedArticuloDescarga.articulo, data.tecnicoNombre, vm.cantidad_total, vm.iinicial, vm.ifinal, vm.finicial, vm.ffinal, data.session)
                    .then(function (data) {
                        actualizarTabla();
                });
            }
     
        }


        function consultarRepetido(x) {
            var result = 0;
            if (vm.articulosGuardados != undefined) {
                for (var i = 0; i < vm.articulosGuardados.length; i++) {
                    if (vm.articulosGuardados[i].descripcion == x) {
                        new PNotify({
                            title: 'Error',
                            text: 'El articulo ya esta agregado.',
                            icon: 'fa fa-info-circle',
                            type: 'error',
                            hide: true
                        });
                        result = 1;
                    }
                }
            }
            return result;
        }      
        
    }

    function actualizarTabla() {
        ordersFactory.detalleArticulosTabla(data.plaza, data.clv_orden, data.session)
            .then(function (data) {
                vm.articulosGuardados = data;
        });
    }

    function completaCampos() {
        new PNotify({
            title: 'Error',
            text: 'Por favor complete todos los campos.',
            icon: 'fa fa-info-circle',
            type: 'error',
            hide: true
        });
    }

    function cancel() {
        if (data.materialGuardado == false || data.materialGuardado == undefined) {
            ordersFactory.eliminarArticulosTabla(data.plaza, data.clv_orden).then(function (data) { });
            ordersFactory.eliminarTodoMaterialExtensiones(data.plaza, data.clv_orden).then(function (data) {});
        }
        $uibModalInstance.dismiss('cancel'); 
    }

    function ok() {

        var descarga = {};
        descarga.idPlaza = data.plaza;
        descarga.Session = data.session;
        descarga.idTecnico = data.tecnico;
        descarga.Articulos = vm.articulosGuardados;
        descarga.Orden = data.clv_orden;
        descarga.Contrato = data.contrato;
        descarga.clvCategoria = vm.selectedClasificacion.clv_categoria;

        ordersFactory.guardarDescargaMaterial(descarga)
            .then(function (data) {
                if (data == 1) {
                    $rootScope.$emit("HideTecnicos", {});
                    $rootScope.$emit("banderaMaterial", {});
                    $uibModalInstance.dismiss('cancel');
                } else {
                    new PNotify({
                        title: 'Error',
                        text: 'El técnico no cuenta con el material suficiente.',
                        icon: 'fa fa-info-circle',
                        type: 'error',
                        hide: true
                    });
                }
        });
    }
}

function ModalDescargaMaterialDetalleCtrl($uibModal, $uibModalInstance, $rootScope, data, ordersFactory) {
    var vm = this;
    vm.openExtensiones = openExtensiones;
    vm.cancel = cancel;

    if (data.adicionales == undefined) {
        vm.showExtensiones = false;
    } else {
        vm.showExtensiones = data.adicionales;
    }

    ordersFactory.consultarArticulosTabla(data.plaza, data.clv_orden)
        .then(function (data) {
            vm.articulosGuardados = data.articulos;
            vm.bitacora = data.bitacora;
            vm.almacen = data.almacen;
            vm.categoria = data.categoria;
    });

    function openExtensiones() {
        vm.animationsEnabled = true;
        var modalInstance = $uibModal.open({
            animation: vm.animationsEnabled,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/dist/js/pages/Ordenes/views/descargaMaterialExtensionesDetalle.tpl.html',
            controller: 'descargaExtensionesDestalleCtrl',
            controllerAs: 'ctrl',
            backdrop: 'static',
            keyboard: false,
            size: 'lg',
            resolve: {
                data: function () {
                    return data;
                }
            }
        });
    }

    function cancel() {
        $uibModalInstance.dismiss('cancel');
    }
}

function descargaExtensionesCtrl($uibModalInstance, $rootScope, data, ordersFactory) {
    var vm = this;
    actualizarTabla();
    var array = [];
    vm.changeClasificacion = changeClasificacion;
    vm.agregarArticulo = agregarArticulo;
    vm.changeArticuloDescarga = changeArticuloDescarga;
    vm.ok = ok;
    vm.cancel = cancel;
    vm.eliminarArticulo = eliminarArticulo;

    ordersFactory.detalleConet(data.plaza, data.clave, data.clv_orden, data.contrato)
        .then(function (data) {
            array.push({
                id: 0,
                label: "---------------"
            });
            for (var i = 0; i < data.extra; i++) {
                array.push({
                    id: i+1,
                    label: "Extensión #"+(i+1)
                });
            }
    });

    vm.selectExtensiones = array
    vm.selectedExtension = array[0];

    ordersFactory.getBitacoraDescarga(data.plaza, data.clv_orden)
        .then(function (data) {
            vm.bitacora = data.bitacora;
            if (vm.bitacora == 0) {
                vm.bitacora = '';
            }
            vm.almacenes = data.almacenes;
            data.almacenes.unshift({
                "id": 0,
                "descripcion": "--------------------------"
            });
            vm.selectedAlmacen = data.almacenes[0];
            data.clasificaciones.unshift({
                "clv_tipo": 0,
                "concepto": "--------------------------"
            });
            vm.clasificaciones = data.clasificaciones;
            vm.selectedClasificacion = data.clasificaciones[0];
    });

    function changeClasificacion() {
        if (vm.selectedClasificacion.clv_tipo != 0) {
            ordersFactory.getArticulosDescarga(data.plaza, data.tecnico, vm.selectedClasificacion.clv_tipo)
                .then(function (data) {
                    data.unshift({
                        "clave": 0,
                        "articulo": "--------------------------",
                    });
                    vm.articulos = data;
                    vm.selectedArticuloDescarga = data[0];
            });
        }

    }

    function agregarArticulo() {
        if (vm.selectedAlmacen.id != 0) {
            if (vm.selectedClasificacion.clv_tipo != 0) {
                if (vm.selectedArticuloDescarga.clave != 0) {
                    var repetido = consultarRepetido(vm.selectedArticuloDescarga.articulo);
                    if (repetido == 0) {
                        if (vm.selectedArticuloDescarga.articulo.toLowerCase().indexOf("cable") != -1 || vm.selectedArticuloDescarga.articulo.toLowerCase().indexOf("aire comprimido") != -1) {
                            if (vm.iinicial == undefined && vm.ifinal == undefined && vm.finicial == undefined && vm.ffinal == undefined) {
                                new PNotify({
                                    title: 'Error',
                                    text: 'Por favor llene los campos del metraje.',
                                    icon: 'fa fa-info-circle',
                                    type: 'error',
                                    hide: true
                                });
                            } else {
                                if (vm.iinicial < vm.ifinal && vm.finicial == 0 && vm.ffinal == 0) {
                                    if (vm.iinicial == 0 || vm.ifinal == 0) {
                                        new PNotify({
                                            title: 'Error',
                                            text: 'El rango de los metrajes interiores no pueden ir en cero.',
                                            icon: 'fa fa-info-circle',
                                            type: 'error',
                                            hide: true
                                        });
                                    } else {
                                        vm.cantidad_total = vm.ifinal - vm.iinicial;
                                        ordersFactory.consultarExistencia(data.plaza, data.tecnico, vm.selectedArticuloDescarga.id, vm.cantidad_total)
                                            .then(function (data) {
                                                if (data == 4) {
                                                    guardarDetalle(1);
                                                } else {
                                                    new PNotify({
                                                        title: 'Error',
                                                        text: 'El técnico no cuenta con el material suficiente.',
                                                        icon: 'fa fa-info-circle',
                                                        type: 'error',
                                                        hide: true
                                                    });
                                                }
                                        });
                                    }
                                } else if (vm.finicial < vm.ffinal && vm.iinicial == 0 && vm.ifinal == 0) {
                                    if (vm.finicial == 0 || vm.ffinal == 0) {
                                        new PNotify({
                                            title: 'Error',
                                            text: 'El rango de los metrajes interiores no pueden ir en cero.',
                                            icon: 'fa fa-info-circle',
                                            type: 'error',
                                            hide: true
                                        });
                                    } else {
                                        vm.cantidad_total = vm.ffinal - vm.finicial;
                                        ordersFactory.consultarExistencia(data.plaza, data.tecnico, vm.selectedArticuloDescarga.id, vm.cantidad_total)
                                            .then(function (data) {
                                                if (data == 4) {
                                                    guardarDetalle(1);
                                                } else {
                                                    new PNotify({
                                                        title: 'Error',
                                                        text: 'El técnico no cuenta con el material suficiente.',
                                                        icon: 'fa fa-info-circle',
                                                        type: 'error',
                                                        hide: true
                                                    });
                                                }
                                        });
                                    }
                                } else if (vm.ifinal <= vm.iinicial || vm.ffinal <= vm.finicial) {
                                    new PNotify({
                                        title: 'Error',
                                        text: 'El rango de los metrajes no se pueden interceptar.',
                                        icon: 'fa fa-info-circle',
                                        type: 'error',
                                        hide: true
                                    });
                                } else {
                                    if (vm.iinicial > vm.finicial) {
                                        if (vm.iinicial <= vm.ffinal) {
                                            new PNotify({
                                                title: 'Error',
                                                text: 'El rango de los metrajes no se pueden interceptar.',
                                                icon: 'fa fa-info-circle',
                                                type: 'error',
                                                hide: true
                                            });
                                        } else {
                                            var interior = vm.ifinal - vm.iinicial;
                                            var exterior = vm.ffinal - vm.finicial;

                                            vm.cantidad_total = interior + exterior;
                                            ordersFactory.consultarExistencia(data.plaza, data.tecnico, vm.selectedArticuloDescarga.id, vm.cantidad_total)
                                                .then(function (data) {
                                                    if (data == 4) {
                                                        guardarDetalle(1);
                                                    } else {
                                                        new PNotify({
                                                            title: 'Error',
                                                            text: 'El técnico no cuenta con el material suficiente.',
                                                            icon: 'fa fa-info-circle',
                                                            type: 'error',
                                                            hide: true
                                                        });
                                                    }
                                            });
                                        }
                                    } else {
                                        if (vm.finicial <= vm.ifinal) {
                                            new PNotify({
                                                title: 'Error',
                                                text: 'El rango de los metrajes no se pueden interceptar.',
                                                icon: 'fa fa-info-circle',
                                                type: 'error',
                                                hide: true
                                            });
                                        } else {
                                            var interior = vm.ifinal - vm.iinicial;
                                            var exterior = vm.ffinal - vm.finicial;
                                            vm.cantidad_total = interior + exterior;
                                            if (vm.ffinal == undefined || vm.finicial == undefined) {
                                                new PNotify({
                                                    title: 'Error',
                                                    text: 'Por favor llene todos los metrajes.',
                                                    icon: 'fa fa-info-circle',
                                                    type: 'error',
                                                    hide: true
                                                });
                                            } else {
                                                ordersFactory.consultarExistencia(data.plaza, data.tecnico, vm.selectedArticuloDescarga.id, vm.cantidad_total)
                                                    .then(function (data) {
                                                        if (data == 4) {
                                                            guardarDetalle(1);
                                                        } else {
                                                            new PNotify({
                                                                title: 'Error',
                                                                text: 'El técnico no cuenta con el material suficiente.',
                                                                icon: 'fa fa-info-circle',
                                                                type: 'error',
                                                                hide: true
                                                            });
                                                        }
                                                });
                                            }
                                        }
                                    }

                                }
                            }
                        } else {
                            if (vm.cantidad <= 0 || vm.cantidad == undefined) {
                                new PNotify({
                                    title: 'Error',
                                    text: 'La cantidad debe ser mayor a 0.',
                                    icon: 'fa fa-info-circle',
                                    type: 'error',
                                    hide: true
                                });
                            } else {
                                ordersFactory.consultarExistencia(data.plaza, data.tecnico, vm.selectedArticuloDescarga.id, vm.cantidad)
                                    .then(function (data) {
                                        if (data == 4) {
                                            guardarDetalle(2);
                                        } else {
                                            new PNotify({
                                                title: 'Error',
                                                text: 'El técnico no cuenta con el material suficiente.',
                                                icon: 'fa fa-info-circle',
                                                type: 'error',
                                                hide: true
                                            });
                                        }
                                });
                            }
                        }
                    }

                    } else {
                        completaCampos();
                    }
                } else {
                    completaCampos();
                }
            } else {
                completaCampos();
            }
        }

        function completaCampos() {
            new PNotify({
                title: 'Error',
                text: 'Por favor complete todos los campos.',
                icon: 'fa fa-info-circle',
                type: 'error',
                hide: true
            });
        }
        function guardarDetalle(flag) {
            console.log(vm.selectedExtension);
            if (vm.selectedExtension == undefined) {
                completaCampos();
            } else {
                if (vm.selectedExtension.id == 0) {
                    new PNotify({
                        title: 'Error',
                        text: 'Por favor complete todos los campos.',
                        icon: 'fa fa-info-circle',
                        type: 'error',
                        hide: true
                    });
                } else {
                    if (flag == 2) {
                        ordersFactory.addArticuloExtensiones(data.plaza, data.clv_orden, vm.selectedArticuloDescarga.clave, data.tecnico, vm.selectedAlmacen.id, 0, 0, 0, 0, vm.cantidad, vm.selectedExtension.id)
                            .then(function (data) {
                                actualizarTabla();
                        });
                    } else {
                        ordersFactory.addArticuloExtensiones(data.plaza, data.clv_orden, vm.selectedArticuloDescarga.clave, data.tecnico, vm.selectedAlmacen.id, vm.iinicial, vm.ifinal, vm.finicial, vm.ffinal, vm.cantidad_total, vm.selectedExtension.id)
                            .then(function (data) {
                                actualizarTabla();
                        });
                    }
                }
                
            }
            
        }
        
        function actualizarTabla(){
            ordersFactory.consultarArticulosTablaExtensiones(data.plaza, data.clv_orden)
                .then(function (data) {
                    vm.articulosGuardados = data.articulos;
            });
        }

        function consultarRepetido(x) {
            var result = 0;
            if (vm.articulosGuardados != undefined) {
                for (var i = 0; i < vm.articulosGuardados.length; i++) {
                    if (vm.articulosGuardados[i].descripcion == x) {
                        new PNotify({
                            title: 'Error',
                            text: 'El articulo ya esta agregado.',
                            icon: 'fa fa-info-circle',
                            type: 'error',
                            hide: true
                        });
                        result = 1;
                    }
                }
            }
            return result;
        }  

        function changeArticuloDescarga() {
            if (vm.selectedArticuloDescarga.articulo.toLowerCase().indexOf("cable") != -1 || vm.selectedArticuloDescarga.articulo.toLowerCase().indexOf("aire comprimido") != -1) {
                vm.showMetraje = true;
                vm.showCantidad = false;
            } else {
                vm.showCantidad = true;
                vm.showMetraje = false;
            }
        }

    if (data.esGuardar == true) {
        vm.disbledGuardar = false;
        vm.btnGuardar = true;
    } else {
        vm.disbledGuardar = true;
        vm.btnGuardar = false;
    }
    
    function eliminarArticulo(x) {
        ordersFactory.eliminarMaterialExtensiones(data.plaza, x)
            .then(function (data) {
                actualizarTabla();
        });
    }
    
    function cancel() {
        ordersFactory.eliminarTodoMaterialExtensiones(data.plaza, data.clv_orden)
            .then(function (data) {
                $uibModalInstance.dismiss('cancel');
        });
        
    }
    
    function ok() {
        $uibModalInstance.dismiss('cancel');
    }
}

function descargaExtensionesDestalleCtrl($uibModalInstance, $rootScope, data, ordersFactory) {
    var vm = this;
    actualizarTabla();
    function actualizarTabla() {
        ordersFactory.consultarExtencionesArticulosDetalle(data.plaza, data.clv_orden)
            .then(function (data) {
                vm.articulosGuardados = data.articulos;
                vm.almacen = data.almacen;
                vm.categoria = data.categoria;
                vm.articulo = data.articulo;
        });
    }

    if (data.esGuardar == true) {
        vm.disbledGuardar = false;
        vm.btnGuardar = true;
    } else {
        vm.disbledGuardar = true;
        vm.btnGuardar = false;
    }

    vm.cancel = function () {
            $uibModalInstance.dismiss('cancel');
    }
}

function ModalAsignacionCtrl($uibModalInstance, $rootScope, items, ordersFactory) {
    var vm = this;
    vm.cancel = cancel;

    function cancel() {
        $uibModalInstance.dismiss('cancel');
    }
}

function bajaPaqueteDeatelleCtrl($uibModalInstance, $rootScope, data, ordersFactory) {
    var vm = this;
    vm.cablemodems = [];
    vm.titulo = "Baja";
    vm.contratosNet = [];
    var cambios = 0;
    vm.activaCambios = activaCambios;
    vm.saveContratoNet = saveContratoNet;
    vm.ok = ok;
    vm.cancel = cancel;

    ordersFactory.getCablemodems(data.plaza, data.contrato, data.orden, data.clave)
        .then(function (data) {
            for (var i = 0; i < data.length; i++) {
                vm.cablemodems.push({
                    ContratoNet: data[i].ContratoNet,
                    Mac: data[i].Mac,
                    Detalle: data[i].Detalle,
                    checado: false
                });
            }
    });
   
    function activaCambios() {
        cambios = 1;
    }

    function saveContratoNet(contrato) {
        if (contrato.selectedMac == true) {
            vm.contratosNet.push({
                ContratoNet: contrato.ContratoNet,
                Mac: contrato.Mac
            });
        } else {
            vm.contratosNet.forEach(function (element, index, array) {
                if (element.ContratoNet == contrato.ContratoNet) {
                    vm.contratosNet.splice(index, 1);
                }
            });
        }

    }

    function ok() {
        var objeto = {};
        objeto.idPlaza = data.plaza;
        objeto.Clave = data.clave;
        objeto.Orden = data.orden;
        objeto.Macs = vm.contratosNet;
        objeto.Status = "B";
        if(cambios == 1){
            ordersFactory.eliminarTodoBPAQU(data.plaza, data.orden)
                .then(function (data) {
                    ordersFactory.bajaPaquete(objeto).then(function (data) {
                        $uibModalInstance.dismiss('cancel');
                    });
            });
        } else {
            $uibModalInstance.dismiss('cancel');
        }
    }

    ordersFactory.consultarBPAQU(data.plaza, data.clave, data.orden).then(function (data) {
        for (var i = 0; i < data.length; i++) {
            for (var j = 0; j < vm.cablemodems.length; j++) {
                if (vm.cablemodems[j].ContratoNet == data[i].contratoNet) {
                    vm.cablemodems[j].checado = true;
                }
            }
        }
    });

    function cancel() {
        $uibModalInstance.dismiss('cancel');
    }
}

function ModalExtensionActualizarCtrl($uibModalInstance, ordersFactory, items, $rootScope) {

}