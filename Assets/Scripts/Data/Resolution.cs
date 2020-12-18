using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using static TMPro.TMP_Dropdown;

namespace Assets.Scripts.Data
{
    /// <summary>
    /// <para>One of the game options is resolution.</para>
    /// 
    /// See <see cref="OptionsManager"/>. <br></br>
    /// See <see cref="Resolutions"/>.
    /// </summary>
    public class Resolution
    {
        private int id;
        public int Id { get => id; }
        private int width;
        public int Width { get => width; }
        private int height;
        public int Height { get => height; }
        private string asString;
        public string AsString { get => asString; }

        /// <summary>
        /// Creates Resolution Object.
        /// </summary>
        /// <param name="id">Every resolution has it's id</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Resolution(int id, int width, int height)
        {
            this.id = id;
            this.width = width;
            this.height = height;
            this.asString = String.Format("{0}x{1}", width.ToString(), height.ToString());
        }
    }
}
