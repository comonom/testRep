using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlsuExample.models
{
    public class Zayavka
    {
        public Zayavka(DataRow row)
        {
            Id = Convert.ToInt32(row["zay_id"]);
            DateStart = Convert.ToDateTime(row["zay_date"]);

            Sotrudnik = new Sotrudnik(row);
        }

        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public Sotrudnik Sotrudnik { get; set; }
        public List<Posetitel> Posetitel { get; set; } = new List<Posetitel>(); // чтобы в будущем не проверять, не нулл ли список
        //сразу объявим его

        public string AllPosetiteli => string.Join(',', Posetitel.Select(s => s.Name));

        public static List<Zayavka> GetAll(string filter)
        {
            List<Zayavka> zayavki = new List<Zayavka>();

            var dt = DbContext.Context.Select(@$"
            Select
             z.idzayavka as zay_id,
             z.datestart as zay_date,
             s.idsotrudniki as sot_id,
             s.name as sot_name,
             p.idposetiteli as pos_id,
             p.name as pos_name
            from zayavka z
            join sotrudniki s ON s.idsotrudniki = z.sotrudniki_idsotrudniki
            join zayavka_has_posetiteli zhp ON zhp.zayavka_idzayavka = z.idzayavka
            join posetiteli p ON p.idposetiteli = zhp.posetiteli_idposetiteli
            WHERE s.name LIKE '%{filter}%' OR p.name LIKE '%{filter}%'
            ");

            foreach(DataRow row in dt.Rows)
            {
                //создаем объект заявки
                var zayavka = new Zayavka(row);
                //проверяем (получаем объект) что такой заявки нету в списке
                //(записи заявки дублируются, отличается лишь посетитель, из-за того что
                //посетителей много на 1 заявку)
                var existZayavka = zayavki.FirstOrDefault(z => z.Id == zayavka.Id);
                if (existZayavka != null) // если уже есть заявка, то просто добавим посетителя
                {
                    existZayavka.Posetitel.Add(new Posetitel(row));
                }
                else
                {
                    zayavka.Posetitel.Add(new Posetitel(row));
                    zayavki.Add(zayavka);
                }
            }

            return zayavki;
        }
    }
}
