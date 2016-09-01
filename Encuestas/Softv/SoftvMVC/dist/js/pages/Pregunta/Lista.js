var datos_respuestas;
$(document).ready(function () {
    LlenarTabla();
    
    
    $.ajax({
        url: "/ResOpcMults/TodasRespuestas/",
        type: "GET",
        success: function (data, textStatus, jqXHR) {
            datos_respuestas = data;
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });

    $(".Agregar").click(function () {
        $('#NombrePregunta').val(" "); 
        $('#TipoPregunta').val(0); 
        $('#PanelPreguntaOptMultiple').hide(); 
        $('#PanelPreguntaOptMultiple-tbody').empty();
        $('#ModalAgregarPregunta').modal('show');

    });
    $('#TipoPregunta').on('change', function () {
        var tipo = $(this).val();
        if (tipo == 3) {
            $('#PanelPreguntaOptMultiple').show();
        } else {
            $('#PanelPreguntaOptMultiple').hide();
        }
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
        "stateSave": false,
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
            {
                sortable: false,
                "render": function (data, type, full, meta) {
                    var tipo;
                    if (full.IdTipoPregunta == 1) {
                        tipo = 'Abierta';
                    } else if (full.IdTipoPregunta == 2) {
                        tipo = 'Cerrada';
                    } else if (full.IdTipoPregunta == 3) {
                        tipo = 'Opción Múltiple';
                    }
                    return ('<td>'+tipo+'</td>');
                }
            },
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

    //$("div.toolbar").html('<div class="input-group input-group-sm"><input class="form-control" type="text" id="buscar"><span class="input-group-btn"><button class="btn btn-info btn-flat" type="button" onclick="Busqueda()"><i class="fa fa-search" aria-hidden="true"></i> Buscar</button></span></div>');
    //respaldo de boton agregar
    $("div.toolbar").html('<button class="btn btn-success Agregar" style="float:right;" ><i class="fa fa-plus" aria-hidden="true"></i> Nueva Pregunta </button> <div class="input-group input-group-sm"><input class="form-control" type="text"><span class="input-group-btn"><button class="btn btn-info btn-flat" type="button">Buscar</button></span></div>');

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
    var opc;
    if (permiso_editar == "False") {
        opc = ""
    }else{
        opc = "<button class='btn btn-warning btn-xs Editar' type='button' id='" + data.IdPregunta + "' onclick='editar(this.id)'><i class='fa fa-pencil' aria-hidden='true'></i> Editar</button>"
    }
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
            //$('#body_opcion_multiple').append("<tr><td><input type='text' class='form-control' id='" + datos.respuestas[i].Id_ResOpcMult + "' value='" + datos.respuestas[i].ResOpcMult + "' disabled> </td></tr>");
            $('#body_opcion_multiple').append("<tr><td><input type='text' class='form-control' id='" + datos.respuestas[i].Id_ResOpcMult + "' value='" + datos.respuestas[i].ResOpcMult + "' disabled> </td><td><button class='btn btn-danger btn-xs eliminar_respuestas'>Quitar</button></td></tr>");
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


function AgregarRespuesta_editar() {
    //$('#body_opcion_multiple').append("<tr><td><input type='text' id='focus' onkeypress='removeFocus()' class='form-control focus'></td></tr>");
    $('#body_opcion_multiple').append("<tr><td><input type='text' id='focus' onkeypress='removeFocus()' class='form-control focus'> </td><td><button class='btn btn-danger btn-xs eliminar_respuestas'>Quitar</button></td></tr>");
    $('#focus').focus();
    var demo2 = new autoComplete({
        selector: '.focus',
        minChars: 1,
        source: function (term, suggest) {
            term = term.toUpperCase();
            var suggestions = [];
            for (i = 0; i < datos_respuestas.length; i++)
                if (~datos_respuestas[i].ResOpcMult.toUpperCase().indexOf(term)) suggestions.push(datos_respuestas[i].ResOpcMult);
            suggest(suggestions);
        }
    });
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
    var duplicado = [];
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
                duplicado.push(respuesta);
            });

        }

    }
    if (nulo == 0) {
        var contador = 0;
        for (var i = 0; i < duplicado.length - 1; i++) {
            for (var j = i + 1; j < duplicado.length; j++) {
                if (duplicado[i].ResOpcMult == duplicado[j].ResOpcMult) {
                    contador += 1;
                }
            }

        }
        if(contador > 0){
            swal("No puede haber preguntas iguales", "", "error");
        } else {
            $.ajax({
                url: "/Pregunta/editarPregunta/",
                type: "POST",
                data: { 'detallePregunta': detallePregunta, 'respuestas': respuestas },
                success: function (data, textStatus, jqXHR) {
                    $('#ModalEditarPregunta').modal("hide");
                    swal({
                        title: "!Hecho!", text: "La pregunta se editó exitosamente!",
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
                    swal("No puede haber preguntas iguales", "", "error");
                }
            });
        }
        
    } else {
        swal("Por favor llena todas las respuestas o elimina las vacias", "", "error");
    }
    
}

$('#TablaRespuestasOM').on('click', '.EliminarRespuestaC', function () {
    $(this).closest('tr').remove();;
});

function AgregarRespuesta() {
    $('#PanelPreguntaOptMultiple-tbody').append("<tr class='nrespuestac'><td></td><td class='nrespuesta'><input class='agregar_reciclado form-control resp' placeholder='Respuestas' type='text' id='focus' onkeypress='quitAutofocus()'></td><td><button class='btn btn-danger btn-xs EliminarRespuestaC'>Quitar</button></td></tr>");
    $('#focus').focus();
    
    var demo1 = new autoComplete({
        selector: '.agregar_reciclado',
        minChars: 1,
        source: function (term, suggest) {
            term = term.toUpperCase();
            var suggestions = [];
            for (i = 0; i < datos_respuestas.length; i++)
                if (~datos_respuestas[i].ResOpcMult.toUpperCase().indexOf(term)) suggestions.push(datos_respuestas[i].ResOpcMult);
            suggest(suggestions);
        }
    });
}

function quitAutofocus() {
    $('#focus').removeAttr("id");
    $('#focus').removeClass("agregar_reciclado");
}

$('#GuardarPregunta').on('click', function () {
    var nombre_pregunta = $('#NombrePregunta').val();
    var tipo_pregunta = $('#TipoPregunta').val();
    var pregunta = {};
    if (nombre_pregunta == " ") {
        swal("El nombre de la pregunta es obligatorio", "", "error");
    } else {
        pregunta.Pregunta = nombre_pregunta;
        if (tipo_pregunta == null) {
            swal("Seleccione el tipo de pregunta", "", "error");
        } else {
            pregunta.IdTipoPregunta = tipo_pregunta;
            if (tipo_pregunta == 3) {
                var Lista_opciones = [];
                var duplicado = [];
                var vacios = 0;
                $('#TablaRespuestasOM > tbody  > tr').each(function () {
                    var test1 = $(this).closest(".nrespuestac").find("input:text").map(function () { return $(this).val(); });
                    for (var a = 0; a < test1.length; a++) {
                        Opciones = {};
                        Opciones.ResOpcMult = test1[a];
                        duplicado.push(test1[a]);
                        Lista_opciones.push(Opciones);
                        if (test1[a] == "") {
                            vacios = vacios + 1;

                        }
                    }
                });
                var contador = 0;
                for (var i = 0; i < duplicado.length - 1; i++) {
                    for (var j = i + 1; j < duplicado.length; j++) {
                        if (duplicado[i] == duplicado[j]) {
                            contador += 1;
                        }
                    }

                }
                if (vacios > 0) {
                    swal("Por favor llena todas las respuestas o elimina las vacias", "", "error");
                } else if (contador > 0) {
                    swal("No puede haber respuestas duplicadas", "", "error");
                } else {
                    $.ajax({
                        url: "/Pregunta/AddPregunta/",
                        type: "POST",
                        data: { 'pregunta': pregunta, 'respuestas': Lista_opciones },
                        success: function (data, textStatus, jqXHR) {
                            swal({
                                title: "!Hecho!", text: "Respuesta agregada con éxito!",
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
                            swal("No puede haber preguntas iguales", "", "error");
                        }
                    });
                }

            } else {
                $.ajax({
                    url: "/Pregunta/AddPregunta/",
                    type: "POST",
                    data: { 'pregunta': pregunta},
                    success: function (data, textStatus, jqXHR) {
                        $('#ModalAgregarPregunta').modal("hide");
                        swal({
                            title: "!Hecho!", text: "Respuesta agregada con éxito!",
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

});