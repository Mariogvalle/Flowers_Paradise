/*using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class ShoppingCartViewModel : INotifyPropertyChanged
{
    private ObservableCollection<ShoppingCartItem> cartItems;

    public ObservableCollection<ShoppingCartItem> CartItems
    {
        get => cartItems;
        set
        {
            cartItems = value;
            OnPropertyChanged();
        }
    }

    public ShoppingCartViewModel()
    {
        CartItems = new ObservableCollection<ShoppingCartItem>();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
*/