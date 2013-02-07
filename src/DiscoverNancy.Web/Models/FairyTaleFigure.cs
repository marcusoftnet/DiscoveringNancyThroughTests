using System.Collections.Generic;

namespace DiscoverNancy.Web.Models
{
    // Model
    public class FairyTaleFigure
    {
        public FairyTaleFigure()
        {
            Name = string.Empty;
            Evil = false;
            Hangarounds = new List<FairyTaleFigure>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public bool Evil { get; set; }
        public IList<FairyTaleFigure> Hangarounds { get; set; }
    }
}