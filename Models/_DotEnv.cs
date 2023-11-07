using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Stones.Models
{
    public class _DotEnv
    {
        public static Dictionary<string, string> Variables { get; private set; }

        public static void Load(string filePath = "D:/Epicode/01_backend/01_esercizi/23.10.13_ALE/Stones/.env")
        {
            Variables = new Dictionary<string, string>();

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(".env file not found");
            }

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(new[] { '=' }, 2);
                if (parts.Length == 2)
                {
                    Variables[parts[0]] = parts[1];
                }
            }
        }

        public static string Get(string key)
        {
            if (Variables.ContainsKey(key))
            {
                return Variables[key];
            }
            else
            {
                return null;
            }
        }
    }
}