<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="TechEquip.Pages.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Регистрация</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>    
    <link rel="stylesheet" href="../Content/bootstrap.min.css" />
    <link rel="stylesheet" href="../Content/bootstrap-grid.min.css" />
    <link rel="icon" href="/images/favicon.ico" type="image/x-icon"/> 
    <link rel="shortcut icon" href="/images/favicon.ico" type="image/x-icon"/>
    <script src="http://bootstraptema.ru/plugins/2016/bsp/bootstrap-show-password.min.js"></script>
    <script src="../Scripts/jquery-3.4.1.min.js"></script>
    <style>
        body {
            margin: 0;
            padding: 0;
            background: #161a1e;
        }

        .card {           
            top: 15%;
            background: #0b0d0f;                                   
            position: absolute;           
            padding-top: 40px ;            
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

        .lblText {            
            color: white;
            font-size: 20px;
            margin: 5px;
        }

        .form-control {            
            border-radius: 10px;
            border: 0px;
            height: 35px;
            padding: 8px 12px;
            color: black;
        }

        h1 {
            color: white;
            font-size: 30px;
        }

        .password {
	        position: relative;
        }

        .btn {
            border-radius: 10px;
        }

        .Error {
            color: red;
            margin-left: 7px;
        }

        input[type="search"]::-webkit-search-cancel-button {
            position: relative;
            -webkit-appearance: none;
            height: 20px;
            width: 20px;
            background: url(/images/remove.svg) 0 0 no-repeat;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-xs-12 col-sm-10 col-md-10 col-lg-8 card ">
                    <img src="/images/avatar.png" class="avatar"/>
                    <div class="card-body">
                    <h1 class="text-center">Регистрация</h1>
                    <div class="form-row">
                        <div class="form-group col-lg-4">
                            <label class="lblText">Фамилия</label>
                            <asp:TextBox id="tbSurname" class="form-control" runat="server" type="search"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredSurname" runat="server" ErrorMessage="Введите фамилию" ControlToValidate="tbSurname"
                            CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator id="RegularSurname" ControlToValidate="tbSurname" ValidationExpression="^[а-яА-ЯёЁa-zA-Z]+$" ErrorMessage="Некорректный ввод символов" display="Dynamic" CssClass="Error" EnableClientScript="true" runat="server"/>
                        </div>                   
                        <div class="form-group col-lg-4">
                            <label class="lblText">Имя</label>
                            <asp:TextBox id="tbName" class="form-control" runat="server" type="search"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredName" runat="server" ErrorMessage="Введите имя" ControlToValidate="tbName"
                            CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator id="RegularName" ControlToValidate="tbName" ValidationExpression="^[а-яА-ЯёЁa-zA-Z]+$" ErrorMessage="Некорректный ввод символов" display="Dynamic" CssClass="Error" EnableClientScript="true"  runat="server"/>
                        </div>
                        <div class="form-group col-lg-4">
                            <label class="lblText">Отчество</label>
                            <asp:TextBox id="tbMiddleName" class="form-control" runat="server" type="search"></asp:TextBox>
                            <asp:RegularExpressionValidator id="RegularMiddleName" ControlToValidate="tbMiddleName" ValidationExpression="^[а-яА-ЯёЁa-zA-Z]+$" ErrorMessage="Некорректный ввод символов" display="Dynamic" CssClass="Error" EnableClientScript="true"  runat="server"/>
                        </div>
                    </div>
                    <div class="form-row">                                          
                        <div class="form-group col-lg-6">
                            <label class="lblText">Почта</label>
                            <asp:TextBox id="tbEmail" class="form-control" runat="server" type="email" OnTextChanged="tbEmail_TextChanged" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredEmail" runat="server" ErrorMessage="Введите почту" ControlToValidate="tbEmail"
                            CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularEmail" runat="server" ErrorMessage="Почта введена неверно" 
                            ControlToValidate="tbEmail" CssClass="Error" Display="Dynamic" ValidationExpression="^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$"></asp:RegularExpressionValidator>
                            <asp:Label runat="server" ID="lblEmailCheck" CssClass="Error" Text="Эта почта уже используется" Visible="false"></asp:Label>
                        </div>
                        <div class="form-group col-lg-6">
                            <label class="lblText">Номер телефона</label>
                            <asp:TextBox id="tbPhoneNumber" class="form-control" runat="server" maxlength="12" type="search"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredPhoneNumber" runat="server" ErrorMessage="Введите номер телефона" ControlToValidate="tbPhoneNumber"
                             CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:Label runat="server" ID="lblPhoneNumberCheck" CssClass="Error" Text="Этот номер телефона уже используется" Visible="false"></asp:Label>
                        </div>
                    </div>                    
                    <div class="form-row">
                        <div class="clearfix visible-md-block"></div>
                        <div class="form-group col-lg-4">
                            <label class="lblText">Логин</label>                          
                            <asp:TextBox id="tbLogin" class="form-control" maxlength="30" runat="server" type="search"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorLogin" runat="server" ErrorMessage="Введите логин" ControlToValidate="tbLogin"
                            CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularLogin" runat="server" ErrorMessage="Длина должна быть минимум 5 символов, которыми могут быть латинские буквы и цифры, первый символ обязательно буква" 
                            ControlToValidate="tbLogin" CssClass="Error" Display="Dynamic" ValidationExpression="^[a-zA-Z][a-zA-Z0-9-_\.]{4,30}$"></asp:RegularExpressionValidator>
                            <asp:Label runat="server" ID="lblLoginCheck" CssClass="Error" Text="Логин уже занят" Visible="false"></asp:Label>
                        </div>
                        <div class="form-group col-lg-4">
                            <label class="lblText">Пароль</label>
                            <div class="password">
                                <asp:TextBox id="tbPassword1" type="password" name="password" class="form-control" maxlength="30" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularPassword" runat="server" ErrorMessage="Длина пароля должна быть больше 6 символов, содеражть минимум одну ланитскую букву, одну цифру и один из символов !@#$%^&*_" 
                                ControlToValidate="tbPassword1" CssClass="Error" Display="Dynamic" ValidationExpression="(?=.*[0-9])(?=.*[!@#$%^&*_])((?=.*[a-z])|(?=.*[A-Z]))[0-9a-zA-Z!@#$%^&*_]{5,}"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredPassword" runat="server" ErrorMessage="Введите пароль" ControlToValidate="tbPassword1"
                                CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>                           
                        </div>                
                        <div class="form-group col-lg-4">
                            <label style="white-space: nowrap;" class="lblText">Подтверждение пароля</label>
                            <div class="password">                                
                                <asp:TextBox id="tbPasswordConfirm1" class="form-control" maxlength="30" runat="server" type="password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredConfirmPassword" runat="server" ErrorMessage="Введите пароль" ControlToValidate="tbPasswordConfirm1"
                                CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblPasswordConfirm" runat="server" CssClass="Error" Visible="false" Text="Пароли не совпадают" Display="Dynamic"></asp:Label>
                            </div>
                            <label style="color: white; float:right;"><input type="checkbox" style="margin: 5px" class="password-checkbox"/>Показать пароль</label>
                        </div>                        
                    </div>                                          
                    <div class="row justify-content-center">
                        <asp:Button ID ="btRegistration" class="btn btn-primary btn-block btn-lg col-lg-6" runat ="server" Text="Зарегистрироваться" OnClick="btRegistration_Click" />
                    </div>                                         
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="https://snipp.ru/cdn/jquery/2.1.1/jquery.min.js"></script>
    <script> 
        $('body').on('click', '.password-checkbox', function(){
            if ($(this).is(':checked')) {
            $(this).addClass('view');
            $('#tbPassword1').attr('type', 'text');
            $('#tbPasswordConfirm1').attr('type', 'text');
            } else {
            $(this).removeClass('view');
            $('#tbPassword1').attr('type', 'password');
            $('#tbPasswordConfirm1').attr('type', 'password');
	    }
        });
    </script>
    <script src="https://cdn.jsdelivr.net/npm/jquery@3.2.1/dist/jquery.min.js" type="text/javascript"></script>
    <script src="https://cdn.jsdelivr.net/npm/jquery.maskedinput@1.4.1/src/jquery.maskedinput.min.js" type="text/javascript"></script>
    <script>
        $.fn.setCursorPosition = function (pos) {
            if ($(this).get(0).setSelectionRange) {
                $(this).get(0).setSelectionRange(pos, pos);
            } else if ($(this).get(0).createTextRange) {
                var range = $(this).get(0).createTextRange();
                range.collapse(true);
                range.moveEnd('character', pos);
                range.moveStart('character', pos);
                range.select();
            }
        };
        $("#tbPhoneNumber").click(function () {
            $(this).setCursorPosition(2);
        }).mask("+79999999999", { autoclear: false });
        $("#tbPhoneNumber").mask("+79999999999", { autoclear: false });
    </script>
</body>
</html>

