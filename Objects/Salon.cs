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
  }
}
