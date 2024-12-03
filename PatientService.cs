namespace PT_App
{
    public partial class MainPage
    {
        public class PatientService
        {
            public async Task<PatientData> FindPatientInDatabase(string cprNumber)
            {
                return await App.Database.GetPatientByCPRAsync(cprNumber);
            }
        }
    }
}
