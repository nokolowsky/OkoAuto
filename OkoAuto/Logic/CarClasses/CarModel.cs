using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkoAuto.Logic.CarClasses
{
    public class CarModelClass
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

        private string _carModel = string.Empty;
        public string CarModel
        {
            get
            {
                return _carModel;
            }
            set
            {
                _carModel = value;
            }
        }
    }
}
