using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace HairSalon
{
  public class Salon
  {
    private int _id;
    private string _name;
    private string _about;

    public Salon(string Name, string About, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _about = About;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetAbout()
    {
      return _about;
    }

    public override bool Equals(System.Object otherSalon)
    {
      if (!(otherSalon is Salon))
      {
        return false;
      }
      else
      {
        Salon newSalon = (Salon) otherSalon;
        bool idEquality = this.GetId() == newSalon.GetId();
        bool nameEquality = this.GetName() == newSalon.GetName();
        bool aboutEquality = this.GetAbout() == newSalon.GetAbout();
        return (idEquality && nameEquality && aboutEquality);
      }
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO salons (name, about) OUTPUT INSERTED.id VALUES (@SalonName, @SalonAbout);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@SalonName";
      nameParameter.Value = this.GetName();

      SqlParameter aboutParameter = new SqlParameter();
      aboutParameter.ParameterName = "@SalonAbout";
      aboutParameter.Value = this.GetAbout();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(aboutParameter);

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

    public void Update(string newName, string newAbout)
    { //Update method for strings
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE salons SET name = @NewName, about = @NewAbout OUTPUT INSERTED.name, INSERTED.about WHERE id = @SalonId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);

      SqlParameter newAboutParameter = new SqlParameter();
      newAboutParameter.ParameterName = "@NewAbout";
      newAboutParameter.Value = newAbout;
      cmd.Parameters.Add(newAboutParameter);

      SqlParameter salonIdParameter = new SqlParameter();
      salonIdParameter.ParameterName = "@SalonId";
      salonIdParameter.Value = this.GetId();
      cmd.Parameters.Add(salonIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
        this._about = rdr.GetString(1);
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

    public static List<Salon> GetAll()
    {
      List<Salon> allSalons = new List<Salon>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM salons;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int salonId = rdr.GetInt32(0);
        string salonName = rdr.GetString(1);
        string salonAbout = rdr.GetString(2);
        Salon newSalon = new Salon(salonName, salonAbout, salonId);
        allSalons.Add(newSalon);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allSalons;
    }

    public static Salon Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM salons WHERE id = @SalonId;", conn);
      SqlParameter salonIdParameter = new SqlParameter();
      salonIdParameter.ParameterName = "@SalonId";
      salonIdParameter.Value = id.ToString();
      cmd.Parameters.Add(salonIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundSalonId = 0;
      string foundSalonName = null;
      string foundSalonAbout = null;
      while(rdr.Read())
      {
        foundSalonId = rdr.GetInt32(0);
        foundSalonName = rdr.GetString(1);
        foundSalonAbout = rdr.GetString(2);
      }
      Salon foundSalon = new Salon(foundSalonName, foundSalonAbout, foundSalonId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundSalon;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM salons;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
