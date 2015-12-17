using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CodingDojo.S02E11.PCR
{
    /// <summary>
    /// Représente un câble avec une position de départ et une position d'arrivée.
    /// </summary>
    [DebuggerDisplay("{ToDebugString()}")]
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

        /// <summary>
        /// Indique si le câble est branché.
        /// </summary>
        public bool EstBranche { get; private set; }

        public event EventHandler Branchement;
        public event EventHandler Debranchement;

        public Cable(int positionDepart, int positionArrivee)
        {
            this.PositionDepart = positionDepart;
            this.PositionArrivee = positionArrivee;
        }

        // Wrap event invocations inside a protected virtual method
        // to allow derived classes to override the event invocation behavior
        protected virtual void OnBranchement(EventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler handler = Branchement;

            // Event will be null if there are no subscribers
            if (handler != null)
            {
                // Use the () operator to raise the event.
                handler(this, e);
            }
        }

        // Wrap event invocations inside a protected virtual method
        // to allow derived classes to override the event invocation behavior
        protected virtual void OnDebranchement(EventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler handler = Debranchement;

            // Event will be null if there are no subscribers
            if (handler != null)
            {
                // Use the () operator to raise the event.
                handler(this, e);
            }
        }

        public void Brancher()
        {
            this.OnBranchement(EventArgs.Empty);
            this.EstBranche = true;
        }

        public void Debrancher()
        {
            this.OnDebranchement(EventArgs.Empty);
            this.EstBranche = false;
        }

        public bool EstIdentiqueA(Cable cable)
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
            if (this.EstIdentiqueA(cable))
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
            return cables.Where(c => !this.EstIdentiqueA(c)).Count(c => this.SeCroiseAvec(c));
        }
        
        private string ToDebugString()
        {
            return string.Format
                (
                    "{0:0000000}{1}{2:0000000}",
                    this.PositionDepart,
                    this.EstBranche ? "-" : string.Empty,
                    this.PositionArrivee
                );
        }
    }
}