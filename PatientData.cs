﻿using SQLite;

namespace PT_App
{
    // PatientData-klassen defineret separat, men kan også placeres i en anden fil
    public class PatientData
    {
        [PrimaryKey]
        public string CPR { get; set; }
        public string Name { get; set; }
        public string Alder { get; set; }
        public string Køn { get; set; }
        public string Højde { get; set; }
        public string Vægt { get; set; }
        public string Etnicitet { get; set; }
        public string Dato { get; set; }
        public string FCV { get; set; }
        public string FEV1 { get; set; }
        public string Ratio { get; set; }

    }
}
