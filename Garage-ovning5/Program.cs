using Garage_ovning5.Garages;
using Garage_ovning5.UI;

namespace Garage_ovning5
{
	internal class Program
	{
		static void Main(string[] args)
		{
			IHandler handler = new GarageHandler();

			IUI menu = new Menu(handler);

			menu.MainMenu();
		}
	}
}
