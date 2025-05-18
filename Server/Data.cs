﻿using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DevelopersHub.RealtimeNetworking.Server
{
    public static class Data
    {
        public class Player
        {
            public int gold = 0;
            public int elixir = 0;
            public int gems = 0;

            public List<Building> buildings = new List<Building>();
        }

        public class Building
        {
            public string id = "";
            public int level = 0;
            public long databaseID = 0;
            public int x = 0;
            public int y = 0;
            public int columns = 0;
            public int rows = 0;
        }

        public class ServerBuilding
        {
            public string id = "";
            public int level = 0;
            public long databaseID = 0;
            public int requiredGold = 0;
            public int requiredElixir = 0;
            public int requiredGems = 0;
            public int columns = 0;
            public int rows = 0;
        }

        public static string Serialize<T>(this T target)
        {
            XmlSerializer xml = new XmlSerializer(typeof(T));
            StringWriter writer = new StringWriter();
            xml.Serialize(writer, target);
            return writer.ToString();
        }
        public static T Deserialize<T>(this string target)
        {
            XmlSerializer xml = new XmlSerializer(typeof(T));
            StringReader reader = new StringReader(target);
            return (T)xml.Deserialize(reader);
        }
    }
}
