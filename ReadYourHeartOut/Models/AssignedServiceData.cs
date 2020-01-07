using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadYourHeartOut.Models
{// viewmodel for vores populatelist på edit user
    public class AssignedServiceData
    {
        public int ServiceID { get; set; }

        public string ServiceName { get; set; }

        public bool Assigned { get; set; }

    }
}
