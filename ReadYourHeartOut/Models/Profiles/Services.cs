using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadYourHeartOut.Models.Profiles
{
    public class Service
    {
        public int ServicesID { get; set; }
        public string ServiceName { get; set; }
        public double Cost { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
