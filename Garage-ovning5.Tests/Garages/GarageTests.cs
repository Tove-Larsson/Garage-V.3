using Garage_ovning5.Garages;
using Garage_ovning5.Vehicles;

namespace Garage_ovning5.Tests.Garages
{
	public class GarageTests
	{
		[Fact]
		public void ParkVehicle_WhenGarageHasFreeSpace_ReturnsSuccess()
		{
			Garage<IVehicle> garage = new Garage<IVehicle>(5);
			IVehicle car = new Car("ABC123", VehicleColor.Red, 4, FuelType.Diesel);

			ParkingResult result = garage.ParkVehicle(car);

			Assert.Equal(ParkingResult.Success, result);
		}

		[Fact]
		public void ParkVehicle_WhenSuccessful_DecreasesFreeSpacesByOne()
		{
			int capacity = 5;
			Garage<IVehicle> garage = new Garage<IVehicle>(capacity);
			IVehicle car = new Car("ABC123", VehicleColor.Red, 4, FuelType.Diesel);

			garage.ParkVehicle(car);

			Assert.Equal(capacity - 1, garage.FreeSpaces);
		}

		[Fact]
		public void ParkVehicle_WhenGarageIsFull_ReturnsFull()
		{
			int capacity = 1;
			Garage<IVehicle> garage = new Garage<IVehicle>(capacity);
			IVehicle car = new Car("ABC123", VehicleColor.Red, 4, FuelType.Diesel);
			IVehicle boat = new Boat("ABC12D", VehicleColor.Blue, 1000);

			garage.ParkVehicle(car);
			ParkingResult result = garage.ParkVehicle(boat);

			Assert.Equal(ParkingResult.Full, result);
		}

		[Fact]
		public void ParkVehicle_WhenLicensePlateAlreadyParked_ReturnsLicensePlateTaken()
		{
			int capacity = 2;
			Garage<IVehicle> garage = new Garage<IVehicle>(capacity);
			IVehicle car = new Car("ABC123", VehicleColor.Red, 4, FuelType.Diesel);
			IVehicle car2 = new Car("ABC123", VehicleColor.Red, 4, FuelType.Diesel);

			garage.ParkVehicle(car);
			ParkingResult result = garage.ParkVehicle(car2);

			Assert.Equal(ParkingResult.LicensePlateTaken, result);
		}


		[Fact]
		public void ParkVehicle_WhenGarageIsFullAndPlateIsTaken_ReturnsFull()
		{
			int capacity = 1;
			Garage<IVehicle> garage = new Garage<IVehicle>(capacity);
			IVehicle car = new Car("ABC123", VehicleColor.Red, 4, FuelType.Diesel);
			IVehicle car2 = new Car("ABC123", VehicleColor.Red, 4, FuelType.Diesel);

			garage.ParkVehicle(car);
			ParkingResult result = garage.ParkVehicle(car2);

			Assert.Equal(ParkingResult.Full, result);
		}

		[Fact]
		public void ParkVehicle_WhenRejectedForTakenPlate_DoesNotChangeFreeSpaces()
		{
			Garage<IVehicle> garage = new Garage<IVehicle>(2);
			IVehicle car = new Car("ABC123", VehicleColor.Red, 4, FuelType.Diesel);
			IVehicle car2 = new Car("ABC123", VehicleColor.Blue, 4, FuelType.Diesel);

			garage.ParkVehicle(car);
			int freeSpacesBefore = garage.FreeSpaces;

			garage.ParkVehicle(car2);

			Assert.Equal(freeSpacesBefore, garage.FreeSpaces);
		}

	}
}
