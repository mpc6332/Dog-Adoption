using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Lab_DogAdoption;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly List<Dog> dogs = new();
    private readonly string fileName = "TextFileAssets/Lab_DogAdoption.txt";

    public MainWindow()
    {
        InitializeComponent();
    }

    // Exits out of the program.
    private void btn_exit_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    // Method that reads through each line of a text file upon loading up the window
    private void wdw_Main_Loaded(object sender, RoutedEventArgs e)
    {
        using (var reader = new StreamReader(fileName))
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                // Splits the text into multiple fields by using the comma as a delimiter.
                var fields = line.Split(',');

                // Trim the fields to remove any unwanted spaces.
                for (var i = 0; i < fields.Length; i++) fields[i] = fields[i].Trim();

                // If the string does have 8 fields, continue reading the line.
                if (fields.Length == 8)
                {
                    var name = fields[0].Trim();
                    var breed = fields[1].Trim();
                    double weight;

                    // If the weight is a double, continue reading the line.
                    if (double.TryParse(fields[2], out weight))
                    {
                        DateTime dob;

                        // If the date provided is in proper format, continue reading the line.
                        if (DateTime.TryParseExact(fields[3], "M/d/yyyy", CultureInfo.InvariantCulture,
                                DateTimeStyles.None, out dob))
                        {
                            var pic = fields[4].Trim();

                            // If the end of the pic string ends with the ".jpg" extension, continue reading the line.
                            if (pic.EndsWith(".jpg") || pic.EndsWith(".JPG"))
                            {
                                double monthsInKennel; // This is

                                // If the monthsInKennel is a double, continue reading the line.
                                if (double.TryParse(fields[5], out monthsInKennel))
                                {
                                    bool haveShots;

                                    // If the state of "hasShots" is set to either true or false, continue reading the line .
                                    if (bool.TryParse(fields[6], out haveShots))
                                    {
                                        string updatedShotsAnswer;

                                        // Converts the "True" status of "haveShots" to "Yes".
                                        if (haveShots)
                                            updatedShotsAnswer = "Yes";
                                        else
                                            updatedShotsAnswer = "No";

                                        var colors = fields[7].Trim();

                                        // Create a new Dog object and set its properties based on the parsed fields.
                                        var dog = new Dog
                                        {
                                            Name = name,
                                            Breed = breed,
                                            Weight = weight,
                                            DOB = dob,
                                            Pic = pic,
                                            MonthsInKennel = monthsInKennel,
                                            UpdatedShotsAnswer = updatedShotsAnswer,
                                            Colors = colors
                                        };

                                        // Add the Dog object to a list.
                                        dogs.Add(dog);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Iterate over the dogs list and append the name associated with each one to the listbox
            foreach (var dog in dogs) lstbox_dogs.Items.Add(dog.Name);

            // First index of the list box is selected by default upon loading up the window
            lstbox_dogs.SelectedIndex = 0;
        }
    }

    // This method loads up all of the other data associated with a dog into other controls, such as a text box and image
    private void lstbox_dogs_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var filepath = "ImageAssets/";
        var selectedIndex = lstbox_dogs.SelectedIndex;
        if (selectedIndex >= 0 && selectedIndex < dogs.Count)
        {
            var selectedDog = dogs[selectedIndex];

            // Use the selectedDog object to display the details for each label
            txt_name.Text = selectedDog.Name;
            txt_birthDate.Text = selectedDog.DOB.ToShortDateString();
            txt_breed.Text = selectedDog.Breed;
            txt_weight.Text = selectedDog.Weight + " lbs";
            txt_shotUpdate.Text = selectedDog.UpdatedShotsAnswer;
            txt_monthsInKennel.Text = selectedDog.MonthsInKennel + " month(s)";
            txt_color.Text = selectedDog.Colors;

            // Set the image of a dog by getting the name associated with the pic
            filepath += selectedDog.Pic;
            img_selectedDogPic.Source = new BitmapImage(new Uri(filepath, UriKind.Relative));
        }
    }

    // Open up the verification window. While doing so, pass down the information from the selected index and dogs list so that the appropriate information can be seen after it has finished loading up.
    private void btn_toVerification_Click(object sender, RoutedEventArgs e)
    {
        var selectedIndex = lstbox_dogs.SelectedIndex;
        var window1 = new Verification(dogs, selectedIndex);
        Visibility = Visibility.Hidden;
        window1.ShowDialog();
        Visibility = Visibility.Visible;
    }
}