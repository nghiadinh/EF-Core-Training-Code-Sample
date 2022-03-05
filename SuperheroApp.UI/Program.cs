using SuperheroApp.Domain;
using SuperheroApp.Data;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SuperheroApp.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("********************");
            Console.WriteLine("Hello Superhero!");
            Console.WriteLine("********************");

            //CreateAvengerTeam();
            //GetAllTeams();
            //InsertIronManToAvenger();
            //UpdateIronMan();
            // DeleteIronMan();
            //InsertBatManAndJusticeLeague();
            //UpdateBatmanBirthdate();
            //InsertWonderWomanToJusticeLeague();
            //CreateBattle();
            //GetWonderWomentEquipments();
            //GetHeroNameAndRealNameInJL();
            ExecuteRawSQL();
        }

        private static void CreateAvengerTeam()
        {
            var avengerTeam = new Team
            {
                Name = "Avenger",
            };
            using (var context = new SuperheroContext())
            {
                context.Teams.Add(avengerTeam);
                context.SaveChanges();
            }
        }

        private static void GetAllTeams()
        {
            using (var context = new SuperheroContext())
            {
                var teams = context.Teams.ToList();

                Console.WriteLine("Hero Teams:");
                foreach (var team in teams)
                {
                    Console.WriteLine(team.Name);
                }
            }
        }

        private static void InsertIronManToAvenger()
        {
            var ironMan = new Hero
            {
                Name = "Iron Man",
                RealName = "Tony",
                Race = HeroRace.Human,
            };
            using (var context = new SuperheroContext())
            {
                var avengerTeam = context.Teams.TagWith("Get Avenger").Where(team => team.Name == "Avenger").FirstOrDefault();

                if (avengerTeam == null)
                {
                    Console.WriteLine("Avenger team not found");
                    return;
                }

                avengerTeam.Heroes.Add(ironMan);
                context.SaveChanges();
            }
        }

        private static void UpdateIronMan()
        {
            using (var context = new SuperheroContext())
            {
                //LINQ query Syntax
                var ironMan = (from hero in context.Heroes
                               where hero.Name == "Iron Man"
                               select hero)
                              .FirstOrDefault();

                if (ironMan == null)
                {
                    Console.WriteLine("Iron Man not found");
                    return;
                }

                ironMan.DateOfBirth = new DateTime(1989, 1, 1);
                ironMan.RealName = "Tony Stark";
                ironMan.Equipments.Add(new Equipment { 
                    Name = "Super Armor",
                    Type = EquipmentType.Outwear
                });

                context.SaveChanges();
            }
        }

        private static void DeleteIronMan()
        {
            using (var context = new SuperheroContext())
            {
                var ironMan = context.Heroes.Where(hero => hero.Name == "Iron Man").FirstOrDefault();

                if (ironMan == null)
                {
                    Console.WriteLine("Iron Man not found");
                    return;
                }

                context.Remove(ironMan);
                context.SaveChanges();
            }
        }

        private static void InsertBatManAndJusticeLeague()
        {
            var batman = new Hero
            {
                Name = "Batman",
                RealName = "Bruce Wayne",
                Race = HeroRace.Human,
            };
            batman.Equipments.Add(new Equipment { Name = "Bat Suite", Type = EquipmentType.Outwear });
            batman.Team = new Team
            {
                Name = "Justice League"
            };


            using (var context = new SuperheroContext())
            {
                context.Heroes.Add(batman);
                context.SaveChanges();
            }
        }

        private static void UpdateBatmanBirthdate()
        {
            Hero batman;
            using (var context = new SuperheroContext())
            {
                batman = context.Heroes.Where(hero => hero.Name == "batman").FirstOrDefault();
            }
            //Do something here

            using (var context = new SuperheroContext())
            {
                batman.DateOfBirth = new DateTime(1988, 1, 1);
                context.Entry(batman).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        private static void InsertWonderWomanToJusticeLeague()
        {
            var wonderWoman = new Hero
            {
                Name = "Wonder Woman",
                RealName = "Diana",
                Race = HeroRace.Human,
            };
            wonderWoman.Equipments.Add(new Equipment { Name = "Sword of Athena", Type = EquipmentType.Weapon });
            wonderWoman.Equipments.Add(new Equipment { Name = "Lasso of Truth", Type = EquipmentType.Tool });
            wonderWoman.Equipments.Add(new Equipment { Name = "Bracelets of Submission", Type = EquipmentType.Outwear });
            wonderWoman.Equipments.Add(new Equipment { Name = "tiara ", Type = EquipmentType.Outwear });

            using (var context = new SuperheroContext())
            {
                var team = context.Teams.TagWith("Get members Of Justice League")
                    .Where(team => team.Name == "Justice League").FirstOrDefault();

                if (team == null)
                {
                    Console.WriteLine("Justice League team not found");
                    return;
                }

                wonderWoman.Team = team;

                context.Heroes.Add(wonderWoman);
                context.SaveChanges();
            }
        }

        private static void CreateBattle()
        {
            using (var context = new SuperheroContext())
            {
                var newBattle = new Battle { Location = "Earth" };
                
                var batman = context.Heroes.Include(hero => hero.Team)
                    .Where(hero => hero.Name == "batman").FirstOrDefault();
                var wonderWoman = context.Heroes.Where(hero => hero.Name == "Wonder Woman").FirstOrDefault();

                newBattle.Heroes = new System.Collections.Generic.List<Hero> { batman, wonderWoman };

                newBattle.Heroes.Add(new Hero
                {
                    Name = "Supper Man",
                    RealName = "Clark Kent",
                    Team = batman.Team
                });

                context.Battles.Add(newBattle);
                context.SaveChanges();
            }
        }
        private static void GetWonderWomentEquipments()
        {
            using (var context = new SuperheroContext())
            {
                //var equipments = context.Equipments.Where(e => e.Hero.Name == "Wonder Woman").ToList();

                //var wonderWoman = context.Heroes
                //    .Where(hero => hero.Name == "Wonder Woman")
                //    .Include(hero => hero.Equipments)
                //    .FirstOrDefault();


                //var wonderWoman = context.Heroes
                //    .Single(hero => hero.Name == "Wonder Woman");
                ////Do some code here
                //context.Entry(wonderWoman)
                //    .Collection(b => b.Equipments)
                //    .Load();

                var wonderWoman = context.Heroes
                   .Single(hero => hero.Name == "Wonder Woman");

                var equipments = wonderWoman.Equipments;
                equipments.ForEach(e => Console.WriteLine(e.Name));
            }
        }

        private static void GetHeroNameAndRealNameInJL()
        {
            using (var context = new SuperheroContext())
            {
                var heroes = context.Heroes
                    .Where(hero => hero.Team.Name == "Justice League")
                    .Select(h => new { Name = h.Name, RealName = h.RealName }).ToList();

                foreach (var hero in heroes)
                {
                    Console.WriteLine("{0}-{1}", hero.Name, hero.RealName);
                }
            }
        }

        private static void ExecuteRawSQL()
        {
            using (var context = new SuperheroContext())
            {
                var heroes = context.Heroes.FromSqlRaw("select * from Heroes").ToList();

                foreach (var hero in heroes)
                {
                    Console.WriteLine("{0}-{1}", hero.Name, hero.RealName);
                }
            }
        }

    }
}
