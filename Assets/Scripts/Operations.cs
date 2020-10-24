using System.Collections.Generic;
using UnityEngine;

namespace Operations
{
    public static class Probability
    {
        public static int GetOdd()
        {
            if (isWinnerSlot())
            {
                return GetSlot(0);
            }
            int randomSlot = Random.Range(1, 5);
            return GetSlot(randomSlot);
        }

        private static int GetSlot(int index)
        {
            return index;
        }

        private static bool isWinnerSlot()
        {
            int randomNumber = Random.Range(1, 100);
            return randomNumber % 10 == 0;
        }
    }
}
