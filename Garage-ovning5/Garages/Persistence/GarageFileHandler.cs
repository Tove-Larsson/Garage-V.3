using Garage_ovning5.Vehicles;
using System;
using System.Collections.Generic;
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
			return $"{vehicle.VehicleType};{vehicle.LicensePlateNumber};{vehicle.Color};{vehicle.NumberOfWheels}";
		}

	}
}
