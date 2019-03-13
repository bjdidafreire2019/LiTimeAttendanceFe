<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginTA.aspx.cs" Inherits="BjDiDaSoft.Core.Application.TimeAttendance.Account.LoginTA" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="shortcut icon" href="../favicon.ico" /> 
    <link rel="stylesheet" type="text/css" href="~/Content/css/login/demo.css" />
    <link rel="stylesheet" type="text/css" href="~/Content/css/login/style.css" />
	<link rel="stylesheet" type="text/css" href="~/Content/css/login/animate-custom.css" />
</head>
<body>
    <div class="container">
    <!-- Codrops top bar -->
    <div class="codrops-top">
      <a href="">
        <strong>&laquo; Laboratorios LIFE</strong><%--Responsive Content Navigator--%>
      </a>
      <%--<span class="right">
        <a href=" http://tympanus.net/codrops/2012/03/27/login-and-registration-form-with-html5-and-css3/">
                        <strong>Back to the Codrops Article</strong>
        </a>
      </span>--%>
      <div class="clr"></div>
    </div><!--/ Codrops top bar -->
    <header>
      <h1>Laboratorios Life <span>Time Attendance</span></h1>
	  <%--<nav class="codrops-demos">
	    <span>Click <strong>"Join us"</strong> to see the form switch</span>
          <a href="index.html" class="current-demo">Demo 1</a>
					<a href="index2.html">Demo 2</a>
					<a href="index3.html">Demo 3</a>
	  </nav>--%>
    </header>
    <section>				
      <div id="container_demo" >
        <!-- hidden anchor to stop jump http://www.css3create.com/Astuce-Empecher-le-scroll-avec-l-utilisation-de-target#wrap4  -->
        <a class="hiddenanchor" id="toregister"></a>
        <a class="hiddenanchor" id="tologin"></a>
        <div id="wrapper">
          <div id="login" class="animate form">
            <form id="form1" runat="server" autocomplete="on"> 
              <h1>Log in</h1> 
              <p> 
                <label for="username" class="uname" data-icon="u" > Usuario </label>
                <input id="username" name="username" required="required" type="text" placeholder="usuario" runat="server" autocomplete="off" />
                <%--<asp:TextBox ID="txtUserName" runat="server" Width="200px"></asp:TextBox>--%>
              </p>
              <p> 
                <label for="password" class="youpasswd" data-icon="p"> Contraseña </label>
                <input id="password" name="password" required="required" type="password" placeholder="eg. X8df!90EO" runat="server" autocomplete="off" /> 
                <%--<asp:TextBox ID="txtUserPassword" runat="server" TextMode="Password" Width="200px" />--%>
              </p>
              <p class="login button"> 
                <asp:Button ID="btnLogin" runat="server" Text="Ingresar" OnClick="btnLogin_Click" />
			  </p>
              <p class="change_link">
                <asp:Label ID="MessageBox" runat="server" Text="" ForeColor="Red"></asp:Label>
			  </p>
            </form>
          </div>

          <div id="register" class="animate form">
            <form  action="mysuperscript.php" autocomplete="on"> 
              <h1> Sign up </h1> 
              <p> 
                <label for="usernamesignup" class="uname" data-icon="u">Your username</label>
                <input id="usernamesignup" name="usernamesignup" required="required" type="text" placeholder="mysuperusername690" />
              </p>
              <p> 
                <label for="emailsignup" class="youmail" data-icon="e" > Your email</label>
                <input id="emailsignup" name="emailsignup" required="required" type="email" placeholder="mysupermail@mail.com"/> 
              </p>
              <p> 
                <label for="passwordsignup" class="youpasswd" data-icon="p">Your password </label>
                <input id="passwordsignup" name="passwordsignup" required="required" type="password" placeholder="eg. X8df!90EO"/>
              </p>
              <p> 
                <label for="passwordsignup_confirm" class="youpasswd" data-icon="p">Please confirm your password </label>
                <input id="passwordsignup_confirm" name="passwordsignup_confirm" required="required" type="password" placeholder="eg. X8df!90EO"/>
              </p>
              <p class="signin button"> 
				<input type="submit" value="Sign up"/> 
			  </p>
              <p class="change_link">  
				Already a member ?
				<a href="#tologin" class="to_register"> Go and log in </a>
			  </p>
            </form>
          </div>
	    </div>
      </div>  
    </section>
  </div>
</body>
</html>
