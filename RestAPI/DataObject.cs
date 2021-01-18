using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Chetch.RestAPI
{
    public class DataObject : Dictionary<String, Object>, IRestAPIObject
    {
        static private readonly JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

        public void Parse(string data)
        {
            DataObject dataObject = jsonSerializer.Deserialize<DataObject>(data);
            foreach(var kv in dataObject)
            {
                Add(kv.Key, kv.Value);
            }
        }

        public string Serialize()
        {
            return jsonSerializer.Serialize(this);
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
