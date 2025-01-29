using OOPsReview;
using FluentAssertions;

namespace TDDUnitTesting
{
    public class Person_Should
    {
        #region Constructor
        #region Successful Tests
        //the [Fact] unit test executes once
        //without the [Fact] annotation, the method is NOT considered a unit test
        [Fact]
        public void Create_An_Instance_Using_The_Default_Constructor()
        {
            //Arrange (this is the setup of values needed for doing the test
            string expectedFirstName = "Unknown";
            string expectedLastName = "Unknown";
            int expectedEmploymentPositionsCount = 0;

            //Act (this is the action that is under test)
            //sut: subject under test
            Person sut = new Person();

            //Assert (This is the check area of the test against accept values)
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.Address.Should().BeNull();
            sut.EmploymentPositions.Count.Should().Be(expectedEmploymentPositionsCount);
        }
        [Fact]
        public void Create_An_Instance_Using_The_Greedy_Constructor_Without_Adress_Employment()
        {
            //Arrange (this is the setup of values needed for doing the test
            string expectedFirstName = "Don";
            string expectedLastName = "Welch";
            int expectedEmploymentPositionsCount = 0;

            //Act (this is the action that is under test)
            //sut: subject under test
            Person sut = new Person("Don","Welch",null,null);

            //Assert (This is the check area of the test against accept values)
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.Address.Should().BeNull();
            sut.EmploymentPositions.Count.Should().Be(expectedEmploymentPositionsCount);
        }

