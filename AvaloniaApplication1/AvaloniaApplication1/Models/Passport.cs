using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication1.Models
{
    public class Passport : BaseClass
    {
        private DateTime? _issueDate = DateTime.Today;
        private string? _series;
        private string? _number;
        private string? _issuePLace;

        public DateTime? IssueDate { get { return _issueDate; } set { _issueDate = value; OnPropertyChanged(nameof(IssueDate)); } }
        public string? Series { get { return _series; } set { _series = value; OnPropertyChanged(nameof(Series)); } }
        public string? Number { get { return _number; } set { _number = value; OnPropertyChanged(nameof(Number)); } }
        public string? IssuePLace { get { return _issuePLace; } set { _issuePLace = value; OnPropertyChanged(nameof(IssuePLace)); } }
        public string? PassportData { get => GetPassportData(); }
        private string GetPassportData()
        {
            var birthPlace = string.Empty;

            if (!string.IsNullOrWhiteSpace(Series))
            {
                birthPlace = $"{Series}";
            }
            if (!string.IsNullOrWhiteSpace(Number))
            {
                birthPlace += $", {Number}";
                birthPlace = birthPlace.TrimStart(' ', ',');
            }
            if (IssueDate is DateTime date && date != DateTime.MinValue)
            {
                birthPlace += $", {date.ToShortDateString()}";
                birthPlace = birthPlace.TrimStart(' ', ',');
            }
            if (!string.IsNullOrWhiteSpace(IssuePLace))
            {
                birthPlace += $", {IssuePLace}";
                birthPlace = birthPlace.TrimStart(' ', ',');
            }

            return birthPlace;
        }
    }
}
