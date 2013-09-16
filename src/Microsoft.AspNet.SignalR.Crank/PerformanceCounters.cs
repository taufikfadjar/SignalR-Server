﻿using System;
using System.Diagnostics;

namespace Microsoft.AspNet.SignalR.Crank
{
    internal class PerformanceCounters
    {
        private PerformanceCounter ServerAvailableMBytesCounter;
        private PerformanceCounter SignalRConnectionsConnectedCounter;
        private PerformanceCounter SignalRConnectionsReconnectedCounter;
        private PerformanceCounter SignalRConnectionsDisconnectedCounter;
        private PerformanceCounter ServerTcpConnectionsEstCounter;

        public PerformanceCounters(string host, string signalRInstance)
        {
            ServerAvailableMBytesCounter = LoadCounter("Memory", "Available MBytes", host);
            ServerTcpConnectionsEstCounter = LoadCounter("TCPv4", "Connections Established", host);

            if (!String.IsNullOrEmpty(signalRInstance))
            {
                SignalRConnectionsConnectedCounter = LoadCounter("SignalR", "Connections Connected", host, signalRInstance);
                SignalRConnectionsReconnectedCounter = LoadCounter("SignalR", "Connections Reconnected", host, signalRInstance);
                SignalRConnectionsDisconnectedCounter = LoadCounter("SignalR", "Connections Disconnected", host, signalRInstance);
            }
        }

        public bool SignalRCountersAvailable
        {
            get
            {
                return (SignalRConnectionsConnectedCounter != null) && (SignalRConnectionsReconnectedCounter != null) && (SignalRConnectionsDisconnectedCounter != null);
            }
        }

        public int ServerAvailableMBytes
        {
            get
            {
                return (ServerAvailableMBytesCounter == null) ? 0 : GetIntValue(ServerAvailableMBytesCounter);
            }
        }

        public int SignalRConnectionsConnected
        {
            get
            {
                return (SignalRConnectionsConnectedCounter == null) ? 0 : GetIntValue(SignalRConnectionsConnectedCounter);
            }
        }

        public int SignalRConnectionsReconnected
        {
            get
            {
                return (SignalRConnectionsReconnectedCounter == null) ? 0 : GetIntValue(SignalRConnectionsReconnectedCounter);
            }
        }

        public int SignalRConnectionsDisconnected
        {
            get
            {
                return (SignalRConnectionsDisconnectedCounter == null) ? 0 : GetIntValue(SignalRConnectionsDisconnectedCounter);
            }
        }

        public int ServerTcpConnectionsEst
        {
            get
            {
                return (ServerTcpConnectionsEstCounter == null) ? 0 : GetIntValue(ServerTcpConnectionsEstCounter);
            }
        }

        private static int GetIntValue(PerformanceCounter counter)
        {
            return (int)Math.Round(counter.NextValue());
        }

        private static PerformanceCounter LoadCounter(string category, string name, string host, string instance = null)
        {
            try
            {
                var counter = new PerformanceCounter(category, name, instance, host);
                counter.NextSample();
                return counter;
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to load counter '{0}\\{1}' on host '{2}'", category, name, host);
                return null;
            }
        }
    }
}