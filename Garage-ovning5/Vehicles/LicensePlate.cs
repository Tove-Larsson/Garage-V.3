using System.Text.RegularExpressions;

namespace Garage_ovning5.Vehicles
{
	internal static class LicensePlate
	{
		private const string LicensePlatePattern = "^[a-zA-Z]{3}([0-9]{3}|[0-9]{2}[a-zA-Z])$";

		public static bool IsValid(string? licensePlateNumber)
		{
			if (string.IsNullOrWhiteSpace(licensePlateNumber))
			{
				return false;
			}

			return Regex.IsMatch(licensePlateNumber, LicensePlatePattern);
		}

		public static string Normalize(string? licensePlateNumber)
		{
			return licensePlateNumber?.Trim().ToUpperInvariant() ?? string.Empty;
		}
	}
}