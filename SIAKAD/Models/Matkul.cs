using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIAKAD.Models
{
    public class Matkul
    {
        [PrimaryKey]
        public string Kode { get; set; }
        public string Nama { get; set; }
        public string Sks { get; set; }
    }
}
