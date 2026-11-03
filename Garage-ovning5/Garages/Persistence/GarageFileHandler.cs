using Garage_ovning5.Vehicles;
using System;
using System.Collections.Generic;
using System.IO;

namespace Garage_ovning5.Garages.Persistence
{
	internal class GarageFileHandler
	{
		public void SaveFile(IHandler garageHandler)
		{
			string folder = "data";

			string filePath = Path.Combine(folder, "garage.txt");

			Console.WriteLine($"Full path to the file: {Path.GetFullPath(filePath)}");

			Directory.CreateDirectory(folder);

			string capacity = garageHandler.Capacity.ToString();

			var parkedVehicles = garageHandler.ListParkedVehicles();

			List<string> rows = new List<string>();

			rows.Add(capacity);

			foreach (var vehicle in parkedVehicles )
			{
				string vehicleLine = $"{vehicle.VehicleType};{vehicle.LicensePlateNumber};{vehicle.Color};{vehicle.NumberOfWheels}";
				rows.Add(vehicleLine);
			}

			foreach (var line in rows )
			{
				Console.WriteLine(line);
			}


			File.WriteAllLines(filePath, rows);
		}
	}
}
