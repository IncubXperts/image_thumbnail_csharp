using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;

namespace ImageThumbnailGeneration
{
    public class ImageThumbnailGenerator
    {
        /// <summary>
        /// Generates a thumbnail from an image.
        /// Accepts any image format supported by ImageSharp (JPEG, PNG, GIF, BMP, WebP, TIFF, ...)
        /// and returns the thumbnail encoded as JPEG in a memory stream.
        /// The aspect ratio is preserved: the thumbnail fits within width x height and is never
        /// distorted. Transparency is not preserved because the output is JPEG.
        /// </summary>
        /// <param name="imageStream">Image file as a readable stream</param>
        /// <param name="width">Maximum width in pixels for the thumbnail (must be greater than 0)</param>
        /// <param name="height">Maximum height in pixels for the thumbnail (must be greater than 0)</param>
        /// <returns>Memory stream positioned at the start, containing the JPEG thumbnail. The caller disposes it.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="imageStream"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Width or height is not greater than 0.</exception>
        /// <exception cref="UnknownImageFormatException">The stream is not a supported image.</exception>
        public static Stream GenerateThumbnail(Stream imageStream, int width, int height)
        {
            if (imageStream == null)
            {
                throw new ArgumentNullException(nameof(imageStream));
            }
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Width must be greater than 0.");
            }
            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height), "Height must be greater than 0.");
            }

            // Load the source image from the input stream. 'using' releases the image's memory.
            using var image = Image.Load(imageStream);

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

            return thumbnailStream;
        }
    }
}
