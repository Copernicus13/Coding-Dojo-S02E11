using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingDojo.S02E11.PCR
{
    public class ElectricianPuzzle
    {
        private Centrale _centrale;

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
            _centrale = new Centrale(data.ToList());
        }

        public int ComputeSolution()
        {
            // Pour retrouver l'état initial, rebrancher tous les câbles :
            foreach (var cable in this._centrale.Cables.ToList())
                cable.Brancher();

            // Pour optimiser le traitement,
            // commencer par débrancher les câbles
            // qui ont le plus de croisements :
            var nombreMaxDeCroisements = this._centrale.CroisementsParCable.Values.Max(croisements => croisements.Count);

            // Débrancher ensuite les câbles en suivant
            // l'ordre décroissant du nombre max de croisements
            // jusqu'à 0 croisement :
            while (nombreMaxDeCroisements > 0)
            {
                var cableQuiSeCroiseUnMax = this._centrale.CroisementsParCable.First(kv => kv.Value.Count == nombreMaxDeCroisements).Key;
                cableQuiSeCroiseUnMax.Debrancher();

                nombreMaxDeCroisements = this._centrale.CroisementsParCable.Values.Max(croisements => croisements.Count);
            }

            // Lorsqu'il n'y a plus de croisements, 
            // le résultat est le nombre de câble encore branchés :
            return this._centrale.Cables.Count(c => c.EstBranche);
        }
    }
}
