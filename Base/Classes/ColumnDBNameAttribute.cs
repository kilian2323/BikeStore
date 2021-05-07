namespace Core.Classes
{
    public class ColumnDBNameAttribute : System.Attribute
    {
        public ColumnDBNameAttribute(string Name) { this.Name = Name; }
        public string Name { get; set; }
    }
}
