using MySql.Data.MySqlClient;
using Parcial2.Models;
using System.Data;


namespace Parcial2.DatabaseHelper
{
    public class DatabaseHelper
    {
        const string Usuario = @"root";
        const string Password = @"12345678";
        const string servidor = @"Localhost";
        const string baseDatos = "parcial2";
        const string Port = "3306";
        const string strConexion = $"server={servidor};Port={Port};uid={Usuario};pwd={Password};database={baseDatos}";


        public static void InsertUser(User user)
        {
            List<MySqlParameter> paramList = new List<MySqlParameter>()
            {
                new MySqlParameter("pName",user.Name),
                new MySqlParameter("pLastName",user.LastName),
                new MySqlParameter("pEmail",user.Email),
                new MySqlParameter("pPhone",user.Phone),
                new MySqlParameter("pAddres",user.Addres)

            };

            ExecStoreProcedure("Insert_User", paramList);


        }

        public static List<User> GetUsers()
        {
            List<User> userList = new List<User>();
            DataTable ds = ExecuteStoreProcedure("spGetUsers", null);

            foreach (DataRow dr in ds.Rows)
            {
                userList.Add(new User()
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString(),
                    LastName = dr["LastName"].ToString(),
                    Email = dr["Email"].ToString(),
                    Phone = Convert.ToInt32(dr["Phone"]),
                    Addres = dr["Addres"].ToString(),
            
                });
            }

            return userList;
        }

        public static User? GetUser(int id)
        {
            DataTable ds = ExecuteStoreProcedure("spGetUser", new List<MySqlParameter>()
            {
                new MySqlParameter("pId", id)
            });

            if (ds.Rows.Count == 0)
            {
                return null;
            }

            return new User()
            {
                Id = Convert.ToInt32(ds.Rows[0]["Id"]),
                Name = ds.Rows[0]["Name"].ToString(),
                LastName = ds.Rows[0]["LastName"].ToString(),
                Email = ds.Rows[0]["Email"].ToString(),
                Phone = Convert.ToInt32(ds.Rows[0]["Phone"]),
                Addres = ds.Rows[0]["Addres"].ToString(),
               
                
            };
        }

        public static void DeleteUser(int id)
        {
            ExecStoreProcedure("spDeleteUser", new List<MySqlParameter>()
            {
                new MySqlParameter("pId", id),
            });
        }

        public static void UpdateUser(User user)
        {
            List<MySqlParameter> paramList = new List<MySqlParameter>()
            {
                new MySqlParameter("pId", user.Id),
                new MySqlParameter("pName", user.Name),
                new MySqlParameter("pLastName",user.LastName),
                new MySqlParameter("pEmail",user.Email),
                new MySqlParameter("pPhone",user.Phone),
                new MySqlParameter("pAddres",user.Addres)
            };

            ExecStoreProcedure("spUpdateUser", paramList);
        }

        //Para select 
        public static DataTable ExecuteStoreProcedure(string procedure, List<MySqlParameter> param)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(strConexion))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = procedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;

                    if (param != null)
                    {
                        foreach (MySqlParameter item in param)
                        {
                            cmd.Parameters.Add(item);
                        }
                    }

                    cmd.ExecuteNonQuery();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ExecStoreProcedure(string procedure, List<MySqlParameter> param)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(strConexion))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = procedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;

                    if (param != null)
                    {
                        foreach (MySqlParameter item in param)
                        {
                            cmd.Parameters.Add(item);
                        }
                    }

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
