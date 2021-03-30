using System;
using System.Collections.Generic;
using System.Text;

namespace Tuple
{
    public class Tuple<TItemOne, TItemTwo>
    {
        public Tuple(TItemOne itemOne, TItemTwo itemTwo)
        {
            this.ItemOne = itemOne;
            this.ItemTwo = itemTwo;
        }
        public TItemOne ItemOne { get; set; }
        public TItemTwo ItemTwo { get; set; }

        public override string ToString()
        {
            return $"{this.ItemOne} -> {this.ItemTwo}";
        }
    }
}
