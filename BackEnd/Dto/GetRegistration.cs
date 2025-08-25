using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Dto
{
    public class GetRegistration
    {
        public string id { get; set; }
        public string fullName { get; set; }
        public DateTime createddate { get; set; }
        public string status { get; set; }
        public string registrationcode { get; set; }
        public string cardtype { get; set; }
        public string cccd { get; set; }
    }
}