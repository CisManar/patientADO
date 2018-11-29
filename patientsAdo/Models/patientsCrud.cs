using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace patientsAdo.Models
{
    public class patientsCrud
    {
        private SqlConnection con;
        public void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            con = new SqlConnection(constring);
        }

        public List<patientsModel> getAll()
        {
            /*
            List<patientsModel> patientsList = new List<patientsModel>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            string query = "select * from patients";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader dtreader = cmd.ExecuteReader();
            patientsModel patient = null;
            if(dtreader.HasRows)
            {
                while(dtreader.Read())
                {
                    patient = new patientsModel();
                    patient.ID = Convert.ToInt32(dtreader["ID"]);
                    patient.fname = dtreader["fname"].ToString();
                    patient.mname = dtreader["mname"].ToString();
                    patient.lname = dtreader["lname"].ToString();
                    patient.gender = Convert.ToInt32(dtreader["gender"]);
                    patient.email = dtreader["email"].ToString();
                    patient.status = Convert.ToInt32(dtreader["status"]);
                    patient.Active = Convert.ToInt32(dtreader["Active"]);
                    patient.creationDate = Convert.ToDateTime(dtreader["creationDate"]);
                    try
                    {
                        patient.CreatedBy = Convert.ToInt32(dtreader["CreatedBy"]);
                    }
                    catch(Exception ex) { };

                    patientsList.Add(patient);
                }
               
            }
            return patientsList;
            */

            connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("getAll",con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dtreader = cmd.ExecuteReader();
            patientsModel patient = null;
            List<patientsModel> patientsList = new List<patientsModel>();
            if (dtreader.HasRows)
            {
                while(dtreader.Read())
                {
                    patient = new patientsModel();
                    patient.ID = Convert.ToInt32(dtreader["ID"]);
                    patient.fname = dtreader["fname"].ToString();
                    patient.mname = dtreader["mname"].ToString();
                    patient.lname = dtreader["lname"].ToString();
                    patient.gender = Convert.ToInt32(dtreader["gender"]);
                    patient.email = dtreader["email"].ToString();
                    patient.lastCheck = Convert.ToDateTime(dtreader["lastCheck"]);
                    patient.status = Convert.ToInt32(dtreader["status"]);
                    patient.Active = Convert.ToInt32(dtreader["Active"]);
                    patient.creationDate = Convert.ToDateTime(dtreader["creationDate"]);
                    try
                    {
                        patient.CreatedBy = Convert.ToInt32(dtreader["CreatedBy"]);
                    }
                    catch (Exception ex) { };

                    patientsList.Add(patient);
                }
            }
            return patientsList;
            }

        public patientsModel getById(int id)
        {
            patientsModel patient = new patientsModel();
            connection();
            SqlCommand cmd = new SqlCommand("getById", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@ID", id);

            SqlDataReader dtreader = cmd.ExecuteReader();
           
            if (dtreader.HasRows)
            {
                while (dtreader.Read())
                {
                    patient = new patientsModel();
                    patient.ID = Convert.ToInt32(dtreader["ID"]);
                    patient.fname = dtreader["fname"].ToString();
                    patient.mname = dtreader["mname"].ToString();
                    patient.lname = dtreader["lname"].ToString();
                    patient.gender = Convert.ToInt32(dtreader["gender"]);
                    patient.email = dtreader["email"].ToString();
                    patient.status = Convert.ToInt32(dtreader["status"]);
                    patient.lastCheck = Convert.ToDateTime(dtreader["lastCheck"]);
                    patient.Active = Convert.ToInt32(dtreader["Active"]);
                    patient.creationDate = Convert.ToDateTime(dtreader["creationDate"]);
                    try
                    {
                        patient.CreatedBy = Convert.ToInt32(dtreader["CreatedBy"]);
                    }
                    catch (Exception ex) { };
                }
            }
            return patient;
        }

        public bool Add(patientsModel patient)
        {
            DateTime creationDate = DateTime.Now;
            connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("Add", con);
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", patient.ID);
            cmd.Parameters.AddWithValue("@fname", patient.fname);
            cmd.Parameters.AddWithValue("@mname", patient.mname);
            cmd.Parameters.AddWithValue("@lname", patient.lname);
            cmd.Parameters.AddWithValue("@gender", patient.gender);
            cmd.Parameters.AddWithValue("@email", patient.email);
            cmd.Parameters.AddWithValue("@lastCheck", creationDate);
            cmd.Parameters.AddWithValue("@status", patient.status);
            cmd.Parameters.AddWithValue("@Active", patient.Active);
            cmd.Parameters.AddWithValue("@creationDate", creationDate);
            cmd.Parameters.AddWithValue("@CreatedBy", 1);

            int i = Convert.ToInt32(cmd.ExecuteNonQuery());
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

        public bool delete(int id)
        {
            DateTime creationDate = DateTime.Now;
            connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete", con);
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ID", id);
            int i = Convert.ToInt32(cmd.ExecuteNonQuery());
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

        public bool update(patientsModel patient)
        {
            // DateTime lastCheck = DateTime.MinValue;

            Nullable <DateTime> lastCheck = null;

            connection();
            SqlCommand cmd = new SqlCommand("Update", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@ID", patient.ID);
            cmd.Parameters.AddWithValue("@fname", patient.fname);
            cmd.Parameters.AddWithValue("@mname", patient.mname);
            cmd.Parameters.AddWithValue("@lname", patient.lname);
            cmd.Parameters.AddWithValue("@gender", patient.gender);
            cmd.Parameters.AddWithValue("@email", patient.email);
            cmd.Parameters.AddWithValue("@lastCheck", lastCheck);
            cmd.Parameters.AddWithValue("@status", patient.status);
            cmd.Parameters.AddWithValue("@Active", patient.Active);
            cmd.Parameters.AddWithValue("@creationDate", patient.creationDate);
            cmd.Parameters.AddWithValue("@CreatedBy", 1);
       
            con.Open();

            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

            /*
      cmd.Parameters.Add("@ID", SqlDbType.Int).Value = patient.ID;
      cmd.Parameters.Add("@fname",SqlDbType.VarChar).Value = patient.fname;
      cmd.Parameters.Add("@mname", SqlDbType.VarChar).Value = patient.mname;
      cmd.Parameters.Add("@lname", SqlDbType.VarChar).Value = patient.lname;
      cmd.Parameters.Add("@gender", SqlDbType.Int).Value = patient.gender;
      cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = patient.email;
      cmd.Parameters.Add("@lastCheck", SqlDbType.DateTime).Value = lastCheck;
      cmd.Parameters.Add("@status", SqlDbType.Int).Value = patient.status;
      cmd.Parameters.Add("@Active", SqlDbType.Int).Value = patient.Active;
      cmd.Parameters.Add("@creationDate", SqlDbType.DateTime).Value = patient.creationDate;
      cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = 1;
      */
        }
    }
}