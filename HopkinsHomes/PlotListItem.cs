using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace HopkinsHomes
{
    class PlotListItem
    {
        public int Id { get; set; }
        public string HouseType { get; set; }
        public string Beds { get; set; }
        public string Garaging { get; set; }
        public string Status { get; set; }
        public int Value => int.TryParse(new string(Status.Where(char.IsDigit).ToArray()), out int result) ? result : -1;
    }
}
