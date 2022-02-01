// -----------------------------------------------------------------------
// <copyright file="IsExternalInit.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment>Allows C# Record Init syntax in .net standard projects</comment>
// -----------------------------------------------------------------------

namespace System.Runtime.CompilerServices;

using System.ComponentModel;

/// <summary>
/// Reserved to be used by the compiler for tracking metadata.
/// This class should not be used by developers in source code.
/// This dummy class is required to compile records when targeting .NET Standard
/// </summary>
[EditorBrowsable( EditorBrowsableState.Never )]
public static class IsExternalInit
{
}
