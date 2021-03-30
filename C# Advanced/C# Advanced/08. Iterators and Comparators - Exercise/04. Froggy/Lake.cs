using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Froggy
{
    public class Lake : IEnumerable<int>
    {
        private List<int> stones;

        public Lake(List<int> stones)
        {
            this.stones = stones;
        }
        public IEnumerator<int> GetEnumerator()
        {
            for (var i = 0; i < stones.Count; i ++)
            {
                if (i % 2 == 0)
                {
                    yield return this.stones[i];
                }
            }

            for (var i = stones.Count - 1; i >= 0; i--)
            {
                if (i % 2 != 0)
                {
                    yield return this.stones[i];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
