using Microsoft.EntityFrameworkCore;

namespace DalbudakSigorta.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }
        public DbSet<Arac> Araclar => Set<Arac>();
        public DbSet<Musteri> Musteriler => Set<Musteri>();
        public DbSet<AracKayit> AracKayitlari => Set<AracKayit>();
        public DbSet<Kullanici> Kullanicilar => Set<Kullanici>();
        public DbSet<Police> Policeler => Set<Police>();
        public DbSet<OdemeBilgisi> OdemeBilgileri => Set<OdemeBilgisi>();

    }
    //code-first => entity, dbcontext => database (sqlite)
    //database-first => sql server
}