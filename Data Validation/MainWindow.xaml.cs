//Ryan Scott - 40533651@live.napier.ac.uk
//Software Engineering - Practical 1
//Visual Studio Modelling Projects

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

namespace Data_Validation
{
    public partial class MainWindow : Window
    {

        //variable being used to store file path
        string path = @"C:\UNI\Year 3\Software Engineering 3 - SET09102\Practicals\Practical 1 - Intro and WPF\Data Validation\data.txt";
        Person[] people = new Person[0];
        bool fileOpen = false;

        List<Person> peopleList = new List<Person>();

        //current and max are used for displaying the people in the list
        int current;
        int max;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_load_Click(object sender, RoutedEventArgs e)
        {
            if (fileOpen == false) //first check is to determine if the file has been opened yet or not
            { 
                
                List<string> lines = File.ReadAllLines(path).ToList();
                
                foreach (var line in lines) //For every line we do the following:
                {
                    string[] entries = line.Split(','); //Split the line into an array by comma (ryan, scott bevcomes [0] = ryan and [1] = scott).
                    Person newPerson = new Person(); //creating a new person


                    //loop to store data as an object (a person)
                    int count = 0;
                    if (entries.Length == 3)
                    {
                        newPerson.name = entries[0];
                        newPerson.age = Int32.Parse(entries[1]);
                        newPerson.address = entries[2];

                        peopleList.Add(newPerson);
                        count++;
                    }
                }

                //changing booean value to true for file being open
                fileOpen = true;
                txt_output.Text = $"File has been read from location: {path}";
                people = peopleList.ToArray();

                max = people.Length;
                current = 1;

                lbl_counter.Content = current + "/" + max;

                lbl_output_name.Content = people[current - 1].name;
                lbl_output_age.Content = people[current - 1].age;
                lbl_output_address.Content = people[current - 1].address;  
            }
            else 
            {
                //will print if the file is already loaded and the user clicks the load button again
                txt_output.Text = "File already loaded";
            }
        }

        public void btn_save_Click(object sender, RoutedEventArgs e)
        {
            txt_output.Text = "";

            string name = "";
            int age = 0;
            string address = "";

            if (fileOpen == false)
            {
                txt_output.Text = "A file needs to be open in order to save a person, please load a file";
            }
            else 
            {
                List<string> errors = new List<String>();

                //Check to see if the user has entered a name
                if (String.IsNullOrEmpty(txt_name.Text))
                {
                    errors.Add("Name cannot be left empty, please enter name");
                }
                else
                {
                    name = txt_name.Text;
                }

                //check to see if address has entered an address
                if (String.IsNullOrEmpty(txt_address.Text))
                {
                    errors.Add("Address cannot be left empty, please enter name");
                }
                else
                {
                    address = txt_address.Text;
                }

                //check to see if user has entered an age
                bool isNumber = int.TryParse(txt_age.Text, out _);
                if (String.IsNullOrEmpty(txt_age.Text))
                {
                    errors.Add("Age cannot be left empty, please enter age");
                }
                else
                {
                    //check to see if what the user has entered is a number
                    if (isNumber == false)
                    {
                        errors.Add("Age must be a number");
                    }
                    else
                    {
                        //check to see if the number is in the right range
                        if (Int32.Parse(txt_age.Text) < 100 && Int32.Parse(txt_age.Text) > 0)
                        {
                            age = Int32.Parse(txt_age.Text);
                        }
                        else
                        {
                            errors.Add("Age must be between 0 and 100");
                        }
                        
                    }
                }

                //if there were no errors in the data vallidation checks then use the data
                if (errors.Count == 0)
                {

                    //Using the data to create a new object (person)
                    Person newPerson = new Person();

                    newPerson.name = name;
                    newPerson.age = age;
                    newPerson.address = address;

                    //person is added to the list of people
                    peopleList.Add(newPerson);

                    //all people are added to a list in the format (name,age,address)
                    List<string> output = new List<string>();
                    foreach (var person in peopleList)
                    {
                        output.Add($"{person.name},{person.age},{person.address}");
                    }

                    //output list is written to the file
                    File.WriteAllLines(path, output);

                    //all data is cleared
                    txt_name.Text = null;
                    txt_age.Text = null;
                    txt_address.Text = null;
                    people = null;
                    fileOpen = false;
                    lbl_counter.Content = "";
                    lbl_output_name.Content = "";
                    lbl_output_age.Content = "";
                    lbl_output_address.Content = "";
                    people = null;
                    peopleList.Clear();

                    //message displayed to user informing them of the success
                    txt_output.Text = ("File has been saved and closed");

                }
                else
                {
                    foreach(var error in errors)
                    {
                        //if any errors occured they are displayed
                        txt_output.AppendText("\n" + error);
                    }
                }
                
            }
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            txt_name.Text = null;
            txt_age.Text = null;
            txt_address.Text = null;
        }

        private void btn_bck_Click(object sender, RoutedEventArgs e)
        {
            if (fileOpen == false)
            {

            }
            else
            {
                if (current == 1)
                {
                    
                }
                else
                {
                    current--;

                    lbl_counter.Content = current + "/" + max;

                    lbl_output_name.Content = people[current - 1].name;
                    lbl_output_age.Content = people[current - 1].age;
                    lbl_output_address.Content = people[current - 1].address;
                }
                

            }
        }

        private void btn_nxt_Click(object sender, RoutedEventArgs e)
        {
            if (fileOpen == false)
            {

            }
            else
            {
                if (current == max)
                {

                }
                else 
                {
                    current++;

                    lbl_counter.Content = current + "/" + max;

                    lbl_output_name.Content = people[current - 1].name;
                    lbl_output_age.Content = people[current - 1].age;
                    lbl_output_address.Content = people[current - 1].address;
                }
                
                
            }
        }
    }
}
