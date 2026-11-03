

using System.Text.Json.Serialization;

namespace Garage_ovning5.Vehicles
{
	[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
	[JsonDerivedType(typeof(Car), typeDiscriminator: "car")]
	[JsonDerivedType(typeof(Motorcycle), typeDiscriminator: "motorcycle")]
	[JsonDerivedType(typeof(Bus), typeDiscriminator: "bus")]
	[JsonDerivedType(typeof(Airplane), typeDiscriminator: "airplane")]
	[JsonDerivedType(typeof(Boat), typeDiscriminator: "boat")]
	internal interface IVehicle
	{
		string LicensePlateNumber { get; }

		VehicleColor Color { get; }

		int NumberOfWheels { get; }

		VehicleType VehicleType { get; }
	}
}
