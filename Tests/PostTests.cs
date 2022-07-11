namespace APITests.Tests;

using APITests.Helpers;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Net;

public class PostTests
{
    private string url = ConfigValues.BASE_URL;

    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void CheckGetAllPostsReturnsCorrectResponseCode()
    {
        RestClient client = new RestClient(url);
        RestRequest request = new RestRequest("/posts", Method.GET);

        IRestResponse response = client.Execute(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public void CheckGetPostReturnsCorrectResponseCode()
    {
        RestClient client = new RestClient(url);
        RestRequest request = new RestRequest("/posts/1", Method.GET);

        IRestResponse response = client.Execute(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        Console.WriteLine(APISetupHelper.getTokenString());
    }

    [Test]
    public void CheckGetPostReturnsCorrectData()
    {
        RestClient client = new RestClient(url);
        RestRequest request = new RestRequest("/posts/1", Method.GET);

        IRestResponse response = client.Execute(request);

        JObject responseData = JObject.Parse(response.Content);

        Assert.That(responseData["title"].ToString(), Is.EqualTo("His mother had always taught him"));
        //Assert.That(responseData.SelectToken("title").ToString(), Is.EqualTo("sunt aut facere repellat provident occaecati excepturi optio reprehenderit"));
    }
}

