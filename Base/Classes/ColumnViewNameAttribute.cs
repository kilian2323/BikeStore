namespace Core.Classes
{
    public class ColumnViewNameAttribute : System.Attribute
    {
        public ColumnViewNameAttribute(string Name) { this.Name = Name; }
        public string Name { get; set; }
    }
}
