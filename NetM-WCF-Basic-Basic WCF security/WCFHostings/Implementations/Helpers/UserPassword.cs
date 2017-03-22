using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace Implementations.Helpers
{
    public class UserPassword : UserNamePasswordValidator
    {
        public static bool IsUserExist(string userName, string password)
        {
            using (SqlConnection connection = new SqlConnection(NorthwindModel.NorthwindContext.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("IsUserExist", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@name", userName);
                    command.Parameters.AddWithValue("@password", password);

                    connection.Open();
                    var success = command.ExecuteScalar();
                    connection.Close();

                    switch ((int)success)
                    {
                        case 0: return true;
                        case 2: throw new Exception("Пользователя не существует!");
                        case 3: throw new Exception("Неправильный пароль!"); 
                        case 1: throw new Exception("Неопознанная ошибка!"); ;
                        
                    }
                }
            }
            return false;
        }

        public static bool IsManager(string userName)
        {
            using (SqlConnection connection = new SqlConnection(NorthwindModel.NorthwindContext.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("IsManager", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@name", userName);

                    connection.Open();
                    var success = command.ExecuteScalar();
                    connection.Close();

                }
            }
            return false;
        }

        public static bool IsOrderOperation(string userName)
        {
            using (SqlConnection connection = new SqlConnection(NorthwindModel.NorthwindContext.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("IsManager", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@name", userName);

                    connection.Open();
                    var success = command.ExecuteScalar();
                    connection.Close();

                }
            }
            return false;
        }

        public override void Validate(string userName, string password)
        {
            if (IsUserExist(userName, password))
                return;

            throw new SecurityTokenValidationException();
        }


    }
}
