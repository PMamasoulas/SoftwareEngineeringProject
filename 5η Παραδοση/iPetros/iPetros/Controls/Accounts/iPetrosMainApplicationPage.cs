using Atom.Core;
using Atom.Core.Accounts;
using Atom.Windows;
using Atom.Windows.Controls;
using Atom.Windows.Controls.Accounts;
using Atom.Windows.Controls.TabControl;

using System;
using System.Windows;

using static Atom.Personalization.Constants;

namespace iPetros
{
    [Documentation("Η κύρια σελίδα της εφαρμογής μας")]
    public class iPetrosMainApplicationPage : TabControlApplicationPage
    {
        #region Protected Properties

        #region Account

        [Documentation("Το κουμπί των ειδοποιήσεων")]
        protected IconButton NotificationsButton { get; private set; }

        [Documentation("Το κουμπί επιλογών του συνδεμένου λογαριασμού")]
        protected ImageDropDown AccountDropDown { get; private set; }

        [Documentation("Το κουμπί του συνδεμένου λογαριασμού")]
        protected MenuButton AccountButton { get; private set; }

        [Documentation("Το κουμπί αποσύνδεσης")]
        protected MenuButton LogOutButton { get; private set; }

        #endregion

        #region Left Size

        [Documentation("Περιέχει τα κουμπιά που είναι διαθέσιμα μόνο στους διαχειριστές")]
        protected StackPanelCollapsibleVerticalMenu ModeratorsMenuOptionsContainer { get; private set; }

        [Documentation("Το κουμπί που οδηγεί στη σελίδα του προσωπικού")]
        protected MenuButton StaffMembersButton { get; private set; }

        [Documentation("Το κουμπί που οδηγεί στη σελίδα των logs")]
        protected MenuButton LogsMenuButton { get; private set; }

        [Documentation("Περιέχει τα κουμπιά που είναι διαθέσιμα στο πρσωπικό")]
        protected StackPanelCollapsibleVerticalMenu GeneralMenuOptionsContainer { get; private set; }

        [Documentation("Το κουμπί που οδηγεί στη σελίδα των πελατών")]
        protected MenuButton CustomersButton { get; private set; }

        [Documentation("Το κουμπί που οδηγεί στη σελίδα των ραντεβού πελατών")]
        protected MenuButton CustomerAppointmentsButton { get; private set; }

        [Documentation("Περιέχει τα κουμπιά που σχετίζονται με τις επιλογές της εφαρμογής")]
        protected StackPanelCollapsibleVerticalMenu ApplicationOptionsContainer { get; private set; }

        [Documentation("Το κουμπί που οδηγεί στις επιλογές της εφαρμογής")]
        protected MenuButton OptionsButton { get; private set; }

        #endregion

        #endregion

        #region Constructors

        [Documentation("Βασικός κατασκευαστής")]
        public iPetrosMainApplicationPage() : base()
        {
            CreateGUI();
        }

        #endregion

        #region Protected Methods

        [Documentation("Διαχειρίζεται την αρχικοποίηση του UI")]
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            AccountDropDown.Text = iPetrosDI.ConnectedUser.Username;

            // Open the calendar
            WindowsControlsDI.GetWindowsDialogManager.OpenAsync(Host, "Ημερολόγιο", IconPaths.CalendarPath, () =>
            {
                return new iPetrosCalendar();
            }, "calendar", false);
        }

        #endregion

        #region Private Methods

        [Documentation("Προσθέτει τα UI elements που χρειάζονται στην σελίδα")]
        private void CreateGUI()
        {
            #region Account

            // Create the drop down
            AccountDropDown = new ImageDropDown()
            {
                Margin = new Thickness(NormalUniformMargin),
                VectorSource = IconPaths.AccountBoxPath
            };

            // Create the account button
            AccountButton = new MenuButton() { Text = "Λογραριασμός", VectorSource = IconPaths.AccountBoxPath };

            AccountButton.Command = new RelayCommand(() =>
            {
                AccountDropDown.IsOpen = false;
            });

            // Add it
            AccountDropDown.Add(AccountButton);

            // Add a separator
            AccountDropDown.AddSeparator();

            // Create the log out button
            LogOutButton = new MenuButton() { Text = "Αποσύνδεση", VectorSource = IconPaths.LogOutPath, ForeColor = Red.ToColor() };

            LogOutButton.Command = new RelayCommand(async () =>
            {
                iPetrosDI.ConnectedUser = null;

                foreach (var map in WindowsControlsDI.GetWindowsDialogManager.Mapper)
                    await WindowsControlsDI.GetWindowsDialogManager.CloseAsync(map.Key);

                await CoreAccountsDI.GetCredentialsDataStorage.ClearDataStorageAsync();

                CoreAccountsDI.ApplicationPagesManager.CurrentPage = ApplicationPage.LogIn;

                // Close the drop down
                AccountDropDown.IsOpen = false;
            });

            // Add it
            AccountDropDown.Add(LogOutButton);

            // Add it to the right side
            TopMenuRightSideItemsContainer.Children.Add(AccountDropDown);

            // Create the notifications drop down button
            NotificationsButton = ControlsFactory.CreateCircularIconButton(IconPaths.BellPath);

            NotificationsButton.Margin = new Thickness(NormalUniformMargin);

            // Add it to the right side
            TopMenuRightSideItemsContainer.Children.Add(NotificationsButton);

            #endregion

            #region Left Side

            ModeratorsMenuOptionsContainer = new StackPanelCollapsibleVerticalMenu()
            {
                Text = "Διαχειριστές",
                IsOpen = true
            };

            AddLeftMenuElement(ModeratorsMenuOptionsContainer);

            #region Staff Members

            StaffMembersButton = ModeratorsMenuOptionsContainer.Add("Προσωπικό", IconPaths.AccountSupervisorCirclePath);

            StaffMembersButton.Command = new RelayCommand(() =>
            {

            });

            #endregion

            #region Logs

            LogsMenuButton = ModeratorsMenuOptionsContainer.Add("Logs", IconPaths.BookOpenPagePath);

            LogsMenuButton.Command = new RelayCommand(() =>
            {
            });

            #endregion

            GeneralMenuOptionsContainer = new StackPanelCollapsibleVerticalMenu()
            {
                Text = "Γενικές επιλογές",
                IsOpen = true
            };

            AddLeftMenuElement(GeneralMenuOptionsContainer);

            #region Customers

            CustomersButton = GeneralMenuOptionsContainer.Add("Πελάτες", IconPaths.AccountGroupPath);

            CustomersButton.Command = new RelayCommand(() =>
            {
            });

            #endregion

            #region Customer Appointments

            CustomerAppointmentsButton = GeneralMenuOptionsContainer.Add("Ραντεβού πελατών", IconPaths.MapMarkerPath);

            CustomerAppointmentsButton.Command = new RelayCommand(() =>
            {
            });

            #endregion

            ApplicationOptionsContainer = new StackPanelCollapsibleVerticalMenu()
            {
                Text = "Επιλογές εφαρμογής",
                IsOpen = true
            };

            AddLeftMenuElement(ApplicationOptionsContainer);

            #region Options

            OptionsButton = ApplicationOptionsContainer.Add("Επιλογές", IconPaths.SettingsPath);

            OptionsButton.Command = new RelayCommand(() =>
            {

            });

            #endregion

            #endregion
        }

        #endregion
    }
}
