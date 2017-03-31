using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlGpsGate
{
    class Models
    {
        public class Combo
        {
            public string Value { get; set; }
            public string Name { get; set; }
        }

        public class RealTime
        {
            public int Id { set; get; }
            public string Date { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string  MsgName { get; set; }
            public string Valid { get; set; }
        }

        public class Events
        {
            public string client { get; set; }
            public string date { get; set; }
            public string event_rule { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string vehicle { get; set; }
            public string IMEI { get; set; }
            public string vehicle_phone_number { get; set; }
            public string default_driver_name { get; set; }
            public string default_driver_email { get; set; }
            public string default_driver_phone_number { get; set; }
            public string current_driver_name { get; set; }
            public string current_driver_email { get; set; }
            public string current_driver_phone_number { get; set; }
        }

    }
}
