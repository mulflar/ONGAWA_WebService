using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ONGAWA_WebService.objetos
{
    public class encuesta_raw
    {
        public long lat;
        public long lon;
        public string fam;
        public int ven;
        public int pri;
        public int cie;
        public int mos;
        public int olo;
        public int agu;
        public int mat;


    }

    public class encuesta_obj
    {
        public long latitud;
        public long longitud;
        public string referencia;
        public bool ventilacion;
        public bool privacidad;

        public string pais;
        public string ciudad;
        public string direccion;
        public string CP;

        public bool mos; // Mosca
        public bool olo; // Olor
        public bool agu; // Agua
        public string mat;  // Losa
        public string cie;  // Cierre

        static string baseUri = "http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false";

        public encuesta_obj(encuesta_raw raw)
        {
            //Ventilación
            if (raw.ven == 0)
                ventilacion = true;
            else
                ventilacion = false;
            //Privacidad
            if (raw.pri == 0)
                privacidad = true;
            else
                ventilacion = false;

            //Mosca
            if (raw.mos == 0)
                mos = true;
            else
                mos = false;

            //Olor
            if (raw.olo == 0)
                olo = true;
            else
                olo = false;

            //Agua
            if (raw.agu == 0)
                agu = true;
            else
                agu = false;

            switch (raw.mat)
            {
                case 1:
                    mat = "Madera";
                    break;

                case 2:
                    mat = "Cerámica";
                    break;

                case 3:
                    mat = "Hormigón";
                    break;

                case 4:
                    mat = "Piedra";
                    break;

                default:
                    break;

            }

            switch (raw.cie)
            {
                case 1:
                    cie = "Cemento";
                    break;

                case 2:
                    cie = "Ladrillo";
                    break;

                case 3:
                    cie = "Madera";
                    break;

                case 4:
                    cie = "Candado";
                    break;
                default:
                    break;

            }



        }

        public void geo()
        {


            string requestUri = string.Format(baseUri, latitud, longitud);

            using (WebClient wc = new WebClient())
            {
                wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
                wc.DownloadStringAsync(new Uri(requestUri));
            }

        }

        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var xmlElm = XElement.Parse(e.Result);

            var status = (from elm in xmlElm.Descendants()
                          where elm.Name == "status"
                          select elm).FirstOrDefault();
            if (status.Value.ToLower() == "ok")
            {
                GoogleMapParser parser = new GoogleMapParser(xmlElm.Value);

                pais = parser.Country;
                ciudad = parser.Location;
                CP = parser.CP;

            }
            else
            {
                Console.WriteLine("No Address Found");
            }
        }

    }
}
