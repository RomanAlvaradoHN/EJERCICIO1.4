using EJERCICIO1._4.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EJERCICIO1._4.Controllers {
    public class DBController {

        //DATABASE CONFIGURATION VARIABLES
        //=======================================================================================
        private readonly static string dbFileName = "MyAppDB.db3";

        private readonly SQLiteOpenFlags flags = SQLiteOpenFlags.ReadWrite |
                                                 SQLiteOpenFlags.Create |
                                                 SQLiteOpenFlags.SharedCache;

        private readonly string dbPath = Path.Combine(FileSystem.AppDataDirectory, dbFileName);
        //---------------------------------------------------------------------------------------
        private SQLiteAsyncConnection connection;
        //======================================================================================






        public DBController() {          
        }



        private async Task Init() {
            if (connection is not null) {
                return;
            }               
            connection = new SQLiteAsyncConnection(dbPath, flags);
            await connection.CreateTableAsync<FotoData>();
        }











        public async Task<List<FotoData>> SelectAll() {
            await Init();
            return await connection.Table<FotoData>().ToListAsync();
        }


        public async Task<FotoData> SelectById(int id) {
            await Init();
            return await connection.Table<FotoData>().Where(col => col.Id == id).FirstOrDefaultAsync();
        }




        public async Task<int> Insert(FotoData fotoData) {
            await Init();
            return await connection.InsertAsync(fotoData);
        }





        public async Task<int> Update(FotoData fotoData) {
            await Init();
            return await connection.UpdateAsync(fotoData);
        }





        public async Task<int> Delete(FotoData fotoData) {
            await Init();
            return await connection.DeleteAsync(fotoData);
        }

    }
}
