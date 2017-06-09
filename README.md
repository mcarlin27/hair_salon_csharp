# Hair Salon

#### A program that allows users to view hair stylists and their client lists. 6/9/17

#### By **Marilyn Carlin**

## Description

A website created with C# and HTML where a ...

### Specs
| Behavior | Example Input | Example Output | Reasoning for Spec |
| :-------------     | :------------- | :------------- | :------------- |
| **WISH LIST: Save one hair salon to database** | "British Hairways" | "British Hairways" | Simplest possible input for Salon object. |
| **WISH LIST: Save multiple salons to database** | "British Hairways" "The Second Combing" | "British Hairways", "The Second Combing" | Database should save multiple instances of an object. |
| **WISH LIST: User can query list of salons** | "Query: All Salons" | "British Hairways, The Second Combing" | Uses a GetAll() method to query database. |
| **WISH LIST: User can view a specific salon** | "British Hairways" "/salons/{id}" | "British Hairways" | Uses Find() method to identify individual salon by id and return full database info for salon. |
| **Save one stylist to database** | "Harry Cutter" | "Harry Cutter" | Simplest possible input for Stylist object. |
| **WISH LIST: Links Stylist to specific Salon** | "Harry Cutter, British Hairways" | "British Hairways: Harry Cutter" | Creates one-to-many relationship between salons and stylists; stylists are inputted with SalonId. |
| **Save multiple stylists to database** | "Harry Cutter" "Dwayne Johnson" | "Harry Cutter", "Dwayne Johnson" | Database should save multiple instances of an object. |
| **User can query list of stylists** | "Query: All Stylists" | "Harry Cutter, Dwayne Johnson" | Uses a GetAll() method to query database. |
| **WISH LIST: User can query list of stylists by salon** | "Query: All Stylists at The Second Combing" | "Harry Cutter, Bruce Willis" | Uses a GetStylists() method to query stylist database by SalonId. |
| **User can view a specific stylist** | "Harry Cutter" "/stylists/{id}" | "Harry Cutter" | Uses Find() method to identify individual stylists by id and return full database info for stylist. |
| **User can edit a specific stylist** | "Edit: Harry Cutter" "/stylists/{id}/edit" | "Harry Cutter" --> "Carry Hutter" | Uses Update() method to edit individual stylist's database entry; stylist identified by id. |
| **User can delete a specific stylist** | "Delete: Harry Cutter" "/stylists/{id}/delete" | "Stylist Deleted!" | Uses Delete() method to delete individual stylist's database entry; stylist identified by id. |
| **User can delete all stylists** | "Delete: All Stylists" "/stylists/delete_all" | "All Stylists Deleted!" | Uses DeleteAll() method to delete all stylists from database. |
| **Save one client to database** | "Vin Diesel" | "Vin Diesel" | Simplest possible input for Client object. |
| **Links Client to specific Stylist** | "Vin Diesel, Harry Cutter" | "Harry Cutter: Vin Diesel" | Creates one-to-many relationship between stylist and clients; clients are inputted with StylistId. |
| **Save multiple clients to database** | "Vin Diesel" "Lady Gaga" | "Vin Diesel", "Lady Gaga" | Database should save multiple instances of an object. |
| **User can query list of clients** | "Query: All Clients" | "Vin Diesel, Lady Gaga" | Uses a GetAll() method to query database. |
| **User can query list of clients by stylist** | "Query: All Clients of Harry Cutter" | "Vin Diesel, Lady Gaga" | Uses a GetClients() method to query client database by StylistId. |
| **User can view a specific client** | "Vin Diesel" "/clients/{id}" | "Vin Diesel" | Uses Find() method to identify individual clients by id and return full database info for client. |
| **User can edit a specific client** | "Edit: Vin Diesel" "/clients/{id}/edit" | "Vin Diesel" --> "Din Viesel" | Uses Update() method to edit individual client's database entry; client identified by id. |
| **User can delete a specific client** | "Delete: Vin Diesel" "/clients/{id}/delete" | "Client Deleted!" | Uses Delete() method to delete individual client's database entry; client identified by id. |
| **User can delete all clients** | "Delete: All Clients" "/clients/delete_all" | "All Clients Deleted!" | Uses DeleteAll() method to delete all clients from database. |

## Setup/Installation Requirements

1. To run this program, you must have a C# compiler. I use [Mono](http://www.mono-project.com).
2. Install the [Nancy](http://nancyfx.org/) framework to use the view engine. Follow the link for installation instructions.
3. Clone this repository.
4. Open the command line--I use PowerShell.
5. In SQLCMD: > CREATE DATABASE hair_salon; > GO > USE hair_salon; > GO > CREATE TABLE stylists (id INT IDENTITY(1,1), name VARCHAR(255)); > CREATE TABLE clients (id INT IDENTITY(1,1), description VARCHAR(255)); > GO
6. Navigate into the repository. Use the command "dnx kestrel" to start the server.
7. On your browser, navigate to "localhost:5004" and enjoy!

## Known Bugs
* No known bugs at this time.

## Technologies Used
* C#
  * Nancy framework
  * Razor View Engine
  * ASP.NET Kestrel HTTP server
  * xUnit

* HTML

## Support and contact details

_Contact mcarlin27 over GitHub with any questions, comments, or concerns._

### License

*{This software is licensed under the MIT license}*

Copyright (c) 2017 **_{Marilyn Carlin}_**
