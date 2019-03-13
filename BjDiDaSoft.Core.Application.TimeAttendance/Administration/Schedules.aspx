<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Schedules.aspx.cs" Inherits="BjDiDaSoft.Core.Application.TimeAttendance.Administration.Schedules" %>

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

      var oSchedule = {
          Id: null,
          CompanyId: null,
          WorkdayId: null,
          UserId: null,
          ScheduleShortName: null,
          ScheduleDescription: null,
          ScheduleStartHour: null,
          ScheduleEndHour: null,
          ScheduleLunchHour: null,
          ScheduleIsNight: null,
          ScheduleAccess: null,
          ScheduleOuterZone: null,
          ScheduleInnerZone: null,
          ScheduleLunchTime: null,
          ScheduleOutputDelay: null, 
          ScheduleEntryDelay: null, 
          ScheduleStatus: null,
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
          $("#ddlJornadaLaboral").val('0');
          $("#abreviatura").val('');
          $("#descripcion").val('');
          $("#horainicio").val('');
          $("#horalunch").val('');
          $("#horafin").val('');
          $("#tiempoalmuerzo").val('');
          $("#demorarinicio").val('');
          $("#demorarfin").val('');
          $("#chkStatus").attr('checked', false);
          $("#chkIsNight").attr('checked', false);

          $("#accesoR").attr('checked', true);
      };

      function setSchedule(operation) {
          oSchedule.Id = $("#scheduleId").val();
          oSchedule.CompanyId = $("#companyId").val();
          oSchedule.UserId = $("#userId").val();
          oSchedule.Operation = operation;

          /*
          if ($("#acceso").is(':checked')) {
              oSchedule.ScheduleAccess = $("#acceso").val();
          } else {
              oSchedule.ScheduleAccess = $("#acceso").val();
          }
          */
          oSchedule.ScheduleAccess = $('input:radio[name=acceso]:checked').val()

          oSchedule.ScheduleDescription = $("#descripcion").val();
          oSchedule.ScheduleEndHour = $("#horafin").val();
          oSchedule.ScheduleEntryDelay = $("#demorarinicio").val();
          oSchedule.ScheduleInnerZone = $("#scheduleInnerZone").val();
          

          if ($("#chkIsNight").is(':checked')) {
              oSchedule.ScheduleIsNight = "S";
          } else {
              oSchedule.ScheduleIsNight = "N";
          }

          oSchedule.ScheduleLunchHour = $("#horalunch").val();
          oSchedule.ScheduleLunchTime = $("#tiempoalmuerzo").val();
          oSchedule.ScheduleOuterZone = $("#scheduleOuterZone").val();
          oSchedule.ScheduleOutputDelay = $("#demorarfin").val();
          oSchedule.ScheduleShortName = $("#abreviatura").val();
          oSchedule.ScheduleStartHour = $("#horainicio").val();

          if ($("#chkStatus").is(':checked')) {
              oSchedule.ScheduleStatus = "A";
          } else {
              oSchedule.ScheduleStatus = "I";
          }
          oSchedule.WorkdayId = $("#ddlJornadaLaboral").val();

          return oSchedule;
      };

      function saveSchedule(operation) {
          if (validateForm() == false) {
              return;
          }

          $.ajax({
              type: 'POST',
              contentType: "application/json; charset=utf-8",
              url: 'Services/ProcessData.asmx/SaveSchedule',
              data: "{'scheduleModel':" + JSON.stringify(setSchedule(operation)) + ' }',
              dataType: 'json',
              async: true,
              success: function (data, textStatus) {
                  var message = JSON.parse(data.d);
                  if (message.ErrorCode == "0") {
                      $("#scheduleMessage").text(message.ErrorMessage);

                      $("#dlgSaveSchedule").dialog({
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

      function deleteSchedule(operation) {
          var rowidd = jQuery("#grid").jqGrid('getGridParam', 'selarrrow');
          if (rowidd != null) {
              for (var i in rowidd) {
                  var rowid = $("#grid").jqGrid("getRowData", rowidd[i]);

                  $.ajax({
                      type: 'POST',
                      contentType: "application/json; charset=utf-8",
                      url: 'Services/ProcessData.asmx/DeleteSchedule',
                      data: "{'scheduleId':" + rowid.SCHEDULE_ID + ",'companyId':" + rowid.COMPANY_ID + ",'userId':" + $("#userId").val() + ' }',
                      dataType: 'json',
                      async: true,
                      success: function (data, textStatus) {
                          var message = JSON.parse(data.d);
                          if (message.ErrorCode == "0") {
                              $("#scheduleMessage").text(message.ErrorMessage);

                              $("#dlgSaveSchedule").dialog({
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
          // Jornada laboral
          if ($("#ddlJornadaLaboral").val() == "0") {
              $("#dlgMessage").dialog();
              $("#textMessage").text("Seleccionar la jornada laboral");
              $("#dlgMessage").dialog("open");
              return false;
          }
          // Abreviatura horario
          if ($("#abreviatura").val() == "") {
              $("#dlgMessage").dialog();
              $("#textMessage").text("Ingresar abreviatura horario");
              $("#dlgMessage").dialog("open");
              return false;
          }
          // Descripción horario
          if ($("#descripcion").val() == "") {
              $("#dlgMessage").dialog();
              $("#textMessage").text("Ingresar descripción de horario");
              $("#dlgMessage").dialog("open");
              return false;
          }
          // Hora inicio jornada laboral del horario
          if ($("#horainicio").val() == "") {
              $("#dlgMessage").dialog();
              $("#textMessage").text("Ingresar hora inicio jornada laboral del horario");
              $("#dlgMessage").dialog("open");
              return false;
          }
          else {
              var hour = $("#horainicio").val().split(":")[0];

              if (parseInt(hour) > 23) {
                  $("#horainicio").val("");
                  $("#dlgMessage").dialog();
                  $("#textMessage").text("Ingresar una hora correcta");
                  $("#dlgMessage").dialog("open");
                  return false;
              }

              var minute = $("#horainicio").val().split(":")[1];

              if (parseInt(minute) > 59) {
                  $("#horainicio").val("");
                  $("#dlgMessage").dialog();
                  $("#textMessage").text("Ingresar una hora correcta");
                  $("#dlgMessage").dialog("open");
                  return false;
              }
          }
          // Hora desayuno / almuerzo / merienda / cena jornada laboral del horario
          if ($("#horalunch").val() == "") {
              $("#dlgMessage").dialog();
              $("#textMessage").text("Ingresar hora almuerzo / merienda jornada laboral del horario");
              $("#dlgMessage").dialog("open");
              return false;
          }
          else {
              var hour = $("#horalunch").val().split(":")[0];

              if (parseInt(hour) > 23) {
                  $("#horalunch").val("");
                  $("#dlgMessage").dialog();
                  $("#textMessage").text("Ingresar una hora correcta");
                  $("#dlgMessage").dialog("open");
                  return false;
              }

              var minute = $("#horalunch").val().split(":")[1];

              if (parseInt(minute) > 59) {
                  $("#horalunch").val("");
                  $("#dlgMessage").dialog();
                  $("#textMessage").text("Ingresar una hora correcta");
                  $("#dlgMessage").dialog("open");
                  return false;
              }
          }
          // Hora fin jornada laboral del horario
          if ($("#horafin").val() == "") {
              $("#dlgMessage").dialog();
              $("#textMessage").text("Ingresar hora fin jornada laboral del horario");
              $("#dlgMessage").dialog("open");
              return false;
          }
          else {
              var hour = $("#horafin").val().split(":")[0];

              if (parseInt(hour) > 23) {
                  $("#horafin").val("");
                  $("#dlgMessage").dialog();
                  $("#textMessage").text("Ingresar una hora correcta");
                  $("#dlgMessage").dialog("open");
                  return false;
              }

              var minute = $("#horafin").val().split(":")[1];

              if (parseInt(minute) > 59) {
                  $("#horafin").val("");
                  $("#dlgMessage").dialog();
                  $("#textMessage").text("Ingresar una hora correcta");
                  $("#dlgMessage").dialog("open");
                  return false;
              }
          }
          // Tiempo que demora el desayuno / almuerzo / merienda / cena
          if ($("#tiempoalmuerzo").val() == "") {
              $("#dlgMessage").dialog();
              $("#textMessage").text("Ingresar el tiempo que dura el desayuno / almuerzo / merienda / cena en la jornada laboral");
              $("#dlgMessage").dialog("open");
              return false;
          }
          else {
              var time = $("#tiempoalmuerzo").val();

              if (parseInt(time) > 60) {
                  $("#tiempoalmuerzo").val("");
                  $("#dlgMessage").dialog();
                  $("#textMessage").text("El tiempo no debe superar los 60 minutos");
                  $("#dlgMessage").dialog("open");
                  return false;
              }
          }
          // Demorar inicio
          if ($("#demorarinicio").val() == "") {
              $("#dlgMessage").dialog();
              $("#textMessage").text("Ingresar los minutos en demorar inicio de entrada a laborar");
              $("#dlgMessage").dialog("open");
              return false;
          }
          else {
              var time = $("#demorarinicio").val();

              if (parseInt(time) > 29) {
                  $("#demorarinicio").val("");
                  $("#dlgMessage").dialog();
                  $("#textMessage").text("No debe exceder más de 29 minutos en demorar inicio");
                  $("#dlgMessage").dialog("open");
                  return false;
              }
          }
          // Demorar fin
          if ($("#demorarfin").val() == "") {
              $("#dlgMessage").dialog();
              $("#textMessage").text("Ingresar los minutos en demorar salida de laborar");
              $("#dlgMessage").dialog("open");
              return false;
          }
          else {
              var time = $("#demorarfin").val();

              if (parseInt(time) > 60) {
                  $("#demorarfin").val("");
                  $("#dlgMessage").dialog();
                  $("#textMessage").text("No debe exceder más de 29 minutos en demorar fin");
                  $("#dlgMessage").dialog("open");
                  return false;
              }
          }
          // Si es horario nocturno no se valida que hora inicio sea mayor a hora fin
          var startHour = $("#horainicio").val();
          var endHour = $("#horafin").val();
          var lunchHour = $("#horalunch").val();

          startHour = startHour.replace(':', '');
          endHour = endHour.replace(':', '');
          lunchHour = lunchHour.replace(':', '');

          if ($("#chkIsNight").is(':checked')) {

              if (parseInt(startHour) < parseInt(endHour)) {
                  $("#horainicio").val("");
                  $("#dlgMessage").dialog();
                  $("#textMessage").text("Hora inicio debe ser mayor a hora fin jornada laboral nocturna");
                  $("#dlgMessage").dialog("open");
                  return false;
              }

              if (parseInt(lunchHour) > parseInt(endHour)) {
                  $("#horalunch").val("");
                  $("#dlgMessage").dialog();
                  $("#textMessage").text("Hora almuerzo debe ser menor a hora fin jornada laboral nocturna");
                  $("#dlgMessage").dialog("open");
                  return false;
              }

          } else {

              if (parseInt(startHour) > parseInt(endHour)) {
                  $("#horainicio").val("");
                  $("#dlgMessage").dialog();
                  $("#textMessage").text("Hora inicio debe ser menor a hora fin jornada laboral");
                  $("#dlgMessage").dialog("open");
                  return false;
              }

              if (parseInt(startHour) > parseInt(lunchHour)) {
                  $("#horainicio").val("");
                  $("#dlgMessage").dialog();
                  $("#textMessage").text("Hora inicio debe ser menor a hora almuerzo jornada laboral");
                  $("#dlgMessage").dialog("open");
                  return false;
              }

              if (parseInt(lunchHour) > parseInt(endHour)) {
                  $("#horalunch").val("");
                  $("#dlgMessage").dialog();
                  $("#textMessage").text("Hora almuerzo debe ser menor a hora fin jornada laboral");
                  $("#dlgMessage").dialog("open");
                  return false;
              }
          }

          if (parseInt(startHour) == parseInt(endHour)) {
              $("#horainicio").val("");
              $("#dlgMessage").dialog();
              $("#textMessage").text("Hora inicio y fin de jornada laboral no deben ser iguales");
              $("#dlgMessage").dialog("open");
              return false;
          }
          if (parseInt(startHour) == parseInt(lunchHour)) {
              $("#horalunch").val("");
              $("#dlgMessage").dialog();
              $("#textMessage").text("Hora inicio y de almuerzo de jornada laboral no deben ser iguales");
              $("#dlgMessage").dialog("open");
              return false;
          }
          if (parseInt(lunchHour) == parseInt(endHour)) {
              $("#horalunch").val("");
              $("#dlgMessage").dialog();
              $("#textMessage").text("Hora almuerzo y fin jornada laboral no deben ser iguales");
              $("#dlgMessage").dialog("open");
              return false;
          }

          return true;
      };

      jQuery(document).ready(function () {

          //acordeones
          $("#accScheduleA").accordion({ collapsible: true, autoHeight: false });
          $("#accScheduleB").accordion({ collapsible: true, autoHeight: false });

          $("#abreviatura").attr("maxlength", 6);
          $("#descripcion").attr("maxlength", 60);
          $("#horainicio").attr("maxlength", 5);
          $("#horalunch").attr("maxlength", 5);
          $("#horafin").attr("maxlength", 5);
          $("#tiempoalmuerzo").attr("maxlength", 2);
          $("#demorarinicio").attr("maxlength", 2);
          $("#demorarfin").attr("maxlength", 2);

          $("#horainicio").numeric({ negative: false, decimal: ":" });
          $("#horalunch").numeric({ negative: false, decimal: ":" });
          $("#horafin").numeric({ negative: false, decimal: ":" });
          $("#tiempoalmuerzo").numeric({ negative: false, decimal: "" });
          $("#demorarinicio").numeric({ negative: false, decimal: "" });
          $("#demorarfin").numeric({ negative: false, decimal: "" });

          /*-------------------------------------------------------------------------------------------------------------------------*/
          //DEFINICIÓN GRIDS                                                                                                         //
          /*-------------------------------------------------------------------------------------------------------------------------*/

          //grid horarios
          $.ajax({
              dataType: "json",
              type: "post",
              url: "Services/LoadColumns.asmx/GetScheduleColumns",
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
                      url: "Services/LoadData.asmx/GetSchedules", //PageMethod
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
                  id: "SCHEDULE_ID" //index of the column with the PK in it    
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
              sortname: "SCHEDULE_DESCRIPTION", //Default SortColumn
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
                      pid = allRowsInGrid[i].SCHEDULE_ID;
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
              title: "Agregar nuevo horario",
              buttonicon: "ui-icon ui-icon-plus",
              onClickButton: function () {
                  $("#codigo").attr('readonly', false);
                  $("#scheduleId").val("0");

                  $("#dialog").dialog({
                      bgiframe: true,
                      autoOpen: true,
                      minHeight: 150,
                      width: 600,
                      closeOnEscape: false,
                      draggable: true,
                      resizable: false,
                      title: "Ingresar Horario",
                      modal: true,
                      buttons: {
                          'Cancelar': function (param) {
                              setDataFieldsEmpty()
                              $(this).dialog('destroy');
                          },
                          'Grabar': function (param) {
                              saveSchedule("add");
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
              title: "Editar horario",
              buttonicon: "ui-icon ui-icon-pencil",
              onClickButton: function () {

                  var rowidd = jQuery("#grid").jqGrid('getGridParam', 'selrow');
                  if (rowidd != null) {

                      var rowid = $("#grid").jqGrid("getRowData", rowidd);

                      $("#scheduleId").val(rowid.SCHEDULE_ID);
                      $("#ddlJornadaLaboral").val(rowid.WORKDAY_ID);
                      $("#abreviatura").val(rowid.SCHEDULE_SHORT_NAME);

                      $("#descripcion").val(rowid.SCHEDULE_DESCRIPTION);
                      //$("#horainicio").val(rowid.SCHEDULE_START_HOUR);
                      var myDate = new Date(rowid.SCHEDULE_START_HOUR);
                      //$("#horainicio").val(myDate.getHours() + ":" + myDate.getMinutes());
                      $("#horainicio").val(timeFormat(myDate.getHours(), myDate.getMinutes()));
                      $("#horainicio").val(rowid.START_HOUR);

                      //$("#horalunch").val(rowid.SCHEDULE_LUNCH_HOUR);
                      myDate = new Date(rowid.SCHEDULE_LUNCH_HOUR);
                      //$("#horalunch").val(myDate.getHours() + ":" + myDate.getMinutes());
                      $("#horalunch").val(timeFormat(myDate.getHours(), myDate.getMinutes()));
                      $("#horalunch").val(rowid.LUNCH_HOUR);

                      //$("#horafin").val(rowid.SCHEDULE_END_HOUR);
                      myDate = new Date(rowid.SCHEDULE_END_HOUR);
                      //$("#horafin").val(myDate.getHours() + ":" + myDate.getMinutes());
                      $("#horafin").val(timeFormat(myDate.getHours(), myDate.getMinutes()));
                      $("#horafin").val(rowid.END_HOUR);

                      $("#tiempoalmuerzo").val(rowid.SCHEDULE_LUNCH_TIME);

                      $("#demorarinicio").val(rowid.SCHEDULE_OUTPUT_DELAY);
                      //$("#codigo").attr('readonly', true);

                      $("#demorarfin").val(rowid.SCHEDULE_ENTRY_DELAY);

                      $("#scheduleInnerZone").val(rowid.SCHEDULE_INNER_ZONE);
                      $("#scheduleOuterZone").val(rowid.SCHEDULE_OUTER_ZONE);
                    
                      if (rowid.SCHEDULE_STATUS == "A") {
                          $("#chkStatus").attr('checked', true);
                          $('#messageStatus').text("Activo");
                      }
                      else {
                          $("#chkStatus").attr('checked', false);
                          $('#messageStatus').text("Inactivo");
                      }
                    
                      if (rowid.SCHEDULE_IS_NIGHT == "S") {
                          $("#chkIsNight").attr('checked', true);
                      }
                      else {
                          $("#chkIsNight").attr('checked', false);
                      }

                      if (rowid.SCHEDULE_ACCESS == "L") {
                          $("#accesoL").attr('checked', true);
                      }
                      else {
                          $("#accesoR").attr('checked', true);
                      }

                      $("#dialog").dialog({
                          bgiframe: true,
                          autoOpen: true,
                          minHeight: 150,
                          width: 600,
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
                                      saveSchedule("edit");
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
                                      saveSchedule();
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
              title: "Eliminar horario",
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
              title: "Buscar empleado",
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
          $("#dlgSaveSchedule").hide();
          $("#div-dialogo").hide();
          $("#confirm-dialog").hide();

          /*-------------------------------------------------------------------------------------------------------------------------*/
          //ACCIONES / EVENTOS                                                                                                       //
          /*-------------------------------------------------------------------------------------------------------------------------*/

          // DropDownList jornada laboral
          $('#<%=ddlJornadaLaboral.ClientID%>').append(
              $('<option></option>').val('0').html('Seleccione')
          );

          $.ajax({
              type: "POST",
              url: "Services/LoadData.asmx/BindWorkDay",
              data: {},
              dataType: "json",
              contentType: "application/json; charset=utf-8",
              success: function (data) {
                  var jsdata = JSON.parse(data.d);

                  $.each(jsdata, function (key, value) {
                      $('#<%=ddlJornadaLaboral.ClientID%>')
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
                  title: "Eliminar Horario",
                  modal: true,
                  buttons: {
                      'No': function (param) {
                          $(this).dialog('destroy');
                      },
                      'Si': function (param) {
                          $(this).dialog('destroy');

                          deleteSchedule('del');
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
           <h1>H o r a r i o s</h1>
          </td>
          <td>&nbsp;</td>
         </tr>
       </table>
       <table id="grid"></table>
       <div id="pager"></div>
      </div>
      
      <div id="dialog" title="Insertar Horario">
       <div id="accScheduleA" >
        <h3>
         <a style="color: Black"><b>Datos Horario</b></a>
        </h3>
        <table>
         <tr>
          <td><label for="jornadalaboral">Jornada Laboral:</label></td>
          <td><asp:DropDownList ID="ddlJornadaLaboral" runat="server" CssClass="dropdown" style="width:140px;height:18px;font-size:small" /></td>
         </tr>
         <tr>
          <td><label for="abreviatura">Abreviatura:</label></td>
          <td><input id="abreviatura" name="abreviatura" required="required" type="text" placeholder="Abreviatura" runat="server" style="width:70px;" /></td>
         </tr>
         <tr>
          <td><label for="descripcion">Descripci&oacute;n:</label></td>
          <td><input id="descripcion" name="descripcion" required="required" type="text" placeholder="Descripci&oacute;n horario" runat="server" style="width:400px;" /></td>
         </tr>
        </table>
       </div>
       <div id="accScheduleB" >
        <h3>
         <a style="color: Black"><b>Jornada Laboral</b></a>
        </h3>
        <table>
         <tr>
          <td><label for="horainicio" data-icon="u">Hora Inicio:</label></td>
          <td><input id="horainicio" name="horainicio" required="required" type="text" placeholder="Hora Inicio" runat="server" style="width:100px;" /></td>
          <td><label for="horalunch" data-icon="u">Hora Almuerzo / Merienda:</label></td>
          <td><input id="horalunch" name="horalunch" required="required" type="text" placeholder="Hora Almuerzo / Merienda" runat="server" style="width:100px;" /></td>
         </tr>  
         <tr>
          <td><label for="horafin" data-icon="u">Hora Fin:</label></td>
          <td>
           <input type="hidden" id="scheduleId" name="scheduleId" runat="server" />
           <input type="hidden" id="companyId" name="companyId" runat="server" />
           <input type="hidden" id="scheduleOuterZone" name="scheduleOuterZone" runat="server" />
           <input type="hidden" id="scheduleInnerZone" name="scheduleInnerZone" runat="server" />
           <input type="hidden" id="userId" name="userId" runat="server" />
           <input id="horafin" name="horafin" required="required" type="text" placeholder="Hora Fin" runat="server" style="width:100px;" />
          </td>
          <td><label for="tiempoalmuerzo" data-icon="u">Tiempo Almuerzo:</label></td>
          <td><input id="tiempoalmuerzo" name="tiempoalmuerzo" required="required" type="text" placeholder="Tiempo Almuerzo" runat="server" style="width:100px;" /><b style="font-size:8pt"> min</b></td>
         </tr>   
         <tr>
          <td><label for="demorarinicio">Demorar inicio:</label></td>
          <td><input id="demorarinicio" name="demorarinicio" required="required" type="text" placeholder="Demorar Inicio" runat="server" style="width:100px;" /><b style="font-size:8pt"> min</b></td>
          <td><label for="demorarfin">Demorar Fin:</label></td>
          <td><input id="demorarfin" name="demorarfin" required="required" type="text" placeholder="Demorar Fin" runat="server" style="width:100px;" /><b style="font-size:8pt"> min</b></td>
         </tr>
         <tr>
          <td><label for="chkStatus">Estado:</label></td>
          <td><input id="chkStatus" name="chkStatus" type="checkbox" /><b id="messageStatus" style="font-size:8pt">Inactivo</b></td>
          <td><label for="chkIsNight">Horario Nocturno:</label></td>
          <td><input id="chkIsNight" name="chkIsNight" type="checkbox" runat="server" />
          </td>
         </tr>
         <tr>
          <td><label for="acceso" data-icon="u">Acceso:</label></td>
          <td>
           <input type="radio" name="acceso" id="accesoL" value="L" style="font-size:8pt" /><b style="font-size:8pt">Libre</b>
           <input type="radio" name="acceso" id="accesoR" value="R" checked style="font-size:8pt" /><b style="font-size:8pt">Restringido</b>
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

      <div id="dlgSaveSchedule" title="Grabar Horario">
	   <p>
        <span class="ui-icon ui-icon-circle-check" style="float:left; margin:0 7px 50px 0;"></span>
        <b id="scheduleMessage"></b>
	   </p>
      </div>
       
    </div>
  </form>
</body>
</html>
