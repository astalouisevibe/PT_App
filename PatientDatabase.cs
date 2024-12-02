using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PT_App
{
    public class PatientDatabase
    {
        private SQLiteAsyncConnection _database;

        public PatientDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<PatientData>().Wait();
            _database.CreateTableAsync<PatientMålinger>().Wait();
            _database.CreateTableAsync<PatientResultater>().Wait();
        }

        // Søger efter en patient via CPR-nummer
        public Task<PatientData> GetPatientByCPRAsync(string cprNumber)
        {
            return _database.Table<PatientData>()
                            .Where(p => p.CPR == cprNumber)
                            .FirstOrDefaultAsync();
        }
        public Task<int> UpdatePatientAsync(PatientData patient)
        {
            return _database.UpdateAsync(patient);
        }

        public Task<int> UpdateMålingerAsync(PatientMålinger målinger)
        {
            return _database.InsertAsync(målinger);
        }
        public Task<List<PatientMålinger>> GetPatientMålingerByCPRAsync(string cprNumber)
        {
            return _database.Table<PatientMålinger>()
                            .Where(m => m.CPR == cprNumber)
                            .ToListAsync();
        }
    }
}