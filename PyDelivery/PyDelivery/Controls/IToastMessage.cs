using System;
using System.Collections.Generic;
using System.Text;

namespace PyDelivery.Controls
{
    public interface IToastMessage
    {
        void LongTime(string ToastMessage);
        void ShortTime(string ToastMessage);
    }
}
