using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  [Collection ("HairSalon")]
  public class SalonTest : IDisposable
  {
    public SalonTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_SalonDatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Salon.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueForSameInfo()
    {
      //Arrange, Act
      Salon firstSalon = new Salon("British Hairways", "a great salon");
      Salon secondSalon = new Salon("British Hairways", "a great salon");
      //Assert
      Assert.Equal(firstSalon, secondSalon);
    }
    [Fact]
    public void Test_Save_SavesSalonToDatabase()
    {
      //Arrange
      Salon testSalon = new Salon("British Hairways", "a great salon");
      testSalon.Save();
      //Act
      List<Salon> result = Salon.GetAll();
      List<Salon> expectedResult = new List<Salon>{testSalon};
      //Assert
      Assert.Equal(result, expectedResult);
    }
    [Fact]
    public void Test_Save_SavesMultipleSalonsToDatabase()
    {
      //Arrange
      Salon firstTestSalon = new Salon("British Hairways", "a great salon");
      firstTestSalon.Save();
      Salon secondTestSalon = new Salon("The Second Combing", "a wonderful salon");
      secondTestSalon.Save();
      //Act
      List<Salon> result = Salon.GetAll();
      List<Salon> expectedResult = new List<Salon>{firstTestSalon, secondTestSalon};
      //Assert
      Assert.Equal(result, expectedResult);
    }
    [Fact]
    public void Test_Save_AssignsIdToSalonInDatabase()
    {
      //Arrange
      Salon testSalon = new Salon("British Hairways", "a great salon");
      testSalon.Save();
      //Act
      Salon savedSalon = Salon.GetAll()[0];
      int testId = testSalon.GetId();
      int expectedId = savedSalon.GetId();
      //Assert
      Assert.Equal(testId, expectedId);
    }
    [Fact]
    public void Test_Find_FindsSalonInDatabase()
    {
      //Arrange
      Salon testSalon = new Salon("British Hairways", "a great salon");
      testSalon.Save();
      //Act
      Salon foundSalon = Salon.Find(testSalon.GetId());
      //Assert
      Assert.Equal(testSalon, foundSalon);
    }
    [Fact]
    public void Test_Update_ReturnsTrueIfSalonInfoIsTheSame()
    {
      //Arrange
      Salon firstTestSalon = new Salon("British Hairways", "a great salon");
      firstTestSalon.Save();
      Salon secondTestSalon = new Salon("The Second Combing", "a wonderful salon", firstTestSalon.GetId());
      //Act
      secondTestSalon.Update("British Hairways", "a great salon");
      //Assert
      Assert.Equal(firstTestSalon, secondTestSalon);
    }
    [Fact]
    public void Test_Delete_ReturnsTrueIfListsAreTheSame()
    {
      //Arrange
      Salon firstTestSalon = new Salon("British Hairways", "a great salon");
      firstTestSalon.Save();
      Salon secondTestSalon = new Salon("The Second Combing", "a wonderful salon");
      secondTestSalon.Save();
      Salon thirdTestSalon = new Salon("Sherlock Combs", "a marvelous salon");
      thirdTestSalon.Save();
      List<Salon> expectedList = new List<Salon>{firstTestSalon, secondTestSalon};
      //Act
      thirdTestSalon.Delete();
      List<Salon> resultList = Salon.GetAll();
      //Assert
      Assert.Equal(resultList, expectedList);
    }
    public void Dispose()
    {
      Salon.DeleteAll();
    }
  }
}
