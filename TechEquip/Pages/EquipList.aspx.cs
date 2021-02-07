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
    public partial class EquipList : System.Web.UI.Page
    {
        private string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrEquipment;
            if (!IsPostBack)
            {
                gvFill(QR);
                ddlEmployeeFill();
                ddlBranchersFill();
                ddlCabinetsFill();
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
            gvEquipment.DataSource = sdsEquip;
            gvEquipment.DataBind();
        }

        private void ddlEmployeeFill()
        {
            sdsEmployee.ConnectionString =
                DBConnection.connection.ConnectionString.ToString();
            sdsEmployee.SelectCommand = DBConnection.qrFio;
            sdsEmployee.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlEmployee.DataSource = sdsEmployee;
            ddlEmployee.DataTextField = "ФИО";
            ddlEmployee.DataValueField = "ID_User";
            ddlEmployee.DataBind();
        }

        private void ddlBranchersFill()
        {
            sdsBranchers.ConnectionString =
                DBConnection.connection.ConnectionString.ToString();
            sdsBranchers.SelectCommand = DBConnection.qrBranchers;
            sdsBranchers.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlBranchers.DataSource = sdsBranchers;
            ddlBranchers.DataTextField = "Name";
            ddlBranchers.DataValueField = "ID_Branchers";
            ddlBranchers.DataBind();
        }

        private void ddlCabinetsFill()
        {
            sdsCabinets.ConnectionString =
                DBConnection.connection.ConnectionString.ToString();
            sdsCabinets.SelectCommand = DBConnection.qrCabinets;
            sdsCabinets.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlCabinet.DataSource = sdsCabinets;
            ddlCabinet.DataTextField = "Name";
            ddlCabinet.DataValueField = "ID_Cabinets";
            ddlCabinet.DataBind();
        }

        protected void gvEquipment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[16].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvEquipment, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Нажмите, чтобы выбрать запись";
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "50px");
            }
        }

        protected void gvEquipment_SelectedIndexChanged(object sender, EventArgs e)
        {

            foreach (GridViewRow row in gvEquipment.Rows)
            {
                if (row.RowIndex == gvEquipment.SelectedIndex)
                {
                    row.ToolTip = string.Empty;
                }
                else
                {
                    row.ToolTip = "Нажмите, чтобы выбрать запись";
                }
            }
            GridViewRow roww = gvEquipment.SelectedRow;
            tbInvNumber.Text = roww.Cells[2].Text.ToString();
            tbKind.Text = roww.Cells[3].Text.ToString();
            tbType.Text = roww.Cells[4].Text.ToString();
            tbManufacturer.Text = roww.Cells[5].Text.ToString();
            tbModel.Text = roww.Cells[6].Text.ToString();
            ddlEmployee.SelectedValue = roww.Cells[7].Text.ToString();
            ddlBranchers.SelectedValue = roww.Cells[14].Text.ToString();
            ddlCabinet.SelectedValue = roww.Cells[16].Text.ToString();
            DBConnection.idEquipment = Convert.ToInt32(roww.Cells[1].Text.ToString());
            DBConnection.IdEquipmentLocation = Convert.ToInt32(roww.Cells[13].Text.ToString());
        }

        protected void btFilter_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                string newQR = QR + "and ([Inventory_Number] like '%" + tbSearch.Text + "%' or [Kind] like '%" + tbSearch.Text + "%' or [Type] like '%" + tbSearch.Text + "%' or " +
                    "[Manufacturer] like '%" + tbSearch.Text + "%' or [Model] like '%" + tbSearch.Text + "%' or [Users].[Surname] like '%" + tbSearch.Text + "%'" +
                    "or [Users].[Name] like '%" + tbSearch.Text + "%' or [Users].[MiddleName] like '%" + tbSearch.Text + "%' or [Position].[Name] like '%" + tbSearch.Text + "%'" +
                    "or [Cabinets].[Name] like '%" + tbSearch.Text + "%' or [Branchers].[Name] like '%" + tbSearch.Text + "%')";
                gvFill(newQR);
            }
        }

        public void gvEquipment_Sorting(object sender, GridViewSortEventArgs e)
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
                case ("Фамилия"):
                    e.SortExpression = "[Users].[Surname]";
                    break;
                case ("Имя"):
                    e.SortExpression = "[Users].[Name]";
                    break;
                case ("Отчество"):
                    e.SortExpression = "[Users].[MiddleName]";
                    break;
                case ("Должность"):
                    e.SortExpression = "[Position].[Name]";
                    break;
                case ("Филиал"):
                    e.SortExpression = "[Cabinets].[Name]";
                    break;
                case ("Кабинет"):
                    e.SortExpression = "[Branchers].[Name]";
                    break;

            }
            sortGridView(gvEquipment, e, out sortDirection, out strField);
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
            DBConnection.IdEquipmentLocation = 0;
            tbInvNumber.Text = string.Empty;
            tbKind.Text = string.Empty;
            tbType.Text = string.Empty;
            tbManufacturer.Text = string.Empty;
            tbModel.Text = string.Empty;
            ddlBranchers.SelectedIndex = 0;
            ddlCabinet.SelectedIndex = 0;
            ddlEmployee.SelectedIndex = 0;
        }

        protected void btInsert_Click(object sender, EventArgs e)
        {
            DBConnection.idEquipment = 0;
            DBProcedures procedures = new DBProcedures();
            procedures.EquipList_Insert(Convert.ToInt32(ddlCabinet.SelectedValue), Convert.ToInt32(ddlBranchers.SelectedValue), Convert.ToInt32(tbInvNumber.Text),
                Convert.ToString(tbKind.Text), Convert.ToString(tbType.Text), Convert.ToString(tbManufacturer.Text), Convert.ToString(tbModel.Text), Convert.ToInt32(ddlEmployee.SelectedValue));
            Cleaner();
            gvFill(QR);
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        protected void btUpdate_Click(object sender, EventArgs e)
        {
            DBProcedures procedures = new DBProcedures();
            procedures.EquipList_Update(Convert.ToInt32(ddlCabinet.SelectedValue), Convert.ToInt32(ddlBranchers.SelectedValue), DBConnection.idEquipment, Convert.ToInt32(tbInvNumber.Text),
                Convert.ToString(tbKind.Text), Convert.ToString(tbType.Text), Convert.ToString(tbManufacturer.Text), Convert.ToString(tbModel.Text),
                Convert.ToInt32(ddlEmployee.SelectedValue), DBConnection.IdEquipmentLocation);
            Cleaner();
            gvFill(QR);
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvEquipment.Rows)
            {
                if (row.Cells[2].Text.Equals(tbSearch.Text) ||
                        row.Cells[3].Text.Equals(tbSearch.Text) ||
                        row.Cells[4].Text.Equals(tbSearch.Text) ||
                        row.Cells[5].Text.Equals(tbSearch.Text) ||
                        row.Cells[6].Text.Equals(tbSearch.Text) ||
                        row.Cells[8].Text.Equals(tbSearch.Text) ||
                        row.Cells[9].Text.Equals(tbSearch.Text) ||
                        row.Cells[10].Text.Equals(tbSearch.Text) ||
                        row.Cells[12].Text.Equals(tbSearch.Text) ||
                        row.Cells[15].Text.Equals(tbSearch.Text) ||
                        row.Cells[17].Text.Equals(tbSearch.Text))
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

        protected void gvEquipment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int Index = Convert.ToInt32(e.RowIndex);
                DBProcedures procedures = new DBProcedures();
                GridViewRow rows = gvEquipment.SelectedRow;
                DBConnection.idEquipment = Convert.ToInt32(gvEquipment.Rows[Index].Cells[1].Text.ToString());
                DBConnection.IdEquipmentLocation = Convert.ToInt32(gvEquipment.Rows[Index].Cells[13].Text.ToString());
                procedures.EquipList_Delete(DBConnection.idEquipment, DBConnection.IdEquipmentLocation);
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