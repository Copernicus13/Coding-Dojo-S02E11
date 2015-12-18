using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingDojo.S02E11.MBN
{
    public class ElectricianPuzzle
    {

        private List<Tuple<int, int>> _fils;

        public void ParseInputFromConsole()
        {
            throw new NotImplementedException();
        }

        public void ParseInput(IEnumerable<Tuple<int, int>> fils)
        {
            _fils = fils.ToList();
        }

        public int ComputeSolution()
        {
            // Deux listes de fils orientés dans le même sens
            var orientesGauche = _fils.Where(data => data.Item1 <= data.Item2).ToList();
            var orientesDroite = _fils.Where(data => data.Item1 >= data.Item2).ToList();

            // On ordonne la liste orientée à gauche
            var filsOrdonnes = orientesGauche.OrderBy(fil => fil.Item1).ToList();

            // On vire tous ceux qui croisent (ex : 1-3, 2-2)
            SupprimeCroises(filsOrdonnes);

            // On rajoute tous ceux qui croisent pas mais orientés dans l'autre sens (ex : 1-3, 5-4)
            var filsOrientesDifferentsNonCroises = new List<Tuple<int, int>>();
            foreach (var fil in orientesDroite)
            {
                if (!Croise(fil, filsOrdonnes))
                {
                    filsOrientesDifferentsNonCroises.Add(fil);
                }
            }

            // Nombre maxi de fils qui ne se croisent pas dans cette orientation
            var maxiOrientesGauche = filsOrdonnes.Union(filsOrientesDifferentsNonCroises).Distinct().Count();

            // On ordonne la liste orientée à droite
            filsOrdonnes = orientesDroite.OrderBy(fil => fil.Item2).ToList();

            // On vire tous ceux qui croisent (ex : 3-1, 2-2)
            SupprimeCroises(filsOrdonnes);

            // On rajoute tous ceux qui croisent pas mais orientés dans l'autre sens (ex : 3-1, 4-5)
            filsOrientesDifferentsNonCroises.Clear();
            foreach (var fil in orientesGauche)
            {
                if (!Croise(fil, filsOrdonnes))
                {
                    filsOrientesDifferentsNonCroises.Add(fil);
                }
            }

            // Nombre maxi de fils qui ne se croisent pas dans cette orientation
            var maxiOrientesDroite = filsOrdonnes.Union(filsOrientesDifferentsNonCroises).Distinct().Count();

            // Nombre maxi
            return Math.Max(maxiOrientesDroite, maxiOrientesGauche);
        }

        /// <summary>
        /// Est ce que le fil en paramètre croise un fil contenue dans la liste en paramètre
        /// </summary>
        /// <param name="fil"></param>
        /// <param name="autreFils"></param>
        /// <returns></returns>
        private bool Croise(Tuple<int, int> fil, List<Tuple<int, int>> autreFils)
        {
            foreach (var autreFil in autreFils)
            {
                if ((fil.Item1 < autreFil.Item1 && fil.Item2 > autreFil.Item2)
                    || (fil.Item1 > autreFil.Item1 && fil.Item2 < autreFil.Item2))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Supprime les fils croisés de la liste
        /// </summary>
        /// <param name="fils"></param>
        private void SupprimeCroises(List<Tuple<int, int>> fils)
        {
            var listCroisement = new List<KeyValuePair<Tuple<int, int>, int>>();
            foreach (var currentFil in fils)
            {
                var nombreCroisements = 0;
                foreach (var autreFil in fils)
                {
                    if (((currentFil.Item1 < autreFil.Item1 && currentFil.Item2 > autreFil.Item2)
                    || (currentFil.Item1 > autreFil.Item1 && currentFil.Item2 < autreFil.Item2)) && !autreFil.Equals(currentFil))
                    {
                        nombreCroisements++;
                    }
                }
                listCroisement.Add(new KeyValuePair<Tuple<int, int>, int>(currentFil, nombreCroisements));
            }
            if (listCroisement.Count(croisement => croisement.Value > 0) > 0)
            {
                listCroisement = listCroisement.OrderBy(croisement => croisement.Value).ToList();
                fils.Remove(listCroisement.Last().Key);
                SupprimeCroises(fils);
            }
        }
    }
}
