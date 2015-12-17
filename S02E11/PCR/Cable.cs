using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CodingDojo.S02E11.PCR
{
    /// <summary>
    /// Représente un câble avec une position de départ et une position d'arrivée.
    /// </summary>
    public class Cable
    {
        /// <summary>
        /// Position sur le bornier de départ.
        /// </summary>
        public int PositionDepart { get; set; }

        /// <summary>
        /// Position sur le bornier d'arrivée.
        /// </summary>
        public int PositionArrivee { get; set; }


        public int NombreCroisements { get; set; }

        public Cable(int positionDepart, int positionArrivee)
        {
            this.PositionDepart = positionDepart;
            this.PositionArrivee = positionArrivee;
        }

        public Cable(Tuple<int, int> tuple)
        {
            this.PositionDepart = tuple.Item1;
            this.PositionArrivee = tuple.Item2;
        }

        public bool EstIdentiqueAvec(Cable cable)
        {
            return this.PositionDepart == cable.PositionDepart && this.PositionArrivee == cable.PositionArrivee;
        }

        /// <summary>
        /// Indique si le câble spécifié 
        /// NE SE CROISE PAS 
        /// avec le câble courant.
        /// </summary>
        /// <param name="cable"><see cref="Cable"/>.</param>
        /// <returns>Vrai, si les câbles ne se croisent pas. Faux, sinon.</returns>
        public bool NeSeCroisePasAvec(Cable cable)
        {
            return !this.SeCroiseAvec(cable);
        }

        /// <summary>
        /// Indique si le câble spécifié 
        /// SE CROISE 
        /// avec le câble courant.
        /// </summary>
        /// <param name="cable"><see cref="Cable"/>.</param>
        /// <returns>Vrai, si les câbles se croisent. Faux, sinon.</returns>
        public bool SeCroiseAvec(Cable cable)
        {
            if (this.EstIdentiqueAvec(cable))
                throw new NotSupportedException("Le câble spécifié est identique au câble courant.");

            return (this.PositionDepart < cable.PositionDepart && this.PositionArrivee > cable.PositionArrivee) ||
                   (this.PositionDepart > cable.PositionDepart && this.PositionArrivee < cable.PositionArrivee);
        }

        /// <summary>
        /// Retourne le nombre de câbles qui se croisent avec le câble courant.
        /// </summary>
        /// <param name="cables">Liste de <see cref="Cable"/>.</param>
        /// <returns>Le nombre de câbles qui se croisent avec le câble courant.</returns>
        public int SeCroiseAvec(IEnumerable<Cable> cables)
        {
            // ReSharper disable once ConvertClosureToMethodGroup
            return cables.Where(c => !this.EstIdentiqueAvec(c)).Count(c => this.SeCroiseAvec(c));
        }


        public static bool OntDesCablesQuiSeCroisent(IList<Cable> cables)
        {
            foreach (var cable in cables)
            {
                if (cable.SeCroiseAvec(cables.Where(c => c.EstIdentiqueAvec(cable))) > 0)
                    return true;
            }
            return false;
        }
    }
}