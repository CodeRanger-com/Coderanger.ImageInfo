// -----------------------------------------------------------------------
// <copyright file="ExifTagValueFactory.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Exif;

using System;
using Coderanger.ImageInfo.Decoders.DecoderUtils;
using Coderanger.ImageInfo.Decoders.Metadata.Exif.Types;

internal static class ExifTagValueFactory
{
  internal static IMetadataTypedValue? Create( MetadataProfileType profile, BinaryReader reader, long dataStart, TiffByteOrder byteOrder )
  {
    var directoryBuffer = reader.ReadBytes( ExifConstants.ExifDirectorySize );
    var component = ExtractComponent( profile, directoryBuffer, dataStart, byteOrder );
    switch( profile )
    {
      case MetadataProfileType.Exif:
      {
        return component.Tag switch
        {
          ExifTag.ProcessingSoftware => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.SubfileType => new ExifInt( reader, component ),
          ExifTag.OldSubfileType => new ExifUShort( reader, component ),
          ExifTag.ImageWidth => new ExifUShort( reader, component ),
          ExifTag.ImageHeight => new ExifUShort( reader, component ),
          ExifTag.BitsPerSample => new ExifUShort( reader, component ),
          ExifTag.Compression => new ExifUShort( reader, component ),
          ExifTag.PhotometricInterpretation => new ExifUShort( reader, component ),
          ExifTag.Thresholding => new ExifUShort( reader, component ),
          ExifTag.CellWidth => new ExifUShort( reader, component ),
          ExifTag.CellLength => new ExifUShort( reader, component ),
          ExifTag.FillOrder => new ExifUShort( reader, component ),
          ExifTag.DocumentName => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.ImageDescription => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.Make => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.Model => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.StripOffsets => new ExifInt( reader, component ),
          ExifTag.Orientation => new ExifEnum( reader, component, ExifEnum.EnumType.Numeric ),
          ExifTag.SamplesPerPixel => new ExifUShort( reader, component ),
          ExifTag.RowsPerStrip => new ExifInt( reader, component ),
          ExifTag.StripByteCounts => new ExifInt( reader, component ),
          ExifTag.XResolution => new ExifURational( reader, component ),
          ExifTag.YResolution => new ExifURational( reader, component ),
          ExifTag.PlanarConfiguration => new ExifUShort( reader, component ),
          ExifTag.GrayResponseUnit => new ExifEnum( reader, component, ExifEnum.EnumType.Numeric ),
          ExifTag.GrayResponseCurve => new ExifUShort( reader, component ),
          ExifTag.T4Options => new ExifInt( reader, component ),
          ExifTag.T6Options => new ExifInt( reader, component ),
          ExifTag.ResolutionUnit => new ExifEnum( reader, component, ExifEnum.EnumType.Numeric ),
          ExifTag.PageNumber => new ExifUShort( reader, component ),
          ExifTag.TransferFunction => new ExifUShort( reader, component ),
          ExifTag.Software => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.DateTime => new ExifDateTime( reader, component ),
          ExifTag.Artist => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.HostComputer => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.Predictor => new ExifEnum( reader, component, ExifEnum.EnumType.Numeric ),
          ExifTag.WhitePoint => new ExifURational( reader, component ),
          ExifTag.PrimaryChromaticities => new ExifURational( reader, component ),
          ExifTag.ColorMap => new ExifUShort( reader, component ),
          ExifTag.HalftoneHints => new ExifUShort( reader, component ),
          ExifTag.TileWidth => new ExifUInt( reader, component ),
          ExifTag.TileLength => new ExifUInt( reader, component ),
          ExifTag.TileOffsets => new ExifUShort( reader, component ),
          ExifTag.TileByteCounts => new ExifInt( reader, component ),
          ExifTag.InkSet => new ExifUShort( reader, component ),
          ExifTag.InkNames => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.NumberOfInks => new ExifUShort( reader, component ),
          ExifTag.DotRange => new ExifByte( reader, component ),
          ExifTag.TargetPrinter => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.ExtraSamples => new ExifEnum( reader, component, ExifEnum.EnumType.Numeric ),
          ExifTag.SampleFormat => new ExifEnum( reader, component, ExifEnum.EnumType.Numeric ),
          ExifTag.SMinSampleValue => new ExifUShort( reader, component ),
          ExifTag.SMaxSampleValue => new ExifUShort( reader, component ),
          ExifTag.TransferRange => new ExifUShort( reader, component ),
          ExifTag.ClipPath => new ExifByte( reader, component ),
          ExifTag.XClipPathUnits => new ExifShort( reader, component ),
          ExifTag.YClipPathUnits => new ExifShort( reader, component ),
          ExifTag.Indexed => new ExifUShort( reader, component ),
          ExifTag.OPIProxy => new ExifUShort( reader, component ),
          ExifTag.JPEGProc => new ExifUInt( reader, component ),
          ExifTag.JPEGInterchangeFormat => new ExifUInt( reader, component ),
          ExifTag.JPEGInterchangeFormatLength => new ExifUInt( reader, component ),
          ExifTag.JPEGRestartInterval => new ExifUShort( reader, component ),
          ExifTag.JPEGLosslessPredictors => new ExifUShort( reader, component ),
          ExifTag.JPEGPointTransforms => new ExifUShort( reader, component ),
          ExifTag.JPEGQTables => new ExifInt( reader, component ),
          ExifTag.JPEGDCTables => new ExifInt( reader, component ),
          ExifTag.JPEGACTables => new ExifInt( reader, component ),
          ExifTag.YCbCrCoefficients => new ExifURational( reader, component ),
          ExifTag.YCbCrSubSampling => new ExifUShort( reader, component ),
          ExifTag.YCbCrPositioning => new ExifUShort( reader, component ),
          ExifTag.ReferenceBlackWhite => new ExifURational( reader, component ),
          ExifTag.Rating => new ExifUShort( reader, component ),
          ExifTag.RatingPercent => new ExifUShort( reader, component ),
          ExifTag.VignettingCorrParams => new ExifShort( reader, component ),
          ExifTag.ChromaticAberrationCorrParams => new ExifShort( reader, component ),
          ExifTag.DistortionCorrParams => new ExifShort( reader, component ),
          ExifTag.ImageID => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.CFARepeatPatternDim => new ExifUShort( reader, component ),
          ExifTag.CFAPattern => new ExifByte( reader, component ),
          ExifTag.BatteryLevel => new ExifURational( reader, component ),
          ExifTag.Copyright => new ExifString( StringEncoding.Utf8, reader, component ),
          ExifTag.ExposureTime => new ExifURational( reader, component ),
          ExifTag.FNumber => new ExifURational( reader, component ),
          ExifTag.ImageResources => new ExifByte( reader, component ),
          ExifTag.ExposureProgram => new ExifUShort( reader, component ),
          ExifTag.SpectralSensitivity => new ExifString( StringEncoding.Ascii, reader, component ),
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
          ExifTag.MeteringMode => new ExifEnum( reader, component, ExifEnum.EnumType.Numeric ),
          ExifTag.LightSource => new ExifUShort( reader, component ),
          ExifTag.Flash => new ExifUShort( reader, component ),
          ExifTag.FocalLength => new ExifURational( reader, component ),
          ExifTag.FlashEnergy => new ExifURational( reader, component ),
          ExifTag.FocalPlaneXResolution => new ExifURational( reader, component ),
          ExifTag.FocalPlaneYResolution => new ExifURational( reader, component ),
          ExifTag.FocalPlaneResolutionUnit => new ExifEnum( reader, component, ExifEnum.EnumType.Numeric ),
          ExifTag.ImageNumber => new ExifInt( reader, component ),
          ExifTag.SecurityClassification => new ExifEnum( reader, component, ExifEnum.EnumType.String ),
          ExifTag.ImageHistory => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.SubjectLocation => new ExifUShort( reader, component ),
          ExifTag.ExposureIndex => new ExifURational( reader, component ),
          ExifTag.TIFFEPStandardID => new ExifByte( reader, component ),
          ExifTag.SensingMethod => new ExifUShort( reader, component ),
          ExifTag.XPTitle => new ExifString( StringEncoding.Ucs2, reader, component ),
          ExifTag.XPComment => new ExifString( StringEncoding.Ucs2, reader, component ),
          ExifTag.XPAuthor => new ExifString( StringEncoding.Ucs2, reader, component ),
          ExifTag.XPKeywords => new ExifString( StringEncoding.Ucs2, reader, component ),
          ExifTag.XPSubject => new ExifString( StringEncoding.Ucs2, reader, component ),
          ExifTag.DNGVersion => new ExifByte( reader, component ),
          ExifTag.DNGBackwardVersion => new ExifByte( reader, component ),
          ExifTag.UniqueCameraModel => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.LocalizedCameraModel => new ExifByte( reader, component ),
          ExifTag.CFAPlaneColor => new ExifByte( reader, component ),
          ExifTag.CFALayout => new ExifUShort( reader, component ),
          ExifTag.LinearizationTable => new ExifUShort( reader, component ),
          ExifTag.BlackLevelRepeatDim => new ExifUShort( reader, component ),
          ExifTag.BlackLevel => new ExifURational( reader, component ),
          ExifTag.BlackLevelDeltaH => new ExifRational( reader, component ),
          ExifTag.BlackLevelDeltaV => new ExifRational( reader, component ),
          ExifTag.WhiteLevel => new ExifInt( reader, component ),
          ExifTag.DefaultScale => new ExifURational( reader, component ),
          ExifTag.DefaultCropOrigin => new ExifInt( reader, component ),
          ExifTag.DefaultCropSize => new ExifInt( reader, component ),
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
          ExifTag.BayerGreenSplit => new ExifInt( reader, component ),
          ExifTag.LinearResponseLimit => new ExifURational( reader, component ),
          ExifTag.CameraSerialNumber => new ExifString( StringEncoding.Ascii, reader, component ),
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
          ExifTag.ActiveArea => new ExifInt( reader, component ),
          ExifTag.MaskedAreas => new ExifInt( reader, component ),
          ExifTag.AsShotPreProfileMatrix => new ExifRational( reader, component ),
          ExifTag.CurrentPreProfileMatrix => new ExifRational( reader, component ),
          ExifTag.ColorimetricReference => new ExifUShort( reader, component ),
          ExifTag.CameraCalibrationSignature => new ExifString( StringEncoding.Utf8, reader, component ),
          ExifTag.ProfileCalibrationSignature => new ExifString( StringEncoding.Utf8, reader, component ),
          ExifTag.ExtraCameraProfiles => new ExifInt( reader, component ),
          ExifTag.AsShotProfileName => new ExifString( StringEncoding.Utf8, reader, component ),
          ExifTag.NoiseReductionApplied => new ExifURational( reader, component ),
          ExifTag.ProfileName => new ExifString( StringEncoding.Utf8, reader, component ),
          ExifTag.ProfileHueSatMapDims => new ExifInt( reader, component ),
          ExifTag.ProfileHueSatMapData1 => new ExifFloat( reader, component ),
          ExifTag.ProfileHueSatMapData2 => new ExifFloat( reader, component ),
          ExifTag.ProfileToneCurve => new ExifFloat( reader, component ),
          ExifTag.ProfileEmbedPolicy => new ExifInt( reader, component ),
          ExifTag.ProfileCopyright => new ExifString( StringEncoding.Utf8, reader, component ),
          ExifTag.ForwardMatrix1 => new ExifRational( reader, component ),
          ExifTag.ForwardMatrix2 => new ExifRational( reader, component ),
          ExifTag.PreviewApplicationName => new ExifString( StringEncoding.Utf8, reader, component ),
          ExifTag.PreviewApplicationVersion => new ExifString( StringEncoding.Utf8, reader, component ),
          ExifTag.PreviewSettingsName => new ExifString( StringEncoding.Utf8, reader, component ),
          ExifTag.PreviewSettingsDigest => new ExifByte( reader, component ),
          ExifTag.PreviewColorSpace => new ExifInt( reader, component ),
          ExifTag.PreviewDateTime => new ExifDateTime( reader, component ),
          ExifTag.SubTileBlockSize => new ExifInt( reader, component ),
          ExifTag.RowInterleaveFactor => new ExifInt( reader, component ),
          ExifTag.ProfileLookTableDims => new ExifInt( reader, component ),
          ExifTag.ProfileLookTableData => new ExifFloat( reader, component ),
          ExifTag.NoiseProfile => new ExifURational( reader, component ),
          ExifTag.TimeCodes => new ExifByte( reader, component ),
          ExifTag.FrameRate => new ExifRational( reader, component ),
          ExifTag.TStop => new ExifRational( reader, component ),
          ExifTag.ReelName => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.CameraLabel => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.OriginalDefaultFinalSize => new ExifInt( reader, component ),
          ExifTag.OriginalBestQualityFinalSize => new ExifInt( reader, component ),
          ExifTag.OriginalDefaultCropSize => new ExifInt( reader, component ),
          ExifTag.ProfileHueSatMapEncoding => new ExifInt( reader, component ),
          ExifTag.ProfileLookTableEncoding => new ExifInt( reader, component ),
          ExifTag.BaselineExposureOffset => new ExifRational( reader, component ),
          ExifTag.DefaultBlackRender => new ExifInt( reader, component ),
          ExifTag.NewRawImageDigest => new ExifByte( reader, component ),
          ExifTag.RawToPreviewGain => new ExifURational( reader, component ),
          ExifTag.DefaultUserCrop => new ExifURational( reader, component ),
          ExifTag.DepthFormat => new ExifUShort( reader, component ),
          ExifTag.DepthNear => new ExifURational( reader, component ),
          ExifTag.DepthFar => new ExifURational( reader, component ),
          ExifTag.DepthUnits => new ExifEnum( reader, component, ExifEnum.EnumType.Numeric ),
          ExifTag.DepthMeasureType => new ExifEnum( reader, component, ExifEnum.EnumType.Numeric ),
          ExifTag.EnhanceParams => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.SemanticName => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.SemanticInstanceID => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.CalibrationIlluminant3 => new ExifUShort( reader, component ),
          ExifTag.CameraCalibration3 => new ExifRational( reader, component ),
          ExifTag.ColorMatrix3 => new ExifRational( reader, component ),
          ExifTag.ForwardMatrix3 => new ExifRational( reader, component ),
          ExifTag.ProfileHueSatMapData3 => new ExifFloat( reader, component ),
          ExifTag.ReductionMatrix3 => new ExifRational( reader, component ),
          ExifTag.PhotoSensitivityType => new ExifEnum( reader, component, ExifEnum.EnumType.Numeric ),
          ExifTag.PhotoStandardOutputSensitivity => new ExifInt( reader, component ),
          ExifTag.PhotoRecommendedExposureIndex => new ExifInt( reader, component ),
          ExifTag.PhotoISOSpeed => new ExifInt( reader, component ),
          ExifTag.PhotoISOSpeedLatitudeyyy => new ExifInt( reader, component ),
          ExifTag.PhotoISOSpeedLatitudezzz => new ExifInt( reader, component ),
          ExifTag.PhotoExifVersion => new ExifByte( reader, component ),
          ExifTag.PhotoDateTimeDigitized => new ExifDateTime( reader, component ),
          ExifTag.PhotoOffsetTime => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.PhotoOffsetTimeOriginal => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.PhotoOffsetTimeDigitized => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.PhotoComponentsConfiguration => new ExifByte( reader, component ),
          ExifTag.PhotoMakerNote => new ExifByte( reader, component ),
          ExifTag.PhotoUserComment => new ExifString( StringEncoding.Undefined, reader, component ),
          ExifTag.PhotoSubSecTime => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.PhotoSubSecTimeOriginal => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.PhotoSubSecTimeDigitized => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.PhotoTemperature => new ExifRational( reader, component ),
          ExifTag.PhotoHumidity => new ExifURational( reader, component ),
          ExifTag.PhotoPressure => new ExifURational( reader, component ),
          ExifTag.PhotoWaterDepth => new ExifRational( reader, component ),
          ExifTag.PhotoAcceleration => new ExifURational( reader, component ),
          ExifTag.PhotoCameraElevationAngle => new ExifRational( reader, component ),
          ExifTag.PhotoFlashpixVersion => new ExifByte( reader, component ),
          ExifTag.PhotoColorSpace => new ExifUShort( reader, component ),
          ExifTag.PhotoPixelXDimension => new ExifUInt( reader, component ),
          ExifTag.PhotoPixelYDimension => new ExifUInt( reader, component ),
          ExifTag.PhotoRelatedSoundFile => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.PhotoFlashEnergy => new ExifURational( reader, component ),
          ExifTag.PhotoSpatialFrequencyResponse => new ExifByte( reader, component ),
          ExifTag.PhotoFocalPlaneXResolution => new ExifURational( reader, component ),
          ExifTag.PhotoFocalPlaneYResolution => new ExifURational( reader, component ),
          ExifTag.PhotoFocalPlaneResolutionUnit => new ExifUShort( reader, component ),
          ExifTag.PhotoSubjectLocation => new ExifUShort( reader, component ),
          ExifTag.PhotoExposureIndex => new ExifURational( reader, component ),
          ExifTag.PhotoSensingMethod => new ExifEnum( reader, component, ExifEnum.EnumType.Numeric ),
          ExifTag.PhotoFileSource => new ExifEnum( reader, component, ExifEnum.EnumType.Numeric ),
          ExifTag.PhotoSceneType => new ExifByte( reader, component ),
          ExifTag.PhotoCFAPattern => new ExifByte( reader, component ),
          ExifTag.PhotoCustomRendered => new ExifEnum( reader, component, ExifEnum.EnumType.Numeric ),
          ExifTag.PhotoExposureMode => new ExifEnum( reader, component, ExifEnum.EnumType.Numeric ),
          ExifTag.PhotoWhiteBalance => new ExifEnum( reader, component, ExifEnum.EnumType.Numeric ),
          ExifTag.PhotoDigitalZoomRatio => new ExifURational( reader, component ),
          ExifTag.PhotoFocalLengthIn35mmFilm => new ExifUShort( reader, component ),
          ExifTag.PhotoSceneCaptureType => new ExifEnum( reader, component, ExifEnum.EnumType.Numeric ),
          ExifTag.PhotoGainControl => new ExifEnum( reader, component, ExifEnum.EnumType.Numeric ),
          ExifTag.PhotoContrast => new ExifEnum( reader, component, ExifEnum.EnumType.Numeric ),
          ExifTag.PhotoSaturation => new ExifEnum( reader, component, ExifEnum.EnumType.Numeric ),
          ExifTag.PhotoSharpness => new ExifEnum( reader, component, ExifEnum.EnumType.Numeric ),
          ExifTag.PhotoDeviceSettingDescription => new ExifByte( reader, component ),
          ExifTag.PhotoSubjectDistanceRange => new ExifEnum( reader, component, ExifEnum.EnumType.Numeric ),
          ExifTag.PhotoImageUniqueID => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.PhotoCameraOwnerName => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.PhotoBodySerialNumber => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.PhotoLensSpecification => new ExifURational( reader, component ),
          ExifTag.PhotoLensMake => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.PhotoLensModel => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.PhotoLensSerialNumber => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.PhotoCompositeImage => new ExifUShort( reader, component ),
          ExifTag.PhotoSourceImageNumberOfCompositeImage => new ExifUShort( reader, component ),
          ExifTag.PhotoSourceExposureTimesOfCompositeImage => new ExifByte( reader, component ),
          ExifTag.PhotoGamma => new ExifURational( reader, component ),

          _ => null,
        };
      }

      case MetadataProfileType.Gps:
      {
        return component.Tag switch
        {
          ExifTag.GPSVersionID => new ExifByte( reader, component ),
          ExifTag.GPSLatitudeRef => new ExifEnum( reader, component, ExifEnum.EnumType.String ),
          ExifTag.GPSLatitude => new ExifURational( reader, component ),
          ExifTag.GPSLongitudeRef => new ExifEnum( reader, component, ExifEnum.EnumType.String ),
          ExifTag.GPSLongitude => new ExifURational( reader, component ),
          ExifTag.GPSAltitudeRef => new ExifEnum( reader, component, ExifEnum.EnumType.Numeric ),
          ExifTag.GPSAltitude => new ExifURational( reader, component ),
          ExifTag.GPSTimeStamp => new ExifURational( reader, component ),
          ExifTag.GPSSatellites => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.GPSStatus => new ExifEnum( reader, component, ExifEnum.EnumType.String ),
          ExifTag.GPSMeasureMode => new ExifEnum( reader, component, ExifEnum.EnumType.String ),
          ExifTag.GPSDOP => new ExifURational( reader, component ),
          ExifTag.GPSSpeedRef => new ExifEnum( reader, component, ExifEnum.EnumType.String ),
          ExifTag.GPSSpeed => new ExifURational( reader, component ),
          ExifTag.GPSTrackRef => new ExifEnum( reader, component, ExifEnum.EnumType.String ),
          ExifTag.GPSTrack => new ExifURational( reader, component ),
          ExifTag.GPSImgDirectionRef => new ExifEnum( reader, component, ExifEnum.EnumType.String ),
          ExifTag.GPSImgDirection => new ExifURational( reader, component ),
          ExifTag.GPSMapDatum => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.GPSDestLatitudeRef => new ExifEnum( reader, component, ExifEnum.EnumType.String ),
          ExifTag.GPSDestLatitude => new ExifURational( reader, component ),
          ExifTag.GPSDestLongitudeRef => new ExifEnum( reader, component, ExifEnum.EnumType.String ),
          ExifTag.GPSDestLongitude => new ExifURational( reader, component ),
          ExifTag.GPSDestBearingRef => new ExifEnum( reader, component, ExifEnum.EnumType.String ),
          ExifTag.GPSDestBearing => new ExifURational( reader, component ),
          ExifTag.GPSDestDistanceRef => new ExifEnum( reader, component, ExifEnum.EnumType.String ),
          ExifTag.GPSDestDistance => new ExifURational( reader, component ),
          ExifTag.GPSProcessingMethod => new ExifString( StringEncoding.Undefined, reader, component ),
          ExifTag.GPSAreaInformation => new ExifString( StringEncoding.Undefined, reader, component ),
          ExifTag.GPSDateStamp => new ExifDate( reader, component ),
          ExifTag.GPSDifferential => new ExifUShort( reader, component ),
          ExifTag.GPSHPositioningError => new ExifURational( reader, component ),

          _ => null,
        };
      }

      case MetadataProfileType.Interoperability:
      {
        component.Tag += ExifConstants.InteroperabilityOffsetFix;
        return component.Tag switch
        {
          ExifTag.InteroperabilityIndex => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.InteroperabilityVersion => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.RelatedImageFileFormat => new ExifString( StringEncoding.Ascii, reader, component ),
          ExifTag.RelatedImageWidth => new ExifUShort( reader, component ),
          ExifTag.RelatedImageLength => new ExifUShort( reader, component ),

          _ => null,
        };
      }
    }

    return null;
  }

