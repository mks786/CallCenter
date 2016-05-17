using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftvMVC.Controllers
{
    public partial class BaseController
    {
        public string BuildScriptsDefault(String Controler)
        {

            //string path =Url.Action(null,Controler);
            String strScript = @"
               //var pageSelected=1;  
            $(document).ready(function () {
              
               ReloadScript();   
               $('#selectState').selectric();
               $('#selectPage').selectric();
               $('#selectSearch').selectric();   
               $('#btnsearchmore').hide();  
              });           











            //funcion para iniciar tooltip
        function loadTooltipIndex() {
            
            //
            $('.btnNew').qtip({
                position: {
                    my: 'right top',
                    at: 'top rigth'
                },
                style: {
                    classes: 'qtip-bootstrap',
                    tip: {
                        corner: true,
                        mimic: 'bottom left',
                        border: 1,
                        width: 88,
                        height: 56
                    }
                },

                content: {
                    text: 'Agregar Nuevo'
                }
            });

            //lockbtn
            $('.btnlockstatus').qtip({
                position: {
                    my: 'right top',
                    at: 'top rigth'
                },
                style: {
                    classes: 'qtip-bootstrap',
                    tip: {
                        corner: true,
                        mimic: 'bottom left',
                        border: 1,
                        width: 88,
                        height: 56
                    }
                },

                content: {
                    text: 'Cambiar Estado'
                }
            });

            //btn editar
            $('.btnedit').qtip({
                position: {
                    my: 'right top',
                    at: 'top rigth'
                },
                style: {
                    classes: 'qtip-bootstrap',
                    tip: {
                        corner: true,
                        mimic: 'bottom left',
                        border: 1,
                        width: 88,
                        height: 56
                    }
                },

                content: {
                    text: 'Editar'
                }
            });

            //btn detalles
            $('.btndetails').qtip({
                position: {
                    my: 'right top',
                    at: 'top rigth'
                },
                style: {
                    classes: 'qtip-bootstrap',
                    tip: {
                        corner: true,
                        mimic: 'bottom left',
                        border: 1,
                        width: 88,
                        height: 56
                    }
                },

                content: {
                    text: 'Detalles'
                }
            });

            //btn eliminar
            $('.btndelete').qtip({
                position: {
                    my: 'right top',
                    at: 'top rigth'
                },
                style: {
                    classes: 'qtip-bootstrap',
                    tip: {
                        corner: true,
                        mimic: 'bottom left',
                        border: 1,
                        width: 88,
                        height: 56
                    }
                },

                content: {
                    text: 'Eliminar'
                }
            });

            //btn actualizar
            $('#btnupdate').qtip({
                position: {
                    my: 'right top',
                    at: 'top rigth'
                },
                style: {
                    classes: 'qtip-bootstrap',
                    tip: {
                        corner: true,
                        mimic: 'bottom left',
                        border: 1,
                        width: 88,
                        height: 56
                    }
                },

                content: {
                    text: 'Actualizar'
                }
            });

            //dd mostrar x registros
            $('.selectpage').qtip({
                position: {
                    my: 'right top',
                    at: 'top rigth'
                },
                style: {
                    classes: 'qtip-bootstrap',
                    tip: {
                        corner: true,
                        mimic: 'bottom left',
                        border: 1,
                        width: 88,
                        height: 56
                    }
                },

                content: {
                    text: 'Cantidad Filas'
                }
            });

            //btn nuevo
            //$('.btn').qtip({
            //    position: {
            //        my: 'right top',
            //        at: 'top rigth'
            //    },
            //    style: {
            //        classes: 'qtip-bootstrap',
            //        tip: {
            //            corner: true,
            //            mimic: 'bottom left',
            //            border: 1,
            //            width: 88,
            //            height: 56
            //        }
            //    },

            //    content: {
            //        text: 'Nuevo'
            //    }
            //});
           
            
        }

            function ReloadScript() {
            loadTooltipIndex();

//              //////////quitar la paginacion del seleccionado
//        $('#Paginator li').click(function () {
//            pageSelected = ($(this).text());
//            console.log(pageSelected);
//            var myClass = $(this).attr('class');
//            if (myClass == 'active') {
//                return false;
//            }
//
//        });

             $('#Paginator li').click(function () {
            pageActual = ($(this).text());
            console.log('pageActual: ' + pageActual);
            if (pageActual == 'Anterior') {
                pageActual =parseInt(pageSelected) - 1;
                pageSelected = pageActual;
                console.log('anterior: ' + pageSelected);

            }
            if (pageActual == 'Siguiente') {
                 if (typeof pageSelected === 'undefined') { pageSelected = 1 }
                pageActual = parseInt(pageSelected) + 1;
                pageSelected = pageActual;
                console.log('siguiente: ' + pageSelected);
                

            }
            else {
                if(pageActual == 'Ultima')
                  pageActual = ($(this).find('a').attr('href').split('?')[1].split('=')[1])
                pageSelected = pageActual
                console.log('default: ' + pageSelected);
            }

            //console.log(pageSelected);
            var myClass = $(this).attr('class');
            if (myClass == 'active') {
                return false;
            }


        });

            /////////
            $(function () {
                $("".btndetails"").click(function () {
                    var url = $(this).attr('href');
                    var indextb = $(this).closest('tr').index();                    
                    var selectedInd = $(this).closest(""#datost"").find('tr').eq(indextb).find(""td:eq(0)"").text();
                    var name = $(this).closest(""#datost"").find('tr').eq(indextb).find(""td.mostrarnombre"").text();
                    //alert(selectedInd);                    
                    $(""#DialogDetails"").dialog({
                        title: 'Detalle '+name,
                        resizable: true,
                        width: 750,
                        position: ['center', 230],
                        show: { effect: 'drop', direction: 'up' },
                        modal: true,
                        draggable: true,
                        open: function (event, ui) {
                         $(this).load(url);
                        },
                        buttons: {
                            'Aceptar': function () {
                                
                                $(this).dialog('close');

                            }
                        }
                    });

                    return false;

                });
            });


            /////////
            $(function () {
                $("".btndelete"").click(function () {
                    //console.log('en btndelete page '+pageSelected);
                    var indextb = $(this).closest('tr').index();
                    path= " + @"""" + Url.Action("QuickIndex", Controler) + @"/""; 
                    var selectedInd = $(this).closest(""#datost"").find('tr').eq(indextb).find(""td:eq(0)"").text();                      
                    var name = $(this).closest(""#datost"").find('tr').eq(indextb).find(""td.mostrarnombre"").text();
                    //var valestado = $('#selectState').val();
                    var page = $('#selectPage').val();
                    var cestado=true;
                    $(""#textw"").empty();
                    var msj='¿Desea dar de baja el Registro:'+name+'?';                       
                    var msjsuccess='Se dio de baja el Registro: '+$.trim(name)+' del sistema';  
                    $(""#textw"").append(msj);
                     var workPageSelected;
                    if (typeof pageSelected === 'undefined') { workPageSelected = 1 }
                    else { workPageSelected = pageSelected }
                    //alert(selectedInd);                    
                    $(""#dialog-confirm"").dialog({
                        title: 'Eliminar',
                        resizable: true,
                        height: 190,
                        width: 400,
                        position: ['center', 230],
                        show: { effect: 'drop', direction: 'up' },
                        modal: true,
                        draggable: true,
                        buttons: {
                            'Aceptar': function () {
                                $.ajax({
                                    type: ""POST"",
                                    beforeSend: function () { $('#ProcesandoLoading').show(); }, 
                                    complete: function () { $('#ProcesandoLoading').hide(); },
                                    url: path,                                  
                                    data: {page: workPageSelected, pageSize: page, id: selectedInd, estado:valestado, cambioestado:cestado },
                                    success: function (data) {

                                       if (data.Success == 'False') {
                    $('#cargandoGrid').css('display', 'none');
                    $('#Buscador').css('display', 'inline');
                    ShowNotification(data.titulomsj, data.Message, data.tipomsj)
                   
                }                
                else {
                    $('#DatosTabla').empty();
                                        //$('#DatosTabla').html(data);
                                        //ReloadScript();                                        

                                        LoadData();
                                        //$('#DatosTabla').html(data);
                                        ReloadScript();
                                        $('#cargandoGrid').css('display', 'none');
                                        ShowNotification('Cambio de estado', msjsuccess, 'success');
                    
                }
                                        

                                    },
                                    error: function (xhr, ajaxOptions, thrownError) {
                                                    ShowNotification('Error', thrownError+''+xhr.status, 'error')
                
                                                }
                                });

                                $(this).dialog('close');

                            },
                            'Cancelar': function () {
                                $(this).dialog('close');
                            }
                        }
                    });

                    return false;

                });
            });

            //select row table
            $(""#DatosTabla tr"").click(function () {
                var selectedText = $(this).find('td:eq(2)').text();
                $('#DatosTabla tr').removeClass('SelectedRow');
                $(this).removeClass().addClass('SelectedRow');

            });


            /////////
            $(function () {
                $("".btnlockstatus"").click(function () {
                    var indextb = $(this).closest('tr').index();
                    path= " + @"""" + Url.Action("QuickIndex", Controler) + @"/""; 
                    var selectedInd = $(this).closest(""#datost"").find('tr').eq(indextb).find(""td:eq(0)"").text();                      
                    var name = $(this).closest(""#datost"").find('tr').eq(indextb).find(""td.mostrarnombre"").text();                    
                    //var valestado = $('#selectState').val();
                    var page = $('#selectPage').val();
                    var cestado=true;
                    $(""#textw"").empty();
                    var msj='¿Desea cambiar de estado el Registro:'+name+'?';   
                    var msjsuccess='Se cambio de estado el Registro: '+$.trim(name)+' del sistema';                    
                    //console.log('este es valor de name '+$.trim(name));
                    $(""#textw"").append(msj);
                    var workPageSelected;
                    if (typeof pageSelected === 'undefined') { workPageSelected = 1 }
                    else { workPageSelected = pageSelected }
                    //alert(selectedInd);                    
                    $(""#dialog-confirm"").dialog({
                        title: 'Cambio Estado',
                        resizable: true,
                        height: 190,
                        width: 400,
                        position: ['center', 230],
                        show: { effect: 'drop', direction: 'up' },
                        modal: true,
                        draggable: true,
                        buttons: {
                            'Aceptar': function () {
                                $.ajax({
                                    type: ""POST"",
                                    beforeSend: function () { $('#ProcesandoLoading').show(); }, 
                                    complete: function () { $('#ProcesandoLoading').hide(); },
                                    url: path,                                  
                                    data: {page: workPageSelected, pageSize: page, id: selectedInd, estado:valestado, cambioestado:cestado },
                                    success: function (data) {
                                                                if (data.Success == 'False') {
                                            $('#cargandoGrid').css('display', 'none');
                                            $('#Buscador').css('display', 'inline');                                            
                                            ShowNotification(data.titulomsj, data.Message, data.tipomsj)
                   
                                        }
                                        else {
                                            $('#DatosTabla').empty();
                                                                //$('#DatosTabla').html(data);
                                                                ReloadScript();                                        

                                                                LoadData();
                                                                //$('#DatosTabla').html(data);
                                                                ReloadScript();
                                                                $('#cargandoGrid').css('display', 'none');
                                                                ShowNotification('Cambio de estado', msjsuccess, 'success');
                    
                                        }
                                       
                                        

                                    },
                                    error: function (xhr, ajaxOptions, thrownError) {
                                                    ShowNotification('Error', thrownError+''+xhr.status, 'error')
                
                                                }
                                });

                                $(this).dialog('close');

                            },
                            'Cancelar': function () {
                                $(this).dialog('close');
                            }
                        }
                    });

                    return false;

                });
            });
            


                


            
             $('.btnLimpiar').click(function () {location.reload();});
             $('.wbtnLimpiar').click(function () {
            //alert('ok')
            //location.reload();
            $('input[type=text]').each(function () {
                $(this).val('');
            });
            $('.searchbox').find('select').each(function () {
                
                
                $(this).select2('val', '');
                //console.log(val);
            });

            $('#selectState').val(1);
            $('#selectState').selectric('init');
            $('#selectPage').val(1);
            $('#selectPage').selectric('init');
            var page = $('#selectPage').val();
            var valestado = $('#selectState').val();
            path= " + @"""" + Url.Action("QuickIndex", Controler) + @"/""; 
            $('#cargandoGrid').show();
            $('#cargandoGrid').css('display', 'inline');            
            //alert(valstate);
            $.ajax({
                type: 'POST',
                url: path, // Method , Controler
               
                data: { page: 1, pagesize: 15, estado: valestado },
            success: function (data) {               
                if (data.Success == 'False') {
                    $('#cargandoGrid').css('display', 'none');
                    $('#Buscador').css('display', 'inline');
                    ShowNotification('Advertencia', data.Message, 'notice')
                   
                }
                else {
                    $('#DatosTabla').html(data);
                    ReloadScript();
                    $('#cargandoGrid').css('display', 'none');
                    
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                ShowNotification('Error', thrownError + '' + xhr.status, 'error')
                $('#cargandoGrid').css('display', 'none');
            }

        });
            
        });

       
        

 



        //actualiza index
        function updateIndex(page, sid, cestado) {
             $('#cargandoGrid').show();
            $('#cargandoGrid').css('display', 'inline');
             path= " + @"""" + Url.Action("QuickIndex", Controler) + @"/""; 
             var valestado = $('#selectState').val();
             $.ajax({
                type: ""GET"",                
                url:path,
                data: { page: 1, pageSize: page, id:sid, estadoselect:valestado, cambioestado:cestado },
                success: function (data) {
                    if (data.Success == 'False') {
                    $('#cargandoGrid').css('display', 'none');
                    $('#Buscador').css('display', 'inline');
                    ShowNotification(data.titulomsj, data.Message, data.tipomsj)
                   
                }
                if (data.Success == 'True') {
                     $('#DatosTabla').html(data);
                    ReloadScript();
                    $('#cargandoGrid').css('display', 'none');
                    ShowNotification(data.titulomsj, data.Message, data.tipomsj)
                }
                else {
                    $('#DatosTabla').html(data);
                    ReloadScript();
                    $('#cargandoGrid').css('display', 'none');
                    
                }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                                                    ShowNotification('Error', thrownError+''+xhr.status, 'error')
                
                                                }
            });
        }
                
        }";
            return strScript;
        }

        public string BuildScriptPageValid()
        {
            string script = @"$(document).ready(function () {    
                                //valida la forma y pone el cargando
                                $('form').submit(function () {
                                if (!$(this).valid()) {
                                console.log('no es valido');
                                $('#ProcesandoLoading').hide();

                                return false;
                                }
                                else {
                                console.log('es valido');
                                $('#ProcesandoLoading').show();

                                return true;
                                }
                                });

                                //pone el el div de cargar
                                $('.btnCreate').click(function () {
                                $('#ProcesandoLoading').show();
                                });
                                });";
            return script;
        }
    }
}