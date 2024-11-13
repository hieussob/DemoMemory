using Telerik.Maui.Controls.DataGrid;

namespace DemoMemory
{
    public class MemoryDataGridTemplateColumn : DataGridTemplateColumn, IDisposable
    {
        private bool _disposed = false;

        // Destructor
        ~MemoryDataGridTemplateColumn()
        {
            Dispose(false);
        }


        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                this.CellContentTemplate = null;
            }

            // Free any unmanaged objects here.
            _disposed = true;
        }
    }
}
