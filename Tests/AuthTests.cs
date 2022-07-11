using APITests.Helpers;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APITests.Tests
{
    public class AuthTests
    {
        private string url = ConfigValues.BASE_URL;

        // Remove [Test] to stop tests failing prior to update. Duplication for example purposes.
        public void doUserLogin()
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("/api/v1/login", Method.POST);
            request.AddJsonBody(new {
                Email = "email1@gmail.com",
                Password = "passwordval",
            });

            IRestResponse response = client.Execute(request);

            JObject responseData = JObject.Parse(response.Content);

            string respToken = responseData["token_val_to_store"].ToString();

            APISetupHelper.setTokenString(respToken);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}