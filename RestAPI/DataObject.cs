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
                return GetInt("id");
            }
        }

        public bool IsNull(String fieldName)
        {
            return this[fieldName] == null;
        }

        public Object GetValue(String fieldName)
        {
            if (IsNull(fieldName)) return null;
            return this[fieldName];
        }

        public Object GetValue<T>(String fieldName, T defaultValue = default(T))
        {
            if (IsNull(fieldName)) return defaultValue;
            return this[fieldName];
        }

        public String GetString(String fieldName)
        {
            return (String)GetValue<String>(fieldName, null);
        }

        public int GetInt(String fieldName, int defaultValue = 0)
        {
            return System.Convert.ToInt32(GetValue(fieldName, defaultValue));
        }

        public uint GetUInt(String fieldName, uint defaultValue = 0)
        {
            return System.Convert.ToUInt32(GetValue(fieldName, defaultValue));
        }

        public long GetLong(String fieldName, long defaultValue = 0)
        {
            return System.Convert.ToInt64(GetValue(fieldName, defaultValue));
        }

        public double GetDouble(String fieldName, double defaultValue = 0)
        {
            return System.Convert.ToDouble(GetValue(fieldName, defaultValue));
        }

        public bool GetAsBool(String fieldName)
        {
            int i = GetInt(fieldName, 0);
            return i > 0;
        }

        //returns date time object of a certain 'kind'
        public DateTime GetDateTime(String fieldName, DateTimeKind kind = DateTimeKind.Local)
        {
            String ds = GetString(fieldName);
            if(ds == null)
            {
                return default(DateTime);
            } else
            {
                DateTime dt = DateTime.Parse(ds);
                return DateTime.SpecifyKind(dt, kind);
            } 
            
        }
    }
}
