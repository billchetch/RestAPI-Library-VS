using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chetch.RestAPI
{
    public interface IRestAPIObject
    {
        void Parse(String data);

        String Serialize();
    }
}
