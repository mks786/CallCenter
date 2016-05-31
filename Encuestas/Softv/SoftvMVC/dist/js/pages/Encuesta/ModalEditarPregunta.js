

$('#tablaPreguntas').on('click', '.EditarPregunta', function () {

    $('#edPanelPreguntaOptMultiple').hide();
    $('#edPanelPreguntaCerrada').hide();
    $('#edPanelPreguntaCerrada-tbody').empty();
    $('#edPanelPreguntaOptMultiple-tbody').empty();
    $('#ed_NombrePregunta').val('');
    $('#ed_TipoPregunta').val('');
    $('#idParaEditar').val('');
    $('#idParaEditar2').val('');


    var id = $(this).attr('rel');
    $('#ModalEditarPregunta').modal('show');
    $('#ed_TipoPregunta').attr('disabled','');
    var result = $.grep(Lista_preguntas, function (e) { return e.IdPregunta == id || e.IdPregunta2 == id; });
    

    for (var r = 0; r < result.length; r++) {
        $('#ed_NombrePregunta').val(result[r].Pregunta);
        $('#ed_TipoPregunta').val(result[r].IdTipoPregunta);

        if (result[r].TipoPregunta == "1") {
            $('#ed_TipoPregunta').attr("disabled",true);
            
           
        }
        else {
            $('#ed_TipoPregunta').attr("disabled", false);
        }   
        $('#ed_TipoPregunta').attr("disabled", true);
        
        $('#idParaEditar').val(result[r].IdPregunta);
        $('#idParaEditar2').val(result[r].IdPregunta2);

        var respuestas = $.grep(Lista_opciones, function (e) { return e.Id_ResOpcMult == result[r].IdPregunta; });

   

        for (var t = 0; t < respuestas.length; t++) {
            if (result[r].IdTipoPregunta == "1") {
            }
            if (result[r].IdTipoPregunta == "2") {
               
                
                $('#edPanelPreguntaCerrada').show();
                $('#edPanelPreguntaOptMultiple').hide();
                $('#edPanelPreguntaCerrada-tbody').append("<tr class='nrespuestac'><td></td><td><input type='text' class='form-control' value='" + respuestas[t].ResOpcMult + "'></td><td><button class='btn btn-danger btn-xs edEliminarRespuestaC'>Quitar</button></td></tr>");
            }
            if (result[r].IdTipoPregunta == "3") {
              
                $('#edPanelPreguntaOptMultiple').show();
                $('#edPanelPreguntaCerrada').hide();
                $('#edPanelPreguntaOptMultiple-tbody').append("<tr class='nrespuesta'><td></td><td><input type='text' class='form-control' id='" + respuestas[t].Id_ResOpcMult2 + "' value='" + respuestas[t].ResOpcMult + "' disabled></td><td><button class='btn btn-danger btn-xs edEliminarRespuestaOM'>Quitar</button></td></tr>");
            }
        }


    }

});

// agrega dinamicamnete al hacer click diferentes tipos de respuesta de la pregunta
$('#edAgregarRespuestasC').click(function () {
    $('#edPanelPreguntaCerrada-tbody').append("<tr class='nrespuestac'><td></td><td class='nrespuesta'><input class='flexdatalist form-control' class='resp' placeholder='Respuesta' type='text'></td><td><button class='btn btn-danger btn-xs edEliminarRespuestaC'>Quitar</button></td></tr>");
    
});

function quitAutofocus() {
    $('#focus').removeAttr("id");
}

// agrega dinamicamnete al hacer click diferentes tipos de respuesta de la pregunta
$('#edAgregarRespuestasOM').click(function () {
    $('#edPanelPreguntaOptMultiple-tbody').append("<tr class='nrespuesta'><td></td><td><input class='flexdatalist form-control' placeholder='Respuesta' type='text'  id='focus' onkeypress='quitAutofocus()'></td><td><button class='btn btn-danger btn-xs edEliminarRespuestaOM'>Quitar</button></td></tr>");
    $('#focus').focus();
});

//funcion: cuando se hace cambio del select tipo pregunta  se ocultan y muestran los paneles correspondientes
//ademas se borran las tablas de cada tipo de pregunta
$('#ed_TipoPregunta').change(function () {
    

    var tipo = $('#ed_TipoPregunta').val();
    if (tipo == "1") {
        $('#ed_tipodecontrol').hide();
        $('#edPanelPreguntaCerrada').hide();
        $('#edPanelPreguntaOptMultiple').hide();

    }
    if (tipo == "2") {
        $('#edPanelPreguntaCerrada').hide();
        $('#edPanelPreguntaOptMultiple').hide();
        $('#edPanelPreguntaCerrada-tbody').empty()   
    }
    if (tipo == "3") {
        $('#edPanelPreguntaCerrada').hide();
        $('#edPanelPreguntaOptMultiple').show();
        $('#edPanelPreguntaOptMultiple-tbody').empty();

    }

});

