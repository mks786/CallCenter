
$(document).ready(function () {    
    $.ajax({
        url: "/CIUDAD/getAllCiudades/",
        type: "GET",
        success: function (data, textStatus, jqXHR) {
            for (var i = 0; i < data.length; i++) {
                $('#plaza_llamadas').append($('<option>', {
                    value: data[i].IdPlaza,
                    text: data[i].Ciudad
                }));
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
    $('#telefono_editar').inputmask("(999)9999999");
    $('#celular_editar').inputmask("999-999-99-99");
    var url;
    $("#plaza_llamadas").change(function () {
        $("#panel_busqueda").show();
        $("#tipo_llamada").removeAttr('disabled');
        var ciudad = $("#plaza_llamadas option:selected").text();
        LlenarTabla($(this).val(), '', '', '', '', ciudad);
        var id_plaza = $(this).val();
        $.ajax({
            url: "/Conexion/listaPlazas/",
            type: "GET",
            data: { "idPlaza": id_plaza },
            success: function (data, textStatus, jqXHR) {
                var ciudad_select = $("#plaza_llamadas option:selected").text();
                $('#nombre_plaza').text("CIUDAD DE " + ciudad_select.toUpperCase() + ", SERVIDOR " + data.Plaza.toUpperCase());
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });
        
    });
    $("#tipo_llamada").change(function () {
        $('#nueva_llamada_link').removeClass('disabled');
        var id_plaza = $("#plaza_llamadas").val();
        var tipo = $(this).val();
        var ciudad = $("#plaza_llamadas option:selected").text();
        var cliente;
        if (tipo == 1) {
            cliente = true;
        } else if (tipo == 2) {
            cliente = false;
        } else {
            cliente = '';
        }
        LlenarTabla(id_plaza, '', '', '', cliente,ciudad);
    });
});

function buscar_por_id() {
    $('.collapse').collapse('hide');
    var id_llamada = $('#input_llamada').val();
    var id_plaza = $("#plaza_llamadas").val();
    if (id_llamada == "") {
        swal("El No. de contrato no puede ir en blanco ", "", "danger");
    } else {
        var ciudad = $("#plaza_llamadas option:selected").text();
        LlenarTabla(id_plaza, '', '', id_llamada, '',ciudad);
    }
}

function buscar_por_nombre() {
    $('.collapse').collapse('hide');
    var nombre = $('#nombre_individual').val();
    var id_plaza = $("#plaza_llamadas").val();
    if (nombre == "") {
        swal("El nombre no puede ir en blanco ", "", "danger");
    } else {
        var ciudad = $("#plaza_llamadas option:selected").text();
        LlenarTabla(id_plaza, '', nombre, '', '',ciudad);
    }
}

function buscar_por_contrato() {
    $('.collapse').collapse('hide');
    var contrato = $('#input_contrato').val();
    var id_plaza = $("#plaza_llamadas").val();
    if (contrato == "") {
        swal("El No. de contrato no puede ir en blanco ", "", "danger");
    } else {
        var ciudad = $("#plaza_llamadas option:selected").text();
        LlenarTabla(id_plaza, contrato, '', '', '',ciudad);
    }
}

function LlenarTabla(idplaza,contrato,cadena,id_llamada,tipo_llamada,ciudad) {

    $('#TablaLlamadas tbody > tr').remove();
    $('#TablaLlamadas').dataTable({
        "processing": true,
        "serverSide": true,
        "bFilter": false,
        "dom": '<"toolbar">frtip',
        "bDestroy": true,
        "info": true,
        "stateSave": false,
        "lengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]],
        "ajax": {
            "url": "/DatosLlamada/GetList/",
            "type": "POST",
            "data": { 'idplaza': idplaza, 'cadena': cadena, 'id_llamada': id_llamada, 'tipo_llamada': tipo_llamada, 'contrato': contrato, 'ciudad':ciudad },
        },
        "fnInitComplete": function (oSettings, json) {


        },
        "fnDrawCallback": function (oSettings) {

        },


        "columns": [
            { "data": "IdLlamada", "orderable": false },
            { "data": "Usuario", "orderable": false },
            {
                sortable: false,
                "render": function (data, type, full, meta) {
                    var cliente;
                    if (full.Contrato == null) {
                        cliente = 'NO';
                    } else {
                        cliente = 'SI';
                    }
                    return ("<td>"+cliente+"<td>");  //Es el campo de opciones de la tabla.
                }
            },
            {
                sortable: false,
                "render": function (data, type, full, meta) {
                    if(full.Contrato == null){
                        return "----";
                    } else {
                        return full.Contrato;
                    }
                }
            },
            { "data": "Nombre", "orderable": false },
            {
                sortable: false,
                "render": function (data, type, full, meta) {
                    var fecha = full.Fecha;
                    fecha = fecha.split(" ", 1); 
                    
                    return ("<td>"+fecha+"<td>");  //Es el campo de opciones de la tabla.
                }
            },
            {
                sortable: false,
                "render": function (data, type, full, meta) {
                    return Opciones(full.IdLlamada);  //Es el campo de opciones de la tabla.
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

    })

    

}

//funcion:retorna las opciones que tendra cada row en la tabla principal
function Opciones(id) {
    var opc;
    if (permiso_editar == "False") {
        //opc = "<button class='btn btn-info btn-xs Detalle' type='button' id='" + id + "' onclick='MostrarDetalles(this)'><i class='fa fa-info' aria-hidden='true'></i> Detalles</button>";
        opc = "<button class='btn btn-info btn-xs Editar' type='button' id='" + id + "' onclick='editarLlamada(this)'><i class='fa fa-info' aria-hidden='true'></i> Detalle</button>";
    } else {
        opc = "<button class='btn btn-info btn-xs Editar' type='button' id='" + id + "' onclick='editarLlamada(this)'><i class='fa fa-info' aria-hidden='true'></i> Detalle</button>";
        // opc = "<button class='btn btn-info btn-xs Detalle' type='button' id='" + id + "' onclick='MostrarDetalles(this)'><i class='fa fa-info' aria-hidden='true'></i> Detalles</button> <button class='btn btn-warning btn-xs Editar' type='button' id='" + id + "' onclick='editarLlamada(this)'><i class='fa fa-pencil' aria-hidden='true'></i> Editar</button>";
    }
    return opc;
}

$('#TablaLlamadas').on('click', '.Detalle', function () {
    $('#ModalDetalleLlamada').modal('show');
});

$('#TablaLlamadas').on('click', '.Editar', function () {
    $('#tab_info').attr('aria-expanded', 'true');
    $('#li_info').addClass('active');
    $('#tab_1').addClass('active');
    $('#tab_2').removeClass('active');
    $('#li_queja').removeClass('active');
    $('#tab_queja').attr('aria-expanded', 'false');
    $('#ModalEditarLlamada').modal('show');
});

$('#TablaLlamadas').on('click', '.Eliminar', function () {
    alert("click");
});


function MostrarDetalles(e){
    var llamada = e.getAttribute('id');
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/Llamada/getOneCall/",
        type: "GET",
        data: { 'plaza': id_plaza, 'id_llamada': llamada },
        success: function (data, textStatus, jqXHR) {
            $('#id_llamda').text(data[0].IdLlamada); 
            $('#atendio_detalle').text(data[0].atendio);
            var contrato = data[0].Contrato;
            var queja = data[0].Clv_Queja;
            var problemaSolucion = data[0].ProblemaSolucion;
            if (problemaSolucion == true) {
                $('#pan_solucion').show();
            } else {
                $('#pan_solucion').hide();
            }
            if (queja < 1) {
                $('#pan_queja').hide();
            } else {
                $('#queja').text(queja);
                $('#pan_queja').show();
            }
            if (contrato == "") {
                $('#pan_contrato').hide();
                $('#pan_detalle').hide();
                $('#pan_nombre').show();
                $('#pan_domicilio').show();
                $('#pan_telefono').show();
                $('#pan_celular').show();
                $('#pan_email').show();
                $('#nombre_completo').text(data[0].nombre);
                $('#domicilio').text(data[0].domicilio);
                $('#telefono').text(data[0].telefono);
                $('#celular').text(data[0].celular);
                $('#email').text(data[0].email);
            } else {
                $('#pan_contrato').show();
                $('#pan_detalle').show();
                $('#pan_nombre').show();
                $('#pan_domicilio').hide();
                $('#pan_telefono').hide();
                $('#pan_celular').hide();
                $('#pan_email').hide();
            }
            $('#contrato_detalle').text(contrato);
            var fecha = data[0].Fecha; 
            var inicio = data[0].HoraInicio;
            var fin = data[0].HoraFin;
            $('#fecha_llamada').text(data[0].Fecha);
            $('#hora_inicio').text(inicio);
            $('#hora_fin').text(fin);
            $('#detalle').text(data[0].Detalle);
            $('#solucion').text(data[0].Solucion);
            $('#nombre_completo').text(data[0].nombre);
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}


function editarLlamada(e) {
    $('#cargando').show();
    $('#panel_tabs_llamadas').hide();
    var llamada = e.getAttribute('id');
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/Llamada/getOneCall/",
        type: "GET",
        data: { 'plaza': id_plaza, 'id_llamada': llamada },
        success: function (data, textStatus, jqXHR) {
            var contrato = data[0].Contrato;
            $('#detalle_editar').val(data[0].Detalle);
            $('#solucion_editar').val(data[0].Solucion);
            $('#contrato_editar').val(data[0].Contrato);
            $('#id_llamada').val(data[0].IdLlamada);
            $('#inicio').val(data[0].HoraInicio);
            $('#atendio').val(data[0].atendio);
            $('#fin').val(data[0].HoraFin);
            $('#fecha').val(data[0].Fecha); 
            $('#id_llamada').val(llamada);
            var queja = data[0].Clv_Queja;
            var informacion = data[0].Clv_Motivo;
            var cliente = data[0].SiEsCliente;
            if(cliente == true){
                if (queja > 1) {
                    $('#contrato_oculto').val(contrato);
                    $('#tipo_llamada_oculto').val(2);
                    $('#contrato_panel_editar').show();
                    $('#detalle_texto').text('Reporte del cliente ');
                    $('#panel_detalle_llamada').show(); 
                    $('#li_queja').show();
                    $('#id_queja').val(queja);
                    getDatosQueja(queja);
                    $('#queja_oculto').val(queja);
                    getTurno(data[0].IdTurno);
                    getClasificacionProblemas(data[0].Clv_Problema);
                    $('#panel_solucion').hide();
                    $('#clasificacion_editar').show(); 
                    $('#clasificacion_solucion_editar').hide();
                    $('#celular_panel_editar').hide();
                    $('#nombre_panel_editar').hide();
                    $('#telefono_panel_editar').hide();
                    $('#domicilio_panel_editar').hide();
                    $('#email_panel_editar').hide(); 
                    $('#panel_tipo_informacion').hide();
                } else if (informacion != null) {
                    $('#contrato_oculto').val(contrato);
                    $('#tipo_llamada_oculto').val(1);
                    $('#queja_oculto').val("");
                    $.ajax({
                        url: "/Llamada/TipoLlamada/",
                        type: "GET",
                        success: function (datas, textStatus, jqXHR) {
                            $('#tipo_informacion_editar option').remove();
                            for (var i = 0; i < datas.length; i++) {
                                if (datas[i].clv_motivo == data[0].Clv_Motivo) {
                                    $('#tipo_informacion_editar').append('<option value="' + datas[i].clv_motivo + '" selected>' + datas[i].descricpion + '</option>');
                                } else {
                                    $('#tipo_informacion_editar').append('<option value="' + datas[i].clv_motivo + '">' + datas[i].descricpion + '</option>');
                                }
                            }
                        },
                        error: function (jqXHR, textStatus, errorThrown) {

                        }
                    });
                    $('#li_queja').hide();
                    $('#contrato_panel_editar').show();
                    $('#detalle_texto').text('Detalle Llamada');
                    $('#panel_detalle_llamada').show();
                    $('#panel_tipo_informacion').hide(); 
                    $('#clasificacion_editar').hide();
                    $('#panel_solucion').hide();
                    $('#clasificacion_solucion_editar').hide(); 
                    $('#panel_tipo_informacion').show();
                    $('#celular_panel_editar').hide();
                    $('#nombre_panel_editar').hide();
                    $('#telefono_panel_editar').hide();
                    $('#domicilio_panel_editar').hide();
                    $('#email_panel_editar').hide();
                } else {
                    $('#contrato_oculto').val(contrato);
                    $('#queja_oculto').val("");
                    $('#tipo_llamada_oculto').val(2);
                    getClasificacionSolucion(data[0].Clv_TipSer, data[0].Clv_Trabajo);
                    getClasificacionProblemas(data[0].Clv_Problema);
                    $('#li_queja').hide();
                    $('#contrato_panel_editar').show();
                    $('#detalle_texto').text('Reporte del cliente ');
                    $('#panel_detalle_llamada').show(); 
                    $('#panel_tipo_informacion').hide();
                    $('#clasificacion_editar').show();
                    $('#panel_solucion').show();
                    $('#clasificacion_solucion_editar').show();
                    $('#celular_panel_editar').hide();
                    $('#nombre_panel_editar').hide();
                    $('#telefono_panel_editar').hide();
                    $('#domicilio_panel_editar').hide();
                    $('#email_panel_editar').hide();
                }
            } else {
                $('#queja_oculto').val("");
                $('#contrato_oculto').val("");
                $('#tipo_llamada_oculto').val(1);
                $.ajax({
                    url: "/Llamada/TipoLlamada/",
                    type: "GET",
                    success: function (datas, textStatus, jqXHR) {
                        $('#tipo_informacion_editar option').remove();
                        for (var i = 0; i < datas.length; i++) {
                            if (datas[i].clv_motivo == data[0].Clv_Motivo) {
                                $('#tipo_informacion_editar').append('<option value="' + datas[i].clv_motivo + '" selected>' + datas[i].descricpion + '</option>');
                            } else {
                                $('#tipo_informacion_editar').append('<option value="' + datas[i].clv_motivo + '">' + datas[i].descricpion + '</option>');
                            }
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {

                    }
                });
                $('#li_queja').hide();
                $('#contrato_panel_editar').hide();
                $('#detalle_texto').text('Detalle Llamada');
                $('#panel_detalle_llamada').show();
                $('#panel_tipo_informacion').hide();
                $('#clasificacion_editar').hide();
                $('#panel_solucion').hide();
                $('#clasificacion_solucion_editar').hide();
                $('#panel_tipo_informacion').show();
                $('#telefono_editar').inputmask('unmaskedvalue');
                $('#celular_editar').inputmask('unmaskedvalue');
                $('#nombre_editar').val(data[0].nombre);
                $('#domicilio_editar').val(data[0].domicilio);
                $('#telefono_editar').val(data[0].telefono);
                $('#celular_editar').val(data[0].celular);
                $('#email_editar').val(data[0].email);
                $('#celular_panel_editar').show();
                $('#nombre_panel_editar').show();
                $('#telefono_panel_editar').show();
                $('#domicilio_panel_editar').show();
                $('#email_panel_editar').show();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
    setTimeout("mostrarDatos()", 1800);
}

function getMotivoLlamada(id) {
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/Llamada/GetMotivoLlamada/",
        type: "GET",
        data: { 'IdPlaza': id_plaza},
        success: function (data, textStatus, jqXHR) {
            $("#selec_motivo_llamada option").remove();
            for (var i = 0; i < data.length; i++) {
                if (data[i].Clv_Motivo == id) {
                    $('#selec_motivo_llamada').append('<option value="' + data[i].Clv_Motivo + '" selected>' + data[i].Descripcion + '</option>');
                } else {
                    $('#selec_motivo_llamada').append('<option value="' + data[i].Clv_Motivo + '">' + data[i].Descripcion + '</option>');
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

function mostrarDatos() {
    $('#cargando').hide();
    $('#panel_tabs_llamadas').show();
}

function getClasificacionSolucion(id, select) {
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/tblClasificacionProblema/GetClasficacionSolucion/",
        type: "GET",
        data: { 'IdPlaza': id_plaza, 'idServicio': id },
        success: function (data, textStatus, jqXHR) {
            $("#select_solucion option").remove();
            for (var i = 0; i < data.length; i++) {
                if (data[i].CLV_TRABAJO == select) {
                    $('#select_solucion').append('<option value="' + data[i].CLV_TRABAJO + '" selected>' + data[i].DESCRIPCION + '</option>');
                } else {
                    $('#select_solucion').append('<option value="' + data[i].CLV_TRABAJO + '">' + data[i].DESCRIPCION + '</option>');
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}
function getClasificacionProblemas(select) {
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/tblClasificacionProblema/GetClasficacionProblemas/",
        type: "GET",
        data: { 'IdPlaza': id_plaza },
        success: function (data, textStatus, jqXHR) {
            $("#select_problemas option").remove();
            for (var i = 0; i < data.length; i++) {
                if (data[i].ClvProblema == select) {
                    $('#select_problemas').append('<option value="' + data[i].ClvProblema + '" selected>' + data[i].Descripcion + '</option>');
                } else {
                    $('#select_problemas').append('<option value="' + data[i].ClvProblema + '">' + data[i].Descripcion + '</option>');
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

function getClasificacion(id, idServicio) {
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/tblClasificacionProblema/GetClasficacionSolucion/",
        type: "GET",
        data: { 'IdPlaza': id_plaza,'idServicio':idServicio  },
        success: function (data, textStatus, jqXHR) {
            for (var i = 0; i < data.length; i++) {
                $("#select_soluciones option").remove();
                if (data[i].CLV_TRABAJO == id) {
                    $('#select_solucion').append('<option value="' + data[i].CLV_TRABAJO + '" selected>' + data[i].DESCRIPCION + '</option>');
                } else {
                    $('#select_solucion').append('<option value="' + data[i].CLV_TRABAJO + '">' + data[i].DESCRIPCION + '</option>');
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

function getDatosQueja(id) {
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/Llamada/getDatosQueja/",
        type: "GET",
        data: { 'plaza': id_plaza, 'queja': id },
        success: function (data, textStatus, jqXHR) {
            getPrioridad(data[0].clvPrioridadQueja);
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}


function getPrioridad(id) {
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/tblClasificacionProblema/GetPrioridad/",
        type: "GET",
        data: { 'IdPlaza': id_plaza },
        success: function (data, textStatus, jqXHR) {
            $("#prioridad_editar option").remove();
            for (var i = 0; i < data.length; i++) {
                if (data[i].clvPrioridadQueja == id) {
                    $('#prioridad_editar').append('<option value="' + data[i].clvPrioridadQueja + '" selected>' + data[i].Descripcion + '</option>');
                } else {
                    $('#prioridad_editar').append('<option value="' + data[i].clvPrioridadQueja + '">' + data[i].Descripcion + '</option>');
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

function getTurno(id) {
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/tblClasificacionProblema/GetTurno/",
        type: "GET",
        data: { 'IdPlaza': id_plaza },
        success: function (data, textStatus, jqXHR) {
            $("#turno_editar option").remove();
            for (var i = 0; i < data.length; i++) {
                if (data[i].IdTurno == id) {
                    $('#turno_editar').append('<option value="' + data[i].IdTurno + '" selected>' + data[i].Turno + '</option>');
                } else {
                    $('#turno_editar').append('<option value="' + data[i].IdTurno + '">' + data[i].Turno + '</option>');
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}


function guardarLlamada(e) {
    var id_plaza = $("#plaza_llamadas").val();
    var llamada = {};
    var contrato = $('#contrato_oculto').val();
    var queja = $('#queja_oculto').val();
    var tipo_llamada = $('#tipo_llamada_oculto').val();
    llamada.tipo_llamada = tipo_llamada;
    llamada.id_llamada = $('#id_llamada').val();
    if(tipo_llamada == 1){
        if(contrato != ""){
            llamada.contrato = contrato;
            llamada.clv_motivo = $('#tipo_informacion_editar').val();
            llamada.detalle = $('#detalle_editar').val();
        }else{
            llamada.clv_motivo = $('#tipo_informacion_editar').val();
            llamada.detalle = $('#detalle_editar').val();
            llamada.domicilio = $('#domicilio_editar').val();
            llamada.nombre = $('#nombre_editar').val();
            llamada.telefono = $('#telefono_editar').inputmask('unmaskedvalue');
            llamada.celular = $('#celular_editar').inputmask('unmaskedvalue');
            llamada.email = $('#email_editar').val();
        }
    } else {
        if(queja > 0){
            llamada.contrato = contrato;
            llamada.detalle = $('#detalle_editar').val();
            llamada.clas_problema = $('#select_problemas').val();
            llamada.queja = queja;
            llamada.prioridad = $('#prioridad_editar').val();
            llamada.turno = $('#turno_editar').val();
        } else {
            llamada.contrato = contrato;
            llamada.detalle = $('#detalle_editar').val();
            llamada.clas_problema = $('#select_problemas').val();
            llamada.solucion = $('#solucion_editar').val();
            llamada.clas_solucion = $('#select_solucion').val();
        }        
    }
    $.ajax({
        url: "/Llamada/editarLLamada/",
        type: "POST",
        data: { 'plaza': id_plaza, 'llamada': llamada },
        success: function (data, textStatus, jqXHR) {
            $('#ModalEditarLlamada').modal("hide");
            LlenarTabla(id_plaza, '', '', data, '');
            swal("La llamada se guaró exitosamente", "", "success");
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}