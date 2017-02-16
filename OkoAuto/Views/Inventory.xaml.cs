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
using System.IO;
using System.Drawing;
using System.ComponentModel;
using OkoAuto.Logic;
using OkoAuto.Database;
using OkoAuto.Logic.CarClasses;


namespace OkoAuto
{
    /// <summary>
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory : Window
    {
        int pictureIndex = 0;

        private bool isMakeChecked = true;
        public bool IsMakeChecked
        {
            get
            {
                return isMakeChecked;
            }
            set
            {
                //Here you mimic your Toggled event calling what you want!
                this.isMakeChecked = value;
            }
        }

        ApplicationStateManager ASM = new ApplicationStateManager();
        DataPortal dp = new DataPortal();
        CarClass Car = new CarClass();


        public Inventory()
        {
            InitializeComponent();
            this.DataContext = this;
            MakeChecked.IsChecked = IsMakeChecked;
            DataPortal dp = new DataPortal();
            ASM.Initialize();
            CarInventorySearch.ItemsSource = null;
        }

        private void Search_Clicked(object sender, RoutedEventArgs e)
        {
        }

        private void MakeBox_Checked(object sender, RoutedEventArgs e)
        {
        }


        private void MakeBox_Unchecked(object sender, RoutedEventArgs e)
        {
        }

        private void ModelBox_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void ModelBox_Unchecked(object sender, RoutedEventArgs e)
        {
        }

        private void TransmissionBox_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void YearBox_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void YearBox_Unchecked(object sender, RoutedEventArgs e)
        {
        }

        private void TrimBox_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void TrimBox_Unchecked(object sender, RoutedEventArgs e)
        {
        }

        private void TransmissionBox_Unchecked(object sender, RoutedEventArgs e)
        {
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            CarInventorySearch.ItemsSource = ASM.GetCarInventory();
        }

        private void NextPic_Click(object sender, System.EventArgs e)
        {
            pictureIndex++;
            if (pictureIndex >= Car.CarImagesArray.Count)
            {
                pictureIndex = 0;
            }
            loadPic();
        }

        private void PrevPic_Click(object sender, System.EventArgs e)
        {
            pictureIndex--;
            if (pictureIndex < 0 )
            {
                pictureIndex = Car.CarImagesArray.Count-1;
            }
            loadPic();
        }

        private void View_Click(object sender, System.EventArgs e)
        {
            
            Car = (CarClass)CarInventorySearch.SelectedItems[0];
            Car.CarImagesArray = dp.getCarImage(Car);
            loadPic();
        }

        private void loadPic()
        {
            using (MemoryStream imgStream = new MemoryStream(Car.CarImagesArray[pictureIndex]))
            {
                var bi = new BitmapImage();
                bi.BeginInit();
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.StreamSource = imgStream;
                bi.EndInit();
                MainImage.Source = bi;
            }
        }
    }
}
