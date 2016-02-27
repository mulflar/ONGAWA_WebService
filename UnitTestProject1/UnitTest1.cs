using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ONGAWA_WebService.objetos;

namespace UnitTestProject1
{
    [TestClass]
    public class GoogleMapObjectTests
    {
        [TestMethod]
        public void CuandoElStringXMlEstaVacioLaDireccionEsVacia()
        {
            string googleObj = "";
            GoogleMapParser parser = new GoogleMapParser(googleObj);
            Assert.AreEqual(parser.Address, string.Empty);
        }

        [TestMethod]
        public void CuandoElStringXMlNoTieneDireccionLaDireccionEsVacia()
        {
            string googleObj = "<GeocodeResponse></GeocodeResponse>";
            GoogleMapParser parser = new GoogleMapParser(googleObj);
            Assert.AreEqual(parser.Address, string.Empty);
        }


        [TestMethod]
        public void CuandoElStatusNoEsOKLaDireccionEsVacia()
        {
           
            GoogleMapParser parser = new GoogleMapParser(xmlSampleNoOK);
            Assert.AreEqual(parser.Address, string.Empty);
        }


        [TestMethod]
        public void CuandoElStatusEsOKYTieneCountryLoPillamos()
        {

            GoogleMapParser parser = new GoogleMapParser(xmlSampleOK);
            Assert.AreEqual(parser.Country, "United States");
        }


        string xmlSampleNoOK = "<GeocodeResponse>" +
                        " <status> FAILED </status>" +
                        "</GeocodeResponse>";

        string xmlSampleOK="<GeocodeResponse>"+
                        " <status> OK </status>"+
                        "<result>"+
  

                    "  <address_component>                                     "+
                    "    <long_name>Santa Clara</long_name>                    "+
                    "    <short_name>Santa Clara</short_name>                  "+
                    "    <type>administrative_area_level_2</type>              "+
                    "    <type>political</type>                                "+
                    "  </address_component>                                    "+
                    "                                                          "+
                    "                                                          "+
                    "  <address_component>                                     "+
                    "    <long_name>California</long_name>                     "+
                    "    <short_name>CA</short_name>                           "+
                    "    <type>administrative_area_level_1</type>              "+
                    "    <type>political</type>                                "+
                    "  </address_component>                                    "+
                    "                                                          "+
                    "                                                          "+
                    "  <address_component>                                     "+
                    "    <long_name>United States</long_name>                  "+
                    "    <short_name>US</short_name>                           "+
                    "    <type>country</type>                                  "+
                    "    <type>political</type>                                "+
                    "  </address_component>                                    "+
                    "                                                          "+
                    "                                                          "+
                    "  <address_component>                                     "+
                    "    <long_name>94043</long_name>                          "+
                    "    <short_name>94043</short_name>                        "+
                    "    <type>postal_code</type>                              "+
                    "  </address_component>                                    "+
                    "                                                          "+
                    "                                                          "+
                    "                                                          "+
                    "    </result>                                             "+
                    "  </GeocodeResponse>                                     ";
                        }
}
