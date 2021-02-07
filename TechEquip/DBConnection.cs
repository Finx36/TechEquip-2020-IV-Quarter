using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace TechEquip
{
    public class DBConnection
    {
        //Подключение к базе данных 
        public static SqlConnection connection = new SqlConnection("Data Source = DESKTOP-RV6IQJS\\SQLEXPRESS; " +
                 " Initial Catalog = TechEquip; Persist Security Info = true;" +
                 " User ID = sa; Password = \"pass13\"");

        public static string qrEquipment = "select[Equipment].[ID_Equipment], [Inventory_Number] as 'Инвентарный номер', [Kind] as 'Вид', [Type] as 'Тип', " +
            "[Manufacturer] as 'Производитель', [Model] as 'Модель'," +
            "[Equipment].[Users_ID], [Surname] as 'Фамилия', [Users].[Name] as 'Имя', [MiddleName] as 'Отчество'," +
            "[Users].[Position_ID], [Position].[Name] as 'Должность'," +
            "[Equipment].[Location_ID], [Location_Select].[Branchers_ID], [Branchers].[Name] as 'Филиал'," +
            "[Location_Select].[Cabinets_ID], [Cabinets].[Name] as 'Кабинет' from [Equipment]" +
            "inner join[Users] on [Users].[ID_User] = [Equipment].[Users_ID]" +
            "inner join[Position] on [Position].[ID_Position] = [Users].[Position_ID]" +
            "inner join[Location_Select] on [Location_Select].[ID_Location] = [Equipment].[Location_ID]" +
            "inner join[Branchers] on [Branchers].[ID_Branchers] = [Location_Select].[Branchers_ID]" +
            "inner join[Cabinets] on [Cabinets].[ID_Cabinets] = [Location_Select].[Cabinets_ID]",
          qrRequest = "select[Request].[ID_Request], [Equipment_ID], [Inventory_Number] as 'Инвентарный номер', [Kind] as 'Вид', [Type] as 'Тип', " +
            "[Manufacturer] as 'Производитель', [Model] as 'Модель'," +
            "[Comment] as 'Комментарий', [Status] as 'Статус', [Order_Opening_Date] as 'Дата открытия заявки'," +
            "[Order_Closing_Date] as 'Дата закрытия заявки'," +
            "[Repair_Cost] as 'Стоимость ремонта', [Service_Org] as 'Наименование сервисной организации' from [Request]" +
            "inner join[Equipment] on [Equipment].[ID_Equipment] = [Request].[Equipment_ID]",
          qrUsers = "select[Users].[ID_User], [Surname] as 'Фамилия', [Users].[Name] as 'Имя', [MiddleName] as 'Отчество', " +
            "[PhoneNumber] as 'Номер телефона', [Email] as 'Эл.Почта', [Users].[Authorization_ID], [Login] as 'Логин'," +
            "[Users].[Position_ID], [Position].[Name] as 'Должность' from [Users]" +
            "inner join[Authorization] on [Authorization].[ID_Authorization] = [Users].[Authorization_ID]" +
            "inner join[Position] on [Position].[ID_Position] = [Users].[Position_ID]",
          qrLocationSelect = "select[Location_Select].[ID_Location], [Location_Select].[Branchers_ID], [Branchers].[Name] as 'Филиал'," +
            "[Location_Select].[Cabinets_ID], [Cabinets].[Name] from [Location_Select]" +
            "inner join[Branchers] on [Branchers].[ID_Branchers] = [Location_Select].[Branchers_ID]" +
            "inner join[Cabinets] on [Cabinets].[ID_Cabinets] = [Location_Select].[Cabinets_ID]",
          qrBranchers = "select * from [Branchers]",
          qrCabinets = "select * from [Cabinets]",
          qrPosition = "select * from [Position]",
          qrFio = "select * from [Fio_View]";

        public static int idMainUser, idPersonal, idAuthUser, IdEquipmentLocation, idEquipment, idEquipRequest, idRequest, idUser, IdLocation, idBranchers, idCabinets, idPosition;

        private SqlCommand command = new SqlCommand("", connection);

        public Int32 Authorization(string login)
        {
            try
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "select [ID_Authorization] from" +
                    "[Authorization] where [Login] = '" + login + "'";
                DBConnection.connection.Open();
                idMainUser = Convert.ToInt32(command.ExecuteScalar().ToString());
                return (idMainUser);
            }
            catch
            {
                idMainUser = 0;
                return (idMainUser);
            }
            finally
            {
                DBConnection.connection.Close();
            }
        }

        public Int32 LoginCheck(string login)
        {
            int loginCheck;
            command.CommandText = "select count (*) from [Authorization] where Login like '%" + login + "%'";
            connection.Open();
            loginCheck = Convert.ToInt32(command.ExecuteScalar().ToString());
            connection.Close();
            return loginCheck;
        }

        public Int32 EmailCheck(string email)
        {
            int emailCheck;
            command.CommandText = "select count (*) from [Users] where Email like '%" + email + "%'";
            connection.Open();
            emailCheck = Convert.ToInt32(command.ExecuteScalar().ToString());
            connection.Close();
            return emailCheck;
        }

        public Int32 PhoneNumberCheck(string phonenumber)
        {
            int PhoneNumberCheck;
            command.CommandText = "select count (*) from [Users] where PhoneNumber like '%" + phonenumber + "%'";
            connection.Open();
            PhoneNumberCheck = Convert.ToInt32(command.ExecuteScalar().ToString());
            connection.Close();
            return PhoneNumberCheck;
        }

        public string UserPosition(Int32 userID)
        {
            string PositionID;
            command.CommandText = "select [Position_ID] from [Users] where [ID_User]  like '%" + idMainUser + "%'";
            connection.Open();
            PositionID = command.ExecuteScalar().ToString();
            connection.Close();
            return PositionID;
        }

        public void getIDPersonal(int idUser)
        {
            try
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "select [ID_User] from [Users] where [Authorization_ID] = '" + idMainUser + "'";
                connection.Open();
                idMainUser = Convert.ToInt32(command.ExecuteScalar().ToString());
            }
            catch
            {
                idMainUser = 0;
            }
            finally
            {
                DBConnection.connection.Close();
            }
        }
    }
}