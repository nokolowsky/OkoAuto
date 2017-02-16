using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkoAuto.Logic.CarClasses
{
    public class CarMakeClass
    {
        private string _carMake = string.Empty;
        public string CarMake
        {
            get
            {
                return _carMake;
            }
            set
            {
                _carMake = value;
            }
        }
    }



}
