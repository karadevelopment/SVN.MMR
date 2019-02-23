using SVN.MMR.Enums;
using System;

namespace SVN.MMR
{
    public static class Elo
    {
        private static double GetExpectedValue(int elo1, int elo2)
        {
            var eloDiff = elo2 - elo1;
            var exp = eloDiff / 2000d;

            var numerator = 1;
            var denominator = 1 + Math.Pow(10, exp);

            return numerator / denominator;
        }

        public static void Adjust(ref int elo1, ref int elo2, Winner winner, out int change1, out int change2)
        {
            var s1 = winner == Winner.Player1 ? 1 : winner == Winner.Draw ? .5 : 0;
            var s2 = winner == Winner.Player2 ? 1 : winner == Winner.Draw ? .5 : 0;
            var e1 = Elo.GetExpectedValue(elo1, elo2);
            var e2 = Elo.GetExpectedValue(elo2, elo1);

            change1 = (int)Math.Round(50 * (s1 - e1));
            change2 = (int)Math.Round(50 * (s2 - e2));

            elo1 += change1;
            elo2 += change2;
        }
    }
}