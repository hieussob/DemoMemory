using DrawnUi.Maui.Draw;

namespace DemoMemory
{
    public class SymbolLabel : SkiaLabel
    {
        public static readonly BindableProperty SymbolCodeProperty = BindableProperty.Create(nameof(SymbolCode), typeof(string), typeof(SymbolLabel), string.Empty,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (bindable is SymbolLabel label)
                {
                    int val = 0;
                    if (newValue is string str)
                    {
                        val = label.SymbolCodeFromHexString(str);
                    }
                    else
                    {
                        val = (int)newValue;
                    }
                    label.Text = char.ConvertFromUtf32(val);
                }
            });
        public string SymbolCode { get => (string)GetValue(SymbolCodeProperty); set => SetValue(SymbolCodeProperty, value); }
        public SymbolLabel()
        {
            FontSize = 18;
            TextColor = Colors.Black;
            VerticalTextAlignment = TextAlignment.Center;
            FontFamily = "Segoe UI Symbol";
            SymbolCode = "&#xE103;";
        }

        public int SymbolCodeFromHexString(string hexString)
        {
            if (hexString.StartsWith("&#x") && hexString.EndsWith(";"))
            {
                string hexPart = hexString.Substring(3, hexString.Length - 4);
                if (int.TryParse(hexPart, System.Globalization.NumberStyles.HexNumber, null, out int symbolCode))
                {
                    return symbolCode;
                }
            }

            return 0;
        }
    }
}
