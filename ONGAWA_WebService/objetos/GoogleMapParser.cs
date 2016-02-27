using System.Linq;
using System.Xml.Linq;

namespace ONGAWA_WebService.objetos
{
    public class GoogleMapParser
    {
        private string googleObj;
        private string country;
        private string location;
        private string cp;
 



        public GoogleMapParser(string googleObj)
        {
            this.googleObj = googleObj;

            if (!string.IsNullOrEmpty(googleObj)) { 
            var xmlElm = XElement.Parse(this.googleObj);

                
            var status = (from elm in xmlElm.Descendants()    //comprobamos el status para ver que todo va bien
                          where elm.Name == "status"
                          select elm).FirstOrDefault();
            if (status!=null&&string.Compare( status.Value.Trim(),"OK",true)==0)
                {
                    var result = xmlElm.Element("result");   //Cuando vemos que sí es correcto el status buscamos la información

                    //Conseguimos el pais
                    var resCountry = (from elm in result.Elements("address_component")
                               where TipoCountry(elm)
                               select elm).FirstOrDefault();

                    if (resCountry != null)
                        this.Country = resCountry.ToString();

                    //Conseguimos la ciudad
                    var resLocalition = (from elm in result.Elements("address_component")
                                         where TipoLocation(elm)
                                         select elm).FirstOrDefault();

                    if (resLocalition != null)
                        this.Country = resLocalition.ToString();

                    //Conseguimos el codigo postal
                    var resCP = (from elm in result.Elements("address_component")
                                      where TipoCP(elm)
                                      select elm).FirstOrDefault();

                    if (resCP != null)
                        this.Country = resCP.ToString();


                }
            }
        }



        private static bool TipoCountry(XElement elm)
        {
            return (string)elm.Element("type") == "country";
        }

        private static bool TipoLocation(XElement elm)
        {
            return (string)elm.Element("type") == "locality";
        }

        private static bool TipoCP(XElement elm)
        {
            return (string)elm.Element("type") == "postal_code";
        }



        private static bool TipoAddress(XElement elm)
        {
            return (string)elm.Element("type") == "country";
        }


       // public string Address { get { return ""; } internal set { this.googleObj = value; } }






        //Devolvemos los parámetros 

        public string Country {
            get
            {
                return country;
            }

            internal set { this.country = value;  } }

        public string Location
        {
            get
            {
                return location;
            }

            internal set { this.location = value; }
        }

        public string CP
        {
            get
            {
                return cp;
            }

            internal set { this.cp = value; }
        }






    }
}