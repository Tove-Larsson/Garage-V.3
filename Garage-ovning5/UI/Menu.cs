using Garage_ovning5.Garages;
using Garage_ovning5.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Garage_ovning5.UI
{
	internal class Menu : IUI
	{

		private readonly IHandler _handler;
		public Menu(IHandler handler)
		{
			_handler = handler;
		}

		public void MainMenu()
		{
			bool running = true;

			while (running)
			{
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.WriteLine("\n========= Main Menu =========\n");
				Console.ResetColor();
				Console.WriteLine("Please select what you want to do!\n");

				Console.WriteLine("1. Create a garage");
				Console.WriteLine("2. Create a garage with pre-parked vehicles");
				Console.WriteLine("0. Exit program");

				string? selectedChoice = Console.ReadLine();

				switch (selectedChoice)
				{
					case "1":
						HandleCreateGarage();
						break;
					case "2":
						HandleCreatePreParkedGarage();
						break;
					case "0":
						Console.WriteLine("You chose to end the application, goodbye!");
						running = false;
						break;
					default:
						Console.WriteLine("Invalid input, please try again");
						break;
				}
			}
		}

		private void HandleCreateGarage()
		{

			bool garageCreated = false;

			while (!garageCreated)
			{
				Console.WriteLine("\nHow many parking spaces should the garage have?\n");

				if (int.TryParse(Console.ReadLine(), out int parkingSpaces) && parkingSpaces > 0 && parkingSpaces <= _handler.MaximumCapacity)
				{
					_handler.CreateEmptyGarage(parkingSpaces);
					Console.WriteLine($"\nA garage has been created with {parkingSpaces} parking spaces!\n");
					garageCreated = true;
				}
				else
				{
					Console.WriteLine($"Invalid input, please enter a number between 1 and {_handler.MaximumCapacity}");
				}
			}
			GarageMenu();
		}

		private void HandleCreatePreParkedGarage()
		{
			Console.WriteLine("Create a pre-parked garage chosen");
			Console.WriteLine("\nOne vehicle per vehicle type has been created, plus 5 extra parking spaces!\n");

			_handler.CreatePreParkedGarage();

			GarageMenu();
		}

		private void GarageMenu()
		{
			bool running = true;

			while (running)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("\n========= Garage menu =========\n");
				Console.ResetColor();
				Console.WriteLine("Please select what you want to do!\n");

				Console.WriteLine($"The garage has {_handler.FreeSpaces} free spaces, {_handler.Capacity} in total\n");

				Console.WriteLine("1. Park a vehicle");
				Console.WriteLine("2. Retrieve a vehicle");
				Console.WriteLine("3. List all parked vehicles");
				Console.WriteLine("4. List parked vehicle types");
				Console.WriteLine("5. Search for a vehicle with license plate number");
				Console.WriteLine("6. Search for a vehicle by its properties, one or multiple");
				Console.WriteLine("0. Back to main menu");

				string? selectedChoice = Console.ReadLine();

				switch (selectedChoice)
				{
					case "1":
						HandleParkVehicle();
						break;
					case "2":
						HandleRetrieveVehicle();
						break;
					case "3":
						HandleListParkedVehicles();
						break;
					case "4":
						HandleListVehicleTypes();
						break;
					case "5":
						HandleSearchByLicensePlate();
						break;
					case "6":
						HandleSearchByProperties();
						break;
					case "0":
						Console.WriteLine("Back to main menu!");
						running = false;
						break;
					default:
						Console.WriteLine("Invalid input, please try again");
						break;
				}

			}
		}

		private void HandleParkVehicle()
		{
			if (_handler.FreeSpaces == 0)
			{
				Console.WriteLine("\nThe garage is full!\n");
				return;
			}

			string licensePlateNumber = VehicleInput.ReadLicensePlateNumber();

			if (_handler.IsLicensePlateTaken(licensePlateNumber))
			{
				Console.WriteLine($"\nA vehicle with licence plate number: {licensePlateNumber} already exists\n");
				return;
			}

			VehicleColor color = VehicleInput.ReadVehicleColor();

			VehicleType? type = VehicleInput.ReadVehicleType();

			if (type is null)
			{
				Console.WriteLine("\nParking cancelled\n");
				return;
			}

			try
			{

				Vehicle? vehicle = VehicleCreator.CreateVehicle(licensePlateNumber, color, type.Value);
				if (vehicle is null)
				{
					Console.WriteLine("\nParking cancelled\n");
					return;
				}

				var result = _handler.ParkVehicle(vehicle);

				switch(result)
				{
					case ParkingResult.Success:
						Console.WriteLine($"\nThe {type} with {vehicle.LicensePlateNumber} license plate number was parked successfully!\n");
						break;
					case ParkingResult.LicensePlateTaken:
						Console.WriteLine($"\nA vehicle with licence plate number: {licensePlateNumber} already exists\n");
						break;
					case ParkingResult.Full:
						Console.WriteLine("\nThe garage is full!\n");
						break;
				}

			}
			catch (ArgumentException ex)
			{
				Console.WriteLine(ex.Message);
			}
		
		}

		private void HandleRetrieveVehicle()
		{

			string licensePlateNumber = InputHelper.ReadString("\nPlease enter the license plate number of the vehicle you want to retrieve\n");

			IVehicle? retrievedVehicle = _handler.RetrieveVehicle(licensePlateNumber);

			if (retrievedVehicle is not null)
			{
				Console.WriteLine($"\nRetrieval of vehicle with license plate number: {retrievedVehicle.LicensePlateNumber} was successful!\n");
			}
			else
			{
				Console.WriteLine($"\nNo vehicle with license plate {licensePlateNumber.ToUpperInvariant()} was found.\n");
			}
		}

		private void HandleListParkedVehicles()
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("\n========= Parked vehicles =========\n");
			Console.ResetColor();

			var parkedVehicles = _handler.ListParkedVehicles();

			if (!parkedVehicles.Any())
			{
				Console.WriteLine("\nThere are no vehicles parked in the garage.\n");
				return;
			}

			foreach (var vehicle in parkedVehicles)
			{
				Console.WriteLine($"{vehicle}\n");
			}
		}

		private void HandleListVehicleTypes()
		{
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine("\n========= Parked vehicle types =========\n");
			Console.ResetColor();

			var countOfVehicleTypes = _handler.ListVehicleTypes();

			foreach (var type in countOfVehicleTypes)
			{
				Console.WriteLine($"{type.Key}: {type.Value}\n");
			}
		}

		private void HandleSearchByLicensePlate()
		{
			string searchedLicensePlate = InputHelper.ReadString("Please enter a licence plate number to see if it is parked!");

			IVehicle? vehicle = _handler.SearchByLicensePlate(searchedLicensePlate);

			if (vehicle is not null)
			{
				Console.WriteLine($"\nA parked vehicle with {vehicle.LicensePlateNumber} was found!\n");
				Console.WriteLine(vehicle);
			}
			else
			{
				Console.WriteLine($"\nNo vehicle with license plate {searchedLicensePlate} was found.\n");
			}
			
		}

		private void HandleSearchByProperties()
		{

			VehicleColor? color = VehicleInput.ReadOptionalVehicleColor();

			int? numberOfWheels = VehicleInput.ReadOptionalNumberOfWheels();

			VehicleType? vehicleType = VehicleInput.ReadOptionalVehicleType();

			IEnumerable<IVehicle> searchedProperties = _handler.SearchByProperties(color, numberOfWheels, vehicleType);

			if (searchedProperties.Count() == 0)
			{
				Console.WriteLine("\nNo parked vehicle matched your search\n");
				return;
			}

			Console.WriteLine($"\nFound {searchedProperties.Count()} vehicles matching your search!\n");

			foreach (IVehicle vehicle in searchedProperties)
			{
				Console.WriteLine(vehicle);
			}

		}

	}
}
