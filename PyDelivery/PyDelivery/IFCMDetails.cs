using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PyDelivery
{
    public interface IFCMDetails
    {
        Task<string> GetAppToken();

       
    }
}
