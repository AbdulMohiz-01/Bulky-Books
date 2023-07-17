# Bulky Books

Bulky Books is an ASP.NET MVC project that allows users to buy books and administrators to manage the book inventory. This README provides an overview of the project, installation instructions, and a brief description of its features.

## Table of Contents
- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Features
The Bulky Books project offers the following features:

### Admin View
- Add new books to the inventory.
- Update book information, including title, author, price, and quantity.
- Delete books from the inventory.
- View the list of available books.

### User View
- Browse the available books.
- View detailed information about a specific book.
- Add books to the shopping cart.
- Purchase books from the shopping cart.

## Installation
To run the Bulky Books project locally, follow these steps:

1. Clone the repository to your local machine using the following command:
   ```
   git clone https://github.com/AbdulMohiz-01/Bulky-Books.git
   ```

2. Open the project in your preferred Integrated Development Environment (IDE).

3. Make sure you have .NET and ASP.NET MVC installed on your machine.

4. Build the solution to restore NuGet packages and compile the project.

5. Configure the database connection in the `Web.config` file to match your local environment. The default connection string is as follows:
   ```xml
   <connectionStrings>
     <add name="DefaultConnection" connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BulkyBooks;Integrated Security=True;MultipleActiveResultSets=True" providerName="System.Data.SqlClient" />
   </connectionStrings>
   ```

6. Use the Package Manager Console (PMC) to apply the database migrations. Run the following command:
   ```
   Update-Database
   ```

7. Run the project and access it through your browser at `http://localhost:port/`.

## Usage
Once the Bulky Books project is up and running, you can start using its features.

### Admin View
1. Access the admin view by logging in as an administrator. The default admin credentials are:
   - Username: admin
   - Password: admin123

2. From the admin dashboard, you can perform the following actions:
   - Add new books by providing the necessary information in the "Add Book" form.
   - Update the details of existing books by clicking on the "Edit" button.
   - Delete books from the inventory using the "Delete" button.
   - View the list of available books in the inventory.

### User View
1. Browse the books available in the inventory by navigating to the home page.

2. Click on a book to view its details, including the title, author, price, and quantity.

3. To purchase a book, click on the "Add to Cart" button. The selected book will be added to your shopping cart.

4. Access your shopping cart by clicking on the cart icon in the navigation menu.

5. In the cart view, you can adjust the quantity of each book or remove items from the cart.

6. To complete the purchase, click on the "Checkout" button and follow the provided instructions.

## Contributing
Contributions to the Bulky Books project are welcome! If you encounter any issues or have suggestions for improvement, please submit them as GitHub issues or create a pull request.

When contributing code, please adhere to the existing code style, follow best practices, and provide detailed commit messages.

## License
The Bulky Books project is licensed under the [MIT License](LICENSE). Feel free to use, modify, and distribute the code as per the terms of this license.
