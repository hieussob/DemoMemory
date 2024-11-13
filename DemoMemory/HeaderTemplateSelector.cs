using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Maui.Controls.DataGrid;

namespace DemoMemory
{
    public class HeaderTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Column1Template { get; set; }
        public DataTemplate Column2Template { get; set; }
        // Add more templates as needed

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var column = (container as DataGridTemplateColumn)?.HeaderText;
            return column switch
            {
                "Column1" => Column1Template,
                "Column2" => Column2Template,
                // Add more cases as needed
                _ => null,
            };
        }
    }
}
