using System;
using System.IO;

namespace ImageThumbnailGeneration
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("This is a thumbnail generation sample code, It generates thumbnail from image");
            // Specify source file of original image and target file where thumbnail image is generated.
            // Example Photo by Julia M Cameron: https://www.pexels.com/photo/photo-of-boy-using-vr-headset-4145356/  
            string InputImageFile = Environment.CurrentDirectory + "\\temp\\large-image.jpg";
            Console.WriteLine("Generating thumbnail for image file " + InputImageFile);
            string OutputThumbnailImageFile = Environment.CurrentDirectory + "\\temp\\tumb-image.jpg";
            GenerateThumbnailFromFile(InputImageFile, OutputThumbnailImageFile);
            Console.WriteLine("Thumbnail created at :");
            Console.WriteLine(OutputThumbnailImageFile);
        }

        /// <summary>
        /// Generates thumbnail from image file provided
        /// </summary>
        /// <param name="InputImageFile">Input image file</param>
        /// <param name="OutputThumbnailImageFile">Output file location for thumbnail image. File will be created/overwritten</param>
        static void GenerateThumbnailFromFile(string InputImageFile, string OutputThumbnailImageFile)
        {
            // Open source file as file stream.
            using FileStream inputFileStream = new(InputImageFile, FileMode.Open);
            // Call image thumbnail generator to generate thumnail 
            using (var thumbnailMemStream = ImageThumbnailGenerator.GenerateThumbnail(inputFileStream, 200, 200))
            {
                var outputFileStream = new FileStream(OutputThumbnailImageFile, FileMode.Create, FileAccess.Write);
                thumbnailMemStream.CopyTo(outputFileStream);
                outputFileStream.Dispose();
                thumbnailMemStream.Dispose();
            }
            // Close open streams. 
            inputFileStream.Dispose();
        }
    }
}