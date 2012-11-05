using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;

namespace bedix
{
    class Database
    {
        SqlConnection DB;
        SqlDataAdapter adapter;
        SqlDataReader queryReader;
        DataTable usertable;
        SqlCommand query = new SqlCommand();

        public Database()
        {
            try
            {
                DB = new SqlConnection(@"Server=.\DB;Database=bedix;User ID = root; Password = semihsemih;Trusted_Connection=True;");
                DB.Open();

                query.Connection = DB;
            }
            catch (DataException ex)
            {
                new Exception("Baglanti hatasi" + ex.Message);
            }
        }
        public DataTable getUser()
        {
            try
            {
                query.CommandText = "select * from kullanicilar";
                adapter = new SqlDataAdapter(query);
                usertable = new DataTable();
                adapter.Fill(usertable);
            }
            catch (DataException ex)
            {
                new Exception("Baglanti hatasi" + ex.Message);
            }
            finally
            {
                adapter.Dispose();
            }

            return usertable;
        }

        public DataTable get(string s)
        {
            try
            {
                query.CommandText = "select * from " + s ;
                adapter = new SqlDataAdapter(query);
                usertable = new DataTable();
                adapter.Fill(usertable);
            }
            catch (DataException ex)
            {
                new Exception("Baglanti hatasi" + ex.Message);
            }
            finally
            {
                query.Dispose();
                adapter.Dispose();
            }

            return usertable;
        }

        public DataTable select(string s, string i)
        {
            try
            {
                query.CommandText = "select "+ i +" from " + s;
                adapter = new SqlDataAdapter(query);
                usertable = new DataTable();
                adapter.Fill(usertable);
            }
            catch (DataException ex)
            {
                new Exception("Baglanti hatasi" + ex.Message);
            }
            finally
            {
                query.Dispose();
                adapter.Dispose();
            }

            return usertable;
        }

        public string selectKod(string table, string ad)
        { 
            string s = "";

             try
            {
                query.CommandText = "SELECT kod FROM " + table + " where adi = '" + ad +"'";
                queryReader = query.ExecuteReader();
                queryReader.Read();
                s = queryReader["kod"].ToString();
            }
            catch (DataException ex)
            {
                new Exception("Baglanti hatasi" + ex.Message);
            }
            finally
            {
                queryReader.Dispose();
                query.Dispose();
                adapter.Dispose();
            }

            return s;
        }

        public void remove(string s)
        {
            try
            {
                query.CommandText = "DELETE FROM " + s;
                query.ExecuteNonQuery();
            }
            catch (DataException ex)
            {
                new Exception("Baglanti hatasi" + ex.Message);
            }
            finally
            {
                query.Dispose();
                adapter.Dispose();
            }
        }
        public int selectFirmaKod()
        {
            int result = 0;

            try
            {
                query.CommandText = "SELECT firmakodu  FROM firma WHERE   id = (SELECT MAX(id)  FROM firma)";
                queryReader = query.ExecuteReader();
                queryReader.Read();
                result = Convert.ToInt32(queryReader["firmakodu"].ToString()) + 1;
            }
            catch (DataException ex)
            {
                new Exception("Baglanti hatasi" + ex.Message);
            }
            finally
            {
                query.Dispose();
                queryReader.Dispose();
                adapter.Dispose();
            }

            return result;
        }
        public int selectUrunKod(string f)
        {
            int result = 0;

            try
            {
                query.CommandText = "SELECT urunkodu  FROM urun WHERE   id = (SELECT MAX(id)  FROM urun where firmakodu ='" + f + "')";
                queryReader = query.ExecuteReader();
                queryReader.Read();
                result = Convert.ToInt32(queryReader["urunkodu"].ToString()) + 1;
            }
            catch (DataException ex)
            {
                new Exception("Baglanti hatasi" + ex.Message);
            }
            finally
            {
                query.Dispose();
                queryReader.Dispose();
                adapter.Dispose();
            }

            return result;
        }
        public int selectRenkKod(string u,string f)
        {
            int result = 0;

            try
            {
                query.CommandText = "SELECT renkkodu  FROM renk WHERE   id = (SELECT MAX(id)  FROM renk where urunkodu ='" + u + "' and firmakodu = '" + f + "')";
                queryReader = query.ExecuteReader();
                queryReader.Read();
                result = Convert.ToInt32(queryReader["renkkodu"].ToString()) + 1;
            }
            catch (DataException ex)
            {
                new Exception("Baglanti hatasi" + ex.Message);
            }
            finally
            {
                query.Dispose();
                queryReader.Dispose();
                adapter.Dispose();
            }

            return result;
        }
        public void insert(string s)
        {
            try
            {
                query.CommandText = "INSERT INTO " + s;
                query.ExecuteNonQuery();                
            }
            catch (DataException ex)
            {
                new Exception("Baglanti hatasi" + ex.Message);
            }
            finally
            {
                query.Dispose();
                adapter.Dispose();
            }
        }

        public int isThere(string s)
        {
            try
            {
                query.CommandText = "select * from " + s;
                adapter = new SqlDataAdapter(query);
                usertable = new DataTable();
                adapter.Fill(usertable);

                if (usertable.Rows.Count == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch (DataException ex)
            {
                new Exception("Baglanti hatasi" + ex.Message);
            }
            finally
            {
                query.Dispose();
                adapter.Dispose();
            }
            return 0;
        }

        public int userControl(string name, string pass)
        {
            usertable = getUser();

            foreach (DataRow row in usertable.Rows)
            {
                if ( name == row["kullaniciAdi"].ToString() && pass == row["sifre"].ToString() )
                {
                    return 1;    
                }
            }
            return 0;
        }
        public int userIsAdmin(string name)
        {
            usertable = getUser();

            foreach (DataRow row in usertable.Rows)
            {
                if (name == row["kullaniciAdi"].ToString())
                {
                    if (row["admin"].ToString() == "1")
                        return 1;
                    else
                        return 0;
                }
            }
            return 0;
        }

        public string selectFRU(string table, string column)
        {
            string result = "";

            try
            {
                query.CommandText = "select " + column + " from " + table;
                queryReader = query.ExecuteReader();
                queryReader.Read();
                result = queryReader[column].ToString();
            }
            catch (DataException ex)
            {
                new Exception("Baglanti hatasi" + ex.Message);
            }
            finally
            {
                query.Dispose();
                queryReader.Dispose();
                adapter.Dispose();
            }

            return result;
        }
    }
}
