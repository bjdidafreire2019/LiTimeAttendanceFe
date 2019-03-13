<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Departments.aspx.cs" Inherits="BjDiDaSoft.Core.Application.TimeAttendance.Administration.Departments" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
  <script src="../Scripts/popup/jquery.bpopup.min.js"></script>

  <script src="../Scripts/jquery-ui.min.js"></script>

  <script type="text/javascript">
      var lastSel;
      var colMode = [];

      var oDepartment = {
          Id: null,
          CompanyId: null,
          SpendingCenterId: null,
          UserId: null,
          DepartmentDescription: null,
          DepartmentStatus: null,
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
          $("#ddlCentroGasto").val('-1');
          $("#codigo").val('');
          $("#descripcion").val('');
          $("#chkStatus").attr('checked', false);
      };

      function setDepartment(operation) {
          oDepartment.Id = $("#codigo").val();
          oDepartment.CompanyId = $("#companyId").val();
          oDepartment.UserId = $("#userId").val();
          oDepartment.Operation = operation;
          oDepartment.DepartmentDescription = $("#descripcion").val();

          if ($("#chkStatus").is(':checked')) {
              oDepartment.DepartmentStatus = "A";
          } else {
              oDepartment.DepartmentStatus = "I";
          }
          oDepartment.SpendingCenterId = $("#ddlCentroGasto").val();

          return oDepartment;
      };

      function saveDepartment(operation) {
          if (validateForm() == false) {
              return;
          }

          $.ajax({
              type: 'POST',
              contentType: "application/json; charset=utf-8",
              url: 'Services/ProcessData.asmx/SaveDepartment',
              data: "{'departmentModel':" + JSON.stringify(setDepartment(operation)) + ' }',
              dataType: 'json',
              async: true,
              success: function (data, textStatus) {
                  var message = JSON.parse(data.d);
                  if (message.ErrorCode == "0") {
                      $("#departmentMessage").text(message.ErrorMessage);

                      $("#dlgSaveDepartment").dialog({
                          modal: true,
                          buttons: {
                              'Acpetar': function (param) {
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
                            'Acpetar': function (param) {
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

      function deleteDepartment(operation) {
          var rowidd = jQuery("#grid").jqGrid('getGridParam', 'selarrrow');
          if (rowidd != null) {
              for (var i in rowidd) {
                  var rowid = $("#grid").jqGrid("getRowData", rowidd[i]);

                  $.ajax({
                      type: 'POST',
                      contentType: "application/json; charset=utf-8",
                      url: 'Services/ProcessData.asmx/DeleteDepartment',
                      data: "{'departmentId':" + rowid.DEPARTMENT_ID + ",'companyId':" + rowid.COMPANY_ID +
                            ",'spendingCenterId':" + rowid.SPENDING_CENTER_ID + ",'userId':" + $("#userId").val() + ' }',
                      dataType: 'json',
                      async: true,
                      success: function (data, textStatus) {
                          var message = JSON.parse(data.d);
                          if (message.ErrorCode == "0") {
                              $("#dlgSaveDepartment").dialog({
                                  modal: true,
                                  buttons: {
                                      'Acpetar': function (param) {
                                          setDataFieldsEmpty()
                                          $(this).dialog('destroy');
                                          $("#dialog").dialog('destroy');
                                          $("#grid").trigger("reloadGrid");
                                      }
                                  }
                              });
                              $("#departmentMessage").text(message.ErrorMessage);
                          }
                          else {
                              $("#textMessage").text(message.ErrorMessage);

                              $("#dlgMessage").dialog({
                                  modal: true,
                                  buttons: {
                                      'Acpetar': function (param) {
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
          // Centro de Gasto
          if ($("#ddlCentroGasto").val() == "-1") {
              $("#dlgMessage").dialog();
              $("#textMessage").text("Seleccionar el centro de gasto");
              $("#dlgMessage").dialog("open");
              return false;
          }
          // Código
          if ($("#codigo").val() == "") {
              $("#dlgMessage").dialog();
              $("#textMessage").text("Ingresar código de departamento");
              $("#dlgMessage").dialog("open");
              return false;
          }
          // Descripción departamento
          if ($("#descripcion").val() == "") {
              $("#dlgMessage").dialog();
              $("#textMessage").text("Ingresar descripción de departamento");
              $("#dlgMessage").dialog("open");
              return false;
          }

          return true;
      };

      jQuery(document).ready(function () {

          //acordeones
          $("#accDepartmentA").accordion({ collapsible: true, autoHeight: false });
          $("#accDepartmentB").accordion({ collapsible: true, autoHeight: false });

          $("#codigo").attr("maxlength", 6);
          $("#descripcion").attr("maxlength", 80);

          $("#codigo").numeric({ negative: false, decimal: "" });

          /*-------------------------------------------------------------------------------------------------------------------------*/
          //DEFINICIÓN GRIDS                                                                                                         //
          /*-------------------------------------------------------------------------------------------------------------------------*/

          //grid departamentos
          $.ajax({
              dataType: "json",
              type: "post",
              url: "Services/LoadColumns.asmx/GetDepartmentColumns",
              data: "{}",
              contentType: "application/json;",
              async: false, //esto es requerido, de otra forma el jqgrid se cargaria antes que el grid
              success: function (data) {
                  var departments = JSON.parse(data.d);
                  $.each(departments, function (index, department) {
                      colMode.push({ name: department.Name, index: department.index, label: department.label, width: department.width, align: department.align, editable: department.editable, edittype: department.editType, editrules: { edithidden: true }, hidden: department.hidden });
                  })

              } //or
          }),
          //acá
          $("#grid").jqGrid(
          {
              datatype: function () {
                  $.ajax(
                  {
                      url: "Services/LoadData.asmx/GetDepartments", //PageMethod
                      data: "{'pageSize':'" + $('#grid').getGridParam("rowNum") +
                            "','currentPage':'" + $('#grid').getGridParam("page") +
                            "','sortColumn':'" + $('#grid').getGridParam("sortname") +
                            "','sortOrder':'" + $('#grid').getGridParam("sortorder") +
                            "','companyId':'" + $('#companyId').val() + "'}", //PageMethod Parametros de entrada

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
                  id: "DEPARTMENT_ID" //index of the column with the PK in it    
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
              sortname: "DEPARTMENT_DESCRIPTION", //Default SortColumn
              sortorder: "asc", //Default SortOrder.
              width: "1000",
              height: "360",
              caption: "Departamentos",
              ondblClickRow: function (id) {
                  gdCustomers.restoreRow(lastSel);
                  gdCustomers.editRow(id, true);
                  lastSel = id;
              },
              gridComplete: function () {
                  var allRowsInGrid = $('#grid').jqGrid('getRowData');
                  for (i = 0; i < allRowsInGrid.length; i++) {
                      pid = allRowsInGrid[i].DEPARTMENT_ID;
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
              title: "Agregar nuevo departamento",
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
                      title: "Ingresar Departamento",
                      modal: true,
                      buttons: {
                          'Cancelar': function (param) {
                              setDataFieldsEmpty()
                              $(this).dialog('destroy');
                          },
                          'Grabar': function (param) {
                              saveDepartment("add");
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
              title: "Editar departamento",
              buttonicon: "ui-icon ui-icon-pencil",
              onClickButton: function () {

                  var rowidd = jQuery("#grid").jqGrid('getGridParam', 'selrow');
                  if (rowidd != null) {

                      var rowid = $("#grid").jqGrid("getRowData", rowidd);

                      $("#codigo").val(rowid.DEPARTMENT_ID);
                      $("#ddlCentroGasto").val(rowid.SPENDING_CENTER_ID);
                      $("#descripcion").val(rowid.DEPARTMENT_DESCRIPTION);

                      if (rowid.DEPARTMENT_STATUS == "A") {
                          $("#chkStatus").attr('checked', true);
                          $('#messageStatus').text("Activo");
                      }
                      else {
                          $("#chkStatus").attr('checked', false);
                          $('#messageStatus').text("Inactivo");
                      }

                      $("#dialog").dialog({
                          bgiframe: true,
                          autoOpen: true,
                          minHeight: 150,
                          width: 600,
                          closeOnEscape: false,
                          draggable: true,
                          resizable: false,
                          title: "Editar Departamento",
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
                                      saveDepartment("edit");
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
                                      saveDepartment();
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
              title: "Eliminar departamento",
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
              title: "Buscar departamento",
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
          $("#dlgSaveDepartment").hide();
          $("#div-dialogo").hide();
          $("#confirm-dialog").hide();

          /*-------------------------------------------------------------------------------------------------------------------------*/
          //ACCIONES / EVENTOS                                                                                                       //
          /*-------------------------------------------------------------------------------------------------------------------------*/

          // DropDownList jornada laboral
          $('#<%=ddlCentroGasto.ClientID%>').append(
              $('<option></option>').val('-1').html('Seleccione')
          );

          $.ajax({
              type: "POST",
              url: "Services/LoadData.asmx/BindSpendingCenter",
              data: {},
              dataType: "json",
              contentType: "application/json; charset=utf-8",
              success: function (data) {
                  var jsdata = JSON.parse(data.d);

                  $.each(jsdata, function (key, value) {
                      $('#<%=ddlCentroGasto.ClientID%>')
                        .append($("<option></option>").val(value.ValueMember).html(value.DisplayMember));
                  });
              },
              error: function (data) {
                  alert("error found");
              }
          });

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
                  title: "Eliminar Departamento",
                  modal: true,
                  buttons: {
                      'No': function (param) {
                          $(this).dialog('destroy');
                      },
                      'Si': function (param) {
                          $(this).dialog('destroy');

                          deleteDepartment('del');
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
           <h1>D e p a r t a m e n t o s</h1>
          </td>
          <td>&nbsp;</td>
         </tr>
       </table>
       <table id="grid"></table>
       <div id="pager"></div>
      </div>
      
      <div id="dialog" title="Insertar Departamento">
       <div id="accDepartmentA" >
        <h3>
         <a style="color: Black"><b>Datos Departamento</b></a>
        </h3>
        <table>
         <tr>
          <td><label for="centrogasto">Centro de Gasto:</label></td>
          <td><asp:DropDownList ID="ddlCentroGasto" runat="server" CssClass="dropdown" style="width:400px;height:18px;font-size:small" /></td>
         </tr>
         <tr>
          <td><label for="codigo">C&oacute;digo:</label></td>
          <td><input id="codigo" name="codigo" required="required" type="text" placeholder="C&oacute;digo" runat="server" style="width:70px;" /></td>
         </tr>
         <tr>
          <td><label for="descripcion">Descripci&oacute;n:</label></td>
          <td><input id="descripcion" name="descripcion" required="required" type="text" placeholder="Descripci&oacute;n departamento" runat="server" style="width:400px;" /></td>
         </tr>
         <tr>
          <td><label for="chkStatus">Estado:</label></td>
          <td><input id="chkStatus" name="chkStatus" type="checkbox" /><b id="messageStatus" style="font-size:8pt">Inactivo</b>
              <input type="hidden" id="companyId" name="companyId" runat="server" />
              <input type="hidden" id="userId" name="userId" runat="server" />
          </td>
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

      <div id="dlgSaveDepartment" title="Grabar Departamento">
	   <p>
        <span class="ui-icon ui-icon-circle-check" style="float:left; margin:0 7px 50px 0;"></span>
        <b id="departmentMessage"></b>
	   </p>
      </div>
               
    </div>
  </form>
</body>
</html>
