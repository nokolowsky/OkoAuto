using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OkoAuto.Logic;
using OkoAuto.Logic.CarClasses;
using OkoAuto.Database;

namespace OkoAuto.Logic
{

    public class ApplicationStateManager
    {
        public List<CarMakeClass> CarMakesList = new List<CarMakeClass>();
        public List<CarModelClass> CarModelList = new List<CarModelClass>();
        public List<CarClass> CarList = new List<CarClass>();
        DataPortal dp = new DataPortal();

        public void Initialize()
        {
            CarMakesList = dp.getCarMakes();
            CarModelList = dp.getCarModels();
            CarList = dp.getCars();

        }


        public List<CarMakeClass> GetCarMakesList()
        {
            return dp.getCarMakes();
        }
        public List<CarModelClass> GetCarModelsList()
        {
            return GetCarModelsList(string.Empty);
        }

        public List<CarModelClass> GetCarModelsList(string Make)
        {
            return dp.getCarModels(Make);
        }

        public List<CarClass> GetCarInventory()
        {
            return dp.getCars();
        }



    }

}
