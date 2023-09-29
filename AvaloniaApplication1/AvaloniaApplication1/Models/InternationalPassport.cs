using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication1.Models
{
    public class InternationalPassport : Passport
    {
        private string _id = string.Empty;
        private DateTime? _passportEnd = DateTime.Today;
        public DateTime? PassportEnd 
        {
            get 
            { 
                return _passportEnd; 
            } 
            set 
            { 
                _passportEnd = value; 
                OnPropertyChanged(nameof(PassportEnd)); 
            } 
        }
        public string ID { get { return _id; } set { _id = value; OnPropertyChanged(nameof(ID)); } }


        public InternationalPassport() 
        {
            ID = Guid.NewGuid().ToString();
        }

        public InternationalPassport(InternationalPassport other)
        {
            ID = other.ID;
            PassportEnd = other.PassportEnd;
            IssueDate = other.IssueDate;
            Number = other.Number;
            Series = other.Series;
            IssuePLace = other.IssuePLace;
        }

        public InternationalPassport(string series, string number, string issuePlace, DateTime issueDate, DateTime passportEnd )
        {
            ID=Guid.NewGuid().ToString();
            Series = series;
            Number = number;
            IssuePLace= issuePlace;
            IssueDate = issueDate;
            PassportEnd = passportEnd;
        }

        
    }
}