        [Fact]
        public void Create_An_Instance_Using_The_Greedy_Constructor_With_Adress_Employment()
        {
            //Arrange (this is the setup of values needed for doing the test
            string expectedFirstName = "Don";
            string expectedLastName = "Welch";
            ResidentAddress expectedAddress = new ResidentAddress(123, "Maple St.",
                                "Edmonton", "AB", "T6Y7U8");

            //how to test a collection?
            //create individual instances of the item in the list
            //in this example those instances are objects
            //you must remember each object has a unique GUID
            //NOTE: you CANNOT reuse a single variable to hold the separate instances
            Employment one = new Employment("PG I", SupervisoryLevel.TeamMember,
                            DateTime.Parse("2013/10/04"), 6.5);
            Employment two = new Employment("PG II", SupervisoryLevel.TeamMember,
                            DateTime.Parse("2020/04/04"));
            List<Employment> employments = new(); //in .Net core, one does not need to include
                                                  //the datatype with need if the construction is
                                                  //using the system default
            employments.Add(one);
            employments.Add(two);

            int expectedEmploymentPositionsCount = 2;

            //Act (this is the action that is under test)
            //sut: subject under test
            Person sut = new Person("Don", "Welch", expectedAddress, employments);

            //Assert (This is the check area of the test against accept values)
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.Address.Should().Be(expectedAddress);
            //before testing the actual contents of the collection, it is best
            //  to check the number of items in the collection
            sut.EmploymentPositions.Count.Should().Be(expectedEmploymentPositionsCount);
            //did the greedy constructor actually use the data I submitted
            //were the instances in the list loaded as expected, including the expected order
            //check the actual contents of the list
            sut.EmploymentPositions.Should().ContainInConsecutiveOrder(employments);

        }
        #endregion
        #region Exception Tests
        //the second test anontation used is called [Theory]
        //it will execute n number of times as a loop
        //n is determind by the number [InlineData()] anontations following the [Theory]
        //to setup the test header, you must include a parameter in a parameter list
        //  one for each, value in the InlineData set of values
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Throw_Exception_Creating_Instance_Missing_FirstName(string testvalue)
        {
            //Arrange
            //possible values for FirstName: null, empty string, blank string
            //the setup of an exception test does not have to be as extensive as a successful test
            //  as the objective is to catch the exception that is thrown
            //in this example there will be no need to check expected values

            //Act
            //the act in this case is the capture of the exception that has been thrown
            //use () => to indicate that the following delegate is to be executed as the required code
            Action action = () => new Person(testvalue, "Welch", null, null);

            //Assert
            //test to see if the expectd exception was thrown
            action.Should().Throw<ArgumentNullException>();
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Throw_Exception_Creating_Instance_Missing_LastName(string testvalue)
        {
            //Arrange
            //possible values for FirstName: null, empty string, blank string
            //the setup of an exception test does not have to be as extensive as a successful test
            //  as the objective is to catch the exception that is thrown
            //in this example there will be no need to check expected values

            //Act
            //the act in this case is the capture of the exception that has been thrown
            //use () => to indicate that the following delegate is to be executed as the required code
            Action action = () => new Person("Don", testvalue, null, null);

            //Assert
            //test to see if the expectd exception was thrown
            action.Should().Throw<ArgumentNullException>();
        }

        //The above two test could be combined into a single method
        [Theory]
        [InlineData(null,"Kase")]
        [InlineData("","Kase")]
        [InlineData("    ","Kase")]
        [InlineData("Charity", null)]
        [InlineData("Cahrity", "")]
        [InlineData("Charity", "   ")]
        public void Throw_Exception_Creating_Instance_Missing_First_Or_Last_Name(string firstname, string lastname)
        {
            //Arrange
            //possible values for FirstName: null, empty string, blank string
            //the setup of an exception test does not have to be as extensive as a successful test
            //  as the objective is to catch the exception that is thrown
            //in this example there will be no need to check expected values

            //Act
            //the act in this case is the capture of the exception that has been thrown
            //use () => to indicate that the following delegate is to be executed as the required code
            Action action = () => new Person(firstname, lastname, null, null);

            //Assert
            //test to see if the expectd exception was thrown
            action.Should().Throw<ArgumentNullException>();
        }
        #endregion

        #endregion

        #region Properties
        #region Successful Tests
        //directly change the firstname (character string exists)
        [Fact]
        public void Directly_Change_First_Name_Via_Property()
        {
            //Arrange
            string expectedFirstName = "Bob";
            //since we are trying to use the property of an instance, one needs an instance in the first place
            Person sut = new Person("Don", "Welch", null, null);

            //Act
            //image the following statement is taken from a programmer's code
            sut.FirstName = "Bob";

            //Assert
            sut.FirstName.Should().Be(expectedFirstName);
        }
        //directly change the lastname (character string exists)
        [Fact]
        public void Directly_Change_Last_Name_Via_Property()
        {
            //Arrange
            string expectedLastName = "Ujest";
            //since we are trying to use the property of an instance, one needs an instance in the first place
            Person sut = new Person("Don", "Welch", null, null);

            //Act
            //image the following statement is taken from a programmer's code
            sut.LastName = "Ujest";

            //Assert
            sut.LastName.Should().Be(expectedLastName);
        }
        //directly change the address (value can be instance or null)
        [Fact]
        public void Directly_Change_Address_Via_Property()
        {
            //Arrange
            ResidentAddress expectedAddress = new ResidentAddress(321,"Ash Lane","Edmonton","AB","E4R5T6");
            //since we are trying to use the property of an instance, one needs an instance in the first place
            Person sut = new Person("Don", "Welch",
                new ResidentAddress(123, "Maple St", "Edmonton", "AB", "Y7U8I9"), null);

            //Act
            //image the following statement is taken from a programmer's code
            sut.Address = new ResidentAddress(321, "Ash Lane", "Edmonton", "AB", "E4R5T6");

            //Assert
            sut.Address.Should().Be(expectedAddress);
        }
        [Fact]
        public void Directly_Change_Address_To_Empty_Value_Via_Property()
        {
            //Arrange
            //since we are trying to use the property of an instance, one needs an instance in the first place
            Person sut = new Person("Don", "Welch",
                new ResidentAddress(123, "Maple St", "Edmonton", "AB", "Y7U8I9"), null);

            //Act
            //image the following statement is taken from a programmer's code
            sut.Address = null;

            //Assert
            sut.Address.Should().BeNull();
        }
        //retreiving the person's full name (consists of instance's first and last name, pattern last, first)
        [Fact]
        public void Retreive_Full_Name()
        {
            //Arrange
            string expectedFullName = "Ujest, Shirley";
            //since we are trying to use the property of an instance, one needs an instance in the first place


            //Act
            //image the following statement is taken from a programmer's code
            //you could retreive the full name into a local string variable
            //you could move the creation of the instance from Arrange to Act
            Person sut = new Person("Shirley", "Ujest", null, null);

            //Assert
            sut.FullName.Should().Be(expectedFullName);
        }

        #endregion
        #region Exception Tests
        //throw ArgumentNullException is firstname cannot be change (missing data)
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Throw_Exception_Directly_Changing_FirstName_With_Missing_Data(string testvalue)
        {
            //Arrange
            Person sut = new Person("Lowan", "Behold",null, null);

            //Act
            //the act in this case is the capture of the exception that has been thrown
            //use () => to indicate that the following delegate is to be executed as the required code
            Action action = () => sut.FirstName = testvalue;

            //Assert
            //test to see if the expectd exception was thrown
            action.Should().Throw<ArgumentNullException>();
        }


        //throw ArgumentNullException is lastname cannot be change (missing data)
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Throw_Exception_Directly_Changing_LastName_With_Missing_Data(string testvalue)
        {
            //Arrange
            Person sut = new Person("Lowan", "Behold", null, null);

            //Act
            //the act in this case is the capture of the exception that has been thrown
            //use () => to indicate that the following delegate is to be executed as the required code
            Action action = () => sut.LastName = testvalue;

            //Assert
            //test to see if the expectd exception was thrown
            action.Should().Throw<ArgumentNullException>();
        }
        #endregion
        #endregion

        #region Methods
        #region Successful Tests
        //can add a new employment to the person's record
        [Fact]
        public void Add_New_Employment_Record_To_Collection()
        {
            //Arrange
            ResidentAddress address = new ResidentAddress(123, "Maple St.",
                                "Edmonton", "AB", "T6Y7U8");
            Employment one = new Employment("PG I", SupervisoryLevel.TeamMember,
                            DateTime.Parse("2013/10/04"), 6.5);
            TimeSpan days = DateTime.Today - DateTime.Parse("2020/04/04");
            double years = Math.Round(days.Days / 365.2, 1);
            Employment two = new Employment("PG II", SupervisoryLevel.TeamMember,
                            DateTime.Parse("2020/04/04"),years);
            List<Employment> employments = new(); //in .Net core, one does not need to include
                                                  //the datatype with need if the construction is
                                                  //using the system default
            employments.Add(one);
            employments.Add(two);

            Person sut = new Person("Lowan", "Behold", address, employments);

            //build expected new employment
            Employment three = new Employment("SUP I", SupervisoryLevel.Supervisor,
                          DateTime.Today,0);

            //reuse the current collection and add the new expected employment
            //employments.Add(three);

            //another way is to create a new list that includes the new employment
            List<Employment> expectedEmployments = new List<Employment>();
            expectedEmployments.Add(one);
            expectedEmployments.Add(two);
            expectedEmployments.Add(three);

            int expectedEmploymentPositionsCount = 3;


            //Act
            sut.AddEmployment(three);

            //Assert
            sut.EmploymentPositions.Count.Should().Be(expectedEmploymentPositionsCount);
         
            sut.EmploymentPositions.Should().ContainInConsecutiveOrder(expectedEmployments);
        }
        //can change the person's first and last name at one time
        [Fact]
        public void Change_First_And_Last_Name()
        {
            //Arrange
            string newFullName = "Kane, Kandy";
            //since we are trying to use the property of an instance, one needs an instance in the first place
            Person sut = new Person("Don", "Welch", null, null);

            //Act
            //image the following statement is taken from a programmer's code
            sut.ChangeFullName("Kandy", "Kane");

            //Assert
            sut.FullName.Should().Be(newFullName);
        }
        #endregion
        #region Exception Tests
        //adding new employment: missing data
        [Fact]
        public void Throw_Exception_When_Adding_Employment_With_Missing_Data()
        {
            //Arrange
           //since we are trying to use the method of an instance, one needs an instance in the first place
            Person sut = new Person("Don", "Welch", null, null);

            //Act
            //image the following statement is taken from a programmer's code
            Action action = () => sut.AddEmployment(null);

            //Assert
            //one can test the contents of the error message being thrown
            //this is done using the .WithMessage(string)
            //a substring of the error message can be check using *.....* for the string
            //one can use string interpolation with the creation of the string
            action.Should().Throw<ArgumentNullException>().WithMessage("*missing employment data*");
        }
        //adding new employment: data already present
        [Fact]
        public void Throw_Exception_When_Addig_Duplicate_Employment_History()
        {
            //Arrange
            ResidentAddress address = new ResidentAddress(123, "Maple St.",
                                "Edmonton", "AB", "T6Y7U8");
            Employment one = new Employment("PG I", SupervisoryLevel.TeamMember,
                            DateTime.Parse("2013/10/04"), 6.5);
            TimeSpan days = DateTime.Today - DateTime.Parse("2020/04/04");
            double years = Math.Round(days.Days / 365.2, 1);
            Employment two = new Employment("PG II", SupervisoryLevel.TeamMember,
                            DateTime.Parse("2020/04/04"), years);
            Employment duplicate = new Employment("SUP I", SupervisoryLevel.Supervisor,
                         DateTime.Today, 0);
            List<Employment> employments = new(); //in .Net core, one does not need to include
                                                  //the datatype with need if the construction is
                                                  //using the system default
            employments.Add(one);
            employments.Add(two);
            employments.Add(duplicate);

            Person sut = new Person("Lowan", "Behold", address, employments);

            //Act
            Action action = () => sut.AddEmployment(duplicate);

            //Assert
            action.Should().Throw<ArgumentException>().WithMessage($"*{duplicate.Title} on {duplicate.StartDate}");
        }
        //change person's name: missing data
        [Theory]
        [InlineData(null,"Behold")]
        [InlineData("", "Behold")]
        [InlineData("    ","Behold")]
        [InlineData("Lowand",null)]
        [InlineData("Lowand","")]
        [InlineData("Lowand","    ")]
        public void Throw_Exception_Changing_FullName_With_Missing_Data(string firstname, string lastname)
        {
            //Arrange
            Person sut = new Person("Charity", "Kase", null, null);

            //Act
            //the act in this case is the capture of the exception that has been thrown
            //use () => to indicate that the following delegate is to be executed as the required code
            Action action = () => sut.ChangeFullName(firstname, lastname);

            //Assert
            //test to see if the expectd exception was thrown
            action.Should().Throw<ArgumentNullException>().WithMessage("*is required*");
        }
        #endregion
        #endregion
    }
}