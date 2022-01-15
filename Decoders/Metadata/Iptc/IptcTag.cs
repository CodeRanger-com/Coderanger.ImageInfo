// -----------------------------------------------------------------------
// <copyright file="IptcTag.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Iptc;

public class IptcTag : IMetadataTag
{
  [MetadataTagDetails( "Record Version", "Identifies the version of the Information Interchange Model" )]
  public const ushort RecordVersion = 0;

  [MetadataTagDetails( "Object Type Reference", "Used to distinguish between different types of objects within the IIM" )]
  public const ushort ObjectType = 3;

  [MetadataTagDetails( "Object Attribute Reference", "Defines the nature of the object independent of the Subject" )]
  public const ushort ObjectAttribute = 4;

  [MetadataTagDetails( "Object Name", "Used as a shorthand reference for the object" )]
  public const ushort ObjectName = 5;

  [MetadataTagDetails( "Edit Status", "Status of the objectdata, according to the practice of the provide" )]
  public const ushort EditStatus = 7;

  [MetadataTagDetails( "Editorial Update", "Indicates the type of update that this object provides to a previous object" )]
  public const ushort EditorialUpdate = 8;

  [MetadataTagEnum( 1, "Priority 0 Most Urgent" )]
  [MetadataTagEnum( 1, "Priority 1 Most Urgent" )]
  [MetadataTagEnum( 2, "Priority 2 High Urgent" )]
  [MetadataTagEnum( 3, "Priority 3 High Urgent" )]
  [MetadataTagEnum( 4, "Priority 4 High Urgent" )]
  [MetadataTagEnum( 5, "Priority 5 Normal Urgency" )]
  [MetadataTagEnum( 6, "Priority 6 Less Urgent" )]
  [MetadataTagEnum( 7, "Priority 7 Less Urgent" )]
  [MetadataTagEnum( 8, "Priority 8 Least Urgent" )]
  [MetadataTagEnum( 9, "Priority 9 User Defined Priority" )]
  [MetadataTagDetails( "Urgency", "Specifies the editorial urgency of content and not necessarily the envelope handling priority" )]
  public const ushort Urgency = 10;

  [MetadataTagDetails( "Subject Reference", "A structured definition of the subject matter" )]
  public const ushort SubjectReference = 12;

  [MetadataTagDetails( "Category", "Identifies the subject of the object in the opinion of the provider" )]
  public const ushort Category = 15;

  [MetadataTagDetails( "Supplemental Category", "Supplemental categories further refine the subject of an object" )]
  public const ushort SupplementalCategory = 20;

  [MetadataTagDetails( "Fixture Identifier", "Identifies object that recurs often and predictably. Enables users to immediately find or recall such an object" )]
  public const ushort FixtureIdentifier = 22;

  [MetadataTagDetails( "Keywords", "Indicates specific information retrieval words" )]
  public const ushort Keywords = 25;

  [MetadataTagDetails( "Location Code", "Indicates the ISO code of a country/geographical location referenced by the content of the object" )]
  public const ushort LocationCode = 26;

  [MetadataTagDetails( "Location Name", "Provides a full, publishable name of a country/geographical location referenced by the content of the object" )]
  public const ushort LocationName = 27;

  [MetadataTagDetails( "Release Date", "Indicates the earliest date the provider intends the object to be used" )]
  public const ushort ReleaseDate = 30;

  [MetadataTagDetails( "Release Time", "Indicates the earliest time the provider intends the object to be used" )]
  public const ushort ReleaseTime = 35;

  [MetadataTagDetails( "Expiration Date", "Indicates the latest date the provider intends the object to be used" )]
  public const ushort ExpirationDate = 37;

  [MetadataTagDetails( "Expiration Time", "Indicates the latest time the provider intends the object to be used" )]
  public const ushort ExpirationTime = 38;

  [MetadataTagDetails( "Special Instructions", "Other editorial instructions concerning the use of the object, such as embargoes and warnings" )]
  public const ushort SpecialInstructions = 40;

