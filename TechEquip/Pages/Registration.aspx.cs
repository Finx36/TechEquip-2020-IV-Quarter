using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechEquip.Pages
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btRegistration_Click(object sender, EventArgs e)
        {
            DBConnection connection = new DBConnection();
            //Проверка уникальности почты
            if (connection.EmailCheck(tbEmail.Text) > 0 & tbEmail.Text != "")
            {
                lblEmailCheck.Visible = true;
            }
            else
            {
                //Проверка уникальности номера телефона
                if (connection.PhoneNumberCheck(tbPhoneNumber.Text) > 0 & tbPhoneNumber.Text != "")
                {
                    lblPhoneNumberCheck.Visible = true;
                }
                else
                    {
                    //Проверка уникальности логина
                    if (connection.LoginCheck(tbLogin.Text) > 0 & tbLogin.Text != "")
                    {
                        lblLoginCheck.Visible = true;
                    }
                    else
                    {
                        //Проверка подтверждения пароля
                        if (tbPassword1.Text != tbPasswordConfirm1.Text)
                        {
                            lblPasswordConfirm.Visible = true;
                        }
                        else
                        {
                            lblLoginCheck.Visible = false;
                            lblPasswordConfirm.Visible = false;
                            DBProcedures procedures = new DBProcedures();
                            procedures.UsersRegistration(tbLogin.Text.ToString(), tbPassword1.Text.ToString(), tbSurname.Text.ToString(), tbName.Text.ToString(),
                                tbMiddleName.Text.ToString(), tbPhoneNumber.Text.ToString(), tbEmail.Text.ToString());
                            Response.Redirect("Authorization.aspx");
                        }
                    }
                }
            }
        }

        protected void tbEmail_TextChanged(object sender, EventArgs e)
        {
            lblEmailCheck.Visible = false;
        }
    }
}