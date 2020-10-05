using System.Collections.Generic;

namespace WorkingWithSortedCollections
{
    public class MyComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return y-x;
        }
    }
}