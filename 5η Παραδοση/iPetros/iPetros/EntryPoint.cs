using Atom.CodeGeneration;
using Atom.Core;
using Atom.Core.Accounts;
using Atom.Windows.Controls.TabControl;

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace iPetros
{
    public static class EntryPoint
    {
        [STAThread]
        public async static Task Main()
        {
            // Initialize the DI
            Framework.Construct<DefaultFrameworkConstruction>()
                     .AddDialogManager()
                     .AddDefaultCredentialsLocalDataStorage($"DataSource=UserCredentials.db")
                     .AddiPetrosServices();

            // Build the framework using the injected services
            Framework.Construction.Build();

            // Set up
            await SetUpAsync();

            // We need to create a new thread to run our application because the STAThread attribute of the main
            // gets ignored when compiled.
            // Issue: https://github.com/dotnet/roslyn/issues/22112
            var thread = new Thread(() =>
            {
                var app = new App();
                app.InitializeComponent();
                app.Run();
            });

            // Set the thread department to STA
            thread.SetApartmentState(ApartmentState.STA);

            // Start the application
            thread.Start();
        }
        
        private static async Task SetUpAsync()
        {
            await CoreAccountsDI.GetCredentialsDataStorage.EnsureDataStorageAsync();
            await iPetrosDI.GetDataStorage.EnsureDataStorageAsync();
        }

        private static void GenerateHelpers()
        {
            var helpers = CodeGeneration.GenerateDataModelHelpers(string.Empty, typeof(CustomerAppointmentDataModel));

            CoreDI.GetFileManager.WriteTextToFileAsync(helpers, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\helpers.cs");
        }

        private static void GenerateEnumHelpers()
        {
            var helpers = CodeGeneration.GenerateEnumHelpers(string.Empty, typeof(StaffMemberType));

            CoreDI.GetFileManager.WriteTextToFileAsync(helpers, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\enumHelpers.cs");
        }
    }
}
