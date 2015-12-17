using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingDojo.S02E11.PCR
{
    public class ElectricianPuzzle
    {
        private IList<Cable> _cables;
        private IDictionary<Cable, int> _croisementsParCable;

        public void ParseInputFromConsole()
        {
            //Ligne 1 : un entier N représentant le nombre de câbles au total. N est compris entre 1 et 1000.
            //Lignes 2 à N + 1 : deux entiers D et F séparés par un espace et compris entre 1 et 1 000 000.
            // D et F représentent la position sur le bornier de départ et la position sur le bornier d'arrivée d'un câble donné. 

            //Il n'y aura pas deux câbles identiques dans les données.
            var nombreCables = int.Parse(Console.ReadLine());
            var cables = new List<Tuple<int, int>>(nombreCables);

            for (var i = 0; i < nombreCables; i++)
            {
                var positionsCable = Console.ReadLine().Split(' ');
                cables.Add(new Tuple<int, int>(int.Parse(positionsCable[0]), int.Parse(positionsCable[1])));
            }

            this.ParseInput(cables);
        }

        public void ParseInput(IEnumerable<Tuple<int, int>> data)
        {
            _cables = data.Select(t => new Cable(t.Item1, t.Item2)).ToList();
        }

        public int ComputeSolution()
        {
            _croisementsParCable = this.CroisementParCable(this._cables);

            while (_croisementsParCable.Values.Sum() > 0)
            {
                var nombreMaxDeCroisements = this._croisementsParCable.Values.Max();
                var cableQuiSeCroiseUnMax = this._croisementsParCable.First(kv => kv.Value == nombreMaxDeCroisements);
                this._cables.Remove(cableQuiSeCroiseUnMax.Key);

                _croisementsParCable = this.CroisementParCable(this._cables);
            }

            return this._cables.Count;
        }

        private IDictionary<Cable, int> CroisementParCable(IList<Cable> cables)
        {
            var croisementsParCable = new Dictionary<Cable, int>(this._cables.Count);
            foreach (var cable in this._cables)
            {
                croisementsParCable.Add(cable, cable.SeCroiseAvec(cables));
            }
            return croisementsParCable;
        }
    }
}
