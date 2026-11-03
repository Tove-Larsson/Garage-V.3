using Garage_ovning5.Vehicles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Garage_ovning5.Garages.Persistence
{
	internal class GarageSaveData
	{
		public int Capacity { get; set; }
		public List<IVehicle> ParkedVehicles { get; set; } = new();
	}

	internal class GarageFileHandler : IGarageFileHandler
	{
		private const string FolderPath = "data";
		private const string FileName = "garage.json";

		public void SaveFile(IHandler garageHandler)
		{
			Directory.CreateDirectory(FolderPath);
			string filePath = GetFilePath();

			var saveData = new GarageSaveData
			{
				Capacity = garageHandler.Capacity,
				ParkedVehicles = new List<IVehicle>(garageHandler.ListParkedVehicles())
			};

			var options = new JsonSerializerOptions { WriteIndented = true };

			string jsonString = JsonSerializer.Serialize(saveData, options);

			File.WriteAllText(filePath, jsonString);

			Console.WriteLine($"The garage was saved to path: {Path.GetFullPath(filePath)}");
		}

		public GarageSaveData? LoadFile()
		{
			string filePath = GetFilePath();

			if (!File.Exists(filePath))
			{
				return null;
			}

			try
			{
				string jsonString = File.ReadAllText(filePath);

				if (string.IsNullOrWhiteSpace(jsonString))
				{
					Console.WriteLine("The save file is empty.");
					return null;
				}

				var saveData = JsonSerializer.Deserialize<GarageSaveData>(jsonString);

				if (saveData is null)
				{
					Console.WriteLine("Could not parse the contents of the save file.");
					return null;
				}

				// Validate that reasonable limits are maintained
				if (saveData.Capacity <= 0 || saveData.Capacity > Garage<IVehicle>.MaximumCapacity)
				{
					Console.WriteLine($"Invalid capacity ({saveData.Capacity}) found in the save file.");
					return null;
				}

				return saveData;
			}
			catch (JsonException ex)
			{
				Console.WriteLine($"Could not read the JSON format in the file: {ex.Message}");
				return null;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An unexpected error occurred while loading: {ex.Message}");
				return null;
			}
		}

		private string GetFilePath()
		{
			return Path.Combine(FolderPath, FileName);
		}
	}
}
