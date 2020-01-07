using Newtonsoft.Json;
using ReadYourHeartOut.Models.Profiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
//using ReadYourHeartOut.Models.Profiles;

namespace ReadYourHeartOut.Utilities
{
    public class UserApi
    {
        private string GetUserDataLink = "https://localhost:44382/api/Users";
        private string PostUserDataLink = "https://localhost:44382/api/Users";
        private string DeleteUserDataLink = "https://localhost:44382/api/Users";
        private string PutUserDataLink = "https://localhost:44382/api/Users";




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
        public async Task<string> PostUserData(User user)
        {
            //laver det opject du vil poste til en json fil som er en string
            string payLoad = JsonConvert.SerializeObject(user);
            //URI som er linket til api
            Uri uri = new Uri(PostUserDataLink);

            //laver et httpcontext som indeholder dit payload, måden den skal encode det og media type.
            HttpContent content = new StringContent(payLoad, System.Text.Encoding.UTF8, "application/json");

            //kald af metoden der poster som får både uri og content som parameter og 
            //som får statuscode tilbage som string
            string response = await UpLoadUserDataPost(uri, content);

            return response.ToString();
        }

        private async Task<string> UpLoadUserDataPost(Uri uri, HttpContent content)
        {
            string response = string.Empty;

            using (HttpClient c = new HttpClient())
            {
                HttpResponseMessage result = await c.PostAsync(uri, content);

                //check if statuscode is successfull, hvis ja bliver det gemt i response
                //hvis ikke er den bare tom, kan så tjekkes i controlleren med et try catch
                if (result.IsSuccessStatusCode)
                {
                    response = result.Content.ReadAsStringAsync().Result;
                }
            }

            return response;
        }
        public string DeleteUserData(int id)
        {
            string result;
            DeleteUserDataLink = DeleteUserDataLink + "/" + id;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(DeleteUserDataLink);
            request.Method = "DELETE";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
        public string PutUserData(int id, User user)
        {
            if (id != user.UserID)
            {
                //burde udnytte errorhandler
                throw new IndexOutOfRangeException();
            }
            string result = string.Empty;
            string payLoad = JsonConvert.SerializeObject(user);

            Uri uri = new Uri(PutUserDataLink + "/" + id);

            HttpContent content = new StringContent(payLoad, Encoding.UTF8, "application/json");
            result = UpLoadUserDataPut(uri, content).ToString();

            return result;
        }

        private async Task<string> UpLoadUserDataPut(Uri uri, HttpContent content)
        {
            string response = string.Empty;

            using (HttpClient c = new HttpClient())
            {
                HttpResponseMessage result = await c.PutAsync(uri, content);

                //check if statuscode is successfull, hvis ja bliver det gemt i response
                //hvis ikke er den bare tom, kan så tjekkes i controlleren med et try catch
                if (result.IsSuccessStatusCode)
                {
                    response = result.StatusCode.ToString();
                }
            }

            return response;
        }



    }
}
