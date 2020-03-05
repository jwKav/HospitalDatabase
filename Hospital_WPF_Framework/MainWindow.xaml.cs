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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;
using System.Diagnostics;
using System.Windows.Threading;
using System.Diagnostics;

namespace lab_62_Hospital_WPF_Framework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<Patient> patients = new List<Patient>();
        static Patient patient = new Patient();
        static Uri url = new Uri(@"https://localhost:44312/api/patients/");
        static HttpClient httpClient = new HttpClient();

        

        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            Thread.Sleep(10000);
        }
        private void ListViewPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        async void GetPatientsAsync()
        {
            using (httpClient = new HttpClient())
            {
                var jsonString = await GetPatientDataAsync(url.ToString());
                patients = JsonConvert.DeserializeObject<List<Patient>>(jsonString);
                ListViewPatients.ItemsSource = patients;
            }
            
            
        }
        async Task<string> GetPatientDataAsync(string url)
        {
            string jsonString = null;
            using (httpClient)
            {
                jsonString = await httpClient.GetStringAsync(url);
            }
            return jsonString;
        }

        private void PatientsButton_Click(object sender, RoutedEventArgs e)
        {
            //new Thread(() =>
            //{
            //    Thread.CurrentThread.IsBackground = true;

            //    Dispatcher.BeginInvoke(
            //        new Action(() =>
            //        {
            //            GetPatientsAsync();
            //        })
            //    );
            //}).Start();

            Task.Run(() =>
            {
                Dispatcher.BeginInvoke(
                    new Action(() =>
                    {
                        GetPatientsAsync();
                    })
                );
            });
        }
    }
}
