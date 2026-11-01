using Garage_ovning5.Vehicles;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Garage_ovning5.Garages
{
	internal class Garage<T> : IEnumerable<T> where T : class, IVehicle
	{

		public const int MaximumCapacity = 2000;
		private readonly int _capacity;
		private readonly List<T> _vehicles;

		public Garage(int capacity)
		{
			if (capacity < 1 || capacity > MaximumCapacity)
			{
				throw new ArgumentOutOfRangeException(nameof(capacity), $"Capacity must be between 1 and {MaximumCapacity}");
			}

			_capacity = capacity;
			_vehicles = new List<T>(capacity);
		}

		public int Capacity
		{
			get { return _capacity; }
		}

		public int FreeSpaces
		{
			get { return _capacity - _vehicles.Count; }
		}


		public ParkingResult ParkVehicle(T vehicle)
		{
			if (!HasFreeParkingSpot())
			{
				return ParkingResult.Full;
			}

			if (IsLicensePlateTaken(vehicle.LicensePlateNumber))
			{
				return ParkingResult.LicensePlateTaken;
			}

			_vehicles.Add(vehicle);
			return ParkingResult.Success;
		}

		public bool HasFreeParkingSpot()
		{
			if (_vehicles.Count < _capacity)
			{
				return true;
			}

			return false;
		}

		public bool IsLicensePlateTaken(string licensePlateNumber)
		{
			string searchedPlate = Vehicle.NormalizeLicensePlate(licensePlateNumber);

			foreach (T vehicle in _vehicles)
			{
				if (vehicle.LicensePlateNumber == searchedPlate)
				{
					return true;
				}
			}

			return false;
		}

		public T? RemoveVehicle(string licensePlateNumber)
		{
			T? vehicle = SearchByLicensePlate(licensePlateNumber);

			if (vehicle is not null)
			{
				_vehicles.Remove(vehicle);
			}

			return vehicle;
		}

		public void ListAllVehicles()
		{
			if (_vehicles.Count == 0)
			{
				Console.WriteLine("There are no vehicles parked in the garage.\n");
				return;
			}

			foreach (T vehicle in _vehicles)
			{
				Console.WriteLine($"{vehicle}\n");
			}
		}

		public int CountOfVehicleType(VehicleType type)
		{
			int count = 0;

			foreach (IVehicle vehicle in _vehicles)
			{
				if (vehicle.VehicleType == type)
				{
					count++;
				}
			}

			return count;
		}

		public T? SearchByLicensePlate(string licensePlateNumber)
		{
			string searchedPlate = Vehicle.NormalizeLicensePlate(licensePlateNumber);

			for (int i = 0; i < _vehicles.Count; i++)
			{
				T vehicle = _vehicles[i];

				if (vehicle.LicensePlateNumber == searchedPlate)
				{
					return vehicle;
				}

			}

			return null;
		}

		private static bool MatchesSearch(IVehicle vehicle, VehicleColor? color, int? wheels, VehicleType? type)
		{
			if (color is not null && vehicle.Color != color)
			{
				return false;
			}

			if (wheels is not null && vehicle.NumberOfWheels != wheels)
			{
				return false;
			}

			if (type is not null && vehicle.VehicleType != type)
			{
				return false;
			}

			return true;

		}

		public List<T> SearchByProperties(VehicleColor? color, int? numberOfWheels, VehicleType? vehicleType)
		{
			List<T> matches = new List<T>();

			foreach (T vehicle in _vehicles)
			{
				if (MatchesSearch(vehicle, color, numberOfWheels, vehicleType))
				{
					matches.Add(vehicle);
				}
			}

			return matches;
		}


		public IEnumerator<T> GetEnumerator()
		{
			return _vehicles.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

	}
}
