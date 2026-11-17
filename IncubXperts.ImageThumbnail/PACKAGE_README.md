# IncubXperts.ImageThumbnail

Generate a JPEG thumbnail from an image stream, keeping the aspect ratio.

```csharp
using ImageThumbnailGeneration;

using FileStream input = File.OpenRead("large-image.jpg");
using Stream thumbnail = ImageThumbnailGenerator.GenerateThumbnail(input, 200, 200);
using FileStream output = File.Create("thumb-image.jpg");
thumbnail.CopyTo(output);
```

- Accepts any format supported by ImageSharp (JPEG, PNG, GIF, BMP, WebP, TIFF, ...).
- The thumbnail fits within the given width x height and is never distorted.
- Output is always JPEG, so transparency is not preserved.

## ImageSharp licensing

This package depends on [SixLabors.ImageSharp](https://github.com/SixLabors/ImageSharp). Version 3.2.0 and later
require a [Six Labors license](https://sixlabors.com/pricing/) for Release builds. Check that your use is covered.

Source and issues: https://github.com/IncubXperts/image_thumbnail_csharp
