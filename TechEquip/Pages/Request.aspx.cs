using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;

namespace TechEquip.Pages
{
    public partial class Request : System.Web.UI.Page
    {
        private string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrEquipment;
            if (!IsPostBack)
            {
                gvFill(QR);
            }
            if (DBConnection.idMainUser == 0)
                Response.Redirect("Authorization.aspx");
        }
        private void gvFill(string qr)
        {
            sdsEquip.ConnectionString =
                DBConnection.connection.ConnectionString.ToString();
            sdsEquip.SelectCommand = qr;
            sdsEquip.DataSourceMode = SqlDataSourceMode.DataReader;
            gvEquip.DataSource = sdsEquip;
            gvEquip.DataBind();
        }

        protected void gvEquip_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[15].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvEquip, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Нажмите, чтобы выбрать запись";
            }
        }

        protected void gvEquip_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            GridViewRow roww = gvEquip.SelectedRow;
            lblChoose.Visible = true;
            lblInv.Visible = true;
            lblInv.Text = roww.Cells[1].Text;
            foreach (GridViewRow row in gvEquip.Rows)
            {
                if (row.RowIndex == gvEquip.SelectedIndex)
                {
                    row.ToolTip = string.Empty;
                }
                else
                {
                    row.ToolTip = "Нажмите, чтобы выбрать запись";
                }
            }     
            DBConnection.idEquipRequest = Convert.ToInt32(roww.Cells[0].Text.ToString());                                 
        }

        public void gvEquip_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("Инвентарный номер"):
                    e.SortExpression = "[Inventory_Number]";
                    break;
                case ("Вид"):
                    e.SortExpression = "[Kind]";
                    break;
                case ("Тип"):
                    e.SortExpression = "[Type]";
                    break;
                case ("Производитель"):
                    e.SortExpression = "[Manufacturer]";
                    break;
                case ("Модель"):
                    e.SortExpression = "[Model]";
                    break;
                case ("Филиал"):
                    e.SortExpression = "[Branchers].[Name]";
                    break;
                case ("Кабинет"):
                    e.SortExpression = "[Cabinets].[Name]";
                    break;

            }
            sortGridView(gvEquip, e, out sortDirection, out strField);
            string strDirection = sortDirection
                == SortDirection.Ascending ? "ASC" : "DESC";
            gvFill(QR + " order by " + e.SortExpression + " " + strDirection);
        }
        private void sortGridView(GridView gridView,
         GridViewSortEventArgs e,
         out SortDirection sortDirection,
         out string strSortField)
        {
            strSortField = e.SortExpression;
            sortDirection = e.SortDirection;

            if (gridView.Attributes["CurrentSortField"] != null &&
                gridView.Attributes["CurrentSortDirection"] != null)
            {
                if (strSortField ==
                    gridView.Attributes["CurrentSortField"])
                {
                    if (gridView.Attributes["CurrentSortDirection"]
                        == "ASC")
                    {
                        sortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        sortDirection = SortDirection.Ascending;
                    }
                }
            }
            gridView.Attributes["CurrentSortField"] = strSortField;
            gridView.Attributes["CurrentSortDirection"] =
                (sortDirection == SortDirection.Ascending ? "ASC"
                : "DESC");
        }

        protected void btInsert_Click(object sender, EventArgs e)
        {
            DBProcedures procedures = new DBProcedures();
            string OpeningDate = DateTime.Now.ToString("dd.MM.yyyy");
            procedures.Request_insert(DBConnection.idEquipRequest, Convert.ToString(tbComment.Text), OpeningDate);
            DBConnection.idEquipRequest = 0;
            gvFill(QR);
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvEquip.Rows)
            {
                if (row.Cells[1].Text.Equals(tbSearch.Text) ||
                        row.Cells[2].Text.Equals(tbSearch.Text) ||
                        row.Cells[3].Text.Equals(tbSearch.Text) ||
                        row.Cells[4].Text.Equals(tbSearch.Text) ||
                        row.Cells[5].Text.Equals(tbSearch.Text) ||
                        row.Cells[14].Text.Equals(tbSearch.Text) ||
                        row.Cells[16].Text.Equals(tbSearch.Text))
                    {
                    row.BackColor = System.Drawing.Color.Green;
                }
                else
                    row.BackColor = System.Drawing.ColorTranslator.FromHtml("#343a40");
            }
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            tbSearch.Text = "";
            btSearch_Click(sender, e);
            gvFill(QR);
        }

        protected void btSend_Click(object sender, EventArgs e)
        {
            int port = 587;
            bool enableSSL = true;

            string Login;
            string Email;
            DBConnection connection = new DBConnection();
            connection.getIDPersonal(DBConnection.idMainUser);
            SqlCommand command = new SqlCommand("", DBConnection.connection);
            command.CommandText = "select [Login] from [Authorization] where [ID_Authorization] = '" + DBConnection.idMainUser + "'";
            DBConnection.connection.Open();
            Login = command.ExecuteScalar().ToString();
            DBConnection.connection.Close();
            command.CommandText = "select [Email] from [Users] where [ID_User] = '" + DBConnection.idMainUser + "'";
            DBConnection.connection.Open();
            Email = command.ExecuteScalar().ToString();
            DBConnection.connection.Close();
            string emailFrom = "bot.ordoweat@bk.ru"; //почта отправителя
            string password = "pp.ordoweat.pp"; //пароль оправителя
            string emailTo = "danlukin36@gmail.com"; //Почта получателя
            string subject = tbTitle.Text; //Заголовок сообщения
            string smtpAddress = "smtp.mail.ru"; //smtp протокол
            string tittle = tbTitle.Text; //Заголовок сообщения;
            string name = "от: " + Login + " почта: " + Email;
            string message = tbMessage.Text; //Сообщение

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(emailFrom);
            mail.To.Add(emailTo);
            mail.Subject = subject;
            mail.Body = tittle + "\r\n" + name + "\r\n" + "---------------------------------------" + "\r\n" + message; //тело сообщения
            mail.IsBodyHtml = false;
            using (SmtpClient smtp = new SmtpClient(smtpAddress, port))
            {
                smtp.Credentials = new NetworkCredential(emailFrom, password);
                smtp.EnableSsl = enableSSL;
                smtp.Send(mail);
            }
        }

        protected void btdkbt_Click(object sender, EventArgs e)
        {
            DBConnection.idMainUser = 0;
            Response.Redirect("Authorization.aspx");
        }

    }
}