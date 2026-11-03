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

		[Theory]
		[InlineData("ABC123")]
		[InlineData("abc123")]
		[InlineData("AbC123")]
		public void SearchByLicensePlate_WithDifferentCasing_ReturnsVehicle(string input)
		{
			Garage<IVehicle> garage = new Garage<IVehicle>(2);
			IVehicle car = new Car("ABC123", VehicleColor.Red, 4, FuelType.Diesel);
			garage.ParkVehicle(car);

			IVehicle? foundVehicle = garage.SearchByLicensePlate(input);

			Assert.Same(car, foundVehicle);
		}

		[Fact]
		public void SearchByLicensePlate_WhenNotParked_ReturnsNull()
		{
			Garage<IVehicle> garage = new Garage<IVehicle>(2);

			var vehicle = garage.SearchByLicensePlate("abc123");

			Assert.Null(vehicle);
		}

		[Fact]
		public void RemoveVehicle_WhenParked_IncreasesFreeSpacesByOne()
		{
			Garage<IVehicle> garage = new Garage<IVehicle>(5);
			IVehicle car = new Car("ABC123", VehicleColor.Red, 4, FuelType.Diesel);
			garage.ParkVehicle(car);
			int spacesBefore = garage.FreeSpaces;

			garage.RemoveVehicle("ABC123");

			int spacesAfter = garage.FreeSpaces;

			Assert.Equal(spacesBefore + 1, spacesAfter);
		}

		[Fact]
		public void RemoveVehicle_WhenParked_ReturnsVehicle()
		{
			Garage<IVehicle> garage = new Garage<IVehicle>(5);
			IVehicle car = new Car("ABC123", VehicleColor.Red, 4, FuelType.Diesel);
			garage.ParkVehicle(car);

			var vehicle = garage.RemoveVehicle(car.LicensePlateNumber);

			Assert.Same(car, vehicle);
		}

		[Fact]
		public void RemoveVehicle_WhenParked_VehicleCanNoLongerBeFound()
		{
			Garage<IVehicle> garage = new Garage<IVehicle>(5);
			IVehicle car = new Car("ABC123", VehicleColor.Red, 4, FuelType.Diesel);
			garage.ParkVehicle(car);

			garage.RemoveVehicle(car.LicensePlateNumber);

			var vehicle = garage.SearchByLicensePlate(car.LicensePlateNumber);

			Assert.Null(vehicle);
		}

		[Fact]
		public void RemoveVehicle_WhenNotParked_ReturnsNull()
		{
			Garage<IVehicle> garage = new Garage<IVehicle>(5);
			IVehicle car = new Car("ABC123", VehicleColor.Red, 4, FuelType.Diesel);
			garage.ParkVehicle(car);

			var removedVehicle = garage.RemoveVehicle("ABC12D");

			Assert.Null(removedVehicle);
		}

		[Fact]
		public void SearchByProperties_ByColorOnly_ReturnsAllVehiclesOfThatColor()
		{
			Garage<IVehicle> garage = CreateGarageWithMixedVehicles();

			List<IVehicle> result = garage.SearchByProperties(VehicleColor.Red, null, null);

			Assert.Equal(2, result.Count);
			Assert.All(result, vehicle => Assert.Equal(VehicleColor.Red, vehicle.Color));
		}


		[Fact]
		public void SearchByProperties_ByColorAndWheels_ReturnsOnlyVehiclesMatchingBoth()
		{
			Garage<IVehicle> garage = CreateGarageWithMixedVehicles();

			List<IVehicle> result = garage.SearchByProperties(VehicleColor.Red, 4, null);

			IVehicle onlyMatch = Assert.Single(result);
			Assert.Equal("ABC123", onlyMatch.LicensePlateNumber);
		}

		[Fact]
		public void SearchByProperties_WithNoCriteria_ReturnsAllVehicles()
		{
			Garage<IVehicle> garage = CreateGarageWithMixedVehicles();

			List<IVehicle> result = garage.SearchByProperties(null, null, null);

			Assert.Equal(3, result.Count);
		}

		[Fact]
		public void SearchByProperties_WhenNothingMatches_ReturnsEmptyList()
		{
			Garage<IVehicle> garage = CreateGarageWithMixedVehicles();

			List<IVehicle> result = garage.SearchByProperties(VehicleColor.Green, null, null);

			Assert.Empty(result);
		}

		[Theory]
		[InlineData("Car", 2)]
		[InlineData("Motorcycle", 1)]
		[InlineData("Boat", 0)]
		public void CountOfVehicleType_WithMixedVehicles_ReturnsCountOfThatType(string typeName, int expected)
		{
			Garage<IVehicle> garage = CreateGarageWithMixedVehicles();
			VehicleType type = Enum.Parse<VehicleType>(typeName);

			int count = garage.CountOfVehicleType(type);

			Assert.Equal(expected, count);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		[InlineData(2001)]
		public void Constructor_WithInvalidCapacity_Throws(int capacity)
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => new Garage<IVehicle>(capacity));
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2000)]
		public void Constructor_WithBoundaryCapacity_SetsCapacity(int capacity)
		{
			Garage<IVehicle> garage = new Garage<IVehicle>(capacity);

			Assert.Equal(capacity, garage.Capacity);
		}

		[Fact]
		public void Enumeration_WithParkedVehicles_YieldsAllVehicles()
		{
			Garage<IVehicle> garage = new Garage<IVehicle>(5);
			IVehicle car = new Car("ABC123", VehicleColor.Red, 4, FuelType.Diesel);
			IVehicle boat = new Boat("DEF456", VehicleColor.Blue, 1000);
			garage.ParkVehicle(car);
			garage.ParkVehicle(boat);

			List<IVehicle> enumerated = new List<IVehicle>();
			foreach (IVehicle vehicle in garage)
			{
				enumerated.Add(vehicle);
			}

			Assert.Equal(2, enumerated.Count);
			Assert.Contains(car, enumerated);
			Assert.Contains(boat, enumerated);
		}

		[Theory]
		[InlineData("ABC123")]
		[InlineData("abc123")]
		[InlineData("AbC123")]
		public void IsLicensePlateTaken_WithDifferentCasing_ReturnsTrue(string input)
		{
			Garage<IVehicle> garage = new Garage<IVehicle>(2);
			garage.ParkVehicle(new Car("ABC123", VehicleColor.Red, 4, FuelType.Diesel));

			bool result = garage.IsLicensePlateTaken(input);

			Assert.True(result);
		}


		private static Garage<IVehicle> CreateGarageWithMixedVehicles()
		{
			Garage<IVehicle> garage = new Garage<IVehicle>(10);

			garage.ParkVehicle(new Car("ABC123", VehicleColor.Red, 4, FuelType.Diesel));
			garage.ParkVehicle(new Car("DEF456", VehicleColor.Blue, 4, FuelType.Gasoline));
			garage.ParkVehicle(new Motorcycle("GHI789", VehicleColor.Red, 2, 1000));

			return garage;
		}
	}
}
