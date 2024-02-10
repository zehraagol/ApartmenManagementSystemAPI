using AparmentSystemAPI.Models.Flats.Interfaces;

namespace AparmentSystemAPI.Models.Flats
{
    public class FlatRepository(AppDbContext context) : BaseRepository<Flat>(context), IFlatRepository
    {
    }

}