  /// <summary>
  /// Action advised; not repeatable. Max length is 2.
  /// </summary>
  [MetadataTagEnum( 1, "Object Kill" )]
  [MetadataTagEnum( 2, "Object Replace" )]
  [MetadataTagEnum( 3, "Object Append" )]
  [MetadataTagEnum( 4, "Object Reference" )]
  [MetadataTagDetails( "Action Advised", "Indicates the type of action that this object provides to a previous object" )]
  public const ushort ActionAdvised = 42;

  [MetadataTagDetails( "Reference Service", "Identifies the Service Identifier of a prior envelope to which the current object refers" )]
  public const ushort ReferenceService = 45;

  [MetadataTagDetails( "Reference Date", "Identifies the date of a prior envelope to which the current object refers" )]
  public const ushort ReferenceDate = 47;

  [MetadataTagDetails( "Reference Number", "Identifies the Envelope Number of a prior envelope to which the current object refers" )]
  public const ushort ReferenceNumber = 50;

  [MetadataTagDetails( "Created Date", "Indicates the date the intellectual content of the object was created rather than the date of the creation of the physical representation" )]
  public const ushort CreatedDate = 55;

  [MetadataTagDetails( "Created Time", "Indicates the time the intellectual content of the object was created rather than the date of the creation of the physical representation" )]
  public const ushort CreatedTime = 60;

  [MetadataTagDetails( "Digital Creation Date", "Indicates the date the digital representation of the object was created" )]
  public const ushort DigitalCreationDate = 62;

  [MetadataTagDetails( "Digital Creation Time", "Indicates the time the digital representation of the object was created" )]
  public const ushort DigitalCreationTime = 63;

  [MetadataTagDetails( "Originating Program", "Identifies the type of program used to originate the object data" )]
  public const ushort OriginatingProgram = 65;

  [MetadataTagDetails( "Program Version", "Used to identify the version of the program referenced in Originating Program" )]
  public const ushort ProgramVersion = 70;

  [MetadataTagEnum( "a", "Morning" )]
  [MetadataTagEnum( "b", "Both Morning and Evening" )]
  [MetadataTagEnum( "p", "Evening" )]
  [MetadataTagDetails( "Object Cycle", "Virtually only used in North America" )]
  public const ushort ObjectCycle = 75;

  [MetadataTagDetails( "By-line", "Contains name of the creator, e.g. writer, photographer or graphic artist" )]
  public const ushort Byline = 80;

  [MetadataTagDetails( "By-line Title", "Title of the creator or creators. Where used, a By-line Title should follow the By-line it modifies" )]
  public const ushort BylineTitle = 85;

  [MetadataTagDetails( "City", "Identifies city of origin from which the object originates, according to guidelines established by the provider" )]
  public const ushort City = 90;

  [MetadataTagDetails( "Sub-location", "Identifies the location within a city from which the object originates, according to guidelines established by the provider" )]
  public const ushort SubLocation = 92;

  [MetadataTagDetails( "Province/State", "Identifies Province/State of origin from which the object originates" )]
  public const ushort ProvinceState = 95;

  [MetadataTagDetails( "Country/Primary Location Code", "Indicates the ISO 3166 code of the country/primary location where the intellectual property of the object was created" )]
  public const ushort CountryCode = 100;

  [MetadataTagDetails( "Country/Primary Location Name", "Provides full, publishable, name of the country/primary location where the intellectual property of the object was created, according to guidelines of the provider" )]
  public const ushort Country = 101;

  [MetadataTagDetails( "Original Transmission Reference", "Represents the location of original transmission acccording to practices of the provider" )]
  public const ushort OriginalTransmissionReference = 103;

  [MetadataTagDetails( "Headline", "Publishable entry providing a synopsis of the contents of the object" )]
  public const ushort Headline = 105;

  [MetadataTagDetails( "Credit", "Identifies the provider of the object, not necessarily the owner/creator" )]
  public const ushort Credit = 110;

  [MetadataTagDetails( "Source", "Identifies the original owner of the intellectual content of the object. This could be an agency, a member of an agency or an individual" )]
  public const ushort Source = 115;

