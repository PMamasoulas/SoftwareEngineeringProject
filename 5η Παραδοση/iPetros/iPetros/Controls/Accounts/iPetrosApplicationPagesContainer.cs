using Atom.Core;
using Atom.Core.Accounts;
using Atom.Web.Accounts;
using Atom.Windows.Controls.Accounts;

using System.Threading.Tasks;
using System.Windows;

namespace iPetros
{
    [Documentation("Περιέχει την σελίδα σύνδεσης καθώς και την κύρια σελίδα της εφαρμογής")]
    public class iPetrosApplicationPagesContainer : BaseApplicationPagesContainer
    {
        #region Constructors

        [Documentation("Βασικός κατασκευαστής")]
        public iPetrosApplicationPagesContainer() : base()
        {
            GoToRegisterButton.Visibility = Visibility.Collapsed;
        }

        #endregion

        #region Protected Methods

        [Documentation("Επιστρέφει το κύριο παράθυρο της εφαρμογής")]
        protected override FrameworkElement CreateMainApplicationPage() => new iPetrosMainApplicationPage();

        [Documentation("Συνδέει τον χρήστη στην εφαρμογή")]
        protected override async Task<IFailable<CredentialsDataModel>> LogInAsync([ParameterDocumentation("Πληροφορίες σχετικά με τα στοιχεία σύνδεσης")] LogInRequestModel model)
        {
            var result = await iPetrosDI.GetDataStorage.LogInAsync(model);

            if (!result.Successful)
                return new Failable<CredentialsDataModel>() { ErrorMessage = result.ErrorMessage };

            iPetrosDI.ConnectedUser = result.Result;

            return new Failable<CredentialsDataModel>()
            {
                Result = new CredentialsDataModel()
                {
                    FirstName = result.Result.FirstName,
                    LastName = result.Result.LastName,
                    Username = result.Result.Username,
                    Email = result.Result.Email,
                    PhoneNumber = result.Result.PhoneNumber
                }
            };
        }

        #endregion
    }
}
