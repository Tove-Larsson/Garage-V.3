using Garage_ovning5.Vehicles;
using System;

namespace Garage_ovning5.Garages
{
	internal class GarageHandler
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

		public void ParkVehicle(IVehicle vehicle)
		{
			throw new NotImplementedException();
		}

		public IVehicle? RetrieveVehicle(string licensePlateNumber)
		{
			throw new NotImplementedException();
		}

		public void ListParkedVehicles()
		{
			throw new NotImplementedException();
		}

		public void ListVehicleTypes()
		{
			throw new NotImplementedException();
		}

		public IVehicle? SearchByLicensePlate(string licensePlateNumber)
		{
			throw new NotImplementedException();
		}

		public void SearchByProperties(VehicleColor? color, int? numberOfWheels, VehicleType? vehicleType)
		{
			throw new NotImplementedException();
		}

	}
}
