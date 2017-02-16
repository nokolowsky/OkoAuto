using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OkoAuto.Logic;
using OkoAuto.Database;
using OkoAuto.Logic.CarClasses;
using System.Text.RegularExpressions;
using System.IO;


namespace OkoAuto
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class AddCar : Window
    {
        ApplicationStateManager ASM = new ApplicationStateManager();
        DataPortal dp = new DataPortal();
        List<string> droppedFiles = new List<String>();
        int pictureIndex = 0;
        public AddCar()
        {
            InitializeComponent();
            ASM.Initialize();
            Makes.ItemsSource = ASM.GetCarMakesList();
            Models.ItemsSource = ASM.GetCarModelsList();


        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Models_SelectionChanged(object sender, System.EventArgs e)
        {

        }

        private void KilometersChanged(object sender, System.EventArgs e)
        {
            if (this.Kilometers.IsFocused && !this.Kilometers.Text.Equals(string.Empty))
            {
                this.Mileage.Text = (Double.Parse(this.Kilometers.Text) * 0.621371).ToString().Split('.')[0];
            }
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {



            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                foreach (string s in e.Data.GetData(DataFormats.FileDrop, true) as string[])
                {
                    if (!droppedFiles.Contains(s))
                    {
                        pictureIndex = droppedFiles.Count();
                        droppedFiles.Add(s);
                    }
                }
            }

            if ((null == droppedFiles) || (!droppedFiles.Any())) { return; }
            else if(DeletePic.Visibility == Visibility.Hidden)
            {
                DeletePic.Visibility = Visibility.Visible;
            }
            //      listFiles.Items.Clear();




            //listFiles.Items.Add(s);

            loadPics();

        }


        private void savePics(CarClass Car)
        {

            foreach (string s in droppedFiles)
            {
                Car.CarImages.Add(s);
            }

            droppedFiles = new List<string>();
        }
        private void loadPics()
        {
            if (droppedFiles.Count() == 0)
            {
                ImageLeft.Source = ImageRight.Source = MainImage.Source = null;
            }
            else
            {
                for (int i = pictureIndex - 1; i <= pictureIndex + 1; i++)
                {
                    int index = i % droppedFiles.Count() < 0 ? (i % droppedFiles.Count()) + droppedFiles.Count() : i % droppedFiles.Count();
                    string selectedFileName = droppedFiles[index];
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(selectedFileName);
                    bitmap.EndInit();
                    if (i == pictureIndex - 1)
                    {
                        ImageLeft.Source = bitmap;
                    }
                    else if (i == pictureIndex + 1)
                    {
                        ImageRight.Source = bitmap;
                    }
                    else
                    {
                        MainImage.Source = bitmap;
                    }
                }
            }
        }

        private void NotesChanged(object sender, System.EventArgs e)
        {
            Notes.Focus();
            Notes.CaretIndex = Notes.Text.Length;
            Notes.ScrollToEnd();
        }

        

        private void MileageChanged(object sender, System.EventArgs e)
        {
            if (this.Mileage.IsFocused && !this.Mileage.Text.Equals(string.Empty)) 
            {
                this.Kilometers.Text = (Double.Parse(this.Mileage.Text) * 1.60934).ToString().Split('.')[0];
            }
        }

        private void Makes_SelectionChanged(object sender,System.EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string Make = Makes.SelectedValue.ToString().Trim();
            Models.ItemsSource = ASM.GetCarModelsList(Make);
        }

        private void NextPic_Click(object sender, System.EventArgs e)
        {
            pictureIndex = ++pictureIndex % droppedFiles.Count();
            loadPics();
        }

        private void PrevPic_Click(object sender, System.EventArgs e)
        {
            pictureIndex = --pictureIndex % droppedFiles.Count();
            loadPics();
        }

        private void DelPic_Click(object sender, System.EventArgs e)
        {
            int index = pictureIndex % droppedFiles.Count() < 0 ? (pictureIndex % droppedFiles.Count()) + droppedFiles.Count() : pictureIndex % droppedFiles.Count();
            droppedFiles.RemoveAt(index);
            if(droppedFiles.Count() == 0)
            {
                DeletePic.Visibility = Visibility.Hidden;
            }
            loadPics();
        }

        private void AddCar_Click(object sender, System.EventArgs e)
        {
            CarClass Car = new CarClass();
            Car.CarMake = Makes.SelectedValue.ToString().Trim();
            Car.CarModel = Models.SelectedValue.ToString().Trim();
            Car.CarYear = Int32.Parse(this.Year.Text);
            Car.VIN = Vin.Text.ToString().Trim();
            Car.CarKilometers = Double.Parse(this.Kilometers.Text);
            Car.Notes = Notes.Text.ToString().Trim();
            Car.IsAvailable = true;
            savePics(Car);    
            dp.AddCar(Car);
        }

        private void CarSpecs_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
