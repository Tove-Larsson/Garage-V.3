using Garage_ovning5.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Garage_ovning5.Garages
{
	internal class GarageHandler : IHandler
	{
		private Garage<IVehicle>? _garage;

		private Garage<IVehicle> CurrentGarage
		{
			get 
			{
				if (_garage is null)
				{
					throw new InvalidOperationException("No garage has been created");
				}

				return _garage; 
			}
			set { _garage = value; }
		}

		public int MaximumCapacity
		{
			get { return Garage<IVehicle>.MaximumCapacity; }
		}

		public int Capacity
		{
			get { return CurrentGarage.Capacity; }
		}

		public int FreeSpaces
		{
			get { return CurrentGarage.FreeSpaces; }
		}

		public void CreateEmptyGarage(int capacity)
		{
			CurrentGarage = new Garage<IVehicle>(capacity);
		}

		public void CreatePreParkedGarage()
		{
			CreateEmptyGarage(10);

			CurrentGarage.ParkVehicle(new Car("Abc123", VehicleColor.Red, 4, FuelType.Gasoline));
			CurrentGarage.ParkVehicle(new Motorcycle("def345", VehicleColor.Blue, 2, 1000));
			CurrentGarage.ParkVehicle(new Bus("Did865", VehicleColor.Yellow, 4, 30));
			CurrentGarage.ParkVehicle(new Airplane("bob261", VehicleColor.Green, 3, 2));
			CurrentGarage.ParkVehicle(new Boat("hej729", VehicleColor.Silver, 200.5));
		}

		public ParkingResult ParkVehicle(IVehicle vehicle)
		{
			return CurrentGarage.ParkVehicle(vehicle);
		}

		public IVehicle? RetrieveVehicle(string licensePlateNumber)
		{
			return CurrentGarage.RemoveVehicle(licensePlateNumber);
		}

		public IEnumerable<IVehicle> ListParkedVehicles()
		{
			return CurrentGarage.ToList();
		}

		public Dictionary<VehicleType, int> ListVehicleTypes()
		{
			var vehicleTypes = new Dictionary<VehicleType, int>();

			foreach (VehicleType vehicleType in Enum.GetValues<VehicleType>())
			{
				int count = CurrentGarage.CountOfVehicleType(vehicleType);
				vehicleTypes.Add(vehicleType, count);

			}
			return vehicleTypes;
		}

		public IVehicle? SearchByLicensePlate(string licensePlateNumber)
		{
			return CurrentGarage.SearchByLicensePlate(licensePlateNumber);
		}

		public IEnumerable<IVehicle> SearchByProperties(VehicleColor? color, int? numberOfWheels, VehicleType? vehicleType)
		{
			return CurrentGarage.SearchByProperties(color, numberOfWheels, vehicleType);
		}

	}
}
