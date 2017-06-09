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
      newStylist.Save();
      Client firstClient = new Client("Vin Diesel", newStylist.GetId());
      Client secondClient = new Client("Vin Diesel", newStylist.GetId());
      //Assert
      Assert.Equal(firstClient, secondClient);
    }
    [Fact]
    public void Test_Save_SavesClientToDatabase()
    {
      //Arrange
      Client testClient = new Client("Vin Diesel", 3);
      testClient.Save();
      //Act
      List<Client> result = Client.GetAll();
      List<Client> expectedResult = new List<Client>{testClient};
      //Assert
      Assert.Equal(result, expectedResult);
    }
    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();
    }
  }
}
