// -----------------------------------------------------------------------
// <copyright file="ExifTagValueFactory.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Exif;

using System;
using Coderanger.ImageInfo.Decoders.DecoderUtils;
using Coderanger.ImageInfo.Decoders.Exif.Types;

internal static class ExifTagValueFactory
{
  internal static ExifValue? Create( ExifProfileType profile, BinaryReader reader, long dataStart, TiffByteOrder byteOrder )
  {
    var directoryBuffer = reader.ReadBytes( ExifDirectorySize );
    var component = ExtractComponent( profile, directoryBuffer, dataStart, byteOrder );
    switch( profile )
    {
      case ExifProfileType.Exif:
      {
        return component.Tag switch
        {
          ExifTag.ProcessingSoftware => new ExifString( ExifStringEncoding.Ascii, reader, component ),
          ExifTag.NewSubfileType => new ExifLong( reader, component ),
          ExifTag.SubfileType => new ExifUShort( reader, component ),
          ExifTag.ImageWidth => new ExifLong( reader, component ),
          ExifTag.ImageHeight => new ExifLong( reader, component ),
          ExifTag.BitsPerSample => new ExifUShort( reader, component ),
          ExifTag.Compression => new ExifUShort( reader, component ),
          ExifTag.PhotometricInterpretation => new ExifUShort( reader, component ),
          ExifTag.Thresholding => new ExifUShort( reader, component ),
          ExifTag.CellWidth => new ExifUShort( reader, component ),
          ExifTag.CellLength => new ExifUShort( reader, component ),
          ExifTag.FillOrder => new ExifUShort( reader, component ),
          ExifTag.DocumentName => new ExifString( ExifStringEncoding.Ascii, reader, component ),
          ExifTag.ImageDescription => new ExifString( ExifStringEncoding.Ascii, reader, component ),
          ExifTag.Make => new ExifString( ExifStringEncoding.Ascii, reader, component ),
          ExifTag.Model => new ExifString( ExifStringEncoding.Ascii, reader, component ),
          ExifTag.StripOffsets => new ExifLong( reader, component ),
          ExifTag.Orientation => new ExifUShort( reader, component ),
          ExifTag.SamplesPerPixel => new ExifUShort( reader, component ),
          ExifTag.RowsPerStrip => new ExifLong( reader, component ),
          ExifTag.StripByteCounts => new ExifLong( reader, component ),
          ExifTag.XResolution => new ExifURational( reader, component ),
          ExifTag.YResolution => new ExifURational( reader, component ),
          ExifTag.PlanarConfiguration => new ExifUShort( reader, component ),
          ExifTag.GrayResponseUnit => new ExifUShort( reader, component ),
          ExifTag.GrayResponseCurve => new ExifUShort( reader, component ),
          ExifTag.T4Options => new ExifLong( reader, component ),
          ExifTag.T6Options => new ExifLong( reader, component ),
          ExifTag.ResolutionUnit => new ExifUShort( reader, component ),
          ExifTag.PageNumber => new ExifUShort( reader, component ),
          ExifTag.TransferFunction => new ExifUShort( reader, component ),
          ExifTag.Software => new ExifString( ExifStringEncoding.Ascii, reader, component ),
          ExifTag.DateTime => new ExifDateTime( reader, component ),
          ExifTag.Artist => new ExifString( ExifStringEncoding.Ascii, reader, component ),
          ExifTag.HostComputer => new ExifString( ExifStringEncoding.Ascii, reader, component ),
          ExifTag.Predictor => new ExifUShort( reader, component ),
          ExifTag.WhitePoint => new ExifURational( reader, component ),
          ExifTag.PrimaryChromaticities => new ExifURational( reader, component ),
          ExifTag.ColorMap => new ExifUShort( reader, component ),
          ExifTag.HalftoneHints => new ExifUShort( reader, component ),
          ExifTag.TileWidth => new ExifLong( reader, component ),
          ExifTag.TileLength => new ExifLong( reader, component ),
          ExifTag.TileOffsets => new ExifUShort( reader, component ),
          ExifTag.TileByteCounts => new ExifLong( reader, component ),
          ExifTag.InkSet => new ExifUShort( reader, component ),
          ExifTag.InkNames => new ExifString( ExifStringEncoding.Ascii, reader, component ),
          ExifTag.NumberOfInks => new ExifUShort( reader, component ),
          ExifTag.DotRange => new ExifByte( reader, component ),
          ExifTag.TargetPrinter => new ExifString( ExifStringEncoding.Ascii, reader, component ),
          ExifTag.ExtraSamples => new ExifUShort( reader, component ),
          ExifTag.SampleFormat => new ExifUShort( reader, component ),
          ExifTag.SMinSampleValue => new ExifUShort( reader, component ),
          ExifTag.SMaxSampleValue => new ExifUShort( reader, component ),
          ExifTag.TransferRange => new ExifUShort( reader, component ),
          ExifTag.ClipPath => new ExifByte( reader, component ),
          ExifTag.XClipPathUnits => new ExifShort( reader, component ),
          ExifTag.YClipPathUnits => new ExifShort( reader, component ),
          ExifTag.Indexed => new ExifUShort( reader, component ),
          ExifTag.OPIProxy => new ExifUShort( reader, component ),
          ExifTag.JPEGProc => new ExifLong( reader, component ),
          ExifTag.JPEGInterchangeFormat => new ExifLong( reader, component ),
          ExifTag.JPEGInterchangeFormatLength => new ExifLong( reader, component ),
          ExifTag.JPEGRestartInterval => new ExifUShort( reader, component ),
          ExifTag.JPEGLosslessPredictors => new ExifUShort( reader, component ),
          ExifTag.JPEGPointTransforms => new ExifUShort( reader, component ),
          ExifTag.JPEGQTables => new ExifLong( reader, component ),
          ExifTag.JPEGDCTables => new ExifLong( reader, component ),
          ExifTag.JPEGACTables => new ExifLong( reader, component ),
          ExifTag.YCbCrCoefficients => new ExifURational( reader, component ),
          ExifTag.YCbCrSubSampling => new ExifUShort( reader, component ),
          ExifTag.YCbCrPositioning => new ExifUShort( reader, component ),
          ExifTag.ReferenceBlackWhite => new ExifURational( reader, component ),
          ExifTag.XMLPacket => new ExifByte( reader, component ),
          ExifTag.Rating => new ( reader, component ),
          ExifTag.RatingPercent => new ExifUShort( reader, component ),
          ExifTag.VignettingCorrParams => new ExifShort( reader, component ),
          ExifTag.ChromaticAberrationCorrParams => new ExifShort( reader, component ),
          ExifTag.DistortionCorrParams => new ExifShort( reader, component ),
          ExifTag.ImageID => new ExifString( ExifStringEncoding.Ascii, reader, component ),
          ExifTag.CFARepeatPatternDim => new ExifUShort( reader, component ),
          ExifTag.CFAPattern => new ExifByte( reader, component ),
          ExifTag.BatteryLevel => new ExifURational( reader, component ),
          ExifTag.Copyright => new ExifString( ExifStringEncoding.Ascii, reader, component ),
          ExifTag.ExposureTime => new ExifURational( reader, component ),
          ExifTag.FNumber => new ExifURational( reader, component ),
          ExifTag.IPTCNAA => new ExifLong( reader, component ),
          ExifTag.ImageResources => new ExifByte( reader, component ),
          ExifTag.ExposureProgram => new ExifUShort( reader, component ),
          ExifTag.SpectralSensitivity => new ExifString( ExifStringEncoding.Ascii, reader, component ),
          ExifTag.ISOSpeedRatings => new ExifUShort( reader, component ),
          ExifTag.Interlace => new ExifUShort( reader, component ),
          ExifTag.TimeZoneOffset => new ExifShort( reader, component ),
          ExifTag.SelfTimerMode => new ExifUShort( reader, component ),
          ExifTag.DateTimeOriginal => new ExifDateTime( reader, component ),
          ExifTag.CompressedBitsPerPixel => new ExifURational( reader, component ),
          ExifTag.ShutterSpeedValue => new ExifRational( reader, component ),
          ExifTag.ApertureValue => new ExifURational( reader, component ),
          ExifTag.BrightnessValue => new ExifRational( reader, component ),
          ExifTag.ExposureBiasValue => new ExifRational( reader, component ),
          ExifTag.MaxApertureValue => new ExifURational( reader, component ),
          ExifTag.SubjectDistance => new ExifRational( reader, component ),
          ExifTag.MeteringMode => new ExifUShort( reader, component ),
          ExifTag.LightSource => new ExifUShort( reader, component ),
          ExifTag.Flash => new ExifUShort( reader, component ),
          ExifTag.FocalLength => new ExifURational( reader, component ),
          ExifTag.FlashEnergy => new ExifURational( reader, component ),
          ExifTag.FocalPlaneXResolution => new ExifURational( reader, component ),
          ExifTag.FocalPlaneYResolution => new ExifURational( reader, component ),
          ExifTag.FocalPlaneResolutionUnit => new ExifUShort( reader, component ),
          ExifTag.ImageNumber => new ExifLong( reader, component ),
          ExifTag.SecurityClassification => new ExifString( ExifStringEncoding.Ascii, reader, component ),
          ExifTag.ImageHistory => new ExifString( ExifStringEncoding.Ascii, reader, component ),
          ExifTag.SubjectLocation => new ExifUShort( reader, component ),
          ExifTag.ExposureIndex => new ExifURational( reader, component ),
          ExifTag.TIFFEPStandardID => new ExifByte( reader, component ),
          ExifTag.SensingMethod => new ExifUShort( reader, component ),
          ExifTag.XPTitle => new ExifString( ExifStringEncoding.Ucs2, reader, component ),
          ExifTag.XPComment => new ExifString( ExifStringEncoding.Ucs2, reader, component ),
          ExifTag.XPAuthor => new ExifString( ExifStringEncoding.Ucs2, reader, component ),
          ExifTag.XPKeywords => new ExifString( ExifStringEncoding.Ucs2, reader, component ),
          ExifTag.XPSubject => new ExifString( ExifStringEncoding.Ucs2, reader, component ),
          ExifTag.DNGVersion => new ExifByte( reader, component ),
          ExifTag.DNGBackwardVersion => new ExifByte( reader, component ),
          ExifTag.UniqueCameraModel => new ExifString( ExifStringEncoding.Ascii, reader, component ),
          ExifTag.LocalizedCameraModel => new ExifByte( reader, component ),
          ExifTag.CFAPlaneColor => new ExifByte( reader, component ),
          ExifTag.CFALayout => new ExifUShort( reader, component ),
          ExifTag.LinearizationTable => new ExifUShort( reader, component ),
          ExifTag.BlackLevelRepeatDim => new ExifUShort( reader, component ),
          ExifTag.BlackLevel => new ExifURational( reader, component ),
          ExifTag.BlackLevelDeltaH => new ExifRational( reader, component ),
          ExifTag.BlackLevelDeltaV => new ExifRational( reader, component ),
          ExifTag.WhiteLevel => new ExifLong( reader, component ),
          ExifTag.DefaultScale => new ExifURational( reader, component ),
          ExifTag.DefaultCropOrigin => new ExifLong( reader, component ),
          ExifTag.DefaultCropSize => new ExifLong( reader, component ),
          ExifTag.ColorMatrix1 => new ExifRational( reader, component ),
          ExifTag.ColorMatrix2 => new ExifRational( reader, component ),
          ExifTag.CameraCalibration1 => new ExifRational( reader, component ),
          ExifTag.CameraCalibration2 => new ExifRational( reader, component ),
          ExifTag.ReductionMatrix1 => new ExifRational( reader, component ),
          ExifTag.ReductionMatrix2 => new ExifRational( reader, component ),
          ExifTag.AnalogBalance => new ExifURational( reader, component ),
          ExifTag.AsShotNeutral => new ExifUShort( reader, component ),
          ExifTag.AsShotWhiteXY => new ExifURational( reader, component ),
          ExifTag.BaselineExposure => new ExifRational( reader, component ),
          ExifTag.BaselineNoise => new ExifURational( reader, component ),
          ExifTag.BaselineSharpness => new ExifURational( reader, component ),
          ExifTag.BayerGreenSplit => new ExifLong( reader, component ),
          ExifTag.LinearResponseLimit => new ExifURational( reader, component ),
          ExifTag.CameraSerialNumber => new ExifString( ExifStringEncoding.Ascii, reader, component ),
          ExifTag.LensInfo => new ExifURational( reader, component ),
          ExifTag.ChromaBlurRadius => new ExifURational( reader, component ),
          ExifTag.AntiAliasStrength => new ExifURational( reader, component ),
          ExifTag.ShadowScale => new ExifRational( reader, component ),
          ExifTag.DNGPrivateData => new ExifByte( reader, component ),
          ExifTag.MakerNoteSafety => new ExifUShort( reader, component ),
          ExifTag.CalibrationIlluminant1 => new ExifUShort( reader, component ),
          ExifTag.CalibrationIlluminant2 => new ExifUShort( reader, component ),
          ExifTag.BestQualityScale => new ExifURational( reader, component ),
          ExifTag.RawDataUniqueID => new ExifByte( reader, component ),
          ExifTag.OriginalRawFileName => new ExifByte( reader, component ),
          ExifTag.ActiveArea => new ExifLong( reader, component ),
          ExifTag.MaskedAreas => new ExifLong( reader, component ),
          ExifTag.AsShotPreProfileMatrix => new ExifRational( reader, component ),
          ExifTag.CurrentPreProfileMatrix => new ExifRational( reader, component ),
          ExifTag.ColorimetricReference => new ExifUShort( reader, component ),
          ExifTag.CameraCalibrationSignature => new ExifString( ExifStringEncoding.Utf8, reader, component ),
          ExifTag.ProfileCalibrationSignature => new ExifString( ExifStringEncoding.Utf8, reader, component ),
          ExifTag.ExtraCameraProfiles => new ExifLong( reader, component ),
          ExifTag.AsShotProfileName => new ExifString( ExifStringEncoding.Utf8, reader, component ),
          ExifTag.NoiseReductionApplied => new ExifURational( reader, component ),
          ExifTag.ProfileName => new ExifString( ExifStringEncoding.Utf8, reader, component ),
          ExifTag.ProfileHueSatMapDims => new ExifLong( reader, component ),
          ExifTag.ProfileHueSatMapData1 => new ExifFloat( reader, component ),
          ExifTag.ProfileHueSatMapData2 => new ExifFloat( reader, component ),
          ExifTag.ProfileToneCurve => new ExifFloat( reader, component ),
          ExifTag.ProfileEmbedPolicy => new ExifLong( reader, component ),
          ExifTag.ProfileCopyright => new ExifString( ExifStringEncoding.Utf8, reader, component ),
          ExifTag.ForwardMatrix1 => new ExifRational( reader, component ),
          ExifTag.ForwardMatrix2 => new ExifRational( reader, component ),
          ExifTag.PreviewApplicationName => new ExifString( ExifStringEncoding.Utf8, reader, component ),
          ExifTag.PreviewApplicationVersion => new ExifString( ExifStringEncoding.Utf8, reader, component ),
          ExifTag.PreviewSettingsName => new ExifString( ExifStringEncoding.Utf8, reader, component ),
          ExifTag.PreviewSettingsDigest => new ExifByte( reader, component ),
          ExifTag.PreviewColorSpace => new ExifLong( reader, component ),
          ExifTag.PreviewDateTime => new ExifDateTime( reader, component ),
          ExifTag.SubTileBlockSize => new ExifLong( reader, component ),
          ExifTag.RowInterleaveFactor => new ExifLong( reader, component ),
          ExifTag.ProfileLookTableDims => new ExifLong( reader, component ),
          ExifTag.ProfileLookTableData => new ExifFloat( reader, component ),
          ExifTag.NoiseProfile => new ExifURational( reader, component ),
          ExifTag.TimeCodes => new ExifByte( reader, component ),
          ExifTag.FrameRate => new ExifRational( reader, component ),
          ExifTag.TStop => new ExifRational( reader, component ),
          ExifTag.ReelName => new ExifString( ExifStringEncoding.Ascii, reader, component ),
          ExifTag.CameraLabel => new ExifString( ExifStringEncoding.Ascii, reader, component ),
          ExifTag.OriginalDefaultFinalSize => new ExifLong( reader, component ),
          ExifTag.OriginalBestQualityFinalSize => new ExifLong( reader, component ),
          ExifTag.OriginalDefaultCropSize => new ExifLong( reader, component ),
          ExifTag.ProfileHueSatMapEncoding => new ExifLong( reader, component ),
          ExifTag.ProfileLookTableEncoding => new ExifLong( reader, component ),
          ExifTag.BaselineExposureOffset => new ExifRational( reader, component ),
          ExifTag.DefaultBlackRender => new ExifLong( reader, component ),
          ExifTag.NewRawImageDigest => new ExifByte( reader, component ),
          ExifTag.RawToPreviewGain => new ExifURational( reader, component ),
          ExifTag.DefaultUserCrop => new ExifURational( reader, component ),
          ExifTag.DepthFormat => new ExifUShort( reader, component ),
          ExifTag.DepthNear => new ExifURational( reader, component ),
          ExifTag.DepthFar => new ExifURational( reader, component ),
          ExifTag.DepthUnits => new ExifUShort( reader, component ),
          ExifTag.DepthMeasureType => new ExifUShort( reader, component ),
          ExifTag.EnhanceParams => new ExifString( ExifStringEncoding.Ascii, reader, component ),
          ExifTag.SemanticName => new ExifString( ExifStringEncoding.Ascii, reader, component ),
          ExifTag.SemanticInstanceID => new ExifString( ExifStringEncoding.Ascii, reader, component ),
          ExifTag.CalibrationIlluminant3 => new ExifUShort( reader, component ),
          ExifTag.CameraCalibration3 => new ExifRational( reader, component ),
          ExifTag.ColorMatrix3 => new ExifRational( reader, component ),
          ExifTag.ForwardMatrix3 => new ExifRational( reader, component ),
          ExifTag.ProfileHueSatMapData3 => new ExifFloat( reader, component ),
          ExifTag.ReductionMatrix3 => new ExifRational( reader, component ),

          _ => null,
        };
      }

      case ExifProfileType.Gps:
      {
        break;
      }

      case ExifProfileType.Interoperability:
      {
        break;
      }
    }

    return null;
  }

