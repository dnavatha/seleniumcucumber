using System;
using System.Text;
using OpenQA.Selenium;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using FTADOTAutomation.Driver;

namespace FTADOTAutomation.Helpers
{
    public class RestAPI
    {

        public static List<(string key, double seconds)> timeSpans = new List<(string key, double seconds)>();
        public static dynamic GET( String URL)
        {
            return GET(URL, new AuthenticationHeaderValue("Bearer", getAuthToken()));
        }
        public static dynamic GET(String URL, AuthenticationHeaderValue Authorization, Dictionary<string, string> headers = null)
        {

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = Authorization;
            client.DefaultRequestHeaders.Add("User-Agent", "Apollo Automation");

            if (headers != null)
            {
                foreach(var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            DateTime start = DateTime.Now;
            HttpResponseMessage response = client.GetAsync(processURL(URL)).Result;
            TimeSpan requestTime = DateTime.Now - start;

            timeSpans.Add(($"[GET] {URL}", requestTime.TotalSeconds));

            client.Dispose();
            return ConsumeResponse(response, URL);
        }
        public static dynamic POST(String URL, dynamic body)
        {
            return POST(URL, body, new AuthenticationHeaderValue("Bearer", getAuthToken()));
        }

        public static dynamic POST(String URL, dynamic body, AuthenticationHeaderValue Authorization)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(processURL(URL));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = Authorization;
            client.Timeout = TimeSpan.FromSeconds(600);

            String bodyString;
            if (body is String)
            {
                bodyString = body;
            }
            else
            {
                bodyString = JsonConvert.SerializeObject(body);
            }

            HttpContent content = new StringContent(bodyString, Encoding.UTF8, "application/json");

            DateTime start = DateTime.Now;

            HttpResponseMessage response = client.PostAsync(processURL(URL), content).Result;

            TimeSpan requestTime = DateTime.Now - start;
            timeSpans.Add(($"[POST] {URL}", requestTime.TotalSeconds));


            client.Dispose();
            return ConsumeResponse(response, URL, body);
        }
        public static dynamic POST(String URL, AuthenticationHeaderValue Authorization, HttpContent content)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(processURL(URL));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = Authorization;
            client.Timeout = TimeSpan.FromSeconds(280);

            DateTime start = DateTime.Now;

            HttpResponseMessage response = client.PostAsync(processURL(URL), content).Result;

            TimeSpan requestTime = DateTime.Now - start;
            timeSpans.Add(($"[POST] {URL}", requestTime.TotalSeconds));

            client.Dispose();
            return ConsumeResponse(response, URL, content);
        }


        public static dynamic PATCH(String URL, dynamic body)
        {
            return PATCH(URL, body, new AuthenticationHeaderValue("Bearer", getAuthToken()));
        }

        public static dynamic PATCH(String URL, dynamic body, AuthenticationHeaderValue Authorization)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(processURL(URL));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = Authorization;

            String bodyString;
            if (body is String)
            {
                bodyString = body;
            }
            else
            {
                bodyString = JsonConvert.SerializeObject(body);
            }

            HttpContent content = new StringContent(bodyString, Encoding.UTF8, "application/json");

            DateTime start = DateTime.Now;

            HttpResponseMessage response = client.PatchAsync(processURL(URL), content).Result;

            TimeSpan requestTime = DateTime.Now - start;
            timeSpans.Add(($"[PATCH] {URL}", requestTime.TotalSeconds));

            client.Dispose();
            return ConsumeResponse(response, URL,body);
        }

        private static String processURL(String URL)
        {
            if (!URL.StartsWith("http"))
            {
                if (!URL.StartsWith("/"))
                {
                    return Environment.GetEnvironmentVariable("SERVER_HOST") + "/"+URL;
                }
                return Environment.GetEnvironmentVariable("SERVER_HOST") + URL;
            }
            return URL;
        }

        private static dynamic ConsumeResponse(HttpResponseMessage response, string URL, dynamic body=null)
        {
            if(!response.IsSuccessStatusCode)
            {
                Log.Critical(processURL(URL));
                Log.Critical(body); 
            }
            response.EnsureSuccessStatusCode();
            String dataObjects = response.Content.ReadAsStringAsync().Result;
            response.Dispose();

            try
            {
                return JsonConvert.DeserializeObject<dynamic>(dataObjects);
            }catch(JsonReaderException)
            {
                return dataObjects;
            }
        }

        private static String getAuthToken()
        {
            if(Setup.isNoBrowserFeature)
            {
                return getAuthTokenAPI();
            }
            String currentUser= (String)((IJavaScriptExecutor)Driver.Setup.driver
                ).ExecuteScript("return window.localStorage.getItem('currentUser')");
            return (String)JsonConvert.DeserializeObject<dynamic>(currentUser)["accessToken"];
        }

        private static String _APIToken = null;

        private static String getAuthTokenAPI()
        {
            if (_APIToken == null)
            {
                string TenantId = Environment.GetEnvironmentVariable(Environment.GetEnvironmentVariable("API_TENANT_ID_SECRETNAME"));
                string ClientId = Environment.GetEnvironmentVariable(Environment.GetEnvironmentVariable("API_CLIENT_ID_SECRETNAME"));
                string ClientSecret = Environment.GetEnvironmentVariable(Environment.GetEnvironmentVariable("API_CLIENT_SECRET_SECRETNAME"));
                string Username = Environment.GetEnvironmentVariable(Environment.GetEnvironmentVariable("API_USERNAME_SECRETNAME"));
                string Password = Environment.GetEnvironmentVariable(Environment.GetEnvironmentVariable("API_PASSWORD_SECRETNAME"));

                HttpContent content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string,string>("grant_type",       "PASSWORD" ),
                new KeyValuePair<string,string>("client_id",        ClientId ),
                new KeyValuePair<string,string>("client_secret",    ClientSecret ),
                new KeyValuePair<string,string>("scope",            $"openid {ClientId}/.default" ),
                new KeyValuePair<string,string>("userName",         Username ),
                new KeyValuePair<string,string>("Password",         Password )
            });

                var response = POST($"https://login.microsoftonline.com/{TenantId}/oauth2/v2.0/token", null, content);

                _APIToken = response["access_token"];
            }
            return _APIToken;
        }
    }
}
