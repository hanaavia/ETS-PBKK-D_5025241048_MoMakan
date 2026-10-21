using MoMakan.Data;
using System.IO;
using System.Text.Json;
using System.Windows;
using Microsoft.Data.SqlClient;
using MoMakan.Core;
using MoMakan.ViewModels;

namespace MoMakan;

public partial class App : Application
{
    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        DispatcherUnhandledException += (s, ev) => { Nav.Error(ev.Exception); ev.Handled = true; };

        try
        {
            var path = System.IO.Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            using var doc = JsonDocument.Parse(System.IO.File.ReadAllText(path));
            Db.ConnStr = doc.RootElement.GetProperty("ConnectionStrings").GetProperty("MoMakan").GetString();
        }
        catch (Exception ex)
        {
            Logger.Write(ex);
            MessageBox.Show("appsettings.json tidak ditemukan atau formatnya salah.", "MoMakan", MessageBoxButton.OK, MessageBoxImage.Error);
            Shutdown();
            return;
        }

        Nav.Main = new MainViewModel();
        var win = new MainWindow { DataContext = Nav.Main };
        win.Show();

        // Validasi koneksi saat startup (mitigasi risiko "connection string salah")
        try { await Db.TestAsync(); }
        catch (Exception ex)
        {
            Logger.Write(ex);
            MessageBox.Show("Tidak dapat terhubung ke database MoMakan_DB.\n\n" +
                "1. Pastikan SQL Server berjalan dan skrip Database\\MoMakan_DB.sql sudah dijalankan di SSMS.\n" +
                "2. Periksa nilai Server di appsettings.json, samakan dengan Server name di SSMS.",
                "Koneksi database gagal", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        Nav.Main.Go(() => new JelajahViewModel());
    }
}
