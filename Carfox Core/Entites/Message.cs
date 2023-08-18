using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfox_Core.Entites
{
    public class Message
    {
        public int MessageId { get; set; }
        public string MessageText { get; set; }
        public string UserName { get; set; }
        public DateTime MessageDate { get; set; } = DateTime.Now;
    }
}
