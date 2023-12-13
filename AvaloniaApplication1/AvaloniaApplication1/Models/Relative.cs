using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication1.Models
{
    public class Relative : Human
    {
        public Relative(Relative relative) 
        {
            //foreach(var prop in relative.GetType().GetProperties())
            //{
            //    prop.

            //}
            FirstName = relative.FirstName;
            LastName = relative.LastName;
            MiddleName = relative.MiddleName;
            Sex = relative.Sex;
            ID = relative.ID;
        }

    }
}
