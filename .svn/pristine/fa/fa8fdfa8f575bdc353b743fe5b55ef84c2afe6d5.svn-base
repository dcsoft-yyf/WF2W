using System.Runtime.CompilerServices;

namespace DouglasCrockford.JsMin.Utilities
{
	/// <summary>
	/// Extensions for Char
	/// </summary>
	internal static class CharExtensions
	{
		[MethodImpl((MethodImplOptions)256 /* AggressiveInlining */)]
		public static bool IsWhitespace( char source)
		{
			return source == ' ' || (source >= '\t' && source <= '\r');
		}
	}
}