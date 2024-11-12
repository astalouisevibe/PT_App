namespace PT_App
{
    public partial class MainPage
    {
        public class CPRValidator
        {
            public bool IsValid(string cprNumber)
            { 
                return !string.IsNullOrWhiteSpace(cprNumber) && cprNumber.Length ==10 && long.TryParse(cprNumber, out _);
            }
        }
    }
}
