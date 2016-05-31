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

});


function AbrirModal() {
    $('#Modal_Plaza').modal("show");
}

function plaza_conexion() {
    $('#Modal_Plaza').modal("hide");
    $('#conetar_plaza').hide();
    //$('#encuestaForm').show();
    $('#texto_plaxa').text('Plaza ' + $("#conexion_plaza option:selected").text());
    var id_plaza = $('#conexion_plaza').val();
    var api = '/CLIENTE/GetClientesporPlazaJson/?id=' + id_plaza + '&contrato=&cliente1=&direccion=';
    $('#panel_cliente').show();
    $('.flexdatalist').flexdatalist({
        minLength: 1,
        url: api,
        selectionRequired: true,
        valueProperty: 'CONTRATO',
        textProperty: '{NOMBRE},{CONTRATO}',
        visibleProperties: ["CONTRATO", "NOMBRE", "Calle", "NUMERO", "Colonia"],
        searchIn: ["NOMBRE", "Calle", "Colonia"],
    });
}

$('#busqueda').change(function () {
    var tipo = $('#busqueda').val();
    if (tipo == 1) {
        $('#por_nombre').show();
        $('#por_contrato').hide();
        $('#datos_contacto').hide();
        $('#datos_contacto_error').hide();
        $('#contrato_id').val('');
    } else if (tipo == 2) {
        $('#por_contrato').show();
        $('#por_nombre').hide();
        $('.flexdatalist').text('');
    }
});

function validar_contrato() {
    var contrato = $('#contrato_id').val();
    if (contrato == "") {
        $('#datos_contacto_error').show();
        $('#datos_contacto').hide();
    } else {
        var id_plaza = $('#conexion_plaza').val();
        $.ajax({
            url: "/CLIENTE/GetClientesporPlazaJson/",
            type: "GET",
            data: { 'id': id_plaza, "contrato": contrato, "cliente1": "", "direccion": "" },
            success: function (data, textStatus, jqXHR) {
                console.log();
                if (data.length == 0) {
                    $('#datos_contacto_error').show();
                    $('#datos_contacto').hide();
                } else {
                    $('#nombre_contrato').text(data[0].NOMBRE);
                    $('#calle_contrato').text('Calle: ' + data[0].Calle);
                    $('#colonia_contrato').text('Colonia: ' + data[0].Colonia);
                    $('#datos_contacto').show();
                    $('#datos_contacto_error').hide();
                }

            },
            error: function (data, jqXHR, textStatus) {
                console.log(data);
            }
        });
    }

}

function enviarEncuesta() {
    var cliente = $('#cliente_id').val();
    var contrato_id = $('#contrato_id').val();
    var no_contrato = 0;
    var tipo_busqueda = $('#busqueda').val();
    if (tipo_busqueda == 1) {
        no_contrato = cliente;
    } else if (tipo_busqueda == 2) {
        no_contrato = contrato_id;
    }
    var plaza = $('#conexion_plaza').val();
    if (no_contrato == 0) {
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
        datos.cliente = parseInt(no_contrato);
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