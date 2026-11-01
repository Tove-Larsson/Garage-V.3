using Garage_ovning5.Garages;
using Garage_ovning5.UI;
using System;

namespace Garage_ovning5
{
	internal class Program
	{
		static void Main(string[] args)
		{
			GarageHandler handler = new GarageHandler();

			Menu menu = new Menu(handler);

			menu.MainMenu();
		}
	}
}
