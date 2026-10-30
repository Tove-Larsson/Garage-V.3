

namespace Garage_ovning5.Vehicles
{
	internal interface IVehicle
	{
		string LicensePlateNumber { get; }

		VehicleColor Color { get; }

		int NumberOfWheels { get; }

		VehicleType VehicleType { get; }
	}
}
