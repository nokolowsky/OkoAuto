using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;

namespace OkoAuto.Logic.CarClasses
{
    public class CarClass
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

        private List<String> _carImages = new List<String>();
        public List<String> CarImages
        {
            get
            {
                return _carImages;
            }
            set
            {
                _carImages = value;
            }
        }

        private List<byte[]> _carImagesArray = new List<byte[]>();
        public List<byte[]> CarImagesArray
        {
            get
            {
                return _carImagesArray;
            }
            set
            {
                _carImagesArray = value;
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

        private Guid _carModelID = Guid.Empty;
        public Guid CarModelID
        {
            get
            {
                return _carModelID;
            }
            set
            {
                _carModelID = value;
            }
        }

        private Guid _carGuid = Guid.Empty;
        public Guid CarGuid
        {
            get
            {
                return _carGuid;
            }
            set
            {
                _carGuid = value;
            }
        }

        private int _carYear = 2000;
        public int CarYear
        {
            get
            {
                return _carYear;
            }
            set
            {
                _carYear = value;
            }
        }

        private double _carKilometers = 0;
        public double CarKilometers
        {
            get
            {
                return _carKilometers;
            }
            set
            {
                _carKilometers = value;
            }
        }

        private double _carMileage = 0;
        public double CarMileage
        {
            get
            {
                return _carMileage = _carKilometers * 0.62;
            }
        }

        private String _vin = string.Empty;
        public String VIN
        {
            get
            {
                if (_vin == null)
                    return string.Empty;
                else
                    return _vin;
            }
            set
            {
                _vin = value;
            }
        }

        private String _notes = string.Empty;
        public String Notes
        {
            get
            {
                if (_notes == null)
                    return string.Empty;
                else
                    return _notes;
            }
            set
            {
                _notes = value;
            }
        }

        private bool _isAvailable = true;
        public bool IsAvailable
        {
            get
            {
                return _isAvailable;

            }
            set
            {
                _isAvailable = value;
            }
        }


        private DateTime _dateAdded;
        public DateTime DateAdded
        {
            get
            {
                return _dateAdded;

            }
            set
            {
                _dateAdded = value;
            }
        }




    }
}
