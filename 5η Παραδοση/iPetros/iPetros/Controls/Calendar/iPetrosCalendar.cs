using Atom.Core;
using Atom.Core.Controls.Calendar;
using Atom.Windows.Controls.Calendar;

namespace iPetros
{
    [Documentation("Το ημερολόγιο της εφαρμογής")]
    public class iPetrosCalendar : Calendar
    {
        #region Constructors

        [Documentation("Βασικός κατασκευαστής")]
        public iPetrosCalendar()
        {
            Initialize();
        }

        #endregion

        #region Private Methods

        [Documentation("Αρχικοποιεί το ημερολόγιο")]
        private void Initialize()
        {
            AddPresenter(new HourBasedCalendarPresenter<CalendarEvent>(new HourBasedCalendarPresenterSettings()
            {
                Name = "Ώρες",
                ReminderEventHeight = e => 120,
                VectorSource = IconPaths.CalendarClockPath
            }, this));
            AddPresenter(new ScheduleBasedCalendarPresenter<CalendarEvent>(new CalendarPresenterSettings()
            {
                Name = "Χρονοπρογραμματισμός",
                VectorSource = IconPaths.CalendarTextPath
            }, this));
        }

        #endregion
    }
}
