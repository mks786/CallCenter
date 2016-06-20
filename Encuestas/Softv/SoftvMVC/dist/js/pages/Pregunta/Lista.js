$(document).ready(function () {
    LlenarTabla();
    
   

    $(".Agregar").click(function () {
        $('#ModalAgregarPregunta').modal('show');

    });

});
var preguntas_lista = [];



function LlenarTabla(cadena) {
    if (cadena == undefined) {

        cadena = "";
    }
    $('#TablaPreguntas').dataTable({
        "processing": true,
        "serverSide": true,
        "bFilter": false,
        "dom": '<"toolbar">frtip',
        "bDestroy": true,
        "info": true,
        "stateSave": true,
        "lengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]],
        "ajax": {
            "url": "/Pregunta/GetList/",
            "type": "POST",
            "data": { 'data': cadena },
        },
        "fnInitComplete": function (oSettings, json) {


        },
        "fnDrawCallback": function (oSettings) {

        },


        "columns": [
            { "data": "IdPregunta", "orderable": false },
            { "data": "Pregunta", "orderable": false },
            { "data": "IdTipoPregunta", "orderable": false },
            //{ "data": "Cerrada", "orderable": false },
            //{ "data": "OpcMultiple", "orderable": false },
            //{ "data": "Abierta", "orderable": false },
            ////{
            ////    sortable: false,
            ////    "render": function (data, type, full, meta) {
            ////        console.log(full);                   
            ////    }
            ////},

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
    })

    $("div.toolbar").html('<div class="input-group input-group-sm"><input class="form-control" type="text" id="buscar"><span class="input-group-btn"><button class="btn btn-info btn-flat" type="button" onclick="Busqueda()">Buscar</button></span></div>');
    //respaldo de boton agregar
    //$("div.toolbar").html('<button class="btn btn-success btn-sm Agregar" style="float:right;" ><i class="fa fa-plus" aria-hidden="true"></i> Nueva Pregunta </button> <div class="input-group input-group-sm"><input class="form-control" type="text"><span class="input-group-btn"><button class="btn btn-info btn-flat" type="button">Buscar</button></span></div>');

}

function Opciones() {
    var botones = "<button class='btn btn-warning btn-xs editarPregunta' id='editarPregunta'>Editar</button>";
    return botones;
}


function Busqueda() {
    var cadena = $('#buscar').val();
    if(cadena == ""){
        LlenarTabla();
    } else {
        LlenarTabla(cadena);
    }
}
//funcion:retorna las opciones que tendra cada row en la tabla principal
function Opciones(data) {
    var opc = "<button class='btn btn-warning btn-xs Editar' type='button' id='" + data.IdPregunta + "' onclick='editar(this.id)'><i class='fa fa-pencil' aria-hidden='true'></i> Editar</button>"
    return opc;
}

function detalle(id){
    $.ajax({
        url: "/Pregunta/GetOnePregunta/",
        type: "GET",
        data: { 'id': id },
        success: function (data, textStatus, jqXHR) {
            detalle_dom(data);  
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}
    
function editar(id) {
    $.ajax({
        url: "/Pregunta/GetOnePregunta/",
        type: "GET",
        data: { 'id': id },
        success: function (data, textStatus, jqXHR) {
            editar_dom(data);
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

function editar_dom(datos) {
    var datos = datos;
    $('#pregunta_nombre').val(datos.pregunta.Pregunta);
    $('#id_pregunta').val(datos.pregunta.IdPregunta);
    var tipo = datos.pregunta.IdTipoPregunta;
    $('#id_tipo_pregunta').val(tipo).change();
    if (tipo == 1) {
        $('#panel_opcion_multiple').hide();

    } else if (tipo == 2) {
        $('#panel_opcion_multiple').hide();
    } else {
        $('#panel_opcion_multiple').show();
        $("#tabla_respuestas > tbody").html("");
        for (var i = 0; i < datos.respuestas.length; i++) {
            $('#body_opcion_multiple').append("<tr><td><input type='text' class='form-control' id='" + datos.respuestas[i].Id_ResOpcMult + "' value='" + datos.respuestas[i].ResOpcMult + "' disabled> </td></tr>");
           //con boton eliminar $('#body_opcion_multiple').append("<tr><td><input type='text' class='form-control' id='" + datos.respuestas[i].Id_ResOpcMult + "' value='" + datos.respuestas[i].ResOpcMult + "' disabled> </td><td><button class='btn btn-danger btn-xs eliminar_respuestas'>Quitar</button></td></tr>");
        }

    }
    $('#ModalEditarPregunta').modal('show');
}

//elimina las respuestas  de la tabla 
$('#tabla_respuestas').on('click', '.eliminar_respuestas', function () {
    $(this).closest('tr').remove();;
});

function detalle_dom(datos) {
    var datos = datos;
    $('#nombre_pregunta').val(datos.pregunta.Pregunta);
    var tipo = datos.pregunta.IdTipoPregunta;
    var nombre_tipo = '';
    if (tipo == 1) {
        $('#multiple_panel').hide();
        var nombre_tipo = 'Pregunta Abierta';

    } else if (tipo == 2) {
        $('#multiple_panel').hide();
        var nombre_tipo = 'Pregunta Cerrada';
    } else {
        var nombre_tipo = 'Pregunta Opción Múltiple';
        $('#multiple_panel').show();
        for (var i = 0; i < datos.respuestas.length; i++) {
            $('#add_panel').append("<option>" + datos.respuestas[i].ResOpcMult + "</option>");
        }

    }
    $('#tipo_pregunta').text(nombre_tipo);
    $('#ModalDetallePregunta').modal('show');
}
$('#TablaPreguntas').on('click', '.Eliminar', function () {
    $('#ModalEliminarPregunta').modal('show');
    alert("click");
});

function AgregarRespuesta() {
    $('#body_opcion_multiple').append("<tr><td><input type='text' id='focus' onkeypress='removeFocus()' class='form-control focus'></td></tr>");
    //boton de eliminar $('#body_opcion_multiple').append("<tr><td><input type='text' id='focus' onkeypress='removeFocus()' class='form-control focus'> </td><td><button class='btn btn-danger btn-xs eliminar_respuestas'>Quitar</button></td></tr>");
    $('#focus').focus();
}

function removeFocus() {
    $('#focus').removeAttr('autofocus').removeAttr('id');
}

$('#id_tipo_pregunta').on('change', function () {
    var tipo = this.value;

    if(tipo == 1){
        $('#panel_opcion_multiple').hide();
    }else if(tipo == 2){
        $('#panel_opcion_multiple').hide();
    } else {
        $("#tabla_respuestas > tbody").html("");
        $('#panel_opcion_multiple').show();
    }
});

function guardar_pregunta() {
    var detallePregunta = {};
    var respuestas = [];
    var nombre = $('#pregunta_nombre').val();
    var tipo_pregunta = $('#id_tipo_pregunta').val();
    var nulo = 0;
    if(nombre == ""){
        swal("Por favor introduce el nombre de la pregunta", "", "error");
    } else {
        detallePregunta.Pregunta = nombre;
        detallePregunta.IdTipoPregunta = tipo_pregunta;
        detallePregunta.IdPregunta = $('#id_pregunta').val();
        if (tipo_pregunta == 3){
            $('#body_opcion_multiple tr').each(function () {
                var respuesta = {};
                var id = $(this).find("td").find('input').eq(0).attr('id');
                var valor = $(this).find("td").find('input').eq(0).val();
                if (valor == "") {
                    nulo = nulo + 1;
                } else {
                    respuesta.ResOpcMult = valor;
                }
                if (id == undefined || id == "focus") {
                    
                    
                } else {
                    respuesta.Id_ResOpcMult = id;
                }

                respuestas.push(respuesta);
            });

        }

    }
    if (nulo == 0) {
        console.log(respuestas);
        $.ajax({
            url: "/Pregunta/editarPregunta/",
            type: "POST",
            data: { 'detallePregunta': detallePregunta, 'respuestas': respuestas },
            success: function (data, textStatus, jqXHR) {
                $('#ModalEditarPregunta').modal("hide");
                LlenarTabla();
                swal("La pregunta se editó exitosamente", "", "success");
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });
    } else {
        console.log('completar respuestas');
    }
    
}