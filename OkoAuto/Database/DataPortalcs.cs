using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using OkoAuto.Logic.CarClasses;
using System.IO;


namespace OkoAuto.Database
{
    public class DataPortal
    {

        public bool authenticateLogin(string UserName, string Password)
        {
            string queryString = string.Format("SELECT Username,Password From account Where Username = '{0}' AND Password = '{1}'", UserName, Password);
            string connectionString = "Database=OkoAuto_1_0;Server=Neil-PC\\SQLEXPRESS;Integrated Security=True;connect timeout = 30";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        if (reader["Username"].ToString() == UserName && reader["Password"].ToString() == Password)
                        {
                            return true;
                        }
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            return false;
        }

        public List<byte[]> getCarImage(CarClass Car)
        {
            string connectionString = "Database=OkoAuto_1_0;Server=Neil-PC\\SQLEXPRESS;Integrated Security=True;connect timeout = 30";
            List<byte[]> images = new List<byte[]>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(string.Format("SELECT Image From CarImages Where CarID = '{0}'",Car.CarGuid), connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        byte[] image = (byte[])(reader["image"]);                       
                        images.Add(image);            
                    }
                }
                finally
                {
                    reader.Close();
                    
                }
                return images;
            }





        }

        public void AddCar(CarClass Car)
        {
           
            string connectionString = "Database=OkoAuto_1_0;Server=Neil-PC\\SQLEXPRESS;Integrated Security=True;connect timeout = 30";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("AddNewCar", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Make", Car.CarMake);
                command.Parameters.AddWithValue("@Model", Car.CarModel);
                command.Parameters.AddWithValue("@Year", Car.CarYear);
                command.Parameters.AddWithValue("@VIN", Car.VIN);
                command.Parameters.AddWithValue("@Notes", Car.Notes);
                command.Parameters.AddWithValue("@IsAvailable", Car.IsAvailable);
                command.Parameters.AddWithValue("@Kilometers", Car.CarKilometers);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Car.CarModelID = (Guid)(reader["CarID"]);
                    }
                }
                finally
                {
                    reader.Close();

                    foreach (string s in Car.CarImages)
                    {
                        command = new SqlCommand("AddNewCarImages", connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        FileStream stream = new FileStream(s, FileMode.Open, FileAccess.Read);
                        BinaryReader photoReader = new BinaryReader(stream);
                        byte[] photo = photoReader.ReadBytes((int)stream.Length);
                        command.Parameters.AddWithValue("@images", photo);
                        command.Parameters.AddWithValue("@CarID", Car.CarModelID);
                        reader = command.ExecuteReader();
                        reader.Close();
                    }
                    

                }
            }

        }
    
        public List<CarModelClass> getCarModels(string Make)
        {
            
            List<CarModelClass> CarModels = new List<CarModelClass>();
            string queryString = string.Empty;
            if (Make.Equals(string.Empty))
            {
                queryString = "SELECT Model From CarModel";
            }
            else
            {
                queryString = string.Format("SELECT Model From CarModel Inner join CarMake on CarMake.ID = CarModel.CarMakeID where CarMake.Make = '{0}'", Make);
            }
            queryString += " Order By Model ASC";

            string connectionString = "Database=OkoAuto_1_0;Server=Neil-PC\\SQLEXPRESS;Integrated Security=True;connect timeout = 30";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        CarModelClass CarModel = new CarModelClass();
                        CarModel.CarModel = reader["Model"].ToString();
                        CarModels.Add(CarModel);
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();

                }
                return CarModels;
                }
            
        }
        public List<CarModelClass> getCarModels()
        {
            return getCarModels(string.Empty);
        }

        public List<CarClass> getCars()
        {
            List<CarClass> Cars = new List<CarClass>();


            string queryString = string.Format("SELECT CarMake.Make AS Make,CarModel.Model As Model,Car.ID As CarID,Car.Year as Year, Car.VIN as VIN, Car.Notes AS Notes, Car.IsAvailable as IsAvailable, Car.Kilometers as Kilometers, car.EntryDate as EntryDate From Car Inner join carmodel on car.CarModelId = Carmodel.id Inner join CarMake on CarMake.ID = CarModel.CarMakeID");
            queryString += " Order By Make ASC";
            string connectionString = "Database=OkoAuto_1_0;Server=Neil-PC\\SQLEXPRESS;Integrated Security=True;connect timeout = 30";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        CarClass Car = new CarClass();
                        Car.CarMake = reader["Make"].ToString().Trim();
                        Car.CarModel = reader["Model"].ToString().Trim();
                        Car.CarYear = Int32.Parse(reader["Year"].ToString().Trim());
                        Car.CarKilometers = Int32.Parse(reader["Kilometers"].ToString().Trim());
                        Car.VIN = reader["VIN"].ToString().Trim();
                        Car.Notes = reader["Notes"].ToString().Trim();
                        Car.DateAdded = (DateTime)reader["EntryDate"];
                        Car.IsAvailable = (bool)reader["IsAvailable"];
                        Car.CarGuid = (Guid)reader["CarID"];
                        Cars.Add(Car);
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();

                }
                return Cars;
            }
        }


        public List<CarMakeClass> getCarMakes()
        {
            List<CarMakeClass> CarMakes = new List<CarMakeClass>();
            

            string queryString = string.Format("SELECT Make From CarMake");
            queryString += " Order By Make ASC";
            string connectionString = "Database=OkoAuto_1_0;Server=Neil-PC\\SQLEXPRESS;Integrated Security=True;connect timeout = 30";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        CarMakeClass CarMake = new CarMakeClass();
                        CarMake.CarMake = reader["Make"].ToString();
                        CarMakes.Add(CarMake);
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                    
                }
                return CarMakes;
            }
        }

    }
}

