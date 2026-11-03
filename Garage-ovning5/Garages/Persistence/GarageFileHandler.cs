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

	internal class GarageFileHandler
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
				return JsonSerializer.Deserialize<GarageSaveData>(jsonString);
			}
			catch (JsonException ex)
			{
				Console.WriteLine($"Kunde inte avläsa JSON-formatet i filen: {ex.Message}");
				return null;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Ett oväntat fel uppstod vid inläsning: {ex.Message}");
				return null;
			}
		}

		private string GetFilePath()
		{
			return Path.Combine(FolderPath, FileName);
		}
	}
}
