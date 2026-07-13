using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace SIAKAD.Models
{
    public class Khs
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nim { get; set; }
        public string Nama { get; set; }
        public string NamaMatkul { get; set; }
        public string Kuis { get; set; }
        public string Tugas { get; set; }
        public string Uts { get; set; }
        public string Uas { get; set; }
        public string Rata { get; set; }
        public string Grade { get; set; }
    }
}
