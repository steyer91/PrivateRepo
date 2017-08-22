using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace TractionCalc
{
    public static class Connect
    {
        public static SqlConnection Polaczenie()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"DataBases\TractionCalc\Data.mdf");
            //string path = @"D:\Prywatne\SEMESTR III\TractionCalc\TractionCalc\Data.mdf";
            string con = $"Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = {path}; Integrated Security = True";

            SqlConnection pol = new SqlConnection(con);
            return pol;
        }
        public static void AddData (string p_ro, string s_bo, string w_H, string wys_h,
        string rd, string bd, string dh, string fi,  string spc,string wspk,string wykn, string wsp_odkrz_k, string bp,
        string wd, string yb)
        {
            string zap = "INSERT INTO DaneKlasa (promien_ro, szerokosc_bo, wysH, wys_h, rd, bd, delta_h, fi, sp_c, wsp_K, wyk_n, wsp_odkrzt_k, bp, Wd, yb)" +
                " VALUES (@p_ro, @s_bo, @w_H, @wys_h, @rd, @bd, @dh, @fi, @spc, @wspk, @wykn, @wspodk, @bp, @wd, @yb) ";
            SqlConnection con = Polaczenie();
            SqlCommand com = new SqlCommand(zap, con);
            
            com.Parameters.AddWithValue("@p_ro", p_ro);
            com.Parameters.AddWithValue("@s_bo", s_bo);
            com.Parameters.AddWithValue("@w_H", w_H);
            com.Parameters.AddWithValue("@wys_h", wys_h);
            com.Parameters.AddWithValue("@rd", rd);
            com.Parameters.AddWithValue("@bd", bd);
            com.Parameters.AddWithValue("@dh", dh);
            com.Parameters.AddWithValue("@fi", fi);
            com.Parameters.AddWithValue("@spc", spc);
            com.Parameters.AddWithValue("@wspk", wspk);
            com.Parameters.AddWithValue("@wykn", wykn);
            com.Parameters.AddWithValue("@wspodk", wsp_odkrz_k);
            com.Parameters.AddWithValue("@bp", bp);
            com.Parameters.AddWithValue("@wd", wd);
            com.Parameters.AddWithValue("@yb", yb);
            //SqlCommand reset1 = new SqlCommand("DBCC CHECKIDENT ('DaneKlasa', RESEED, 0)", con);
            //SqlCommand reset2 = new SqlCommand("DBCC CHECKIDENT ('DaneKlasa', RESEED)", con);

            try { con.Open(); com.ExecuteNonQuery(); }// reset2.ExecuteNonQuery(); reset1.ExecuteNonQuery(); }
            catch ( SqlException ex ){ throw ex; }
            finally { con.Close(); }

        }

        public static void DeleteData(string test)
        {
            string test1 = test;
            string zap = "DELETE FROM DaneKlasa where ID=" + test1;
            
            SqlConnection con = Polaczenie();
            SqlCommand com = new SqlCommand(zap, con);
            //SqlCommand reset1 = new SqlCommand("DBCC CHECKIDENT ('DaneKlasa', RESEED, 0)", con);
            //SqlCommand reset2 = new SqlCommand("DBCC CHECKIDENT ('DaneKlasa', RESEED)", con);
            try { con.Open(); com.ExecuteNonQuery(); }// reset2.ExecuteNonQuery(); reset1.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { con.Close(); }
        }
        public static List<DaneKlasa> DataList()
        {
            List<DaneKlasa> ListaDane = new List<DaneKlasa>();
            SqlConnection con = Polaczenie();
            string wysw = "SELECT ID, promien_ro, szerokosc_bo, wysH, wys_h, rd, bd, delta_h FROM DaneKlasa";
          
            SqlCommand com = new SqlCommand(wysw, con);

            try {
                con.Open();
                SqlDataReader read = com.ExecuteReader();

                while (read.Read())
                {
                    DaneKlasa daneKlasa = new DaneKlasa();
                    daneKlasa.ID = (int)read["ID"];
                    daneKlasa.promienro = read["promien_ro"].ToString();
                    daneKlasa.szerbo = read["szerokosc_bo"].ToString();
                    daneKlasa.WysH = read["wysH"].ToString();
                    daneKlasa.Wys_h = read["wys_h"].ToString();
                    daneKlasa.Rd = read["rd"].ToString();
                    daneKlasa.Bd = read["bd"].ToString();
                    daneKlasa.Dh = read["delta_h"].ToString();

                    ListaDane.Add(daneKlasa);
                }
                read.Close();
            }
            catch (SqlException ex) { throw ex; }
            finally { con.Close(); }

            return ListaDane;

        }
        public static List<DaneKlasa> DataList1()
        {
            List<DaneKlasa> ListaDane = new List<DaneKlasa>();
            SqlConnection con = Polaczenie();
            string wysw = "SELECT ID, fi, sp_C, wsp_K, wyk_n, wsp_odkrzt_k, bp, Wd, yb FROM DaneKlasa";
            
            SqlCommand com = new SqlCommand(wysw, con);

            try
            {
                con.Open();
                SqlDataReader read = com.ExecuteReader();

                while (read.Read())
                {
                    DaneKlasa daneKlasa = new DaneKlasa();
                    daneKlasa.ID = (int)read["ID"];
                    daneKlasa.Fi = read["fi"].ToString();
                    daneKlasa.Spc = read["sp_C"].ToString();
                    daneKlasa.WspK = read["wsp_K"].ToString();
                    daneKlasa.Wykn = read["wyk_n"].ToString();
                    daneKlasa.Wsp_odrz_k = read["wsp_odkrzt_k"].ToString();
                    daneKlasa.Bp = read["bp"].ToString();
                    daneKlasa.Wd = read["Wd"].ToString();
                    daneKlasa.y_b = read["yb"].ToString();
                    

                    ListaDane.Add(daneKlasa);
                }
                read.Close();
            }
            catch (SqlException ex) { throw ex; }
            finally { con.Close(); }

            return ListaDane;

        }
        public static List<DaneKlasa> DaneObliczenia1(string index)
        {
            List<DaneKlasa> ListaDane = new List<DaneKlasa>();
            SqlConnection con = Polaczenie();
            string wysw = "SELECT * FROM DaneKlasa WHERE ID=" + index;

            SqlCommand com = new SqlCommand(wysw, con);

            try
            {
                con.Open();
                SqlDataReader read = com.ExecuteReader();

                while (read.Read())
                {
                    DaneKlasa daneKlasa = new DaneKlasa();
                    daneKlasa.ID = (int)read["ID"];
                    daneKlasa.promienro = read["promien_ro"].ToString();
                    daneKlasa.szerbo = read["szerokosc_bo"].ToString();
                    daneKlasa.WysH = read["wysH"].ToString();
                    daneKlasa.Wys_h = read["wys_h"].ToString();
                    daneKlasa.Rd = read["rd"].ToString();
                    daneKlasa.Bd = read["bd"].ToString();
                    daneKlasa.Dh = read["delta_h"].ToString();

                    ListaDane.Add(daneKlasa);
                }
                read.Close();
            }
            catch (SqlException ex) { throw ex; }
            finally { con.Close(); }

            return ListaDane;

        }
    
        public static List<DaneKlasa> DaneObliczenia2(string index)
        {
            List<DaneKlasa> ListaDane = new List<DaneKlasa>();
            SqlConnection con = Polaczenie();
            string wysw = "SELECT * FROM DaneKlasa WHERE ID=" + index;

            SqlCommand com = new SqlCommand(wysw, con);

            try
            {
                con.Open();
                SqlDataReader read = com.ExecuteReader();

                while (read.Read())
                {
                    DaneKlasa daneKlasa = new DaneKlasa();
                    daneKlasa.ID = (int)read["ID"];
                    daneKlasa.Fi = read["fi"].ToString();
                    daneKlasa.Spc = read["sp_C"].ToString();
                    daneKlasa.WspK = read["wsp_K"].ToString();
                    daneKlasa.Wykn = read["wyk_n"].ToString();
                    daneKlasa.Wsp_odrz_k = read["wsp_odkrzt_k"].ToString();
                    daneKlasa.Bp = read["bp"].ToString();
                    daneKlasa.Wd = read["Wd"].ToString();
                    daneKlasa.y_b = read["yb"].ToString();

                    ListaDane.Add(daneKlasa);
                }
                read.Close();
            }
            catch (SqlException ex) { throw ex; }
            finally { con.Close(); }

            return ListaDane;

        }
    }
}
