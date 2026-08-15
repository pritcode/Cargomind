#pragma warning disable OPENAI001
using System.ClientModel.Primitives;
using Azure.Identity;
using OpenAI;
using OpenAI.Responses;

namespace CargoMindApi.Services
{
	public class AiService
	{
		private readonly ResponsesClient _client;

		public AiService()
		{
			const string model = "gpt-5-mini";
			const string endpoint =
				"https://idpreetam-1347-resource.services.ai.azure.com/openai/v1";

			var tokenPolicy = new BearerTokenPolicy(
				new DefaultAzureCredential(),
				"https://ai.azure.com/.default");

			_client = new ResponsesClient(
				authenticationPolicy: tokenPolicy,
				options: new ResponsesClientOptions
				{
					Endpoint = new Uri(endpoint)
				});
		}

		public string Ask(string message)
		{
			var options = new CreateResponseOptions
			{
				Model = "gpt-5-mini",
				InputItems =
				{
					ResponseItem.CreateUserMessageItem(message)
				}
			};

			ResponseResult response = _client.CreateResponse(options);

			return response.GetOutputText();
		}
	}
}
