using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Flowers_Paradise.Models;
using Firebase.Storage;

namespace Flowers_Paradise.ViewModels
{

    public class ProductListModels : INotifyPropertyChanged
    {
        private FirebaseClient firebase;
        private readonly FirebaseStorage firebaseStorage;

        public ObservableCollection<Product> Products { get; set; }

        //para producto seleccionado
        public ObservableCollection<Product> FilteredProducts { get; set; }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                if (_selectedProduct != value)
                {
                    _selectedProduct = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedProduct)));
                    // Manejar la acción de selección
                    OnProductSelected();
                }
            }
        }

        private readonly INavigation _navigation;


        public ProductListModels()
        {
            Products = new ObservableCollection<Product>();
            firebase = new FirebaseClient("https://flowersparadise-7ba1b-default-rtdb.firebaseio.com/");
            firebaseStorage = new FirebaseStorage("gs://flowersparadise-7ba1b.appspot.com");

            LoadProductsAsync();
        }

        private void OnProductSelected()
        {
            if (SelectedProduct != null)
            {
//                var page = new Views.DetalleProducto(ProductListModels SelectedProduct);
//                _navigation.PushAsync(page);
                //await _navigation.PushAsync(new Views.DetalleProducto(SelectedProduct));
                // Aquí puedes manejar la acción cuando se selecciona un producto
                // Por ejemplo, navegar a una página de detalles

            }
        }

        private async void LoadProductsAsync()
        {
            var products = await GetProductsAsync();
            foreach (var product in products)
            {
                Products.Add(product);
            }
        }
        private async Task<List<Product>> GetProductsAsync()
        {
            return (await firebase.Child("products")
                       .OnceAsync<Product>()).Select(item => new Product
                       {
                           Id = item.Key,
                           Nombre = item.Object.Nombre,
                           Descripcion = item.Object.Descripcion,
                           Precio = item.Object.Precio,
                           Foto = item.Object.Foto,
                           Categoria = item.Object.Categoria
                       }).ToList();
        }

        private async void LoadProducts()
        {
            try
            {
                var products = await firebase
                    .Child("products")
                    .OnceAsync<Product>();
                foreach (var product in products)
                {
                    // Assuming Products is a collection like ObservableCollection
                    Products.Add(product.Object);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
  
