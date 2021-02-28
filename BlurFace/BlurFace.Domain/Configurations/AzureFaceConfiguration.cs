namespace BlurFace.Configurations
{
	public class AzureFaceConfiguration : IAzureFaceConfiguration
	{
		public string Endpoint { get; set; }
		public string Key { get; set; }
		public int BlurRadius { get; set; }
	}
}
