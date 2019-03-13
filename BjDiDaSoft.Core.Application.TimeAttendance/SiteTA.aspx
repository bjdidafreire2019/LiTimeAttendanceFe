<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteTA.aspx.cs" Inherits="BjDiDaSoft.Core.Application.TimeAttendance.SiteTA" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
  <title></title>
  <link rel="stylesheet" href="~/Content/menu.css" type="text/css" media="screen" />
  <!--[if IE 6]>
  <style>
    body {behavior: url("csshover3.htc");}
    #menu li .drop {background:url("img/drop.gif") no-repeat right 8px; 
  </style>
  <![endif]-->
  <script type="text/javascript" src="../Scripts/js/jquery-1.5.2.min.js"></script>
  <script type="text/javascript" src="../Scripts/jquery-1.10.2.min.js"></script>
  <script type="text/javascript">
    /*
    $().ready(function() {
        $(document).everyTime(3000, function() {
            $.ajax({
                type: "POST",
                url: "ValidateSession.aspx/KeepActiveSession",
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: VerifySessionState,
                error: function(XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus + ": " + XMLHttpRequest.responseText);
                }
            });
        });
    });
      
    var cantValidaciones = 0;
  
    function VerifySessionState(result) {
        if (result.d) {
            $("#EstadoSession").text("activo");
        }
        else
            $("#EstadoSession").text("expiro");
 
        $("#cantValidaciones").text(cantValidaciones);
        cantValidaciones++;
    }
      
    function SessionAbandon() {
        $.ajax({
            type: "POST",
            url: "ValidateSession.aspx/SessionAbandon",
            data: {},
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            error: function(XMLHttpRequest, textStatus, errorThrown) {
                alert(textStatus + ": " + XMLHttpRequest.responseText);
            }
        });
    }
    */

