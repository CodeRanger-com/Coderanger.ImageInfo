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

  internal static IMetadataTypedValue? Create( ushort record, ushort tag, ReadOnlySpan<byte> data )
  {
    if( record == 1 || record >= 7 )
    {
      // We dont care about the Envelope or ObjectData tags
      return null;
    }

    IMetadataTypedValue? tagValue = tag switch
    {
      IptcTag.Urgency 
      or IptcTag.ActionAdvised 
      or IptcTag.ObjectPreviewFileFormat => new IptcEnum( tag, IptcEnum.EnumType.Numeric ),
      
      IptcTag.ObjectCycle 
      or IptcTag.ImageOrientation => new IptcEnum( tag, IptcEnum.EnumType.String ),
      
      IptcTag.RecordVersion 
      or IptcTag.ObjectPreviewFileVersion => new IptcUShort( tag ),
      
      IptcTag.ObjectType 
      or IptcTag.ObjectAttribute 
      or IptcTag.ObjectName 
      or IptcTag.EditStatus 
      or IptcTag.EditorialUpdate 
      or IptcTag.SubjectReference 
      or IptcTag.Category 
      or IptcTag.SupplementalCategory 
      or IptcTag.FixtureIdentifier 
      or IptcTag.Keywords 
      or IptcTag.LocationCode 
      or IptcTag.LocationName 
      or IptcTag.SpecialInstructions 
      or IptcTag.ReferenceService 
      or IptcTag.OriginatingProgram 
      or IptcTag.ProgramVersion 
      or IptcTag.Byline 
      or IptcTag.BylineTitle 
      or IptcTag.SubLocation 
      or IptcTag.ProvinceState 
      or IptcTag.CountryCode 
      or IptcTag.Country 
      or IptcTag.OriginalTransmissionReference 
      or IptcTag.Headline 
      or IptcTag.Credit 
      or IptcTag.Source 
      or IptcTag.CopyrightNotice 
      or IptcTag.Contact 
      or IptcTag.Caption 
      or IptcTag.WriterEditor 
      or IptcTag.ImageType 
      or IptcTag.City => new IptcString( tag ),
      
      IptcTag.ReleaseDate 
      or IptcTag.ExpirationDate 
      or IptcTag.CreatedDate 
      or IptcTag.DigitalCreationDate 
      or IptcTag.ReferenceDate => new IptcDate( tag ),
      
      IptcTag.ReleaseTime 
      or IptcTag.ExpirationTime 
      or IptcTag.CreatedTime 
      or IptcTag.DigitalCreationTime => new IptcTime( tag ),
      
      IptcTag.ReferenceNumber => new IptcULong( tag ),
      
      _ => new IptcByte( tag ),
    };

    tagValue.SetValue( data );
    if( tagValue.HasValue )
    {
      return tagValue;
    }

    return null;
  }
}
