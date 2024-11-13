//using System.Collections.ObjectModel;

//namespace DemoMemory
//{
//    class Main
//    {
//        public Main()
//        {
//            //for (int i = 0; i < 30; i++)
//            //{

//            //    var column = new DataGridTextColumn
//            //    {
//            //        HeaderText = $"Column{i}",
//            //        CellContentTemplate = new DataTemplate(() =>
//            //        {
//            //            var frame = new Frame()
//            //            {
//            //                HeightRequest = 50,
//            //                WidthRequest = 50,
//            //                BackgroundColor = GetRandomColor()
//            //            };
//            //            return frame;
//            //        }),
//            //    };
//            //    dataGrid.Columns.Add(column);
//            //}
//            //dataGrid.ItemsSource = Items;


//            //for (int i = 0; i < 30; i++)
//            //{
//            //    dynamic item = new ExpandoObject();
//            //    item.Country = "Country " + i;
//            //    item.Capital = "Capital " + i;
//            //    item.Population = i * 10000;

//            //    this.Data.Add(item);
//            //}

//            // Init();
//        }
//        // private DynamicDataGrid dataGrid;
//        // public ObservableCollection<ExpandoObject> Data { get; set; }
//        private ObservableCollection<MyDynamicObject> _items;
//        public ObservableCollection<MyDynamicObject> Data = [];
//        //{
//        //    get => _items;
//        //    set
//        //    {
//        //        if (_items != null)
//        //        {
//        //            _ = CleanupItems(_items);
//        //        }
//        //        _items = value;
//        //    }
//        //}

//        //private async Task CleanupItems(ObservableCollection<MyDynamicObject> items)
//        //{
//        //    dynamicDataGrid.Columns.Clear();

//        //    foreach (MemoryDataGridTemplateColumn column in dynamicDataGrid.Columns)
//        //    {
//        //        column.Dispose();
//        //    }

//        //    const int batchSize = 50;
//        //    var itemsList = items.ToList();

//        //    for (int i = 0; i < itemsList.Count; i += batchSize)
//        //    {
//        //        var batch = itemsList.Skip(i).Take(batchSize);
//        //        foreach (var item in batch)
//        //        {
//        //            // Cleanup graphics resources
//        //            item.Dispose();
//        //            //item.Color = null;
//        //        }

//        //        if (i + batchSize < itemsList.Count)
//        //        {
//        //            await Task.Yield(); // Cho phép UI thread xử lý
//        //        }
//        //    }

//        //    items.Clear();
//        //    GC.Collect();
//        //    GC.WaitForPendingFinalizers();
//        //    GC.Collect();
//        //    //GC.Collect(0);
//        //}

//        private int count = 0;
//        public void CreateDynamicData()
//        {
//            this.Data = [];
//            var timeConvert = new TimeConverter();
//            this.Data = new ObservableCollection<MyDynamicObject>();
//            for (int i = 0; i < 10; i++)
//            {
//                var row = new MyDynamicObject() { Id = i };
//                for (int j = 0; j < 20; j++)
//                {
//                    //row[string.Format("Column{0}", j)] = $"Cell {i}{j}";
//                    ++count;
//                }

//                this.Data.Add(row);
//            }
//            for (int i = 0; i < count; i++)
//            {

//                var column = new DataGridTextColumn
//                {
//                    HeaderText = $"Column{i}",
//                    CellRenderer = new CustomColumnRenderer()
//                    //{
//                    //    var boder = new Frame
//                    //    {
//                    //        HeightRequest = 50,
//                    //        WidthRequest = 50,
//                    //        BackgroundColor = GetRandomColor()
//                    //    };
//                    //    return boder;
//                    //})
//                };
//                dataGrid.Columns.Add(column);
//            }
//            dataGrid.ItemsSource = this.Data;
//        }

//        //public void Init()
//        //{
//        //    dataGrid = new DynamicDataGrid();
//        //    var items = new ObservableCollection<DataItem>();
//        //    for (var i = 0; i < 50; i++)
//        //    {
//        //        items.Add(
//        //            new DataItem
//        //            {
//        //                Name = $"Name{i}",
//        //                Colors = GetColors(),
//        //                Times = GetTimes(),
//        //            });
//        //    }
//        //    Items = new(items);
//        //    var a = new ObservableCollection<string>();
//        //    for (var i = 0; i < 50; i++)
//        //    {
//        //        a.Add($"Column {i}");
//        //    }
//        //    dataGrid.ItemsSource = Items;
//        //    stackLayout.Add(dataGrid);
//        //}

//        private Dictionary<int, Color> GetColors()
//        {
//            var colors = new Dictionary<int, Color>();
//            for (var i = 0; i < 50; i++)
//            {
//                colors.Add(i, GetRandomColor());
//            }
//            return colors;

//        }
//        private static readonly Random random = new();
//        private Color GetRandomColor()
//        {
//            return colors[random.Next(colors.Count)];
//        }
//        private string GetRandomTimes()
//        {
//            return times[random.Next(times.Count)];
//        }
//        private void Button_Clicked(object sender, EventArgs e)
//        {
//            //if (dataGrid != null)
//            //{
//            //    dataGrid.ItemsSource = null;
//            //    stackLayout.Remove(dataGrid);
//            //    dataGrid = null;
//            //}
//            dataGrid.ItemsSource = null;
//            Items.Clear();
//            foreach (var item in this.Data)
//            {
//                (item as MyDynamicObject).Dispose();
//            }
//            GC.Collect();
//            GC.WaitForPendingFinalizers();
//            GC.Collect();
//            //GC.Collect();
//        }
//        private void Button_Clicked_1(object sender, EventArgs e)
//        {
//            for (int i = 0; i < 30; i++)
//            {
//                Items.Add(new DataItem
//                {
//                    Name = $"Name {i}",
//                    Item = new Items
//                    {
//                        Colors = GetRandomColor(),
//                        Times = GetRandomTimes()
//                    }
//                });
//            }
//            dataGrid.ItemsSource = Items;
//            // CreateDynamicData();
//            //this.Data = [];
//            //this.Data = new ObservableCollection<MyDynamicObject>();
//            //for (int i = 0; i < 30; i++)
//            //{
//            //    var row = new MyDynamicObject() { Id = i };
//            //    for (int j = 0; j < 5; j++)
//            //    {
//            //        row[string.Format("Column {0}", j)] = string.Format("Cell {0} {1}", i, j);
//            //    }

//            //    this.Data.Add(row);
//            //}
//            //dynamicDataGrid.ItemsSource = this.Data;
//            // Init();
//            //var a = new ObservableCollection<string>();
//            //for (var i = 0; i < 50; i++)
//            //{
//            //    a.Add($"Column {i}");
//            //}
//            //dataGrid.ItemsSource = a;
//        }
//        private static readonly IList<string> times =
//        [
//            "11:00",
//            "12:00",
//            "13:00",
//            "14:00",
//            "15:00",
//            "16:00",
//        ];
//        private static readonly IList<Color> colors =
//        [
//            Color.FromArgb("#ff57BDF8"),
//            Color.FromArgb("#ffFFD54C"),
//            Color.FromArgb("#ffFFAE2E"),
//            Color.FromArgb("#ff555E65"),
//            Color.FromArgb("#ffE57388"),
//            Color.FromArgb("#ffE4E7E9"),
//        ];

//        private Dictionary<int, string> GetTimes()
//        {
//            var colors = new Dictionary<int, string>();
//            for (var i = 0; i < 50; i++)
//            {
//                colors.Add(i, GetRandomTimes());
//            }
//            return colors;
//        }
//    }
//}
