namespace FIConfiguration.BuisnessLayer
{
	public class InputData
	{
		public string Operation { get; set; }
		public string SubscriptionId { get; set; }
		public string ResourceGroupName { get; set; }
		public string ApplicationGatewayName { get; set; }

		public string ProbeName { get; set; }
		public string Protocol { get; set; }
		public string Host { get; set; }
		public string Probepath { get; set; }

		public string HttpSettingsName { get; set; }
		public string HttpSettingsPort { get; set; }
		//public string HttpSettingsProtocol { get; set; }

		public string BckendPoolName { get; set; }
		public string BckendipAddress { get; set; }

		public string RuleName { get; set; }
		public string RulePath { get; set; }

		public string brearerToken { get; set; }
		//brearerToken
	}

}