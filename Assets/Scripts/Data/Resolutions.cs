using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Data
{
    /// <summary>
    /// Static class containing all possible resolutions.
    /// </summary>
    public static class Resolutions
    {
        public readonly static Resolution res1024x768 = new Resolution(0, 1024, 768);
        public readonly static Resolution res1280x960 = new Resolution(1, 1280, 960);

        public readonly static Resolution res1280x720 = new Resolution(2, 1280, 720);
        public readonly static Resolution res1600x900 = new Resolution(3, 1600, 900);
        public readonly static Resolution res1920x1080 = new Resolution(4, 1920, 1080);

        public static readonly List<Resolution> allResolutions = new List<Resolution>() 
        {
            res1024x768, res1280x960, res1280x720, res1600x900, res1920x1080
        };
    }
}
