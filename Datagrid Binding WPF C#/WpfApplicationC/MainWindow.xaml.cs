using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApplicationC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public class User
        {   public string Name { get; set; }

            public int Id { get; set; }

            public int No { get; set; }

            public int No2 { get; set; }

            



        }
        public MainWindow()
        {
            InitializeComponent();
           
            //string[] lines = { "line1", "line2" };

            //DataGridTextColumn col1 = new DataGridTextColumn();
            //dataGrid.Columns.Add(col1);
            //col1.Binding = new Binding(".");

            // dataGrid.Items.Add(lines[0]);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            List<User> users = new List<User>();
            int counter = 0;
            string line;

            // Read the file and display it line by line.

            try
            {
                System.IO.StreamReader file =
                   new System.IO.StreamReader(@"C:\temp\oscourse\360-p6.txt");

                while ((line = file.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    string[] cells1 = line.Split('"');
                    string[] cells = cells1[2].Split(',');
                    Console.Write(cells[0]);
                    int no1 = Int32.Parse(cells[1]);
                    int no2 = Int32.Parse(cells[2]);
                    int no3 = Int32.Parse(cells[3]);
                    users.Add(new User() { Name = cells1[1], No2 = no1, Id = no2, No = no3 });
                    counter++;
                }

                file.Close();

                // Suspend the screen.
                Console.ReadLine();


            }
            catch (IOException ioex)
            {
                throw new IOException("An error occurred while processing the file.", ioex);
            }
            catch (Exception ex)
            {
                throw new Exception("An generic error ocurred.");
            }

            dataGrid.ItemsSource = users;
        }
    }
}
