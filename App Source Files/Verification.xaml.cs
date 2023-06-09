using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Accessibility;

namespace Lab_DogAdoption;

/// <summary>
///     Interaction logic for Verification.xaml
/// </summary>
public partial class Verification : Window
{
    private readonly int selectedIndex;
    private readonly List<Dog> verifiedDogs = new();
    private string filepath = "ImageAssets/";
    public string SavedTextLastName = "";

    // Initialize the component alongside passing down the parameters from the "MainWindow" window.
    public Verification(List<Dog> list, int selectedIndex)
    {
        InitializeComponent();
        verifiedDogs = list;
        this.selectedIndex = selectedIndex;
    }

    // Exit the window. This doesn't end the application. Instead, it returns to the "MainWindow" window.
    private void btn_Cancel_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    // Display an adoption confirmation message.
    private void btn_Confirm_Click(object sender, RoutedEventArgs e)
    {
        // If the text box is empty, set the text to "0.00" and color it white
        if (string.IsNullOrEmpty(txt_donationBox.Text))
        {
            txt_donationBox.Text = "0.00";
            txt_donationBox.Foreground = Brushes.White;
        }

        // If the text of the donation box is a double, pull up the confirmation window. If not, show a label that displays that there was something wrong with the format of the text within the text box
        if (double.TryParse(txt_donationBox.Text, out var number))
        {
            MessageBox.Show("Your adoption has been confirmed.");
            Close();
        }
        else
        {
            lbl_donationError.Content = "WARNING: If you are\nmaking a donation,\nmake sure you are only\nusing numbers!";
        }

    }

    // Load up all of the dog information when loading up the window
    private void wdw_Verification_Loaded(object sender, RoutedEventArgs e)
    {
        var selectedDog = verifiedDogs[selectedIndex];

        filepath += selectedDog.Pic;
        img_vdogpic.Source = new BitmapImage(new Uri(filepath, UriKind.Relative));

        // Use the selectedDog object to display the details for each label
        txt_name.Text = selectedDog.Name;
        txt_birthDate.Text = selectedDog.DOB.ToShortDateString();
        txt_breed.Text = selectedDog.Breed;
        txt_weight.Text = selectedDog.Weight + " lbs";
        txt_shotUpdate.Text = selectedDog.UpdatedShotsAnswer;
        txt_monthsInKennel.Text = selectedDog.MonthsInKennel + " month(s)";
        txt_color.Text = selectedDog.Colors;
    }

    // When the text box focus is gained, remove any present commas
    private void txt_donationBox_GotFocus(object sender, RoutedEventArgs e)
    {
        txt_donationBox = (TextBox)sender;
        txt_donationBox.Text = txt_donationBox.Text.Replace(",", "");

        // If the donation box is empty upon focusing on the donation box, hide the ghost text
        if (string.IsNullOrEmpty(txt_donationBox.Text))
        {
            lbl_ghostText.Visibility = Visibility.Hidden;
        }
    }

    // When the text box focus is lost, add commas for every third digit
    private void txt_donationBox_LostFocus(object sender, RoutedEventArgs e)
    {
        txt_donationBox = (TextBox)sender;


        if (double.TryParse(txt_donationBox.Text, out var number)) txt_donationBox.Text = number.ToString("N");

        // If the donation box is empty, show ghost text
        if (string.IsNullOrEmpty(txt_donationBox.Text))
        {
            lbl_ghostText.Visibility = Visibility.Visible;
        }
    }
}