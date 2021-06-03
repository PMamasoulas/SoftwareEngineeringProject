using Atom.Core;
using Atom.Windows.Controls;

using System;
using System.Collections.Generic;
using System.Reflection;

using static Atom.Personalization.Constants;

namespace iPetros
{
    [Documentation("Παρέχει βοηθητικές μεθόδους σχετικά με τα models")]
    public static class iPetrosDataModelHelpers
    {
        #region Base Data Model

        [Documentation("Αντιστοιχεί τις ιδιότητες του βασικού μοντέλου σε ορισμένες τιμές")]
        public static Lazy<PropertyMapper<BaseDataModel>> BaseDataModelMapper { get; } = new Lazy<PropertyMapper<BaseDataModel>>(() =>
        {
            // Create the mapper
            var mapper = new PropertyMapper<BaseDataModel>()
                .MapTitle(x => x.Id, "Αναγνωριστικό");

            // Validate the mapper
            mapper.Validate();

            // Return the mapper
            return mapper;
        });

        #endregion

        #region Base Date Data Model

        [Documentation("Αντιστοιχεί τις ιδιότητες του βασικού μοντέλου ημερομηνιών σε ορισμένες τιμές")]
        public static Lazy<PropertyMapper<BaseDateDataModel>> BaseDateDataModelMapper { get; } = new Lazy<PropertyMapper<BaseDateDataModel>>(() =>
        {
            // Create the mapper
            var mapper = new PropertyMapper<BaseDateDataModel>()
                .MapTitle(x => x.DateCreated, "Ημερομηνία δημιουργίας")
                .MapTitle(x => x.DateModified, "Ημερομηνία επεξεργασίας");

            mapper.CopyMapsFrom(BaseDataModelMapper.Value);

            // Validate the mapper
            mapper.Validate();

            // Return the mapper
            return mapper;
        });

        [Documentation("Παρέχει μεταφραστές για τις τιμές ορισμένων ιδιοτήτων του βασικού μεντέλου ημερομηνιών")]
        public static Lazy<Translator<BaseDateDataModel>> BaseDateDataModelTranslator { get; } = new Lazy<Translator<BaseDateDataModel>>(() =>
        {
            var translator = new Translator<BaseDateDataModel>()
             .SetLocalDateTimeTranslator(x => x.DateCreated)
             .SetLocalDateTimeTranslator(x => x.DateModified);

            return translator;
        });

        #endregion

        #region Customer Data Model

        [Documentation("Αντιστοιχεί τις ιδιότητες του μοντέλου πελάτη σε ορισμένες τιμές")]
        public static Lazy<PropertyMapper<CustomerDataModel>> CustomerDataModelMapper { get; } = new Lazy<PropertyMapper<CustomerDataModel>>(() =>
        {
            // Create the mapper
            var mapper = new PropertyMapper<CustomerDataModel>()
                .MapTitle(x => x.FirstName, "Όνομα")
                .MapTitle(x => x.LastName, "Επίθετο")
                .MapTitle(x => x.VAT, "Α.Φ.Μ.")
                .MapTitle(x => x.Email, "Email")
                .MapTitle(x => x.PhoneNumber, "Αριθμός τηλεφώνου")
                .MapTitle(x => x.NormalizedName, "Κανονικοποιημένο όνομα");

            mapper.CopyMapsFrom(BaseDateDataModelMapper.Value);

            // Validate the mapper
            mapper.Validate();

            // Return the mapper
            return mapper;
        });

        [Documentation("Παρέχει μεταφραστές για τις τιμές ορισμένων ιδιοτήτων του μεντέλου πελάτη")]
        public static Lazy<Translator<CustomerDataModel>> CustomerDataModelTranslator { get; } = new Lazy<Translator<CustomerDataModel>>(() =>
        {
            var translator = new Translator<CustomerDataModel>();

            translator.CopyMapsFrom(BaseDateDataModelTranslator.Value);

            return translator;
        });

        public static Lazy<IEnumerable<PropertyInfo>> DefaultCustomerDataModelProperties { get; } = new Lazy<IEnumerable<PropertyInfo>>(() =>
        {
            return typeof(CustomerDataModel).GetProperties(nameof(CustomerDataModel.FirstName), nameof(CustomerDataModel.LastName), nameof(CustomerDataModel.VAT), nameof(CustomerDataModel.Email), nameof(CustomerDataModel.PhoneNumber));
        });

        public static DataGrid<CustomerDataModel> CreateCustomerDataModelDataGrid()
        {
            return new DataGrid<CustomerDataModel>() { Mapper = CustomerDataModelMapper.Value, Translator = CustomerDataModelTranslator.Value };
        }

        public static DataGrid<CustomerDataModel> CreateDefaultCustomerDataModelDataGrid()
        {
            var dataGrid = CreateCustomerDataModelDataGrid();

            foreach (var property in DefaultCustomerDataModelProperties.Value)
                dataGrid.UnsafeShowData(property);

            return dataGrid;
        }

