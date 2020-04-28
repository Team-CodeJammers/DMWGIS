using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMWGIS.API.Models
{
    public class UserNotification
    {
        //       "alertid" : "123456",
        //"userid" : "9995401111",
        //"latitude" : "10.0159",
        //"longitude" : "76.3419",
        //"city" : "Kochi",
        //"event" : "flood",
        //"type" : "medical",
        //"status" : "active",
        //"createtime" : "26-04-2020 18:35:00"
        public string alertid { get; set; }
        public string userid { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string city { get; set; }
        public string eventtype { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string createtime { get; set; }

    }
}
