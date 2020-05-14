namespace protocol_change
{
    using System;
    using System.Net;

    class Program
    {
        protected static bool TestUrl(string url)
        {
            var wr = (HttpWebRequest)WebRequest.Create(url);

            try
            {
                var hwr = (HttpWebResponse)wr.GetResponse();
                return (hwr.StatusCode == HttpStatusCode.OK);
            }
            catch (WebException wex)
            {
                var statusCode = ((HttpWebResponse)wex.Response).StatusCode;

                switch (statusCode)
                {
                    case HttpStatusCode.Unauthorized:

                    break;
                    case HttpStatusCode.InternalServerError:
                    break;
                    default:

                    break;
                }
                return false;
            }
        }

        protected static string ChangeUriScheme(UriBuilder uri)
        {
            uri.Scheme = Uri.UriSchemeHttps;
            uri.Port = -1;

            return uri.ToString();
        }

        static void Main(string[] args)
        {
            foreach (var item in args)
            {
                Console.WriteLine(item);

                try
                {
                    UriBuilder url = new UriBuilder(item);
                    if (url.Scheme != Uri.UriSchemeHttp) continue;

                    string updatedUri = ChangeUriScheme(url);

                    Console.WriteLine("URL " + updatedUri + " Passes: " + (TestUrl(updatedUri) ? "Yes" : "No"));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    continue;
                }
            }
        }
    }
}
