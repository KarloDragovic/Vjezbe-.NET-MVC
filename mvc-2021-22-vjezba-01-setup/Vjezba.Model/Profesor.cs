using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vjezba.Model
{
    public enum Zvanje
    {
        Asistent,
        Predavac,
        VisiPredavac,
        ProfVisokeSkole
    }

    public class Profesor : Osoba
    {
        public string Odjel { get; set; }
        public DateTime DatumIzbora { get; set; }
        public Zvanje Zvanje { get; set; }
        public List<Predmet> Predmeti { get; set; }

        public Profesor()
        {
            this.Predmeti = new List<Predmet>();
        }

        public int KolikoDoReizbora()
        {
            if(this.Zvanje == Zvanje.Asistent)
            {
                return 4 - (DateTime.Today.Year - DatumIzbora.Year);
            }
            else
            {
                return 5 - (DateTime.Today.Year - DatumIzbora.Year);
            } 
        }
    }
}
