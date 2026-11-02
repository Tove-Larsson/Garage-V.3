using Garage_ovning5.Vehicles;
using System;

namespace Garage_ovning5.UI
{
	internal static class VehicleCreator
	{

		public static IVehicle? CreateVehicle(string licensePlateNumber, VehicleColor color, VehicleType type)
		{

			switch(type)
			{
				case VehicleType.Car:
					return CreateCar(licensePlateNumber, color);
				case VehicleType.Motorcycle:
					return CreateMotorcycle(licensePlateNumber, color);
				case VehicleType.Bus:
					return CreateBus(licensePlateNumber, color);
				case VehicleType.Airplane:
					return CreateAirplane(licensePlateNumber, color);
				case VehicleType.Boat:
					return CreateBoat(licensePlateNumber, color);
				default:
					break;
			}

			return null;

		}

		private static IVehicle? CreateCar(string licensePlateNumber, VehicleColor color)
		{

			int numberOfWheels = InputHelper.ReadInt("How many wheels does your car have?");
			FuelType? fuelType = ReadFuelType();

			if (fuelType is not null)
			{
				return new Car(licensePlateNumber, color, numberOfWheels, fuelType.Value);
			}

			return null;
		}

		private static FuelType? ReadFuelType()
		{

			while (true)
			{
				Console.WriteLine("What type of fuel does your car use?");
				Console.WriteLine("1. Gasoline");
				Console.WriteLine("2. Diesel");
				Console.WriteLine("3. Electric");
				Console.WriteLine("4. Hybrid");
				Console.WriteLine("0. Cancel");

				switch (Console.ReadLine())
				{
					case "1": return FuelType.Gasoline;
					case "2": return FuelType.Diesel;
					case "3": return FuelType.Electric;
					case "4": return FuelType.Hybrid;
					case "0": return null;
					default:
						Console.WriteLine("Invalid input, please try again");
						break;
				}
			}
		}

		private static IVehicle? CreateMotorcycle(string licensePlateNumber, VehicleColor color)
		{
			int numberOfWheels = InputHelper.ReadInt("How many wheels does your motorcycle have?");
			int cylinderVolume = InputHelper.ReadInt("What is your motorcycles cylinder volume?");

			return new Motorcycle(licensePlateNumber, color, numberOfWheels, cylinderVolume);
		}


		private static IVehicle? CreateBus(string licensePlateNumber, VehicleColor color)
		{
			int numberOfSeats = InputHelper.ReadInt("How many seats does your bus have?");
			int numberOfWheels = InputHelper.ReadInt("How many wheels does your bus have?");
			
			return new Bus(licensePlateNumber, color, numberOfWheels, numberOfSeats);
		}

		private static IVehicle? CreateAirplane(string licensePlateNumber, VehicleColor color)
		{
			int numberOfWheels = InputHelper.ReadInt("How many wheels does your airplane have?");
			int numberOfEngines = InputHelper.ReadInt("How many engines does your airplane have?");

			return new Airplane(licensePlateNumber, color, numberOfWheels, numberOfEngines);

		}

		private static IVehicle? CreateBoat(string licensePlateNumber, VehicleColor color)
		{

			double length = InputHelper.ReadDouble("What is your boats length?");

			return new Boat(licensePlateNumber, color, length);
		}
	}
}
