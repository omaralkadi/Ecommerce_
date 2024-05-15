using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat_Core.Entities
{
    public class BaseEntity<Tkey>
    {
        public Tkey id { get; set; }
    }
}
