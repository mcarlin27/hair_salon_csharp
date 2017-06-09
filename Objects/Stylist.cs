using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace HairSalon
{
  public class Stylist
  {
    private int _id;
    private string _name;
    private string _bio;
    private int _salonId;

    public Stylist(string Name, string Bio, int SalonId, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _bio = Bio;
      _salonId = SalonId;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetBio()
    {
      return _bio;
    }
    public int GetSalonId()
    {
      return _salonId;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = this.GetId() == newStylist.GetId();
        bool nameEquality = this.GetName() == newStylist.GetName();
        bool bioEquality = this.GetBio() == newStylist.GetBio();
        bool salonIdEquality = this.GetSalonId() == newStylist.GetSalonId();
        return (idEquality && nameEquality && bioEquality && salonIdEquality);
      }
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO stylists (name, bio, salon_id) OUTPUT INSERTED.id VALUES (@StylistName, @StylistBio, @SalonId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@StylistName";
      nameParameter.Value = this.GetName();

      SqlParameter bioParameter = new SqlParameter();
      bioParameter.ParameterName = "@StylistBio";
      bioParameter.Value = this.GetBio();

      SqlParameter salonIdParameter = new SqlParameter();
      salonIdParameter.ParameterName = "@SalonId";
      salonIdParameter.Value = this.GetSalonId();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(bioParameter);
      cmd.Parameters.Add(salonIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Update(string newName, string newBio)
    { //Update method for strings
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE stylists SET name = @NewName, bio = @NewBio OUTPUT INSERTED.name, INSERTED.bio WHERE id = @StylistId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);

      SqlParameter newBioParameter = new SqlParameter();
      newBioParameter.ParameterName = "@NewBio";
      newBioParameter.Value = newBio;
      cmd.Parameters.Add(newBioParameter);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = this.GetId();
      cmd.Parameters.Add(stylistIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
        this._bio = rdr.GetString(1);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Update(int newSalonId)
    { //Update method for integers
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE stylists SET salon_id = @NewSalonId OUTPUT INSERTED.salon_id WHERE id = @StylistId;", conn);

      SqlParameter newSalonIdParameter = new SqlParameter();
      newSalonIdParameter.ParameterName = "@NewSalonId";
      newSalonIdParameter.Value = newSalonId;
      cmd.Parameters.Add(newSalonIdParameter);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = this.GetId();
      cmd.Parameters.Add(stylistIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._stylistId = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        string stylistBio = rdr.GetString(2);
        int stylistSalonId = rdr.GetInt32(3);
        Stylist newStylist = new Stylist(stylistName, stylistBio, stylistsalonId, stylistId);
        allStylists.Add(newStylist);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allStylists;
    }

    public static Stylist Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists WHERE id = @StylistId;", conn);
      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = id.ToString();
      cmd.Parameters.Add(stylistIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundStylistId = 0;
      string foundStylistName = null;
      string foundStylistBio = null;
      int foundStylistSalonId = 0;
      while(rdr.Read())
      {
        foundStylistId = rdr.GetInt32(0);
        foundStylistName = rdr.GetString(1);
        foundStylistBio = rdr.GetString(2);
        foundStylistSalonId = rdr.GetInt32(3);
      }
      Stylist foundStylist = new Stylist(foundStylistName, foundStylistBio, foundStylistSalonId, foundStylistId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundStylist;
    }

    public List<Client> GetClients()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE stylist_id = @StylistId;", conn);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = this.GetId();
      cmd.Parameters.Add(stylistIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Client> clients = new List<Client> {};
      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int clientStylistId = rdr.GetInt32(2);
        Client newClient = new Client(clientName, clientStylistId, clientId);
        clients.Add(newClient);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return clients;
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM stylists WHERE id = @StylistId;", conn);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = this.GetId();

      cmd.Parameters.Add(stylistIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM stylists;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
