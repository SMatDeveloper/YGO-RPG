namespace SimpleSQL.Demos
{
    using SimpleSQL;

    public class ClassProgressionTable
    {
        [PrimaryKey]
        public int TableID { get; set; }
        public int ClassID { get; set; }
        public int Level { get; set; }
        public int SSLevel { get; set; }
        public int CPDMod { get; set; }
        public int AbVBonus { get; set; }
        public int AdditionalAbilities { get; set; }
    }
}
