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
        Salon newSalon = new Salon(Request.Form["salon-name"], Request.Form["salon-about"]);
        newSalon.Save();
        List<Salon> allSalons = Salon.GetAll();
        return View["salons.cshtml", allSalons];
      }; //posts from form adding new salon, returns list of all salons

      Get["/stylists/new"] = _ => {
        List<Salon> AllSalons = Salon.GetAll();
        return View["add_stylist.cshtml", AllSalons];
      }; //navigates to form to add new stylist

      Post["/stylists/new"] = _ => {
        Stylist newStylist = new Stylist(Request.Form["stylist-name"], Request.Form["stylist-about"], Request.Form["salon-id"]);
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

      Get["/salons/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Salon SelectedSalon = Salon.Find(parameters.id);
        List<Stylist> SalonStylists = SelectedSalon.GetStylists();
        model.Add("salon", SelectedSalon);
        model.Add("stylists", SalonStylists);
        return View["salon.cshtml", model];
      }; //retrieves individual salon pages

      Get["/salon/{id}/edit"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Salon SelectedSalon = Salon.Find(parameters.id);
        string salonEdit = Request.Query["salon-edit"];
        model.Add("form-type", salonEdit);
        model.Add("salon", SelectedSalon);
        return View["edit.cshtml", model];
      }; //edit individual salon

      Patch["/salon/{id}/edit"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Salon SelectedSalon = Salon.Find(parameters.id);
        SelectedSalon.Update(Request.Form["salon-name"], Request.Form["salon-about"]);
        List<Stylist> SalonStylists = SelectedSalon.GetStylists();
        model.Add("salon", SelectedSalon);
        model.Add("stylists", SalonStylists);
        return View["salon.cshtml", model];
      }; //returns edited salon page

      Get["salon/{id}/delete"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Salon SelectedSalon = Salon.Find(parameters.id);
        string salonDelete = Request.Query["salon-delete"];
        model.Add("form-type", salonDelete);
        model.Add("salon", SelectedSalon);
        return View["delete.cshtml", model];
      }; //delete individual salon

      Delete["salon/{id}/delete"] = parameters => {
        Salon SelectedSalon = Salon.Find(parameters.id);
        SelectedSalon.Delete();
        List<Salon> allSalons = Salon.GetAll();
        return View["salons.cshtml", allSalons];
      }; //returns confirmation of deleted salon


    }
  }
}
