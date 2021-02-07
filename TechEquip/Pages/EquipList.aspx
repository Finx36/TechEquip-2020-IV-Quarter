<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EquipList.aspx.cs" Inherits="TechEquip.Pages.EquipList" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Список оборудования</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="../Content/bootstrap.min.css" />
    <link rel="stylesheet" href="../Content/bootstrap-grid.min.css" />
    <link rel="icon" href="/images/favicon.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="/images/favicon.ico" type="image/x-icon" />
    <link rel="stylesheet" href="../Content/css/ExitBt.css" />
    <link rel="stylesheet" href="../Content/css/FilterBut.css" />
    <link rel="stylesheet" href="../Content/css/BasePages.css" />
    <link rel="stylesheet" href="../Content/css/EmailBt.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css" /> 
    <link rel="stylesheet" href="../Content/css/select2.min.css" />
    <link rel="stylesheet" href="../Content/css/select2.css" />
    <link rel="stylesheet" href="../Content/select2-bootstrap.css" />
    <script src="../scripts/jquery-3.4.1.min.js"></script>
    <script src="../scripts/jquery-3.4.1.js"></script>
    <script type="text/javascript">
        function test(e) {
            var keynum;
            if (window.event) // IE
            {
                keynum = e.keyCode
            }
            else if (e.which) // webkit
            {
                keynum = e.which
            }
            if (keynum == 13) __doPostback('btSearch', '');
        }
    </script>
    <script type="text/javascript">
        $("#tbSearch").keyup(function (event) {
            if (event.keyCode == 13) {
                __doPostback('btSearch', '');
            }
        });
    </script>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        //Иннициализация tooltip
        $(document).ready(function () {
            $('.tooltips').tooltip();
        });
    </script>
    <script type="text/javascript">
        function isDelete() {
            return confirm("Вы уверенны, что хотите удалить выбранную запись?");
        }
    </script>
    <style>
        .ddl1 {
            background: #9bc7de;
            outline: none;
            cursor: pointer;
        }

        .Error {
            color: red;
            margin-left: 7px;
        }

        .kabum {
            border: 1px solid gray;
        }

        .txtLabel {
            color: black;
            margin: 5px;
            font-size: 20px;
            font-family: 'Century Gothic';
        }

        #tbMessage {
            min-height: 73px;
            resize: none;
            max-height: 350px;
        }

        .contact i {
            font-size: 22px;
        }

        .select2-container .select2-selection--single{
            height: 46px !important;
        }

        @media screen and (min-width: 1200px) {
            .select2-container .select2-selection--single {
                width: 350px !important;
            }
        }

        @media screen and (min-width: 992px) and (max-width: 1200px)  {
            .select2-container .select2-selection--single {
                width: 290px !important;
            }
        }

        @media screen and (max-width: 991px) {
            .select2-container .select2-selection--single {
                width: 320px !important;
            }
        }

        @media screen and (max-width: 767px) {
            .select2-container .select2-selection--single {
                width: 100% !important;
            }
        }



    </style>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-lg sticky-top navbar-dark bg-primary">
            <a href="#" id="techequip" class="navbar-brand">Tech-Equip</a>
            <div style="display: block">                
                <button id="dkbt4" class="btn btn-dark justify-content-end tooltips" runat="server" data-placement="left" title="Выход" onServerClick="btdkbt_Click" CausesValidation="false" type="button">
                    <span class="fa fa-sign-out" style="color: white;" aria-hidden="true"></span>
                </button>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarmenu" aria-controls="navbarmenu" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
            </div>
            <div class="collapse navbar-collapse" id="navbarmenu">
                <ul class="navbar-nav mr-auto mt-2 mt-lg-0">
                    <li class="nav-item active">
                        <a class="nav-link" href="EquipList.aspx">Список оборудования<span class="sr-only">(current)</span></a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="RequestList.aspx">Список заявок</a>
                    </li>
                </ul>
            </div>
            <button id="dkbt5" class="btn btn-dark justify-content-end tooltips" runat="server" data-placement="left" title="Выход" onServerClick="btdkbt_Click" CausesValidation="false" type="button">
                <span class="fa fa-sign-out" style="color: white;" aria-hidden="true"></span>
            </button>
        </nav>

    
        <asp:SqlDataSource ID="sdsEquip" runat="server"></asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsEmployee" runat="server"></asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsBranchers" runat="server"></asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsCabinets" runat="server"></asp:SqlDataSource>
        <h1 class="title">Список оборудования</h1>
        <div class="container-fluid">
        <div class="row justify-content-center">
            <div class="container">
                <div class="form-row justify-content-center col-12 aling-items-center">                   
                    <div class="d-flex justify-content-start filterbut">
                        <span id="ifilter" class="fa fa-filter" aria-hidden="true"></span>
                        <asp:Button ID="btFiler" class="btn btn-primary tooltips" runat="server" data-placement="bottom" title="Фильтрация строк таблицы" Visible="true" OnClick="btFilter_Click" CausesValidation="False"></asp:Button>                        
                    </div> 
                    <div class="col-10 col-sm-6 col-md-6 col-lg-6">                       
                    <div class="tbWithIcon" style="margin-top: 10px;">                           
                            <asp:TextBox ID="tbSearch" class="form-control mr-sm-2 search-query" type="text" runat="server" placeholder="Поиск"></asp:TextBox>
                            <span id="isearch" class="fa fa-search" aria-hidden="true"></span>
                            <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Visible="true" Width="30" CausesValidation="False" />
                            <i id="icancel" class="fa fa-times" aria-hidden="true"></i>
                            <asp:Button ID="btCancel" runat="server" OnClick="btCancel_Click" Visible="true" Width="30" CausesValidation="False" />
                        </div>
                    </div>
                    <div class="d-flex justify-content-end" style="padding: 0;">
                        <div class="email-bt">
                            <a data-toggle="modal" data-target="#myModal" type="button">
                                <div class="text-call">
                                    <i class="fa fa-envelope" aria-hidden="true"></i>
                                    <span>Обратная<br>
                                        связь</span>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="container col-11">
                <div class="Table mt-3 scrbar" style="max-height: 490px; overflow-y: auto; -webkit-overflow-scrolling: touch;">
                    <asp:GridView ID="gvEquipment" runat="server" Class="table table-dark table-condensed table-hover" AllowSorting="true" UseAccessibleHeader="true" CurrentSortDirection="ASC"
                        Font-Size="20px" AlternatingRowStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                        OnRowDataBound="gvEquipment_RowDataBound" OnSelectedIndexChanged="gvEquipment_SelectedIndexChanged" OnSorting="gvEquipment_Sorting" OnRowDeleting="gvEquipment_RowDeleting">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btDelete" runat="server" ImageUrl="~/Images/trash.svg" ControlStyle-Width="30px" CommandName="Delete" OnClientClick="return isDelete()" ToolTip="Удалить" CausesValidation="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="container">
                <div class="row">
                    <div class="form-group col-md-6 col-lg-4">
                        <label class="lblText">Инвентарный номер</label>
                        <asp:TextBox ID="tbInvNumber" class="form-control" runat="server" type="search" TextMode="Number" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredInvNumber" runat="server" ErrorMessage="Введите инвентарный номер" ControlToValidate="tbInvNumber"
                            CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularInvNumber" ControlToValidate="tbInvNumber" ValidationExpression="^[0-9]+$"
                            ErrorMessage="Некорректный ввод символов" Display="Dynamic" CssClass="Error" EnableClientScript="true" runat="server" />
                    </div>
                    <div class="form-group col-md-6 col-lg-4">
                        <label class="lblText">Вид</label>
                        <asp:TextBox ID="tbKind" class="form-control" type="search" runat="server" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredKind" runat="server" ErrorMessage="Укажите вид оборудования" ControlToValidate="tbKind"
                            CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularKind" ControlToValidate="tbKind" ValidationExpression="^[а-яА-ЯёЁa-zA-Z\ ]+$"
                            ErrorMessage="Некорректный ввод символов" Display="Dynamic" CssClass="Error" EnableClientScript="true" runat="server" />
                    </div>
                    <div class="form-group col-md-6 col-lg-4">
                        <label class="lblText">Тип</label>
                        <asp:TextBox ID="tbType" class="form-control" type="search" runat="server" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredType" runat="server" ErrorMessage="Укажите тип оборудования" ControlToValidate="tbType"
                            CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularType" ControlToValidate="tbType" ValidationExpression="^[а-яА-ЯёЁa-zA-Z\ ]+$"
                            ErrorMessage="Некорректный ввод символов" Display="Dynamic" CssClass="Error" EnableClientScript="true" runat="server" />
                    </div>

                    <div class="form-group col-md-6 col-lg-4">
                        <label class="lblText">Производитель</label>
                        <asp:TextBox ID="tbManufacturer" class="form-control" type="search" runat="server" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredManufacturer" runat="server" ErrorMessage="Укажите произвотителя" ControlToValidate="tbManufacturer"
                            CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularManufacturer" ControlToValidate="tbManufacturer" ValidationExpression="^[а-яА-ЯёЁa-zA-Z0-9\-]+$"
                            ErrorMessage="Некорректный ввод символов" Display="Dynamic" CssClass="Error" EnableClientScript="true" runat="server" />
                    </div>
                    <div class="form-group col-md-6 col-lg-4">
                        <label class="lblText">Модель</label>
                        <asp:TextBox ID="tbModel" class="form-control" type="search" runat="server" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredModel" runat="server" ErrorMessage="Укажите произвотителя" ControlToValidate="tbModel"
                            CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularModel" ControlToValidate="tbModel" ValidationExpression="^[а-яА-ЯёЁa-zA-Z0-9\-]+$"
                            ErrorMessage="Некорректный ввод символов" Display="Dynamic" CssClass="Error" EnableClientScript="true" runat="server" />
                    </div>
                    <div class="form-group col-md-6 col-lg-4">
                        <label class="lblText">Сотрудник</label>
                        <asp:DropDownList class="form-control ddl1" AutoPostBack="True" ID="ddlEmployee" runat="server"></asp:DropDownList>                     
                    </div>

                    <div class="form-group col-md-6 col-lg-4">
                        <label class="lblText">Филиал</label>
                        <asp:DropDownList class="form-control ddl1" AutoPostBack="True" ID="ddlBranchers" runat="server"></asp:DropDownList>
                    </div>
                    <div class="form-group col-md-6 col-lg-4">
                        <label class="lblText">Кабинет</label>
                        <asp:DropDownList class="form-control ddl1" AutoPostBack="True" ID="ddlCabinet" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group col-md-6 col-lg-4">
                        <asp:Button ID="btInsert" class="btn btn-primary btn-block btn-lg" OnClick="btInsert_Click" runat="server" Text="Записать" />
                    </div>
                    <div class="form-group col-md-6 col-lg-4">
                        <asp:Button ID="btUpdate" class="btn btn-primary btn-block btn-lg tooltips" OnClick="btUpdate_Click" runat="server" Text="Изменить" data-placement="bottom" title="Выберете поле из таблицы перед измением"/>
                    </div>
                </div>
                <div class="modal fade bd-example-modal-lg" id="myModal" tabindex="-1" role="dialog" style="padding: 0;" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h3 class="modal-title" id="myModalLabel">Обратная связь</h3>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="container-fluid bd-example-row">
                                    <div class="row">
                                        <div class="form-group col-12">
                                            <label class="txtLabel">Тема сообщения</label>
                                            <asp:TextBox ID="tbTitle" class="form-control kabum" runat="server" type="search" placeholder="Укажите тему сообщения" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredTitle" ValidationGroup="1" runat="server" ErrorMessage="Введите заголовок сообщения" ControlToValidate="tbTitle"
                                                CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-12">
                                            <label class="txtLabel">Сообщение</label>
                                            <asp:TextBox ID="tbMessage" class="form-control kabum" runat="server" MaxLength="500" placeholder="Напишите сообщение" TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredMessage" ValidationGroup="1" runat="server" ErrorMessage="Введите ваше сообщение" ControlToValidate="tbMessage"
                                                CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row contact" style="border-top: 1px solid #dee2e6; margin-top: 10px;">
                                        <h3 style="padding-left: 0; margin-top: 10px;" class="col-12">Контактная информация</h3>
                                        <div class="col-12">
                                            <i class="fa fa-phone" aria-hidden="true">
                                                <label class="txtLabel" style="margin-left: 8px;">8 (800) 555-35-35</label>
                                            </i>
                                        </div>
                                        <div class="col-12">
                                            <i class="fa fa-envelope" aria-hidden="true">
                                                <label class="txtLabel">i_d.o.lukin@mpt.ru</label>
                                            </i>
                                        </div>
                                        <div class="col-12">
                                            <i class="fa fa-map-marker" style="margin-left: 5px;" aria-hidden="true">
                                                <label class="txtLabel" style="margin-left: 10px;">г. Москва, Нахимовский проспект, 21</label>
                                            </i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btSend" class="btn btn-lg btn-primary col-5 col-lg-2" runat="server" ValidationGroup="1" Text="Отправить" Style="padding-left: 10px;" OnClick="btSend_Click" />
                                <asp:Button ID="tbSecondCancel" class="btn btn-lg btn-secondary col-5 col-lg-2" runat="server" CausesValidation="false" Text="Закрыть" Style="padding-left: 10px;" data-dismiss="modal" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>      
    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ho+j7jyWK8fNQe+A12Hb8AhRq26LrZ/JpcUGGOn+Y7RsweNrtN/tE3MoK7ZeZDyx" crossorigin="anonymous"></script>ipt>
    <script>
        $("#tbMessage").on('input', function (e) {
            this.style.height = '1px';
            this.style.height = (this.scrollHeight + 6) + 'px';
        });
    </script>
</body>
</html>
<script src="../scripts/select2.min.js"></script>
<script>
    $(".ddl1").select2({
        theme: "bootstrap",
        width: 'resolve'
    });
</script>




