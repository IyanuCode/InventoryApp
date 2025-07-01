using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApp
{
    public class Program
    {

        public class Product
        {
            public string? Name { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public string? SerialNumber { get; set; }
        }
        // List to store products
        private static List<Product> products = new List<Product>();

        // File path to store products
        private const string filePath = "products.txt";
        private const string filePath2 = "products.csv";

        // Entry point of the application
        public static void Main(string[] args)
        {
            RunMenuLoop();
        }
        // Method to run the menu loop
        private static void RunMenuLoop()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Inventory Management System");
                Console.WriteLine("Press '1' To Add Product");
                Console.WriteLine("Press '2' To View Products");
                Console.WriteLine("Press '3' To Update Product");
                Console.WriteLine("Press '4' To Delete Product");
                Console.WriteLine("Press '5' To Search Product");
                Console.WriteLine("Press '0' To Exit");
                Console.Write("Enter an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProduct();
                        break;
                    case "2":
                        ViewProduct();
                        break;
                    case "3":
                        UpdateProduct();
                        break;
                    case "4":
                        DeleteProduct();
                        break;
                    case "5":
                        SearchProduct();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid input, please try again.");
                        break;
                }
            }
        }
        // Method to add a product
        private static void AddProduct()
        {
            Console.Clear();
            var product = new Product();

            Console.Write("Enter product name: ");
            string? name = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(name) && name.Length > 2)
            {
                Console.WriteLine("Product name must be at least 3 characters long and cannot be empty.");
                return;
            }
            else
            {
                product.Name = name;
            }

            Console.Write("Enter product quantity: ");
            if (int.TryParse(Console.ReadLine(), out int quantity))
            {
                product.Quantity = quantity;
            }
            else
            {
                Console.WriteLine("Invalid quantity. Please enter a valid number.");
                return;
            }

            Console.Write("Enter product price: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                product.Price = price;
            }
            else
            {
                Console.WriteLine("Invalid price. Please enter a valid amount.");
                return;
            }

            product.SerialNumber = GenerateSerialNumber();

            Console.WriteLine($"price: ₦{product.Price}, \n quantity: {product.Quantity}, \n serial number: {product.SerialNumber}");
            products.Add(product);
            Console.WriteLine("Product added successfully!");
            Console.WriteLine("Press any key to continue...");

            WriteToFile();

            Console.WriteLine("Press any key to return to the menu.");

            // Wait for user input before returning to the menu
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                return;
            }
            else
            {
                Console.WriteLine("Press any key to continue...");
            }
        }
        /*-----------------------------------------End of AddProduct Method-----------------------------------------*/

        /*-----------------------------------------View Products Method-----------------------------------------*/
        private static void ViewProduct()
        {
            Console.Clear();
            LoadFromFile();
            if (products.Count == 0)
            {
                Console.WriteLine("No products available.");
            }
            else
            {
                Console.WriteLine("Available Products:\n");
                 Console.WriteLine( $"{ "Serial",-15} | { "Name",-25} | { "Qty",-5} | { "Price",-10}");
                 Console.WriteLine(new string('-', 65));
                foreach (var product in products)
                {
                    Console.WriteLine($"Name: {product.Name,-25}, Quantity: {product.Quantity, -5}, Price: ₦{product.Price, -10:N2}, Serial Number: {product.SerialNumber, -15}");
                }
            }
            MenuOption();
        }

        /*------------------------------------------End of View Products Method-----------------------------------------*/


        /*-----------------------------------------Load from file-----------------------------------------*/
        private static void LoadFromFile()
        {
            products.Clear(); 

        if (File.Exists(filePath2))
        {
        string[] lines = File.ReadAllLines(filePath);

        
        int startIndex = lines[0].StartsWith("Serial") ? 1 : 0;

        for (int i = startIndex; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');

            if (parts.Length == 4)
            {
                products.Add(new Product
                {
                    SerialNumber = parts[0].Trim('"'),
                    Name = parts[1].Trim('"'),
                    Quantity = int.Parse(parts[2]),
                    Price = decimal.Parse(parts[3])
                });
            }
        }
    }
}

        
        /* ------------------------------------------End of Load from file-----------------------------------------*/



        /*-----------------------------------------Update Product Method-----------------------------------------*/

        private static void UpdateProduct()
        {
            Console.Clear();
            Console.Write("Enter the serial number of the product you want to update: ");
            string? serialNumber = Console.ReadLine()?.Trim();
             if (!string.IsNullOrWhiteSpace(serialNumber) && serialNumber.Length > 2 && serialNumber.StartsWith("SN-"))
            {
                Console.WriteLine("Press 1 to update Name");
                Console.WriteLine("Press 2 to update Quantity");    
                Console.WriteLine("Press 3 to update Price");
                Console.WriteLine("Press 0 to exit");
                Console.WriteLine("Press any other key to return to the main menu.");
                Console.Write("Input: ");
                string? input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input) || input.Length < 1 || !Char.IsDigit(input[0]))
                {
                    Console.WriteLine("Invalid input. Please enter a valid option and must be a number.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                else{
                    switch (input)
                    {
                        case "1":
                            Console.Clear();
                            Console.Write("Enter the new name for the product: ");
                            string? newName = Console.ReadLine()?.Trim();
                            if (!string.IsNullOrWhiteSpace(newName) && newName.Length > 1)
                            {
                                var product = products.FirstOrDefault(p => p.SerialNumber == serialNumber);
                                if (product != null)
                                {
                                    product.Name = newName;
                                    Console.WriteLine("Product name updated successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("Product not found.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid name. Please enter a valid name.");
                            }
                            WriteToFile();
                            MenuOption();
                            break;

                        case "2":
                            Console.Clear();
                            Console.Write("Enter the new quantity for the product: ");
                            if (int.TryParse(Console.ReadLine(), out int newQuantity))
                            {
                                var product = products.FirstOrDefault(p => p.SerialNumber == serialNumber);
                                if (product != null)
                                {
                                    product.Quantity = newQuantity;
                                    Console.WriteLine("Product quantity updated successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("Product not found.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid quantity. Please enter a valid number.");
                            }
                            WriteToFile();
                            MenuOption();
                            break;

                        case "3":
                            Console.Clear();
                            Console.Write("Enter the new price for the product: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal newPrice))
                            {
                                var product = products.FirstOrDefault(p => p.SerialNumber == serialNumber);
                                if (product != null)
                                {
                                    product.Price = newPrice;
                                    Console.WriteLine("Product price updated successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("Product not found.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid price. Please enter a valid amount.");
                            }
                            WriteToFile();
                            MenuOption();
                            break;

                        case "0":
                            Console.Clear();
                            Console.WriteLine("Exiting the update process.");
                            return;

                        default:
                            Console.WriteLine("Invalid option. Please enter a valid option.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            return;
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid serial number. Please enter a valid serial number.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }             

        }
        /*-----------------------------------------End of Update Product Method-----------------------------------------*/


        /*-----------------------------------------Delete Product Method-----------------------------------------*/
        private static void DeleteProduct(){
            Console.Clear();
            Console.Write("Enter the serial number of the product you want to delete: ");
            string? serialNumber = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(serialNumber) && serialNumber.Length > 2 && serialNumber.StartsWith("SN-"))
            {
                var product = products.FirstOrDefault(p => p.SerialNumber == serialNumber);
                if (product != null)
                {
                    products.Remove(product);
                    Console.WriteLine("Product deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid serial number. Please enter a valid serial number.");
            }
            WriteToFile();
            MenuOption();
        }


        /*------------------------------------------End of Delete Product Method-----------------------------------------*/

        /*-----------------------------------------Search Product Method-----------------------------------------*/
        private static void SearchProduct()
        {
            Console.Clear();
            Console.Write("Enter the name of the product you want to search for: ");
            string? searchName = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(searchName) || searchName.Length < 3)
            {
                Console.WriteLine("Please enter a valid product name with at least 3 characters.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            var foundProducts = products.Where(p => p.Name != null && p.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase)).ToList();

            if (foundProducts.Count > 0)
            {
                Console.WriteLine("Search Results:");
                foreach (var product in foundProducts)
                {
                    Console.WriteLine($"Name: {product.Name}, Quantity: {product.Quantity}, Price: {product.Price:C}, Serial Number: {product.SerialNumber}");
                }
            }
            else
            {
                Console.WriteLine("No products found matching the search criteria.");
            }
        
            MenuOption();
        }

        /*--------------------------------------------End of Search Product Method-----------------------------------------*/


        /*-----------------------------------------WriteToFile-----------------------------------------*/
        private static void WriteToFile()
        {
            try
            {
                using (var writer = new System.IO.StreamWriter(filePath))
                {
                     writer.WriteLine(
                        $"{ "Serial",-15} | { "Name",-20} | { "Qty",-5} | { "Price",-10}");
                        writer.WriteLine(new string('-', 60));
                    foreach (var product in products)
                    {
                       writer.WriteLine(
                    $"{product.SerialNumber,-15} | {product.Name,-20} | {product.Quantity,-5} | ₦{product.Price,-10:N2}");
                    }
                }
                Console.WriteLine("Products saved to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
            }




            // Also trying CSV format for export
            try
        {
            using (var writer = new System.IO.StreamWriter("filePath2"))
        {
            writer.WriteLine("SerialNumber,Name,Quantity,Price");

            foreach (var product in products)
            {
                writer.WriteLine($"\"{product.SerialNumber}\",\"{product.Name}\",{product.Quantity},{product.Price}");
            }
        }

        Console.WriteLine(" Products exported to CSV successfully.");
    }
        catch (Exception ex)
    {
        Console.WriteLine($"Could not export to CSV: {ex.Message}");
    }









            
        }

        /*------------------------------------------End of WriteToFile-----------------------------------------*/



        /*-----------------------------------------Menu Option-----------------------------------------*/
        private static void MenuOption(){
            Console.WriteLine("Press 1 to go back to the main menu");
            Console.WriteLine("Press any other key to exit the application");

             var choice = Console.ReadKey(true).Key;
            if (choice == ConsoleKey.D1 || choice == ConsoleKey.NumPad1)
            {
                Console.Clear();
                WriteToFile();
                RunMenuLoop();
            }
            else
            {
                WriteToFile();
                Console.Clear();
                Console.WriteLine("Exiting the application. Thank you!");
                Environment.Exit(0);
            }
        }

         /*-----------------------------------------End of Menu Option-----------------------------------------*/




        /*-----------------------------------------Generate Serial Number Method-----------------------------------------*/


        private static int _serialCounter = 1000;

        private static string GenerateSerialNumber()
        {
            return $"SN-{_serialCounter++}";
        }   
        /*-----------------------------------------End of Generate Serial Number Method-----------------------------------------*/

    }
}