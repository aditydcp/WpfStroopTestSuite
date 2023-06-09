using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfStroopTestSuite.Models;
using Type = WpfStroopTestSuite.Models.Type;

namespace WpfStroopTestSuite.Repositories
{
    internal class TrialSetRepository
    {
        private readonly List<TrialSet> trialSets = new();

        public IEnumerable<TrialSet> GetAllTrialSets() { return trialSets; }

        public TrialSetRepository()
        {
            // square set
            trialSets.Add(
                new TrialSet(
                    new List<Trial>() {
                        new Trial(Color.Red, Type.Square),
                        new Trial(Color.Green, Type.Square),
                        new Trial(Color.Blue, Type.Square),
                        new Trial(Color.Yellow, Type.Square)
                    }));

            // word set
            trialSets.Add(
                new TrialSet(
                    new List<Trial>()
                    {
                        new Trial(Color.Red, Type.Red),
                        new Trial(Color.Red, Type.Green),
                        new Trial(Color.Red, Type.Blue),
                        new Trial(Color.Red, Type.Yellow),
                        new Trial(Color.Green, Type.Red),
                        new Trial(Color.Green, Type.Green),
                        new Trial(Color.Green, Type.Blue),
                        new Trial(Color.Green, Type.Yellow),
                        new Trial(Color.Blue, Type.Red),
                        new Trial(Color.Blue, Type.Green),
                        new Trial(Color.Blue, Type.Blue),
                        new Trial(Color.Blue, Type.Yellow),
                        new Trial(Color.Yellow, Type.Red),
                        new Trial(Color.Yellow, Type.Green),
                        new Trial(Color.Yellow, Type.Blue),
                        new Trial(Color.Yellow, Type.Yellow),
                    }));
        }
    }
}
