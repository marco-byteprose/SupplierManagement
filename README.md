# Supplier Management Application
This is a full stack application to manage Suppliers and their Products which are stored within a SQLite database. This consists of adding new suppliers, editing their details, and adding new products.

No deletion of Suppliers is added since in a real-world setting, there would be more constraints and specific processes for removing Suppliers. And this would prevent from accidental, unauthorized removal of Suppliers. 

## API
The API receives HTTP requests from the Razor application and communicates with the Northwind.db to make changes within the Suppliers or Products tables.

## Razor
The frontend is what the user will interact with and see Supplier and Product information. It communicates with the API to request information or make changes to the related tables.
