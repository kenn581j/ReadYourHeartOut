using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadYourHeartOut.Models.Profiles;

namespace ReadYourHeartOut.Data
{
    public class DbInitializer
    {
        public static void Initialize(UserContext context)
        {
            context.Database.EnsureCreated();

            // Look for any users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }
            var services = new Service[]
            {
                new Service{ServiceID=1000,ServiceName="Grammateket", Cost=29.99},
                new Service{ServiceID=1001,ServiceName="MatematikLegFlex", Cost=49.99},
                new Service{ServiceID=1002,ServiceName="IntoWords", Cost=99.99}
            };

            foreach (Service service in services)
            {
                context.Services.Add(service);
            }
            context.SaveChanges();

            var users = new User[]
            {
                new User{UserName="Bob",Email="bob@dot.com",JoinDate=DateTime.Now},
                new User{UserName="John",Email="john@dot.com",JoinDate=DateTime.Now},
                new User{UserName="Jim",Email="Jim@dot.com",JoinDate=DateTime.Now},
                new User{UserName="Gurlie",Email="gurlie@dot.com",JoinDate=DateTime.Now}
            };

            foreach (User user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();



        }
    }
}