$('#EditarPregunta').click(function () {

    var IdPregunta = $('#idParaEditar').val();
    var IdPregunta2 = $('#idParaEditar2').val();
   

    var detallePregunta = {};
    var duplicado = [];
    var lista_opciones_aux = [];
    detallePregunta.IdPregunta = generateUUID();
    detallePregunta.IdPregunta2 = IdPregunta2;
    detallePregunta.Pregunta = $('#ed_NombrePregunta').val();
    detallePregunta.IdTipoPregunta = $('#ed_TipoPregunta').val();
    detallePregunta.txtTipoPregunta = $("#ed_TipoPregunta option:selected").text();
    var nombre_pregunta = $('#ed_NombrePregunta').val();

    if (nombre_pregunta == "") {
        swal("El nombre de la pregunta es obligatorio", "", "error");
    } else {
        var tbody = $("#edPanelPreguntaOptMultiple-tbody");
        var seleccion = $('#ed_TipoPregunta').val();
        if (seleccion == "1") {
            EliminarDeArreglo(Lista_preguntas, "IdPregunta", IdPregunta);//se elimina la pregunta del arreglo de preguntas
            Lista_preguntas.push(detallePregunta);
            if (IdPregunta2 == undefined) {
                ActualizaListaPreguntas(true);
            } else {
                ActualizaListaPreguntas();
            }
            $('#ModalEditarPregunta').modal("hide");
        }
        else if (seleccion == "2") {
            $('#edPanelPreguntaCerrada').empty();
            EliminarDeArreglo(Lista_preguntas, "IdPregunta", IdPregunta);//se elimina la pregunta del arreglo de preguntas
            Lista_preguntas.push(detallePregunta);
            if (IdPregunta2 == undefined) {
                ActualizaListaPreguntas(true);
            } else {
                        ActualizaListaPreguntas();
             }
            $('#ModalEditarPregunta').modal("hide");
        }
        else if (seleccion == "3") {
            if (tbody.children().length == 0) {
                swal("Por favor agrega respuestas a la pregunta", "", "error");
            } else {
                var vacios = 0;
                $('#edTablaRespuestasOM > tbody  > tr').each(function () {
                    var x = $(this).closest(".nrespuesta").find("input:text").map(function () { return $(this); });
                    var test1 = $(this).closest(".nrespuesta").find("input:text").map(function () { return $(this).val(); });
                    for (var i = 0; i < x.length; i++) {
                        Opciones = {};
                        Opciones.Id_ResOpcMult = detallePregunta.IdPregunta;
                        if (x[i].attr('id') == 'undefined') {
                            Opciones.Id_ResOpcMult2 = '';
                        } else if (x[i].attr('id') == 'focus') {
                            Opciones.Id_ResOpcMult2 = '';
                        }
                        else {
                            Opciones.Id_ResOpcMult2 = x[i].attr('id');
                        }
                        if (x[i].val() == "") {
                            vacios = vacios + 1;
                        } else {
                            Opciones.ResOpcMult = x[i].val();
                            duplicado.push(x[i].val());
                            lista_opciones_aux.push(Opciones);
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
                    EliminarDeArreglo(Lista_opciones, "IdPregunta", IdPregunta2);

                    for (var i = 0; i < lista_opciones_aux.length; i++) {
                        Lista_opciones.push(lista_opciones_aux[i]);
                    }
                    EliminarDeArreglo(Lista_preguntas, "IdPregunta", IdPregunta);//se elimina la pregunta del arreglo de preguntas
                    Lista_preguntas.push(detallePregunta);
                    ActualizaListaPreguntas();
                    $('#ModalEditarPregunta').modal("hide");
                    $('#PanelPreguntaCerrada').hide();
                }
            }
        }
    }
    

});

$('#edTablaRespuestasOM').on('click', '.edEliminarRespuestaOM', function () {
    $(this).closest('tr').remove();
});

//elimina las respuestas  de la tabla 
$('#edTablaRespuestasC').on('click', '.edEliminarRespuestaC', function () {
    $(this).closest('tr').remove();
});