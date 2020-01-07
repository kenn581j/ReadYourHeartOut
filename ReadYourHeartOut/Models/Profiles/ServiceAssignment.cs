using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadYourHeartOut.Models.Profiles
{
    public class ServiceAssignment
    {
        public int ServiceID { get; set; }
        public int UserID { get; set; }

        //navigation properties
        public Service Service { get; set; }
        public User User { get; set; }
    }
}
