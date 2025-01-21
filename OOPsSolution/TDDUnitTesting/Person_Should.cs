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

            int expectedEmploymentPositionsCount = 0;

            //Act (this is the action that is under test)
            //sut: subject under test
            Person sut = new Person("Don", "Welch", expectedAddress, null);

            //Assert (This is the check area of the test against accept values)
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.Address.Should().Be(expectedAddress);
            sut.EmploymentPositions.Count.Should().Be(expectedEmploymentPositionsCount);
        }
        #endregion
        #region Exception Tests
        #endregion
        #endregion

        #region Properties
        #endregion

        #region Methods
        #endregion
    }
}