# MoMakan - Katalog Kuliner Murah Mahasiswa ITS (WPF + SQL Server)

## Cara menjalankan
1. **SSMS**: buka `Database/MoMakan_DB.sql` → Execute, lalu jalankan `Database/02_Data_Sekitar_ITS.sql` (mengganti data awal dengan tempat makan di sekitar ITS).
2. **Connection string**: edit `appsettings.json`, ganti nilai `Server=` sesuai instance SQL Server kamu:
   - `localhost` / `.` (instance default)
   - `.\SQLEXPRESS` (SQL Express)
   - `(localdb)\MSSQLLocalDB` (LocalDB)
   - `NAMA-PC\SQLEXPRESS`  (lihat di kotak "Server name" saat login SSMS; pakai nilai yang sama)
   Jika memakai SQL login: `Server=...;Database=MoMakan_DB;User Id=sa;Password=...;TrustServerCertificate=True;`
3. **Visual Studio 2022** (workload ".NET desktop development", .NET 8 SDK): buka `MoMakan.csproj`, tekan F5.
   NuGet `Microsoft.Data.SqlClient` akan ter-restore otomatis.

## Akun dari data seed (password semuanya `MoMakan2026`, cek tabel Pengguna di SSMS untuk username)
## Struktur (MVVM)
- `Models/` entitas · `Data/` Db (ADO.NET, query berparameter) + Repository
- `Services/` AuthService, PersetujuanService, UlasanService, RekomendasiService, Validator, Guard (cek peran)
- `ViewModels/` state, command, validasi · `Views/Templates.xaml` semua halaman (DataTemplate) · `MainWindow.xaml` shell
- `Themes/` Colors.Terang / Colors.Gelap (palet gelato pastel) + Styles.xaml
- Log error teknis: `Logs/error.log` di folder output.

## Font & gambar
- Font Libre Baskerville (judul) dan Plus Jakarta Sans (isi) disematkan di `Assets/Fonts` (lisensi OFL).
- Tempat tanpa foto otomatis memakai ilustrasi kategori di `Assets/Kategori`.
