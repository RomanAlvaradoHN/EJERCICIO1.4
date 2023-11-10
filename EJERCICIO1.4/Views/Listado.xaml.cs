using EJERCICIO1._4.Models;

namespace EJERCICIO1._4.Views;

public partial class Listado : ContentPage
{
	public Listado()
	{
		InitializeComponent();
	}



	protected override async void OnAppearing() {
		base.OnAppearing();

		viewListado.ItemsSource = await App.db.SelectAll();
	}





    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args) {
		await Navigation.PushAsync(new FotoView(args.SelectedItem as FotoData));
    }
}