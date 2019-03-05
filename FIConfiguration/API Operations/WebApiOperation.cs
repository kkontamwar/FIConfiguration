using FIConfiguration.Constants;
using System;
using System.Net.Http;
using System.Text;

namespace FIConfiguration.API_Operations
{
	public static class WebApiOperation
	{
		public static string ExecutivePutAPI(string ApiUri, string JsonInput, string brearerToken)
		{
			var httpContent = new StringContent(JsonInput, Encoding.UTF8, "application/json");
			using (var client = new HttpClient())
			{
				try
				{
					client.BaseAddress = new Uri(ApiUri);
					client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", brearerToken);
					var responseTask = client.PutAsync(ApiUri, httpContent);
					responseTask.Wait();
					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						string reslt = result.Content.ReadAsStringAsync().Result;
						return reslt;
					}
					return string.Empty;
				}
				catch (HttpRequestException)
				{
					//Console.WriteLine(e.InnerException.Message);
				}
				return string.Empty;
			}
		}

		public static string ExecutiveGetAPI(string ApiUri, string brearerToken )
		{
			using (var client = new HttpClient())
			{
				try
				{
					client.BaseAddress = new Uri(ApiUri);
					client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", brearerToken);

					var responseTask = client.GetAsync(ApiUri);
					responseTask.Wait();
					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						string reslt = result.Content.ReadAsStringAsync().Result;
						return reslt;
					}
					return string.Empty;
				}
				catch (HttpRequestException)
				{
					//Console.WriteLine(e.InnerException.Message);
				}
				return string.Empty;
			}
		}
	}
}