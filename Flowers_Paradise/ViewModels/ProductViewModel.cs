using Flowers_Paradise.Models;
using Flowers_Paradise.Config;
using System.Windows.Input;
using Firebase.Storage;

namespace Flowers_Paradise.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        private readonly FirebaseService _firebaseService;
        private readonly FirebaseStorage _firebaseStorage;

        private string _nombre;
        private string _descripcion;
        private decimal _precio;
        private string _foto;
        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set => SetProperty(ref _selectedProduct, value);
        }

        public string Nombre
        {
            get => _nombre;
            set => SetProperty(ref _nombre, value);
        }

        public string Descripción
        {
            get => _descripcion;
            set => SetProperty(ref _descripcion, value);
        }

        public decimal Precio
        {
            get => _precio;
            set => SetProperty(ref _precio, value);
        }

        public string Foto
        {
            get => _foto;
            set => SetProperty(ref _foto, value);
        }



        public ICommand AddProductCommand { get; set; }
        public ICommand TakePhotoCommand { get; }


        public ProductViewModel()
        {
            _firebaseService = new FirebaseService();
            AddProductCommand = new Command(async () => await AddProductAsync());
            TakePhotoCommand = new Command(async () => await TakePhotoAsync());
        }

        private async Task TakePhotoAsync()
        {
            try
            {
                var result = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Tomar Foto"
                });

                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    try
                    {
                        // Aquí puedes manejar el archivo, como subirlo a Firebase Storage
                        // Supongamos que tienes un método UploadPhotoAsync que se encarga de esto
                        var photoUrl = await _firebaseService.UploadPhotoAsync(stream, result.FullPath);

                        // Almacena la URL de la foto en la propiedad Foto de tu producto
                        Foto = photoUrl;
                    }
                    finally
                    {
                        // Asegúrate de cerrar el stream para liberar los recursos
                        stream.Close();
                    }
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                Console.WriteLine($"Feature not supported: {fnsEx.Message}");
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                Console.WriteLine($"Permission exception: {pEx.Message}");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        //Logica para eliminar producto
        public async Task DeleteProductAsync(string productId)
        {
            await _firebaseService.DeleteProductAsync(productId);
        }

        //logica para actualizar producto
        public async Task UpdateProductAsync(Product product)
        {
            await _firebaseService.UpdateProductAsync(product);
        }



        private async Task AddProductAsync()
        {
            if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Descripción) || Precio <= 0 || string.IsNullOrWhiteSpace(Foto))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor complete todos los campos.", "OK");
                return;
            }

            var newProduct = new Product
            {
                Nombre = Nombre,
                Descripcion = Descripción,
                Precio = Precio,
                Foto = Foto
            };

            await _firebaseService.AddProductAsync(newProduct);

            // Limpiar campos después de agregar
            Nombre = string.Empty;
            Descripción = string.Empty;
            Precio = 0;
            Foto = string.Empty;

            await Application.Current.MainPage.DisplayAlert("Éxito", "Producto agregado exitosamente.", "OK");

        }
    }
}
