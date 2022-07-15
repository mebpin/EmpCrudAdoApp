using EmpCrudAdoApp.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EmpCrudAdoApp.Repository
{
    public class EmployeeRepo
    {
        
        string conStr = "server = DELL\\SQL2019;database=MyDb; user id= sa;password=timalsina";
            //to add Employee record
        public void AddEmployee(Employee emp)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string insertQuery = $"INSERT INTO employees VALUES(" +
                    $"{emp.Id}, '{emp.Name}','{emp.Address}','{emp.PhoneNo}')";
                SqlCommand cmd = new SqlCommand(insertQuery, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //to get all data from table
        public IEnumerable<Employee> GetAllEmployee()
        {
            List<Employee> empList = new List<Employee>();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                string query = "Select * From employees";
                SqlCommand cm = new SqlCommand(query, con);

                using (SqlDataReader rdr = cm.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        Employee emp = new Employee();
                        emp.Id = Convert.ToInt32(rdr["EmpId"]);
                        emp.Name = rdr["Name"].ToString();
                        emp.Address = rdr["Address"].ToString();
                        emp.PhoneNo = Convert.ToInt64(rdr["PhoneNo"]);
                        empList.Add(emp);
                    }
                }

                con.Close();
            }

            return empList;
        }
        //to get record of particular employee
        public Employee GetEmployeeData(int id)
        { 
            Employee emp= new Employee();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sqlQuery = "SELECT * FROM employees WHERE EmpId='" + id + "'";
                SqlCommand cm = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cm.ExecuteReader();
                while (rdr.Read())
                {
                    emp.Id = Convert.ToInt32(rdr["EmpId"]);
                    emp.Name = rdr["Name"].ToString();
                    emp.Address = rdr["Address"].ToString();
                    emp.PhoneNo = Convert.ToInt64(rdr["PhoneNo"]);
                }
                con.Close();
            }
            return emp;
        }

        //to update the record
        public void UpdateEmployee(Employee emp, int id)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string updateQuery = $"Update employees SET Name='{emp.Name}', Address='{emp.Address}', PhoneNo='{emp.PhoneNo}' Where EmpId='{id}'";
                using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

        }

        //to delete the record
        public void DeleteEmployee(Employee emp, int id)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string updateQuery = $"DELETE FROM employees WHERE EmpId='{id}'";
                using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

        }

    }

}
