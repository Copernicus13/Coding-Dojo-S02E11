using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingDojo.S02E11.MBN
{
    public class ElectricianPuzzle
    {

        private List<Tuple<int, int>> _data;

        public void ParseInputFromConsole()
        {
            throw new NotImplementedException();
        }

        public void ParseInput(IEnumerable<Tuple<int, int>> data)
        {
            _data = data.ToList();
        }

        public int ComputeSolution()
        {
            //var result = 0;
            var countLeft = _data.Where(data => data.Item1 <= data.Item2).ToList();
            var countRight = _data.Where(data => data.Item1 >= data.Item2).ToList();

            List<Tuple<int, int>> goodData = new List<Tuple<int, int>>();

            // nombre maxi de fils type (1-2, 2-3, 3-3)
            List<Tuple<int, int>> croisedData = new List<Tuple<int, int>>();
            if (countLeft.Count() > countRight.Count())
            {
                goodData = countLeft.OrderBy(data => data.Item1).ToList();

                foreach (var data in _data)
                {
                    foreach (var dataBis in _data)
                    {
                        if (data.Item1 > dataBis.Item1 && data.Item2 < dataBis.Item2 && data != dataBis)
                        {
                            croisedData.Add(data);
                        }
                    }
                }

                // On vire tous ceux qui croisent encore (1-3, 2-2)
                List<Tuple<int, int>> removeData = new List<Tuple<int, int>>();
                foreach (var data in countLeft)
                {
                    removeData.AddRange(countLeft.Where(dataBis => dataBis.Item1 > data.Item1 && dataBis.Item2 < data.Item2));
                }

                // On rajoute tous ceux qui croisent pas avant (2-3, 2-1)
                List<Tuple<int, int>> stillGoodData = new List<Tuple<int, int>>();
                foreach (var data in countLeft)
                {
                    stillGoodData.AddRange(_data.Where(dataBis => dataBis.Item1 <= data.Item1 && dataBis.Item2 < dataBis.Item1
                        && !croisedData.Contains(dataBis)));
                }

                return goodData.Count - removeData.Distinct().Count() + stillGoodData.Distinct().Count();
            }
            else
            {
                goodData = countRight.OrderBy(data => data.Item2).ToList();
                
                foreach (var data in _data)
                {
                    foreach (var dataBis in _data)
                    {
                        if (data.Item1 > dataBis.Item1 && data.Item2 < dataBis.Item2 && data != dataBis)
                        {
                            croisedData.Add(dataBis);
                        }
                    }
                }

                // On vire tous ceux qui croisent encore (1-3, 2-2)
                List<Tuple<int, int>> removeData = new List<Tuple<int, int>>();
                foreach (var data in countLeft)
                {
                    removeData.AddRange(countLeft.Where(dataBis => dataBis.Item1 > data.Item1 && dataBis.Item2 < data.Item2));
                }

                // On rajoute tous ceux qui croisent pas avant (2-3, 2-1)
                List<Tuple<int, int>> stillGoodData = new List<Tuple<int, int>>();
                foreach (var data in countLeft)
                {
                    stillGoodData.AddRange(_data.Where(dataBis => dataBis.Item1 <= data.Item1 && dataBis.Item2 < dataBis.Item1
                        && !croisedData.Contains(dataBis)));
                }

                return goodData.Count - removeData.Distinct().Count() + stillGoodData.Distinct().Count();
            }
        }
    }
}
