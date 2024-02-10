using AparmentSystemAPI.Apartment;
using AparmentSystemAPI.Models;
using AparmentSystemAPI.Repositories;

namespace AparmentSystemAPI.Flats
{
    public class FlatRepository(AppDbContext context) : BaseRepository<Flat>(context), IFlatRepository
    {
    }
}
