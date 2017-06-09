using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  [Collection("HairSalon")]
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_StylistDatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Stylist.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueForSameInfo()
    {
      //Arrange, Act
      Stylist firstStylist = new Stylist("Harry Cutter", "a great stylist");
      Stylist secondStylist = new Stylist("Harry Cutter", "a great stylist");
      //Assert
      Assert.Equal(firstStylist, secondStylist);
    }
    [Fact]
    public void Test_Save_SavesStylistToDatabase()
    {
      //Arrange
      Stylist testStylist = new Stylist("Harry Cutter", "a great stylist");
      testStylist.Save();
      //Act
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> expectedResult = new List<Stylist>{testStylist};
      //Assert
      Assert.Equal(result, expectedResult);
    }
    [Fact]
    public void Test_Save_SavesMultipleStylistsToDatabase()
    {
      //Arrange
      Stylist firstTestStylist = new Stylist("Harry Cutter", "a great stylist");
      firstTestStylist.Save();
      Stylist secondTestStylist = new Stylist("Dwayne Johnson", "a wonderful stylist");
      secondTestStylist.Save();
      //Act
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> expectedResult = new List<Stylist>{firstTestStylist, secondTestStylist};
      //Assert
      Assert.Equal(result, expectedResult);
    }
    [Fact]
    public void Test_Save_AssignsIdToStylistInDatabase()
    {
      //Arrange
      Stylist testStylist = new Stylist("Harry Cutter", "a great stylist");
      testStylist.Save();
      //Act
      Stylist savedStylist = Stylist.GetAll()[0];
      int testId = testStylist.GetId();
      int expectedId = savedStylist.GetId();
      //Assert
      Assert.Equal(testId, expectedId);
    }
    [Fact]
    public void Test_Find_FindsStylistInDatabase()
    {
      //Arrange
      Stylist testStylist = new Stylist("Harry Cutter", "a great stylist");
      testStylist.Save();
      //Act
      Stylist foundStylist = Stylist.Find(testStylist.GetId());
      //Assert
      Assert.Equal(testStylist, foundStylist);
    }
    [Fact]
    public void Test_Update_ReturnsTrueIfStylistInfoIsTheSame()
    {
      //Arrange
      Stylist firstTestStylist = new Stylist("Harry Cutter", "a great stylist");
      firstTestStylist.Save();
      Stylist secondTestStylist = new Stylist("Dwayne Johnson", "a wonderful stylist", firstTestStylist.GetId());
      //Act
      secondTestStylist.Update("Harry Cutter", "a great stylist");
      //Assert
      Assert.Equal(firstTestStylist, secondTestStylist);
    }
    [Fact]
    public void Test_Delete_ReturnsTrueIfListsAreTheSame()
    {
      //Arrange
      Stylist firstTestStylist = new Stylist("Harry Cutter", "a great stylist");
      firstTestStylist.Save();
      Stylist secondTestStylist = new Stylist("Dwayne Johnson", "a wonderful stylist");
      secondTestStylist.Save();
      Stylist thirdTestStylist = new Stylist("Jason Statham", "a marvelous stylist");
      thirdTestStylist.Save();
      List<Stylist> expectedList = new List<Stylist>{firstTestStylist, secondTestStylist};
      //Act
      thirdTestStylist.Delete();
      List<Stylist> resultList = Stylist.GetAll();
      //Assert
      Assert.Equal(resultList, expectedList);
    }
    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
