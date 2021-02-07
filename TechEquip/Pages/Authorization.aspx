<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Authorization.aspx.cs" Inherits="TechEquip.Pages.Authorization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Авторизация</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>    
    <link rel="stylesheet" href="../Content/bootstrap.min.css" />
    <link rel="stylesheet" href="../Content/bootstrap-grid.min.css" />
    <link rel="icon" href="/images/favicon.ico" type="image/x-icon"/> 
    <link rel="shortcut icon" href="/images/favicon.ico" type="image/x-icon"/>
    <style>
        body{
            margin: 0;
            padding: 0;
            background: #161a1e;            
        }

        .login-box {
            width: 320px;
            height: 420px;
            background: rgba(0, 0, 0, 0.5);
            color: #fff;
            top: 50%;
            left: 50%;            
            position: absolute;
            transform: translate(-50%,-50%);
            box-sizing: border-box;
            padding: 70px 30px;            
            box-shadow:  0 10px 10px rgba(0,0,0,0.22);            
        }

        .avatar {
            width: 100px;
            height: 100px;
            border-radius: 50%;
            box-shadow: 0 -5px 5px -5px rgba(0, 0, 0, .5);
            position: absolute;
            top: -50px;
            left: calc(50% - 50px);
        }

        h1 {
            margin: 0;
            padding: 0 0 20px;
            text-align: center;
            font-size: 22px;
        }

        .lblText {
            margin: 0;
            margin-left: 3px;
            padding: 0;
            font-weight: bold;
        }        

        .login-box .loginform {
            width: 100%;
            margin-bottom: 20px;
        }

        .login-box .loginform:focus {
           border-bottom-color: #007bff;
        }

        .login-box .loginform {
            border: none;
            border-bottom: 1px solid #fff;
            background: transparent;
            outline: none;
            height: 40px;
            color: #fff;
            font-size: 16px;
        }

        .container-fluid {
            text-align: center;
        }

        a {            
            font-size: 18px;
            color: #fff;            
        }
        
        .password {
	        position: relative;
        }
        
        .password-control {
	        position: absolute;
	        top: 11px;
	        right: 6px;
	        display: inline-block;
	        width: 20px;
	        height: 20px;
	        background: url(/images/view.svg) 0 0 no-repeat;
        }
        .password-control.view {
	        background: url(/images/no-view.svg) 0 0 no-repeat;
        }
        .Error {
            color: red;
        }
    </style>
</head>
<body>           
    <div class="login-box">
        <img src="/images/avatar.png" class="avatar"/>
        <h1>Авторизация</h1>
        <form id="form1" runat="server">
            <asp:Label runat="server" ID="lblAuthorization" Text="Неверный логин или пароль" Visible="false" CssClass="Error"></asp:Label>
            <div class="form-group" >
                <label class="lblText">Логин</label> 
                <asp:TextBox ID="tbLogin1" class="loginform" name="login" Style="margin-bottom: 5px;" runat="server" maxlength="50" placeholder="Введите логин" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredLogin" runat="server" ErrorMessage="Введите логин"
                        Display="Dynamic" ControlToValidate="tbLogin1" CssClass="Error"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label class="lblText">Пароль</label>  
                <div class="password">
                    <asp:TextBox ID="tbPassword1" class="loginform" Style="margin-bottom: 5px;" type="password" name="password" runat="server" maxlength="50" placeholder="Введите пароль"></asp:TextBox>
                    <a href="#" class="password-control"></a>
                    <asp:RequiredFieldValidator ID="RequiredPassword" runat="server"  ErrorMessage="Введите пароль"
                        Display="Dynamic" ControlToValidate="tbPassword1" CssClass="Error"></asp:RequiredFieldValidator>
                </div>
            </div>           
            <asp:Button ID="btSubmit" class="btn btn-primary btn-block btn-lg" type="reset" runat="server" Text="Вход" OnClick="btSubmit_Click" />
            <div class="container-fluid mt-2">
                <a href="Registration.aspx">Регистрация</a>
            </div>
        </form>
    </div>
    <script src="https://snipp.ru/cdn/jquery/2.1.1/jquery.min.js"></script>
    <script>
        $('body').on('click', '.password-control', function () {
            if ($('#tbPassword1').attr('type') == 'password') {
                $(this).addClass('view');
                $('#tbPassword1').attr('type', 'text');
            } else {
                $(this).removeClass('view');
                $('#tbPassword1').attr('type', 'password');
            }
            return false;
        });
    </script>
</body>
</html>
