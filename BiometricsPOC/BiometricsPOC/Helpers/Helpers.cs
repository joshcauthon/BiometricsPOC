using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiometricsPOC.Helpers
{
    public class Helpers
    {
        public static string HandleUrl(string url)
        {
            var source = "https://dev-v3.getgov2go.com/";

            if (!string.IsNullOrEmpty(url))
            {
                var querystring = url.Substring(41);

                source = "https://dev-v3.getgov2go.com/#/token/" + querystring;
                return source;
            }

            return source;
        }
    }
}
