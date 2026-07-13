using SIAKAD.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Essentials;
using System.Linq;

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
            await db.CreateTableAsync<Khs>();
        }

        //MAHASISWA
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

        //MATKUL
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

        //KHS
        public static async Task<List<Khs>> getKhs()
        {
            await init();
            return await db.Table<Khs>().ToListAsync();
        }
        public static async Task<int> addKhs(Khs khs)
        {
            await init();
            return await db.InsertAsync(khs);
        }
        public static async Task<int> deleteKhs(Khs khs)
        {
            await init();
            return await db.DeleteAsync(khs);
        }
        public static async Task<int> updateKhs(Khs khs)
        {
            await init();
            return await db.UpdateAsync(khs);
        }
        public static async Task<bool> CekKhs(string nim, string namaMatkul)
        {
            await init();
            var data = await db.Table<Khs>().Where(n => n.Nim == nim && n.NamaMatkul == namaMatkul).FirstOrDefaultAsync();
            return data != null;
        }
        public static async Task<List<Khs>> getKhsGroup()
        {
            await init();
            var data = await db.Table<Khs>().ToListAsync();
            return data.GroupBy(x => x.Nim).Select(g => g.First()).ToList();
        }
        public static async Task<int> deleteKhsByNim(string nim)
        {
            await init();

            var data = await db.Table<Khs>().Where(x => x.Nim == nim).ToListAsync();

            int jumlah = 0;

            foreach (var item in data)
            {
                jumlah += await db.DeleteAsync(item);
            }

            return jumlah;
        }
    }
}
