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
    public partial class Branchers : System.Web.UI.Page
    {
        private string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrBranchers;
            if (!IsPostBack)
            {
                gvFill(QR);
            }
            if (DBConnection.idMainUser == 0)
                Response.Redirect("../Authorization.aspx");
        }
        private void gvFill(string qr)
        {
            sdsBranchers.ConnectionString =
                DBConnection.connection.ConnectionString.ToString();
            sdsBranchers.SelectCommand = qr;
            sdsBranchers.DataSourceMode = SqlDataSourceMode.DataReader;
            gvBranchers.DataSource = sdsBranchers;
            gvBranchers.DataBind();
        }

        protected void gvBranchers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvBranchers, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Нажмите, чтобы выбрать запись";
            }
        }

        protected void gvBranchers_SelectedIndexChanged(object sender, EventArgs e)
        {

            foreach (GridViewRow row in gvBranchers.Rows)
            {
                if (row.RowIndex == gvBranchers.SelectedIndex)
                {
                    row.ToolTip = string.Empty;
                }
                else
                {
                    row.ToolTip = "Нажмите, чтобы выбрать запись";
                }
            }
            GridViewRow roww = gvBranchers.SelectedRow;
            tbBranchers.Text = roww.Cells[2].Text.ToString();
            DBConnection.idBranchers = Convert.ToInt32(roww.Cells[1].Text.ToString());
        }

        public void gvBranchers_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("Филиалы"):
                    e.SortExpression = "[Name]";
                    break;
            }
            sortGridView(gvBranchers, e, out sortDirection, out strField);
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

        protected void gvBranchers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int Index = Convert.ToInt32(e.RowIndex);
                DBProcedures procedures = new DBProcedures();
                GridViewRow rows = gvBranchers.SelectedRow;
                DBConnection.idBranchers = Convert.ToInt32(gvBranchers.Rows[Index].Cells[1].Text.ToString());
                procedures.Branchers_Delete(DBConnection.idBranchers);
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
            DBConnection.idBranchers = 0;
            tbBranchers.Text = string.Empty;
        }

        protected void btInsert_Click(object sender, EventArgs e)
        {
            DBConnection.idBranchers = 0;
            DBProcedures procedures = new DBProcedures();
            procedures.Branchers_Insert(Convert.ToString(tbBranchers.Text));
            Cleaner();
            gvFill(QR);
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        protected void btUpdate_Click(object sender, EventArgs e)
        {
            DBProcedures procedures = new DBProcedures();
            procedures.Branchers_Update(DBConnection.idBranchers, Convert.ToString(tbBranchers.Text));
            Cleaner();
            gvFill(QR);
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvBranchers.Rows)
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