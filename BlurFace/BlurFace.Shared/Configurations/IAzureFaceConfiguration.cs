namespace BlurFace
{
	public interface IAzureFaceConfiguration
	{
		string Endpoint { get; }
		string Key { get; }
		int BlurRadius { get; }
	}
}