  internal static (ushort Tag, short DataType, int ComponentCount, byte[] Data ) CrackData( byte[] directoryBuffer, TiffByteOrder byteOrder )
  {
    var tag = DataConversion.UInt16FromBuffer( directoryBuffer, TagByteStart, byteOrder );
    var dataType = DataConversion.Int16FromBuffer( directoryBuffer, TypeByteStart, byteOrder );
    var componentCount = DataConversion.Int32FromBuffer( directoryBuffer, ComponentCountByteStart, byteOrder );
    var valueBuffer = directoryBuffer.AsSpan( DataByteStart, 4 ).ToArray();
    return (tag, dataType, componentCount, valueBuffer);
  }

  private static ExifComponent ExtractComponent( ExifProfileType profile, byte[] directoryBuffer, long dataStart, TiffByteOrder byteOrder )
  {
    var (tag, dataType, componentCount, valueBuffer) = CrackData( directoryBuffer, byteOrder );

    return new ExifComponent( profile: profile,
                              tag: tag,
                              dataType: dataType,
                              componentCount: componentCount,
                              dataValueBuffer: valueBuffer,
                              dataStart: dataStart,
                              byteOrder: byteOrder );
  }

  private const int ExifDirectorySize = 12;
  private const int TagByteStart = 0;
  private const int TypeByteStart = 2;
  private const int ComponentCountByteStart = 4;
  private const int DataByteStart = 8;
}
