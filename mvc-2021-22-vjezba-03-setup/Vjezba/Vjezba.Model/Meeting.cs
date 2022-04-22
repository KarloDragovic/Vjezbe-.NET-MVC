using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vjezba.Model
{
    public enum TipSastanka
    {
        InPerson, VideoCall
    }
    public enum Status
    {
        Scheduled, Cancelled
    }
    public class Meeting
    {
        [Key]
        public int Id { get; set; }
        public DateTime? Pocetak { get; set; }
        public DateTime? Kraj { get; set; }
        public TipSastanka TipSastanka { get; set; }
        public Status Status { get; set; }
        public string Lokacija { get; set; }
        public string Komentari { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }

    }
}
