var tipo_de_busqueda = 0;
var tipo_fecha = 0;
$(function () {

    $.ajax({
        url: "/Conexion/ListaConexiones/",
        type: "GET",
        success: function (data, textStatus, jqXHR) {
            for (var i = 0; i < data.length; i++) {
                $('#conexion_plaza').append($('<option>', {
                    value: data[i].IdConexion,
                    text: data[i].Plaza
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
});



function AbrirModal() {
    $('#Modal_Plaza').modal("show");
}

function plaza_conexion() {
    $('#Modal_Plaza').modal("hide");
    $('#conetar_plaza').hide();
    //$('#encuestaForm').show();
    $('#texto_plaza').text('Plaza ' + $("#conexion_plaza option:selected").text());
    $('#panel_busqueda').show();

    var id_plaza = $('#conexion_plaza').val();
    $.ajax({
        url: "/CLIENTE/getTipoServicio/",
        type: "GET",
        data: { "IdPlaza": id_plaza },
        success: function (data, textStatus, jqXHR) {
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
}


$('#busqueda_masiva').on('click', function () {
    var id_plaza = $('#conexion_plaza').val();
    var tipo_de_servicio = $('#tipo_servicio').val();
    var fecha_inicio = '';
    var fecha_final = '';
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

    if (tipo_de_busqueda == 0) {
        swal("Por favor selecciona el tipo de busqueda", "", "error");
    } else if (tipo_de_busqueda == 1) {
        if (contratado == '' && suspendidos == '' && cancelados == '' && temporales == '' && instalados == '' && desconectados == '' && fuera_servicio == '') {
            swal("Por favor selecciona un status del cliente", "", "error");
        } else {
            TablaStatus(id_plaza, tipo_de_servicio, tipo_de_busqueda, contratado, suspendidos, cancelados, temporales, instalados, desconectados, fuera_servicio);
        }
    } else {

        if (tipo_fecha == 0) {
            swal("Por favor selecciona un status para buscar por fechas", "", "error");
        } else {
            fecha_inicio = $('#fecha_inicio').val();
            fecha_final = $('#fecha_final').val();
            if (fecha_inicio == '' || fecha_final == '') {
                swal("Por favor selecciona el rango de fechas", "", "error");
            } else {
                TablaFecha(id_plaza, tipo_de_servicio, tipo_de_busqueda, tipo_fecha, fecha_inicio, fecha_final);
            }
        }
    }

});

$('#buscar_por_nombre').on('click', function () {
    var id_plaza = $('#conexion_plaza').val();
    var nombre = $('#nombre_individual').val();
    console.log(nombre);
    $('#panel_masivo').hide();
    $('#panel_individual').show();
    $.ajax({
        url: "/CLIENTE/GetClientesPRUEBA/",
        type: "GET",
        data: { 'IdPlaza': id_plaza, "contrato": "", "Nombrecliente": nombre, "direccion": "" },
        success: function (data, textStatus, jqXHR) {
            console.log(data);
            if (data.length == 0) {
                $('#panel_individual').hide();
                $('#invalido').show();
            } else {
                $('#invalido').hide();
                $('#TablaIndividual tbody > tr').remove();
                for (var i = 0; i < data.length; i++) {
                    $('#TablaIndividual tbody').append('<tr><td>' + data[i].CONTRATO + '</td><td>' + data[i].NOMBRE + '</td><td>' + data[i].Ciudad + '</td><td>' + data[i].Colonia + '</td><td>' + data[i].Calle + '</td><td>' + data[i].NUMERO + '</td><td><button class="btn btn-success" onclick="aplicando_encuesta(this)" data-contrato="' + data[i].CONTRATO + '" data-name="' + data[i].NOMBRE + '" data-ciudad="' + data[i].Ciudad + '" data-colonia="' + data[i].Colonia + '" data-calle="' + data[i].Calle + '" data-numero="' + data[i].NUMERO + '" data-telefono="' + data[i].TELEFONO + '"><i class="fa fa-pie-chart" aria-hidden="true"></i>Aplicar</button></td></tr>');

                }
            }
        },
        error: function (data, jqXHR, textStatus) {
            console.log(data);
        }

    });
});

$('#buscar_por_direccion').on('click', function () {
    var id_plaza = $('#conexion_plaza').val();
    var calle = $('#calle').val();
    var ciudad = $('#ciudad').val();
    var numero = $('#numero_domicilio').val();
    var colonia = $('#colonia_domicilio').val();
    $('#panel_masivo').hide();
    $('#panel_individual').show();
    $.ajax({
        url: "/CLIENTE/GetClientesPRUEBA/",
        type: "GET",
        data: { 'IdPlaza': id_plaza, "contrato": "", "Nombrecliente": "", "calle": calle, "colonia": colonia, "ciudad": ciudad, "numero": numero },
        success: function (data, textStatus, jqXHR) {
            console.log(data);
            if (data.length == 0) {
                $('#panel_individual').hide();
                $('#invalido').show();
            } else {
                $('#invalido').hide();
                $('#TablaIndividual tbody > tr').remove();
                for (var i = 0; i < data.length; i++) {
                    $('#TablaIndividual tbody').append('<tr><td>' + data[i].CONTRATO + '</td><td>' + data[i].NOMBRE + '</td><td>' + data[i].Ciudad + '</td><td>' + data[i].Colonia + '</td><td>' + data[i].Calle + '</td><td>' + data[i].NUMERO + '</td><td><button class="btn btn-success" onclick="aplicando_encuesta(this)" data-contrato="' + data[i].CONTRATO + '" data-name="' + data[i].NOMBRE + '" data-ciudad="' + data[i].Ciudad + '" data-colonia="' + data[i].Colonia + '" data-calle="' + data[i].Calle + '" data-numero="' + data[i].NUMERO + '" data-telefono="' + data[i].TELEFONO + '"><i class="fa fa-pie-chart" aria-hidden="true"></i>Aplicar</button></td></tr>');

                }
            }
        },
        error: function (data, jqXHR, textStatus) {
            console.log(data);
        }

    });
});

$('#buscar_por_contrato').on('click', function () {
    var id_plaza = $('#conexion_plaza').val();
    var contrato = $('#input_contrato').val();
    $('#panel_masivo').hide();
    $('#panel_individual').show();
    $.ajax({
        url: "/CLIENTE/GetClientesPRUEBA/",
        type: "GET",
        data: { 'IdPlaza': id_plaza, "contrato": contrato, "cliente1": "", "direccion": "" },
        success: function (data, textStatus, jqXHR) {
            console.log(data);
            if (data.length == 0) {
                $('#panel_individual').hide();
                $('#invalido').show();
            } else {
                $('#invalido').hide();
                $('#TablaIndividual tbody > tr').remove();
                $('#TablaIndividual tbody').append('<tr><td>' + data[0].CONTRATO + '</td><td>' + data[0].NOMBRE + '</td><td>' + data[0].Ciudad + '</td><td>' + data[0].Colonia + '</td><td>' + data[0].Calle + '</td><td>' + data[0].NUMERO + '</td><td><button class="btn btn-success" onclick="aplicando_encuesta(this)" data-contrato="' + data[0].CONTRATO + '" data-name="' + data[0].NOMBRE + '" data-ciudad="' + data[0].Ciudad + '" data-colonia="' + data[0].Colonia + '" data-calle="' + data[0].Calle + '" data-numero="' + data[0].NUMERO + '" data-telefono="' + data[0].TELEFONO + '"><i class="fa fa-pie-chart" aria-hidden="true"></i>Aplicar</button></td></tr>');
            }
        },
        error: function (data, jqXHR, textStatus) {
            console.log(data);
        }

    });
});

function aplicando_encuesta(e) {
    $('#nombre_cliente_modal').text(e.getAttribute("data-name"));
    $('#ciudad_modal').text("Ciudad: " + e.getAttribute("data-ciudad"));
    $('#colonia_modal').text("Colonia: " + e.getAttribute("data-colonia"));
    $('#calle_modal').text("Calle: " + e.getAttribute("data-calle"));
    $('#numero_modal').text("Número: " + e.getAttribute("data-numero"));
    $('#telefono_modal').text("Teléfono: " + e.getAttribute("data-telefono"));
    $('#cliente_id').val(e.getAttribute("data-contrato"))
    $('#modal_aplicar').modal('show');
}

function enviarEncuesta() {
    var contrato_id = $('#cliente_id').val();
    var plaza = $('#conexion_plaza').val();
    if (contrato_id == 0) {
        swal("Agrega un cliente válido", "", "error");
    } else {
        var html = $('#encuestaForm');
        var datos = {};
        var count = 0;
        datos.id_encuesta = new Array();
        datos.cliente = new Array();
        datos.id_plaza = new Array();
        datos.pregunta = new Array();
        datos.respuestas = new Array();
        var cliente = [];
        var datos_preguntas = [];
        var datos_respuestas = [];
        var data = $('#encuestaForm').serializeArray().reduce(function (obj, item, indice) {
            obj[item.name] = item.value;
            if (parseInt(item.name)) {
                var id_pregunta = parseInt(item.name);
                var respuestas = {};
                if (item.value != "") {
                    count += 1;
                    respuestas.id_pregunta = id_pregunta;
                    respuestas.respuesta = item.value;
                    datos_respuestas.push(respuestas);
                }

            } else {
                if (item.name != "id_encuesta") {
                    var preguntas = {};
                    var tipo = item.name.split('s')[0].replace('(', '').replace(')', '').replace('p', '');
                    var idPregunta = item.name.split('s')[1].replace('(', '').replace(')', '');
                    preguntas.id_pregunta = parseInt(idPregunta);
                    preguntas.tipo = parseInt(tipo);
                    preguntas.nombre = item.value;
                    datos_preguntas.push(preguntas);

                }
            }
            return obj;
        }, {});
        datos.id_plaza = parseInt(plaza);
        datos.cliente = parseInt(contrato_id);
        datos.id_encuesta = parseInt($('#encuesta_id').val());
        for (var i = 0; i < datos_preguntas.length; i++) {
            var pregunta = {};
            pregunta.id_pregunta = datos_preguntas[i].id_pregunta;
            pregunta.nombre = datos_preguntas[i].nombre;
            pregunta.tipoPregunta = datos_preguntas[i].tipo;
            datos.pregunta.push(pregunta);
            for (var j = 0; j < datos_respuestas.length; j++) {
                if (datos_preguntas[i].id_pregunta == datos_respuestas[j].id_pregunta) {
                    var respuestas = {};
                    if (datos_respuestas[j].id_respuesta) {
                        respuestas.id_respuesta = datos_respuestas[j].id_respuesta;
                    }
                    respuestas.id_pregunta = datos_respuestas[j].id_pregunta;
                    respuestas.respuesta = datos_respuestas[j].respuesta;
                    datos.respuestas.push(respuestas);
                }
            }
        }
        if (count < contador_preguntas) {
            swal("Por favor contesta todas las preguntas", "", "error");
        } else {
            $.ajax({
                url: "/Encuesta/DatosEncuesta/",
                type: "POST",
                data: { 'encuesta': datos },
                success: function (data, textStatus, jqXHR) {
                    document.getElementById("encuestaForm").reset();
                    $('#por_nombre').hide();
                    $('#por_contrato').hide();
                    $('#datos_contacto').hide();
                    $('#datos_contacto_error').hide();
                    $('#modal_aplicar').modal('hide');
                    swal("La Encuesta se guaró exitosamente", "", "success");

                },
                error: function (jqXHR, textStatus, errorThrown) {
                    document.getElementById("encuestaForm").reset();
                    $('#por_nombre').hide();
                    $('#por_contrato').hide();
                    $('#datos_contacto').hide();
                    swal("Error al envair la encuesta", "", "error");
                }
            });
        }

    }

}



function TablaStatus(idplaza, tiposervicio, tipobusqueda, contratado, suspendidos, cancelados, temporales, instalados, desconectados, fuera_servicio) {

    $('#panel_individual').hide();
    $('#TablaMasiva tbody').lenght = 0;
    $('#panel_masivo').show();
    $('#TablaMasiva').dataTable({
        "processing": true,
        "serverSide": true,
        "bFilter": false,
        "dom": '<"toolbar">frtip',
        "bDestroy": true,
        "info": true,
        "stateSave": true,
        "lengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]],
        "ajax": {
            "url": "/CLIENTE/FiltradoMasivo/",
            "type": "POST",
            "data": { 'idplaza': idplaza, 'idtipser': tiposervicio, 'tipobusqueda': tipobusqueda, 'contratado': contratado, 'suspendidos': suspendidos, 'cancelados': cancelados, 'temporales': temporales, 'instalados': instalados, 'desconectados': desconectados, 'fuera_servicio': fuera_servicio, 'fecha': '', 'finicio': '', 'ftermino': '' },
        },
        "columns": [
            { "data": "CONTRATO", "orderable": false },
            { "data": "NOMBRE", "orderable": false },
            { "data": "Ciudad", "orderable": false },
            { "data": "Colonia", "orderable": false },
            { "data": "Calle", "orderable": false },
            { "data": "NUMERO", "orderable": false },
            {
                sortable: false, "render": function (data, type, full, meta) {
                    return "<button class='btn btn-success' onclick='aplicando_encuesta(this)' data-contrato='" + full.CONTRATO + "' data-name='" + full.NOMBRE + "' data-ciudad='" + full.Ciudad + "' data-colonia='" + full.Colonia + "' data-calle='" + full.Calle + "' data-numero='" + full.NUMERO + "' data-telefono='" + full.TELEFONO + "'><i class='fa fa-pie-chart' aria-hidden='true'></i> Aplicar</button>";
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



function TablaFecha(idplaza, tiposervicio, tipobusqueda, fecha, inicial, final) {

    $('#panel_individual').hide();
    $('#TablaMasiva tbody').lenght = 0;
    $('#panel_masivo').show();
    $('#TablaMasiva').dataTable({
        "processing": true,
        "serverSide": true,
        "bFilter": false,
        "dom": '<"toolbar">frtip',
        "bDestroy": true,
        "info": true,
        "stateSave": true,
        "lengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]],
        "ajax": {
            "url": "/CLIENTE/FiltradoMasivo/",
            "type": "GET",
            "data": { 'idplaza': idplaza, 'idtipser': tiposervicio, 'tipobusqueda': tipobusqueda, 'contratado': '', 'suspendidos': '', 'cancelados': '', 'temporales': '', 'instalados': '', 'desconectados': '', 'fuera_servicio': '', 'fecha': fecha, 'finicio': inicial, 'ftermino': final },
        },
        "columns": [
            { "data": "CONTRATO", "orderable": false },
            { "data": "NOMBRE", "orderable": false },
            { "data": "Ciudad", "orderable": false },
            { "data": "Colonia", "orderable": false },
            { "data": "Calle", "orderable": false },
            { "data": "NUMERO", "orderable": false },
            {
                sortable: false, "render": function (data, type, full, meta) {
                    console.log(full);
                    return "<button class='btn btn-success' onclick='aplicando_encuesta(this)' data-contrato='" + full.CONTRATO + "' data-name='" + full.NOMBRE + "' data-ciudad='" + full.Ciudad + "' data-colonia='" + full.Colonia + "' data-calle='" + full.Calle + "' data-numero='" + full.NUMERO + "' data-telefono='" + full.TELEFONO + "'><i class='fa fa-pie-chart' aria-hidden='true'></i>Aplicar</button>";
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
