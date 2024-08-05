    
namespace Flowers_Paradise.CustomControls;
public abstract class MasterPage : ContentPage
{
    public MasterPage() 
    {
        var template = Application.Current.Resources["MasterTemplate"] as ControlTemplate;
        ControlTemplate= template;
    }

}