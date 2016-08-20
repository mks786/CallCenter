var id_proceso_encuesta;
$(document).ready(function () {
    LlenarTabla();
    $('#filtro_select').on('change', function () {
        var tipo = $(this).val();
        if(tipo == 1){
            LlenarTabla('',1);
        } else if(tipo == 2) {
            LlenarTabla('', 2);
        } else {
            LlenarTabla('', '');
        }
    });
});
function LlenarTabla(cadena,filtro) {
    $('#TablaUniversoClientes tbody > tr').remove();
    $('#TablaUniversoClientes').dataTable({
        "processing": true,
        "serverSide": true,
        "bFilter": true,
        //"dom": '<"toolbar">frtip',
        dom: 'lirtip',
        "bDestroy": true,
        "info": true,
        "stateSave": true,
        "lengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]],
        "ajax": {
            "url": "/UniversoEncuesta/GetList/",
            "type": "GET",
            "data": { 'proceso': id_proceso_cargar,'cadena':cadena,'filtro':filtro }
        },
        "fnInitComplete": function (oSettings, json) {


        },
        "fnDrawCallback": function (oSettings) {

        },


        "columns": [
            { "data": "Contrato", "orderable": false },
            { "data": "Nombre", "orderable": false },
            { "data": "Tel", "orderable": false },
            { "data": "Cel", "orderable": false },
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
    id_proceso_encuesta = full.IdProcesoEnc;
    if(full.Aplicada == false){
        return "<button class='btn btn-primary' onclick='aplicando_encuesta(this)' data-proceso='" + full.IdProcesoEnc + "' id='" + full.Id + "' data-contrato='" + full.Contrato + "' data-name='" + full.Nombre + "' data-plaza='" + full.IdPlaza + "'>Aplicar</button>";
    } else {
        return "<button class='btn btn-success disabled'>Aplicada</button>";
    }
    
}
function aplicando_encuesta(e) {
    var plaza = e.getAttribute("data-plaza");
    var contrato = e.getAttribute("data-contrato");
    var id = e.getAttribute("id");
    var proceso = e.getAttribute("data-proceso");
    
    $('#id_universo').val(id);
    $('#id_proceso').val(proceso);
    
    $.ajax({
        url: "/ProcesoEncuesta/GetDataClient/",
        type: "GET",
        data: { 'contrato': contrato, 'plaza': plaza },
        success: function (data, textStatus, jqXHR) {
            $('#nombre_cliente_modal').text(e.getAttribute("data-name"));
            $('#ciudad_modal').text("Ciudad: " + data[0].Ciudad);
            $('#colonia_modal').text("Colonia: " + data[0].Colonia);
            $('#calle_modal').text("Calle: " + data[0].Calle);
            $('#numero_modal').text("Número: " + data[0].NUMERO);
            $('#telefono_modal').text("Teléfono: " + e.getAttribute("data-telefono"));
            $('#cliente_id').val(e.getAttribute("data-contrato"));
            $('#modal_aplicar').modal('show');
            
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
   
}

function enviarEncuesta() {
    var contrato_id = $('#cliente_id').val();
    var plaza = $('#conexion_plaza').val();
    var id_proceso = $('#id_proceso').val();
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
            var id_universo = $('#id_universo').val();
            $.ajax({
                url: "/Encuesta/DatosEncuesta/",
                type: "POST",
                data: { 'encuesta': datos, 'universo': id_proceso },
                success: function (data, textStatus, jqXHR) {
                    changeStatus(id_universo);
                    document.getElementById("encuestaForm").reset();
                    $('#por_nombre').hide();
                    $('#por_contrato').hide();
                    $('#datos_contacto').hide();
                    $('#datos_contacto_error').hide();
                    $('#modal_aplicar').modal('hide');

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
function changeStatus(id) {
    $.ajax({
        url: "/UniversoEncuesta/Editar/",
        type: "POST",
        data: { 'id': id },
        success: function (data, textStatus, jqXHR) {
            swal({
                title: "!Hecho!", text: "La encuesta se aplicó correctamente!",
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
            document.getElementById("encuestaForm").reset();

        }
    });
}

function buscar() {
    var cadena = $('#text_buscar').val();
    LlenarTabla(cadena,'');
}

function confirmaTerminar() {
    $('#Espere').modal('show');
    $.ajax({
        url: "/UniversoEncuesta/TerminarProceso/",
        type: "POST",
        data: { 'id_proceso': id_proceso_encuesta },
        success: function (data, textStatus, jqXHR) {
            $('#Espere').modal('hide');
            swal({
                title: "!Hecho!", text: "El proceso fue terminado exitosamente!!",
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

function confirmaTerminarEncuesta() {
    var id_universo = $('#id_universo').val();
    var contrato_id = $('#cliente_id').val();
    $.ajax({
        url: "/UniversoEncuesta/TerminarEncuesta/",
        type: "POST",
        data: { 'id_proceso': id_universo, 'contrato': contrato_id },
        success: function (data, textStatus, jqXHR) {
            swal({
                title: "!Hecho!", text: "El proceso fue terminado exitosamente!!",
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