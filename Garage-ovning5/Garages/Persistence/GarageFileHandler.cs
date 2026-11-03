using Garage_ovning5.Vehicles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Garage_ovning5.Garages.Persistence
{
	internal class GarageFileHandler
	{
		private const string FolderPath = "data";
		private const string FileName = "garage.txt";

		public void SaveFile(IHandler garageHandler)
		{
			Directory.CreateDirectory(FolderPath);

			string filePath = GetFilePath();

			List<string> rows = CreateGarageRows(garageHandler);

			File.WriteAllLines(filePath, rows);
		}

		private string GetFilePath()
		{
			string filePath = Path.Combine(FolderPath, FileName);

			Console.WriteLine($"Full path to the file: {Path.GetFullPath(filePath)}");

			return filePath;
		}

		private List<string> CreateGarageRows(IHandler garageHandler)
		{
			string capacity = garageHandler.Capacity.ToString();

			var parkedVehicles = garageHandler.ListParkedVehicles();

			List<string> garageRows = new List<string>();

			garageRows.Add(capacity);

			foreach (var vehicle in parkedVehicles)
			{
				garageRows.Add(CreateVehicleRow(vehicle));
			}

			return garageRows;
		}

		private string CreateVehicleRow(IVehicle vehicle)
		{
			string commonPart = $"{vehicle.VehicleType};{vehicle.LicensePlateNumber};{vehicle.Color};{vehicle.NumberOfWheels}";

			return $"{commonPart};{CreateVehicleSpecificDetail(vehicle)}";
		}

		private string CreateVehicleSpecificDetail(IVehicle vehicle)
		{
			switch (vehicle)
			{
				case Car car:
					return car.FuelType.ToString();
				case Motorcycle motorcycle:
					return motorcycle.CylinderVolume.ToString();
				case Bus bus:
					return bus.NumberOfSeats.ToString();
				case Airplane airplane:
					return airplane.NumberOfEngines.ToString();
				case Boat boat:
					return boat.Length.ToString(CultureInfo.InvariantCulture);
				default:
					throw new NotSupportedException($"Vehicle type {vehicle.VehicleType} cannot be saved.");
			}
		}
	}
}
