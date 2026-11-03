

namespace Garage_ovning5.Garages.Persistence
{
	internal interface IGarageFileHandler
	{
		void SaveFile(IHandler garageHandler);
		GarageSaveData? LoadFile();

	}
}
