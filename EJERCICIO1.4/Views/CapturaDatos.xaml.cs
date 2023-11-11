using EJERCICIO1._4.Controllers;
using EJERCICIO1._4.Models;


namespace EJERCICIO1._4.Views;

public partial class CapturaDatos : ContentPage
{
    private byte[] fotoArray;
    private string fotoPath;


    public CapturaDatos(){
		InitializeComponent();
	}





    public async void TakeAPhoto(object sender, EventArgs e) {
        if (MediaPicker.Default.IsCaptureSupported) {
            
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
           
            if (photo != null) {
                try {

                    using (MemoryStream ms = new MemoryStream()) {
                        Stream st = await photo.OpenReadAsync();
                        await st.CopyToAsync(ms);
                        imgFoto.Source = ImageSource.FromStream(() => new MemoryStream(ms.ToArray()));
                        fotoPath = Path.Combine(App.photosDirectory, photo.FileName);
                        fotoArray = ms.ToArray();
                    }

                } catch (Exception ex) {
                    await DisplayAlert("Atención", ex.Message, "Aceptar");
                }
            }
        }
    }






    private async void OnBtnAgregarClicked(object sender, EventArgs e) {
        try {

            //Guardado del archivo de imagen en fisico.
            using (FileStream photoFile = File.OpenWrite(fotoPath)) {
                Stream st = new MemoryStream(fotoArray);
                await st.CopyToAsync(photoFile);
            }

            FotoData fotoData = new FotoData(
                fotoArray,
                fotoPath,
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