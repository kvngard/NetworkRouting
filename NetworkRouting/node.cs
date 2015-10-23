using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkRouting
{
    public class node
    {
        public int id;
        public int index;
        public double value;
        public node prev = null;

        public node(int id, double value, int index)
        {
            this.id = id;
            this.value = value;
            this.index = index;
        }
    }

}
