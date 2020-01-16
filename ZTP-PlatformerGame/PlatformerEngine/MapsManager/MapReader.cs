using PlatformerEngine.Sprites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PlatformerEngine.MapsManager
{
    public class MapReader
    {
        private readonly string file;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">The name of map file (it has to be located in folder where .exe file is)</param>
        public MapReader(string fileName)
        {
            file = fileName;
        }
        public List<Sprite> BuildMap(MapBuilder builder)
        {
            string[] lines = File.ReadAllLines(file);
            foreach(var line in lines)
            {
                string[] segmentsInLine = line.Split(' ');
                foreach(var segment in segmentsInLine)
                {
                    switch(segment)
                    {
                        case "0":
                            builder.BuildSegment(0);
                            break;
                        case "1":
                            builder.BuildSegment(1);
                            break;
                        case "2":
                            builder.BuildSegment(2);
                            break;
                        case "3":
                            builder.BuildSegment(3);
                            break;
                        default:
                            throw new ArgumentException("Invalid block segment in map file");
                    }
                }
                builder.NewLine();
            }
            return builder.MapObjects;
        }
    }
}
