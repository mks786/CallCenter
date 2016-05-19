// creamos un objeto donde guardaremos la lista preguntas que contendra la encuesta
var Lista_preguntas = [];
var Lista_opciones = [];

$(document).ready(function () {
    LlenaTabla();

    $(".Agregar").click(function () {
        $('#ModalAgregarEncuesta').modal('show');
        $('#msnTablavacia').show();
        $('#tablaPreguntas').hide();
        $('#TbodyPreguntas').empty();
        $('#nombreEncuesta').val('');
        $('#Descripcion_encuesta').val('');
        Lista_preguntas = [];
        Lista_opciones = [];
       
     
        document.getElementById("tituloModalAgregarEncuesta").innerHTML = "Agregar Encuesta";
        document.getElementById("guardarEncuesta").innerHTML = "Guardar Encuesta";
    });
});

//funcion:Cuando el documento este listo se llenara la tabla principal  con los registros de
//las encuestas disponibles ,con el complemento DATATABLES para mostrar y paginar facilmente
function LlenaTabla() {

    var objRoles = {};
    var data = JSON.stringify(objRoles);

    $('#TablaEncuesta').dataTable({
        "processing": true,
        "serverSide": true,
        "bFilter": false,
        "dom": '<"toolbar">frtip',
        "bDestroy": true,
        "info": true,
        "stateSave": true,
        "lengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]],
        "ajax": {
            "url": "/Encuesta/GetList/",
            "type": "POST",
            "data": { 'data': data },
        },
        "fnInitComplete": function (oSettings, json) {


        },
        "fnDrawCallback": function (oSettings) {


        },
        "columns": [
            { "data": "IdEncuesta", "orderable": false },
            { "data": "TituloEncuesta", "orderable": false },
            { "data": "Descripcion", "orderable": false },
            { "data": "FechaCreacion", "orderable": false },
        {
            sortable: false,
            "render": function (data, type, full, meta) {
                return Opciones(full.IdEncuesta);
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

    $("div.toolbar").html('<button class="btn btn-success btn-sm Agregar" style="float:right;" ><i class="fa fa-plus" aria-hidden="true"></i> Nueva encuesta</button> <div class="input-group input-group-sm"><input class="form-control" type="text"><span class="input-group-btn"><button class="btn btn-info btn-flat" type="button">Buscar</button></span></div>');

}

//funcion:retorna las opciones que tendra cada row en la tabla principal
function Opciones(id) {
    var opc = "<button class='btn btn-info btn-xs Detalle'  rel='" + id + "' type='button'>Detalles</button> <button class='btn btn-warning btn-xs Editar' rel='" + id + "' type='button'><i class='fa fa-pencil' aria-hidden='true'></i> Editar</button> <button class='btn btn-danger btn-xs eliminar' rel='" + id + "' type='button'> <i class='fa fa-trash-o' aria-hidden='true'></i> Eliminar</button> <a href='/Encuesta/Details/" + id + "' class='btn btn-success btn-xs'><i class='fa fa-pie-chart' aria-hidden='true'></i> Aplicar</a>"
    return opc;
}





$('#TablaEncuesta').on('click', '.Eliminar', function () {
    alert("click");
});

function ActualizaListaPreguntas(editar) {

    if (Lista_preguntas.length == 0) {
        $('#msnTablavacia').show();
        $('#tablaPreguntas').hide();
    }
    $('#TbodyPreguntas').empty();
    if (editar == true) {
        for (var b = 0; b < Lista_preguntas.length; b++) {
            $('#tablaPreguntas').append("<tr><td>dsgdfgdsdgsf" + Lista_preguntas[b].Pregunta + "</td><td>" + Lista_preguntas[b].txtTipoPregunta + "</td><button class='btn btn-info btn-xs detallepregunta' rel='" + Lista_preguntas[b].IdPregunta2 + "'>Detalles</button> <button class='btn btn-warning btn-xs EditarPregunta ' rel='" + Lista_preguntas[b].IdPregunta2 + "'>Editar</button> <button class='btn btn-danger btn-xs EliminaPregunta' rel='" + Lista_preguntas[b].IdPregunta2 + "'>Eliminar</button></td></tr>");

        }

    } else {
        for (var b = 0; b < Lista_preguntas.length; b++) {
            $('#tablaPreguntas').append("<tr><td>" + Lista_preguntas[b].Pregunta + "</td><td>" + Lista_preguntas[b].txtTipoPregunta + "</td><td><button class='btn btn-info btn-xs detallepregunta' rel='" + Lista_preguntas[b].IdPregunta + "'>Detalles</button> <button class='btn btn-warning btn-xs EditarPregunta ' rel='" + Lista_preguntas[b].IdPregunta + "'>Editar</button> <button class='btn btn-danger btn-xs EliminaPregunta' rel='" + Lista_preguntas[b].IdPregunta + "'>Eliminar</button></td></tr>");

        }
    }


}




