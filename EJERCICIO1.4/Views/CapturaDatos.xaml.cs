using EJERCICIO1._4.Models;

namespace EJERCICIO1._4.Views;

public partial class CapturaDatos : ContentPage
{
    private byte[] fotoArray;


    public CapturaDatos(){
		InitializeComponent();
	}





    public async void TakeAPhoto(object sender, EventArgs e) {
        if (MediaPicker.Default.IsCaptureSupported) {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
            if (photo != null) {
                try {
                    using Stream stream = await photo.OpenReadAsync();

                    using (MemoryStream memoryStream = new MemoryStream()) {
                        await stream.CopyToAsync(memoryStream);
                        fotoArray = memoryStream.ToArray();
                    }

                    ImageSource imgSource = StreamImageSource.FromStream(() => new MemoryStream(fotoArray));
                    imgFoto.Source = imgSource;

                } catch (Exception ex) {
                    // Manejo de excepciones
                    await DisplayAlert("Atención", ex.Message, "Aceptar");
                }
            }
        }
    }






    private async void OnBtnAgregarClicked(object sender, EventArgs e) {
        try {
            FotoData fotoData = new FotoData(
                fotoArray,
                txtNombre.Text,
                txtDescripcion.Text
            );

            if (!fotoData.GetDatosInvalidos().Any()) {
                await App.db.Insert(fotoData);
                LimpiarCampos();
                await DisplayAlert("Success", "Datos guardados", "Aceptar");

            } else {
                string msj = string.Join("\n", fotoData.GetDatosInvalidos());
                await DisplayAlert("Datos Invalidos:", msj, "Acepar");
            }

        } catch (Exception ex) {
            await DisplayAlert("Error", ex.Message, "Aceptar");
        }
    }





    private async void OnBtnListaClicked(object sender, EventArgs e) {
        await Navigation.PushAsync(new Listado());
    }





    private void LimpiarCampos() {
        imgFoto.Source = null;
        txtNombre.Text = string.Empty;
        txtDescripcion.Text = string.Empty;
    }

}