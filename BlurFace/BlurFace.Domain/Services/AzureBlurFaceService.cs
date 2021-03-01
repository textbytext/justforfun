using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BlurFace
{
	public class AzureBlurFaceService : IBlurFaceService
	{
		private readonly IFaceClient faceClient;
		private readonly ILogger<AzureBlurFaceService> logger;
		private readonly ImageProcessor imageProcessor = new ImageProcessor();
		private readonly IAzureFaceConfiguration azureFaceConfiguration;

		public AzureBlurFaceService(
			IAzureFaceConfiguration azureFaceConfiguration,
			ILogger<AzureBlurFaceService> logger)
		{
			this.logger = logger;

			// https://docs.microsoft.com/ru-ru/azure/cognitive-services/face/tutorials/faceapiincsharptutorial

			var client = new FaceClient(
				new ApiKeyServiceClientCredentials(azureFaceConfiguration.Key),
				new System.Net.Http.DelegatingHandler[] { });
			client.Endpoint = azureFaceConfiguration.Endpoint;

			this.faceClient = client;

			this.azureFaceConfiguration = azureFaceConfiguration;
		}

		public async Task<byte[]> Blur(Uri uri)
		{
			logger.LogDebug($"uri: {uri}");

			using var wc = new WebClient();
			var bytes = await wc.DownloadDataTaskAsync(uri);

			logger.LogDebug($"uri: {uri}, size: {bytes.Length}"); 

			return await Blur(bytes);
		}

		public async Task<byte[]> Blur(byte[] imageBytes)
		{
			using var stream = new MemoryStream();
			await stream.WriteAsync(imageBytes);
			stream.Position = 0;
			var detected = await Recognize(stream);

			return imageProcessor.Blur(imageBytes, detected, azureFaceConfiguration.BlurRadius);
		}

		private async Task<IEnumerable<FacePosition>> Recognize(Stream imageStream)
		{
			// The list of detected faces.
			var detectedFaces = await DetectFaces(imageStream);

			// transform
			return detectedFaces.Select(detected =>
			{
				var result = new FacePosition
				{
					Height = detected.FaceRectangle.Height,
					Width = detected.FaceRectangle.Width,
					Left = detected.FaceRectangle.Left,
					Top = detected.FaceRectangle.Top
				};

				logger.LogDebug($"detected: {result}");

				return result;
			}).ToList();
		}

		private async Task<IList<DetectedFace>> DetectFaces(Stream imageStream)
		{
			// The list of Face attributes to return.
			/*IList<FaceAttributeType?> faceAttributes =
				new FaceAttributeType?[]
				{
			FaceAttributeType.Gender, FaceAttributeType.Age,
			FaceAttributeType.Smile, FaceAttributeType.Emotion,
			FaceAttributeType.Glasses, FaceAttributeType.Hair
				};*/

			return await faceClient.Face.DetectWithStreamAsync(imageStream, true, false);
		}

	}
}
