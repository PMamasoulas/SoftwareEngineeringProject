
using static Atom.Personalization.Constants;

namespace iPetros
{
    [Documentation("Αντιπροσωπεύει ένα μέλος προσωπικού")]
    public class StaffMemberDataModel : BaseDateDataModel
    {
        #region Public Properties

        [Documentation("Μικρό όνομα")]
        public string FirstName { get; set; }

        [Documentation("Επίθετο")]
        public string LastName { get; set; }

        [Documentation("Όνομα χρήστη")]
        public string Username { get; set; }

        [Documentation("Κωδικός πρόσβασης, σε αυτή την υλοποίηση δεν έχουμε κρυπτογραφήσει τον κωδικό πρόσβασης")]
        public string Password { get; set; }

        [Documentation("Ο τύπος προσωπικού")]
        public StaffMemberType Type { get; set; }

        [Documentation("Το email του χρήστη")]
        public string Email { get; set; }

        [Documentation("Ο αριθμός τηλεφώνου του χρήστη")]
        public string PhoneNumber { get; set; }

        [Documentation("Το χρώμα που αντιπροσωπεύει τον υπάλληλο")]
        public string Color { get; set; } = Blue;

        #endregion

        #region Constructors

        [Documentation("Βασικός κατασκευαστής")]
        public StaffMemberDataModel() : base()
        {

        }

        #endregion
    }
}
