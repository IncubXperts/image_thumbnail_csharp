using SkiaSharp;
using System;
using System.IO;

namespace ImageThumbnailGeneration
{
    /// <summary>Generates image thumbnails.</summary>
    public class ImageThumbnailGenerator
    {
        private const int JpegQuality = 75;

        /// <summary>
        /// Generates a thumbnail from an image.
        /// Accepts any image format supported by SkiaSharp (JPEG, PNG, GIF, BMP, WebP, ...)
        /// and returns the thumbnail encoded as JPEG in a memory stream.
        /// The aspect ratio is preserved: the thumbnail fits within width x height and is never
        /// distorted. Transparent areas are flattened onto a white background because JPEG has no alpha.
        /// </summary>
        /// <param name="imageStream">Image file as a readable stream</param>
        /// <param name="width">Maximum width in pixels for the thumbnail (must be greater than 0)</param>
        /// <param name="height">Maximum height in pixels for the thumbnail (must be greater than 0)</param>
        /// <returns>Memory stream positioned at the start, containing the JPEG thumbnail. The caller disposes it.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="imageStream"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Width or height is not greater than 0.</exception>
        /// <exception cref="InvalidDataException">The stream is not a supported image.</exception>
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

            // Load the source image from the input stream (works for non-seekable streams too).
            using SKData data = SKData.Create(imageStream);
            using SKBitmap? source = data == null ? null : SKBitmap.Decode(data);
            if (source == null || source.Width <= 0 || source.Height <= 0)
            {
                throw new InvalidDataException("The stream does not contain a supported image.");
            }

            // Fit the image inside width x height, keeping the aspect ratio.
            double scale = Math.Min((double)width / source.Width, (double)height / source.Height);
            int targetWidth = Math.Max(1, (int)Math.Round(source.Width * scale));
            int targetHeight = Math.Max(1, (int)Math.Round(source.Height * scale));

            // Draw onto an opaque white canvas, since JPEG cannot store transparency.
            var info = new SKImageInfo(targetWidth, targetHeight, SKColorType.Rgba8888, SKAlphaType.Opaque);
            using var target = new SKBitmap(info);
            using (var canvas = new SKCanvas(target))
            {
                canvas.Clear(SKColors.White);
                using SKImage sourceImage = SKImage.FromBitmap(source);
                canvas.DrawImage(sourceImage, new SKRect(0, 0, targetWidth, targetHeight),
                    new SKSamplingOptions(SKCubicResampler.Mitchell));
            }

            // Encode the thumbnail as JPEG into a memory stream
            using SKImage thumbnail = SKImage.FromBitmap(target);
            using SKData encoded = thumbnail.Encode(SKEncodedImageFormat.Jpeg, JpegQuality);
            var thumbnailStream = new MemoryStream();
            encoded.SaveTo(thumbnailStream);

            // Reset the memory stream position to the beginning
            thumbnailStream.Position = 0;
            return thumbnailStream;
        }
    }
}
