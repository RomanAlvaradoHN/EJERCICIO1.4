using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EJERCICIO1._4.Models {
    public class FotoData {
        private List<string> invalidData = new List<string>();
        private byte[] foto;
        private string nombre;
        private string descripcion;





        public FotoData() { }

        public FotoData(byte[] foto, string nombre, string descripcion) {
            this.Foto = foto;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
        }




        public List<string> GetDatosInvalidos() {
            return this.invalidData;
        }





        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }




        [Column("Foto")]
        public byte[] Foto {
            get { return this.foto; }

            set {
                if (value != null && value.Length > 0) {
                    this.foto = value;
                } else {
                    this.invalidData.Add("Foto");
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
