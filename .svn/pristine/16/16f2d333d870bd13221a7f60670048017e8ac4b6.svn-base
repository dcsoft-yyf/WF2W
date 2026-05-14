using System.Text;

namespace DouglasCrockford.JsMin.Utilities
{
	/// <summary>
	/// Extensions for StringBuilder
	/// </summary>
	internal static class StringBuilderExtensions
	{
		/// <summary>
		/// Removes the all leading white-space characters from the current <see cref="StringBuilder"/> instance
		/// </summary>
		/// <param name="source">Instance of <see cref="StringBuilder"/></param>
		/// <returns>Instance of <see cref="StringBuilder"/> without leading white-space characters</returns>
		public static StringBuilder TrimStart(StringBuilder source)
		{
			int charCount = source.Length;
			if (charCount == 0)
			{
				return source;
			}

			int charIndex = 0;

			while (charIndex < charCount)
			{
				char charValue = source[charIndex];
				if (! CharExtensions.IsWhitespace( charValue))
				{
					break;
				}

				charIndex++;
			}

			if (charIndex > 0)
			{
				source.Remove(0, charIndex);
			}

			return source;
		}
	}
}