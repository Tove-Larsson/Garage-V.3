using Garage_ovning5.Vehicles;
using System;

namespace Garage_ovning5.UI
{
	internal static class VehicleInput
	{
		public static string ReadLicensePlateNumber()
		{
			while (true)
			{
				Console.WriteLine("\nWhat is your vehicles license plate number?\n");

				string input = LicensePlate.Normalize(Console.ReadLine());

				if (LicensePlate.IsValid(input))
				{
					return input;
				}

				Console.WriteLine("\nLicense plate number must be three letters followed by three digits (e.g. ABC123), or three letters, two digits and one letter (e.g. ABC12D)\n");
			}
		}

		public static VehicleType? ReadVehicleType()
		{
			while (true)
			{
				Console.WriteLine("\nWhat type of vehicle do you want to park?\n");
				Console.WriteLine("1. Car");
				Console.WriteLine("2. Motorcycle");
				Console.WriteLine("3. Bus");
				Console.WriteLine("4. Airplane");
				Console.WriteLine("5. Boat");
				Console.WriteLine("0. Cancel");

				switch (Console.ReadLine())
				{
					case "1": return VehicleType.Car;
					case "2": return VehicleType.Motorcycle;
					case "3": return VehicleType.Bus;
					case "4": return VehicleType.Airplane;
					case "5": return VehicleType.Boat;
					case "0": return null;
					default:
						Console.WriteLine("Invalid input, please try again");
						break;
				}
			}
		}

		public static VehicleColor ReadVehicleColor()
		{
			while (true)
			{
				Console.WriteLine("\nWhat color is your vehicle?\n");
				Console.WriteLine("1. Red");
				Console.WriteLine("2. Blue");
				Console.WriteLine("3. Black");
				Console.WriteLine("4. White");
				Console.WriteLine("5. Green");
				Console.WriteLine("6. Yellow");
				Console.WriteLine("7. Pink");
				Console.WriteLine("8. Silver");
				Console.WriteLine("9. Grey");
				Console.WriteLine("10. Purple");

				switch (Console.ReadLine())
				{
					case "1": return VehicleColor.Red;
					case "2": return VehicleColor.Blue;
					case "3": return VehicleColor.Black;
					case "4": return VehicleColor.White;
					case "5": return VehicleColor.Green;
					case "6": return VehicleColor.Yellow;
					case "7": return VehicleColor.Pink;
					case "8": return VehicleColor.Silver;
					case "9": return VehicleColor.Grey;
					case "10": return VehicleColor.Purple;
					default:
						Console.WriteLine("Invalid input, please try again");
						break;
				}

			}
		}

		public static VehicleColor? ReadOptionalVehicleColor()
		{
			while (true)
			{
				Console.WriteLine("\nWhat color do you want to search for?\n");
				Console.WriteLine("0. Any color");
				Console.WriteLine("1. Red");
				Console.WriteLine("2. Blue");
				Console.WriteLine("3. Black");
				Console.WriteLine("4. White");
				Console.WriteLine("5. Green");
				Console.WriteLine("6. Yellow");
				Console.WriteLine("7. Pink");
				Console.WriteLine("8. Silver");
				Console.WriteLine("9. Grey");
				Console.WriteLine("10. Purple");

				switch (Console.ReadLine())
				{
					case "0": return null;
					case "1": return VehicleColor.Red;
					case "2": return VehicleColor.Blue;
					case "3": return VehicleColor.Black;
					case "4": return VehicleColor.White;
					case "5": return VehicleColor.Green;
					case "6": return VehicleColor.Yellow;
					case "7": return VehicleColor.Pink;
					case "8": return VehicleColor.Silver;
					case "9": return VehicleColor.Grey;
					case "10": return VehicleColor.Purple;
					default:
						Console.WriteLine("Invalid input, please try again");
						break;
				}
			}
		}

		public static int? ReadOptionalNumberOfWheels()
		{

			while (true)
			{
				Console.WriteLine("\nHow many wheels do you want to search for in a vehicle? Press enter for any\n");

				string? input = Console.ReadLine();

				if (string.IsNullOrWhiteSpace(input))
				{
					return null;
				}

				if (int.TryParse(input, out int wheels) && wheels >= 0)
				{
					return wheels;
				}

				Console.WriteLine("Invalid input, please enter a number or press Enter");
			}

		}

		public static VehicleType? ReadOptionalVehicleType()
		{
			while (true)
			{
				Console.WriteLine("\nWhat type of vehicle do you want to search for?\n");
				Console.WriteLine("0. Any type");
				Console.WriteLine("1. Car");
				Console.WriteLine("2. Motorcycle");
				Console.WriteLine("3. Bus");
				Console.WriteLine("4. Airplane");
				Console.WriteLine("5. Boat");

				switch (Console.ReadLine())
				{
					case "0": return null;
					case "1": return VehicleType.Car;
					case "2": return VehicleType.Motorcycle;
					case "3": return VehicleType.Bus;
					case "4": return VehicleType.Airplane;
					case "5": return VehicleType.Boat;
					default:
						Console.WriteLine("Invalid input, please try again");
						break;
				}
			}
		}

	}
}
