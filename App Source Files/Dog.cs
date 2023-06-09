using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_DogAdoption
{
    public class Dog
    {
        public string Name { get; set; }
        public string Breed { get; set; }
        public double Weight { get; set; }
        public DateTime DOB { get; set; }
        public string Pic { get; set; }
        public double MonthsInKennel { get; set; }
        public string UpdatedShotsAnswer { get; set; }
        public string Colors { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}\nBreed: {Breed}\nWeight: {Weight}\nDOB: {DOB.ToShortDateString()}\nPic: {Pic}\nMonthsInKennel: {MonthsInKennel}\nHaveShots: {UpdatedShotsAnswer}\nColors: {Colors}";
        }
    }
}
