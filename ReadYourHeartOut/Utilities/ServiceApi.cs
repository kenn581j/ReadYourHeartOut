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

namespace ReadYourHeartOut.Utilities
{
    public class ServiceApi
    {
        private string GetServiceDataLink = "https://localhost:44382/api/Services";
        private string CreateServiceDataLink = "https://localhost:44382/api/Services";
        private string DeleteServiceDataLink = "https://localhost:44382/api/Services";
        private string PutServiceDataLink = "https://localhost:44382/api/Services";

        public List<Service> GetServiceData()
        {
            List<Service> services = new List<Service>();
            string json = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GetServiceDataLink);
            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                json = reader.ReadToEnd();
            }
            services = JsonConvert.DeserializeObject<List<Service>>(json);

            return services;
        }


        public async Task<string> PostServiceData(Service service)
        {
            //laver det opject du vil poste til en json fil som er en string
            string payLoad = JsonConvert.SerializeObject(service);
            //URI som er linket til api
            Uri uri = new Uri(CreateServiceDataLink);

            //laver et httpcontext som indeholder dit payload, måden den skal encode det og media type.
            HttpContent content = new StringContent(payLoad, Encoding.UTF8, "application/json");

            //kald af metoden der poster som får både uri og content som parameter og 
            //som får statuscode tilbage som string
            var response = await UpLoadServiceDataPost(uri, content);

            return response.ToString();
        }

        private async Task<string> UpLoadServiceDataPost(Uri uri, HttpContent content)
        {
            string response = string.Empty;

            using(HttpClient c = new HttpClient())
            {
                HttpResponseMessage result = await c.PostAsync(uri, content);

                //check if statuscode is successfull, hvis ja bliver det gemt i response
                if (result.IsSuccessStatusCode)
                {
                    //ændret at det er content der bliver sendt tilbage
                    //response = result.StatusCode.ToString();
                    response = result.Content.ReadAsStringAsync().Result;
                }
            }

            return response;
        }

        public string DeleteServiceData(int id)
        {
            string result;
            DeleteServiceDataLink = DeleteServiceDataLink + "/" + id;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(DeleteServiceDataLink);
            request.Method = "DELETE";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        public string PutServiceData(int id, Service service)
        {
            if(id != service.ServiceID)
            {
                throw new IndexOutOfRangeException();
            }
            string result = string.Empty;
            string payLoad = JsonConvert.SerializeObject(service);

            Uri uri = new Uri(PutServiceDataLink + "/" + id);

            HttpContent content = new StringContent(payLoad, Encoding.UTF8, "application/json");
            result = UpLoadServiceDataPut(uri, content).ToString();

            return result;
        }

        private async Task<string> UpLoadServiceDataPut(Uri uri, HttpContent content)
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
