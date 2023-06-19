// Shop Owner
using System;
using System.Collections.Generic;
using System.IO;

class Admin
{
    static List<string> shopOwners = new List<string>();
    static List<string> groceryItems = new List<string>();
    static List<decimal> itemPrices = new List<decimal>();
    static List<int> itemQuantities = new List<int>();
    static bool isShopOwnerLoggedIn = false;

    static void Main()
    {
        LoadDataFromFiles();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Welcome to the Console App!");
            Console.WriteLine("1. Register as Shop Owner");
            Console.WriteLine("2. Login as Shop Owner");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RegisterShopOwner();
                    break;

                case "2":
                    LoginAsShopOwner();
                    break;

                case "3":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void RegisterShopOwner()
    {
        Console.WriteLine("======= Shop Owner Registration =========");
        Console.Write("Enter your shop name: ");
        string name = Console.ReadLine();
        Console.Write("Enter your shop phone number: ");
        string phoneNumber = Console.ReadLine();

        string shopOwnerData = $"Name: {name}, Phone Number: {phoneNumber}";
        shopOwners.Add(shopOwnerData);
        Console.WriteLine("Shop Owner registered successfully!");

        // Save shop owner data to file
        File.AppendAllText("ShopOwner/ShopOwner_Details.txt", shopOwnerData + Environment.NewLine);

        Console.WriteLine();
    }

    static void LoginAsShopOwner()
    {
        Console.WriteLine("=== Shop Owner Login ===");
        Console.Write("Enter your phone number: ");
        string phoneNumber = Console.ReadLine();

        bool found = false;
        foreach (string shopOwnerData in shopOwners)
        {
            if (shopOwnerData.Contains(phoneNumber))
            {
                found = true;
                Console.WriteLine("Shop Owner login successfully!");
                isShopOwnerLoggedIn = true;
                DisplayShopOwnerMenu();
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine("Shop Owner not found. Please register first.");
            Console.WriteLine();
        }
    }

    static void DisplayShopOwnerMenu()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("=== Shop Owner Menu ===");
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. Update Item");
            Console.WriteLine("3. Delete Item");
            Console.WriteLine("4. View Item List");
            Console.WriteLine("5. Compute Total Cost");
            Console.WriteLine("6. Save All Items");
            Console.WriteLine("7. Logout");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddStock();
                    break;
                case "2":
                    UpdateStock();
                    break;
                case "3":
                    DeleteStock();
                    break;
                case "4":
                    ViewStockList();
                    break;
                case "5":
                    ComputeTotalCost();
                    break;
                case "6":
                    SaveGroceryListToFile();
                    break;
                case "7":
                    Logout();
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void Logout()
    {
        isShopOwnerLoggedIn = false;
        groceryItems.Clear();
        itemPrices.Clear();
        itemQuantities.Clear();
        Console.WriteLine("Logged out as Shop Owner.");
        Console.WriteLine();
    }

    static void AddStock()
    {
        Console.Write("Enter the number of grocery items to add: ");
        int itemCount = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < itemCount; i++)
        {
            Console.Write($"Enter the name of grocery item #{i + 1}: ");
            string itemName = Console.ReadLine();
            groceryItems.Add(itemName);

            Console.Write($"Enter the price of {itemName}: ");
            decimal itemPrice = Convert.ToDecimal(Console.ReadLine());
            itemPrices.Add(itemPrice);

            Console.Write($"Enter the quantity of {itemName}: ");
            int itemQuantity = Convert.ToInt32(Console.ReadLine());
            itemQuantities.Add(itemQuantity);

            Console.WriteLine();
        }
    }

    static void UpdateStock()
    {
        Console.Write("Enter the index of the item to update: ");
        int index = Convert.ToInt32(Console.ReadLine());

        if (index >= 0 && index < groceryItems.Count)
        {
            Console.WriteLine($"Updating item at index {index}:");
            Console.WriteLine($"Item Name: {groceryItems[index]}");
            Console.WriteLine($"Price: {itemPrices[index]}");
            Console.WriteLine($"Quantity: {itemQuantities[index]}");

            Console.WriteLine("Enter the updated information:");

            Console.Write("Enter the new item name (or leave blank to keep the existing name): ");
            string itemName = Console.ReadLine();
            if (!string.IsNullOrEmpty(itemName))
            {
                groceryItems[index] = itemName;
            }

            Console.Write("Enter the new price (or leave blank to keep the existing price): ");
            string priceInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(priceInput))
            {
                decimal itemPrice = Convert.ToDecimal(priceInput);
                itemPrices[index] = itemPrice;
            }

            Console.Write("Enter the new quantity (or leave blank to keep the existing quantity): ");
            string quantityInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(quantityInput))
            {
                int itemQuantity = Convert.ToInt32(quantityInput);
                itemQuantities[index] = itemQuantity;
            }

            Console.WriteLine("Item updated successfully!");
        }
        else
        {
            Console.WriteLine("Invalid item index.");
        }
    }

    static void DeleteStock()
    {
        Console.Write("Enter the index of the item to delete: ");
        int index = Convert.ToInt32(Console.ReadLine());

        if (index >= 0 && index < groceryItems.Count)
        {
            groceryItems.RemoveAt(index);
            itemPrices.RemoveAt(index);
            itemQuantities.RemoveAt(index);

            Console.WriteLine("Item deleted successfully!");
        }
        else
        {
            Console.WriteLine("Invalid item index.");
        }
    }

    static void ViewStockList()
    {
        Console.WriteLine("=================== Grocery List =================");
        Console.WriteLine("| Index |   Item Name   |   Price   |  Quantity  |");
        Console.WriteLine("|-------|---------------|-----------|------------|");

        for (int i = 0; i < groceryItems.Count; i++)
        {
            Console.WriteLine($"| {i,5} | {groceryItems[i],13} | {itemPrices[i],9:C2} | {itemQuantities[i],10} |");
        }

        Console.WriteLine("=================================================");
    }

    static void ComputeTotalCost()
    {
        decimal totalCost = 0;

        for (int i = 0; i < groceryItems.Count; i++)
        {
            decimal itemTotal = itemPrices[i] * itemQuantities[i];
            totalCost += itemTotal;
        }

        Console.WriteLine($"Total Cost: {totalCost:C2}");
        Console.WriteLine();
    }

    static void SaveGroceryListToFile()
{
    if (!isShopOwnerLoggedIn)
    {
        Console.WriteLine("Shop Owner not logged in. Please log in first.");
        return;
    }

    Console.WriteLine("Enter your phone number: ");
    string phoneNumber = Console.ReadLine();

    string folderPath = "ShopOwner";
    string fileName = $"{phoneNumber}.txt";
    string filePath = Path.Combine(folderPath, fileName);

    try
    {
        Directory.CreateDirectory(folderPath);

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Write the grocery item list to the file
            writer.WriteLine("=========================== Item List =========================");
            writer.WriteLine("| Index |   Item Name   |   Price   |  Quantity  |  Subtotal  |");
            writer.WriteLine("|-------|---------------|-----------|------------|------------|");

            decimal totalCost = 0;

            for (int i = 0; i < groceryItems.Count; i++)
            {
                decimal subtotal = itemPrices[i] * itemQuantities[i];
                totalCost += subtotal;

                writer.WriteLine($"| {i,5} | {groceryItems[i],13} | {itemPrices[i],9:C2} | {itemQuantities[i],10} | {subtotal,10:C2} |");
            }

            writer.WriteLine("================================================================");

            writer.WriteLine($"Total cost of the grocery list: {totalCost:C2}");

            Console.WriteLine("Grocery list saved to file successfully!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error occurred while saving the grocery list: {ex.Message}");
    }
}

    static void LoadDataFromFiles()
    {
        string filePath = "ShopOwner/ShopOwner_Details.txt";

        if (File.Exists(filePath))
        {
            shopOwners = new List<string>(File.ReadAllLines(filePath));
        }
        else
        {
            Console.WriteLine("Shop Owner data file not found.");
        }
    }
}

