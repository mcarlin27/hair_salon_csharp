using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  [Collection ("HairSalon")]
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_ClientDatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Client.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueForSameInfo()
    {
      //Arrange, Act
      Stylist newStylist = new Stylist("Harry Cutter", "a great stylist");
      Client firstClient = new Client("Vin Diesel", newStylist.GetId());
      Client secondClient = new Client("Vin Diesel", newStylist.GetId());
      //Assert
      Assert.Equal(firstClient, secondClient);
    }
    public void Dispose()
    {
      Client.DeleteAll();
    }
  }
}
