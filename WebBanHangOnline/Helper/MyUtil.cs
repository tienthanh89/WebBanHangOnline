using System.Text;

namespace WebBanHangOnline.Helper
{
    public class MyUtil
    {
        public static string GenerateRandoomKey(int length = 5)
        {
            var pattern = @"aljhsfjalhlskjhalfhQJAFHUAFHAFJKHJ";
            
            var sb  = new StringBuilder();
            var rd = new Random();

            for(int i = 0; i < length; i++)
            {
                sb.Append(pattern[rd.Next(0,pattern.Length)]);
            }

            return sb.ToString();
        }
    }
}
