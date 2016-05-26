



function GuardaUsuario() {
    
    
     if ($('#Nombre').val() == "") {
        swal("Define un nombre para el usuario", "", "error");
    }
    else if($('#email').val()==""){
        swal("Define un correo para el usuario", "", "error");
    }
    else if ($('#pass').val()=="") {
        swal("Define un contraseña para el usuario", "", "error");
    }
    else if ($('#cpass').val() == "") {
        swal("Confirma contraseña", "", "error");
    }
    else if ($('#pass').val() != $('#cpass').val()) {
        swal("Las contraseñas no coinciden", "", "error");
    }

    else if ($('#rol').val() == null) {

        swal("Define un rol para el usuario", "", "error");
    }
    else {
        var objUsuario = {};
        objUsuario.IdRol= $('#rol').val();
        objUsuario.Nombre= $('#Nombre').val();
        objUsuario.Email=$('#email').val();
        objUsuario.Usuario=
        objUsuario.Password= $('#pass').val();
        var checked = $("#Status").parent('[class*="icheckbox"]').hasClass("checked");
        if (checked) {
            objUsuario.Estado = 1;
        }
        else{
            objUsuario.Estado = 0;
        }
           
        console.log(objUsuario);
        
    }
   
   
    
   
   
   
  
   
    



       
}