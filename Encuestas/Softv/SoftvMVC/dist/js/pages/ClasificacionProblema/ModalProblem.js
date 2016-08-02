function agregar() {
    $('#ModalClasificacionProblem').modal('show');
}


function enviar_problema() {

    if (document.getElementById('Activo').checked) { var a = true } else { var a = false }

    var objClasificacionProblema = {};

    objClasificacionProblema.Descripcion = $('#Descripcion').val();
    objClasificacionProblema.Activo = a;


    if (objClasificacionProblema.Descripcion == "") {
        swal("Por favor ingrese un Problema", "", "error");
    }
    else {
        $.ajax({
            url: "/ClasificacionProblema/Create/",
            type: "POST",
            data: { 'objClasificacionProblema': objClasificacionProblema },
            success: function (data, textStatus, jqXHR) {
                swal({
                    title: "!Hecho!", text: "Clasificación de Problema agregado con éxito!",
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

function detalle_Problema(data) {

    var IdProblema = (data.getAttribute('id'));
    var Descripcion = (data.getAttribute('data-name'));
    var Activo = (data.getAttribute('data-name2'));


    $('.detalle_Problem').val(data.getAttribute('data-name'));
    $('.detalle_Problem').attr('id', data.getAttribute('id'));


    if (Activo == "true") {
        $('#Activo_detalle').prop("checked", true);
    } else {
        $('#Activo_detalle').prop("checked", false);
    }

    $('#ModalDetalleClasificacionProblem').modal('show');
}


function editar_Problema(data) {

    var IdProblema = (data.getAttribute('id'));
    var Descripcion = (data.getAttribute('data-name'));
    var Activo = (data.getAttribute('data-name2'));

    $('.editar_Problem').val(data.getAttribute('data-name'));
    $('.editar_Problem').attr('id', data.getAttribute('id'));


    if (Activo == "true") {
        $('#Activo_editar').prop("checked", true);
    } else {
        $('#Activo_editar').prop("checked", false);
    }
    $('#ModalEditarClasificacionProblem').modal('show');

}



function enviar_editar_MotivoInfo() {
    var activo = $('#Activo_editar').prop("checked");
    if (activo) { var a = true } else { var a = false }

    var Descripcion = $('.editar_Problem').val();
    var IdProblem = $('.editar_Problem').attr('id');


    var objClasificacionProblema = {};
    objClasificacionProblema.ClvProblema = IdProblem;
    objClasificacionProblema.Descripcion = Descripcion;
    objClasificacionProblema.Activo = a;


    if (objClasificacionProblema.Descripcion == "") {
        swal("Por favor seleccione un Rol", "", "error");
    }
    else {

        $.ajax({
            url: "/ClasificacionProblema/Edit/",
            type: "POST",
            data: { 'objClasificacionProblema': objClasificacionProblema },
            success: function (data, textStatus, jqXHR) {
                swal({
                    title: "!Hecho!", text: "Clasificación de Problema editado con éxito!",
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


$('#ClasificacionProblem').on('click', '.eliminar', function () {


    var id = $(this).attr('id');
    var Descripcion = $(this).attr('data-name');

    $('#IdProblema').val(id);
    $('#Eliminar_Problema').text(Descripcion)
    $('#ModalEliminaClasificacionProblem').modal('show');

})

function confirmar_eliminar_Problema() {

    var IdProblema = $('#IdProblema').val();

    if (Descripcion == "") {
        swal("Por favor introduce el problema", "", "error");
    } else {
        $.ajax({
            url: "/ClasificacionProblema/Delete/",
            type: "POST",
            data: { 'id': IdProblema },
            success: function (data, textStatus, jqXHR) {
                swal({
                    title: "!Hecho!", text: "Clasificación del Problema eliminado con éxito!",
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