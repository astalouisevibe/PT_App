namespace PT_App
{
    public partial class MainPage
    {
        public class PatientService
        {
            // Metode til at søge efter patient i databasen
            public async Task<PatientData> FindPatientInDatabase(string cprNumber)
            {
                return await App.Database.GetPatientByCPRAsync(cprNumber);
            }
        }
    }
}
