using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingDojo.S02E11.EDM
{
    public class ElectricianPuzzle_EDM
    {

        /// <summary>
        /// Nombre de câbles.
        /// </summary>
        public int NbCables { get; set; }

        /// <summary>
        /// Nombre de câbles sans croisement.
        /// </summary>
        public int NbCablesMaxSansCroisement
        {
            get { return PositionFinaleCablesList.Count; }
        }
        
        /// <summary>
        /// Position initialie des câbles sur le bornier.
        /// </summary>
        public IEnumerable<Tuple<int, int>> PositionInitialeCablesList { get; set; }

        /// <summary>
        /// Position finale des câbles sur le bornier.
        /// </summary>
        public List<Tuple<int, int>> PositionFinaleCablesList { get; set; }
        

        public void ParseInputFromConsole()
        {
            throw new NotImplementedException();
        }


        public void ParseInput(IEnumerable<Tuple<int, int>> data)
        {
            this.NbCables = data.Count();
            this.PositionInitialeCablesList = data;
            this.PositionFinaleCablesList = new List<Tuple<int, int>>();
        }


        public int ComputeSolution()
        {
            foreach (var positionCable in this.PositionInitialeCablesList)
            {
                if ((!IsCroisement(positionCable)) && (!IsEquals(positionCable))) {

                    this.PositionFinaleCablesList.Add(positionCable);
                }
            }
            
            return this.NbCablesMaxSansCroisement;
        }


        /// <summary>
        /// Test si la position du câble passé en paramètre provoque un croisement.
        /// </summary>
        /// <param name="positionCable">Position du câble testé.</param>
        /// <returns>Retourne true si c'est le cas, false sinon.</returns>
        private Boolean IsCroisement(Tuple<int, int> positionCable)
        {
            foreach (var position in this.PositionInitialeCablesList)
            {
                if ((positionCable.Item1 > position.Item1) && (positionCable.Item2 < position.Item2)) return true;
            }

            return false;
        }

        ///// <summary>
        ///// Test si la position du câble passé en paramètre correspont à la position d'un même câble.
        ///// </summary>
        ///// <param name="positionCable">Position du câble testé.</param>
        ///// <returns>Retourne true si c'est le cas, false sinon.</returns>
        private Boolean IsEquals(Tuple<int, int> positionCable)
        {
            foreach (var position in this.PositionFinaleCablesList)
            {
                if ((positionCable.Item1 == position.Item1) && (positionCable.Item2 == position.Item2)) return true;
            }

            return false;
        } 
    }
}
