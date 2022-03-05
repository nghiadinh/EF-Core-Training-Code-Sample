namespace SuperheroApp.Domain
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EquipmentType Type { get; set; }

        public int HeroId { get; set; }
        public virtual Hero Hero { get; set; }
    }
}
