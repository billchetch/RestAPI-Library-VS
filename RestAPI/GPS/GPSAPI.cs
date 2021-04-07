using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chetch.RestAPI.Network;

namespace Chetch.RestAPI.GPS
{
    public class GPSAPI : APIService
    {
        public const String GPS_SERVICE_NAME = "GPS";

        public class GPSPosition : DataObject
        {
            public double Latitiude
            {
                get
                {
                    return GetDouble("latitude");
                }
            }

            public double Longitude
            {
                get
                {
                    return GetDouble("longitude");
                }
            }

            public double Bearing
            {
                get
                {
                    return GetDouble("bearing");
                }
            }

            public double Speed
            {
                get
                {
                    return GetDouble("speed");
                }
            }

            public DateTime Timestamp
            {
                get
                {
                    return GetDateTime("timestamp");
                }
            }
        }

        public class GPSStatus : DataObject
        {
            public bool IsRecording
            {
                get
                {
                    return this.ContainsKey("status") ? this["status"].ToString().ToLower().Equals("recording") : false;
                }
            }
        }

        static public GPSAPI Create()
        {
            return NetworkAPI.CreateAPIService<GPSAPI>(GPS_SERVICE_NAME);
        }

        public GPSAPI() : base() { }

        public GPSAPI(String baseURL) : base(baseURL)
        {

        }

        
        public GPSStatus GetStatus()
        {
            return Get<GPSStatus>("status");
        }

        public GPSPosition GetLatestPosition()
        {
            return Get<GPSPosition>("latest-position");
        }
    }
}
