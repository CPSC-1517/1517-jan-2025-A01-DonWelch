using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview
{
    public class Person
    {
        private string _FirstName;
        private string _LastName;
        public string FirstName 
        {
            get { return _FirstName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("First Name", "First Name is required. Cannot be empty.");
                }
                _FirstName = value;
            } 
        }
        public string LastName
        {
            get { return _LastName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Last Name", "Last Name is required. Cannot be empty.");
                }
                _LastName = value;
            }
        }
        public ResidentAddress Address { get; set; }
        public List<Employment> EmploymentPositions { get; set; }

        public string FullName { get { return LastName + ", " + FirstName; } }

        public Person()
        {
            FirstName = "Unknown";
            LastName = "Unknown";
            EmploymentPositions = new List<Employment>();
        }

        public Person(string firstname, string lastname,
                ResidentAddress address, List<Employment> employments)
        {
         
            FirstName = firstname;
            LastName = lastname;
            Address = address;
            if(employments != null)
                EmploymentPositions = employments;
            else
                EmploymentPositions = new List<Employment>();
        }

        public void AddEmployment(Employment employment)
        {
            if (employment == null)
                throw new ArgumentNullException("Employment require, missing employment data. Unable to add employment");

            //one could code a loop to examine each item in the collection to determind if there
            //  is a duplicate history instance
            //However, lets used methods that have already been built to do searching of a collection
            //First step: determine if you need a copy of the instance
            //  in this case: only the knowledge that an instance exist is needed (do not actual need the instance)
            //                only at least one needs to exist: .Any()
            if (EmploymentPositions.Any(x => x.Title.Equals(employment.Title)
                                          && x.StartDate.Equals(employment.StartDate)))
                throw new ArgumentException($"Duplicate employment. Employment record with position {employment.Title} on {employment.StartDate}.");
            EmploymentPositions.Add(employment);
        }

        public void ChangeFullName(string firstname, string lastname)
        {
            //remember that the code was refactored earlier to place the validation for missing
            //  data within the property, thus, there will be no need for additional code
            FirstName = firstname;
            LastName = lastname;
        }
    }
}
