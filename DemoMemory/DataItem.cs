namespace DemoMemory;

public partial class DataItem
{
    public string Name { get; set; }
    //public Color Colors { get; set; }
    //public string Times { get; set; }
    public Items Item { get; set; }
}

public class Items
{
    public Color Colors { get; set; }
    public string Times { get; set; }
}
