using System.Runtime.Serialization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

namespace DalbudakSigorta.Data
{
    public static class SeedData
    {
        public static void TestVerileriniDoldur(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<DataContext>();

            if(context != null)
            {
                if(context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

               
                if(!context.Araclar.Any())
                {
                    context.Araclar.AddRange(
                        new Arac{
                            PlakaIlKodu = "034",
                            PlakaKodu = "ABC101",
                            AracMarka = "Dacia",
                            AracModel = "Duster Essential Blue DCI 115",
                            AracModelYili = 2023,
                            MotorNo = "52WVC10338",
                            SasiNo ="HK6SA54AH9J", 
                            KaskoDegeri = 1100000
                        },

                        new Arac{
                            PlakaIlKodu = "034",
                            PlakaKodu = "ABC102",
                            AracMarka = "Mercedes",
                            AracModel = "A 200 FL AMG",
                            AracModelYili = 2022,
                            MotorNo = "52WVC10338",
                            SasiNo ="HK6SA54AH9J", 
                            KaskoDegeri = 1750000 
                        },

                        new Arac{
                            PlakaIlKodu = "034",
                            PlakaKodu = "ABC103",
                            AracMarka = "Renault",
                            AracModel = "Captur Icon 1.3 TCE EDC 130",
                            AracModelYili = 2020,
                            MotorNo = "52WVC10338",
                            SasiNo ="HK6SA54AH9J", 
                            KaskoDegeri = 1200000 
                        },

                        new Arac{
                            PlakaIlKodu = "034",
                            PlakaKodu = "ABC104",
                            AracMarka = "Opel",
                            AracModel = "Corsa 1.2 130 AT8 Ultimate",
                            AracModelYili = 2021,
                            MotorNo = "52WVC10338",
                            SasiNo ="HK6SA54AH9J", 
                            KaskoDegeri = 950000 
                        },

                        new Arac{
                            PlakaIlKodu = "034",
                            PlakaKodu = "ABC105",
                            AracMarka = "Toyota",
                            AracModel = "Corolla 1.8 Hybrid Dream E-CVT",
                            AracModelYili = 2021,
                            MotorNo = "52WVC10338",
                            SasiNo ="HK6SA54AH9J", 
                            KaskoDegeri = 1150000 
                        },

                        new Arac{
                            PlakaIlKodu = "034",
                            PlakaKodu = "ABC106",
                            AracMarka = "Mercedes",
                            AracModel = "A 180 d AMG 1.5 (116) 7G-DCT",
                            AracModelYili = 2019,
                            MotorNo = "52WVC10338",
                            SasiNo ="HK6SA54AH9J", 
                            KaskoDegeri = 1500000 
                        },

                        new Arac{
                            PlakaIlKodu = "034",
                            PlakaKodu = "ABC107",
                            AracMarka = "Toyota",
                            AracModel = "Corolla 1.8 Hybrid Dream E-CVT",
                            AracModelYili = 2020,
                            MotorNo = "52WVC10338",
                            SasiNo ="HK6SA54AH9J", 
                            KaskoDegeri = 1090000 
                        },

                        new Arac{
                            PlakaIlKodu = "034",
                            PlakaKodu = "ABC108",
                            AracMarka = "Toyota",
                            AracModel = "Corolla 1.8 Hybrid Dream E-CVT",
                            AracModelYili = 2022,
                            MotorNo = "52WVC10338",
                            SasiNo ="HK6SA54AH9J", 
                            KaskoDegeri = 1250000 
                        },

                        new Arac{
                            PlakaIlKodu = "034",
                            PlakaKodu = "ABC109",
                            AracMarka = "Dacia",
                            AracModel = "Duster Essential Blue DCI 115",
                            AracModelYili = 2022,
                            MotorNo = "52WVC10338",
                            SasiNo ="HK6SA54AH9J", 
                            KaskoDegeri = 970000
                        }



                    );
                    context.SaveChanges();
                }
            }
        }
    }
}