using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace SIAKAD.Models
{
    public class Mahasiswa
    {
        [PrimaryKey]
        public string Nim { get; set; }
        public string Nama { get; set; }
        public string Prodi { get; set; }
        public string NoTelp { get; set; }
        public string Alamat { get; set; }
        public string Foto { get; set; }
    }
}
