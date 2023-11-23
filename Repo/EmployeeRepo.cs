using EmployeeData.Models;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeData.Repo
{
    public class EmployeeRepo
    {

        public IEnumerable<EmployeeModel> GetAllEmployeeList()
        {
            IEnumerable<EmployeeModel> employees = new List<EmployeeModel>();
            try
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
                IConfiguration configuration = builder.Build();
                string connstring = configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(connstring))
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlCmd = new SqlCommand
                    {
                        CommandText = "Proc_GetEmployeeDetails",
                        CommandType = CommandType.StoredProcedure,
                        Connection = connection
                    };

                    SqlDataAdapter da = new SqlDataAdapter
                    {
                        SelectCommand = sqlCmd
                    };
                    da.Fill(ds);

                    employees = (from DataRow dr in ds.Tables[0].Rows
                                 select new EmployeeModel()
                                 {
                                     EmployeeID=Convert.ToInt32(dr["EmployeeID"]),
                                     EmployeeCode = Convert.ToInt32(dr["EmployeeCode"]),
                                     EmployeeName = Convert.ToString(dr["EmployeeName"]),
                                     DateOfBirth = Convert.ToDateTime(dr["DateofBirth"]),
                                     Gender = Convert.ToBoolean(dr["Gender"]),
                                     Department = Convert.ToString(dr["Department"]),
                                     Designation = Convert.ToString(dr["Designation"]),
                                     BasicSalary = Convert.ToInt32(dr["BasicSalary"])
                                 }).ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return employees;
        }


        internal void SaveEmployeeDetails(EmployeeModel employee, string mode)
        {
            try
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
                IConfiguration configuration = builder.Build();
                string connstring = configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection connection = new SqlConnection(connstring))
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlCmd = new SqlCommand
                    {
                        CommandText = "Proc_SaveEmployeeDetails",
                        CommandType = CommandType.StoredProcedure,
                        Connection = connection 
                    };
                    SqlParameter paramEmployeeID = new SqlParameter
                    {
                        ParameterName = "@EmployeeID",
                        SqlDbType = SqlDbType.Int,
                        Value = employee.EmployeeID, 
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter paramEmployeeCode = new SqlParameter
                    {
                        ParameterName = "@EmployeeCode",
                        SqlDbType = SqlDbType.Int,
                        Value = employee.EmployeeCode,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter paramEmployeeName = new SqlParameter
                    {
                        ParameterName = "@EmployeeName",
                        SqlDbType = SqlDbType.VarChar,
                        Value = employee.EmployeeName,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter paramDateofBirth = new SqlParameter
                    {
                        ParameterName = "@DateofBirth",
                        SqlDbType = SqlDbType.DateTime,
                        Value = employee.DateOfBirth,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter paramGender = new SqlParameter
                    {
                        ParameterName = "@Gender",
                        SqlDbType = SqlDbType.Bit,
                        Value = employee.Gender,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter paramDepartment = new SqlParameter
                    {
                        ParameterName = "@Department",
                        SqlDbType = SqlDbType.VarChar,
                        Value = employee.Department,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter paramDesignation = new SqlParameter
                    {
                        ParameterName = "@Designation",
                        SqlDbType = SqlDbType.VarChar,
                        Value = employee.Designation,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter paramBasicSalary = new SqlParameter
                    {
                        ParameterName = "@BasicSalary",
                        SqlDbType = SqlDbType.Float,
                        Value = employee.BasicSalary,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter parammode = new SqlParameter
                    {
                        ParameterName = "@mode",
                        SqlDbType = SqlDbType.VarChar,
                        Value = mode,
                        Direction = ParameterDirection.Input
                    };

                    sqlCmd.Parameters.Add(paramEmployeeID);
                    sqlCmd.Parameters.Add(paramEmployeeCode);
                    sqlCmd.Parameters.Add(paramEmployeeName);
                    sqlCmd.Parameters.Add(paramDateofBirth);
                    sqlCmd.Parameters.Add(paramGender);
                    sqlCmd.Parameters.Add(paramDepartment);
                    sqlCmd.Parameters.Add(paramDesignation);
                    sqlCmd.Parameters.Add(paramBasicSalary);
                    sqlCmd.Parameters.Add(parammode);
                    connection.Open();
                    sqlCmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
