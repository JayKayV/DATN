using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Globalization;
using System.Linq;
using System.Diagnostics;

namespace SharedLibrary.Ultility
{
    public static class ColorHelper
    {
        public static Color GetColorFrom(string data)
        {
            try
            {
                return GetColorFromName(data);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"[INFO]: Will now try to convert \"{data}\" by hex");
            }
            return FromHex(data);
        }

        private static readonly Dictionary<string, Color> _colorsByName = typeof(Color)
            .GetRuntimeProperties()
            .Where(p => p.PropertyType == typeof(Color))
            .ToDictionary(p => p.Name, p => (Color)p.GetValue(null), StringComparer.OrdinalIgnoreCase);

        public static Color GetColorFromName(string name)
        {
            Color color;

            if (_colorsByName.TryGetValue(name, out color))
                return color;

            throw new InvalidOperationException($"{name} is not a valid color");
        }

        public static Color FromHex(string data)
        {
            if (string.IsNullOrEmpty(data))
                return Color.Transparent;
            var startIndex = 0;
            if (data.StartsWith("#"))
                startIndex++;
            var r = int.Parse(data.Substring(startIndex, 2), NumberStyles.HexNumber);
            var g = int.Parse(data.Substring(startIndex + 2, 2), NumberStyles.HexNumber);
            var b = int.Parse(data.Substring(startIndex + 4, 2), NumberStyles.HexNumber);
            var a = data.Length > 6 + startIndex ? int.Parse(data.Substring(startIndex + 6, 2), NumberStyles.HexNumber) : 255;

            return new Color(r, g, b, a);
        }
    }
}
