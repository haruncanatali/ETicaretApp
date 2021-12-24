using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretApp.Models;

namespace ETicaretApp.DbAccess.Abstract
{
    public interface IMarkaDal:IEntityRepository<Marka>,IFilterRepository<Marka>
    {
    }
}
