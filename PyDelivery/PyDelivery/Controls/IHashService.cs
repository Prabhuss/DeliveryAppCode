using System;
using System.Collections.Generic;
using System.Text;

namespace PyDelivery.Controls
{
    public interface IHashService
    {
        string GenerateHashkey();
        void StartSMSRetriverReceiver();
    }
}
