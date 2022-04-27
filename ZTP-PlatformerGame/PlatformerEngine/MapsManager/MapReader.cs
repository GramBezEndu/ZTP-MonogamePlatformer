namespace PlatformerEngine.MapsManager
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using PlatformerEngine.Sprites;

    public class MapReader
    {
        private readonly string file;

        /// <summary>
        /// Map Reader constructor.
        /// </summary>
        /// <param name="filePath">Path of map text file.</param>
        public MapReader(string filePath)
        {
            file = filePath;
        }

        public List<Sprite> BuildMap(MapBuilder builder)
        {
            string[] lines = File.ReadAllLines(file);
            foreach (string line in lines)
            {
                string[] segmentsInLine = line.Split(' ');
                foreach (string segment in segmentsInLine)
                {
                    switch (segment)
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
