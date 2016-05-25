$('#guardar_pregunta').click(function () {
    var nombre_pregunta = $('#pregunta_nombre').val();
    var tipo_pregunta = $("#id_tipo_pregunta option:selected").val();
    if (tipo_pregunta == 1) {
        var detallePregunta = {};
        detallePregunta.IdPregunta = generateUUID();
        detallePregunta.Pregunta = nombre_pregunta;
        detallePregunta.IdTipoPregunta = tipo_pregunta;
        preguntas_lista.push(detallePregunta);
    }
    console.log(preguntas_lista);
});