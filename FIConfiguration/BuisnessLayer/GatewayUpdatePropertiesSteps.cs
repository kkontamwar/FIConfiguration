using FIConfiguration.API_Operations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FIConfiguration.BuisnessLayer
{
	public static class GatewayUpdatePropertiesSteps
	{

		public static RootObject GetApplicationGateWay(InputData value)
		{
			var geturl = String.Format(Constant.applicationGatewaysAPI, value.SubscriptionId, value.ResourceGroupName, value.ApplicationGatewayName);
			string gatewayDetails = WebApiOperation.ExecutiveGetAPI(geturl, value.brearerToken);
			var result = JsonConvert.DeserializeObject<RootObject>(gatewayDetails);
			return result;
		}


		public static RootObject AddRule(RootObject gatewayObject, InputData value)
		{
			string ruleJson = "{	\"name\": \"RuleName\",	\"id\": \"/subscriptions/5d077714-2b1c-43d0-81f2-bc711d989167/resourceGroups/RuleResourceGroupName/providers/Microsoft.Network/applicationGateways/RuleApplicationGatewayName/urlPathMaps/NowCloudConnectPathRule/pathRules/RuleName\",	\"properties\": {		\"provisioningState\": \"Succeeded\",		\"paths\": [\"RulePath\"],		\"backendAddressPool\": {			\"id\": \"/subscriptions/5d077714-2b1c-43d0-81f2-bc711d989167/resourceGroups/NowCloudConnectRG/providers/Microsoft.Network/applicationGateways/NowCloudConnectAppGW/backendAddressPools/BackendPool\"		},		\"backendHttpSettings\": {			\"id\": \"/subscriptions/5d077714-2b1c-43d0-81f2-bc711d989167/resourceGroups/NowCloudConnectRG/providers/Microsoft.Network/applicationGateways/NowCloudConnectAppGW/backendHttpSettingsCollection/backdHttpSettings\"		},		\"rewriteRuleSet\": null	}}";
			StringBuilder sb = new StringBuilder(ruleJson);
			sb.Replace("RuleResourceGroupName", value.ResourceGroupName);
			sb.Replace("RuleApplicationGatewayName", value.ApplicationGatewayName);
			sb.Replace("RuleName", value.RuleName);
			sb.Replace("RulePath", value.RulePath);
			sb.Replace("BackendPool", value.BckendPoolName);
			sb.Replace("backdHttpSettings", value.HttpSettingsName);

			PathRule PathRuleObject = JsonConvert.DeserializeObject<PathRule>(sb.ToString());

			//gatewayObject.properties.urlPathMaps.where

			gatewayObject.properties.urlPathMaps[0].properties.pathRules.Add(PathRuleObject);
			return gatewayObject;
		}



		public static RootObject UpdateRule(RootObject gatewayObject, InputData value)
		{
			//gatewayObject.properties.urlPathMaps.where
			List<string> lst = new List<string>();
			PathRule PathRuleObject = gatewayObject.properties.urlPathMaps[0].properties.pathRules.Find(x => x.name == value.RuleName);
			PathRuleObject.properties.paths[0] = value.RulePath;
			return gatewayObject;
		}




		public static RootObject AddBackendAddressPool(RootObject gatewayObject, InputData value)
		{
			string backendAddressPoolJson = "{        \"name\": \"bckendPoolName\",        \"id\": \"/subscriptions/5d077714-2b1c-43d0-81f2-bc711d989167/resourceGroups/bckendPoolResourceGroupName/providers/Microsoft.Network/applicationGateways/bckendPoolApplicationGatewayName/backendAddressPools/appGatewayBackendPool\",        \"etag\": \"W/\\\"8f6a4a08-26ab-4810-a9cf-b3c5dc9d24dc\\\"\",        \"properties\": {          \"provisioningState\": \"Succeeded\",          \"backendAddresses\": [            {              \"ipAddress\": \"bckendPoolIPAddress\"            }          ],          \"urlPathMaps\": [],          \"pathRules\": []        },        \"type\": \"Microsoft.Network/applicationGateways/backendAddressPools\" }";
			StringBuilder sb = new StringBuilder(backendAddressPoolJson);
			sb.Replace("bckendPoolName", value.BckendPoolName);
			sb.Replace("bckendPoolResourceGroupName", value.ResourceGroupName);
			sb.Replace("bckendPoolApplicationGatewayName", value.ApplicationGatewayName);
			sb.Replace("bckendPoolIPAddress", value.BckendipAddress);
			BackendAddressPool AddProbeObject = JsonConvert.DeserializeObject<BackendAddressPool>(sb.ToString());
			gatewayObject.properties.backendAddressPools.Add(AddProbeObject);
			return gatewayObject;
		}

		public static RootObject AddHTTPSettings(RootObject gatewayObject, InputData value)
		{
			int HttpSettingsPort = Convert.ToInt32(value.HttpSettingsPort);
			//string httpSettingsJson = "{	\"name\": \"HttpSettingsName\",	\"properties\": {		\"provisioningState\": \"Succeeded\",		\"port\": " + HttpSettingsPort + ",		\"protocol\": \"Http\",		\"cookieBasedAffinity\": \"Disabled\",		\"hostName\": null,		\"pickHostNameFromBackendAddress\": false,		\"affinityCookieName\": \"ApplicationGatewayAffinity\",		\"path\": null,		\"requestTimeout\": 30,		\"urlPathMaps\": [],		\"pathRules\": []	},	\"type\": \"Microsoft.Network/applicationGateways/backendHttpSettingsCollection\"}";

			string httpSettingsJson = "{	\"name\": \"HttpSettingsName\"," +
									   "	\"id\": \"/subscriptions/5d077714-2b1c-43d0-81f2-bc711d989167/resourceGroups/HttpSettingsResourceGroupName/providers/Microsoft.Network/applicationGateways/HttpSettingsApplicationGatewayName/backendHttpSettingsCollection/HttpSettingsName\",	" +
									   "\"etag\": \"W/\\\"311fcf51-d48b-4499-ae01-c27a9ffff531\\\"\",	" +
					 "\"properties\": {		" +
										 "\"provisioningState\": \"Succeeded\",	" +
										 "	\"port\": " + HttpSettingsPort + "," +
										 "		\"protocol\": \"Http\",	" +
										 "	\"cookieBasedAffinity\": \"Disabled\"," +
										 "		\"pickHostNameFromBackendAddress\": false," +
										 "		\"requestTimeout\": 30,	" +
										 "	\"probe\": {" +
										 "			\"id\": \"/subscriptions/5d077714-2b1c-43d0-81f2-bc711d989167/resourceGroups/HttpSettingsResourceGroupName/providers/Microsoft.Network/applicationGateways/HttpSettingsApplicationGatewayName/probes/HttpSettingsProbeName\"		},	" +
		   "	\"urlPathMaps\": [{			\"id\": \"/subscriptions/5d077714-2b1c-43d0-81f2-bc711d989167/resourceGroups/HttpSettingsResourceGroupName/providers/Microsoft.Network/applicationGateways/HttpSettingsApplicationGatewayName/urlPathMaps/NowCloudConnectPathRule\"		}],	" +
		   "	\"pathRules\": []	}," +
		   "	\"type\": \"Microsoft.Network/applicationGateways/backendHttpSettingsCollection\"}";

			StringBuilder sb = new StringBuilder(httpSettingsJson);
			sb.Replace("HttpSettingsResourceGroupName", value.ResourceGroupName);
			sb.Replace("HttpSettingsApplicationGatewayName", value.ApplicationGatewayName);
			sb.Replace("HttpSettingsName", value.HttpSettingsName);
			sb.Replace("HttpSettingsProbeName", value.ProbeName);

			BackendHttpSettingsCollection AddProbeObject = JsonConvert.DeserializeObject<BackendHttpSettingsCollection>(sb.ToString());
			gatewayObject.properties.backendHttpSettingsCollection.Add(AddProbeObject);
			return gatewayObject;
		}

		public static RootObject AddProbe(RootObject gatewayObject, InputData value)
		{
			string probJson = "{        \"name\": \"ProbeName\",        \"id\": \"/subscriptions/5d077714-2b1c-43d0-81f2-bc711d989167/resourceGroups/ProbeResourceGroupName/providers/Microsoft.Network/applicationGateways/ProbeApplicationGatewayName/probes/ProbeName\",        \"etag\": \"W/\\\"8f6a4a08-26ab-4810-a9cf-b3c5dc9d24dc\\\"\",        \"properties\": {          \"provisioningState\": \"Succeeded\",          \"protocol\": \"ProbeProtocol\",          \"host\": \"ProbeHost\",          \"path\": \"Probepath\",          \"interval\": 30,          \"timeout\": 30,          \"unhealthyThreshold\": 20,          \"pickHostNameFromBackendHttpSettings\": false,          \"minServers\": 0,          \"match\": {            \"body\": \"\",            \"statusCodes\": [              \"200-399\"            ]          },          \"backendHttpSettings\": [            {              \"id\": \"/subscriptions/5d077714-2b1c-43d0-81f2-bc711d989167/resourceGroups/NowCloudConnectRG/providers/Microsoft.Network/applicationGateways/NowCloudConnectAppGW/backendHttpSettingsCollection/appGatewayBackendHttpSettings\"            }          ]        },        \"type\": \"Microsoft.Network/applicationGateways/probes\"      }";
			StringBuilder sb = new StringBuilder(probJson);
			sb.Replace("ProbeResourceGroupName", value.ResourceGroupName);
			sb.Replace("ProbeApplicationGatewayName", value.ApplicationGatewayName);
			sb.Replace("ProbeName", value.ProbeName);
			sb.Replace("ProbeHost", value.ProbeHost);
			sb.Replace("ProbeProtocol", value.Protocol);
			sb.Replace("Probepath", value.Probepath);
			object AddProbeObject = JsonConvert.DeserializeObject<Object>(sb.ToString());
			gatewayObject.properties.probes.Add(AddProbeObject);
			return gatewayObject;
		}
	}
}