namespace SimpleSQL.Demos
{
    using SimpleSQL;
    public class GameClass
    {
        [PrimaryKey]
        public int ClassID { get; set; }
        public string Name { get; set; }
        public int ClassDice { get; set; }
        public int HitpointMod { get; set; }
    }
}