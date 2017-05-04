using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class JMove
    {
        private string name;
        private string direction;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        public string Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JMove"/> class.
        /// </summary>
        /// <param name="name2">The name2.</param>
        /// <param name="direction2">The direction2.</param>
        public JMove(string name2, string direction2)
        {
            name = name2;
            direction = direction2;
        }
    }
}
