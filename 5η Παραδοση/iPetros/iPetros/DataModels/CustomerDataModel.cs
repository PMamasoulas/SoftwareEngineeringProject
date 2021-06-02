using Atom.Relational;

namespace iPetros
{
    [Documentation("Αντιπροσωπεύει έναν πελάτη")]
    public class CustomerDataModel : BaseDateDataModel
    {
        #region Public Properties

        [Documentation("Το μικρό όνομα")]
        public string FirstName { get; set; }

        [Documentation("Το επίθετο")]
        public string LastName { get; set; }

        [Documentation("Το Α.Φ.Μ")]
        [Unique]
        public string VAT { get; set; }

        [Documentation("Το email του πελάτη")]
        public string Email { get; set; }

        [Documentation("Ο αριθμός τηλεφώνου του πελάτη")]
        public string PhoneNumber { get; set; }

        #endregion

        #region Constructors

        [Documentation("Βασικός κατασκευαστής")]
        public CustomerDataModel()
        {

        }

        #endregion
    }
}
