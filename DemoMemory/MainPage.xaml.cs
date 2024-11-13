using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DemoMemory
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<DataItem> Items { get; set; } = new ObservableCollection<DataItem>();

        public MainPage()
        {
            InitializeComponent();

            //for (int i = 0; i < 30; i++)
            //{
            //    Items.Add(new DataItem
            //    {
            //        Name = $"Name {i}",
            //        Colors = GetRandomColor(),
            //        Times = GetRandomTimes()
            //    });
            //}

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //if (dataGrid != null)
            //{
            //    dataGrid.ItemsSource = null;
            //    stackLayout.Remove(dataGrid);
            //    dataGrid = null;
            //}
            Items.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            for (int i = 0; i < 30; i++)
            {
                Items.Add(new DataItem
                {
                    Name = $"Name {i}",
                    Item = new Items
                    {
                        Colors = GetRandomColor(),
                        Times = GetRandomTimes()
                    }
                });
            }
        }
        private Dictionary<int, Color> GetColors()
        {
            var colors = new Dictionary<int, Color>();
            for (var i = 0; i < 50; i++)
            {
                colors.Add(i, GetRandomColor());
            }
            return colors;

        }
        private static readonly IList<string> times =
        [
            "11:00",
                    "12:00",
                    "13:00",
                    "14:00",
                    "15:00",
                    "16:00",
                ];
        private static readonly IList<Color> colors =
        [
            Color.FromArgb("#ff57BDF8"),
                    Color.FromArgb("#ffFFD54C"),
                    Color.FromArgb("#ffFFAE2E"),
                    Color.FromArgb("#ff555E65"),
                    Color.FromArgb("#ffE57388"),
                    Color.FromArgb("#ffE4E7E9"),
                ];
        private static readonly Random random = new();
        private Color GetRandomColor()
        {
            return colors[random.Next(colors.Count)];
        }
        private string GetRandomTimes()
        {
            return times[random.Next(times.Count)];
        }
    }

    public class MyDynamicObject : INotifyPropertyChanged, IDisposable
    {
        private int id;
        private WeakReference<List<DynamicDataItem>> mainViewWeakReference;

        ~MyDynamicObject()
        {
            Dispose();
        }
        public List<DynamicDataItem> Data
        {
            get
            {
                if (mainViewWeakReference is null || !mainViewWeakReference.TryGetTarget(out var targetView))
                {
                    // Possibly View could occupy a lot of memory
                    targetView = new List<DynamicDataItem>();

                    if (mainViewWeakReference is null)
                    {
                        mainViewWeakReference = new WeakReference<List<DynamicDataItem>>(targetView);
                    }
                    else
                    {
                        try
                        {
                            mainViewWeakReference.SetTarget(targetView);
                        }
                        catch (Exception)
                        {

                        }
                    }
                }

                return targetView;
            }
        }
        // CLR property 
        public int Id
        {
            get => this.id;
            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //public override IEnumerable<string> GetDynamicMemberNames() => Data.Keys;

        //public override bool TryGetMember(GetMemberBinder binder, out object result)
        //{
        //    if (binder.Name == nameof(this.Id))
        //    {
        //        result = this.Id;
        //    }
        //    else
        //    {
        //        result = this[binder.Name];
        //    }

        //    return true;
        //}

        //public override bool TrySetMember(SetMemberBinder binder, object value)
        //{
        //    this[binder.Name] = value;
        //    return true;
        //}

        //public object this[string columnName]
        //{
        //    get
        //    {
        //        if (Data.ContainsKey(columnName))
        //        {
        //            return Data[columnName];
        //        }
        //        return null;
        //    }
        //    set
        //    {
        //        if (!Data.ContainsKey(columnName))
        //        {
        //            Data.Add(columnName, value);
        //            OnPropertyChanged(columnName);
        //        }
        //        else
        //        {
        //            if (Data[columnName] != value)
        //            {
        //                Data[columnName] = value;
        //                OnPropertyChanged(columnName);
        //            }
        //        }
        //    }
        //}

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Dispose()
        {
            Data.Clear();
        }
    }

    public class DynamicDataItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Color ItemColor { get; set; }
        public string ItemTimes { get; set; }
    }
}
