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
                var querystring = url;

                if (querystring.Contains("app.getgov2go.com"))
                {
                    var token  = url.Substring(41);

                    source = "https://dev-v3.getgov2go.com/#/token/" + token;
                    return source;
                }
                else
                {
                    source = url;
                    return source;
                }

            }

            return source;
        }
    }
}
