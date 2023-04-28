using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlsuExample.models
{
    public class Sotrudnik
    {
        public Sotrudnik(DataRow row)
        {
            Id = Convert.ToInt32(row["sot_id"]);
            Name = row["sot_name"].ToString();
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
