using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Precios
{
    class Remplazar
    {
        public string CleanName(string name)
        {
            name = name.Replace("á", "a");
            name = name.Replace("Á", "A");
            name = name.Replace("é", "e");
            name = name.Replace("É", "E");
            name = name.Replace("í", "i");
            name = name.Replace("Í", "I");
            name = name.Replace("ó", "o");
            name = name.Replace("Ó", "O");
            name = name.Replace("Ú", "U");
            name = name.Replace("ú", "u");
            name = name.Replace("ü", "u");
            name = name.Replace("Ü", "U");
            return name;
        }

        public string CleanPrice(string name)
        {
            
            name = name.Replace("\",", ".");
            name = name.Replace("\"price\":", "");
            name = name.Replace("\"", "");
            name = name.Replace(",", "");
            name = name.Trim();
            return name;
        }

        public string GetUrl (string url)
        {
            int inicio = 0;
            int final = 0;
            int Getcadn = 0;
            string subcadena;
            string Search = "content=\"https://byprice.com/producto/";
            string search2 = ">";
            string search3 = "/";

            inicio = url.IndexOf(Search, inicio);
            final = url.IndexOf(search2, inicio);

            Getcadn = final - inicio;
            subcadena = url.Substring(inicio, Getcadn);
            inicio = 0;
            final = 0;

            subcadena = subcadena.Replace(Search, "");
            subcadena = subcadena.Replace("\"", "");

            //final = subcadena.IndexOf(search3, inicio);

            //subcadena = subcadena.Substring(inicio, final);


            return subcadena;
        }

        public string Active (string display )
        {
            int inicio = 0;
            int final = 0;
            int Getcadn = 0;
            string subcadena;
            string Search = "\"display\":";
            string search2 = ",";

            inicio = display.IndexOf(Search, inicio);
            final = display.IndexOf(search2, inicio);

            Getcadn = final - inicio;
            subcadena = display.Substring(inicio, Getcadn);

            subcadena = subcadena.Replace("\"display\": ", "");

            if (subcadena == "0")
            {
                display = "false";
            }
            else if (subcadena == "1")
            {
                display = "true";
            }

            else
            {
                display = "false";
            }

            return display;
        }

        public string GetBrand (string brand)
        {
            //"brand": {"key": "valdecas", "name": "VALDECAS"}, 
            int inicio = 0;
            int final = 0;
            int Getcadn = 0;
            string subcadena;
            string getbrand = "\"brand\": {";
            string Search = "},";
            string name = "\"name\":";
            string finname = " \"";
            string key = "\"key\":";
            

            inicio = brand.IndexOf(getbrand, inicio);
            final = brand.IndexOf(Search, inicio);

            Getcadn = (final + 2) - inicio;
            subcadena = brand.Substring(inicio, Getcadn);
            inicio = 0;
            final = 0;

            inicio = subcadena.IndexOf(name, inicio);
            final = subcadena.IndexOf(finname, inicio);

            Getcadn = (subcadena.Length) - final;

            subcadena = subcadena.Substring(final, Getcadn);

            subcadena = subcadena.Replace("},", ""); 
            subcadena = subcadena.Replace("+" + @"\", "");
            inicio = 0;
            final = 0;

            if (subcadena.IndexOf(key, inicio) > 0)
            {
                inicio = subcadena.IndexOf(",", inicio);
                subcadena = subcadena.Substring(0, inicio);
            }

            brand = subcadena.Replace("\"", "").Trim();
            return brand;
        }
        public string GetPresentation (string Presentation)
        {
            string name = "\"names\": [";
            //string Search = "],";
            string Search = "\"";
            int inicio = 0;
            int final = 0;
            int Getcadn = 0;
            string subcadena;

            inicio = Presentation.IndexOf(name, inicio);
            final = Presentation.IndexOf(Search, inicio + name.Length);
            final = Presentation.IndexOf(Search, final + 1);

            Getcadn = (final) - inicio;
            subcadena = Presentation.Substring(inicio, Getcadn);

            Presentation = subcadena.Replace(name, "");
            Presentation = Presentation.Replace("\"", "");

            return Presentation;
        }
        public string[] getprices(string prices)
        {
            int inicio = 0;
            int final = 0;
            int Getcadn = 0;
            int on = 1;
            long count = 0;
            string subcadena;
            string segprices = "\"prices\": [";
            string Search = "],";
            string parab = "{";
            string parcer = "}";
            string auxprice = "";
            string aux = "";
            string auxname = "";

            inicio = prices.IndexOf(segprices, inicio);
            final = prices.IndexOf(Search, inicio);

            Getcadn = (final + 2) - inicio;
            subcadena = prices.Substring(inicio, Getcadn);
            inicio = 0;
            final = 0;

            count = subcadena.LongCount(letra => letra.ToString() == "{");
            string[] precio = new String[count];

            for (int i = 0; i < count; i++)
            {
                inicio = subcadena.IndexOf(parab, inicio);
                final = subcadena.IndexOf(parcer, inicio);

                Getcadn = (final + 1) - inicio;

                auxprice = segmetprices(subcadena.Substring(inicio, Getcadn));
                aux = segmentretailer(subcadena.Substring(inicio, Getcadn));
                auxname = segmentname(subcadena.Substring(inicio, Getcadn));
                inicio = final;
                precio[i] = aux + "- " + auxname + ":" + auxprice + ",";

            }

            return precio;
        }
        public string GetName(string Name)
        {

            int inicio = 0;
            int final = 0;
            int Getcadn = 0;
            string subcadena;
            string getbrand = ", \"name\": \"";
            string item = "item_uuid";
            string Search = "}";

            inicio = Name.IndexOf(item, inicio);
            inicio = Name.IndexOf(getbrand, inicio);
            final = Name.IndexOf(Search, inicio);

            Getcadn = final  - inicio;
            subcadena = Name.Substring(inicio, Getcadn);



            return subcadena;
        }

        public string segmetprices (string prices)
        {
            string price = "\"price\": ";
            string end = ",";
            int inicio = 0;
            int final = 0;
            int Getcadn = 0;
            string subcadena;

            inicio = prices.IndexOf(price, inicio);
            final = prices.IndexOf(end, inicio);

            Getcadn = (final) - inicio;
            subcadena = prices.Substring(inicio, Getcadn);

            prices = subcadena.Replace("\"price\": ", "");

            return prices;
        }
        public string segmentretailer (string retailer)
        {
            string price = "\"retailer\": ";
            string end = ",";
            int inicio = 0;
            int final = 0;
            int Getcadn = 0;
            string subcadena;

            inicio = retailer.IndexOf(price, inicio);
            final = retailer.IndexOf(end, inicio);

            Getcadn = (final) - inicio;
            subcadena = retailer.Substring(inicio, Getcadn);

            retailer = subcadena.Replace("\"retailer\": ", "");
            retailer = retailer.Replace("\"", "");

            return retailer;
        }
        public string segmentname (string name)
        {
            string price = "\"name\": ";
            string end = ",";
            int inicio = 0;
            int final = 0;
            int Getcadn = 0;
            string subcadena;

            inicio = name.IndexOf(price, inicio);
            final = name.IndexOf(end, inicio);

            Getcadn = (final) - inicio;
            subcadena = name.Substring(inicio, Getcadn);

            name = subcadena.Replace("\"name\": ", "");
            name = name.Replace("\"", "");

            return name;
        }

        public string[] GetDistibuitor (string[] distributor )
        {
            string aux= "";
            int verda;
            string[] precios = new String[1];
            int count = 0;
            for (int i = 0; i < distributor.Length; i++)
            {
                if (i > 0)
                {


                    aux = distributor[i - 1];
                    verda = String.Compare(aux, distributor[i]);
                    if (verda != 0 )
                    {
                        precios[count] = distributor[i];
                        count++;
                        Array.Resize(ref precios, precios.Length + 1);
                    }
                }

                else if (i == 0)
                {
                    precios[count] = distributor[i];
                    count++;
                    Array.Resize(ref precios, precios.Length + 1);
                }
            }

            return precios;
        }

        public string precio (string precio)
        {
            int inicio = 0, fin = 0;
            string search = ":";
            string endsearch =",";
            string subcadena = "";
            int Getcadn = 0;

            inicio = precio.IndexOf(search, inicio);
            fin = precio.IndexOf(endsearch, inicio);
            Getcadn = (fin) - inicio;
            precio = precio.Substring(inicio, Getcadn);
            precio = precio.Replace(search, "");
            precio = precio.Replace(endsearch, "");

            return precio;
            
        }

        public string retailer(string retailer)
        {
            int inicio = 0, fin = 0;
            string search = ":";
            string subcadena = "";
            int Getcadn = 0;

            fin = retailer.IndexOf(search, inicio);

            retailer = retailer.Substring(inicio, fin);
            retailer = retailer.Replace(search, "");

            return retailer;
            
        }
    }
}
