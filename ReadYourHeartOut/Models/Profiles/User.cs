using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReadYourHeartOut.Models.Profiles
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }
        public byte[] RowVersion { get; set; }
        public ICollection<ServiceAssignment> ServicesAssignment { get; set; }

    }
}
