using System.Text.RegularExpressions;

namespace PharmacyOnline.Common
{
    public class filterCharacters
    {
        public static string cleanInput(string input)
        {
            try
            {
                return Regex.Replace(input, @"[^\w\.@-\s]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }catch (Exception ex)
            {
                return input;
            }
        }
    }
}
