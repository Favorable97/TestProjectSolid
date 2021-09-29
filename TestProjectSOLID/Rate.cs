using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace TestProjectSOLID
{
    /*
     * Класс описывает таблицу Rate в БД
     */
    [Table]
    class Rate
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int RateID { get; set; }
        [Column]
        public int CurrencyID { get; set; }
        [Column]
        public DateTime Date { get; set; }
        [Column]
        public Int16 Nominal { get; set; }
        [Column]
        public decimal Value { get; set; }
    }
}
