using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data.SqlClient;



namespace TechEquip.Pages
{
    public partial class Authorization : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btSubmit_Click(object sender, EventArgs e)
        {
            string Password;
            DBConnection connection = new DBConnection();
            connection.Authorization(tbLogin1.Text);
            connection.getIDPersonal(DBConnection.idMainUser);
            switch (DBConnection.idMainUser)
            {
                case (0):
                    tbLogin1.BackColor = ColorTranslator.FromHtml("#cc0000");
                    tbPassword1.BackColor = ColorTranslator.FromHtml("#cc0000");
                    lblAuthorization.Visible = true;
                    break;
                default:
                    //Проверка пароля
                    SqlCommand command = new SqlCommand("", DBConnection.connection);
                    command.CommandText = "select [Password] from [Authorization] where [ID_Authorization] = '" + DBConnection.idMainUser + "'";
                    DBConnection.connection.Open();
                    Password = command.ExecuteScalar().ToString(); //Строка (пароль) из базы данных
                    DBConnection.connection.Close();
                    if (tbPassword1.Text.ToString() == Password)
                    {
                        switch (connection.UserPosition(DBConnection.idMainUser))
                        {
                            //Адмнистратор
                            case ("1"):
                                Response.Redirect("Admin/Users.aspx");
                                break;
                            //Кладовщик
                            case ("2"):
                                Response.Redirect("EquipList.aspx");
                                break;
                            default:
                                Response.Redirect("Request.aspx");
                                break;
                        }
                        break;
                    }
                    else
                    {
                        tbLogin1.BackColor = ColorTranslator.FromHtml("#cc0000");
                        tbPassword1.BackColor = ColorTranslator.FromHtml("#cc0000");
                        lblAuthorization.Visible = true;
                    }
                    break;
            }
        }
    }
}