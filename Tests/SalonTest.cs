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
    public void Dispose()
    {
      Salon.DeleteAll();
    }
  }
}
