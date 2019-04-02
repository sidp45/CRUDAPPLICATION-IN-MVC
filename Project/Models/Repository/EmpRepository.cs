using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project.Models.Repository
{
    public class EmpRepository
    {

        private SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlCon"].ToString();
            con = new SqlConnection(constr);
        }
        //To Add Employee details
        public bool AddEmployee(EmpModel obj)
        {
            connection();
            SqlCommand com = new SqlCommand("AddNewEmpDetails", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Name", obj.Name);
            com.Parameters.AddWithValue("@City", obj.City);
            com.Parameters.AddWithValue("@Address", obj.Address);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
            //To view employee details with generic list
        public List<EmpModel> GetAllEmployees()
        {
            connection();
            List<EmpModel> EmpList = new List<EmpModel>();
            SqlCommand com = new SqlCommand("GetEmployees", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow 
            foreach(DataRow dr in  dt.Rows)
            {
                EmpList.Add(

                  new EmpModel
                  {
                      EmpId=Convert.ToInt32(dr["Id"]),
                      Name=Convert.ToString(dr["Name"]),
                      City=Convert.ToString(dr["City"]),
                      Address=Convert.ToString(dr["Address"])

                  } );
            }
            return EmpList;
        }

        //To Update Employee details
        public bool UpdateEmployee(EmpModel obj)
        {
            connection();
            SqlCommand com = new SqlCommand("UpdateEmpDetails", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@EmpId", obj.EmpId);
            com.Parameters.AddWithValue("@Name", obj.Name);
            com.Parameters.AddWithValue("@City", obj.City);
            com.Parameters.AddWithValue("@Address", obj.Address);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if(i>=1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        //To delete Employee details 
        public bool DeleteEmployee(int id)
        {
            connection();
            SqlCommand com = new SqlCommand("DeleteEmpById", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@EmpId", id);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if(i>=1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public EmpModel getdetail(int Id)
        {
            EmpModel empModel = new EmpModel();
            connection();
            SqlCommand com = new SqlCommand("GetEmployees", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", Id);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            empModel.Name = Convert.ToString(dt.Rows[0]["Name"]);
            empModel.City = Convert.ToString(dt.Rows[0]["City"]);
            empModel.Address = Convert.ToString(dt.Rows[0]["Address"]);
            return empModel;
        }


    }
}