using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingDojo.S02E11.KGA
{
    public class ElectricianPuzzle
    {
        private IList<Tuple<int, int>> cableList;

        public void ParseInputFromConsole()
        {
            throw new NotImplementedException();
        }

        public void ParseInput(IEnumerable<Tuple<int, int>> data)
        {
            // Les câbles sont triés par leur première extrémité puis par la seconde.
            cableList = data.OrderBy(t => t.Item1).ThenBy(t => t.Item2).ToList();
        }

        public int ComputeSolution()
        {
            int n = cableList.Count();

            var maximumAt = new List<int> {1};

            // On cherche la plus grande suite croissante jusqu'à i.
            for (int i = 1; i < n; i++)
            {
                int maximum = 1;

                for (int j = 0; j < i; j++)
                {
                    // si la seconde extrémité de j est inférieure à celle de i, alors
                    // maximumAt[j] + 1 (la plus grande suite jusqu'à j, plus 1 pour i) peut être une valeur de maximumAt[i]
                    if (cableList[i].Item2 >= cableList[j].Item2)
                    {
                        maximum = Math.Max(maximum, maximumAt[j] + 1);
                    }
                }

                maximumAt.Add(maximum);
            }

            return maximumAt.Max();
        }
    }
}