namespace MinimalAPI.Data;

public interface IHotelRepository : IDisposable
{
    Task<List<Hotel>> GetAllHotelsAsync();
    Task<List<Hotel>> GetHotelsAsync(string name);
    Task<List<Hotel>> GetHotelsAsync(Coordinate coordinate);
    Task<Hotel> GetHotelAsync(int hotelId);
    Task InsertHotelAsync(Hotel hotel);
    Task UpdateHotelAsync(Hotel hotel);
    Task DeleteHotelAsync(int hotelId);
    Task SaveAsync();
}
