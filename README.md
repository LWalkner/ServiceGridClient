# Rest Client for Service Grid
This example show you how you could access the Service Grid API in C#.

---
## Dependencies

### used packages / libraries
- __RestSharp__  
_library used for HTTP requests_
- __Newtonsoft__  
_used for handling JSON responses_

### You can find the documentation of the libraries here:
- RestSharp: https://restsharp.dev/getting-started/getting-started.html
- Newtonsoft: https://www.newtonsoft.com/json/help/html/Introduction.htm

---
## Usage

Change the values of the _clientId_ and _clientSecret_ variables to your Clients Id and Secret like it is specified in the Identity Management.  
Also make sure to change the _serviceGridEndpoint_ and set it to your desired Endpoint.

```Csharp
//This client must be available at the Identity Management 
var clientId = "yourClientId";
//Copy the client secret from the Identity management in the following line
var clientSecret = "yourClientSecret";
//enter the endpoint for your service grid
var serviceGridEndpoint = "yourEndpoint";
```
---
## Program description
The program starts with requesting the access token by sending the client Id and client secret to an endpoint.
Be careful, because the program does not validate the SSL certificate and ignores all errors.  
The access token is obtained from the response.  
With the access token the Api path for the datasources is requested. The response is printed in the console.

