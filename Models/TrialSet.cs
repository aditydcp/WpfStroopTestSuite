using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfStroopTestSuite.Models
{
    public class TrialSet
    {
        public List<Trial> Trials { get; set; }

        // ensure no 3 consecutive trials are the same
        private static readonly int maxStreak = 3;
        private int consecutiveStreak = 0;
        private int lastTrialIndex = -1;

        private readonly Random random = new();

        public TrialSet(List<Trial> trials)
        {
            Trials = trials;
        }

        /// <summary>
        /// Pick a random trial from this set.
        /// Trial picked won't be the same as the previous trial
        /// if the previous 2 trials are the same.
        /// </summary>
        /// <returns><see cref="Trial"/> object</returns>
        public Trial GetRandomTrial()
        {
            bool search = true;
            int index = -1;

            while (search)
            {
                index = random.Next(0, Trials.Count);

                if (index == lastTrialIndex) consecutiveStreak++;
                else consecutiveStreak = 0;

                if (consecutiveStreak < maxStreak) search = false;
            }

            lastTrialIndex = index;
            return Trials[index];
        }
    }
}
