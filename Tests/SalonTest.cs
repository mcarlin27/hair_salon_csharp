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
    public void Dispose()
    {
      Salon.DeleteAll();
    }
  }
}
