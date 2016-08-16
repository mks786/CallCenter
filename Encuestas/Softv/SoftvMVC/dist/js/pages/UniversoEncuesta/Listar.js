var tipo_de_busqueda = 0;
var tipo_fecha = 0;
$(document).ready(function () {
    LlenarTabla();
    $('.collapse').collapse('show');
    $.ajax({
        url: "/Conexion/Plazas/",
        type: "GET",
        success: function (data, textStatus, jqXHR) {
            for (var i = 0; i < data.length; i++) {
                $('#plazas').append($('<option>', {
                    value: data[i].IdConexion,
                    text: data[i].Plaza
                }));
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
    $('#plazas').on('change', function () {
        var id = $(this).val();
        $('#contenido_editar').show();
        $.ajax({
            url: "/Conexion/ListaConexiones/",
            type: "GET",
            success: function (data, textStatus, jqXHR) {
                $('#ciudades').removeAttr('disabled'); 
                $('#encuestas').removeAttr('disabled');
                $('#nombre_proceso').removeAttr('disabled');
                $('#ciudades').empty();
                $('#ciudades').append('<option disabled selected>--------------------------</option>');
                for (var i = 0; i < data.length; i++) {
                    if (id == data[i].IdPlaza) {
                        $('#ciudades').append($('<option>', {
                            value: data[i].IdPlaza,
                            text: data[i].Ciudad
                        }));
                    }

                }
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });
        $.ajax({
            url: "/CLIENTE/getTipoServicio/",
            type: "GET",
            data: { "IdPlaza": id },
            success: function (data, textStatus, jqXHR) {
                $('#tipo_servicio').empty();
                $('#tipo_servicio').append('<option selected disabled>-----------------------------------------</option>');
                for (var i = 0; i < data.length; i++) {
                    $('#tipo_servicio').append($('<option>', {
                        value: data[i].Clv_TipSer,
                        text: data[i].Concepto
                    }));
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });
    });

    $.ajax({
        url: "/UniversoEncuesta/todasEncuestas/",
        type: "GET",
        success: function (data, textStatus, jqXHR) {
            $('#encuestas').empty();
            $('#encuestas').append('<option selected disabled>--------------------</option>');
            for (var i = 0; i < data.length; i++) {
                $('#encuestas').append($('<option>', {
                    value: data[i].IdEncuesta,
                    text: data[i].TituloEncuesta
                }));
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
    $('input[type=radio][name=busqueda]').change(function () {
        if (this.value == '1') {
            $('#panel_fechas').hide();
            $('#panel_status').show();
            tipo_de_busqueda = 1;
        }
        else if (this.value == '2') {
            $('#panel_status').hide();
            $('#panel_fechas').show();
            tipo_de_busqueda = 2;
        }
    });
    $('input[type=radio][name=fechas]').change(function () {
        if (this.value == '1') {
            tipo_fecha = 1;
        } else if (this.value == '2') {
            tipo_fecha = 2;
        } else if (this.value == '3') {
            tipo_fecha = 3;
        }
    });
    
    $('#select_filtro').on('change', function () {
        var tipo = $(this).val();
        if(tipo == 1){
            LlenarTabla('', 1);
        } else if (tipo == 2) {
            LlenarTabla('', 2);
        } else {
            LlenarTabla('', '');
        }
    });
});

function LlenarTabla(cadena, filtro) {
    $('#TablaUniverso tbody > tr').remove();
    $('#TablaUniverso').dataTable({
        "processing": true,
        "serverSide": true,
        "bFilter": false,
        "dom": '<"toolbar">frtip',
        "bDestroy": true,
        "info": true,
        "stateSave": false,
        "lengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]],
        "ajax": {
            "url": "/ProcesoEncuesta/GetList/",
            "type": "GET",
            "data": {'cadena':cadena,'filtro':filtro}
        },
        "fnInitComplete": function (oSettings, json) {


        },
        "fnDrawCallback": function (oSettings) {

        },


        "columns": [
            { "data": "IdProcesoEnc", "orderable": false },
            { "data": "NombreProceso", "orderable": false },
            { "data": "Encuesta", "orderable": false },
            { "data": "TipSer", "orderable": false },
            { "data": "FechaInicio", "orderable": false },
            { "data": "Usuario", "orderable": false },
            { "data": "StatusEncuesta", "orderable": false },
            {
                sortable: false,
                "render": function (data, type, full, meta) {
                    return Opciones(full);  //Es el campo de opciones de la tabla.
                }
            }
        ],

        language: {
            processing: "Procesando información...",
            search: "Buscar&nbsp;:",
            lengthMenu: "Mostrar _MENU_ Elementos",
            info: "Mostrando   _START_ de _END_ Total _TOTAL_ elementos",
            infoEmpty: "No hay elemetos para mostrar",
            infoFiltered: "(filtrados _MAX_ )",
            infoPostFix: "",
            loadingRecords: "Búsqueda en curso...",
            zeroRecords: "No hay registros para mostrar",
            emptyTable: "No hay registros disponibles",
            paginate: {
                first: "Primera",
                previous: "Anterior",
                next: "Siguiente",
                last: "Ultima"
            },
            aria: {
                sortAscending: ": activer pour trier la colonne par ordre croissant",
                sortDescending: ": activer pour trier la colonne par ordre décroissant"
            }
        },


        "order": [[0, "asc"]]

    });
}

function Opciones(full) {
    var opc;
    if (full.StatusEncuesta == "Pendiente") {
        opc = "<a href='/ProcesoEncuesta/Details/" + full.IdProcesoEnc + "' class='btn btn-success btn-xs' type='button' id='" + full.IdProcesoEnc + "' onclick='aplicarEncuesta(this)'><i class='fa fa-chart' aria-hidden='true'></i> Aplicar</a> <button id='" + full.IdProcesoEnc + "' class='btn btn-xs btn-danger'><i class='fa fa-trash-o' aria-hidden='true'></i> Eliminar</button>";

    } else {
        opc = "<a href='/ProcesoEncuesta/Details/" + full.IdProcesoEnc + "' class='btn btn-primary btn-xs' type='button' id='" + full.IdProcesoEnc + "' onclick='aplicarEncuesta(this)'><i class='fa fa-chart' aria-hidden='true'></i> Consultar</a> <button id='" + full.IdProcesoEnc + "' class='btn btn-xs btn-danger'><i class='fa fa-trash-o' aria-hidden='true'></i> Eliminar</button>";
    }
        // opc = "<button class='btn btn-info btn-xs Detalle' type='button' id='" + id + "' onclick='MostrarDetalles(this)'><i class='fa fa-info' aria-hidden='true'></i> Detalles</button> <button class='btn btn-warning btn-xs Editar' type='button' id='" + id + "' onclick='editarLlamada(this)'><i class='fa fa-pencil' aria-hidden='true'></i> Editar</button>";
    return opc;
}



function MostrarModalProceso() {
    $('#NuevoProceso').modal('show');
}

function GuardarUniverso() {
    var universo = {};
    var plaza = $('#plazas').val();
    var ciudad = $('#ciudades option:selected').text();
    var encuesta = $('#encuestas').val();
    var encuesta_nombre = $('#encuestas option:selected').text();
    var nombre = $('#nombre_proceso').val();
    var tipo_servicio = $('#tipo_servicio').val();
    var servicio_nombre = $('#tipo_servicio option:selected').text();
    var fecha_creacion = new Date();
    var dd = fecha_creacion.getDate();
    var mm = fecha_creacion.getMonth() + 1; //January is 0!
    var yyyy = fecha_creacion.getFullYear();
    fecha_creacion = dd + '-' + mm + '-' + yyyy;
    universo.fecha_creacion = fecha_creacion;
    universo.plaza = plaza;
    if(ciudad == "--------------------------"){
        universo.ciudad = null;
    } else {
        universo.ciudad = ciudad;
    }
    
    universo.encuesta = encuesta;
    universo.NombreProceso = nombre;
    universo.usuario = usuario;
    universo.tipo_busqueda_id = tipo_de_busqueda;
    universo.encuesta_nombre = encuesta_nombre;
    if(plaza == null){
        swal("Selecciona una plaza", "", "error");
    } else {
        if(encuesta == null){
            swal("Selecciona una encuesta", "", "error");
        } else {
           if(nombre == ""){
                swal("Asignale un nombre a esta encuesta", "", "error");
           } else {
               if (tipo_servicio == null) {
                   swal("Selecciona un tipo de servicio", "", "error");
               } else {
                   universo.TipSer = servicio_nombre;
                   if (tipo_de_busqueda == 0) {
                       swal("Selecciona un tipo de busqueda", "", "error");
                   } else if (tipo_de_busqueda == 1) {
                       universo.TipoBusqueda = "Status";
                       var contratado = '';
                       if (document.getElementById('contratado').checked) {
                           contratado = $("#contratado").val();
                       }
                       var suspendidos = '';
                       if (document.getElementById('suspendidos').checked) {
                           suspendidos = $("#suspendidos").val();
                       }
                       var cancelados = '';
                       if (document.getElementById('cancelados').checked) {
                           cancelados = $("#cancelados").val();
                       }
                       var temporales = '';
                       if (document.getElementById('temporales').checked) {
                           temporales = $("#temporales").val();
                       }
                       var instalados = '';
                       if (document.getElementById('instalados').checked) {
                           instalados = $("#instalados").val();
                       }
                       var desconectados = '';
                       if (document.getElementById('desconectados').checked) {
                           desconectados = $("#desconectados").val();
                       }
                       var fuera_servicio = '';
                       if (document.getElementById('fuera_servicio').checked) {
                           fuera_servicio = $("#fuera_servicio").val();
                       }
                       if (contratado == '' && suspendidos == '' && cancelados == '' && temporales == '' && instalados == '' && desconectados == '' && fuera_servicio == '') {
                           swal("Por favor selecciona un status del cliente", "", "error");
                       } else {
                           universo.tipo_busqueda = tipo_de_busqueda;
                           universo.contratado = contratado;
                           universo.suspendidos = suspendidos;
                           universo.temporales = temporales;
                           universo.instalados = instalados;
                           universo.desconectados = desconectados;
                           universo.fuera_servicio = fuera_servicio;
                           //envio de datos po estatus
                           $('#Espere').modal('show');
                           $.ajax({
                               url: "/ProcesoEncuesta/Create/",
                               type: "POST",
                               data:universo,
                               success: function (data, textStatus, jqXHR) {
                                   $('#Espere').modal('hide');
                                   swal({
                                       title: "!Hecho!", text: "Proceso agregado exitosamente, ahora puedes aplicar la encuesta!",
                                       type: "success",
                                       showCancelButton: false,
                                       confirmButtonColor: "#5cb85c",
                                       confirmButtonText: "Aceptar",
                                       cancelButtonText: "Aceptar",
                                       closeOnConfirm: false,
                                       closeOnCancel: false
                                   }, function (isConfirm) {
                                       location.reload();
                                   });
                               },
                               error: function (jqXHR, textStatus, errorThrown) {

                               }
                           });
                       }
                   }//tipo de busaueda 1
                   else if (tipo_de_busqueda == 2) {
                       universo.TipoBusqueda = "Rango de Fechas";
                       if (tipo_fecha == 0) {
                           swal("Por favor selecciona un status para buscar por fechas", "", "error");
                       } else {
                           fecha_inicio = $('#fecha_inicio').val();
                           fecha_final = $('#fecha_final').val();
                           if(fecha_inicio == "" || fecha_final == ""){
                               swal("Por favor selecciona las fechas del filtro", "", "error");
                           } else {
                               var inicio = new Date(fecha_inicio);
                               var fin = new Date(fecha_final);
                               var dd = inicio.getDate();
                               var mm = inicio.getMonth() + 1; //January is 0!
                               var yyyy = inicio.getFullYear();
                               var dd2 = fin.getDate();
                               var mm2 = fin.getMonth() + 1; //January is 0!
                               var yyyy2 = fin.getFullYear();

                               if (dd < 10) {
                                   dd = '0' + dd
                               }

                               if (mm < 10) {
                                   mm = '0' + mm
                               }
                               if (dd2 < 10) {
                                   dd2 = '0' + dd
                               }

                               if (mm2 < 10) {
                                   mm2 = '0' + mm2
                               }

                               fecha_inicio = dd + '-' + mm + '-' + yyyy;
                               fecha_final = dd2 + '-' + mm2 + '-' + yyyy2;
                               if (fecha_inicio == '' || fecha_final == '') {
                                   swal("Por favor selecciona el rango de fechas", "", "error");
                               } else {
                                   universo.FechaInicio = fecha_creacion;
                                   universo.fecha_final = fecha_final;
                                   universo.tipo_de_busqueda = tipo_de_busqueda;
                                   universo.fecha_inicio = fecha_inicio;
                                   universo.tipo_fecha = tipo_fecha;
                                   if(tipo_fecha == 1){
                                       universo.tipo_fecha_nombre = "Fecha de Contratación";
                                   } else if (tipo_fecha == 2) {
                                       universo.tipo_fecha_nombre = "Fecha de Instalación";
                                   } else {
                                       universo.tipo_fecha_nombre = "Fecha de Cancelación";
                                   }
                                   //enviar datos con fechas
                                   console.log(universo);
                                   $.ajax({
                                       url: "/ProcesoEncuesta/Create/",
                                       type: "POST",
                                       data: universo,
                                       success: function (data, textStatus, jqXHR) {
                                           swal({
                                               title: "!Hecho!", text: "Proceso agregado exitosamente, ahora puedes aplicar la encuesta!",
                                               type: "success",
                                               showCancelButton: false,
                                               confirmButtonColor: "#5cb85c",
                                               confirmButtonText: "Aceptar",
                                               cancelButtonText: "Aceptar",
                                               closeOnConfirm: false,
                                               closeOnCancel: false
                                           }, function (isConfirm) {
                                               location.reload();
                                           });
                                       },
                                       error: function (jqXHR, textStatus, errorThrown) {

                                       }
                                   });
                               }
                           }
                          
                       }
                   }
               }
           }
        }
        
    }
}


function Buscar() {
    var cadena = $('#texto_buscar').val();
    LlenarTabla(cadena,'');
}