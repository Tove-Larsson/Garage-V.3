using Garage_ovning5.Vehicles;
using System.Collections.Generic;

namespace Garage_ovning5.Garages
{
	internal interface IHandler
	{

		public int MaximumCapacity { get; }

		public int Capacity { get; }

		public int FreeSpaces { get; }

		public void CreateEmptyGarage(int capacity);

		public void CreatePreParkedGarage();

		public ParkingResult ParkVehicle(IVehicle vehicle);

		public IVehicle? RetrieveVehicle(string licensePlateNumber);

		public IEnumerable<IVehicle> ListParkedVehicles();

		public Dictionary<VehicleType, int> ListVehicleTypes();

		public IVehicle? SearchByLicensePlate(string licensePlateNumber);

		public IEnumerable<IVehicle> SearchByProperties(VehicleColor? color, int? numberOfWheels, VehicleType? vehicleType);

		public bool IsLicensePlateTaken(string licensePlateNumber);

	}
}
