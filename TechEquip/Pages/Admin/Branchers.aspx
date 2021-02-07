<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Branchers.aspx.cs" Inherits="TechEquip.Pages.Admin.Branchers" EnableEventValidation="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Филиалы</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>    
    <link rel="stylesheet" href="../../Content/bootstrap.min.css" />
    <link rel="stylesheet" href="../../Content/bootstrap-grid.min.css" />
    <link rel="icon" href="/images/favicon.ico" type="image/x-icon"/> 
    <link rel="shortcut icon" href="/images/favicon.ico" type="image/x-icon"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css"/>
    <link rel="stylesheet" href="../../Content/css/ExitBt.css" />
    <link rel="stylesheet" href="../../Content/css/FilterBut.css" />
    <link rel="stylesheet" href="../../Content/css/BasePages.css" />
    <link rel="stylesheet" href="../../Content/css/EmailBt.css" />
    <script src="scripts/jquery-3.4.1.min.js"></script>
    <script src="scripts/jquery-3.4.1.js"></script>
    <script type="text/javascript">
        function test(e) {
            var keynum;
            if (window.event) // для IE
            {
                keynum = e.keyCode
            }
            else if (e.which) // для webkit
            {
                keynum = e.which
            }
            if (keynum == 13) __doPostback('btSearch', '');
        }
    </script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
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
</head>   
<body>
    <form id="form1" runat="server">
    <nav class="navbar navbar-expand-lg sticky-top navbar-dark bg-primary">        
        <a href="#" id="techequip" class="navbar-brand">Tech-Equip</a>
        <div style="display: block">
            <button id="dkbt4" class="btn btn-dark justify-content-end tooltips" runat="server" data-placement="left" title="Выход" onserverclick="btdkbt_Click" causesvalidation="false" type="button">
                <span class="fa fa-sign-out" style="color: white;" aria-hidden="true"></span>
            </button>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarmenu" aria-controls="navbarmenu" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
        </div>
        <div class="collapse navbar-collapse" id="navbarmenu">
            <ul class="navbar-nav mr-auto mt-2 mt-lg-0">
              <li class="nav-item">
                <a class="nav-link" href="Users.aspx">Пользователи</a>
              </li>
              <li class="nav-item">
                <a class="nav-link" href="Position.aspx">Должности</a>
              </li>
              <li class="nav-item active">
                <a class="nav-link" href="Branchers.aspx">Филиалы<span class="sr-only">(current)</span></a>
              </li>
              <li class="nav-item">
                <a class="nav-link" href="Cabinets.aspx">Кабинеты</a>
              </li>
            </ul>
        </div>
        <button id="dkbt5" class="btn btn-dark justify-content-end tooltips" runat="server" data-placement="left" title="Выход" onserverclick="btdkbt_Click" causesvalidation="false" type="button">
            <span class="fa fa-sign-out" style="color: white;" aria-hidden="true"></span>
        </button>
    </nav>   
        <asp:SqlDataSource ID="sdsBranchers" runat="server"></asp:SqlDataSource>
        <h1 class="title">Список филиалов</h1>
        <div class="container">
            <div class="row justify-content-center">
                <div class="form-row justify-content-center col-12 aling-items-center">
                    <div class="d-flex justify-content-start filterbut">
                        <span id="ifilter" class="fa fa-filter" aria-hidden="true"></span>
                        <asp:Button ID="btFiler" class="btn btn-primary tooltips" data-placement="bottom" title="Фильтрация строк таблицы" runat="server" Visible="true" CausesValidation="False"></asp:Button>                      
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
                <div class="form-row justify-content-center col-12">
                    <div class="Table col-lg-6 col-10 mt-4 scrbar" style="max-height:490px;overflow-y:auto; -webkit-overflow-scrolling: touch;">
                        <asp:GridView id="gvBranchers" runat="server" Class="table table-dark table-condensed table-hover" AllowSorting="true" UseAccessibleHeader="true" CurrentSortDirection="ASC" 
                            Font-Size="20px" AlternatingRowStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" 
                            OnRowDataBound="gvBranchers_RowDataBound"  OnSelectedIndexChanged="gvBranchers_SelectedIndexChanged" OnSorting="gvBranchers_Sorting" OnRowDeleting="gvBranchers_RowDeleting" >
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btDelete" runat="server" ImageUrl="~/Images/trash.svg" ControlStyle-Width="30px" CommandName="Delete" OnClientClick="return isDelete()" ToolTip="Удалить" CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>               
                    <div class="col-sm-8 col-md-6 col-lg-4">
                        <div class="form-group col-lg-12">
                            <label class="lblText">Филиал</label>                               
                            <asp:TextBox ID="tbBranchers" class="form-control" runat="server" type="search" MaxLength="50" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredBranchers" runat="server" ErrorMessage="Поле не должно быть пустым" class="Error" ControlToValidate="tbBranchers" display="Dynamic" EnableClientScript="true"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator id="RegularBranchers" ControlToValidate="tbBranchers" ValidationExpression="^[а-яА-ЯёЁa-zA-Z0-9\s\.\,]+$" ErrorMessage="Некорректный ввод символов" display="Dynamic" class="Error"  EnableClientScript="true"  runat="server"/>
                        </div>
                        <div class="form-group col-lg-12">
                            <asp:Button ID ="btInsert" class="btn btn-primary btn-block btn-lg" runat ="server" Text="Записать" OnClick="btInsert_Click"/>                            
                        </div>
                        <div class="form-group col-lg-12">
                             <asp:Button ID ="btUpdate" class="btn btn-primary btn-block btn-lg tooltips" runat="server" Text="Изменить поле" OnClick="btUpdate_Click" data-placement="bottom" title="Выберете поле из таблицы перед измением" />
                        </div>                                                                                    
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
                                            <asp:TextBox ID="tbMessage" class="form-control kabum" runat="server" maxlength="500" placeholder="Напишите сообщение" textmode="MultiLine" ></asp:TextBox>
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
                                 <asp:Button ID="btSend" class="btn btn-lg btn-primary col-5 col-lg-2" runat="server" ValidationGroup="1" Text="Отправить" style="padding-left: 10px;" OnClick="btSend_Click"/>
                                 <asp:Button ID="tbSecondCancel" class="btn btn-lg btn-secondary col-5 col-lg-2" runat="server" Text="Закрыть" style="padding-left: 10px;" data-dismiss="modal"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div> 
         </div> 
    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ho+j7jyWK8fNQe+A12Hb8AhRq26LrZ/JpcUGGOn+Y7RsweNrtN/tE3MoK7ZeZDyx" crossorigin="anonymous"></script>
    <script>
        $("#tbMessage").on('input', function (e) {
            this.style.height = '1px';
            this.style.height = (this.scrollHeight + 6) + 'px';
        });
    </script>
</body>
</html>
