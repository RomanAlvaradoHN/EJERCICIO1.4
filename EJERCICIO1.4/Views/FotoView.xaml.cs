using EJERCICIO1._4.Models;


namespace EJERCICIO1._4.Views;

public partial class FotoView : ContentPage {
	FotoData fd;


	public FotoView(FotoData fotoData) {
		InitializeComponent();
		fd = fotoData;
	}



	protected override void OnAppearing() {
		base.OnAppearing();

		//estaPagina.BindingContext = fd;
		// Source="{Binding FotoArray, Converter={StaticResource toStreamImageSource}}"

		fotoView.Source = ImageSource.FromFile(fd.FotoPath);
    }

}