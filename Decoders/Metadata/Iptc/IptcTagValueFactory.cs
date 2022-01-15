// -----------------------------------------------------------------------
// <copyright file="IptcTagValueFactory.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Iptc;

using Coderanger.ImageInfo.Decoders.Metadata.Iptc.Types;

internal class IptcTagValueFactory
{
  internal static bool IsRepeatable( ushort tag )
  {
    return tag switch
    {
      IptcTag.ObjectAttribute 
      or IptcTag.SubjectReference 
      or IptcTag.SupplementalCategory 
      or IptcTag.Keywords 
      or IptcTag.LocationCode 
      or IptcTag.LocationName 
      or IptcTag.ReferenceService 
      or IptcTag.ReferenceDate 
      or IptcTag.ReferenceNumber 
      or IptcTag.Byline 
      or IptcTag.BylineTitle 
      or IptcTag.Contact 
      or IptcTag.WriterEditor => true,

      _ => false,
    };
  }

  internal static IMetadataTypedValue? Create( ushort record, ushort tag, byte[] data )
  {
    if( record == 1 || record >= 7 )
    {
      // We dont care about the Envelope or ObjectData tags
      return null;
    }

    IMetadataTypedValue? tagValue;

    switch( tag )
    {
      case IptcTag.Urgency:
      case IptcTag.ActionAdvised:
      case IptcTag.ObjectPreviewFileFormat:
        tagValue = new IptcEnum( tag, data, IptcEnum.EnumType.Numeric );
        break;

      case IptcTag.ObjectCycle:
      case IptcTag.ImageOrientation:
        tagValue = new IptcEnum( tag, data, IptcEnum.EnumType.String );
        break;

      case IptcTag.RecordVersion:
      case IptcTag.ObjectPreviewFileVersion:
        tagValue = new IptcUShort( tag, data );
        break;

      case IptcTag.ObjectType:
      case IptcTag.ObjectAttribute:
      case IptcTag.ObjectName:
      case IptcTag.EditStatus:
      case IptcTag.EditorialUpdate:
      case IptcTag.SubjectReference:
      case IptcTag.Category:
      case IptcTag.SupplementalCategory:
      case IptcTag.FixtureIdentifier:
      case IptcTag.Keywords:
      case IptcTag.LocationCode:
      case IptcTag.LocationName:
      case IptcTag.SpecialInstructions:
      case IptcTag.ReferenceService:
      case IptcTag.OriginatingProgram:
      case IptcTag.ProgramVersion:
      case IptcTag.Byline:
      case IptcTag.BylineTitle:
      case IptcTag.SubLocation:
      case IptcTag.ProvinceState:
      case IptcTag.CountryCode:
      case IptcTag.Country:
      case IptcTag.OriginalTransmissionReference:
      case IptcTag.Headline:
      case IptcTag.Credit:
      case IptcTag.Source:
      case IptcTag.CopyrightNotice:
      case IptcTag.Contact:
      case IptcTag.Caption:
      case IptcTag.WriterEditor:
      case IptcTag.ImageType:
        tagValue = new IptcString( tag, data );
        break;

      case IptcTag.City:
        tagValue = new IptcString( tag, data );
        break;

      case IptcTag.ReleaseDate:
      case IptcTag.ExpirationDate:
      case IptcTag.CreatedDate:
      case IptcTag.DigitalCreationDate:
      case IptcTag.ReferenceDate:
        tagValue = new IptcDate( tag, data );
        break;

      case IptcTag.ReleaseTime:
      case IptcTag.ExpirationTime:
      case IptcTag.CreatedTime:
      case IptcTag.DigitalCreationTime:
        tagValue = new IptcTime( tag, data );
        break;

      case IptcTag.ReferenceNumber:
        tagValue = new IptcULong( tag, data );
        break;

      default:
        tagValue = new IptcByte( tag, data );
        break;
    }

    tagValue.SetValue();

    return tagValue;
  }
}
