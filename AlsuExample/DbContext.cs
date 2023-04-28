using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlsuExample
{
    public class DbContext
    {
        private static DbContext _context;
        public static DbContext Context
        {
            get
            {
                if (_context == null)
                    _context = new DbContext();
                return _context;
            }
        }

        private MySqlConnection _connection;

        public DbContext()
        {
            _connection = new MySqlConnection("server=cfif31.ru;user id=ISPr22-32_KalininAV;password=ISPr22-32_KalininAV;database=ISPr22-32_KalininAV_Zayavki;charset=utf8");
            _connection.Open();
        }

        public DataTable Select(string sql)
        {
            DataTable dt = new DataTable();
            var command = new MySqlCommand(sql, _connection);
            using (var reader = command.ExecuteReader())
            {
                dt.Load(reader);
            }

            return dt;
        }
    }
}
