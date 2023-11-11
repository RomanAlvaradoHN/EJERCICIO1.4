using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EJERCICIO1._4.Models {
    public class FotoData {
        private List<string> invalidData = new List<string>();
        private byte[] fotoArray;
        private string fotoPath;
        private string nombre;
        private string descripcion;





        public FotoData() { }

        public FotoData(byte[] fotoArray, string fotoPath, string nombre, string descripcion) {
            this.FotoArray = fotoArray;
            this.FotoPath = fotoPath;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
        }




        public List<string> GetDatosInvalidos() {
            return this.invalidData;
        }





        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }



        [Column("FotoArray")]
        public byte[] FotoArray {
            get { return this.fotoArray; }

            set {
                if (value != null && value.Length > 0) {
                    this.fotoArray = value;
                } else {
                    this.invalidData.Add("FotoArray");
                }
            }
        }


        [Column("FotoPath")]
        public string FotoPath{
            get { return this.fotoPath; }

            set {
                if (!string.IsNullOrEmpty(value)) {
                    this.fotoPath = value;
                } else {
                    this.invalidData.Add("FotoPath");
                }
            }
        }



        [Column("Nombre")]
        public string Nombre {
            get { return this.nombre; }

            set {
                if (!string.IsNullOrEmpty(value)) {
                    this.nombre = value;
                } else {
                    this.invalidData.Add("Nombre");
                }
            }
        }



        [Column("Descripcion")]
        public string Descripcion {
            get { return this.descripcion; }

            set {
                if (!string.IsNullOrEmpty(value)) {
                    this.descripcion = value;
                } else {
                    this.invalidData.Add("Descripcion");
                }
            }
        }




    }
}
