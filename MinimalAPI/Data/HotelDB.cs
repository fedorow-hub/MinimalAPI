namespace MinimalAPI.Data
{
    public class HotelDB : DbContext
    {
        public HotelDB(DbContextOptions<HotelDB> options) : base(options) { }
        public DbSet<Hotel> Hotels => Set<Hotel>();
    }
}
