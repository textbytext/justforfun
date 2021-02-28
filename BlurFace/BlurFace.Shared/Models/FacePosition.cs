namespace BlurFace
{
	public class FacePosition
	{
		public int Width { get; set; }
		public int Height { get; set; }
		public int Left { get; set; }
		public int Top { get; set; }

		public override string ToString()
		{
			return $"Left: {Left}, Top: {Top}, Width: {Width}, Height: {Height}";
		}
	}
}
