using Microsoft.EntityFrameworkCore;
using ChessGame.Models;
using System.Data.Entity;

namespace ChessGame.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                context.Database.EnsureCreated();
                if (context.maps.Any())
                {
                    
                }
                else
                {
                    for(int Id = 1; Id <= 12; Id++)
                    {
                        var Map = new MapEntity[]
                {
                    new MapEntity{Idmaps = Id,X = 1,Y =1, Value = 5 },
                    new MapEntity{Idmaps = Id,X = 1,Y =2, Value = 2 },
                    new MapEntity{Idmaps = Id,X = 1,Y =3, Value = 3 },
                    new MapEntity{Idmaps = Id,X = 1,Y =4, Value = 9 },
                    new MapEntity{Idmaps = Id,X = 1,Y =5, Value = 10 },
                    new MapEntity{Idmaps = Id,X = 1,Y =6, Value = 3 },
                    new MapEntity{Idmaps = Id,X = 1,Y =7, Value = 2 },
                    new MapEntity{Idmaps = Id,X = 1,Y =8, Value = 5 },
                    new MapEntity{Idmaps = Id,X = 1,Y =9, Value = 1 },
                    new MapEntity{Idmaps = Id,X = 2,Y =9, Value = 1 },
                    new MapEntity{Idmaps = Id,X = 3,Y =9, Value = 1000 },
                    new MapEntity{Idmaps = Id,X = 4,Y =9, Value = 1000 },

                    new MapEntity{Idmaps = Id,X = 8,Y =1, Value = 15 },
                    new MapEntity{Idmaps = Id,X = 8,Y =2, Value = 12 },
                    new MapEntity{Idmaps = Id,X = 8,Y =3, Value = 13 },
                    new MapEntity{Idmaps = Id,X = 8,Y =4, Value = 19 },
                    new MapEntity{Idmaps = Id,X = 8,Y =5, Value = 20 },
                    new MapEntity{Idmaps = Id,X = 8,Y =6, Value = 13 },
                    new MapEntity{Idmaps = Id,X = 8,Y =7, Value = 12},
                    new MapEntity{Idmaps = Id,X = 8,Y =8, Value = 15 },

                    new MapEntity{Idmaps = Id,X = 2,Y =1, Value = 1 },
                    new MapEntity{Idmaps = Id,X = 2,Y =2, Value = 1 },
                    new MapEntity{Idmaps = Id,X = 2,Y =3, Value = 1 },
                    new MapEntity{Idmaps = Id,X = 2,Y =4, Value = 1 },
                    new MapEntity{Idmaps = Id,X = 2,Y =5, Value = 1 },
                    new MapEntity{Idmaps = Id,X = 2,Y =6, Value = 1 },
                    new MapEntity{Idmaps = Id,X = 2,Y =7, Value = 1 },
                    new MapEntity{Idmaps = Id,X = 2,Y =8, Value = 1 },

                    new MapEntity{Idmaps = Id,X = 7,Y =1, Value = 11 },
                    new MapEntity{Idmaps = Id,X = 7,Y =2, Value = 11 },
                    new MapEntity{Idmaps = Id,X = 7,Y =3, Value = 11 },
                    new MapEntity{Idmaps = Id,X = 7,Y =4, Value = 11 },
                    new MapEntity{Idmaps = Id,X = 7,Y =5, Value = 11 },
                    new MapEntity{Idmaps = Id,X = 7,Y =6, Value = 11 },
                    new MapEntity{Idmaps = Id,X = 7,Y =7, Value = 11 },
                    new MapEntity{Idmaps = Id,X = 7,Y =8, Value = 11 },

                    new MapEntity{Idmaps = Id,X = 3,Y =1, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 3,Y =2, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 3,Y =3, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 3,Y =4, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 3,Y =5, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 3,Y =6, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 3,Y =7, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 3,Y =8, Value = 0 },

                    new MapEntity{Idmaps = Id,X = 4,Y =1, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 4,Y =2, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 4,Y =3, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 4,Y =4, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 4,Y =5, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 4,Y =6, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 4,Y =7, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 4,Y =8, Value = 0 },

                    new MapEntity{Idmaps = Id,X = 5,Y =1, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 5,Y =2, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 5,Y =3, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 5,Y =4, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 5,Y =5, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 5,Y =6, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 5,Y =7, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 5,Y =8, Value = 0 },

                    new MapEntity{Idmaps = Id,X = 6,Y =1, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 6,Y =2, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 6,Y =3, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 6,Y =4, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 6,Y =5, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 6,Y =6, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 6,Y =7, Value = 0 },
                    new MapEntity{Idmaps = Id,X = 6,Y =8, Value = 0 },
            };
                        foreach (var m in Map)
                        {
                            context.maps.Add(m);
                        }
                    }
                }
                
                
                context.SaveChanges();
            }
        }
    }
}
