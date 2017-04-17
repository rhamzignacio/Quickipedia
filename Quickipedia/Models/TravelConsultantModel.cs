using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quickipedia.Models
{
    public class TravelConsultantModel
    {
        public Guid ID { get; set; }
        public string ClientCode { get; set; }
        public string AgentName { get; set; }
        public string AgentNo1A { get; set; }
        public string AgentNo1B { get; set; }
        public string AgentNo5J { get; set; }
        public Guid? AgentID { get; set; }
        public string Status { get; set; }
    }
}