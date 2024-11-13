using System.Dynamic;

namespace DemoMemory;

public partial class DataItem : DynamicObject
{
    public string Name { get; set; }
    public HeaderItem HeaderItem { get; set; }
    public Items Item1 { get; set; }
    public Items Item2 { get; set; }
    //public Items Item3 { get; set; }
    //public Items Item4 { get; set; }
    //public Items Item5 { get; set; }
    //public Items Item6 { get; set; }
    //public Items Item7 { get; set; }
    //public Items Item8 { get; set; }
}

public class Items
{
    public Color Colors { get; set; }
    public string Times { get; set; }
}

public class HeaderItem
{
    public string Name { get; set; }
    public int Count { get; set; }
}