// -----------------------------------------------------------------------
// <copyright file="XmpTagFactory.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Xmp;

internal static class XmpTagFactory
{
  internal static void AddXmp( ReadOnlySpan<byte> buffer, ref Metadata metadata )
  {
    var xmpData = new XmpData();
    xmpData.SetValue( buffer );

    metadata.AddTag( MetadataProfileType.Xmp, xmpData );
  }

  internal static void AddXmp( string value, ref Metadata metadata )
  {
    var xmpData = new XmpData();
    xmpData.SetValue( value );

    metadata.AddTag( MetadataProfileType.Xmp, xmpData );
  }
}
