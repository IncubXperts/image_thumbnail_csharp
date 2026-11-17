using ImageThumbnailGeneration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.IO;
using Xunit;

namespace ImageThumbnailGeneration.Tests
{
    public class ImageThumbnailGeneratorTests
    {
        private static MemoryStream CreateImage(int width, int height, bool png = false)
        {
            using var image = new Image<Rgba32>(width, height, new Rgba32(200, 100, 50, 255));
            var ms = new MemoryStream();
            if (png) image.Save(ms, new PngEncoder()); else image.Save(ms, new JpegEncoder());
            ms.Position = 0;
            return ms;
        }

        private static Image Load(Stream s) => Image.Load(s);

        [Fact]
        public void LandscapeImage_IsScaledToFitWidth_KeepingAspectRatio()
        {
            using var input = CreateImage(1000, 500);
            using var thumb = ImageThumbnailGenerator.GenerateThumbnail(input, 200, 200);
            using var result = Load(thumb);
            Assert.Equal(200, result.Width);
            Assert.Equal(100, result.Height);
        }

        [Fact]
        public void PortraitImage_IsScaledToFitHeight_KeepingAspectRatio()
        {
            using var input = CreateImage(500, 1000);
            using var thumb = ImageThumbnailGenerator.GenerateThumbnail(input, 200, 200);
            using var result = Load(thumb);
            Assert.Equal(100, result.Width);
            Assert.Equal(200, result.Height);
        }

        [Fact]
        public void Output_IsJpeg_AndPositionedAtStart()
        {
            using var input = CreateImage(400, 400, png: true);
            using var thumb = ImageThumbnailGenerator.GenerateThumbnail(input, 100, 100);
            Assert.Equal(0, thumb.Position);
            Assert.Equal("JPEG", Image.DetectFormat(thumb).Name);
        }

        [Fact]
        public void PngInput_IsAccepted()
        {
            using var input = CreateImage(300, 300, png: true);
            using var thumb = ImageThumbnailGenerator.GenerateThumbnail(input, 100, 100);
            thumb.Position = 0;
            using var result = Load(thumb);
            Assert.Equal(100, result.Width);
            Assert.Equal(100, result.Height);
        }

        [Fact]
        public void NullStream_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => ImageThumbnailGenerator.GenerateThumbnail(null!, 100, 100));
        }

        [Theory]
        [InlineData(0, 100)]
        [InlineData(100, 0)]
        [InlineData(-1, 100)]
        [InlineData(100, -5)]
        public void InvalidSize_Throws(int width, int height)
        {
            using var input = CreateImage(50, 50);
            Assert.Throws<ArgumentOutOfRangeException>(() => ImageThumbnailGenerator.GenerateThumbnail(input, width, height));
        }

        [Fact]
        public void NonImageStream_Throws()
        {
            using var input = new MemoryStream(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            Assert.Throws<UnknownImageFormatException>(() => ImageThumbnailGenerator.GenerateThumbnail(input, 100, 100));
        }
    }
}
