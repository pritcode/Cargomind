#pragma warning disable OPENAI001
using System.ClientModel.Primitives;
using Azure.Identity;
using CargoMindApi.Models.Entities;
using OpenAI;
using OpenAI.Responses;

namespace CargoMindApi.Services
{
	public class AiService
	{
		private readonly ResponsesClient _client;
		private readonly string _model;

		public AiService(IConfiguration configuration)
		{
			_model = configuration["AI:Model"]!;
			var endpoint = configuration["AI:Endpoint"];

			var tokenPolicy = new BearerTokenPolicy(
				new DefaultAzureCredential(),
				"https://ai.azure.com/.default");

			_client = new ResponsesClient(
				authenticationPolicy: tokenPolicy,
				options: new ResponsesClientOptions
				{
					Endpoint = new Uri(endpoint!)
				});
		}

		public async Task<string> AskAsync(string message)
		{
			var options = new CreateResponseOptions
			{
				Model = _model,
				InputItems =
				{
					ResponseItem.CreateUserMessageItem(message)
				}
			};

			ResponseResult response = await _client.CreateResponseAsync(options);

			return response.GetOutputText();
		}

		public async Task<string> AnalyzeShipmentAsync(Shipment shipment, string question)
		{
			var shipmentContext = $"""
				Shipment Information:
				AWB Number: {shipment.AwbNumber}
				Status: {shipment.Status}
				Current Location: {shipment.CurrentLocation}
				Origin: {shipment.Origin}
				Destination: {shipment.Destination}
				Expected Delivery Date: {shipment.ExpectedDeliveryDate:yyyy-MM-dd}
				Weight: {shipment.RecordedWeightKg} kg
				Dimensions: {shipment.RecordedLengthCm} x {shipment.RecordedWidthCm} x {shipment.RecordedHeightCm} cm
				""";

					var prompt = $"""
				You are CargoMind, an AI assistant for logistics operations.

				Analyze the shipment information below and answer the user's question.

				Only use the shipment information provided.
				If the information is insufficient to answer the question, say so.

				{shipmentContext}

				User question:
				{question}
				""";

				var options = new CreateResponseOptions
				{
					Model = _model,
					InputItems =
					{
						ResponseItem.CreateUserMessageItem(prompt)
					}
				};

				ResponseResult response =
					await _client.CreateResponseAsync(options);

				return response.GetOutputText();
		}
	}
}
