using PlatformerEngine.Sprites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PlatformerEngine.MapsManager
{
    public class MapReader
    {
        private string file;
        /// <summary>
        /// File should be located in a specific folder (TODO: implement)
        /// </summary>
        /// <param name="fileName"></param>
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
                            builder.BuildSegmentZero();
                            break;
                        case "1":
                            builder.BuildSegmentOne();
                            break;
                        case "2":
                            builder.BuildSegmentTwo();
                            break;
                        case "3":
                            builder.BuildSegmentThree();
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
