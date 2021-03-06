﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Chetch.RestAPI
{
    public class DataObjectCollection<T> : List<T>, IRestAPIObject where T : DataObject, new()
    {
        static private readonly JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

        public void Parse(string data)
        {
            var list = jsonSerializer.Deserialize<List<T>>(data);
            foreach(var item in list){
                Add(item);
            }
        }

        public string Serialize()
        {
            throw new NotImplementedException();
        }

        public T find(String fieldName, Object value)
        {
            foreach(var t in this)
            {
                var val = t.GetValue(fieldName);
                if (val.Equals(value)) return t;
            }

            return default(T);
        }
    }
}
