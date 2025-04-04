﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRMLeads.Models
{
    public class LeadsEntity
    {
        [Key] [DisplayName("Lead Id")]
        public int Id { get; set; }
        [DisplayName("Lead Date")]
        [DataType(DataType.Date)]
        public DateTime LeadDate{ get; set; }
        public string Name { get; set; }
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
        public string Mobile { get; set; }
        [DisplayName("Lead Source")]
        public string LeadSource { get; set; }
        [DisplayName("Lead Status")]
        public string LeadStatus { get; set; }
        [DisplayName("Follow Up Date")]
        [DataType(DataType.Date)]
        public DateTime NextFollowUpDate{ get; set; }
    }
}
