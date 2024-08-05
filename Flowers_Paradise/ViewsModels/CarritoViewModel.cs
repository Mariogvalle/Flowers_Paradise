/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowers_Paradise.ViewsModels
{
    public class CarritoViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<CarritoItem> _carritoItems;

        public ObservableCollection<CarritoItem> CarritoItems
        {
            get => _carritoItems;
            set
            {
                _carritoItems = value;
                OnPropertyChanged();
            }
        }

        public CarritoViewModel()
        {
            CarritoItems = new ObservableCollection<CarritoItem>();
        }

        public void AgregarProducto(Producto producto)
        {
            var item = CarritoItems.FirstOrDefault(ci => ci.Producto.Id == producto.Id);
            if (item != null)
            {
                item.Cantidad++;
            }
            else
            {
                CarritoItems.Add(new CarritoItem { Producto = producto, Cantidad = 1 });
            }

            OnPropertyChanged(nameof(CarritoItems));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
*/