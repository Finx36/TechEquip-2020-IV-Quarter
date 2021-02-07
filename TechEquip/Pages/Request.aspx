<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Request.aspx.cs" Inherits="TechEquip.Pages.Request" EnableEventValidation="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Отправление заявки</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>    
    <link rel="stylesheet" href="../Content/bootstrap.min.css" />
    <link rel="stylesheet" href="../Content/bootstrap-grid.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css"/>
    <link rel="stylesheet" href="https://bootstraptema.ru/plugins/2015/kw-count/kwcount.css" />
    <link rel="icon" href="/images/favicon.ico" type="image/x-icon"/> 
    <link rel="shortcut icon" href="/images/favicon.ico" type="image/x-icon"/>
    <link rel="stylesheet" href="../Content/css/ExitBt.css" />
    <link rel="stylesheet" href="../Content/css/FilterBut.css" />
    <link rel="stylesheet" href="../Content/css/BasePages.css" />
    <link rel="stylesheet" href="../Content/css/EmailBt.css" />
    <script type="text/javascript" src="https://bootstraptema.ru/plugins/jquery/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="https://bootstraptema.ru/plugins/2015/kw-count/kwcount.js"></script>  
    <script src="../scripts/jquery-3.4.1.min.js"></script>
    <script src="../scripts/jquery-3.4.1.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        //Иннициализация tooltip
        $(document).ready(function () {
            $('.tooltips').tooltip();
        });
    </script>
    <style>
        body {
	        background: #161a1e;
            font-family: 'Century Gothic';
	    }
        .title {
            color: white;
            text-align: center;           
        }
        .lblText {            
            color: white;
            margin: 5px;
            font-size: 20px;
        }
        
        .form-control {            
            border-radius: 10px;
            border: 0px;                                    
        }
        .btn {
            border-radius: 10px;
        }
        #tbComment {            
            min-height: 60px;
            resize: none;
            max-height: 610px;            
        }
        @media screen and (min-width: 992px) {
            #techequip 
            {
                border-right: 0px;
            }
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

     </style>               
</head>   
<body>

    <form id="form1" runat="server">
    <nav class="navbar navbar-expand-lg sticky-top navbar-dark bg-primary">        
        <a href="#" id="techequip" class="navbar-brand">Tech-Equip</a>
        <button id="dkbt6" class="btn btn-dark tooltips pull-right" runat="server" style="display: initial; right: 10px;" data-placement="left" title="Выход" onserverclick="btdkbt_Click" causesvalidation="false" type="button">
            <span class="fa fa-sign-out" style="color: white;" aria-hidden="true"></span>
        </button>
    </nav>   
        <asp:SqlDataSource ID="sdsEquip" runat="server"></asp:SqlDataSource>
        <h1 class="title">Отправление заявки</h1>
        <div class="container-fluid">
            <div class="row justify-content-center">
                <div class="form-row justify-content-center col-12 aling-items-center">
                    <div class="d-flex justify-content-start filterbut">
                        <span id="ifilter" class="fa fa-filter" aria-hidden="true"></span>
                        <asp:Button ID="btFiler" class="btn btn-primary tooltips" data-placement="bottom" title="Фильтрация строк таблицы" runat="server" Visible="true" CausesValidation="False"></asp:Button>                      
                    </div> 
                        <div class="col-10 col-sm-6 col-md-6 col-lg-4">
                             <div class="tbWithIcon" style="margin-top:10px;">
                                <asp:TextBox ID="tbSearch" class="form-control mr-sm-2 search-query" type="text" runat="server" placeholder="Поиск"></asp:TextBox>
                                <span id="isearch" class="fa fa-search" aria-hidden="true"></span>
                                <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Visible="true" Width="30" CausesValidation="False" />
                                <i id="icancel" class="fa fa-times" aria-hidden="true"></i>
                                <asp:Button ID="btCancel" runat="server" OnClick="btCancel_Click" Visible="true" Width="30" CausesValidation="False" />
                            </div>
                        </div>                   
                        <div class="d-flex justify-content-end" style="padding: 0;">
                            <div class="email-bt">
                                <a data-toggle="modal" data-target="#myModal" type="button" >
                                    <div class="text-call">
                                        <i class="fa fa-envelope" aria-hidden="true"></i>
                                        <span>Обратная<br>
                                            связь</span>
                                    </div>
                                </a>
                            </div>
                        </div>
                    </div>               
                <div class="Table col-lg-9 col-10 scrbar mt-3" style="max-height: 490px; overflow-y: auto; -webkit-overflow-scrolling: touch;">
                    <div class="form-row">
                        <asp:label id="lblChoose" runat="server" class="lblText" visible="false" >Выбран инвентарный номер:</asp:label>
                        <asp:label id="lblInv" runat="server" class="lblText" visible="false"></asp:label>
                    </div>                   
                    <asp:GridView ID="gvEquip" runat="server" Class="table table-dark table-condensed table-hover" AllowSorting="true" UseAccessibleHeader="true" CurrentSortDirection="ASC"
                        Font-Size="20px" AlternatingRowStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center"
                        OnRowDataBound="gvEquip_RowDataBound" OnSelectedIndexChanged="gvEquip_SelectedIndexChanged" OnSorting="gvEquip_Sorting">
                    </asp:GridView>
                </div>
                <div class="form-row justify-content-center">
                    <div class="form-group col-10 col-md-10 col-lg-10">
                        <label class="lblText">Комментарий</label>
                        <asp:TextBox ID="tbComment" class="form-control" runat="server" maxlength="500" placeholder="Напишите сообщение" textmode="MultiLine" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredComment" runat="server" ErrorMessage="Введите ваше сообщение" ControlToValidate="tbComment"
                                                CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div class="btn btn-block col-10 col-md-10 col-lg-10">
                        <asp:Button ID="btSendRequest" class="btn btn-primary btn-lg btn-block tooltips" OnClick="btInsert_Click" runat="server" Text="Отправить заявку" data-placement="bottom" title="Выберете поле из таблицы перед отправкой"/>
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
        <script>
            $("#tbComment").kwcount();
        </script>
    <script>
        $("#tbComment").on('input', function (e) {
            this.style.height = '1px';
            this.style.height = (this.scrollHeight + 6) + 'px';
        });
    </script>
    <script>
        $("#tbMessage").on('input', function (e) {
            this.style.height = '1px';
            this.style.height = (this.scrollHeight + 6) + 'px';
        });
    </script>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ho+j7jyWK8fNQe+A12Hb8AhRq26LrZ/JpcUGGOn+Y7RsweNrtN/tE3MoK7ZeZDyx" crossorigin="anonymous"></script>
</body>
</html>




