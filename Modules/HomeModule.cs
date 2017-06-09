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

      Get["/stylists/new"] = _ => {
        List<Salon> AllSalons = Salon.GetAll();
        return View["add_stylist.cshtml", AllSalons];
      }; //navigates to form to add new stylist

      Post["/stylists/new"] = _ => {
        Stylist newStylist = new Stylist(Request.Form["stylist-name"], Request.Form["stylist-bio"], Request.Form["salon-id"]);
        newStylist.Save();
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      }; //posts from form adding new stylist, returns list of all stylists

      Get["/clients/new"] = _ => {
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["add_client.cshtml", AllStylists];
      }; //navigates to form to add new client

      Post["/clients/new"] = _ => {
        Client newClient = new Client(Request.Form["client-name"], Request.Form["stylist-id"]);
        newClient.Save();
        List<Client> allClients = Client.GetAll();
        return View["clients.cshtml", allClients];
      }; //posts from form adding new client, returns list of all clients

    }
  }
}
