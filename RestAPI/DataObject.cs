using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chetch.RestAPI
{
    public class DataObject : Dictionary<String, Object>, IRestAPIObject
    {
        public void Parse(string data)
        {
            throw new NotImplementedException();
        }

        public string Serialize()
        {
            throw new NotImplementedException();
        }

        public int ID
        {
            get
            {
                var obj = this["id"];
                return System.Convert.ToInt32(obj);
            }
        }
    }
}
