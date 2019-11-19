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
            //context.Database.EnsureCreated();

            //// Look for any users.
            //if (context.Users.Any())
            //{
            //    return;   // DB has been seeded
            //}

            //var users = new User[]
            //{
            //    new User{UserName="Bob",Email="bob@dot.com",JoinDate=DateTime.Now},
            //    new User{UserName="John",Email="john@dot.com",JoinDate=DateTime.Now},
            //    new User{UserName="Jim",Email="Jim@dot.com",JoinDate=DateTime.Now},
            //    new User{UserName="Gurlie",Email="gurlie@dot.com",JoinDate=DateTime.Now}
            //};

            //foreach (User user in users)
            //{
            //    context.Users.Add(user);
            //}
            //context.SaveChanges();

            //var services = new Service[]
            //{
            //    new Service{ServiceID=1000,ServiceName="Grammateket", Cost=29.99},
            //    new Service{ServiceID=1001,ServiceName="MatematikLegFlex", Cost=49.99},
            //    new Service{ServiceID=1002,ServiceName="IntoWords", Cost=99.99}
            //};

            //foreach (Service service in services)
            //{
            //    context.Services.Add(service);
            //}
            //context.SaveChanges();
            context.Database.EnsureCreated();
            User[] users = new User[4];
            Service[] services = new Service[5];

            if (!context.Services.Any())
            {
                services = new Service[]
                {
                new Service{ServiceName="Grammateket", Cost=29.99},
                new Service{ServiceName="MatematikLegFlex", Cost=49.99},
                new Service{ServiceName="IntoWords", Cost=99.99},
                new Service{ServiceName="OutroWords", Cost=99.99},
                new Service{ServiceName="Outrotek", Cost=39.99}
                };

                foreach (Service s in services)
                {
                    context.Services.Add(s);
                }
                //context.SaveChanges(); 
            }

            // Look for any users.
            if (!context.Users.Any())
            {
                users = new User[]
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
                //context.SaveChanges();
            }


            if (!context.ServiceAssignments.Any())
            {
                var serviceAssignments = new ServiceAssignment[]
                {
                new ServiceAssignment{
                ServiceID = services.Single(s => s.ServiceName == "Grammateket").ServiceID,
                UserID = users.Single(u => u.UserName == "Bob").UserID
                },
                new ServiceAssignment{
                ServiceID = services.Single(s => s.ServiceName == "MatematikLegFlex").ServiceID,
                UserID = users.Single(u => u.UserName == "Bob").UserID
                },
                new ServiceAssignment{
                ServiceID = services.Single(s => s.ServiceName == "IntoWords").ServiceID,
                UserID = users.Single(u => u.UserName == "Bob").UserID
                },
                new ServiceAssignment{
                ServiceID = services.Single(s => s.ServiceName == "MatematikLegFlex").ServiceID,
                UserID = users.Single(u => u.UserName == "John").UserID
                },
                new ServiceAssignment{
                ServiceID = services.Single(s => s.ServiceName == "Grammateket").ServiceID,
                UserID = users.Single(u => u.UserName == "John").UserID
                },
                new ServiceAssignment{
                ServiceID = services.Single(s => s.ServiceName == "IntoWords").ServiceID,
                UserID = users.Single(u => u.UserName == "Jim").UserID
                },
                new ServiceAssignment{
                ServiceID = services.Single(s => s.ServiceName == "OutroWords").ServiceID,
                UserID = users.Single(u => u.UserName == "Gurlie").UserID},
                };

                foreach (ServiceAssignment serviceAssignemt in serviceAssignments)
                {
                    context.ServiceAssignments.Add(serviceAssignemt);
                }
                context.SaveChanges();
            }


        }
    }
}
