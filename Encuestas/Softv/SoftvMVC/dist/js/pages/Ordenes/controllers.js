ordersApp
    .controller('showOrders', showOrders)
    .controller('ModalAddCtrl', ModalAddCtrl)
    .controller('ModalServiceCtrl', ModalServiceCtrl)
    .controller('ModalDomicilioCtrl', ModalDomicilioCtrl)
    .controller('ModalExtensionCtrl', ModalExtensionCtrl)
    .controller('ModalExtensionAddCtrl', ModalExtensionAddCtrl)
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
    .controller('ModalDescargaMaterialCtrl', ModalDescargaMaterialCtrl);


function showOrders(ordersFactory, $scope, $uibModal, $log, $rootScope) {
    var vm = this;
    vm.showPanels = true;
    vm.idPlaza;
    vm.showBtnNuevo = true;
    vm.showBtnBuscar = true;
    vm.showBtnEjecutar = true;
    vm.showBtnConsultar = true;
    ordersFactory.getListPlaces().then(function (data) {
        data.unshift({
            "Plaza": "----------------",
            "IdConexion": 0
        });
        vm.places = data;
        vm.selectedPlace = vm.places[0];
    });
    
    vm.buscarOrden = function (id) {
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


    vm.changePlace = function () {
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
            ordersFactory.getDetailOrders(vm.idPlaza, contrato).then(function (data) {
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


    vm.open = function (size) {

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

    vm.openConsultar = function (orden) {
        ordersFactory.consultarDetalleOrden(vm.idPlaza, orden).then(function (data) {
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

    vm.openEjecutar = function (orden) {
        ordersFactory.consultarDetalleOrden(vm.idPlaza, orden).then(function (data) {
            data.plaza = vm.idPlaza;
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

}


function ModalAddCtrl($uibModal, $uibModalInstance, ordersFactory, plaza, $rootScope) {
    var vm = this;
    var d = new Date();
    vm.fecha = d.toLocaleDateString();
    ordersFactory.getDataTecnicos(plaza).then(function (data) {
        data.unshift({
            "Nombre": "Default",
            "clvTecnico": 0
        });
        vm.tecnicos = data;
        vm.selectedTecnico = data[0];
        vm.tecnicoVisible = true;
    });


    $rootScope.$on("CallParentMethod", function () {
        vm.showAllOrders();
    });

    vm.showAllOrders = function () {
        ordersFactory.getAllOrders(plaza, vm.noOrden).then(function (data) {
             vm.DetailsOrders = data;
        });
    }


    vm.deleteDetalle = function (clave, index) {
        ordersFactory.deleteDetailOrder(plaza, clave).then(function (data) {
            new PNotify({
                title: 'Eliminación Exitosa',
                text: 'Detalle de orden eliminada!',
                type: 'success'
            });
            vm.DetailsOrders.splice(index, 1);
        });
    }

    vm.ok = function () {
        var flag = 0;
        for (var i = 0; i < vm.DetailsOrders.length; i++) {
            if (vm.DetailsOrders[i].descripcion = "BPAQU - BAJA DE PAQUETE DE INTERNET") {
                flag = 1;
                var items = {
                    plaza: plaza,
                    orden: vm.noOrden
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

    vm.buscarCliente = function () {
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

    vm.detalleExtra = function (x) {
        if (x.accion == "Ext. Adicionales") {
            ordersFactory.detalleConet(plaza, x.clave, x.clv_orden, vm.contratoCliente).then(function (data) {
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
            ordersFactory.detalleCamdo(plaza, x.clave, x.clv_orden, vm.contratoCliente).then(function (data) {
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

    vm.open = function (size) {
        if (vm.contratoCliente != undefined && vm.contratoCliente != null && vm.contratoCliente != "") {
            vm.items = {
                plaza: plaza,
                contrato: vm.contratoCliente,
                orden: vm.noOrden
            };
            if (vm.noOrden == undefined) {
                ordersFactory.addPre(plaza, vm.contratoCliente, vm.observaciones).then(function (data) {
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

    vm.cancel = function () {
        if (vm.noOrden == undefined || vm.noOrden == null || vm.noOrden == "") {
            $uibModalInstance.dismiss('cancel');
        } else {
            ordersFactory.cancelOrder(plaza, vm.noOrden).then(function (data) {
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
    ordersFactory.motivosCancelacion(items.plaza).then(function (data) {
        vm.motivos = data;
        vm.selectedMotivo = data[0];
    });
    vm.ok = function () {
        vm.cancel();
        ordersFactory.guardarMotivo(items.plaza, items.orden, vm.selectedMotivo.clv_motivo).then(function (data) {
            $rootScope.$emit("guardarDetalle", {});
        });
    }
    vm.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function ModalServiceCtrl($uibModal, $uibModalInstance, ordersFactory, items, $rootScope) {
    
    var vm = this;
    vm.realizaTrabajo = true;
    ordersFactory.getDataServices(items.plaza, items.contrato).then(function (data) {
        vm.servicios = data;
        data.unshift({
            "clv_servicio": 0,
            "servicio": "--------------------------"
        });
        vm.selectedService = data[0];
    });

    vm.changeService = function () {
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
    vm.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    vm.ok = function () {
        ordersFactory.getDataActivo(items.plaza, vm.selectedService.clv_servicio, items.contrato).then(function (data) {
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
                } else if (vm.selectedTrabajo.descripcion ==  "IPAQU - Instalación De Paquete") {
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
                }
                ordersFactory.saveDetailOrder(items.plaza, items.orden, vm.selectedTrabajo.clv_trabajo, vm.observaciones, trabajo, vm.selectedService.clv_servicio).then(function (data) {
                    items.clave = data;
                    $rootScope.$emit("CallParentMethod", {});
                    $uibModalInstance.dismiss('cancel');
                });
                
            }
        });
    }
}

function ModalDomicilioCtrl($uibModalInstance, ordersFactory, items, $rootScope) {
    var vm = this;

    ordersFactory.getCiudades(items.plaza).then(function (data) {
        data.unshift({
            "Clv_Ciudad": 0,
            "Nombre": "--------------------------"
        });
        vm.ciudades = data;
        vm.selectedCiudad = data[0];
    });

    vm.changeCiudad = function () {
        if (vm.selectedCiudad.Clv_Ciudad != 0) {
            ordersFactory.getColonia(items.plaza, vm.selectedCiudad.Clv_Ciudad).then(function (data) {
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

    vm.changeColonia = function () {
        if (vm.selectedColonia.clv_colonia != 0) {
            ordersFactory.getCalle(items.plaza, vm.selectedColonia.clv_colonia).then(function (data) {
                data.unshift({
                    "Clv_Calle": 0,
                    "Nombre": "--------------------------"
                });
                vm.calles = data;
                vm.selectedCalle = data[0];
            });
        }
    }

    vm.ok = function () {
        if (vm.selectedCiudad.Clv_Ciudad == 0 || vm.selectedColonia.clv_colonia == 0 || vm.selectedCalle.Clv_Calle == 0 || vm.numero == undefined) {
            new PNotify({
                title: 'Error',
                text: 'Por favor llene los campos correctamente.',
                icon: 'fa fa-info-circle',
                type: 'error',
                hide: true
            });
        } else {
            ordersFactory.saveCambioDomicilio(items.plaza, items.clave, items.orden, items.contrato, vm.selectedCiudad.Clv_Ciudad, vm.selectedColonia.clv_colonia, vm.selectedCalle.Clv_Calle, vm.numero,vm.telefono,vm.entreCalles, vm.numeroInterior).then(function (data) {
                $uibModalInstance.dismiss('cancel'); 
            });
        }
    }

    vm.cancel = function () {
        ordersFactory.deleteDetailOrder(items.plaza, items.clave).then(function (data) {
            $rootScope.$emit("CallParentMethod", {});
        });
        $uibModalInstance.dismiss('cancel');
    };
}

function ModalExtensionCtrl($uibModalInstance, ordersFactory, items, $rootScope) {

    var vm = this;

    ordersFactory.getExtensiones(items.plaza, items.contrato).then(function (data) {
        vm.extInstaladas = data;
    });
    vm.extCancelar = 1;

    vm.cancel = function () {
        ordersFactory.deleteDetailOrder(items.plaza, items.clave).then(function (data) {
            $rootScope.$emit("CallParentMethod", {});
        });
        $uibModalInstance.dismiss('cancel');
    };

    vm.ok = function () {
        ordersFactory.tieneCanexConex(items.plaza, items.orden).then(function (data) {
            if (data.conex > 0 || data.canex > 0) {
                if (data.conex > 0) {
                    alert('cancelacion');
                    new PNotify({
                        title: 'Error',
                        text: 'Existe una orden de cancelación de extensión por lo cual no se puede agregar este concepto.',
                        icon: 'fa fa-info-circle',
                        type: 'error',
                        hide: true
                    });
                } else if (data.canex > 0) {
                    alert('contratacion');
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
    }
}

function ModalExtensionAddCtrl($uibModalInstance, ordersFactory, items, $rootScope) {
    var vm = this;

    vm.extAgregar = 1;
    vm.cancel = function () {
        ordersFactory.deleteDetailOrder(items.plaza, items.clave).then(function (data) {
            $rootScope.$emit("CallParentMethod", {});
        });
        $uibModalInstance.dismiss('cancel');
    };

    vm.ok = function () {
        ordersFactory.tieneCanexConex(items.plaza, items.orden).then(function (data) {
            if (data.conex > 0 || data.canex > 0) {
                if (data.conex > 0) {
                    alert('cancelacion');
                    new PNotify({
                        title: 'Error',
                        text: 'Existe una orden de cancelación de extensión por lo cual no se puede agregar este concepto.',
                        icon: 'fa fa-info-circle',
                        type: 'error',
                        hide: true
                    });
                } else if (data.canex > 0) {
                    alert('contratacion');
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
    }
}

function ModalBajaPaqueteCtrl($uibModalInstance, ordersFactory, items, $rootScope, $http) {
    var vm = this;

    ordersFactory.getCablemodems(items.plaza, items.contrato).then(function (data) {
        vm.cablemodems = data;
    });

    vm.titutlo = "Baja";
    vm.contratosNet = [];
    vm.saveContratoNet = function (contrato) {
        if (contrato.selectedMac == true) {
            vm.contratosNet.push({ ContratoNet: contrato.ContratoNet, Mac: contrato.Mac });
        }else{
            vm.contratosNet.forEach(function (element, index, array) {
                if (element.ContratoNet == contrato.ContratoNet) {
                    vm.contratosNet.splice(index, 1);
                }
            });
        }
        
    }

    vm.ok = function () {
        var objeto = {};
        objeto.idPlaza = items.plaza;
        objeto.Clave = items.clave;
        objeto.Orden = items.orden;
        objeto.Macs = vm.contratosNet;
        objeto.Status = "B";
        ordersFactory.bajaPaquete(objeto).then(function (data) {
            $uibModalInstance.dismiss('cancel');
        });
    }

    vm.cancel = function () {
        ordersFactory.deleteDetailOrder(items.plaza, items.clave).then(function (data) {
            $rootScope.$emit("CallParentMethod", {});
        });
        $uibModalInstance.dismiss('cancel');
    };
}

function ModalDomicilioNetCtrl($uibModalInstance, ordersFactory, items, $rootScope) {
    var vm = this;

    ordersFactory.getCiudades(items.plaza).then(function (data) {
        data.unshift({
            "Clv_Ciudad": 0,
            "Nombre": "--------------------------"
        });
        vm.ciudades = data;
        vm.selectedCiudad = data[0];
    });

    vm.changeCiudad = function () {
        if (vm.selectedCiudad.Clv_Ciudad != 0) {
            ordersFactory.getColonia(items.plaza, vm.selectedCiudad.Clv_Ciudad).then(function (data) {
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

    vm.changeColonia = function () {
        if (vm.selectedColonia.clv_colonia != 0) {
            ordersFactory.getCalle(items.plaza, vm.selectedColonia.clv_colonia).then(function (data) {
                data.unshift({
                    "Clv_Calle": 0,
                    "Nombre": "--------------------------"
                });
                vm.calles = data;
                vm.selectedCalle = data[0];
            });
        }
    }

    vm.ok = function () {
        if (vm.selectedCiudad.Clv_Ciudad == 0 || vm.selectedColonia.clv_colonia == 0 || vm.selectedCalle.Clv_Calle == 0 || vm.numero == undefined) {
            new PNotify({
                title: 'Error',
                text: 'Por favor llene los campos correctamente.',
                icon: 'fa fa-info-circle',
                type: 'error',
                hide: true
            });
        } else {
            ordersFactory.saveCambioDomicilio(items.plaza, items.clave, items.orden, items.contrato, vm.selectedCiudad.Clv_Ciudad, vm.selectedColonia.clv_colonia, vm.selectedCalle.Clv_Calle, vm.numero, vm.telefono, vm.entreCalles, vm.numeroInterior).then(function (data) {
                $uibModalInstance.dismiss('cancel');
            });
        }
    }

    vm.cancel = function () {
        ordersFactory.deleteDetailOrder(items.plaza, items.clave).then(function (data) {
            $rootScope.$emit("CallParentMethod", {});
        });
        $uibModalInstance.dismiss('cancel');
    };
}


function ModalDesconexionPaqueteCtrl($uibModalInstance, ordersFactory, items, $rootScope, $http) {
    var vm = this;

    ordersFactory.getCablemodems(items.plaza, items.contrato).then(function (data) {
        vm.cablemodems = data;
    });

    vm.titutlo = "Desconexión";
    vm.contratosNet = [];
    vm.saveContratoNet = function (contrato) {
        if (contrato.selectedMac == true) {
            vm.contratosNet.push({ ContratoNet: contrato.ContratoNet, Mac: contrato.Mac });
        } else {
            vm.contratosNet.forEach(function (element, index, array) {
                if (element.ContratoNet == contrato.ContratoNet) {
                    vm.contratosNet.splice(index, 1);
                }
            });
        }

    }

    vm.ok = function () {
        var objeto = {};
        objeto.idPlaza = items.plaza;
        objeto.Clave = items.clave;
        objeto.Orden = items.orden;
        objeto.Macs = vm.contratosNet;
        objeto.Status = "S";
        ordersFactory.bajaPaquete(objeto).then(function (data) {
            $uibModalInstance.dismiss('cancel');
        });
    }

    vm.cancel = function () {
        ordersFactory.deleteDetailOrder(items.plaza, items.clave).then(function (data) {
            $rootScope.$emit("CallParentMethod", {});
        });
        $uibModalInstance.dismiss('cancel');
    };
}


function ModalInstalacionPaqueteCtrl($uibModalInstance, ordersFactory, items, $rootScope, $http) {
    var vm = this;

    ordersFactory.getCablemodems(items.plaza, items.contrato).then(function (data) {
        vm.cablemodems = data;
    });

    vm.titutlo = "Instalación";
    vm.contratosNet = [];
    vm.saveContratoNet = function (contrato) {
        if (contrato.selectedMac == true) {
            vm.contratosNet.push({ ContratoNet: contrato.ContratoNet, Mac: contrato.Mac });
        } else {
            vm.contratosNet.forEach(function (element, index, array) {
                if (element.ContratoNet == contrato.ContratoNet) {
                    vm.contratosNet.splice(index, 1);
                }
            });
        }

    }

    vm.ok = function () {
        var objeto = {};
        objeto.idPlaza = items.plaza;
        objeto.Clave = items.clave;
        objeto.Orden = items.orden;
        objeto.Macs = vm.contratosNet;
        objeto.Status = "I";
        ordersFactory.bajaPaquete(objeto).then(function (data) {
            $uibModalInstance.dismiss('cancel');
        });
    }

    vm.cancel = function () {
        ordersFactory.deleteDetailOrder(items.plaza, items.clave).then(function (data) {
            $rootScope.$emit("CallParentMethod", {});
        });
        $uibModalInstance.dismiss('cancel');
    };
}


function ModalReconexionPaqueteCtrl($uibModalInstance, ordersFactory, items, $rootScope) {
    var vm = this;

    ordersFactory.getCablemodems(items.plaza, items.contrato).then(function (data) {
        vm.cablemodems = data;
    });

    vm.titulo = "Reconexión";
    vm.contratosNet = [];
    vm.saveContratoNet = function (contrato) {
        if (contrato.selectedMac == true) {
            vm.contratosNet.push({ ContratoNet: contrato.ContratoNet, Mac: contrato.Mac });
        } else {
            vm.contratosNet.forEach(function (element, index, array) {
                if (element.ContratoNet == contrato.ContratoNet) {
                    vm.contratosNet.splice(index, 1);
                }
            });
        }

    }

    vm.ok = function () {
        var objeto = {};
        objeto.idPlaza = items.plaza;
        objeto.Clave = items.clave;
        objeto.Orden = items.orden;
        objeto.Macs = vm.contratosNet;
        objeto.Status = "I";
        ordersFactory.bajaPaquete(objeto).then(function (data) {
            $uibModalInstance.dismiss('cancel');
        });
    }

    vm.cancel = function () {
        ordersFactory.deleteDetailOrder(items.plaza, items.clave).then(function (data) {
            $rootScope.$emit("CallParentMethod", {});
        });
        $uibModalInstance.dismiss('cancel');
    };
}

function ModalconsultarOrdenCtrl($uibModal, $uibModalInstance, detalle, ordersFactory) {
    var vm = this;

    vm.contratoCliente = detalle.contrato;
    vm.noOrden = detalle.clv_orden;
    if (detalle.status == "P") {;
        vm.pe = true;
    } else if (detalle.status == "E") {
        vm.ej = true;
    } else {
        vm.vis = true;
    }
    vm.mostrarDetallesDimicilio = false;
    for (var i = 0; i < detalle.detallesOrdenes.length; i++) {
        if (detalle.detallesOrdenes[i].accion == "Domicilio") {
             vm.mostrarDetallesDimicilio = true;
        }
    }
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
    if (detalle.status != "P") {
        vm.ejecuto = detalle.ejecuto;
    }
    vm.detalleExtra = function (x) {
        if (x.accion == "Ext. Adicionales") {
            ordersFactory.detalleConet(detalle.plaza, x.clave, x.clv_orden, detalle.contrato).then(function (data) {
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
            ordersFactory.detalleCamdo(detalle.plaza, x.clave, x.clv_orden, detalle.contrato).then(function (data) {
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
    vm.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }
}

function ModalEjecutarOrdenCtrl($uibModal, $uibModalInstance, detalle, ordersFactory) {
    var vm = this;

    vm.contratoCliente = detalle.contrato;
    vm.noOrden = detalle.clv_orden;
    vm.tipo = true;
    vm.fechaEjecutar = false;
    vm.fechaVisita = true;
    vm.mostrarDetallesDimicilio = false;
    for (var i = 0; i < detalle.detallesOrdenes.length; i++) {
        if (detalle.detallesOrdenes[i].accion == "Domicilio") {
            vm.mostrarDetallesDimicilio = true;
        }
    }

    vm.showFechas = function () {
        console.log(vm.tipo);
        if (vm.tipo == true) {
            vm.fechaEjecutar = false;
            vm.fechaVisita = true;
        } else {
            vm.fechaEjecutar = true;
            vm.fechaVisita = false;
        }
    }

    vm.descargaMaterial = function () {
        detalle.tecnico = vm.selectedTecnico.clvTecnico;
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

    ordersFactory.getDataTecnicos(detalle.plaza).then(function (data) {
        vm.tecnicos = data;
        vm.selectedTecnico = data[0];
    });
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
    if (detalle.status != "P") {
        vm.ejecuto = detalle.ejecuto;
    }
    vm.detalleExtra = function (x) {
        if (x.accion == "Ext. Adicionales") {
            ordersFactory.detalleConet(detalle.plaza, x.clave, x.clv_orden, detalle.contrato).then(function (data) {
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
            ordersFactory.detalleCamdo(detalle.plaza, x.clave, x.clv_orden, detalle.contrato).then(function (data) {
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
    vm.cancel = function () {
        $uibModalInstance.dismiss('cancel');
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

    vm.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }
}

function ModalDescargaMaterialCtrl($uibModalInstance, data, ordersFactory) {
    var vm = this;
    console.log(data);
    ordersFactory.getBitacoraDescarga(data.plaza, data.clv_orden).then(function (data) {
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

    vm.changeClasificacion = function () {
        if (vm.selectedClasificacion.clv_tipo != 0) {         
            ordersFactory.getArticulosDescarga(data.plaza, data.tecnico, vm.selectedClasificacion.clv_tipo).then(function (data) {
                data.unshift({
                    "clave": 0,
                    "articulo": "--------------------------"
                });
                vm.articulos = data;
                vm.selectedArticuloDescarga = data[0];
            });
        }
        
    }

    vm.changeArticuloDescarga = function () {
        if (vm.selectedArticuloDescarga.articulo.toLowerCase().indexOf("cable") != -1 || vm.selectedArticuloDescarga.articulo.toLowerCase().indexOf("aire comprimido") != -1) {
            vm.showMetraje = true;
            vm.showCantidad = false;
        } else {
            vm.showCantidad = true;
            vm.showMetraje = false;
        }
    }

    vm.agregarArticulo = function () {
        if (vm.selectedAlmacen.id != 0) {
            if (vm.selectedClasificacion.clv_tipo != 0) {
                if (vm.selectedArticuloDescarga.clave != 0) {
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
                            if (vm.iinicial < vm.ifinal && vm.finicial == 0 && vm.ffinal == 0 || vm.finicial == undefined || vm.ffinal == undefined) {
                                if (vm.iinicial == 0 || vm.ifinal == 0) {
                                    new PNotify({
                                        title: 'Error',
                                        text: 'El rango de los metrajes interiores no pueden ir en cero.',
                                        icon: 'fa fa-info-circle',
                                        type: 'error',
                                        hide: true
                                    });
                                } else {
                                    console.log('valido');
                                }
                            } else if (vm.iinicial == 0 && vm.ifinal == 0 && vm.finicial < vm.ffinal || vm.iinicial == undefined || vm.ifinal == undefined) {
                                console.log('valido');
                            }else if (vm.ifinal <= vm.iinicial || vm.finicial <= vm.ifinal || vm.ffinal <= vm.finicial) {
                                new PNotify({
                                    title: 'Error',
                                    text: 'El rango de los metrajes no se pueden interceptar.',
                                    icon: 'fa fa-info-circle',
                                    type: 'error',
                                    hide: true
                                });
                            } else {
                                console.log('valido');
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

    vm.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }
}