using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingDojo.S02E11.PCR
{
    public class Centrale
    {
        /// <summary>
        /// Liste des <see cref="Cable"/>s de la <see cref="Centrale"/>.
        /// </summary>
        public ICollection<Cable> Cables
        {
            get { return this.CroisementsParCable.Keys; }
        }

        /// <summary>
        /// Croisements entre les câbles :
        /// Clé : câble.
        /// Valeur : liste de câbles qui se croisent avec le câble de la clé.
        /// </summary>
        public IDictionary<Cable, ICollection<Cable>> CroisementsParCable { get; set; }

        public Centrale(ICollection<Tuple<int, int>> input)
        {
            this.CroisementsParCable = new Dictionary<Cable, ICollection<Cable>>(input.Count);

            foreach (var tuple in input)
            {
                var newCable = new Cable(tuple.Item1, tuple.Item2);
                newCable.Branchement += OnBranchementCable;
                newCable.Debranchement += OnDebranchementCable;
                this.CroisementsParCable.Add(newCable, new List<Cable>(input.Count));
            }
        }

        private void OnBranchementCable(object sender, EventArgs e)
        {
            var cableBranche = sender as Cable;

            if (!this.CroisementsParCable.ContainsKey(cableBranche))
                throw new InvalidOperationException();

            var cablesQuiSeCroisentAvec =
                from c in this.Cables
                where !c.EstIdentiqueA(cableBranche) // Le câble ne peut pas se croiser avec lui-même.
                where c.SeCroiseAvec(cableBranche)
                select c;

            // Mettre à jour la liste des câbles qui croisent le câble qui vient d'être branché :
            this.CroisementsParCable[cableBranche] = cablesQuiSeCroisentAvec.ToList();
        }

        private void OnDebranchementCable(object sender, EventArgs e)
        {
            var cableDebranche = sender as Cable;

            if (!this.CroisementsParCable.ContainsKey(cableDebranche))
                throw new InvalidOperationException();

            // Retirer le câble débranché de la liste des câbles qui le croisent :
            foreach (var cableCroise in this.CroisementsParCable[cableDebranche])
                this.CroisementsParCable[cableCroise].Remove(cableDebranche);

            // Vider la liste des câbles qui croisent le câble débranché :
            this.CroisementsParCable[cableDebranche].Clear();
        }
    }
}
