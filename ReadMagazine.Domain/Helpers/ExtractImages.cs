using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ReadMagazine.Domain.Helpers
{
    public static class ExtractImages
    {
        #region Metodos para extraer urlImagen
        /// <summary>Default Uri</summary>
        private const string TEMPURI = "http://tempuri.org";
        private const string STR_IMGTAG_SRC_EXP = @"<img\s+[^>]*\bsrc\s*\=\s*[\x27\x22](?<Url>[^\x27\x22]*)[\x27\x22]";

        /// <summary>
        /// Extracts the first image Url from a html string
        /// </summary>
        /// <param name="htmlString">A string containing html code</param>
        /// <returns>a string with the Url or first image in the htmlString parameter</returns>
        /// <remarks>This method uses regular expressions,so using System.Text.RegularExpressions; must be addeed</remarks>
        public static string ExtractFirstHtmlImage(string htmlString)
        {
            string respuesta = TEMPURI;
            try
            {
                var rgx = new Regex(
                    STR_IMGTAG_SRC_EXP,
                                RegexOptions.IgnoreCase | RegexOptions.Multiline);

                var match = rgx.Match(htmlString);

                respuesta = match.Groups["Url"].Value;

                if (respuesta == "")
                    respuesta = TEMPURI;
            }
            catch { respuesta = TEMPURI; }

            return respuesta;
        }

        /// <summary>
        /// Extracts the first image Url from a html string
        /// </summary>
        /// <param name="htmlString">A string containing html code</param>
        /// <returns>a collection with the image Urls contained in htmlString parameter</returns>
        /// <remarks>This method uses regular expressions,so using System.Text.RegularExpressions; 
        /// must be addeed</remarks>
        public static List<string> ExtractImageUrisFromHtml(string htmlString)
        {

            var rgx = new Regex(STR_IMGTAG_SRC_EXP,
                                RegexOptions.IgnoreCase | RegexOptions.Multiline);

            var lista = new List<string>();
            var matches = rgx.Matches(htmlString);

            foreach (Match match in matches)
            {
                var url = match.Groups["Url"].Value;
                if (!string.IsNullOrWhiteSpace(url) && !lista.Contains(url))
                {
                    lista.Add(
                    match.Groups["Url"].Value);
                }
            }
            return lista;
        }

        public static List<string> ExtractTagsCompletteImgFromHtml(string htmlString)
        {
            var rgx = new Regex(STR_IMGTAG_SRC_EXP,
                                RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var lista = new List<string>();
            var matches = rgx.Matches(htmlString);

            foreach (Match match in matches)
            {
                var imgTag = match.Groups[0].Value;
                if (!string.IsNullOrWhiteSpace(imgTag) && !lista.Contains(imgTag))
                {
                    lista.Add(imgTag);
                }
            }
            return lista;
        }

        public static Image DownloadImage(string _URL)
        {
            Image _tmpImage = null;

            try
            {
                // Open a connection
                System.Net.HttpWebRequest _HttpWebRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(_URL);

                _HttpWebRequest.AllowWriteStreamBuffering = true;

                // You can also specify additional header values like the user agent or the referer: (Optional)
                _HttpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
                _HttpWebRequest.Referer = "http://www.google.com/";

                // set timeout for 20 seconds (Optional)
                _HttpWebRequest.Timeout = 20000;

                // Request response:
                System.Net.WebResponse _WebResponse = _HttpWebRequest.GetResponse();

                // Open data stream:
                System.IO.Stream _WebStream = _WebResponse.GetResponseStream();

                // convert webstream to image
                _tmpImage = Image.FromStream(_WebStream);

                // Cleanup
                _WebResponse.Close();
                _WebResponse.Close();
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
                return null;
            }

            return _tmpImage;
        }

        public static Image CambiarTamanoImagen(String urlImage, int pAncho, int pAlto)
        {
            var imagen = DownloadImage(urlImage);
            imagen.Save(@"c:\imagenOriginal.jpg");
            //creamos un bitmap con el nuevo tamaño
            Bitmap vBitmap = new Bitmap(pAncho, pAlto);
            //creamos un graphics tomando como base el nuevo Bitmap
            using (Graphics vGraphics = Graphics.FromImage((Image)vBitmap))
            {
                //especificamos el tipo de transformación, se escoge esta para no perder calidad.
                vGraphics.InterpolationMode = InterpolationMode.Bicubic;
                //Se dibuja la nueva imagen
                vGraphics.DrawImage(imagen, 0, 0, pAncho, pAlto);
            }
            //retornamos la nueva imagen
            ((Image)vBitmap).Save(@"c:\imagenCopia.jpg"); ;
            return ((Image)vBitmap);
        }
        #endregion
    }
}
