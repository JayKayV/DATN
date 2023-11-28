using System;
using System.Collections.Generic;

namespace SharedLibrary.TileSet.Tiled
{
    public class TiledLayerProperty
    {
        public string name;
        public string type;
        public object value;
    }

    public class TiledMapLayer
    {
        public int id;
        public int[] data;
        public int x;
        public int y;
        public int height;
        public int width;
        public int opacity;
        public string name;
        public string type;
        public bool visible;
        public List<TiledLayerProperty> properties;

        public T? GetProperty<T>(string propertyName)
        {
            TiledLayerProperty? property = properties.Find(x => x.name == propertyName);
            if (property == null)
                return default(T);

            if (typeof(T) == typeof(float))
            {
                //Dangerous cast
                object newValue = Convert.ToSingle(property.value);
                return (T) newValue;
            }
            return (T) property.value;
        }
    }
}
