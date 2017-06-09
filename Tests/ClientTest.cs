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
    [Fact]
    public void Test_Save_SavesMultipleClientsToDatabase()
    {
      //Arrange
      Client firstTestClient = new Client("Vin Diesel", 3);
      firstTestClient.Save();
      Client secondTestClient = new Client("Lady Gaga", 2);
      secondTestClient.Save();
      //Act
      List<Client> result = Client.GetAll();
      List<Client> expectedResult = new List<Client>{firstTestClient, secondTestClient};
      //Assert
      Assert.Equal(result, expectedResult);
    }
    [Fact]
    public void Test_Save_AssignsIdToClientInDatabase()
    {
      //Arrange
      Client testClient = new Client("Vin Diesel", 3);
      testClient.Save();
      //Act
      Client savedClient = Client.GetAll()[0];
      int testId = testClient.GetId();
      int expectedId = savedClient.GetId();
      //Assert
      Assert.Equal(testId, expectedId);
    }
    [Fact]
    public void Test_Find_FindsClientInDatabase()
    {
      //Arrange
      Client testClient = new Client("Vin Diesel", 3);
      testClient.Save();
      //Act
      Client foundClient = Client.Find(testClient.GetId());
      //Assert
      Assert.Equal(testClient, foundClient);
    }
    [Fact]
    public void Test_Update_ReturnsTrueIfClientInfoIsTheSame()
    {
      //Arrange
      Client firstTestClient = new Client("Vin Diesel", 3);
      firstTestClient.Save();
      Client secondTestClient = new Client("Dwayne Johnson", 3, firstTestClient.GetId());
      //Act
      secondTestClient.Update("Vin Diesel");
      //Assert
      Assert.Equal(firstTestClient, secondTestClient);
    }
    [Fact]
    public void Test_Update_ReturnsTrueIfStylistIdsAreTheSame()
    {
      //Arrange
      Stylist newStylist = new Stylist("Harry Cutter", "a great stylist");
      newStylist.Save();
      Client firstTestClient = new Client("Vin Diesel", newStylist.GetId());
      firstTestClient.Save();
      Client secondTestClient = new Client("Vin Diesel", 1, firstTestClient.GetId());
      //Act
      secondTestClient.Update(firstTestClient.GetStylistId());

      //Assert
      Assert.Equal(firstTestClient, secondTestClient);
    }
    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();
    }
  }
}
