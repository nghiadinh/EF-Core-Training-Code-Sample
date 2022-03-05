using System.Collections.Generic;

namespace SuperheroApp.Domain
{
    public class Battle
    {
        public int Id { get; set; }
        public string Location { get; set; }

        public virtual List<Hero> Heroes { get; set; }
    }
}
