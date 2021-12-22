﻿// -----------------------------------------------------------------------
// <copyright file="ExifComponent.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Exif;

using Coderanger.ImageInfo.Decoders.DecoderUtils;

internal struct ExifComponent
{
  public ExifComponent( ExifProfileType profile, ushort tag, short dataType, int componentCount, byte[] dataValueBuffer, long dataStart, TiffByteOrder byteOrder )
  {
    Profile = profile;
    Tag = tag;
    DataType = dataType;
    ComponentCount = componentCount;
    DataValueBuffer = dataValueBuffer;
    ByteOrder = byteOrder;
    DataStart = dataStart;
  }

  public ExifProfileType Profile { get; init; }
  public ushort Tag { get; init; }
  public short DataType { get; init; }
  public int ComponentCount { get; init; }
  public byte[] DataValueBuffer { get; init; }
  public long DataStart { get; init; }
  public TiffByteOrder ByteOrder { get; init; }

  public byte ComponentSize
  {
    get
    {
      if( _componentSize == 0 )
      {
        GetComponentSize();
      }
      return _componentSize;
    }
    set
    {
      _componentSize = value;
    }
  }
  private byte _componentSize = 0;

  private void GetComponentSize()
  {
    switch( DataType )
    {
      case 1: // unsigned byte
      case 2: // ascii strings    
      case 6: // signed byte
        ComponentSize = 1;
        break;

      case 3: // unsigned short
      case 8: // signed short
        ComponentSize = 2;
        break;

      case 4: // unsigned long
      case 9: // signed long
      case 11: // single float
        ComponentSize = 4;
        break;

      case 5: // unsigned rational
      case 10: // signed rational
      case 12: // double float
        ComponentSize = 8;
        break;
    }
  }
}
