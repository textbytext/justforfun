using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BlurFace
{
	public interface IBlurFaceService
	{
		Task<byte[]> Blur(Uri uri);
		Task<byte[]> Blur(byte[] imageBytes);
	}
}
