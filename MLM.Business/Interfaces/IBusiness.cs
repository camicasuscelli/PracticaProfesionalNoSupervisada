using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLM.Common.DTO;

namespace MLM.Business.Interfaces
{
    public interface IBusiness
    {
        Usuario GetUser(string user);
    }
}
