using System.Configuration;
using System.Data;
using CRMLeads.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CRMLeads.Data
{
    public class LeadsRepository
    {
        private readonly SqlConnection _connection;

        public LeadsRepository(IConfiguration configuration)
        {
            string connStr = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(connStr);

        }

        public List<LeadsEntity> GetAllLeads()
        {
            List<LeadsEntity> leadListEntity = new List<LeadsEntity>();
            SqlCommand cmd = new SqlCommand("GetAllLeads", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable Dt = new DataTable();
            dataAdapter.Fill(Dt);

            foreach (DataRow dr in Dt.Rows)
            {
                leadListEntity.Add(
                    new LeadsEntity
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        LeadDate = Convert.ToDateTime(dr["LeadDate"]),
                        Name = dr["Name"].ToString(),
                        EmailAddress = dr["EmailAddress"].ToString(),
                        Mobile = dr["Mobile"].ToString(),
                        LeadSource = dr["LeadSource"].ToString(),
                        LeadStatus = dr["LeadStatus"].ToString(),
                        NextFollowUpDate = Convert.ToDateTime(dr["NextFollowUpDate"])
                    });
            }
            return leadListEntity;
        }


        public LeadsEntity GetLeadsById(int id)
        {
            LeadsEntity leadListEntity = new LeadsEntity();
            SqlCommand cmd = new SqlCommand("GetLeadDetailsById", _connection);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            cmd.Parameters.Add(new SqlParameter("@Id", id));

            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            foreach (DataRow dr in dataTable.Rows)
            {
                leadListEntity= new LeadsEntity
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        LeadDate = Convert.ToDateTime(dr["LeadDate"]),
                        Name = dr["Name"].ToString(),
                        EmailAddress = dr["EmailAddress"].ToString(),
                        Mobile = dr["Mobile"].ToString(),
                        LeadSource = dr["LeadSource"].ToString(),
                        LeadStatus = dr["LeadStatus"].ToString(),
                        NextFollowUpDate = Convert.ToDateTime(dr["NextFollowUpDate"])
                    };
            }
            return leadListEntity;
        }


        public bool AddLead(LeadsEntity lead)
        {
            SqlCommand cmd = new SqlCommand("AddLead", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LeadDate", lead.LeadDate);
            cmd.Parameters.AddWithValue("@Name", lead.Name);
            cmd.Parameters.AddWithValue("@EmailAddress", lead.EmailAddress);
            cmd.Parameters.AddWithValue("@Mobile", lead.Mobile);
            cmd.Parameters.AddWithValue("@LeadSource", lead.LeadSource);
            cmd.Parameters.AddWithValue("@LeadStatus", lead.LeadStatus);
            cmd.Parameters.AddWithValue("@NextFollowUpDate", lead.NextFollowUpDate);
            _connection.Open();
            int i = cmd.ExecuteNonQuery();
            _connection.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool EditLead(int Id,LeadsEntity lead)
        {
            SqlCommand cmd = new SqlCommand("Editlead", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@LeadDate", lead.LeadDate);
            cmd.Parameters.AddWithValue("@Name", lead.Name);
            cmd.Parameters.AddWithValue("@EmailAddress", lead.EmailAddress);
            cmd.Parameters.AddWithValue("@Mobile", lead.Mobile);
            cmd.Parameters.AddWithValue("@LeadSource", lead.LeadSource);
            cmd.Parameters.AddWithValue("@LeadStatus", lead.LeadStatus);
            cmd.Parameters.AddWithValue("@NextFollowUpDate", lead.NextFollowUpDate);
            _connection.Open();
            int i = cmd.ExecuteNonQuery();
            _connection.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteLead(int Id)
        {
            SqlCommand cmd = new SqlCommand("DeleteLeadDetails", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            _connection.Open();
            int i = cmd.ExecuteNonQuery();
            _connection.Close();
            return i >= 1;
        }

    }
}
