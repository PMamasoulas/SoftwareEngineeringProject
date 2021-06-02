
using System;

namespace iPetros
{
    [Documentation("Αντιπροσωπεύει ένα ραντεβού πελάτη")]
    public class CustomerAppointmentDataModel : BaseDateDataModel
    {
        #region Public Properties

        [Documentation("Η ημερομηνία και ώρα έναρξης του ραντεβού")]
        public DateTimeOffset DateStart { get; set; }

        [Documentation("Η ημερομηνία και ώρα λήξης του ραντεβού")]
        public DateTimeOffset DateEnd { get; set; }

        [Documentation("Σημείωση σχετικά με το ραντεβού")]
        public string Note { get; set; }

        #region Relationships

        [Documentation("Το id του πελάτη")]
        public int CustomerId { get; set; }

        [Documentation("Ο πελάτης")]
        public CustomerDataModel Customer { get; set; }

        [Documentation("Το id του υπαλλήλου")]
        public int StaffMemberId { get; set; }

        [Documentation("Ο υπάλληλος")]
        public StaffMemberDataModel StaffMember { get; set; }

        #endregion

        #endregion

        #region Constructors

        [Documentation("Βασικός κατασκευαστής")]
        public CustomerAppointmentDataModel()
        {

        }

        #endregion
    }
}
