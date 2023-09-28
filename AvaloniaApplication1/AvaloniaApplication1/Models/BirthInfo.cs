using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication1.Models
{
    public class BirthInfo : BaseClass
    {
        private DateTime? _birthDate = DateTime.Today;
        private string? _country;
        private string? _region;
        private string? _city;

        public DateTime? BirthDate {  get { return _birthDate; } set {  _birthDate = value; OnPropertyChanged(nameof(BirthDate)); } }
        public string? Country { get { return _country; } set { _country = value; OnPropertyChanged(nameof(Country)); } }
        public string? Region { get { return _region; } set { _region = value; OnPropertyChanged(nameof(Region)); } }
        public string? City { get { return _city; } set { _city = value; OnPropertyChanged(nameof(City)); } }
        public string? BirthPlace { get => GetBirthPlace(); }
        private string GetBirthPlace()
        {
            var birthPlace = string.Empty;

            if (!string.IsNullOrWhiteSpace(Country))
            {
                birthPlace = $"{Country}";
            }
            if (!string.IsNullOrWhiteSpace(Region))
            {
                birthPlace += $", {Region}";
                birthPlace = birthPlace.TrimStart(' ', ',');
            }
            if (!string.IsNullOrWhiteSpace(City))
            {
                birthPlace += $", {City}";
                birthPlace = birthPlace.TrimStart(' ', ',');
            }

            return birthPlace;
        }
    }
}
