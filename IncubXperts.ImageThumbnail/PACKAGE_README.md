# IncubXperts.ImageThumbnail

Generate a JPEG thumbnail from an image stream, keeping the aspect ratio.

```csharp
using ImageThumbnailGeneration;

using FileStream input = File.OpenRead("large-image.jpg");
using Stream thumbnail = ImageThumbnailGenerator.GenerateThumbnail(input, 200, 200);
using FileStream output = File.Create("thumb-image.jpg");
thumbnail.CopyTo(output);
```

- Accepts any format supported by SkiaSharp (JPEG, PNG, GIF, BMP, WebP, ...).
- The thumbnail fits within the given width x height and is never distorted.
- Output is always JPEG; transparent areas are flattened onto white.

Source and issues: https://github.com/IncubXperts/image_thumbnail_csharp
