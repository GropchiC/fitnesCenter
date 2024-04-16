using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Tmds.DBus.Protocol;

namespace fitnesCenter;

public partial class Fitnes : Window
{
    private List<mat> _services;
    string fullTable = "SELECT * FROM klient";
    string connStr = "server=10.10.1.24;database=smz;port=3306;User Id=root;password=root";
    private MySqlConnection conn;
    public MainWindow()
    {
        InitializeComponent();
        ShowTable(fullTable);
        FillStatus();
    }
    public void ShowTable(string sql)
    {
        _services = new List<mat>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var Cli = new mat()
            {
                ID = reader.GetInt32("ID"),
                Familia = reader.GetString("Familia"),
                Imia = reader.GetString("Imia"),
                NomerTelefona = reader.GetString("NomerTelefona"),
                Datarojdeneeia = reader.GetString("Datarojdeneeia"),
                Pol = reader.GetString("Pol"),
                Skidka = reader.GetInt32("Skidka")
            };
            _services.Add(Cli);
        }
            
        conn.Close();
        DataGrid.ItemsSource = _services;
    }
    private void Search(object? sender, TextChangedEventArgs e)
    {
        var gds = _services;
        gds = gds.Where(x => x.Familia.Contains(ServSearch.Text)).ToList();
        DataGrid.ItemsSource = gds;
    }

    private void Del(object? sender, RoutedEventArgs e)
    {
        try
        {
            mat usr = DataGrid.SelectedItem as mat;
            if (usr == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM klient WHERE id = " + usr.ID;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            _services.Remove(usr);
            ShowTable(fullTable);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}