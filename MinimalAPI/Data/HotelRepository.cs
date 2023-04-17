namespace MinimalAPI.Data;

public class HotelRepository : IHotelRepository
{
    private readonly HotelDB _context;

    public HotelRepository(HotelDB context)
    {
        _context = context;
    }

    public Task<List<Hotel>> GetAllHotelsAsync() => _context.Hotels.ToListAsync();

    public Task<List<Hotel>> GetHotelsAsync(string name) =>
        _context.Hotels.Where(h => h.Name.Contains(name)).ToListAsync();

    public async Task<List<Hotel>> GetHotelsAsync(Coordinate coordinate) =>
        await _context.Hotels.Where(hotel =>
            hotel.Latitude > coordinate.Latitude - 1 &&
            hotel.Latitude < coordinate.Latitude + 1 &&
            hotel.Longitude > coordinate.Longitude - 1 &&
            hotel.Longitude > coordinate.Longitude + 1)
        .ToListAsync();

    public async Task<Hotel> GetHotelAsync(int hotelId)
    {
        var hotelFromDb = await _context.Hotels.FindAsync(new object[] { hotelId });
        if (hotelFromDb == null)
        {
            throw new Exception("Not found");
        }
        return hotelFromDb;
    }

    async public Task InsertHotelAsync(Hotel hotel) => await _context.Hotels.AddAsync(hotel);

    public async Task UpdateHotelAsync(Hotel hotel)
    {
        var hotelFromDb = await _context.Hotels.FindAsync(new object[] { hotel.Id });
        if (hotelFromDb == null) return;
        hotelFromDb.Name = hotel.Name;
        hotelFromDb.Longitude = hotel.Longitude;
        hotelFromDb.Latitude = hotel.Latitude;
    }

    public async Task DeleteHotelAsync(int hotelId)
    {
        var hotelFromDb = await _context.Hotels.FindAsync(new object[] { hotelId });
        if (hotelFromDb == null) return;
        _context.Remove(hotelFromDb);
    }

    public async Task SaveAsync() => await _context.SaveChangesAsync();

    private bool _disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
