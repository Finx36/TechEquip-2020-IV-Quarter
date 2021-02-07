using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace TechEquip
{
    public class DBProcedures
    {
        private SqlCommand command = new SqlCommand("", DBConnection.connection);
        //Процедуры для работы с  данными из БД
        private void commandConfig(string config)
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "[dbo].[" + config + "]";
            command.Parameters.Clear();
        }
        public void EquipList_Insert(int Cabinets_ID, int Branchers_ID, int Inventory_Number, string Kind, string Type, string Manufacturer, string Model,
            int Users_ID)
        {
            commandConfig("Equipment_insert");
            command.Parameters.AddWithValue("@Cabinets_ID", Cabinets_ID);
            command.Parameters.AddWithValue("@Branchers_ID", Branchers_ID);
            command.Parameters.AddWithValue("@Inventory_Number", Inventory_Number);
            command.Parameters.AddWithValue("@Kind", Kind);
            command.Parameters.AddWithValue("@Type", Type);
            command.Parameters.AddWithValue("@Manufacturer", Manufacturer);
            command.Parameters.AddWithValue("@Model", Model);
            command.Parameters.AddWithValue("@Users_ID", Users_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void EquipList_Update(int Cabinets_ID, int Branchers_ID, int ID_Equipment, int Inventory_Number, string Kind, string Type, string Manufacturer, string Model,
            int Users_ID, int Location_ID)
        {
            commandConfig("Equipment_Update");
            command.Parameters.AddWithValue("@Cabinets_ID", Cabinets_ID);
            command.Parameters.AddWithValue("@Branchers_ID", Branchers_ID);
            command.Parameters.AddWithValue("@ID_Equipment", ID_Equipment);
            command.Parameters.AddWithValue("@Inventory_Number", Inventory_Number);
            command.Parameters.AddWithValue("@Kind", Kind);
            command.Parameters.AddWithValue("@Type", Type);
            command.Parameters.AddWithValue("@Manufacturer", Manufacturer);
            command.Parameters.AddWithValue("@Model", Model);
            command.Parameters.AddWithValue("@Users_ID", Users_ID);
            command.Parameters.AddWithValue("@Location_ID", Location_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void EquipList_Delete(int ID_Equipment, int Location_ID)
        {
            commandConfig("Equipment_delete");
            command.Parameters.AddWithValue("@ID_Equipment", ID_Equipment);
            command.Parameters.AddWithValue("@Location_ID", Location_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void Position_Insert(string Name)
        {
            commandConfig("Position_insert");
            command.Parameters.AddWithValue("@Name", Name);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void Position_Update(int ID_Position, string Name)
        {
            commandConfig("Position_Update");
            command.Parameters.AddWithValue("@ID_Position", ID_Position);
            command.Parameters.AddWithValue("@Name", Name);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void Position_Delete(int ID_Position)
        {
            commandConfig("Position_delete");
            command.Parameters.AddWithValue("@ID_Position", ID_Position);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void Cabinets_Insert(string Name)
        {
            commandConfig("Cabinets_insert");
            command.Parameters.AddWithValue("@Name", Name);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void Cabinets_Update(int ID_Cabinets, string Name)
        {
            commandConfig("Cabinets_Update");
            command.Parameters.AddWithValue("@ID_Cabinets", ID_Cabinets);
            command.Parameters.AddWithValue("@Name", Name);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void Cabinets_Delete(int ID_Cabinets)
        {
            commandConfig("Cabinets_delete");
            command.Parameters.AddWithValue("@ID_Cabinets", ID_Cabinets);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void Branchers_Insert(string Name)
        {
            commandConfig("Branchers_insert");
            command.Parameters.AddWithValue("@Name", Name);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void Branchers_Update(int ID_Branchers, string Name)
        {
            commandConfig("Branchers_Update");
            command.Parameters.AddWithValue("@ID_Branchers", ID_Branchers);
            command.Parameters.AddWithValue("@Name", Name);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void Branchers_Delete(int ID_Branchers)
        {
            commandConfig("Branchers_delete");
            command.Parameters.AddWithValue("@ID_Branchers", ID_Branchers);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void UsersRegistration(string Login, string Password, string Surname, string Name, string MiddleName,
    string PhoneNumber, string Email)
        {
            commandConfig("Users_Registration");
            command.Parameters.AddWithValue("@Login", Login);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@Surname", Surname);
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@MiddleName", MiddleName);
            command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
            command.Parameters.AddWithValue("@Email", Email);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void Users_Update(int ID_User, int Position_ID)
        {
            commandConfig("Users_update");
            command.Parameters.AddWithValue("@ID_User", ID_User);
            command.Parameters.AddWithValue("@Position_ID", Position_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void Users_Delete(int ID_User, int Authorization_ID)
        {
            commandConfig("Users_delete");
            command.Parameters.AddWithValue("@ID_User", ID_User);
            command.Parameters.AddWithValue("@Authorization_ID", Authorization_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void Request_insert(int Equipment_ID, string Comment, string Order_Opening_Date)
        {
            commandConfig("Request_insert");
            command.Parameters.AddWithValue("@Equipment_ID", Equipment_ID);
            command.Parameters.AddWithValue("@Comment", Comment);
            command.Parameters.AddWithValue("@Order_Opening_Date", Order_Opening_Date);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void Request_Update(int ID_Request, string Status, string Order_Closing_Date, int Repair_Cost, string Service_Org)
        {
            commandConfig("Request_update");
            command.Parameters.AddWithValue("@ID_Request", ID_Request);
            command.Parameters.AddWithValue("@Status", Status);
            command.Parameters.AddWithValue("@Order_Closing_Date", Order_Closing_Date);
            command.Parameters.AddWithValue("@Repair_Cost", Repair_Cost);
            command.Parameters.AddWithValue("@Service_Org", Service_Org);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void Request_Delete(int ID_Request)
        {
            commandConfig("Request_delete");
            command.Parameters.AddWithValue("@ID_Request", ID_Request);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
    }
}