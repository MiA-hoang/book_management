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
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.ConnectionString = ConnectionString;
                    con.Open();
                }
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
            // Set ValueMember and DisplayMember BEFORE DataSource to prevent premature SelectedIndexChanged event
            cmb.ValueMember = Keyvalue;
            cmb.DisplayMember = DisplayValue;
            cmb.DataSource = dt;
            cmb.SelectedIndex = -1; // Reset selection to avoid triggering events with incomplete binding
        }

        public static string getFieldValue(string sql)
        {
            string value = "";
            try
            {
                if (con == null || con.State != ConnectionState.Open)
                {
                    Connect();
                }
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        value = result.ToString();
                        // Check if the value is "System.Data.DataRowView" which indicates incorrect data binding
                        if (value == "System.Data.DataRowView")
                        {
                            throw new InvalidOperationException(
                                "Lỗi: Giá trị trả về là DataRowView. Vui lòng kiểm tra việc truyền tham số từ ComboBox/DataGridView. " +
                                "Sử dụng SelectedValue hoặc truy cập cột cụ thể thay vì ToString() trên DataRowView.");
                        }
                    }
                }
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thực thi truy vấn: {ex.Message}\n\nSQL: {sql}",
                    "Lỗi truy vấn", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
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
