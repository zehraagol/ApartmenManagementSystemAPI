using AparmentSystemAPI.Models.Flats.Interfaces;
using AparmentSystemAPI.Models.Flats;
using AparmentSystemAPI.Models.MainBuildings.Interfaces;

namespace AparmentSystemAPI.Models.MainBuildings
{
    public class MainBuildingRepository(AppDbContext context) : BaseRepository<MainBuilding>(context), IMainBuildingRepository
    {
    }
}
