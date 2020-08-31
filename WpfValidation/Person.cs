using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WpfValidation
{
    public class Person : IDataErrorInfo
    {
        public PersonValidator _validator { get; set; }

        public Dictionary<string, string> _errors { get; private set; }

        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                Validate();
            }
        }

        private string _birthday;

        public string Birthday
        {
            get
            {
                return _birthday;
            }
            set
            {
                _birthday = value;
                Validate();
            }
        }

        private string _phoneNumber;

        public string Phonenumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                Validate();
            }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string name]
        {
            get
            {
                string error;
                if (_errors.TryGetValue(name, out error))
                {
                    return error;
                }
                return null;
            }
        }

        public Person()
        {
            _validator = new PersonValidator();
            _errors = new Dictionary<string, string>();
        }

        private void Validate()
        {
            var result = _validator.Validate(this);
            _errors = result.Errors.GroupBy(error => error.PropertyName)
                .ToDictionary(group => group.Key,
                              group => String.Join(Environment.NewLine,
                                           group.Select(error => error.ErrorMessage)));
        }
    }
}