  internal ref struct CrackedData
  {
    private CrackedData( ushort tag, short dataType, int componentCount, ReadOnlySpan<byte> data )
    {
      Tag = tag;
      DataType = dataType;
      ComponentCount = componentCount;
      DataBuffer = data;
    }

    internal static CrackedData Create( ReadOnlySpan<byte> directoryBuffer, TiffByteOrder byteOrder )
    {
      var tag = DataConversion.UInt16FromBuffer( directoryBuffer.Slice( TagByteStart, 2 ), byteOrder );
      var dataType = DataConversion.Int16FromBuffer( directoryBuffer.Slice( TypeByteStart, 2 ), byteOrder );
      var componentCount = DataConversion.Int32FromBuffer( directoryBuffer.Slice( ComponentCountByteStart, 4 ), byteOrder );
      var valueBuffer = directoryBuffer.Slice( DataByteStart, 4 );

      return new CrackedData( tag, dataType, componentCount, valueBuffer );
    }
      
    internal readonly ushort Tag { get; init; }
    internal readonly short DataType { get; init; }
    internal readonly int ComponentCount { get; init; }
    internal readonly ReadOnlySpan<byte> DataBuffer { get; init; }

    private const int TagByteStart = 0;
    private const int TypeByteStart = 2;
    private const int ComponentCountByteStart = 4;
    private const int DataByteStart = 8;
  }

  private static ExifComponent ExtractComponent( MetadataProfileType profile, ReadOnlySpan<byte> directoryBuffer, long dataStart, TiffByteOrder byteOrder )
  {
    var data = CrackedData.Create( directoryBuffer, byteOrder );
    return new ExifComponent( profile: profile,
                              tag: data.Tag,
                              dataType: data.DataType,
                              componentCount: data.ComponentCount,
                              dataValueBuffer: data.DataBuffer,
                              dataStart: dataStart,
                              byteOrder: byteOrder );
  }
}
