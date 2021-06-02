using System;
using System.Collections.Generic;
using System.Text;

namespace iPetros
{
    [Documentation("Κανόνες αναζήτησης για ραντεβού πελατών")]
    public class CustomerAppointmentDataStorageArgs : DateDataStorageArgs
    {
        #region Public Properties

        [Documentation("Περιορίζει τα αποτελέσματα σε ραντεβού που ξεκινάν μετά από μία συγκεκριμένη ημερομηνία")]
        public DateTimeOffset? DateStartAfter { get; set; }

        [Documentation("Περιορίζει τα αποτελέσματα σε ραντεβού που ξεκινάν πριν από μια συγκεκτριμένη ημερομην΄λια")]
        public DateTimeOffset? DateStartBefore { get; set; }

        [Documentation("Περιορίζει τα ραντεβού σε ραντεβού ορισμένων μελών προσωπικού")]
        public IEnumerable<int> StaffMembers { get; set; }

        [Documentation("Περιορίζει τα ραντεβού σε ραντεβού ορισμένων πελατών")]
        public IEnumerable<int> Customers { get; set; }

        #endregion

        #region Constructors

        [Documentation("Βασικός κατασκευαστής")]
        public CustomerAppointmentDataStorageArgs()
        {

        }

        #endregion
    }
}
