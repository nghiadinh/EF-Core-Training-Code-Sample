using System.Collections.Generic;

namespace SuperheroApp.Domain
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Hero> Heroes { get; set; } = new List<Hero>();
    }
}
