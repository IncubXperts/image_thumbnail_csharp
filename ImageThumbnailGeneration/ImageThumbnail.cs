using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace ImageThumbnailGeneration
{
    public class ImageThumbnailGenerator
    {
        /// <summary>
        /// Generates a thumbnail from an image. 
        /// Accepts image as JPG / PNG formats and returns memory stream of thumbnail
        /// It maintains aspect ratio of the image while generating thumbnail
        /// </summary>
        /// <param name="imageStream">Image file as stream</param>
        /// <param name="width">Width in pixels for thumbnail</param>
        /// <param name="height">Height in pixels for thumbnail</param>
        /// <returns></returns>
        public static Stream GenerateThumbnail(Stream imageStream, int width, int height)
        {
            // Load the source image from the input stream
            var image = Image.Load(imageStream);

            // Resize the image to create a thumbnail
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(width, height),
                Mode = ResizeMode.Max
            }));

            // Save the thumbnail image to a memory stream
            var thumbnailStream = new MemoryStream();
            image.Save(thumbnailStream, new JpegEncoder());

            // Reset the memory stream position to the beginning
            thumbnailStream.Position = 0;

            // Return the thumbnail memory stream
            return thumbnailStream;
        }
    }

}

