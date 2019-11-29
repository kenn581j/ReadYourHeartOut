using Newtonsoft.Json;
using ReadYourHeartOut.Models.Profiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
//using ReadYourHeartOut.Models.Profiles;

namespace ReadYourHeartOut.Utilities
{
    public class UserApi
    {
        private string GetUserDataLink = "https://localhost:44382/api/Users";




        public List<User> GetUserData()
        {
            List<User> users = new List<User>();
            string json = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GetUserDataLink);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using(StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                json = reader.ReadToEnd();
                }
            users = JsonConvert.DeserializeObject<List<User>>(json);

            return users;
        }




    }
}
