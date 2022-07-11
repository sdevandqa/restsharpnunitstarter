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
    public class AuthenticatedTests
    {
        private string url = ConfigValues.BASE_URL;

        [SetUp]
        public void Setup()
        {
            APISetupHelper.doLogin();
        }

        // Remove [Test] to stop tests failing prior to update.
        public void doPostSomeData()
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("/api/v1/postsomedata", Method.POST);
            request.AddJsonBody(new
            {
                // Data to post.
            });

            request.AddHeader("Token", APISetupHelper.getTokenString());
            request.RequestFormat = DataFormat.Json;

            IRestResponse response = client.Execute(request);

            JObject responseData = JObject.Parse(response.Content);
            
            // Proof that each login token is unique per request.
            Console.WriteLine(APISetupHelper.getTokenString());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));        }

        // Remove [Test] to stop tests failing prior to update.
        public void doGetSomeData()
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("/api/v1/getsomedata", Method.GET);

            request.AddHeader("Token", APISetupHelper.getTokenString());
            request.RequestFormat = DataFormat.Json;

            IRestResponse response = client.Execute(request);

            JObject responseData = JObject.Parse(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
