using Carfox_Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfox_Core.Repositories
{
    public interface ITypeHubClient
    {
        Task BroadcastMsg(Message message);
    }
}
