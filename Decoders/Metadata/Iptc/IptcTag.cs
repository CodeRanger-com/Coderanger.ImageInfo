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

  [MetadataTagDetails( "Subject Reference", "A structured definition of the subject matter" )]
  public const ushort Category = 15;

  /// <summary>
  /// Supplemental categories. Max length is 32.
  /// </summary>
  public const ushort SupplementalCategory = 20;

  /// <summary>
  /// Fixture identifier; not repeatable. Max length is 32.
  /// </summary>
  public const ushort FixtureIdentifier = 22;

  /// <summary>
  /// Keywords. Max length is 64.
  /// </summary>
  public const ushort Keywords = 25;

  /// <summary>
  /// Location code. Max length is 3.
  /// </summary>
  public const ushort LocationCode = 26;

  /// <summary>
  /// Location name. Max length is 64.
  /// </summary>
  public const ushort LocationName = 27;

  /// <summary>
  /// Release date. Format should be CCYYMMDD.
  /// Not repeatable; max length is 8.
  /// <example>
  /// A date will be formatted as CCYYMMDD; e.g. "19890317" for 17 March 1989.
  /// </example>
  /// </summary>
  public const ushort ReleaseDate = 30;

  /// <summary>
  /// Release time. Format should be HHMMSS±HHMM.
  /// Not repeatable; max length is 11.
  /// <example>
  /// A time value will be formatted as HHMMSS±HHMM; e.g. "090000+0200" for 9 o'clock Berlin time;
  /// two hours ahead of UTC.
  /// </example>
  /// </summary>
  public const ushort ReleaseTime = 35;

  /// <summary>
  /// Expiration date. Format should be CCYYMMDD.
  /// Not repeatable; max length is 8.
  /// <example>
  /// A date will be formatted as CCYYMMDD; e.g. "19890317" for 17 March 1989.
  /// </example>
  /// </summary>
  public const ushort ExpirationDate = 37;

  /// <summary>
  /// Expiration time. Format should be HHMMSS±HHMM.
  /// Not repeatable; max length is 11.
  /// <example>
  /// A time value will be formatted as HHMMSS±HHMM; e.g. "090000+0200" for 9 o'clock Berlin time;
  /// two hours ahead of UTC.
  /// </example>
  /// </summary>
  public const ushort ExpirationTime = 38;

  /// <summary>
  /// Special instructions; not repeatable. Max length is 256.
  /// </summary>
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

  /// <summary>
  /// Reference service. Max length is 10.
  /// </summary>
  public const ushort ReferenceService = 45;

  /// <summary>
  /// Reference date. Format should be CCYYMMDD.
  /// Not repeatable; max length is 8.
  /// <example>
  /// A date will be formatted as CCYYMMDD; e.g. "19890317" for 17 March 1989.
  /// </example>
  /// </summary>
  public const ushort ReferenceDate = 47;

  /// <summary>
  /// ReferenceNumber. Max length is 8.
  /// </summary>
  public const ushort ReferenceNumber = 50;

  /// <summary>
  /// Created date. Format should be CCYYMMDD.
  /// Not repeatable; max length is 8.
  /// <example>
  /// A date will be formatted as CCYYMMDD; e.g. "19890317" for 17 March 1989.
  /// </example>
  /// </summary>
  public const ushort CreatedDate = 55;

  /// <summary>
  /// Created time. Format should be HHMMSS±HHMM.
  /// Not repeatable; max length is 11.
  /// <example>
  /// A time value will be formatted as HHMMSS±HHMM; e.g. "090000+0200" for 9 o'clock Berlin time;
  /// two hours ahead of UTC.
  /// </example>
  /// </summary>
  public const ushort CreatedTime = 60;

  /// <summary>
  /// Digital creation date. Format should be CCYYMMDD.
  /// Not repeatable; max length is 8.
  /// <example>
  /// A date will be formatted as CCYYMMDD; e.g. "19890317" for 17 March 1989.
  /// </example>
  /// </summary>
  public const ushort DigitalCreationDate = 62;

  /// <summary>
  /// Digital creation time. Format should be HHMMSS±HHMM.
  /// Not repeatable; max length is 11.
  /// <example>
  /// A time value will be formatted as HHMMSS±HHMM; e.g. "090000+0200" for 9 o'clock Berlin time;
  /// two hours ahead of UTC.
  /// </example>
  /// </summary>
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

  /// <summary>
  /// Credit; not repeatable. Max length is 32.
  /// </summary>
  public const ushort Credit = 110;

  /// <summary>
  /// Source; not repeatable. Max length is 32.
  /// </summary>
  public const ushort Source = 115;

  /// <summary>
  /// Copyright notice; not repeatable. Max length is 128.
  /// </summary>
  public const ushort CopyrightNotice = 116;

  /// <summary>
  /// Contact. Max length 128.
  /// </summary>
  public const ushort Contact = 118;

  /// <summary>
  /// Caption; not repeatable. Max length is 2000.
  /// </summary>
  public const ushort Caption = 120;

  /// <summary>
  /// Local caption.
  /// </summary>
  public const ushort LocalCaption = 121;

  /// <summary>
  /// Caption writer. Max length is 32.
  /// </summary>
  public const ushort WriterEditor = 122;

  /// <summary>
  /// Image type; not repeatable. Max length is 2.
  /// </summary>
  public const ushort ImageType = 130;

  /// <summary>
  /// Image orientation; not repeatable. Max length is 1.
  /// </summary>
  [MetadataTagEnum( "L", "Landscape" )]
  [MetadataTagEnum( "P", "Portrait" )]
  [MetadataTagEnum( "S", "Square" )]
  public const ushort ImageOrientation = 131;

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
  public const ushort ObjectPreviewFileFormat = 200;

  /// <summary>
  /// Custom field 2
  /// </summary>
  public const ushort ObjectPreviewFileVersion = 201;

  public const ushort CustomField3 = 202;
  public const ushort CustomField4 = 203;
  public const ushort CustomField5 = 204;
  public const ushort CustomField6 = 205;
  public const ushort CustomField7 = 206;
  public const ushort CustomField8 = 207;
  public const ushort CustomField9 = 208;
  public const ushort CustomField10 = 209;
  public const ushort CustomField11 = 210;
  public const ushort CustomField12 = 211;
  public const ushort CustomField13 = 212;
  public const ushort CustomField14 = 213;
  public const ushort CustomField15 = 214;
  public const ushort CustomField16 = 215;
  public const ushort CustomField17 = 216;
  public const ushort CustomField18 = 217;
  public const ushort CustomField19 = 218;
  public const ushort CustomField20 = 219;

  public const ushort Prefs = 221;
  public const ushort ClassifyState = 225;
  public const ushort SimilarityIndex = 228;
  public const ushort DocumentNotes = 230;
  public const ushort DocumentHistory = 231;
  public const ushort ExifCameraInfo = 232;
  public const ushort CatalogSets = 255;
}
