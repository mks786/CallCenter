
$('#tablaPreguntas').on('click', '.EliminaPregunta', function () {
    var id = $(this).attr('rel');
    $('#ModalEliminaPregunta').modal('show');
    $('#idpregunta').val(id);

});




$('#confirmaEliminaPregunta').click(function () {
    var id = $('#idpregunta').val();
   EliminarDeArreglo(Lista_preguntas, 'IdPregunta', id);
   swal("Correcto !", "Se ha eliminado la pregunta!", "success");
   $('#ModalEliminaPregunta').modal('hide');
   ActualizaListaPreguntas();
});