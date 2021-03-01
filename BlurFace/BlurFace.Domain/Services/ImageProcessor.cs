using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;

namespace BlurFace
{
	public enum ImageQualities
	{
		Low = 1,
		Medium = 2,
		High = 3,
		Original = 4
	}

	public class ImageProcessor
	{
		public byte[] Blur(byte[] image, IEnumerable<FacePosition> rectangles, int blurRadius)
		{
			using var stream2 = new MemoryStream();
			stream2.Write(image);
			stream2.Position = 0;
			return Blur(stream2, rectangles, blurRadius);
		}

		public byte[] Blur(Stream imageStream, IEnumerable<FacePosition> rectangles, int blurRadius)
		{
			using var image = Image.Load(imageStream, out var format);

			foreach (var rectangle in rectangles)
			{
				var rec = new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height);
				image.Mutate(c => c.BoxBlur(blurRadius, rec));
			}
			
			var result = new MemoryStream();
			image.Save(result, format);
			return result.ToArray();
		}
	}
}
