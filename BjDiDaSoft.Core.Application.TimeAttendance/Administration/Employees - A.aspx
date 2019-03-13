<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="BjDiDaSoft.Core.Application.TimeAttendance.Administration.Employees" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
  <title></title>
  <link rel="stylesheet" type="text/css" href="~/Content/css/cupertino/jquery-ui-1.7.2.custom.css" />
  <link rel="stylesheet" type="text/css" href="~/Content/css/ui.jqgrid.css" />
  <link rel="stylesheet" type="text/css" href="~/Content/popup.css" />

  <%--<link rel="stylesheet" type="text/css" href="~/Content/css/login/demo.css" />
  <link rel="stylesheet" type="text/css" href="~/Content/css/login/style.css" />
  <link rel="stylesheet" type="text/css" href="~/Content/css/login/animate-custom.css" />--%>

  <script type="text/javascript" src="../Scripts/js/jquery-1.5.2.min.js"></script>
  <script type="text/javascript" src="../Scripts/js/grid.locale-es.js"></script>
  <script type="text/javascript" src="../Scripts/js/jquery.jqGrid.min.js"></script>
  <script type="text/javascript" src="../Scripts/js/grid.base.js"></script>
  <script type="text/javascript" src="../Scripts/js/grid.common.js"></script>
  <script type="text/javascript" src="../Scripts/js/grid.formedit.js"></script>
  <script type="text/javascript" src="../Scripts/js/jquery.fmatter.js"></script>
  <script type="text/javascript" src="../Scripts/js/jsonXml.js"></script>
  <script type="text/javascript" src="../Scripts/js/jquery.tablednd.js"></script>
  <script type="text/javascript" src="../Scripts/js/ui.multiselect.js"></script>
  <script type="text/javascript" src="../Scripts/js/grid.inlinedit.js"></script>
  <script type="text/javascript" src="../Scripts/js/jQDNR.js"></script>
  <script type="text/javascript" src="../Scripts/js/jqModal.js"></script>

  <script type="text/javascript" src="../Scripts/jquery.currency.js"></script>
  <script type="text/javascript" src="../Scripts/jquery.numeric.js"></script>
  <script type="text/javascript" src="../Scripts/jquery.ui.datepicker.js"></script>

  <%--<script src="../Scripts/jquery.bpopup-x.x.x.min.js"></script>--%>
  <script src="../Scripts/popup/jquery.bpopup.min.js"></script>

  <%--<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.21/jquery-ui.min.js"></script>--%>
  <script src="../Scripts/jquery-ui.min.js"></script>
        
  <script type="text/javascript">
    var $table = $('#grid');
    var lastSel;
    var colMode = [];

    var oEmployee = {
        Id: null,
        IdentificationNumber: null,
        Name: null,
        BirthDate: null,
        Weight: null,
        Operation: null
    };

    ////////////////////////////////////////////////
    ////////////// Funciones Globales //////////////
    ////////////////////////////////////////////////
    function confirmMessageDialog() {
        $("#confirm-dialog").dialog({
            modal: true,
            buttons: {
                "Sí": function () {
                    $(this).dialog("close");
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
    };


    function agregarBotonesGrid(id) {
        var botonEdicion = "<button title='Editar Obra Social' class='botonEditar' onclick=\"window.location.href='/ObraSocial/EditarObraSocial?obraSocialId=" + id + "'\"></button>";
        $("#grid").setRowData(id, { acciones: botonEdicion });
        $(".botonEditar").button({
            text: false, icons: {
                primary: "ui-icon-pencil"
            }
        });
    };

    function setDataFieldsEmpty() {
        $("#txtCedula").val('');
        $("#txtNombre").val("");
        $("#txtFecha").val("");
        $("#txtPeso").val('');
    };

    function setEmployee() {
        oEmployee.Id = "_empty";
        oEmployee.IdentificationNumber = $("#txtCedula").val();
        oEmployee.Name = $("#txtNombre").val();
        oEmployee.BirthDate = $("#txtFecha").val();
        oEmployee.Weight = $("#txtPeso").val();
        oEmployee.Operation = "add";

        return oEmployee;
    };

    
    function saveEmployee() {
        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            url: 'Services/ProcessData.asmx/SaveEmployee',
            data: "{'employeeModel':" + JSON.stringify(setEmployee()) + ' }',
            dataType: 'json',
            async: true,
            success: function (data, textStatus) {
                //$("#dlgSaveEmployee").dialog("open");
                setDataFieldsEmpty();
                $("#dialog").dialog('destroy');
                $("#grid").trigger("reloadGrid");
            },
            error: function (error) {

            }
        });
    };

    jQuery(document).ready(function () {

        //FORMATOS BUTTONS
        $("#btnSaveEmployee").button({ icons: { primary: "ui-icon-disk" } });
        $("#btnCancelEmployee").button({ icons: { primary: "ui-icon-closethick" } });

        //FORMATOS/#CARACTERES
        $("#txtCedula").attr("maxlength", 13);
        $("#txtNombre").attr("maxlength", 60);
        //$("#txtFecha").mask("99/99/9999");

        //FORMATOS/NÚMEROS-DECIMALES
        $("#txtCedula").numeric({ negative: false, decimal: "" });
        $("#txtPeso").numeric({ negative: false, decimal: "." });

        $("#txtFecha").datepicker({
            onSelect: function (date) {
                $("#_StartDate").val(date);

                /*
                if ($("#_EndDate").val() != "") {
                    if (compare_dates(date, $("#_EndDate").val())) {
                        $("#textMessage").text("@Resources.Global.DepositTicketSearch_DateTo");
                        $("#dlgMessage").dialog("open");
                        $("#_StartDate").val('');
                    } else {
                        $("#_StartDate").val(date);
                    }
                    $(".ui-datepicker a").removeAttr("href");
                }
                */
            }
        });

        //aca es modificacion
        $.ajax({
            dataType: "json",
            type: "post",
            url: "Services/LoadColumns.asmx/GetEmployeeColumns",
            data: "{}",
            contentType: "application/json;",
            async: false, //esto es requerido, de otra forma el jqgrid se cargaria antes que el grid
            success: function (data) {
                var employees = JSON.parse(data.d);
                $.each(employees, function (index, employee) {
                    colMode.push({ name: employee.Name, index: employee.index, label: employee.label, width: employee.width, align: employee.align, editable: employee.editable, edittype: employee.editType, editrules: { edithidden: true }, hidden: false });
                })

            } //or
        }),
        //acá
        $("#grid").jqGrid(
        {
            datatype: function () {
                $.ajax(
                {
                    url: "Services/LoadData.asmx/GetEmployees", //PageMethod
                    data: "{'pageSize':'" + $('#grid').getGridParam("rowNum") +
                          "','currentPage':'" + $('#grid').getGridParam("page") +
                          "','sortColumn':'" + $('#grid').getGridParam("sortname") +
                          "','sortOrder':'" + $('#grid').getGridParam("sortorder") + "'}", //PageMethod Parametros de entrada

                    dataType: "json",
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    complete: function (jsondata, stat) {
                        if (stat == "success")
                            jQuery("#grid")[0].addJSONData(JSON.parse(jsondata.responseText).d);
                        else
                            alert(JSON.parse(jsondata.responseText).Message);
                    }
                });
            },
            jsonReader: //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            {
                root: "Items",
                page: "CurrentPage",
                total: "PageCount",
                records: "RecordCount",
                repeatitems: true,
                cell: "Row",
                id: "EMPLOYEE_ID" //index of the column with the PK in it    
            },

            colModel: colMode,

            pager: "#pager", //Pager.
            loadtext: 'Cargando datos...',
            recordtext: "{0} - {1} de {2} elementos",
            emptyrecords: 'No hay resultados',
            pgtext: 'Pág: {0} de {1}', //Paging input control text format.
            rowNum: "10", // PageSize.
            rowList: [10, 20, 30], //Variable PageSize DropDownList. 
            viewrecords: true, //Show the RecordCount in the pager.
            multiselect: true,
            sortname: "EMPLOYEE_NAME", //Default SortColumn
            sortorder: "asc", //Default SortOrder.
            width: "1000",
            height: "230",
            caption: "Empleados",
            ondblClickRow: function (id) {
                gdCustomers.restoreRow(lastSel);
                gdCustomers.editRow(id, true);
                lastSel = id;
            },
            gridComplete: function () {
                var allRowsInGrid = $('#grid').jqGrid('getRowData');
                for (i = 0; i < allRowsInGrid.length; i++) {
                    pid = allRowsInGrid[i].EMPLOYEE_ID;
                    //agregarBotonesGrid(pid);
                    vHref = "<a href='#' onclick='openForm(" + pid + ")'>View</a>";
                    //vHref = "<a href='#' onclick='openForm(" + pid + ", " + vPhrase + ")'>View</a>";
                }
            }
        }).navGrid("#pager", { edit: true, add: true, search: true, del: true },
        { url: "Services/ProcessData.asmx/EditData", modal: true, top: 100, left: 400, height: 400, width: 500, closeAfterEdit: true },
        { url: "Services/ProcessData.asmx/EditData", modal: true, top: 100, left: 400, height: 400, width: 500, closeAfterAdd: true },
        { url: "Services/ProcessData.asmx/DeleteData" })
        .navButtonAdd('#pager', {
            caption: "", //Add
            title: "Agregar Empleado",
            buttonicon: "ui-icon ui-icon-person",
            onClickButton: function () {
                //alert("Adding Row");
                //$("#dialog").dialog();
                $("#dialog").dialog({
                    bgiframe: true,
                    autoOpen: true,
                    minHeight: 150,
                    width: 650,
                    closeOnEscape: false,
                    draggable: true,
                    resizable: false,
                    title: "Ingresar Empleado",
                    modal: true,
                    buttons: {
                        'Cancelar': function (param) {
                            setDataFieldsEmpty()
                            $(this).dialog('destroy');
                        },
                        'Grabar': function (param) {
                            saveEmployee();
                            //setDataFieldsEmpty();
                            //$(this).dialog('destroy');
                        }
                    }
                });
            },
            position: "last"
        })
        .navButtonAdd('#pager', {
            caption: "",
            title: "",
            buttonicon: "ui-icon ui-icon-trash",
            onClickButton: function () {

                var rowidd = jQuery("#grid").jqGrid('getGridParam', 'selrow');
                if (rowidd != null) {
                    var params = {};

                    confirmMessage("Esta seguro de eliminar el registro seleccionado", params);
                }
                else {
                    $("#textMessage").text("Seleccionar un registro");
                    $("#dlgMessage").dialog("open");
                }
            }
        })
        .navButtonAdd('#pager', {
            caption: "",
            title: "",
            buttonicon: "ui-icon-search",
            onClickButton: function () {
                jQuery("#grid").jqGrid('filterToolbar', { defaultSearch: "cn", stringResult: true, ignoreCase: true });
            }
        })
        .navButtonAdd('#pager', {
            caption: "", //Edit
            title: "Editar Empleado",
            buttonicon: "ui-icon ui-icon-pencil",
            onClickButton: function () {
                //alert("Deleting Row");
                var rowidd = jQuery("#grid").jqGrid('getGridParam', 'selrow');
                if (rowidd != null) {

                    $("#txtCedula").val(rowidd.EMPLOYEE_INUMBER);
                    $("#txtNombre").val(rowidd.EMPLOYEE_NAME);
                    $("#txtFecha").val(rowidd.EMPLOYEE_BIRTH_DATE);
                    $("#txtPeso").val(rowidd.EMPLOYEE_WEIGHT);

                    $("#dialog").dialog({
                        bgiframe: true,
                        autoOpen: true,
                        minHeight: 150,
                        width: 650,
                        closeOnEscape: false,
                        draggable: true,
                        resizable: false,
                        title: "Editar Empleado",
                        modal: true,
                        buttons: [
                            {
                                text: "Cancelar",
                                icons: {
                                    primary: "ui-icon ui-icon-contact"
                                },
                                click: function() {
                                    setDataFieldsEmpty()
                                    $(this).dialog('destroy');
                                }
                                // Uncommenting the following line would hide the text,
                                // resulting in the label being used as a tooltip
                                //showText: false
                            },
                            {
                                text: "Grabar",
                                icons: {
                                    primary: "ui-icon ui-icon-heart"
                                },
                                click: function() {
                                    updateEmployee();
                                    $(this).dialog("close");
                                }
                                // Uncommenting the following line would hide the text,
                                // resulting in the label being used as a tooltip
                                //showText: false
                            }
                        ]


                        /*
                        buttons: {
                            'Cancelar': function (param) {
                                setDataFieldsEmpty()
                                $(this).dialog('destroy');
                            },
                            'Grabar': function (param) {
                                saveEmployee();
                                //setDataFieldsEmpty();
                                //$(this).dialog('destroy');
                            }
                    }
                    */
                    });
                }
                else {
                    $("#textMessage").text("Seleccionar un registro");
                    $("#dlgMessage").dialog("open");
                }
            },
            position: "last"
        });
        

        jQuery.extend(jQuery.jgrid.edit, {
            ajaxEditOptions: { contentType: "application/json" },
            recreateForm: true,
            serializeEditData: function (postData) {
                return JSON.stringify(postData);
            }
        });

        jQuery.extend(jQuery.jgrid.del, {
            ajaxDelOptions: { contentType: "application/json" },
            serializeDelData: function (postData) {
                return JSON.stringify(postData);
            }
        });

        
        //// Evento click
        //$('#boton').click(function(e) {
        //    // Prevenir la acción por defecto
        //    e.preventDefault();
        //    // Se lanza el método bPopup 
        //    $('#elemento_a_mostrar').bPopup();
        //});
        
        //$('#boton').click(function () {
        //    $('#popup').bPopup();
        //});

        //$('#boton').click(function () {
        //    //confirmMessageDialog();
        //    $("#dialog").dialog();
        //});

        //$("#dialog").dialog();
        $("#dialog").hide();

        /*-------------------------------------------------------------------------------------------------------------------------*/
        //ACCIONES / EVENTOS                                                                                                       //
        /*-------------------------------------------------------------------------------------------------------------------------*/

        $("#btnSaveEmployee").click(function () {
            $.ajax({
                url: 'Services/ProcessData.asmx?op=SaveEmployee',
                type: 'POST',
                //data: { "employeeModel": setEmployee() },
                data: JSON.stringify(setEmployee()),
                dataType: 'json',
                //contentType: "application/json",
                success: function (data2) {
                    //$("#dlgSaveEmployee").dialog("open");
                    setDataFieldsEmpty();
                }
            });
        });

        $("#btnCancelEmployee").click(function () {
            setDataFieldsEmpty();
            //cleanObject();
            $("#dialog").dialog('destroy');
        });



        /*-------------------------------------------------------------------------------------------------------------------------*/
        // DEFINICION DE FUNCIONES                                                                                                 //
        /*-------------------------------------------------------------------------------------------------------------------------*/
        function confirmMessage(message, params) {
            /*Idem alerta*/
            var message = message.split('\n').join('<br>');

            /*Idem alerta*/
            var show = '<p style="font-size: 12px;"><span class="ui-icon ui-icon-alert" style="float:left; margin:0 7px 20px 0;"></span>' + message + '</p>';

            /*Idem alerta*/
            $("#div-dialogo").html(show);

            /*Enviamos los parámetros a params*/
            $("#div-dialogo").data("params", params);

            $("#div-dialogo").dialog({
                bgiframe: true,
                autoOpen: true,
                minHeight: 150,
                width: 350,
                closeOnEscape: false,
                draggable: true,
                resizable: false,
                title: "Eliminar Empleado",
                modal: true,
                buttons: {
                    'Si': function (param) {
                        $(this).dialog('destroy');

                        addCheckSelected()
                    },
                    'No': function (param) {
                        $(this).dialog('destroy');
                    }
                }
            });
        }//confirmMessage

    });
  </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <div align="center">
      <table>
        <tr>
          <td></td>
          <td>
            <%--<asp:Image ImageUrl="~/css/MCC11_Logo_Horizontal_2-color.jpg" ID="myImg" runat="server" />--%>
          </td>
        </tr>
        <tr>
          <td>
            <h1>E m p l e a d o s</h1>
            <%--<h3>Jason Ulloa</h3>--%>
          </td>
          <td>&nbsp;</td>
        </tr>
      </table>
      <table id="grid"></table>
      <div id="pager"></div>
      </div>
      
      <%--<div id="dialog1" title="Edición Empleado">
       <p>Contenido de la ventana</p>
       <table>
           <tr>
               <td><label for="cedula" class="uname" data-icon="u" >C&eacute;dula:</label></td>
               <td><input id="cedula" name="username" required="required" type="text" placeholder="cédula" runat="server" /></td>
           </tr>
           <tr>
               <td><label for="nombre" class="uname" data-icon="u" >Nombre:</label></td>
               <td><input id="nombre" name="username" required="required" type="text" placeholder="nombre" runat="server" /></td>
           </tr>
           <tr>
               <td><label for="fecha" class="uname" data-icon="u" >Fecha:</label></td>
               <td><input id="fecha" name="username" required="required" type="text" placeholder="fecha" runat="server" /></td>
           </tr>
           <tr>
               <td><label for="peso" class="uname" data-icon="u" >Peso:</label></td>
               <td><input id="peso" name="username" required="required" type="text" placeholder="peso" runat="server" /></td>
           </tr>
           <tr>
               <td><asp:Button ID="btnSave" runat="server" Text="Grabar" OnClick="btnSave_Click" /></td>
               <td><asp:Button ID="btnCancel" runat="server" Text="Cancelar" OnClick="btnCancel_Click" /></td>
           </tr>
       </table>
      </div>--%>

       <div id="dialog" title="Edición Empleado">
         <p>
          <label for="cedula" class="uname" data-icon="u" >C&eacute;dula:</label>
          <input id="txtCedula" name="txtCedula" required="required" type="text" placeholder="cédula" runat="server" />
         </p>
         <p>
          <label for="nombre" class="uname" data-icon="u" >Nombre:</label>
          <input id="txtNombre" name="txtNombre" required="required" type="text" placeholder="nombre" runat="server" />
         </p>
         <p>
          <label for="fecha" class="uname" data-icon="u" >Fecha:</label>
          <input id="txtFecha" name="txtFecha" required="required" type="text" placeholder="fecha" runat="server" />
         </p>
         <p>
           <label for="peso" class="uname" data-icon="u" >Peso:</label>
           <input id="txtPeso" name="txtPeso" required="required" type="text" placeholder="peso" runat="server" />
         </p>
         <%--
         <p class="login button">
          <asp:Button ID="btnSave" runat="server" Text="Grabar" OnClick="btnSave_Click" />
          <button id="btnSaveEmployee">Grabar</button>
         </p>
         <p class="login button">
          <asp:Button ID="btnCancel" runat="server" Text="Cancelar" OnClick="btnCancel_Click" />
          <button id="btnCancelEmployee">Cancelar</button>
         </p>
         --%>
      </div>

      <div id="confirm-dialog" title="Titulo dialog" style="display:none;">
       <p>Accepto.</p>
      </div>

      <!-- Botón -->
      <%--<button id="boton">Mostrar mensaje</button>--%>
      <%--<!-- Contenido a mostrar -->
      <div id="elemento_a_mostrar">El plugin bPopup funciona correctamente</div>
      
      <div id="popup" style="display: none;">
       <span class="button b-close"><span>X</span></span>
       If you can't get it up use<br />
       <span class="logo">bPopup</span>
      </div>--%>
      <%--<button onclick="confirmMessageDialog()">Abrir ventana emergente</button>--%>
      
      <div id="dlgMessage" title="" >
	   <p>
        <span class="ui-icon ui-icon-circle-check" style="float:left; margin:0 7px 50px 0;"></span>
        <b id="textMessage"></b>
	   </p>
      </div>

      <div id="div-dialogo"></div>

    </div>
    </form>
</body>
</html>
