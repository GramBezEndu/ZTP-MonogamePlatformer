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
