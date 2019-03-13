<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PermissionTypes.aspx.cs" Inherits="BjDiDaSoft.Core.Application.TimeAttendance.Administration.PermissionTypes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link rel="stylesheet" type="text/css" href="~/Content/css/cupertino/jquery-ui-1.7.2.custom.css" />
  <link rel="stylesheet" type="text/css" href="~/Content/css/ui.jqgrid.css" />
  <link rel="stylesheet" type="text/css" href="~/Content/popup.css" />
  <link rel="stylesheet" type="text/css" href="~/Content/css/SiteForm.css" />

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
  <script src="../Scripts/jquery-ui.min.js"></script>

  <script type="text/javascript">
      var lastSel;
      var colMode = [];

      var oPermissionType = {
          Id: null,
          CompanyId: null,
          UserId: null,
          PermissionTypeDescription: null,
          PermissionTypeType: null,
          PermissionTypeDiscount: null,
          PermissionTypeStatus: null,
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

      function setDataFieldsEmpty() {
          $("#codigo").val('');
          $("#descripcion").val('');
          $("#chkStatus").attr('checked', false);
      };

      function setPermissionType(operation) {
          oPermissionType.Id = $("#codigo").val();
          oPermissionType.CompanyId = $("#companyId").val();
          oPermission.UserId = $("#userId").val();
          oPermissionType.Operation = operation;
          oPermissionType.PermissionTypeDescription = $("#descripcion").val();

          if ($("#chkStatus").is(':checked')) {
              oPermissionType.PermissionTypeStatus = "A";
          } else {
              oPermissionType.PermissionypeStatus = "I";
          }
          oPermission.PermissionTypeType = $('input:radio[name=permiso]:checked').val()

          return oPermissionType;
      };

      function savePermissionType(operation) {
          if (validateForm() == false) {
              return;
          }

          $.ajax({
              type: 'POST',
              contentType: "application/json; charset=utf-8",
              url: 'Services/ProcessData.asmx/SavePermissionType',
              data: "{'permissionTypeModel':" + JSON.stringify(setPermissionType(operation)) + ' }',
              dataType: 'json',
              async: true,
              success: function (data, textStatus) {
                  var message = JSON.parse(data.d);
                  if (message.ErrorCode == "0") {
                      $("#permissionTypeMessage").text(message.ErrorMessage);

                      $("#dlgSavePermissionType").dialog({
                          modal: true,
                          buttons: {
                              'Aceptar': function (param) {
                                  setDataFieldsEmpty()
                                  $(this).dialog('destroy');
                                  $("#dialog").dialog('destroy');
                                  $("#grid").trigger("reloadGrid");
                              }
                          }
                      });
                  }
                  else {
                      $("#textMessage").text(message.ErrorMessage);

                      $("#dlgMessage").dialog({
                          modal: true,
                          buttons: {
                              'Aceptar': function (param) {
                                  $("#dlgMessage").dialog('destroy');
                              }
                          }
                      });
                  }
              },
              error: function (error) {

              }
          });
      };

      function deletePermissionType(operation) {
          var rowidd = jQuery("#grid").jqGrid('getGridParam', 'selarrrow');
          if (rowidd != null) {
              for (var i in rowidd) {
                  var rowid = $("#grid").jqGrid("getRowData", rowidd[i]);

                  $.ajax({
                      type: 'POST',
                      contentType: "application/json; charset=utf-8",
                      url: 'Services/ProcessData.asmx/DeletePermissionType',
                      data: "{'permissionTypeId':" + rowid.PERMISSION_TYPE_ID + ",'companyId':" + rowid.COMPANY_ID + ",'userId':" + $("#userId").val() + ' }',
                      dataType: 'json',
                      async: true,
                      success: function (data, textStatus) {
                          var message = JSON.parse(data.d);
                          if (message.ErrorCode == "0") {
                              $("#PermissionTypeMessage").text(message.ErrorMessage);

                              $("#dlgSavePermissionType").dialog({
                                  modal: true,
                                  buttons: {
                                      'Aceptar': function (param) {
                                          setDataFieldsEmpty()
                                          $(this).dialog('destroy');
                                          $("#dialog").dialog('destroy');
                                          $("#grid").trigger("reloadGrid");
                                      }
                                  }
                              });
                          }
                          else {
                              $("#textMessage").text(message.ErrorMessage);

                              $("#dlgMessage").dialog({
                                  modal: true,
                                  buttons: {
                                      'Aceptar': function (param) {
                                          $("#dlgMessage").dialog('destroy');
                                      }
                                  }
                              });
                          }
                      },
                      error: function (error) {

                      }
                  });
              }
          }
      };

      function validateForm(operation) {
          // Código tipo de permiso
          if ($("#codigo").val() == "") {
              $("#dlgMessage").dialog();
              $("#textMessage").text("Ingresar código tipo permiso");
              $("#dlgMessage").dialog("open");
              return false;
          }
          // Descripción tipo permiso
          if ($("#descripcion").val() == "") {
              $("#dlgMessage").dialog();
              $("#textMessage").text("Ingresar descripción tipo permiso");
              $("#dlgMessage").dialog("open");
              return false;
          }

          return true;
      };

      jQuery(document).ready(function () {

          //acordeones
          $("#accPermissionTypeA").accordion({ collapsible: true, autoHeight: false });
          $("#accPermissionTypeB").accordion({ collapsible: true, autoHeight: false });

          $("#descripcion").attr("maxlength", 80);
          $("#codigo").attr("maxlength", 4);

          $("#codigo").numeric({ negative: false, decimal: "" });

          /*-------------------------------------------------------------------------------------------------------------------------*/
          //DEFINICIÓN GRIDS                                                                                                         //
          /*-------------------------------------------------------------------------------------------------------------------------*/

          //grid tipo permiso
          $.ajax({
              dataType: "json",
              type: "post",
              url: "Services/LoadColumns.asmx/GetPermissionTypeColumns",
              data: "{}",
              contentType: "application/json;",
              async: false, //esto es requerido, de otra forma el jqgrid se cargaria antes que el grid
              success: function (data) {
                  var permissionTypes = JSON.parse(data.d);
                  $.each(permissionTypes, function (index, permissionType) {
                      colMode.push({ name: permissionType.Name, index: permissionType.index, label: permissionType.label, width: permissionType.width, align: permissionType.align, editable: permissionType.editable, edittype: permissionType.editType, editrules: { edithidden: true }, hidden: permissionType.hidden });
                  })

              } //or
          }),
          //acá
          $("#grid").jqGrid(
          {
              datatype: function () {
                  $.ajax(
                  {
                      url: "Services/LoadData.asmx/GetPermissionTypes", //PageMethod
                      data: "{'pageSize':'" + $('#grid').getGridParam("rowNum") +
                            "','currentPage':'" + $('#grid').getGridParam("page") +
                            "','sortColumn':'" + $('#grid').getGridParam("sortname") +
                            "','sortOrder':'" + $('#grid').getGridParam("sortorder") +
                            "','companyId':'" + $('#companyId').val() + "'}",  //PageMethod Parámetros de entrada

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
                  id: "PERMISSION_TYPE_ID" //index of the column with the PK in it    
              },

              colModel: colMode,

              pager: "#pager", //Pager.
              loadtext: 'Cargando datos...',
              recordtext: "{0} - {1} de {2} elementos",
              emptyrecords: 'No hay resultados',
              pgtext: 'Pág: {0} de {1}', //Paging input control text format.
              rowNum: "15", // PageSize.
              rowList: [10, 15, 20, 30], //Variable PageSize DropDownList. 
              viewrecords: true, //Show the RecordCount in the pager.
              multiselect: true,
              sortname: "PERMISSION_TYPE_DESCRIPTION", //Default SortColumn
              sortorder: "asc", //Default SortOrder.
              width: "1000",
              height: "360",
              caption: "Tipo Permiso",
              ondblClickRow: function (id) {
                  gdCustomers.restoreRow(lastSel);
                  gdCustomers.editRow(id, true);
                  lastSel = id;
              },
              gridComplete: function () {
                  var allRowsInGrid = $('#grid').jqGrid('getRowData');
                  for (i = 0; i < allRowsInGrid.length; i++) {
                      pid = allRowsInGrid[i].CHARGE_ID;
                      //agregarBotonesGrid(pid);
                      vHref = "<a href='#' onclick='openForm(" + pid + ")'>View</a>";
                      //vHref = "<a href='#' onclick='openForm(" + pid + ", " + vPhrase + ")'>View</a>";
                  }
              }
          }).navGrid("#pager", { edit: false, add: false, search: false, del: false },
          { url: "Services/ProcessData.asmx/EditData", modal: true, top: 100, left: 400, height: 400, width: 500, closeAfterEdit: true },
          { url: "Services/ProcessData.asmx/EditData", modal: true, top: 100, left: 400, height: 400, width: 500, closeAfterAdd: true },
          { url: "Services/ProcessData.asmx/DeleteData" })
          .navButtonAdd('#pager', {
              caption: "", //Add
              title: "Agregar nuevo tipo permiso",
              buttonicon: "ui-icon ui-icon-plus",
              onClickButton: function () {
                  $("#codigo").attr('readonly', false);

                  $("#dialog").dialog({
                      bgiframe: true,
                      autoOpen: true,
                      minHeight: 150,
                      width: 600,
                      closeOnEscape: false,
                      draggable: true,
                      resizable: false,
                      title: "Ingresar Tipo Permiso",
                      modal: true,
                      buttons: {
                          'Cancelar': function (param) {
                              setDataFieldsEmpty()
                              $(this).dialog('destroy');
                          },
                          'Grabar': function (param) {
                              savePermissionType("add");
                              //setDataFieldsEmpty();
                              //$(this).dialog('destroy');
                          }
                      }
                  });
              },
              position: "last"
          })
          .navButtonAdd('#pager', {
              caption: "", //Edit
              title: "Editar tipo permiso",
              buttonicon: "ui-icon ui-icon-pencil",
              onClickButton: function () {

                  var rowidd = jQuery("#grid").jqGrid('getGridParam', 'selrow');
                  if (rowidd != null) {

                      var rowid = $("#grid").jqGrid("getRowData", rowidd);

                      $("#codigo").attr('readonly', true);

                      $("#codigo").val(rowid.PERMISSION_TYPE_ID);
                      $("#descripcion").val(rowid.PERMISSION_TYPE_DESCRIPTION);

                      if (rowid.PERMISSION_TYPE_STATUS == "A") {
                          $("#chkStatus").attr('checked', true);
                          $('#messageStatus').text("Activo");
                      }
                      else {
                          $("#chkStatus").attr('checked', false);
                          $('#messageStatus').text("Inactivo");
                      }

                      if (rowid.PERMISSION_TYPE_TYPE == "L") {
                          $("#permisoL").attr('checked', true);
                      }
                      else {
                          $("#permisoP").attr('checked', true);
                      }

                      $("#dialog").dialog({
                          bgiframe: true,
                          autoOpen: true,
                          minHeight: 150,
                          width: 600,
                          closeOnEscape: false,
                          draggable: true,
                          resizable: false,
                          title: "Editar Tipo Permiso",
                          modal: true,
                          buttons: [
                              {
                                  text: "Cancelar",
                                  icons: {
                                      primary: "ui-icon ui-icon-contact"
                                  },
                                  click: function () {
                                      setDataFieldsEmpty()
                                      $("#dialog").dialog('destroy');
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
                                  click: function () {
                                      saveContractType("edit");
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
                                      savePermissionType();
                                      //setDataFieldsEmpty();
                                      //$(this).dialog('destroy');
                                  }
                          }
                          */
                      });
                  }
                  else {
                      $("#dlgMessage").dialog();
                      $("#textMessage").text("Seleccionar un registro");
                      $("#dlgMessage").dialog("open");
                  }
              },
              position: "last"
          })
          .navButtonAdd('#pager', {
              caption: "",
              title: "Eliminar tipo permiso",
              buttonicon: "ui-icon ui-icon-trash",
              onClickButton: function () {

                  var rowidd = jQuery("#grid").jqGrid('getGridParam', 'selrow');
                  if (rowidd != null) {
                      var params = {};

                      confirmMessage("Esta seguro de eliminar el registro seleccionado", params);
                  }
                  else {
                      $("#dlgMessage").dialog();
                      $("#textMessage").text("Seleccionar un registro");
                      $("#dlgMessage").dialog("open");
                  }
              }
          })
          .navButtonAdd('#pager', {
              caption: "",
              title: "Buscar tipo permiso",
              buttonicon: "ui-icon-search",
              onClickButton: function () {
                  jQuery("#grid").jqGrid('filterToolbar', { defaultSearch: "cn", stringResult: true, ignoreCase: true });
              }
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

          $("#dialog").hide();
          $("#dlgMessage").hide();
          $("#dlgSavePermissionType").hide();
          $("#div-dialogo").hide();
          $("#confirm-dialog").hide();

          /*-------------------------------------------------------------------------------------------------------------------------*/
          //ACCIONES / EVENTOS                                                                                                       //
          /*-------------------------------------------------------------------------------------------------------------------------*/

          // CheckBox estado
          $('#chkStatus').change(function () {
              var checkeado = $(this).attr("checked");
              if (checkeado) {
                  $('#messageStatus').text("Activo");
              } else {
                  $('#messageStatus').text("Inactivo");
              }
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
                  title: "Eliminar Tipo Permiso",
                  modal: true,
                  buttons: {
                      'No': function (param) {
                          $(this).dialog('destroy');
                      },
                      'Si': function (param) {
                          $(this).dialog('destroy');

                          deletePermissionType('del');
                          $("#grid").trigger("reloadGrid");
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
          <td></td>
         </tr>
         <tr>
          <td>
           <h1>Tipo Permiso</h1>
          </td>
          <td>&nbsp;</td>
         </tr>
       </table>
       <table id="grid"></table>
       <div id="pager"></div>
      </div>
      
      <div id="dialog" title="Insertar Tipo Permiso">
       <div id="accPermissionTypeA" >
        <h3>
         <a style="color: Black"><b>Datos Tipo Permiso</b></a>
        </h3>
        <table>
         <tr>
          <td><label for="codigo">C&oacute;digo:</label></td>
          <td><input id="codigo" name="codigo" required="required" type="text" placeholder="C&oacute;digo" runat="server" style="width:35px;" />
              <input type="hidden" id="companyId" name="companyId" runat="server" />
              <input type="hidden" id="userId" name="userId" runat="server" />
          </td>
         </tr>
         <tr>
          <td><label for="descripcion">Descripci&oacute;n:</label></td>
          <td><input id="descripcion" name="descripcion" required="required" type="text" placeholder="Descripci&oacute;n tipo permiso" runat="server" style="width:400px;" /></td>
         </tr>
         <tr>
          <td><label for="acceso" data-icon="u">Permiso:</label></td>
          <td>
           <input type="radio" name="permiso" id="permisoL" value="L" style="font-size:8pt" /><b style="font-size:8pt">Laboral</b>
           <input type="radio" name="permiso" id="permisoP" value="P" checked style="font-size:8pt" /><b style="font-size:8pt">Personal</b>
          </td>
         </tr> 
         <tr>
          <td><label for="chkStatus">Estado:</label></td>
          <td><input id="chkStatus" name="chkStatus" type="checkbox" /><b id="messageStatus" style="font-size:8pt">Inactivo</b></td>
         </tr>
        </table>
       </div>
      </div>


      <div id="confirm-dialog" title="Titulo dialog" style="display:none;">
       <p>Accepto.</p>
      </div>
      
      <div id="dlgMessage" title="Aviso">
	   <p>
        <span class="ui-icon ui-icon-circle-check" style="float:left; margin:0 7px 50px 0;"></span>
        <b id="textMessage"></b>
	   </p>
      </div>

      <div id="div-dialogo"></div>
    
      <div id="dlgSavePermissionType" title="Grabar Tipo Permiso">
	   <p>
        <span class="ui-icon ui-icon-circle-check" style="float:left; margin:0 7px 50px 0;"></span>
        <b id="permissionTypeMessage"></b>
	   </p>
      </div>
    
    </div>
    </form>
</body>
</html>