        public static DataForm<CustomerDataModel> CreateCustomerDataModelDataForm()
        {
            return new DataForm<CustomerDataModel>() { Mapper = CustomerDataModelMapper.Value }
               .ShowInput(x => x.FirstName, settings => settings.IsRequired = true)
               .ShowInput(x => x.LastName, settings => settings.IsRequired = true)
               .ShowInput(x => x.VAT, settings => settings.IsRequired = true)
               .ShowTextInput(x => x.Email, TextValue.Email)
               .ShowTextInput(x => x.PhoneNumber, TextValue.Phone);
        }

        #endregion

        #region Staff Member Data Model

        [Documentation("Αντιστοιχεί τις ιδιότητες του μοντέλου υπαλλήλου σε ορισμένες τιμές")]
        public static Lazy<PropertyMapper<StaffMemberDataModel>> StaffMemberDataModelMapper { get; } = new Lazy<PropertyMapper<StaffMemberDataModel>>(() =>
        {
            // Create the mapper
            var mapper = new PropertyMapper<StaffMemberDataModel>()
                .MapTitle(x => x.FirstName, "Όνομα")
                .MapTitle(x => x.LastName, "Επίθετο")
                .MapTitle(x => x.Username, "Όνομα χρήστη")
                .MapTitle(x => x.Password, "Κωδικός πρόσβασης")
                .MapTitle(x => x.Type, "Τύπος")
                .MapTitle(x => x.Email, "Email")
                .MapTitle(x => x.PhoneNumber, "Αριθμός τηλεφώνου")
                .MapTitle(x => x.Color, "Χρώμα")
                .MapTitle(x => x.NormalizedName, "Κανονικοποιημένο όνομα");

            mapper.CopyMapsFrom(BaseDateDataModelMapper.Value);

            // Validate the mapper
            mapper.Validate();

            // Return the mapper
            return mapper;
        });

        [Documentation("Παρέχει μεταφραστές για τις τιμές ορισμένων ιδιοτήτων του μεντέλου υπαλλήλου")]
        public static Lazy<Translator<StaffMemberDataModel>> StaffMemberDataModelTranslator { get; } = new Lazy<Translator<StaffMemberDataModel>>(() =>
        {
            var translator = new Translator<StaffMemberDataModel>()
             .SetTranslator(x => x.Type, x => x.ToLocalizedString());

            translator.CopyMapsFrom(BaseDateDataModelTranslator.Value);

            return translator;
        });

        public static Lazy<IEnumerable<PropertyInfo>> DefaultStaffMemberDataModelProperties { get; } = new Lazy<IEnumerable<PropertyInfo>>(() =>
        {
            return typeof(StaffMemberDataModel).GetProperties(nameof(StaffMemberDataModel.FirstName), nameof(StaffMemberDataModel.LastName), nameof(StaffMemberDataModel.Username), nameof(StaffMemberDataModel.Type), nameof(StaffMemberDataModel.Email), nameof(StaffMemberDataModel.PhoneNumber), nameof(StaffMemberDataModel.Color));
        });

        public static DataGrid<StaffMemberDataModel> CreateStaffMemberDataModelDataGrid()
        {
            return new DataGrid<StaffMemberDataModel>() { Mapper = StaffMemberDataModelMapper.Value, Translator = StaffMemberDataModelTranslator.Value }
              .SetLabelUIElement(x => x.Type, (model) => model.Type.ToLocalizedString(), (model) => model.Type.ToColorHex())
              .SetColorUIElement(x => x.Color);
        }

        public static DataGrid<StaffMemberDataModel> CreateDefaultStaffMemberDataModelDataGrid()
        {
            var dataGrid = CreateStaffMemberDataModelDataGrid();

            foreach (var property in DefaultStaffMemberDataModelProperties.Value)
                dataGrid.UnsafeShowData(property);

            return dataGrid;
        }

        public static DataForm<StaffMemberDataModel> CreateStaffMemberDataModelDataForm()
        {
            return new DataForm<StaffMemberDataModel>() { Mapper = StaffMemberDataModelMapper.Value }
               .ShowInput(x => x.FirstName, settings => settings.IsRequired = true)
               .ShowInput(x => x.LastName, settings => settings.IsRequired = true)
               .ShowInput(x => x.Username, settings => settings.IsRequired = true)
               .ShowTextInput(x => x.Email, TextValue.Email, settings => settings.IsRequired = true)
               .ShowTextInput(x => x.PhoneNumber, TextValue.Phone, settings => settings.IsRequired = true)
               .ShowStringColorInput(x => x.Color, settings => settings.IsRequired = true);
        }

        #endregion

        #region Customer Appointment Data Model

        [Documentation("Αντιστοιχεί τις ιδιότητες του μοντέλου ραντεβού πελάτη σε ορισμένες τιμές")]
        public static Lazy<PropertyMapper<CustomerAppointmentDataModel>> CustomerAppointmentDataModelMapper { get; } = new Lazy<PropertyMapper<CustomerAppointmentDataModel>>(() =>
        {
            // Create the mapper
            var mapper = new PropertyMapper<CustomerAppointmentDataModel>()
                .MapTitle(x => x.DateStart, "Ημερομηνία έναρξης")
                .MapTitle(x => x.DateEnd, "Ημερομηνία λήξης")
                .MapTitle(x => x.Note, "Σημείωση")
                .MapTitle(x => x.CustomerId, "Αναγνωριστικό πελάτη")
                .MapTitle(x => x.Customer, "Πελάτης")
                .MapTitle(x => x.StaffMemberId, "Αναγνωριστικό υπαλλήλου")
                .MapTitle(x => x.StaffMember, "Υπάλληλος");

            mapper.CopyMapsFrom(BaseDateDataModelMapper.Value);

            // Validate the mapper
            mapper.Validate();

            // Return the mapper
            return mapper;
        });

