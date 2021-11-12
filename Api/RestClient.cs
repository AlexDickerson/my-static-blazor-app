using Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Api
{
    public class RestClient
    {
        private JToken ReadResponse(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            string body = reader.ReadToEnd();
            try
            {
                return JObject.Parse(body);
            }
            catch (Exception exc)
            {
                Exception myExc = new Exception("Could not parse json body:" + body, exc);

                throw myExc;
            }
        }
        public JToken DoRequest(string path,string sessionID)
        {
            if (!path.StartsWith("/"))
            {
                path = "/" + path;
            }
            string fullUrl = path;
            string fullURLCopy = fullUrl;
            
            string requestURL = string.Empty;
            WebRequest request = HttpWebRequest.CreateDefault(new Uri(fullUrl));


            WebResponse response;
            try
            {
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //This prevents the SSL/TLS Secure Channel error.  Forces to use TLS 1.2
                if (ServicePointManager.SecurityProtocol.HasFlag(SecurityProtocolType.Tls12) == false)
                {
                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
                }

                // request.ContentType = "application/json;charset=UTF-8";
                request.Timeout = 3 * 1000;

                using (response = request.GetResponse())
                {

                    using (Stream responseStream = response.GetResponseStream())
                    {
                        return ReadResponse(responseStream);
                    }
                }
            }
            catch (System.Net.WebException e)
            {
                
                string gZipString = "";
                //check if e.response is null, then throw a new error
                if (e.Response != null)
                {
                    using (WebResponse response1 = e.Response)
                    {
                        System.IO.Compression.GZipStream g;
                        //GZipStream g = new GZipStream(;


                        HttpWebResponse httpResponse = (HttpWebResponse)response1;
                        Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                        //using (var streamReader = new StreamReader(response1.GetResponseStream(), Encoding.UTF8))
                        try
                        {
                            using (var gZipReader = new GZipStream(response1.GetResponseStream(), CompressionMode.Decompress))
                            {
                                if (gZipReader != null)
                                {
                                    var output = new MemoryStream();
                                    gZipReader.CopyTo(output);
                                    gZipString = Encoding.UTF8.GetString(output.GetBuffer(), 0, (int)output.Length);
                                    Console.WriteLine(gZipString);
                                    string y = "";
                                }
                            }
                        }
                        catch (Exception e3) //If the unzipping doesn't work just throw the original exception below
                        {
                            gZipString = "Issues uncompressing error message: " + e3.Message;

                        }
                    }
                }

                Exception e2 = new Exception(e.Message + System.Environment.NewLine + requestURL + System.Environment.NewLine + "error body:-->" + gZipString, e);
                throw e2;

              
            }
            catch (Exception e)
            {

                //Exception e2 = new Exception(requestURL, e);
                Exception e2 = new Exception(e.Message + System.Environment.NewLine + requestURL, e);
                //log.Error(e2);
                throw e2;

            }
        }
    }
}
