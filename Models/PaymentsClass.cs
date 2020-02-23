using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Data;

namespace LowCostHousing.Models
{
    public class PaymentsClass
    {
        public string ClientGivenName { get; set; }
        public string ClientFamilyName { get; set; }
        public string ProjectName { get; set; }
        public int MobileNumber { get; set; }
        public int LotNo { get; set; }
        public int ReferenceNumber { get; set; }
        [Key]
        public int ClientRegistrationId { get; set; }
        public int ProjectMasterId { get; set; }
        public double TotalAmount { get; set; }
        public double AmountPaid { get; set; }
        public string PaidOn { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
            
        public int SaveDetails()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-O68M5NP;Initial Catalog=LowCostHousing;User ID=root;Password=root12345;Trusted_Connection=True");
            string query = "INSERT INTO Payments(ClientRegistrationId, TotalAmount,AmountPaid,PaidOn,ReceivedBy,CreatedBy) " + 
                           "values ('" + ClientRegistrationId + "','" + TotalAmount + "','" + AmountPaid + "','" + PaidOn + "',1,1)";
                           
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }

    }
}
