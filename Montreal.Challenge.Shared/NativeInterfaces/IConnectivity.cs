using Montreal.Challenge.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Montreal.Challenge.Shared.NativeInterfaces
{    public interface IConnectivity
    {
        Task<bool> CanPing(string ip);
        Task<bool> CanConnect(string ip, int port);
        bool IsConnected();
        bool IsConnectedWifi();
        bool IsConnectedMobile();
        ConnectionType ConnectionType { get; }
    }
}
