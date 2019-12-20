using PlatformerEngine.Sprites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PlatformerEngine.MapsManager
{
    public class MapReader
    {
        private string filePath;
        /// <summary>
        /// File should be located in a specific folder (TODO: implement)
        /// </summary>
        /// <param name="fileName"></param>
        public MapReader(string fileName)
        {
        }
        public List<Sprite> BuildMap(MapBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
