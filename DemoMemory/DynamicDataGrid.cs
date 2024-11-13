using System.Collections;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.DataGrid;

namespace DemoMemory;

public class DynamicDataGrid : RadDataGrid, IDisposable
{
    private List<WeakReference<MemoryDataGridTemplateColumn>> _templateColumns = new List<WeakReference<MemoryDataGridTemplateColumn>>();
    private List<Grid> _grid = new List<Grid>();

    public DynamicDataGrid()
    {
    }

    public new static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource),
        typeof(IList),
        typeof(DynamicDataGrid),
        propertyChanged: OnItemsSourceChanged);

    public new IList ItemsSource
    {
        get => (IList)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }
    private bool _isCleaningUp = false;

    private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var grid = (DynamicDataGrid)bindable;
        //grid.SetItemsSource();
        if (grid._isCleaningUp) return;

        // Cleanup old columns trước khi set source mới
        grid._isCleaningUp = true;
        try
        {
            // Clear existing columns
            grid.Columns.Clear();

            foreach (var column in grid.Columns)
            {
                if (column is MemoryDataGridTemplateColumn graphicColumn)
                {
                    graphicColumn.Dispose();
                }
            }


            // Force immediate collection
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
        finally
        {
            grid._isCleaningUp = false;
        }
        grid.SetItemsSource();
        //base.OnItemsSourceChanged(oldValue, newValue);
    }

    private void SetItemsSource()
    {
        base.ItemsSource = ItemsSource;
        Dispose();
        CreateDynamicColumns();
    }

    private void CreateDynamicColumns()
    {
        Columns.Add(CreateFirstColumn());

        for (var i = 0; i < 50; i++)
        {
            Columns.Add(CreateDefaultColumn(i));
        }
    }

    private MemoryDataGridTemplateColumn CreateDefaultColumn(int index)
    {
        var dataTemplate = new MemoryDataGridTemplateColumn
        {
            CanUserReorder = false,
            SizeMode = DataGridColumnSizeMode.Fixed,
            Width = 40,
            HeaderContentTemplate = new DataTemplate(() =>
            {
                var headerGrid = new Grid
                {
                    WidthRequest = 40,
                };

                var medicalCheckNameGrid = new Grid();

                var medicalCheckNameLabel = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Start,
                    VerticalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    Padding = new Thickness(0, 8, 0, 0),
                    Text = $"column {index}"
                };

                medicalCheckNameGrid.Children.Add(medicalCheckNameLabel);
                headerGrid.Add(medicalCheckNameGrid);
                _grid.Add(headerGrid);
                return headerGrid;
            }),
            CellContentTemplate = new DataTemplate(() =>
            {
                var grid = new Grid();
                var frame = new Frame()
                {
                    BorderColor = Colors.Black,
                    BackgroundColor = Colors.White,
                    Padding = new Thickness(0),
                    HeightRequest = 40,
                    WidthRequest = 40,
                };
                frame.SetBinding(RadBorder.BackgroundColorProperty, new Binding($"Colors[{index}]"));
                var labelTime = new Label()
                {
                    TextColor = Colors.Black,
                    FontSize = 12,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    HeightRequest = 40,
                    WidthRequest = 40,
                };
                labelTime.SetBinding(Label.TextProperty, new Binding($"Times[{index}]"));
                grid.Add(frame);
                grid.Add(labelTime);
                _grid.Add(grid);
                return grid;
            })
        };

        _templateColumns.Add(new WeakReference<MemoryDataGridTemplateColumn>(dataTemplate));
        return dataTemplate;
    }

    private MemoryDataGridTemplateColumn CreateFirstColumn()
    {
        var dataTemplate = new MemoryDataGridTemplateColumn
        {
            CanUserReorder = false,
            SizeMode = DataGridColumnSizeMode.Auto,
            HeaderContentTemplate = new DataTemplate(() =>
            {
                var grid = new Grid
                {
                    HeightRequest = 40,
                    BackgroundColor = Colors.Red,
                };
                _grid.Add(grid);
                return grid;
            }),
            CellContentTemplate = new DataTemplate(() =>
            {
                var parentsGrid = new Grid
                {
                    HeightRequest = 40,
                };

                var clientInformationGrid = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.Start,
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 0,
                };
                var border = new RadBorder
                {
                    BorderColor = Colors.Coral,
                    BorderThickness = new Thickness(0, 0, 1, 0),
                    HeightRequest = 40,
                    Content = clientInformationGrid
                };

                var label = new Label
                {
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    TextColor = Colors.Coral,
                };
                label.SetBinding(Label.TextProperty, new Binding(nameof(DataItem.Name)));
                _grid.Add(parentsGrid);
                return parentsGrid;
            }),

            IsFrozen = true,
        };

        _templateColumns.Add(new WeakReference<MemoryDataGridTemplateColumn>(dataTemplate));
        return dataTemplate;
    }

    public void Dispose()
    {
        if (_grid.Count > 0)
        {
            foreach (var grd in _grid)
            {
                ClearBindings(grd);
            }
            _grid.Clear();
        }

        // Clear columns and their templates
        foreach (var column in Columns)
        {
            if (column is DataGridTemplateColumn templateColumn)
            {
                ClearDataTemplate(templateColumn.HeaderContentTemplate);
                ClearDataTemplate(templateColumn.CellContentTemplate);
            }
        }
        Columns.Clear();

        // Clear template columns
        foreach (var weakReference in _templateColumns)
        {
            if (weakReference.TryGetTarget(out var templateColumn))
            {
                templateColumn.Dispose();
            }
        }
        _templateColumns.Clear();

        GC.Collect();
        GC.SuppressFinalize(this);
    }

    private void ClearDataTemplate(DataTemplate dataTemplate)
    {
        if (dataTemplate == null) return;

        var content = dataTemplate.CreateContent();
        if (content is View view)
        {
            ClearBindings(view);
        }
    }

    private void ClearBindings(IView view)
    {
        // Clear all BindableProperty bindings
        foreach (var property in view.GetType().GetProperties())
        {
            if (!property.CanWrite || property.PropertyType != typeof(BindableProperty)) continue;
            var bindableProperty = (BindableProperty?)property.GetValue(view);
            if (bindableProperty == null) continue;
            if (view is BindableObject bindableObject)
            {
                bindableObject.RemoveBinding(bindableProperty);
            }
        }

        // Clear BindingContext
        if (view is BindableObject { BindingContext: not null } bindableObjectWithContext)
        {
            bindableObjectWithContext.BindingContext = null;
            bindableObjectWithContext.ClearValue(BindingContextProperty);
        }

        // Clear GestureRecognizers
        if (view is View mauiView)
        {
            var pointerRecognizers = mauiView.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .ToList();
            foreach (var recognizer in pointerRecognizers)
            {
                mauiView.GestureRecognizers.Remove(recognizer);
            }

            mauiView.GestureRecognizers?.Clear();
        }

        // Recursively clear bindings for child elements
        if (view is Layout layout)
        {
            foreach (var child in layout.Children)
            {
                ClearBindings(child);
            }
        }
    }
}
