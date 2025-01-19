using CMS.Application.Configurations;
using Microsoft.Extensions.Configuration;

namespace CMS.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        private static readonly List<Employee> _employees = [];
        private static readonly List<Cafe> _cafes = [];

        public static void InitializeSeedData(ApplicationConfiguration appConfig)
        {
            if (_employees.Count == 0)
            {
                _employees.AddRange([
                   Employee.Create(
                "Oliver",
                EmailAddress.Of("Oliver.adams@example.com"),
                PhoneNumber.Of("89898889"),
                Gender.Female,
                null),
             Employee.Create(
                "Benjamin",
                EmailAddress.Of("benjamin@example.com"),
                PhoneNumber.Of("84444444"),
                Gender.Male,
                null),
             Employee.Create(
                "Maximus",
                EmailAddress.Of("maximus@example.com"),
                PhoneNumber.Of("85555555"),
                Gender.Female,
                null),
             Employee.Create(
                "Adeline",
                EmailAddress.Of("adeline@example.com"),
                PhoneNumber.Of("95858555"),
                Gender.Female,
                null),
            ]);

                var imageFilesDomain = appConfig.UploadedImageHostPath;


                _cafes.AddRange([
                     Cafe.Create(CafeId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")),
                "Brewtopia",
                "A cozy and artistic cafe with an eclectic mix of coffee blends and homemade pastries. Perfect for artists and creatives looking to relax and be inspired.",
                "CBD",
                $"{imageFilesDomain}/brewtopia.jpg"),

            Cafe.Create(CafeId.Of(new Guid("BA73345F-B787-42F8-97ED-0FC96814A4BC")),
                "DailyGrind",
                "A modern cafe offering high-quality specialty coffees and quick bites in a sleek, urban environment. Perfect for busy professionals looking to refuel during their day.",
                "South",
                 $"{imageFilesDomain}/dailygrind.jpg"),

            Cafe.Create(CafeId.Of(new Guid("422BB298-3A5C-42C0-BD3D-FC5BCE33A32A")),
                "LeafBean",
                "A peaceful retreat with an emphasis on organic coffee, herbal teas, and a plant-filled atmosphere. A perfect space for anyone seeking serenity and a calm environment.",
                "East Coast",
                 $"{imageFilesDomain}/leafandbean.jpg"),
                ]);

                Random random = new Random();

                _cafes[0].AddEmployee(_employees[0], DateTime.UtcNow.AddDays(-random.Next(0,4)));
                _cafes[0].AddEmployee(_employees[1], DateTime.UtcNow.AddDays(-random.Next(0, 4)));
                _cafes[1].AddEmployee(_employees[2], DateTime.UtcNow.AddDays(-random.Next(0, 4)));


            }

        }


        public static IEnumerable<Employee> Employees
        {
            get
            {
               return _employees;
            }
        }

        public static IEnumerable<Cafe> Cafes {
            get
            {

            return _cafes; 
            
            
            }


        
        }
          

      


        /**
         
2. Name: The Daily Grind
Description: A modern cafe offering high-quality specialty coffees and quick bites in a sleek, urban environment. Perfect for busy professionals looking to refuel during their day.
Location:

Latitude: 40.730610° N
Longitude: 73.935242° W
Address: 456 Main St
City: New York
Country: USA
Image File Path: /images/dailygrind.jpg


3. Name: The Leaf & Bean
Description: A peaceful retreat with an emphasis on organic coffee, herbal teas, and a plant-filled atmosphere. A perfect space for anyone seeking serenity and a calm environment.
Location:

Latitude: 51.5074° N
Longitude: 0.1278° W
Address: 22 Green Street
City: London
Country: United Kingdom
Image File Path: /images/leafandbean.jpg
4. Name: Urban Perks
Description: A trendy cafe located in the heart of the city, offering specialty lattes, pastries, and artisanal sandwiches. The lively atmosphere is perfect for people-watching or a quick meet-up with friends.
Location:

Latitude: 48.8566° N
Longitude: 2.3522° E
Address: 78 Rue de Paris
City: Paris
Country: France
Image File Path: /images/urbanperks.jpg
5. Name: The Cozy Corner
Description: A welcoming and intimate neighborhood cafe offering everything from specialty coffees to homemade cookies. It’s the go-to spot for a quiet afternoon or a casual meetup.
Location:

Latitude: 34.0522° N
Longitude: 118.2437° W
Address: 789 Oak Avenue
City: Los Angeles
Country: USA
Image File Path: /images/cozycorner.jpg
6. Name: Perk & Brew
Description: A sophisticated cafe known for its expertly crafted espresso drinks, gourmet pastries, and beautiful views. The modern interior and friendly baristas create a relaxed atmosphere.
Location:

Latitude: 41.9028° N
Longitude: 12.4964° E
Address: 100 Via della Sapienza
City: Rome
Country: Italy
Image File Path: /images/perkandbrew.jpg
7. Name: Java & Joy
Description: A vibrant, colorful cafe that’s all about fun and great coffee. Known for its creative coffee art, friendly baristas, and lively atmosphere.
Location:

Latitude: 34.0522° N
Longitude: 118.2437° W
Address: 1200 Sunset Blvd
City: Los Angeles
Country: USA
Image File Path: /images/javaandjoy.jpg
8. Name: The Roasted Bean
Description: A traditional yet modern cafe that specializes in hand-roasted coffee and artisan pastries. Known for its cozy vibe and excellent customer service.
Location:

Latitude: 39.9042° N
Longitude: 116.4074° E
Address: 45 Chaoyang Road
City: Beijing
Country: China
Image File Path: /images/roastedbean.jpg
9. Name: Moonlit Brew
Description: A charming, intimate cafe offering rich coffees, decadent desserts, and a relaxing atmosphere. Known for its late-night hours and perfect ambiance for working or socializing.
Location:

Latitude: 37.7749° N
Longitude: 122.4194° W
Address: 333 Market St
City: San Francisco
Country: USA
Image File Path: /images/moonlitbrew.jpg
10. Name: The Green Cup
Description: A health-conscious cafe specializing in organic coffee, plant-based snacks, and eco-friendly practices. Its minimalist design and green focus make it a great place for eco-conscious individuals.
Location:

Latitude: 55.7558° N
Longitude: 37.6173° E
Address: 10 Tverskaya Street
City: Moscow
Country: Russia
Image File Path: /images/greencup.jpg
         * */
    }
}
