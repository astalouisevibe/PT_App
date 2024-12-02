using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT_App.Server
{
    public class SaveValues
    {
        public async Task Save(string cprNumber, double fev1ProcessValue, double fcvProcessValue, double ratio)
        {
            var maaling = new PatientMålinger()
            {
                CPR = cprNumber,
                Dato = DateTime.Now.ToString(),
                FCV = fcvProcessValue.ToString(),
                FEV1 = fev1ProcessValue.ToString(),
                Ratio = ratio.ToString()
            };
            await App.Database.UpdateMålingerAsync(maaling);
        }

       
    }
}