        [Documentation("Παρέχει μεταφραστές για τις τιμές ορισμένων ιδιοτήτων του μεντέλου ραντεβού πελάτη")]
        public static Lazy<Translator<CustomerAppointmentDataModel>> CustomerAppointmentDataModelTranslator { get; } = new Lazy<Translator<CustomerAppointmentDataModel>>(() =>
        {
            var translator = new Translator<CustomerAppointmentDataModel>()
             .SetLocalDateTimeTranslator(x => x.DateStart)
             .SetLocalDateTimeTranslator(x => x.DateEnd)
             
             .SetTranslator(x => x.Customer, x => x.FirstName + " " + x.LastName)
             .SetTranslator(x => x.StaffMember, x => x.FirstName + " " + x.LastName);

            translator.CopyMapsFrom(BaseDateDataModelTranslator.Value);

            return translator;
        });

        /// <summary>
        /// Gets the default properties of the <see cref="CustomerAppointmentDataModel"/>.
        /// NOTE: The values of these properties are the most commonly used values of the model!
        /// </summary>
        public static Lazy<IEnumerable<PropertyInfo>> DefaultCustomerAppointmentDataModelProperties { get; } = new Lazy<IEnumerable<PropertyInfo>>(() =>
        {
            return typeof(CustomerAppointmentDataModel).GetProperties(nameof(CustomerAppointmentDataModel.StaffMember), nameof(CustomerAppointmentDataModel.Customer), nameof(CustomerAppointmentDataModel.DateStart), nameof(CustomerAppointmentDataModel.DateEnd), nameof(CustomerAppointmentDataModel.Note));
        });

        /// <summary>
        /// Creates and returns a <see cref="DataGrid{TClass}"/> for <see cref="CustomerAppointmentDataModel"/>s
        /// </summary>
        public static DataGrid<CustomerAppointmentDataModel> CreateCustomerAppointmentDataModelDataGrid()
        {
            return new DataGrid<CustomerAppointmentDataModel>() { Mapper = CustomerAppointmentDataModelMapper.Value, Translator = CustomerAppointmentDataModelTranslator.Value };
        }

        /// <summary>
        /// Creates and returns a <see cref="DataGrid{TClass}"/> used for presenting
        /// <see cref="CustomerAppointmentDataModel"/>s that has pre-selected the default columns
        /// </summary>
        public static DataGrid<CustomerAppointmentDataModel> CreateDefaultCustomerAppointmentDataModelDataGrid()
        {
            var dataGrid = CreateCustomerAppointmentDataModelDataGrid();

            foreach (var property in DefaultCustomerAppointmentDataModelProperties.Value)
                dataGrid.UnsafeShowData(property);

            return dataGrid;
        }

        /// <summary>
        /// Creates and returns a <see cref="DataForm{TClass}"/> for a <see cref="CustomerAppointmentDataModel"/>
        /// </summary>
        public static DataForm<CustomerAppointmentDataModel> CreateCustomerAppointmentDataModelDataForm()
        {
            return new DataForm<CustomerAppointmentDataModel>() { Mapper = CustomerAppointmentDataModelMapper.Value }
               .ShowDateAndTimeInput(x => x.DateStart)
               .ShowDateAndTimeInput(x => x.DateEnd)
               .ShowInput(x => x.Note);
        }

        #endregion

        #region Extensions

        #region Staff Member Type

        public const string StaffMemberTypeAdminColor = Blue;

        public const string StaffMemberTypeModeratorColor = Red;

        public const string StaffMemberTypeStaffMemberColor = Green;

        [Documentation("Επιστρέφει ένα string το οποίο αντιπροσωπεύει τον ορισμένο τύπο προσωπικού")]
        public static string ToLocalizedString(this StaffMemberType staffMemberType)
        {
            if (staffMemberType == StaffMemberType.Admin)
                return "Admin";
            else if (staffMemberType == StaffMemberType.Moderator)
                return "Διαχειριστής";
            else
                return "Μέλος";
        }

        [Documentation("Επιστρέφει ένα stirng το οποίο αντιπροσωπεύει ένα χρώμα σε hex μορφή για τον ορισμένο τύπο προσωπικού")]
        public static string ToColorHex(this StaffMemberType staffMemberType)
        {
            if (staffMemberType == StaffMemberType.Admin)
                return StaffMemberTypeAdminColor;
            else if (staffMemberType == StaffMemberType.Moderator)
                return StaffMemberTypeModeratorColor;
            else
                return StaffMemberTypeStaffMemberColor;
        }

        #endregion

        #endregion
    }
}
