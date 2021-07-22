using System;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace API_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //This client must be available at the Identity Management 
            var clientId = "yourClientId";
            //Copy the client secret from the Identity management in the following line
            var clientSecret = "yourClientSecret";
            //enter the endpoint for your service grid
            var serviceGridEndpoint = "yourEndpoint";

            var tokenApiPath = "/connect/token";
            var datasourceApiPath = "/api/v1/datasources";
            var identityServicePort = "9430";
            var apiPort = "9400";

            //Get the Accesstoken with Client Credentials
            var accessTokenClient = new RestClient(serviceGridEndpoint + ":" + identityServicePort + tokenApiPath);
            //ignore ssl error in this example by bypassing ssl validation check
            accessTokenClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true; 

            var tokenRequest = new RestRequest(Method.POST);
            tokenRequest.AddHeader("Accept", "application/json");
            tokenRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            tokenRequest.AddParameter("grant_type", "client_credentials");
            tokenRequest.AddParameter("client_id", clientId);
            tokenRequest.AddParameter("client_secret", clientSecret);

            var tokenResponse = accessTokenClient.Execute(tokenRequest);
            if (tokenResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Error: " + tokenResponse.StatusCode);
                return;
            }
            var tokenObj = JObject.Parse(tokenResponse.Content);
            var accessToken = tokenObj.GetValue("access_token").ToString();

            //Call api with token
            var apiClient = new RestClient(serviceGridEndpoint + ":" + apiPort + datasourceApiPath);
            //ignore ssl error in this example by bypassing ssl validation check
            apiClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            var dataSourcesRequest = new RestRequest(Method.GET);
            dataSourcesRequest.AddHeader("Authorization", "Bearer " + accessToken);

            var dataSourcesResponse = apiClient.Execute(dataSourcesRequest);
            if (dataSourcesResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var dataSourcesObj = JObject.Parse(dataSourcesResponse.Content);
                //example output
                //{
                //  "dataSources": [
                //    {
                //      "name": "SG_DEMO",
                //      "dataSourceId": "16f1b938-e5c4-42eb-87af-663a6077fc1a",
                //      "state": "Offline"
                //    }
                //  ]
                //}
                Console.WriteLine(dataSourcesObj);
            }
            else
            {
                Console.WriteLine("Error: " + dataSourcesResponse.StatusCode);
            }

        }
    }
}
