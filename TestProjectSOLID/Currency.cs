using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace TestProjectSOLID
{
    [Table]
    class Currency
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int CurrencyID { get; set; }
        [Column]
        public string ID { get; set; }
        [Column]
        public string NumCode { get; set; }
        [Column]
        public string CharCode { get; set; }

    }
}
