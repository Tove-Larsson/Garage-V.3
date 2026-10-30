using System;
using System.Text.RegularExpressions;

namespace Garage_ovning5.Vehicles
{
	internal abstract class Vehicle : IVehicle
	{
		private const string LicensePlatePattern = "^[a-zA-Z]{3}([0-9]{3}|[0-9]{2}[a-zA-Z])$";
		private string _licensePlateNumber = string.Empty;
		private VehicleColor _color;
		private int _numberOfWheels;


		public string LicensePlateNumber
		{
			get { return _licensePlateNumber; }
			private set
			{
				string normalized = NormalizeLicensePlate(value);

				if (!IsValidLicensePlate(normalized))
				{
					throw new ArgumentException("\nLicense plate number must be three letters followed by three digits (e.g. ABC123), or three letters, two digits and one letter (e.g. ABC12D)\n");
				}

				_licensePlateNumber = normalized;
			}
		}

		public VehicleColor Color
		{
			get { return _color; }
			set { _color = value; }
		}

		public int NumberOfWheels
		{
			get { return _numberOfWheels; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Number of wheels can not be negative");
				}
				_numberOfWheels = value;
			}
		}

		public abstract VehicleType VehicleType { get; }


		public Vehicle(string licensePlateNumber, VehicleColor color, int numberOfWheels)
		{
			LicensePlateNumber = licensePlateNumber;
			Color = color;
			NumberOfWheels = numberOfWheels;
		}

		public static bool IsValidLicensePlate(string? licensePlateNumber)
		{
			if (string.IsNullOrWhiteSpace(licensePlateNumber))
			{
				return false;
			}

			return Regex.IsMatch(licensePlateNumber, LicensePlatePattern);
		}

		public static string NormalizeLicensePlate(string? licensePlateNumber)
		{
			return licensePlateNumber?.Trim().ToUpperInvariant() ?? string.Empty;
		}

		public override string ToString()
		{
			return $"Vehicle type: {GetType().Name}\n" +
				   $"License plate: {LicensePlateNumber}\n" +
				   $"Color: {Color}\n" +
				   $"Number of wheels: {NumberOfWheels}\n";
		}
	}
}