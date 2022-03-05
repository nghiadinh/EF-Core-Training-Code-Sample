using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperheroApp.Domain
{
    public class Hero
    {
        public int Id { get; set; }
        [Column("HeroName")]
        public string Name { get; set; }
        public string RealName { get; set; }
        public HeroRace Race { get; set; }
        public DateTime DateOfBirth { get; set; }

        public virtual Team Team { get; set; }
        public virtual List<Equipment> Equipments { get; set; } = new List<Equipment>();
        public virtual List<Battle> Battles { get; set; }
    }
}
