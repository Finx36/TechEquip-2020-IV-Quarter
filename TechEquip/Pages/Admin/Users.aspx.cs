using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;

namespace TechEquip.Pages.Admin
{
    public partial class Users : System.Web.UI.Page
    {
        private string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrUsers;
            if (!IsPostBack)
            {
                gvFill(QR);
                ddlPositionFill();
            }
            if (DBConnection.idMainUser == 0)
                Response.Redirect("../Authorization.aspx");
        }

        private void gvFill(string qr)
        {
            sdsUsers.ConnectionString =
                DBConnection.connection.ConnectionString.ToString();
            sdsUsers.SelectCommand = qr;
            sdsUsers.DataSourceMode = SqlDataSourceMode.DataReader;
            gvUsers.DataSource = sdsUsers;
            gvUsers.DataBind();
        }

        private void ddlPositionFill()
        {
            sdsPosition.ConnectionString =
                DBConnection.connection.ConnectionString.ToString();
            sdsPosition.SelectCommand = DBConnection.qrPosition;
            sdsPosition.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlPosition.DataSource = sdsPosition;
            ddlPosition.DataTextField = "Name";
            ddlPosition.DataValueField = "ID_Position";
            ddlPosition.DataBind();
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[9].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvUsers, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Нажмите, чтобы выбрать запись";
            }
        }

        protected void gvUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

            foreach (GridViewRow row in gvUsers.Rows)
            {
                if (row.RowIndex == gvUsers.SelectedIndex)
                {
                    row.ToolTip = string.Empty;
                }
                else
                {
                    row.ToolTip = "Нажмите, чтобы выбрать запись";
                }
            }
            GridViewRow roww = gvUsers.SelectedRow;
            ddlPosition.SelectedValue = roww.Cells[9].Text.ToString();
            DBConnection.idUser = Convert.ToInt32(roww.Cells[1].Text.ToString());
        }

        public void gvUsers_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("Фамилия"):
                    e.SortExpression = "[Surname]";
                    break;
                case ("Имя"):
                    e.SortExpression = "[Name]";
                    break;
                case ("Отчество"):
                    e.SortExpression = "[MiddleName]";
                    break;
                case ("Номер телефона"):
                    e.SortExpression = "[PhoneNumber]";
                    break;
                case ("Эл.почта"):
                    e.SortExpression = "[Email]";
                    break;
                case ("Логин"):
                    e.SortExpression = "[Login]";
                    break;
                case ("Должность"):
                    e.SortExpression = "[Position].[Name]";
                    break;
            }
            sortGridView(gvUsers, e, out sortDirection, out strField);
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

        protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int Index = Convert.ToInt32(e.RowIndex);
                DBProcedures procedures = new DBProcedures();
                GridViewRow rows = gvUsers.SelectedRow;
                DBConnection.idUser = Convert.ToInt32(gvUsers.Rows[Index].Cells[1].Text.ToString());
                DBConnection.idAuthUser = Convert.ToInt32(gvUsers.Rows[Index].Cells[7].Text.ToString());
                procedures.Users_Delete(DBConnection.idUser, DBConnection.idAuthUser);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Запись успешно удалена.')", true);
                gvFill(QR);
                Cleaner();
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось удалить запись :(')", true);
            }
        }

        protected void Cleaner()
        {
            DBConnection.idUser = 0;
            DBConnection.idAuthUser = 0;
            ddlPosition.SelectedIndex = 0;
        }

        protected void btUpdate_Click(object sender, EventArgs e)
        {
            DBProcedures procedures = new DBProcedures();
            procedures.Users_Update(DBConnection.idUser, Convert.ToInt32(ddlPosition.SelectedValue));
            Cleaner();
            gvFill(QR);
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvUsers.Rows)
            {
                if (row.Cells[2].Text.Equals(tbSearch.Text))
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
            Response.Redirect("../Authorization.aspx");
        }

    }
}
