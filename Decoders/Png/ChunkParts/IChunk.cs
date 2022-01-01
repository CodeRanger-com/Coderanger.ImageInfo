// -----------------------------------------------------------------------
// <copyright file="IChunk.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Png.ChunkParts;

internal interface IChunk
{
  //public bool IsOfType( byte[] chunkTypeSignature );
  public void LoadData( BinaryReader reader );
  public void SkipToEnd( BinaryReader reader );
}
