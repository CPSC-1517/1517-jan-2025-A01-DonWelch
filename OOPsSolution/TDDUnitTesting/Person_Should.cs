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
        #endregion

        #endregion

        #region Properties
        #region Successful Tests
        //directly change the firstname (character string exists)
        //directly change the lastname (character string exists)
        //directly change the address (value can be instance or null)
        //retreiving the person's full name (consists of instance's first and last name, pattern last, first)
        #endregion
        #region Exception Tests
        //throw ArgumentNullException is firstname cannot be change (missing data)
        //throw ArgumentNullException is lastname cannot be change (missing data)
        #endregion
        #endregion

        #region Methods
        #endregion
    }
}