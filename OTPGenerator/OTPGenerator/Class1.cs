namespace OTPGenerator
{
    public class OTPGenerator
    {

        public static string Generate(int userId)
        {
            DateTime _dateTime = DateTime.UtcNow;
            return _dateTime.ToString("dd-MM-yyyy-HH-mm-ss");

        }
    }
}