using System.Globalization;

namespace Gecko.NCore.Client
{
	public class NCoreUtility
	{
		public const char QuoteChar = '\'';   //  '
		public const char EscapeChar = '\\';  //  \

		public static string QuoteField(string toEscape)
		{
			return QuoteChar.ToString(CultureInfo.InvariantCulture) +
			       toEscape.Replace(QuoteChar.ToString(CultureInfo.InvariantCulture), EscapeChar.ToString(CultureInfo.InvariantCulture) + QuoteChar.ToString(CultureInfo.InvariantCulture)) +
			       QuoteChar.ToString(CultureInfo.InvariantCulture);
		}
	}
}