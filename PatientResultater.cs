using SQLite;

namespace PT_App
{
    public class PatientResultater
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string CPR { get; set; }
        public string Dato { get; set; }
        public string FCV { get; set; }
        public string FEV1 { get; set; }
        public string Ratio { get; set; }
    }
}
