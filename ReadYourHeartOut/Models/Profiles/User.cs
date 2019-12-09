using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReadYourHeartOut.Models.Profiles
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }
        public byte[] RowVersion { get; set; }
        public ICollection<Service> Services { get; set; }

    }
}
