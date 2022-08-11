using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmpMaster1.Models
{
    public class EmployeeDB
    {
        string cs = ConfigurationManager.ConnectionStrings["EmpEntities"].ConnectionString;

        public List<Employee> ListAll()
        {
            List<Employee> lst = new List<Employee>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("SelectEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new Employee
                    {
                        EmployeeID = Convert.ToInt32(rdr["EmployeeId"]),
                        Name = rdr["Name"].ToString(),
                        Email = rdr["Email"].ToString(),
                        Phone_No = rdr["Phone_No"].ToString(),
                        Address = rdr["Address"].ToString(),
                        State = rdr["State"].ToString(),
                        City = rdr["City"].ToString(),
                        Agree = Convert.ToBoolean(rdr["Agree"]),
                    });
                }
                return lst;
            }
        }

        public List<State> GetStateListAll()
        {
            List<State> lst = new List<State>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("SelectState", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new State
                    {
                        StateId = Convert.ToInt32(rdr["StateId"]),
                        Name = rdr["Name"].ToString(),
                    });
                }
                return lst;
            }
        }
        public List<City> GetCityListAll(int StateId)
        {
            List<City> lst = new List<City>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("SelectCity", con);
                
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@StateId", StateId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new City
                    {
                        CityId = Convert.ToInt32(rdr["CityId"]),
                        CityName = rdr["CityName"].ToString(),
                    });
                }
                return lst;
            }
        }
        //internal object Update(EmpMaster1.Employee emp)
        //{
        //    throw new NotImplementedException();
        //}

        //internal object Add(object employee)
        //{
        //    throw new NotImplementedException();
        //}

        //Method for Adding an Employee  
        public int Add(Employee emp1)
        {
            try
            {
                int i;

                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("InsertUpdateEmployee", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Id", emp1.EmployeeID);
                    com.Parameters.AddWithValue("@Name", emp1.Name);
                    com.Parameters.AddWithValue("@Email", emp1.Email);
                    com.Parameters.AddWithValue("@State", emp1.State);
                    com.Parameters.AddWithValue("@City", emp1.City);
                    com.Parameters.AddWithValue("@Phone_No", emp1.Phone_No);
                    com.Parameters.AddWithValue("@Address", emp1.Address);
                    com.Parameters.AddWithValue("@Agree", emp1.Agree);
                    com.Parameters.AddWithValue("@Action", "Insert");
                    i = com.ExecuteNonQuery();
                }
                return i;
            }
            catch (Exception xs)
            {

                throw xs;
            }
          
        }

        //Method for Updating Employee record  
        public int Update(Employee emp1)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("InsertUpdateEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", emp1.EmployeeID);
                com.Parameters.AddWithValue("@Name", emp1.Name);
                com.Parameters.AddWithValue("@Email", emp1.Email);
                com.Parameters.AddWithValue("@State", emp1.State);
                com.Parameters.AddWithValue("@City", emp1.City);
                com.Parameters.AddWithValue("@Phone_No", emp1.Phone_No);
                com.Parameters.AddWithValue("@Address", emp1.Address);
                com.Parameters.AddWithValue("@Agree", emp1.Agree);
                com.Parameters.AddWithValue("@Action", "Update");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        //Method for Deleting an Employee  
        public int Delete(int ID)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("DeleteEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", ID);
                i = com.ExecuteNonQuery();
            }
            return i;
        }
    }
}
