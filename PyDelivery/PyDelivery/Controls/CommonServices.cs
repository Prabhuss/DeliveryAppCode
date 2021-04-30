using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PyDelivery.Controls
{
    public static class CommonServices
    {
        public static void ListenToSmsRetriever()
        {
            DependencyService.Get<IListenToSmsRetriever>()?.ListenToSmsRetriever();
        }
    }
    public interface IListenToSmsRetriever
    {
        void ListenToSmsRetriever();
    }
}
