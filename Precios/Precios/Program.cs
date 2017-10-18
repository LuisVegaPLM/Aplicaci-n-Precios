using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Net;
using System.Data.SqlClient;
using System.Data.Common;
using Newtonsoft.Json;
using System.Json;



namespace Precios
{
    class Program
    {

        static void Main(string[] args)
        {
            
            string pathArchivo = "C:\\Users\\luis.vega\\Desktop";
            var root = Path.Combine(pathArchivo, "JSON");
            StreamWriter SW;
            SW = System.IO.File.CreateText(root + @"\Precio.json");
            root = Path.Combine(root, "Precio.json");
            int count = 0;


            String conexion = "data source=195.192.2.249;initial catalog=Medinet 20170911;user id=sa;password=t0m$0nl@t1n@";
            
            SqlConnection cnn = new SqlConnection(conexion);
            cnn.Open();
            String consulta = "select  p.PresentationId,p.DivisionId,p.CategoryId,p.ProductId, p.PharmaFormId,  pt.Brand, p.Presentation, b.BarCode,b.BarCodeId from BarCodes b inner join ProductBarCodes pb on b.BarCodeId = pb.BarCodeId inner join Presentations p on pb.PresentationId = p.PresentationId inner join Products pt on p.ProductId = pt.ProductId";
            SqlCommand cmd = new SqlCommand(consulta, cnn);


            SqlDataReader read = cmd.ExecuteReader();

            
            
            while (read.Read())
            {
               
                try
                {


                    string Active = "false";
                    string brand = "";
                    string presentation = "";
                    string[] getpreciosTotal;
                    string[] getprecios;
                    string price = "";
                    string Distri = "";
                    string Name = "";


                    BarCodes BarCodes_reult = new BarCodes();

                    BarCodes_reult.PresentationId = Convert.ToInt32(read.GetValue(0));
                    BarCodes_reult.DivisionId = Convert.ToInt32(read.GetValue(1));
                    BarCodes_reult.CategoryId = Convert.ToInt32(read.GetValue(2));
                    BarCodes_reult.ProductId = Convert.ToInt32(read.GetValue(3));
                    BarCodes_reult.PharmaFormId = Convert.ToInt32(read.GetValue(4));
                    BarCodes_reult.BarCodeId = Convert.ToInt32(read.GetValue(8));
                    BarCodes_reult.BarCode = read.GetValue(7).ToString();
                    BarCodes_reult.Presentation = read.GetValue(6).ToString();

                    if (BarCodes_reult.BarCodeId == 4470)
                       {
                           int num = 0;
                           num = 2;
                       }

                    if (BarCodes_reult.BarCodeId == 3332)
                    {
                        int num2 = 0;
                        num2 = 2;
                    }

                    Remplazar clean = new Remplazar();

                    //string cadena = "https://api.byprice.com/item/" + BarCodes_reult.BarCode.Trim();
                    string cadena = "https://byprice.com/producto/" + BarCodes_reult.BarCode.Trim();
                    string GetCadena = "https://gate.byprice.com/public/item/details?uuid=";

                  		
                    WebRequest request = WebRequest.Create(cadena);
                    request.Credentials = CredentialCache.DefaultCredentials;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                    
                    //////////////////////////////////
                    responseFromServer = clean.GetUrl(responseFromServer);

                    GetCadena = GetCadena + responseFromServer;

                    WebRequest request1 = WebRequest.Create(GetCadena);
                    request1.Credentials = CredentialCache.DefaultCredentials;
                    HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
                    Stream dataStream1 = response1.GetResponseStream();
                    StreamReader reader1 = new StreamReader(dataStream1);
                    string responseFromServer1 = reader1.ReadToEnd();

                    

                    
                    

                    Active = clean.Active(responseFromServer1);

                    if (Active == "true")
                    {
                        responseFromServer1 = clean.CleanName(responseFromServer1);
                        brand = clean.GetBrand(responseFromServer1);
                        presentation = clean.GetPresentation(responseFromServer1);
                        getpreciosTotal = clean.getprices(responseFromServer1);
                        Name = clean.GetName(responseFromServer1);
                        //getprecios = clean.GetDistibuitor(getpreciosTotal);

                        for (int j = 0; j < getpreciosTotal.Length; j++)
                        {

                            price = clean.precio(getpreciosTotal[j]);
                            Distri = clean.retailer(getpreciosTotal[j]);

                            string Precio = "{\"PresentationId\":" + BarCodes_reult.PresentationId + ",\"DivisionId\":" + BarCodes_reult.DivisionId + ",\"CategoryId\":" + BarCodes_reult.CategoryId + ",\"ProductId\":" + BarCodes_reult.ProductId + ",\"PharmaFormId\":" + BarCodes_reult.PharmaFormId + ",\"BarCodeId\":" + BarCodes_reult.BarCodeId + ",\"NombreProducto\":\"" + presentation + "\",\"Nombredistribuidor\":\"" + Distri + "\",\"Precio\":\"" + price + "\",\"Presentation\":\"" + BarCodes_reult.Presentation + "\",} \n";

                           Console.WriteLine("{\"PresentationId\":" + BarCodes_reult.PresentationId + ",\"DivisionId\":" + BarCodes_reult.DivisionId + ",\"CategoryId\":" + BarCodes_reult.CategoryId + ",\"ProductId\":" + BarCodes_reult.ProductId + ",\"PharmaFormId\":" + BarCodes_reult.PharmaFormId + ",\"BarCodeId\":" + BarCodes_reult.BarCodeId + ",\"NombreProducto\":\"" + presentation + "\",\"Nombredistribuidor\":\"" + Distri + "\",\"Precio\":\"" + price + "\"}");


                            SW.Write(Precio);

                            //SqlConnection cnnsp = new SqlConnection(conexion);
                            //cnnsp.Open();
                            //SqlCommand spcmd = new SqlCommand("plm_spUpdatePriceByProductBarCodeByPriceSource", cnnsp);
                            //spcmd.CommandType = System.Data.CommandType.StoredProcedure;
                            //spcmd.Parameters.Clear();

                            //spcmd.Parameters.AddWithValue("@BarCodeId", BarCodes_reult.BarCodeId);
                            //spcmd.Parameters.AddWithValue("@PresentationId", BarCodes_reult.PresentationId);
                            //spcmd.Parameters.AddWithValue("@Price", precio[j]);
                            //spcmd.Parameters.AddWithValue("@SourceName", name[j]);

                            //spcmd.ExecuteNonQuery();
                            //cnnsp.Close();

                        }


                    }


                    
                    //reader1.Close();
                    //dataStream1.Close();
                    //response1.Close();
                    count = count + 1;
                    //if (count > 20)
                    //{
                    //    break;
                    //}

                }
                catch
                {

                }

            }
            count = count;
            SW.Close();
            SW.Dispose();
            Console.ReadKey(true);
        }

        

    }
}
