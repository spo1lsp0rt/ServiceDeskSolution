using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDesk
{
    public class RequestModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public string title { get; set; }
        public string content { get; set; }

        public RequestModel(string _name, string _email, string _title, string _content)
        {
            name = _name;
            email = _email;
            title = _title;
            content = _content;
        }
    }
}
