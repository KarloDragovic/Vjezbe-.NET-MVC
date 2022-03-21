using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vjezba.Model
{
    public class Osoba
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }

        private string _oib;
        public string OIB
        {
            get
            {
                return this._oib;
            }
            set
            {
                if (value.Length != 11 || !value.All(c => c >= 48 && c <= 57))
                {
                    throw new InvalidOperationException("OIB je krivog formata");
                }
                this._oib = value;
            }
        }

        private string _jmbg;
        public string JMBG
        {
            get
            {
                return this._jmbg;
            }
            set
            {
                if (value.Length != 13 || !value.All(c => c >= 48 && c <= 57))
                {
                    throw new InvalidOperationException("JMBG je krivog formata");
                }
                this._jmbg = value;

                int dan = int.Parse(value.Substring(0,2));
                int mjesec = int.Parse(value.Substring(2, 2));
                int godina = 1000 + int.Parse(value.Substring(4, 3));

                this._datumRodjenja = new DateTime(godina, mjesec, dan);
            }
        }

        private DateTime _datumRodjenja;
        public DateTime DatumRodjenja
        {
            get
            {
                return this._datumRodjenja;
            }
        }
    }
}
