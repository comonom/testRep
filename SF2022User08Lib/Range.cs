using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF2022User08Lib
{
    //Объявляем класс промежутка времени, чтобы удобнее хранить и работать с этим
    internal class Range
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
    }
}
