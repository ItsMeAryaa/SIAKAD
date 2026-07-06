using SIAKAD.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Essentials;

namespace SIAKAD.Data
{
    public class DatabaseHelper
    {
        static SQLiteAsyncConnection db;

        static async Task init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(
                FileSystem.AppDataDirectory,
                "SIAKAD.db");

            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<Mahasiswa>();
            await db.CreateTableAsync<Matkul>();
            //await db.CreateTableAsync<Khs>();
        }

        public static async Task<int> addMahasiswa(Mahasiswa mahasiswa)
        {
            await init();
            return await db.InsertAsync(mahasiswa);
        }

        public static async Task<List<Mahasiswa>> getMahasiswa()
        {
            await init();
            return await db.Table<Mahasiswa>().ToListAsync();
        }

        public static async Task<int> updateMahasiswa(Mahasiswa mahasiswa)
        {
            await init();
            return await db.UpdateAsync(mahasiswa);
        }

        public static async Task<int> deleteMahasiswa(Mahasiswa mahasiswa)
        {
            await init();
            return await db.DeleteAsync(mahasiswa);
        }

        public static async Task<Mahasiswa> getMahasiswaByNim(string nim)
        {
            await init();
            return await db.Table<Mahasiswa>().Where(m => m.Nim == nim).FirstOrDefaultAsync();
        }

        public static async Task<List<Matkul>> getMatkul()
        {
            await init();
            return await db.Table<Matkul>().ToListAsync();
        }

        public static async Task<int> addMatkul(Matkul matkul)
        {
            await init();
            return await db.InsertAsync(matkul);
        }

        public static async Task<int> deleteMatkul(Matkul matkul)
        {
            await init();
            return await db.DeleteAsync(matkul);
        }

        public static async Task<int> updateMatkul(Matkul matkul)
        {
            await init();
            return await db.UpdateAsync(matkul);
        }
    }
}
