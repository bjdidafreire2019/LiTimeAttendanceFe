<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpendingCenter.aspx.cs" Inherits="BjDiDaSoft.Core.Application.TimeAttendance.Administration.SpendingCenter" %>

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
  <script src="../Scripts/jquery-ui.min.js"></script>

  <script type="text/javascript">
      var lastSel;
      var colMode = [];

      var oSpendingCenter = {
          Id: null,
          CompanyId: null,
          UserId: null,
          SpendingCenterDescription: null,
          SpendingCenterStatus: null,
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
          $("#abreviatura").val('');
          $("#descripcion").val('');
          $("#chkStatus").attr('checked', false);
      };

      function setSpendingCenter(operation) {
          oSpendingCenter.Id = $("#codigo").val();
          oSpendingCenter.CompanyId = $("#companyId").val();
          oSpendingCenter.UserId = $("#userId").val();
          oSpendingCenter.Operation = operation;
          oSpendingCenter.SpendingCenterDescription = $("#descripcion").val();

          if ($("#chkStatus").is(':checked')) {
              oSpendingCenter.SpendingCenterStatus = "A";
          } else {
              oSpendingCenter.SpendingCenterStatus = "I";
          }

          return oSpendingCenter;
      };

      function saveSpendingCenter(operation) {
          if (validateForm() == false) {
              return;
          }

          $.ajax({
              type: 'POST',
              contentType: "application/json; charset=utf-8",
              url: 'Services/ProcessData.asmx/SaveSpendingCenter',
              data: "{'spendingCenterModel':" + JSON.stringify(setSpendingCenter(operation)) + ' }',
              dataType: 'json',
              async: true,
              success: function (data, textStatus) {
                  //$("#dlgSaveSchedule").dialog("open");
                  setDataFieldsEmpty();
                  $("#dialog").dialog('destroy');
                  $("#grid").trigger("reloadGrid");
              },
              error: function (error) {

              }
          });
      };

      function deleteSpendingCenter(operation) {
          var rowidd = jQuery("#grid").jqGrid('getGridParam', 'selarrrow');
          if (rowidd != null) {
              for (var i in rowidd) {
                  var rowid = $("#grid").jqGrid("getRowData", rowidd[i]);

                  $.ajax({
                      type: 'POST',
                      contentType: "application/json; charset=utf-8",
                      url: 'Services/ProcessData.asmx/DeleteSpendingCenter',
                      data: "{'spendingCenterId':" + rowid.SPENDING_CENTER_ID + ",'companyId':" + rowid.COMPANY_ID + ",'userId':" + $("#userId").val() + ' }',
                      dataType: 'json',
                      async: true,
                      success: function (data, textStatus) {
                          //$("#dlgSaveSpendingCenter").dialog("open");
                          setDataFieldsEmpty();
                          $("#dialog").dialog('destroy');
                          $("#grid").trigger("reloadGrid");
                      },
                      error: function (error) {

                      }
                  });
              }
          }
      };

      function validateForm(operation) {
          // Código centro de gasto
          if ($("#codigo").val() == "") {
              $("#dlgMessage").dialog();
              $("#textMessage").text("Ingresar código centro de gasto");
              $("#dlgMessage").dialog("open");
              return false;
          }
          // Abreviatura centro de gasto
          if ($("#abreviatura").val() == "") {
              $("#dlgMessage").dialog();
              $("#textMessage").text("Ingresar abreviatura centro de gasto");
              $("#dlgMessage").dialog("open");
              return false;
          }
          // Descripción centro de gasto
          if ($("#descripcion").val() == "") {
              $("#dlgMessage").dialog();
              $("#textMessage").text("Ingresar descripción centro de gasto");
              $("#dlgMessage").dialog("open");
              return false;
          }

          return true;
      };

      jQuery(document).ready(function () {

          //acordeones
          $("#accSpendingCenterA").accordion({ collapsible: true, autoHeight: false });
          $("#accSpendingCenterB").accordion({ collapsible: true, autoHeight: false });

          $("#abreviatura").attr("maxlength", 6);
          $("#descripcion").attr("maxlength", 60);
          $("#codigo").attr("maxlength", 6);

          $("#codigo").numeric({ negative: false, decimal: "" });

          /*-------------------------------------------------------------------------------------------------------------------------*/
          //DEFINICIÓN GRIDS                                                                                                         //
          /*-------------------------------------------------------------------------------------------------------------------------*/

          //grid horarios
          $.ajax({
              dataType: "json",
              type: "post",
              url: "Services/LoadColumns.asmx/GetSpendingCenterColumns",
              data: "{}",
              contentType: "application/json;",
              async: false, //esto es requerido, de otra forma el jqgrid se cargaria antes que el grid
              success: function (data) {
                  var schedules = JSON.parse(data.d);
                  $.each(schedules, function (index, schedule) {
                      colMode.push({ name: schedule.Name, index: schedule.index, label: schedule.label, width: schedule.width, align: schedule.align, editable: schedule.editable, edittype: schedule.editType, editrules: { edithidden: true }, hidden: schedule.hidden });
                  })

              } //or
          }),
          //acá
          $("#grid").jqGrid(
          {
              datatype: function () {
                  $.ajax(
                  {
                      url: "Services/LoadData.asmx/GetSpendingCenters", //PageMethod
                      data: "{'pageSize':'" + $('#grid').getGridParam("rowNum") +
                            "','currentPage':'" + $('#grid').getGridParam("page") +
                            "','sortColumn':'" + $('#grid').getGridParam("sortname") +
                            "','sortOrder':'" + $('#grid').getGridParam("sortorder") +
                            "','companyId':'" + $('#companyId').val() + "'}",  //PageMethod Parametros de entrada

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
                  id: "SPENDING_CENTER_ID" //index of the column with the PK in it    
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
              sortname: "SPENDING_CENTER_DESCRIPTION", //Default SortColumn
              sortorder: "asc", //Default SortOrder.
              width: "1000",
              height: "360",
              caption: "Horarios",
              ondblClickRow: function (id) {
                  gdCustomers.restoreRow(lastSel);
                  gdCustomers.editRow(id, true);
                  lastSel = id;
              },
              gridComplete: function () {
                  var allRowsInGrid = $('#grid').jqGrid('getRowData');
                  for (i = 0; i < allRowsInGrid.length; i++) {
                      pid = allRowsInGrid[i].SPENDING_CENTER_ID;
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
              title: "Agregar nuevo centro de gasto",
              buttonicon: "ui-icon ui-icon-plus",
              onClickButton: function () {
                  //$("#codigo").attr('readonly', false);
                  $("#scheduleId").val("0");

                  $("#dialog").dialog({
                      bgiframe: true,
                      autoOpen: true,
                      minHeight: 150,
                      width: 600,
                      closeOnEscape: false,
                      draggable: true,
                      resizable: false,
                      title: "Ingresar Centro de Gasto",
                      modal: true,
                      buttons: {
                          'Cancelar': function (param) {
                              setDataFieldsEmpty()
                              $(this).dialog('destroy');
                          },
                          'Grabar': function (param) {
                              saveSpendingCenter("add");
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
              title: "Editar centro de gasto",
              buttonicon: "ui-icon ui-icon-pencil",
              onClickButton: function () {

                  var rowidd = jQuery("#grid").jqGrid('getGridParam', 'selrow');
                  if (rowidd != null) {

                      var rowid = $("#grid").jqGrid("getRowData", rowidd);

                      $("#codigo").val(rowid.SPENDING_CENTER_ID);
                      //$("#abreviatura").val(rowid.SPENDING_CENTER_SHORT_NAME);
                      $("#descripcion").val(rowid.SPENDING_CENTER_DESCRIPTION);

                      if (rowid.SPENDING_CENTER_STATUS == "A") {
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
                          title: "Editar Centro de Gasto",
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
                                      saveSpendingCenter("edit");
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
                                      saveSpendingCenter();
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
              title: "Eliminar centro de gasto",
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
              title: "Buscar centro de gasto",
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

          $("#dialog_i").hide();
          $("#dialog").hide();
          $("#dlgMessage").hide();
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
                  title: "Eliminar Centro de Gasto",
                  modal: true,
                  buttons: {
                      'No': function (param) {
                          $(this).dialog('destroy');
                      },
                      'Si': function (param) {
                          $(this).dialog('destroy');

                          deleteSpendingCenter('del');
                          $("#grid").trigger("reloadGrid");
                      }
                  }
              });
          }//confirmMessage

          function timeFormat(hour, minute) {
              var time = "";
              if (hour.toString().length == 1) {
                  hour = "0" + hour.toString();
              }
              if (minute.toString().length == 1) {
                  minute = "0" + minute.toString();
              }
              time = hour.toString() + ":" + minute.toString();

              return time;
          }
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
           <h1>Centros de Gasto</h1>
          </td>
          <td>&nbsp;</td>
         </tr>
       </table>
       <table id="grid"></table>
       <div id="pager"></div>
      </div>
      
      <div id="dialog" title="Insertar Centro de Gasto">
       <div id="accSpendingCenterA" >
        <h3>
         <a style="color: Black"><b>Datos Centro de Gasto</b></a>
        </h3>
        <table>
         <tr>
          <td><label for="codigo">Código:</label></td>
          <td><input id="codigo" name="codigo" required="required" type="text" placeholder="C&oacute;digo" runat="server" style="width:70px;" />
              <input type="hidden" id="companyId" name="companyId" runat="server" />
              <input type="hidden" id="userId" name="userId" runat="server" />
          </td>
         </tr>
         <tr>
          <td><label for="abreviatura">Abreviatura:</label></td>
          <td><input id="abreviatura" name="abreviatura" required="required" type="text" placeholder="Abreviatura" runat="server" style="width:70px;" /></td>
         </tr>
         <tr>
          <td><label for="descripcion">Descripci&oacute;n:</label></td>
          <td><input id="descripcion" name="descripcion" required="required" type="text" placeholder="Descripci&oacute;n horario" runat="server" style="width:400px;" /></td>
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
       
    </div>
  </form>
</body>
</html>
