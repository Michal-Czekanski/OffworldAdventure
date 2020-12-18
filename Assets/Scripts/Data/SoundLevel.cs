using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Data
{
    /// <summary>
    /// Represents sound level.
    /// </summary>
    public class SoundLevel
    {
        private int level;
        /// <summary>
        /// Value between 0 and 100.
        /// </summary>
        public int Level { get => level;  }

        public static readonly int maxSoundLevel = 100;
        public static readonly int minSoundLevel = 0;

        /// <summary>
        /// Creates SoundLevel object. Ensures that sound level is of appriopriate value.
        /// </summary>
        /// <param name="level">Sound level should be between 0 and 100.</param>
        public SoundLevel(int level)
        {
            this.level = Mathf.Clamp(level, minSoundLevel, maxSoundLevel);
        }
    }
}
