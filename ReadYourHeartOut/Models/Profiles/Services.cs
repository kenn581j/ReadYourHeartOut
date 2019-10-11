using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReadYourHeartOut.Models.Profiles
{
    public class Service
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public double Cost { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public User Users { get; set; }
    }
}
