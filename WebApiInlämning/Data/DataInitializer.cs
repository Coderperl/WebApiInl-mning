using Microsoft.EntityFrameworkCore;

namespace WebApiInlämning.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _context;

        public DataInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            _context.Database.Migrate();
            SeedAdds();
        }

        public void SeedAdds()
        {
            if (!_context.Advertisements.Any(a => a.Name == "Jonas banantruck"))
            {
                _context.Advertisements.Add(new Advertisement()
                {
                    Name = "Jonas banantruck", 
                    Description = "Världens bästa bananer finns här!!"
                });
            }

            if (!_context.Advertisements.Any(a => a.Name == "Kristans Golfbanan"))
            {
                _context.Advertisements.Add(new Advertisement
                {
                    Name = "Kristans Golfbanan", 
                    Description = "Världens bästa golf bananer finns här!!"
                });
            }

            if (!_context.Advertisements.Any(a => a.Name == "Tommy's Hårdkurs"))
            {
                _context.Advertisements.Add(new Advertisement
                {
                    Name = "Tommy's Hårdkurs", 
                    Description = "Världskända Tommy Hårding besöker Cirkus 20:e Maj!"
                });
            }

            if (!_context.Advertisements.Any(a => a.Name == "DjungelJonny på tur."))
            {
                _context.Advertisements.Add(new Advertisement
                {
                    Name = "DjungelJonny på tur.",
                    Description = "N1, DjungelJonny Håller föredrag om modulära kärnreaktorer."
                });
            }

            if (!_context.Advertisements.Any(a => a.Name == "Måns mörker"))
            {
                _context.Advertisements.Add(new Advertisement
                {
                    Name = "Måns mörker",
                    Description = "Måns prommenerar planlöst i en mörk skog."
                });
            }

            if (!_context.Advertisements.Any(a => a.Name == "Stefans Hockey föreläsning"))
            {
                _context.Advertisements.Add(new Advertisement
                {
                    Name = "Stefans Hockey föreläsning",
                    Description = "Stefans världsrenommerade hockeykompetens sätts på prov."
                });
            }

            _context.SaveChanges();
        }
    }
}