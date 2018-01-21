using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace OrgChart.Models
{
    public static class Extensions
    {
        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            List<T> result = new List<T>();

            foreach (var row in table.Rows)
            {
                var item = CreateItemFromRow<T>((DataRow)row, properties);
                result.Add(item);
            }

            return result;
        }

        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            foreach (var property in properties)
            {
                if (row[property.Name] == DBNull.Value)
                    property.SetValue(item, null, null);
                else
                    property.SetValue(item, row[property.Name], null);
            }
            return item;
        }


    }

    public class SqlDb
    {
        public static Employee GetEmployeeDetails(int id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlDB"].ConnectionString);
            Employee result = new Employee();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_employee_details", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Employee_id", id));
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            result.Id = (int)reader[0];
            result.Name = (string)reader[1];
            result.Role = (string)reader[2];
            result.WorkLocation = (string)reader[3];
            result.DepartmentName = (string)reader[4];
            result.Reporties = (int)reader[5];
            result.relationship = (string)reader[6];
            con.Close();
            return result;


        }
        //Child
        public static List<Employee> GetListSubordinates(int managerId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlDB"].ConnectionString);
            using (con)
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("sp_child", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Employee_id", managerId));
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                return ds.Tables[0].ToList<Employee>();

            }


        }
        //sibling
        public static List<Employee> GetSiblings(int employeeId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlDB"].ConnectionString);
            using (con)
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("sp_sibling", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Employee_id", employeeId));
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                return ds.Tables[0].ToList<Employee>();

            }


        }
        //Manager
        public static Employee GetManagerDetails(int employeeId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlDB"].ConnectionString);
            Employee result = new Employee();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_manager", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Employee_id", employeeId));
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            result.Id = (int)reader[0];
            result.Name = (string)reader[1];
            result.Role = (string)reader[2];
            result.WorkLocation = (string)reader[3];
            result.DepartmentName = (string)reader[4];
            result.Reporties = (int)reader[5];
            result.relationship = (string)reader[6];
            con.Close();
            return result;


        }
        //init
        public static Employee GetInit()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlDB"].ConnectionString);
            Employee result = new Employee();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Init", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            result.Id = (int)reader[0];
            result.Name = (string)reader[1];
            result.Role = (string)reader[2];
            result.WorkLocation = (string)reader[3];
            result.DepartmentName = (string)reader[4];
            result.Reporties = (int)reader[5];
            result.relationship = (string)reader[6];
            con.Close();
            return result;


        }


    }
}