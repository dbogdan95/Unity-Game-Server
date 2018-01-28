﻿using System;
using System.Collections.Generic;
using BytesBuffer;

namespace Server
{
    class NetworkHandleData
    {
        private delegate void Packet_(int Index, byte[] Data);
        private Dictionary<int, Packet_> Packets;

        public void InitMessages()
        {
            Packets = new Dictionary<int, Packet_>();
        }

        public void HandleData(int index, byte[] data)
        {
            int packetnum;
            Packet_ Packet;
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);
            packetnum = buffer.ReadInteger();
            buffer = null;

            if (packetnum == 0)
                return;

            if (Packets.TryGetValue(packetnum, out Packet))
            {
                Packet.Invoke(index, data);
            }
        }
    }
}
