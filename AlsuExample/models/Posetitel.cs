using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlsuExample.models
{
    public class Posetitel
    {
        public Posetitel(DataRow row)
        {
            Id = Convert.ToInt32(row["pos_id"]);
            Name = row["pos_name"].ToString();
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