  [MetadataTagDetails( "Copyright Notice", "Contains any necessary copyright notice" )]
  public const ushort CopyrightNotice = 116;

  [MetadataTagDetails( "Contact", "Identifies the person or organisation which can provide further background information on the object" )]
  public const ushort Contact = 118;

  [MetadataTagDetails( "Caption", "Textual description of the object, particularly used where the object is not text" )]
  public const ushort Caption = 120;

  [MetadataTagDetails( "Writer/Editor", "Identification of the name of the person involved in the writing, editing or correcting the object or caption/abstract" )]
  public const ushort WriterEditor = 122;

  [MetadataTagDetails( "Image Type", "Digit and character which describes the image type" )]
  public const ushort ImageType = 130;

  [MetadataTagEnum( "L", "Landscape" )]
  [MetadataTagEnum( "P", "Portrait" )]
  [MetadataTagEnum( "S", "Square" )]
  [MetadataTagDetails( "Image Orientation", "Indicates the layout of the image area" )]
  public const ushort ImageOrientation = 131;

  [MetadataTagDetails( "Language Identifier", "Describes the major national language of the object, according to the 2-letter codes of ISO 639:1988" )]
  public const ushort LanguageIdentifier = 135;

  [MetadataTagEnum( 0, "No Object Data" )]
  [MetadataTagEnum( 1, "IPTC-NAA Digital Newsphoto Parameter Record" )]
  [MetadataTagEnum( 2, "IPTC7901 Recommended Message Format" )]
  [MetadataTagEnum( 3, "Adobe Tagged Image File Format (tiff)" )]
  [MetadataTagEnum( 4, "Adobe Illustrator" )]
  [MetadataTagEnum( 5, "AppleSingle" )]
  [MetadataTagEnum( 6, "NAA 89-3" )]
  [MetadataTagEnum( 7, "MacBinary II" )]
  [MetadataTagEnum( 8, "IPTC Unstructured Character Oriented File Format" )]
  [MetadataTagEnum( 9, "United Press International ANPA 1312 variant" )]
  [MetadataTagEnum( 10, "United Press International Down-Load Message" )]
  [MetadataTagEnum( 11, "JPEG File Interchange (jfif)" )]
  [MetadataTagEnum( 12, "Kodak Photo-CD Image-Pac" )]
  [MetadataTagEnum( 13, "Microsoft Bit Mapped Graphics File (bmp)" )]
  [MetadataTagEnum( 14, "Microsoft Digital Audio File (wav)" )]
  [MetadataTagEnum( 15, "Microsoft Audio plus Moving Video (avi)" )]
  [MetadataTagEnum( 16, "PC DOS/Windows Executable Files (exe)" )]
  [MetadataTagEnum( 17, "Compressed Binary File (zip)" )]
  [MetadataTagEnum( 18, "Apple Audio Interchange File Format (aiff)" )]
  [MetadataTagEnum( 19, "Microsoft RIFF Wave" )]
  [MetadataTagEnum( 20, "Macromedia Freehand" )]
  [MetadataTagEnum( 21, "Hypertext Markup Language (html)" )]
  [MetadataTagEnum( 22, "MPEG 2 Audio Layer 2" )]
  [MetadataTagEnum( 23, "MPEG 2 Audio Layer 3" )]
  [MetadataTagEnum( 24, "Adobe Portable Document File (pdf)" )]
  [MetadataTagEnum( 25, "News Industry Text Format (nitf)" )]
  [MetadataTagEnum( 26, "Tape Archive (tar)" )]
  [MetadataTagEnum( 27, "Tidningarnas Telegrambyra NITF version" )]
  [MetadataTagEnum( 28, "Ritzaus Bureau NITF version" )]
  [MetadataTagEnum( 29, "Corel Draw (cdr)" )]
  [MetadataTagDetails( "Preview File Format", "Represents the file format of the object preview" )]
  public const ushort ObjectPreviewFileFormat = 200;

  [MetadataTagDetails( "Preview File Version", "Represents the version of the object preview file format" )]
  public const ushort ObjectPreviewFileVersion = 201;
}
