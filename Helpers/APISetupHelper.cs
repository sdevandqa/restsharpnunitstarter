using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITests.Helpers
{
    public static class APISetupHelper
    {
        // Can extend this to store and pass data between different tests/api calls if needed.
        private static string Token = "";

        public static string getTokenString()
        {
            return Token;
        }
        public static void setTokenString(string newToken)
        {
            Token = newToken;
        }

        public static void doLogin()
        {
            RestClient client = new RestClient(ConfigValues.BASE_URL);
            RestRequest request = new RestRequest("/api/v1/login", Method.POST);
            request.AddJsonBody(new
            {
                email = "email1@email.com",
                Password = "passwordval",
            });

            IRestResponse response = client.Execute(request);

            JObject responseData = JObject.Parse(response.Content);

            string respToken = responseData["Token"].ToString();

            setTokenString(respToken);
        }
    }
}
