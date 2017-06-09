using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace HairSalon
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      }; //homepage

      Get["/salons"] = _ => {
        List<Salon> allSalons = Salon.GetAll();
        return View["salons.cshtml", allSalons];
      }; //list of all salons

      Get["/stylists"] = _ => {
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      }; //list of all stylists

      Get["/clients"] = _ => {
        List<Client> allClients = Client.GetAll();
        return View["clients.cshtml", allClients];
      }; //list of all clients

      Get["/salons/new"] = _ => {
        return View["add_salon.cshtml"];
      }; //navigates to form to add new salon

      Post["/salons/new"] = _ => {
        Salon newSalon = new Salon(Request.Form["salon-name"], Request.Form["salon-bio"]);
        newSalon.Save();
        List<Salon> allSalons = Salon.GetAll();
        return View["salons.cshtml", allSalons];
      }; //posts from form adding new salon, returns list of all salons

    }
  }
}
