function eliminarEncuesta(id) {
    var name_encuesta = id.getAttribute("data-name");
    var id_encuesta = id.getAttribute("id");
    $('#Modal_id_encuesta').val(id_encuesta);
    $('#Modal_nombre_encuesta').text(name_encuesta);
    $('#ModalEliminarEncuesta').modal('show');
}



$('#EliminaEncuesta').click(function () {

  var datos=  $('#Modal_id_encuesta').val();

    $.ajax({
        url: "/Encuesta/Delete/",
        type: "POST",
        data: { 'id': datos },
        success: function (data, textStatus, jqXHR) {

            $('#ModalEliminarEncuesta').modal('hide');
            swal({
                title: "!Hecho!", text: "Encuesta se eliminó exitosamente!",
                type: "error",
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
            sweetAlert("Oops...", "La encuesta ya esta aplicada", "error");
        }
    });


});