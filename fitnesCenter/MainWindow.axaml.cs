using Avalonia.Controls;

namespace fitnesCenter;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    public void Authorization(object? sender, RoutedEventArgs e)
    {
        if (Login.Text == "admin" && Password.Text == "admin")
        {
            Fitnes Admin = new Fitnes();
            Admin.delButton.IsVisible = true;
            this.Hide();
            Admin.Show();
        }
        else 
        {
            Fitnes client = new Fitnes();
            client.delButton.IsVisible = false;
            this.Hide();
            client.Show();
        }
    }

    public void Exit(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
}