using FIConfiguration.API_Operations;
using FIConfiguration.BuisnessLayer;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FIConfiguration
{
	public class ValuesController : ApiController
	{
		// GET api/values
		public async Task<string> Get()
		{

			string clientId = "9cf11af2-b7dd-4081-af7f-98f3d34b4737";
			List<string> lst = new List<string>();
			var credentials = new UserPasswordCredential("", "");
			var authContext = new AuthenticationContext("https://login.microsoftonline.com/414e96d3-f8a4-4c10-b323-11fa8ebfe195/oauth2/token"); //https://login.microsoftonline.com/414e96d3-f8a4-4c10-b323-11fa8ebfe195/oauth2/token
																																				//var authContext = new AuthenticationContext("https://login.microsoftonline.com/common/oauth2/authorize");


			var authResult = await authContext.AcquireTokenAsync("https://managment.azure.com/", clientId, credentials);
			if (authResult == null)
			{
				lst.Add("Error");
			}

			return null;

			//var credentials = SdkContext.AzureCredentialsFactory.FromServicePrincipal(clientId, clientSecret, tenantId, AzureEnvironment.AzureGlobalCloud);
			//var azure = Azure.Configure().Authenticate(credentials).WithSubscription(subscriptionID);
			//var resourecelist = azure.ResourceGroups.List().ToList();

			//var serviceCreds = await ApplicationTokenProvider.LoginSilentAsync(tenantId, clientId, secret);
			//var resourceClient = new ResourceManagementClient(serviceCreds);
			//resourceClient.SubscriptionId = subscriptionID;
		}

		// GET api/values/5
		public string Get(int id)
		{
			return "value";
		}

		// POST api/values
		public void Post([FromBody]string value)
		{

		}
		/// <summary>
		/// 1. Create Probe
		/// 2. 
		/// </summary>
		/// <param name="value"></param>
		// PUT api/values/5
		public HttpResponseMessage Put(InputData value)
		{
			RootObject resultObject = null;
			var message = string.Empty;

			switch (value.Operation)
			{
				case "CreateProbe":
					var gatewayObject = GatewayUpdatePropertiesSteps.GetApplicationGateWay(value);
					resultObject = GatewayUpdatePropertiesSteps.AddProbe(gatewayObject, value);
					//var AddedhttpSettingObject = GatewayUpdatePropertiesSteps.AddHTTPSettings(AddedProbeObject, value);
					//var AddBackendAddressPoolObject = GatewayUpdatePropertiesSteps.AddBackendAddressPool(AddedhttpSettingObject, value);
					//var AddRuleObject = GatewayUpdatePropertiesSteps.AddRule(AddBackendAddressPoolObject, value);
					//var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(AddRuleObject);
					//var apiurl = String.Format(Constant.applicationGatewaysAPI, value.SubscriptionId, value.ResourceGroupName, value.ApplicationGatewayName);
					//WebApiOperation.ExecutivePutAPI(apiurl, jsonString, value.brearerToken);
					break;
				case "AddHTTPSettings":
					var appSettingObj = GatewayUpdatePropertiesSteps.GetApplicationGateWay(value);
					resultObject = GatewayUpdatePropertiesSteps.AddHTTPSettings(appSettingObj, value);
					break;
				case "BackendAddressPool":
					var backendAddressPoolObj = GatewayUpdatePropertiesSteps.GetApplicationGateWay(value);
					resultObject = GatewayUpdatePropertiesSteps.AddBackendAddressPool(backendAddressPoolObj, value);
					break;
				case "AddRule":
					var addRuleObj = GatewayUpdatePropertiesSteps.GetApplicationGateWay(value);
					resultObject = GatewayUpdatePropertiesSteps.AddRule(addRuleObj, value);
					break;
				case "UpdateRule":
					var UpdateRuleObj = GatewayUpdatePropertiesSteps.GetApplicationGateWay(value);
					resultObject = GatewayUpdatePropertiesSteps.UpdateRule(UpdateRuleObj, value);
					break;

				default:
					break;
			}

			var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(resultObject);
			var apiurl = String.Format(Constant.applicationGatewaysAPI, value.SubscriptionId, value.ResourceGroupName, value.ApplicationGatewayName);
			string reslt = WebApiOperation.ExecutivePutAPI(apiurl, jsonString, value.brearerToken);

			if (string.IsNullOrEmpty(reslt))
			{
				switch (value.Operation)
				{
					case "CreateProbe":
						message = "Probe cannot be created, Please confirm input and try again !!";
						break;
					case "AddHTTPSettings":
						message = "HTTP Settings cannot be created, Please confirm input and try again !!";
						break;
					case "BackendAddressPool":
						message = "Backend AddressPool cannot be created, Please confirm input and try again !!";
						break;
					case "AddRule":
						message = "Rule could not be created, Please confirm input and try again !!";
						break;
					default:
						break;
				}
				HttpError err = new HttpError(message);
				return Request.CreateResponse(HttpStatusCode.NotFound, err);
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.OK);
			}
		}

		// DELETE api/values/5
		public void Delete(int id)
		{
		}
	}


}
