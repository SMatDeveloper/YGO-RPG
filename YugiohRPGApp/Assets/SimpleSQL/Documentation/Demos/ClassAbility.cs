namespace SimpleSQL.Demos
{
    using SimpleSQL;
    public class ClassAbility 
    {
        [PrimaryKey]
        public int AbilityID { get; set; }  
        public int ClassID { get; set; }
        public string AbilityName { get; set; }
        public string AbilityText { get; set; }

    }
}