</script>
</head>
<body>
    <form id="form1" runat="server">
    <ul id="menu">
  <li class="menu_left"><a href="#" class="drop">Administraci&oacute;n</a>
     
   <div class="dropdown_2columns">
    <div class="col_2">
     <h2>Administraci&oacute;n</h2>
     <ul class="simple">
      <li><a href="Administration/Charges.aspx" target="main">Cargos</a></li>
      <li><a href="Administration/SpendingCenter.aspx" target="main">Centro de Gasto</a></li>
      <li><a href="Administration/Departments.aspx" target="main">Departamentos</a></li>
      <li><a href="Administration/Companies.aspx" target="main">Empresas</a></li>
      <li><a href="Administration/Holidays.aspx" target="main">Feriados</a></li>
      <li><a href="Administration/Schedules.aspx" target="main">Horarios</a></li>
      <li><a href="Administration/Workday.aspx" target="main">Jornada Laboral</a></li>
      <li><a href="Administration/Readers.aspx" target="main">Lectores</a></li>
      <li><a href="Administration/Parameters.aspx" target="main">Par&aacute;metros</a></li>
      <li><a href="Administration/ContractTypes.aspx" target="main">Tipo Contrato</a></li>
      <li><a href="Administration/PermissionTypes.aspx" target="main">Tipo Permiso</a></li>
      <li><a href="Account/LoginTA.aspx">Salir</a></li>
     </ul>   
    </div>
   </div>
         
  </li>
  
  <li class="menu_left"><a href="#" class="drop">Usuarios / Perfiles</a>
     
   <div class="dropdown_2columns">
    <div class="col_2">
     <h2>Usuarios / Perfiles</h2>
     <ul class="simple">
      <li><a href="Options.aspx" target="main">Opciones</a></li>
      <li><a href="ProfileOptions.aspx" target="main">Opciones x Perfil</a></li>
      <li><a href="Users.aspx" target="main">Usuarios</a></li>
     </ul>   
    </div>
   </div>
         
  </li>

  <li class="menu_left"><a href="#" class="drop">Personal</a>
     
   <div class="dropdown_2columns">
    <div class="col_2">
     <h2>Personal</h2>
     <ul class="simple">
      <li><a href="ChangeTime.aspx" target="main">Cambio Jornada</a></li>
      <li><a href="Administration/Employees.aspx" target="main">Empleados</a></li>
      <li><a href="EmployeeHolidays.aspx" target="main">Feriados x Empleado</a></li>
      <li><a href="Overtimes.aspx" target="main">Partes / Permisos</a></li>
      <li><a href="Transfers.aspx" target="main">Transferencias</a></li>
     </ul>   
    </div>
   </div>
         
  </li>
  
  <li class="menu_left"><a href="#" class="drop">Procesos</a>
     
   <div class="dropdown_2columns">
    <div class="col_2">
     <h2>Procesos</h2>
     <ul class="simple">
      <li><a href="EndOfDay.aspx" target="main">Fin de D&iacute;a</a></li>
      <li><a href="OnLineTransactions.aspx" target="main">Transacciones en L&iacute;nea</a></li>
     </ul>   
    </div>
   </div>
         
  </li>
  
  <li><a href="#" class="drop">Reportes</a><!-- Begin 4 columns Item -->
     
   <div class="dropdown_4columns"><!-- Begin 4 columns container -->
    <div class="col_4">
     <h2>Reportes</h2>
    </div>
    <div class="col_2">
     <!-- <h3>Some Links</h3> -->
     <ul>
      <li><a href="AssistanceReport.aspx" target="main">Asistencia</a></li>
      <li><a href="AuditReport.aspx" target="main">Auditor&iacute;a</a></li>
      <li><a href="AuthOvertimeReport.aspx" target="main">Autorizaci&oacute;n Trabajo Horas Suplementarias</a></li>
      <li><a href="CoffeShopReport.aspx" target="main">Cafeter&iacute;a</a></li>
      <li><a href="ChangeTimeReport.aspx" target="main">Cambio de Jornada</a></li>
      <li><a href="AuditChangeTimeReport.aspx" target="main">Creaci&oacute;n / Modificaci&oacute;n Cambio Jornada</a></li>
      <li><a href="AuditChangePartReport.aspx" target="main">Creaci&oacute;n / Modificaci&oacute;n Partes / Permisos</a></li>
      <li><a href="AuditChangeTransferReport.aspx" target="main">Creaci&oacute;n / Modificaci&oacute;n Transferencias</a></li>
      <li><a href="HolidaysReport.aspx">D&iacute;as Feriados</a></li>
      <li><a href="DepartmentScheduleReport.aspx" target="main">Horarios por Departamento</a></li>
     </ul>   
    </div>
    <div class="col_2">
     <!-- <h3>Other Stuff</h3> -->
     <ul>
      <li><a href="OvertimeReport.aspx" target="main">Horas Extras</a></li>
      <li><a href="AbsenceReport.aspx" target="main">Inasistencia</a></li>
      <li><a href="FailureLunchReport.aspx" target="main">Incumplimiento Lunch</a></li>
      <li><a href="IrregularitiesReport.aspx" target="main">Novedades / Irregularidades</a></li>
      <li><a href="PermissionReport.aspx" target="main">Partes / Permisos</a></li>
      <li><a href="PersonalReport.aspx" target="main">Personal</a></li>
      <li><a href="TransactionsReport.aspx" target="main">Transacciones</a></li>
      <li><a href="TransferReport.aspx" target="main">Transferencias</a></li>
      <li><a href="OfficialLocationReport.aspx" target="main">Ubicaci&oacute;n Funcionario</a></li>
      <li><a href="#">More...</a></li>
     </ul>   
    </div>
   </div><!-- End 4 columns container -->
     
  </li><!-- End 4 columns Item -->
 
 </ul>
 
 <div id="center">
  <!-- <iframe id="main" name="main" src="Home.aspx" frameborder="0" height="90%" scrolling="no" width="100%"></iframe> -->
  <iframe id="main" name="main" src="Home.aspx" frameborder="0" scrolling="no" height="600px" width="100%"></iframe>
 </div> 
    </form>
</body>
</html>
