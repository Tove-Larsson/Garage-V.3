using Garage_ovning5.Garages;
using Garage_ovning5.Vehicles;
using System;
using System.Collections.Generic;

namespace Garage_ovning5.UI
{
	internal class Menu
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

			Garage<IVehicle>? garage = null;

			while (garage is null)
			{
				Console.WriteLine("\nHow many parking spaces should the garage have?\n");

				if (int.TryParse(Console.ReadLine(), out int parkingSpaces) && parkingSpaces > 0 && parkingSpaces <= Garage<IVehicle>.MaximumCapacity)
				{
					Console.WriteLine($"\nA garage has been created with {parkingSpaces} parking spaces!\n");
					garage = new Garage<IVehicle>(parkingSpaces);
				}
				else
				{
					Console.WriteLine($"Invalid input, please enter a number between 1 and {garage?.Capacity}");
				}
			}
			GarageMenu(garage);
		}

		private void HandleCreatePreParkedGarage()
		{
			Console.WriteLine("Create a pre-parked garage chosen");
			Console.WriteLine("\nOne vehicle per vehicle type has been created, plus 5 extra parking spaces!\n");

			Garage<IVehicle> preParkedGarage = new Garage<IVehicle>(10);

			preParkedGarage.ParkVehicle(new Car("Abc123", VehicleColor.Red, 4, FuelType.Gasoline));
			preParkedGarage.ParkVehicle(new Motorcycle("def345", VehicleColor.Blue, 2, 1000));
			preParkedGarage.ParkVehicle(new Bus("Did865", VehicleColor.Yellow, 4, 30));
			preParkedGarage.ParkVehicle(new Airplane("bob261", VehicleColor.Green, 3, 2));
			preParkedGarage.ParkVehicle(new Boat("hej729", VehicleColor.Silver, 200.5));

			GarageMenu(preParkedGarage);

		}

		private void GarageMenu(Garage<IVehicle> garage)
		{
			bool running = true;

			while (running)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("\n========= Garage menu =========\n");
				Console.ResetColor();
				Console.WriteLine("Please select what you want to do!\n");

				Console.WriteLine($"The garage has {garage.FreeSpaces} free spaces, {garage.Capacity} in total\n");

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
						HandleParkVehicle(garage);
						break;
					case "2":
						HandleRetrieveVehicle(garage);
						break;
					case "3":
						HandleListParkedVehicles(garage);
						break;
					case "4":
						HandleListVehicleTypes(garage);
						break;
					case "5":
						HandleSearchByLicensePlate(garage);
						break;
					case "6":
						HandleSearchByProperties(garage);
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

		private void HandleParkVehicle(Garage<IVehicle> garage)
		{

			if (!garage.HasFreeParkingSpot())
			{
				Console.WriteLine("\nThe garage is full!\n");
				return;
			}

			string licensePlateNumber = VehicleInput.ReadLicensePlateNumber();

			if (garage.IsLicensePlateTaken(licensePlateNumber))
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

				bool parked = garage.ParkVehicle(vehicle);

				if (parked)
				{
					Console.WriteLine($"\nThe {type} with {vehicle.LicensePlateNumber} license plate number was parked successfully!\n");
				}
				else
				{
					Console.WriteLine("\nThe vehicle could not be parked\n");
				}

			}
			catch (ArgumentException ex)
			{
				Console.WriteLine(ex.Message);
				return;
			}
		
		}

		private void HandleRetrieveVehicle(Garage<IVehicle> garage)
		{

			string licensePlateNumber = InputHelper.ReadString("\nPlease enter the license plate number of the vehicle you want to retrieve\n");

			IVehicle? retrievedVehicle = garage.RemoveVehicle(licensePlateNumber);

			if (retrievedVehicle is not null)
			{
				Console.WriteLine($"\nRetrieval of vehicle with license plate number: {retrievedVehicle.LicensePlateNumber} was successful!\n");
			}
			else
			{
				Console.WriteLine($"\nNo vehicle with license plate {licensePlateNumber} was found.\n");
			}
		}

		private void HandleListParkedVehicles(Garage<IVehicle> garage)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("\n========= Parked vehicles =========\n");
			Console.ResetColor();

			garage.ListAllVehicles();
		}

		private void HandleListVehicleTypes(Garage<IVehicle> garage)
		{
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine("\n========= Parked vehicle types =========\n");
			Console.ResetColor();

			Console.WriteLine($"Cars: {garage.CountOfVehicleType(VehicleType.Car)}");
			Console.WriteLine($"Motorcycles: {garage.CountOfVehicleType(VehicleType.Motorcycle)}");
			Console.WriteLine($"Buses: {garage.CountOfVehicleType(VehicleType.Bus)}");
			Console.WriteLine($"Airplanes: {garage.CountOfVehicleType(VehicleType.Airplane)}");
			Console.WriteLine($"Boats: {garage.CountOfVehicleType(VehicleType.Boat)}\n");

		}

		private void HandleSearchByLicensePlate(Garage<IVehicle> garage)
		{
			string searchedLicensePlate = InputHelper.ReadString("Please enter a licence plate number to see if it is parked!");

			IVehicle? vehicle = garage.SearchByLicensePlate(searchedLicensePlate);

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

		private void HandleSearchByProperties(Garage<IVehicle> garage)
		{

			VehicleColor? color = VehicleInput.ReadOptionalVehicleColor();

			int? numberOfWheels = VehicleInput.ReadOptionalNumberOfWheels();

			VehicleType? vehicleType = VehicleInput.ReadOptionalVehicleType();

			List<IVehicle> searchedProperties = garage.SearchByProperties(color, numberOfWheels, vehicleType);

			if (searchedProperties.Count == 0)
			{
				Console.WriteLine("\nNo parked vehicle matched your search\n");
				return;
			}

			Console.WriteLine($"\nFound {searchedProperties.Count} vehicles matching your search!\n");

			foreach (Vehicle vehicle in searchedProperties)
			{
				Console.WriteLine(vehicle);
			}

		}

	}
}
