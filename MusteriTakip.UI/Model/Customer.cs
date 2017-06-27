using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusteriTakip.UI.Model
{
    public class Customer
    {
        const string RootGroup = "<Root>";
        const string FirstNameAndLastName = RootGroup + "/" + "<FirstAndLastName>";
        const string TabbedGroup = FirstNameAndLastName + "/" + "{Tabs}";
        const string ContactGroup = TabbedGroup + "/" + "Contact";
        const string HomeAddressAndPhone = ContactGroup + "/" + "<HomeAddressAndPhone->";
        const string EmailInfo = ContactGroup + "/" + "<EmailAndSkype->";
        const string GroupAndTitle = ContactGroup + "/" + "<GroupAndTitle->";

        [Key, Display(AutoGenerateField = false)]
        public long Id { get; set; }
        [Display(Name = "Adı", GroupName = FirstNameAndLastName, Order = 1)]
        public string Name { get; set; }
        [Display(Name = "Soyadı", GroupName = FirstNameAndLastName, Order = 2)]
        public string Lastname { get; set; }
        [Display(Name = "Telefon-1", GroupName = HomeAddressAndPhone)]
        public string Phone1 { get; set; }
        [Display(Name = "Telefon-2", GroupName = HomeAddressAndPhone)]
        public string Phone2 { get; set; }
        [Display(Name = "Adres-1", GroupName = HomeAddressAndPhone)]
        public string AddressLine1 { get; set; }
        [Display(Name = "Adres-2", GroupName = HomeAddressAndPhone)]
        public string AddressLine2 { get; set; }
        [Display(Name = "E-Posta", GroupName = EmailInfo, Order = 5)]
        public string Mail { get; set; }
        [Display(Name = "Eklenme Tarihi", GroupName = GroupAndTitle, Order = 5)]
        public DateTime DateCreated { get; set; }
    }
}
