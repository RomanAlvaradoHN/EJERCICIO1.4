using EJERCICIO1._4.Controllers;
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

                    using (MemoryStream ms = new MemoryStream()) {
                        Stream st = await photo.OpenReadAsync();
                        await st.CopyToAsync(ms);
                        imgFoto.Source = ImageSource.FromStream(() => new MemoryStream(ms.ToArray()));
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

            //Valida existencia del directorio, si no existe, lo crea
            if(!Directory.Exists(App.photosDirectory)) {
                Directory.CreateDirectory(App.photosDirectory);
            }

            //Instanciamos el objeto con los datos requeridos
            FotoData fotoData = new FotoData(
                fotoArray,
                txtNombre.Text,
                txtDescripcion.Text
            );


            //Guardado en SQLite y almacenamiento. (Si uno de los datos no es valido, se lanza el alert)
            if (!fotoData.GetDatosInvalidos().Any()) {
                await App.db.Insert(fotoData);

                //Guardado del archivo de imagen en fisico.
                string path = Path.Combine(App.photosDirectory, txtNombre.Text);
                using (FileStream photoFile = File.OpenWrite(path)) {
                    Stream st = new MemoryStream(fotoArray);
                    await st.CopyToAsync(photoFile);
                }

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



    private void OnBtnLimpiarClicked(object sender, EventArgs e) {
        LimpiarCampos();
    }



    private void LimpiarCampos() {
        fotoArray = new byte[0];
        imgFoto.Source = null;
        txtNombre.Text = string.Empty;
        txtDescripcion.Text = string.Empty;
    }

}