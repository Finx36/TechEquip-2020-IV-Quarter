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
    public partial class RequestList : System.Web.UI.Page
    {
        private string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrRequest;
            if (!IsPostBack)
            {
                gvFill(QR);
            }
            if (DBConnection.idMainUser == 0)
                Response.Redirect("Authorization.aspx");
        }

        private void gvFill(string qr)
        {
            sdsRequestList.ConnectionString =
                DBConnection.connection.ConnectionString.ToString();
            sdsRequestList.SelectCommand = qr;
            sdsRequestList.DataSourceMode = SqlDataSourceMode.DataReader;
            gvRequestList.DataSource = sdsRequestList;
            gvRequestList.DataBind();
        }

        protected void gvRequestList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvRequestList, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Нажмите, чтобы выбрать запись";
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "50px");
            }
        }

        protected void gvRequestList_SelectedIndexChanged(object sender, EventArgs e)
        {

            foreach (GridViewRow row in gvRequestList.Rows)
            {
                if (row.RowIndex == gvRequestList.SelectedIndex)
                {
                    row.ToolTip = string.Empty;
                }
                else
                {
                    row.ToolTip = "Нажмите, чтобы выбрать запись";
                }
            }
            GridViewRow roww = gvRequestList.SelectedRow;
            tbRepairCost.Text = roww.Cells[12].Text.ToString();
            tbServiceOrg.Text = roww.Cells[13].Text.ToString();
            DBConnection.idRequest = Convert.ToInt32(roww.Cells[1].Text.ToString());
        }

        public void gvRequestList_Sorting(object sender, GridViewSortEventArgs e)
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
                case ("Комментарий"):
                    e.SortExpression = "[Comment]";
                    break;
                case ("Статус"):
                    e.SortExpression = "[Status]";
                    break;
                case ("Дата открытия заявки"):
                    e.SortExpression = "[Order_Opening_Date]";
                    break;
                case ("Дата закрытия заявки"):
                    e.SortExpression = "[Order_Closing_Date]";
                    break;
                case ("Стоимость ремонта"):
                    e.SortExpression = "[Repair_Cost]";
                    break;
                case ("Наименование сервисной организации"):
                    e.SortExpression = "[Service_Org]";
                    break;

            }
            sortGridView(gvRequestList, e, out sortDirection, out strField);
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

        protected void Cleaner()
        {
            DBConnection.idEquipment = 0;
            tbRepairCost.Text = string.Empty;
            tbServiceOrg.Text = string.Empty;
        }

        protected void btUpdate_Click(object sender, EventArgs e)
        {
            DBProcedures procedures = new DBProcedures();
            string status = "Завершён";
            string ClosingDate = DateTime.Now.ToString("dd.MM.yyyy");
            procedures.Request_Update(DBConnection.idRequest, status, ClosingDate, Convert.ToInt32(tbRepairCost.Text), Convert.ToString(tbServiceOrg.Text));
            Cleaner();
            gvFill(QR);
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvRequestList.Rows)
            {
                if (row.Cells[3].Text.Equals(tbSearch.Text) ||
                        row.Cells[4].Text.Equals(tbSearch.Text) ||
                        row.Cells[5].Text.Equals(tbSearch.Text) ||
                        row.Cells[6].Text.Equals(tbSearch.Text) ||
                        row.Cells[7].Text.Equals(tbSearch.Text) ||
                        row.Cells[8].Text.Equals(tbSearch.Text) ||
                        row.Cells[9].Text.Equals(tbSearch.Text) ||
                        row.Cells[10].Text.Equals(tbSearch.Text) ||
                        row.Cells[11].Text.Equals(tbSearch.Text) ||
                        row.Cells[12].Text.Equals(tbSearch.Text) ||
                        row.Cells[13].Text.Equals(tbSearch.Text))
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

        protected void gvRequestList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int Index = Convert.ToInt32(e.RowIndex);
                DBProcedures procedures = new DBProcedures();
                GridViewRow rows = gvRequestList.SelectedRow;
                DBConnection.idRequest = Convert.ToInt32(gvRequestList.Rows[Index].Cells[1].Text.ToString());
                procedures.Request_Delete(DBConnection.idRequest);
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