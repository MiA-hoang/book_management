using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    internal class DAO
    {
        public static string ConnectionString =
            @"Data Source=(LocalDB)\MSSQLLocalDB;

 AttachDbFilename=|DataDirectory|\Database.mdf;
 Integrated Security=True";


        public static SqlConnection con = new SqlConnection();

        public static void Connect()
        {
            con.ConnectionString = ConnectionString;
            try
            {
                if (con != null && con.State == ConnectionState.Closed)
                    con.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void Close()
        {
            try
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public static DataTable LoadDataToTable(string sql)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, DAO.con);
            adapter.Fill(dt);
            return dt;
        }

        public static string getSQLdateFromText(string dateDDMMYYYY)
        {
            string[] elemets = dateDDMMYYYY.Split('/');
            return elemets[2] + '/' + elemets[1] + '/' + elemets[0];

        }

        public static void FillDataToCombo(string sql, ComboBox cmb, string Keyvalue, string DisplayValue)
        {
            SqlDataAdapter mydataAdapter = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            mydataAdapter.Fill(dt);
            cmb.DataSource = dt;
            cmb.ValueMember = Keyvalue;
            cmb.DisplayMember = DisplayValue;
        }

        public static string getFieldValue(string sql)
        {
            string value = "";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                value = reader.GetValue(0).ToString();
            }
            reader.Close();
            return value;
        }

        public static bool CheckKey(string sql)
        {
            SqlDataAdapter mydata = new SqlDataAdapter(sql, con);
            DataTable table = new DataTable();
            mydata.Fill(table);
            if (table.Rows.Count > 0)
                return true;

            else
                return false;
        }


    }
}
