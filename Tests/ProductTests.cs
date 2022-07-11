namespace APITests.Tests;

using APITests.Models;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Net;

public class ProductsTests
{
    private string url = ConfigValues.BASE_URL;
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void CheckGetAllProductsReturnsCorrectResponseCode()
    {
        RestClient client = new RestClient(url);
        RestRequest request = new RestRequest("/products", Method.GET);

        IRestResponse response = client.Execute(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public void CheckGetAllProductsReturnsSpecificItemData()
    {
        RestClient client = new RestClient(url);
        RestRequest request = new RestRequest("/products", Method.GET);

        IRestResponse response = client.Execute(request);

        JObject responseData = JObject.Parse(response.Content);

        //Assert.That(responseData["products"][1]["title"].ToString(), Is.EqualTo("iPhone X"));
        Assert.That(responseData.SelectToken("products[1].title").ToString(), Is.EqualTo("iPhone X"));
    }

    [Test]
    public void CheckGetProductReturnsCorrectResponseCode()
    {
        RestClient client = new RestClient(url);
        RestRequest request = new RestRequest("/products/1", Method.GET);

        IRestResponse response = client.Execute(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public void CheckGetProductReturnsCorrectData()
    {
        RestClient client = new RestClient(url);

        RestRequest request = new RestRequest("/products/1", Method.GET);

        IRestResponse response = client.Execute(request);

        JObject responseData = JObject.Parse(response.Content);

        var titleText = responseData["title"].ToString();

        Assert.That(titleText, Is.EqualTo("iPhone 9"));
    }

    [Test]
    public void CheckThatUserCanCreatePost()
    {
        RestClient client = new RestClient(url);

        RestRequest request = new RestRequest("/products/add", Method.POST);
        request.AddJsonBody(new { name = "My Product" });

        IRestResponse response = client.Execute(request);

        JObject responseData = JObject.Parse(response.Content);

        Console.WriteLine(responseData);

        // Would have expected a 201 but fake API returning 200.
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public void CheckProductsReturnsForbiddenIfNotAuth()
    {
        RestClient client = new RestClient(url);

        RestRequest request = new RestRequest("/auth/products", Method.GET);

        IRestResponse response = client.Execute(request);

        JObject responseData = JObject.Parse(response.Content);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
    }

    [Test]
    // Another example of how we can access response data and make assertions without using JObject.
    public void ProductTestUsingDeserializeMethod()
    {
        // arrange
        RestClient client = new RestClient(url);
        RestRequest request = new RestRequest("/products/1", Method.GET);

        // act
        IRestResponse response = client.Execute(request);

        ProductModel productResponse =
            new JsonDeserializer().
            Deserialize<ProductModel>(response);

        // assert
        Assert.That(productResponse.Title, Is.EqualTo("Banana"));
    }

}

