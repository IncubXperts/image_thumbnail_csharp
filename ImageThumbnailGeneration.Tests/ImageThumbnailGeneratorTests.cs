using ImageThumbnailGeneration;
using SkiaSharp;
using System;
using System.IO;
using Xunit;

namespace ImageThumbnailGeneration.Tests
{
    public class ImageThumbnailGeneratorTests
    {
        private static MemoryStream CreateImage(int width, int height, SKEncodedImageFormat format = SKEncodedImageFormat.Jpeg, SKColor? color = null)
        {
            using var bitmap = new SKBitmap(width, height);
            bitmap.Erase(color ?? new SKColor(200, 100, 50));
            using var image = SKImage.FromBitmap(bitmap);
            using var data = image.Encode(format, 90);
            var ms = new MemoryStream();
            data.SaveTo(ms);
            ms.Position = 0;
            return ms;
        }

        private static SKBitmap Load(Stream s) => SKBitmap.Decode(s);

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
            using var input = CreateImage(400, 400, SKEncodedImageFormat.Png);
            using var thumb = ImageThumbnailGenerator.GenerateThumbnail(input, 100, 100);
            Assert.Equal(0, thumb.Position);
            using var codec = SKCodec.Create(thumb);
            Assert.Equal(SKEncodedImageFormat.Jpeg, codec.EncodedFormat);
        }

        [Fact]
        public void PngInput_IsAccepted()
        {
            using var input = CreateImage(300, 300, SKEncodedImageFormat.Png);
            using var thumb = ImageThumbnailGenerator.GenerateThumbnail(input, 100, 100);
            using var result = Load(thumb);
            Assert.Equal(100, result.Width);
            Assert.Equal(100, result.Height);
        }

        [Fact]
        public void TransparentPng_IsFlattenedOntoWhite()
        {
            using var input = CreateImage(100, 100, SKEncodedImageFormat.Png, SKColors.Transparent);
            using var thumb = ImageThumbnailGenerator.GenerateThumbnail(input, 50, 50);
            using var result = Load(thumb);
            var pixel = result.GetPixel(25, 25);
            Assert.True(pixel.Red > 240 && pixel.Green > 240 && pixel.Blue > 240, $"Expected white, got {pixel}");
        }

        [Fact]
        public void NonSeekableStream_IsAccepted()
        {
            using var source = CreateImage(300, 150);
            using var input = new NonSeekableStream(source);
            using var thumb = ImageThumbnailGenerator.GenerateThumbnail(input, 100, 100);
            using var result = Load(thumb);
            Assert.Equal(100, result.Width);
            Assert.Equal(50, result.Height);
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
            Assert.Throws<InvalidDataException>(() => ImageThumbnailGenerator.GenerateThumbnail(input, 100, 100));
        }

        private sealed class NonSeekableStream : Stream
        {
            private readonly Stream _inner;
            public NonSeekableStream(Stream inner) => _inner = inner;
            public override bool CanRead => true;
            public override bool CanSeek => false;
            public override bool CanWrite => false;
            public override long Length => throw new NotSupportedException();
            public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }
            public override void Flush() { }
            public override int Read(byte[] buffer, int offset, int count) => _inner.Read(buffer, offset, count);
            public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
            public override void SetLength(long value) => throw new NotSupportedException();
            public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
        }
    }
}
