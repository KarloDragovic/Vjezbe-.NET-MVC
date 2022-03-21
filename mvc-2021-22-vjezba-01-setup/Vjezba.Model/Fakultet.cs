using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vjezba.Model
{
    public class Fakultet
    {
        public List<Osoba> Osobe { get; set; }

        public Fakultet()
        {
            this.Osobe = new List<Osoba>();
        }

        public int KolikoProfesora()
        {
            int brojProfesora= 0;

            foreach (Osoba osoba in Osobe)
            {
                if (osoba.GetType() == typeof(Profesor))
                {
                    brojProfesora++;
                }
            }
            return brojProfesora;
        }
        public int KolikoStudenata()
        {
            int brojStudenata = 0;

            foreach (Osoba osoba in Osobe)
            {
                if (osoba.GetType() == typeof(Student))
                {
                    brojStudenata++;
                }
            }
            return brojStudenata;
        }
        public Student DohvatiStudenta(string jmbag)
        {
            List<Student> students = new List<Student>();
            foreach (Osoba osoba in Osobe)
            {
                if (typeof(Student) == osoba.GetType())
                {
                    students.Add((Student)osoba);
                }
            }
            foreach (Student student in students)
            {
                if (String.Equals(student.JMBAG, jmbag))
                {
                    return student;
                }
            }

            return null;
        }

        public IEnumerable<Profesor> DohvatiProfesore()
        {
            List<Profesor> profesori = new List<Profesor>();
            foreach (Osoba osoba in Osobe)
            {
                if (typeof(Profesor) == osoba.GetType())
                {
                    profesori.Add((Profesor)osoba);
                }
            }

            profesori.Sort(delegate (Profesor x, Profesor y)
            {
                return DateTime.Compare(x.DatumIzbora, y.DatumIzbora);
            });

            return profesori;
        }

        public IEnumerable<Student> DohvatiStudente91()
        {
            List<Student> studenti = new List<Student>();
            foreach (Osoba osoba in Osobe)
            {
                if (typeof(Student) == osoba.GetType())
                {
                    studenti.Add((Student)osoba);
                }
            }

            return studenti.Where(s => { return s.DatumRodjenja.Year > 1991; });
        }

        public IEnumerable<Student> DohvatiStudente91NoLinq()
        {
            List<Student> studenti = new List<Student>();
            foreach (Osoba osoba in Osobe)
            {
                if (typeof(Student) == osoba.GetType())
                {
                    studenti.Add((Student)osoba);
                }
            }

            return studenti.FindAll(s => { return s.DatumRodjenja.Year > 1991; });
        }

        public List<Student> StudentiNeTvzD()
        {
            List<Student> studenti = new List<Student>();
            foreach (Osoba osoba in Osobe)
            {
                if (typeof(Student) == osoba.GetType())
                {
                    studenti.Add((Student)osoba);
                }
            }

            return studenti.FindAll(s =>
            {
                return !s.JMBAG.StartsWith("0246") && s.Prezime.StartsWith("D");
            });
        }

        public List<Student> DohvatiStudente91List()
        {
            List<Student> studenti = new List<Student>();
            foreach (Osoba osoba in Osobe)
            {
                if (typeof(Student) == osoba.GetType())
                {
                    studenti.Add((Student)osoba);
                }
            }

            return studenti.FindAll(s => { return s.DatumRodjenja.Year > 1991; });
        }

        public Student NajboljiProsjek(int god)
        {
            List<Student> studenti = new List<Student>();
            foreach (Osoba osoba in Osobe)
            {
                if (typeof(Student) == osoba.GetType())
                {
                    studenti.Add((Student)osoba);
                }
            }

            studenti.Sort(delegate (Student x, Student y)
            {
                return y.Prosjek.CompareTo(x.Prosjek);
            });

            return studenti.FirstOrDefault(s => 
            {
                return s.DatumRodjenja.Year == god;    
            });
        }

        public List<Student> StudentiGodinaOrdered(int god)
        {
            List<Student> studenti = new List<Student>();
            foreach (Osoba osoba in Osobe)
            {
                if (typeof(Student) == osoba.GetType() && osoba.DatumRodjenja.Year == god)
                {
                    studenti.Add((Student)osoba);
                }
            }

            studenti.Sort(delegate (Student x, Student y)
            {
                return y.Prosjek.CompareTo(x.Prosjek);
            });

            return studenti;
        }

        public List<Profesor> SviProfesori(bool asc)
        {
            List<Profesor> profesori = new List<Profesor>();
            foreach (Osoba osoba in Osobe)
            {
                if (typeof(Profesor) == osoba.GetType())
                {
                    profesori.Add((Profesor)osoba);
                }
            }

            profesori.Sort(delegate (Profesor x, Profesor y)
            {
                if (String.Compare(x.Prezime, y.Prezime) == 0)
                {
                    return String.Compare(x.Ime, y.Ime);
                }
                else
                {
                    return String.Compare(x.Prezime, y.Prezime);
                }
            });

            if (asc)
            {
                return profesori;
            }
            else
            {
                profesori.Reverse();
                return profesori;
            }
        }

        public int KolikoProfesoraUZvanju(Zvanje zvanje)
        {
            List<Profesor> profesori = new List<Profesor>();
            int brojProfesora = 0;
            foreach (Osoba osoba in Osobe)
            {
                if (typeof(Profesor) == osoba.GetType())
                {
                    profesori.Add((Profesor)osoba);
                }
            }

            foreach (var p in profesori)
            {
                if (p.Zvanje == zvanje)
                {
                    brojProfesora++;
                }
            }

            return brojProfesora;
        }

        public List<Profesor> NeaktivniProfesori(int x)
        {
            List<Profesor> profesori = new List<Profesor>();
            foreach (Osoba osoba in Osobe)
            {
                if (typeof(Profesor) == osoba.GetType())
                {
                    profesori.Add((Profesor)osoba);
                }
            }

            return profesori.FindAll(p => 
            {
                if(p.Predmeti.Count < x && (p.Zvanje.Equals(Zvanje.VisiPredavac) || p.Zvanje.Equals(Zvanje.Predavac)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }

        public List<Profesor> AktivniAsistenti(int x, int minEcts)
        {
            List<Profesor> profesori = new List<Profesor>();
            int brPredmeta = 0;
            foreach (Osoba osoba in Osobe)
            {
                if (typeof(Profesor) == osoba.GetType())
                {
                    profesori.Add((Profesor)osoba);
                }
            }

            return profesori.FindAll(p =>
            {
                if (p.Zvanje.Equals(Zvanje.Asistent))
                {
                    foreach (Predmet predmet in p.Predmeti)
                    {
                        if (predmet.ECTS >= minEcts)
                        {
                            brPredmeta++;
                        }
                    }

                    return brPredmeta > x;
                }
                else
                {
                    return false;
                }
            });
        }

        public void IzmjeniProfesore(Action<Profesor> action)
        {
            List<Profesor> profesori = new List<Profesor>();
            foreach (Osoba osoba in Osobe)
            {
                if (typeof(Profesor) == osoba.GetType())
                {
                    action((Profesor)osoba);
                    profesori.Add((Profesor)osoba);
                }
            }
        }
    }
}
