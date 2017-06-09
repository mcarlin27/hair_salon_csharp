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
