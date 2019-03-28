﻿using Montreal.Challenge.Shared.Enums;
using Montreal.Challenge.Shared.NativeInterfaces;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Montreal.Challenge.Shared
{
    public class NativeConnectivity : IConnectivity
    {
        public Task<bool> CanPing(string ip)
        {
            return CrossConnectivity.Current.IsReachable(ip);
        }

        public Task<bool> CanConnect(string ip, int port)
        {
            Ping myPing = new Ping();
            PingReply reply = myPing.Send(ip, port);
            return Task.FromResult(reply.Status == IPStatus.Success);
        }

        public bool IsConnected()
        {
            return CrossConnectivity.Current.IsConnected;
        }

        public bool IsConnectedWifi()
        {
            return CrossConnectivity.Current.ConnectionTypes.Any(c => c == Plugin.Connectivity.Abstractions.ConnectionType.WiFi || c == Plugin.Connectivity.Abstractions.ConnectionType.Desktop);
        }

        public bool IsConnectedMobile()
        {
            return CrossConnectivity.Current.ConnectionTypes.Any(c => c == Plugin.Connectivity.Abstractions.ConnectionType.Cellular);
        }

        public ConnectionType ConnectionType
        {
            get
            {
                if (IsConnectedWifi())
                    return ConnectionType.Wifi;

                else if (IsConnectedMobile())
                    return ConnectionType.GPRS;

                else return ConnectionType.None;
            }
        }
    }
}
