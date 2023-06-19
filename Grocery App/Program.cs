// // Customer

// using System;
// using System.Collections.Generic;
// using System.IO;

// class Program
// {
//     static List<string> customers = new List<string>();
//     static List<string> groceryItems = new List<string>();
//     static List<decimal> itemPrices = new List<decimal>();
//     static List<int> itemQuantities = new List<int>();
    
//     static bool isCustomerLoggedIn = false;
  
//     static void Main()
//     {
//         LoadDataFromFiles();

//         bool exit = false;
//         while (!exit)
//         {
//             Console.WriteLine("===== Welcome to the Console App! =====");    
//             Console.WriteLine("1. Register as User"); 
//             Console.WriteLine("2. Login as User");
//             Console.WriteLine("3. Exit");
//             Console.Write("Enter your choice: ");
//             string choice = Console.ReadLine();

//             switch (choice)
//             {
//                 case "1":
//                     RegisterCustomer();
//                     break;
                
//                 case "2":
//                     LoginAsCustomer();
//                     break;

//                 case "3":
//                     exit = true;
//                     break;

//                 default:
//                     Console.WriteLine("Invalid choice. Please try again.");
//                     break;
//             }
//         }
      
//     }

//     static void RegisterCustomer()
//     {
//         Console.WriteLine("======= User Registration =======");
//         Console.Write("Enter your name: ");
//         string name = Console.ReadLine();
//         Console.Write("Enter your phone number: ");
//         string phoneNumber = Console.ReadLine();

//         string customerData = $"Name: {name}, Phone Number: {phoneNumber}";
//         customers.Add(customerData);
//         Console.WriteLine("Customer registered successfully!");

//         // Save user data to file
//         File.AppendAllText("Customer/Customer_Details.txt", customerData + Environment.NewLine);
        
//         Console.WriteLine();
//     }

//     static void LoginAsCustomer()
//     {
//         Console.WriteLine("========= Customer Login ========");
//         Console.Write("Enter your phone number: ");
//         string phoneNumber = Console.ReadLine();

//         bool found = false;
//         foreach (string customerData in customers)
//         {
//             if (customerData.Contains(phoneNumber))
//             {
//                 found = true;
//                 Console.WriteLine("Customer Login Successfully!");
//                 isCustomerLoggedIn = true;                
//                 DisplayCustomerMenu();
//                 break;
//             }
//         }

//         if (!found)
//         {
//             Console.WriteLine("Customer not found. Please register first.");
//             Console.WriteLine();
//         }
//     }

//     static void DisplayCustomerMenu()
//     {
//         bool exit = false;
//         while (!exit)
//         {
//             Console.WriteLine("========== CUSTOMER MENU ==========");
//             Console.WriteLine("0. View Shops");
//             Console.WriteLine("1. Add Item To Cart");
//             Console.WriteLine("2. View Item List");
//             Console.WriteLine("3. Compute total cost");
//             Console.WriteLine("4. Remove Item");
//             Console.WriteLine("5. Checkout");
//             Console.WriteLine("6. Logout");
    
//             Console.WriteLine("==================================");
//             Console.Write("Enter your choice (1-6): ");
//             string choice = Console.ReadLine();

//             switch (choice)
//             { 
//                 case "0":
//                 ViewShop();
//                 break;

//                 case "1":
//                     AddItem();
//                     break;
//                 case "2":
//                     ViewItem();
//                     break;
//                 case "3":
//                     ComputeTotalCost();
//                     break;
//                 case "4":
//                     RemoveItem();
//                     break;
//                 case "5":
//                     SaveGroceryListToFile();
//                     break;
//                 case "6":
//                     Logout(); // Added a new case for logout
//                     exit = true;
//                     break;
//                 default:
//                     Console.WriteLine("Invalid choice. Please try again.");
//                     break;
//             }
//         }
//     }

//     static void Logout()
//     {
//         isCustomerLoggedIn = false;
//         groceryItems.Clear();
//         itemPrices.Clear();
//         itemQuantities.Clear();
//         Console.WriteLine("Logged out as Shop Owner.");
//         Console.WriteLine();
//     }

//     static void ViewShop(){
//         string folderPath = "ShopOwner";
//         string[] txtFiles = Directory.GetFiles(folderPath, "*.txt");

//         Console.WriteLine("List of Shops:");

//          for (int i = 0; i < txtFiles.Length; i++)
//         {
//             string fileName = Path.GetFileNameWithoutExtension(txtFiles[i]);
//             Console.WriteLine($"{i + 1}. {fileName}");
//         }
        
//         Console.Write("Choose the shop: ");
//         string input = Console.ReadLine();
//         if (int.TryParse(input, out int selectedIndex))
//         {
//             int fileIndex = selectedIndex - 1;
//             if (fileIndex >= 0 && fileIndex < txtFiles.Length)
//             {
//                 string selectedFile = txtFiles[fileIndex];
//                 string fileContents = File.ReadAllText(selectedFile);

//                 Console.WriteLine($"Contents of the selected file ({Path.GetFileName(selectedFile)}):");
//                 Console.WriteLine(fileContents);
//             }
//             else
//             {
//                 Console.WriteLine("Invalid index. Please select a valid index.");
//             }
//         }
//         else
//         {
//             Console.WriteLine("Invalid input. Please enter a valid index.");
//         }
    
//     }
//     static void AddItem(){
//         Console.Write("Enter the number of grocery items to add: ");
//         int itemCount = Convert.ToInt32(Console.ReadLine());

//         for (int i = 0; i < itemCount; i++)
//         {
//             Console.Write($"Enter the name of grocery item #{i + 1}: ");
//             string itemName = Console.ReadLine();
//             groceryItems.Add(itemName);

//             Console.Write($"Enter the price of {itemName}: ");
//             decimal itemPrice = Convert.ToDecimal(Console.ReadLine());
//             itemPrices.Add(itemPrice);

//             Console.Write($"Enter the quantity of {itemName}: ");
//             int itemQuantity = Convert.ToInt32(Console.ReadLine());
//             itemQuantities.Add(itemQuantity);
//             Console.WriteLine();
            
//          }
//         Console.WriteLine("All Grocery Items Added!");
//     }

//        static void ViewItem()
//     {
//         Console.WriteLine("=================== Grocery List =================");
//         Console.WriteLine("| Index |   Item Name   |   Price   |  Quantity  |");
//         Console.WriteLine("|-------|---------------|-----------|------------|");

//         for (int i = 0; i < groceryItems.Count; i++)
//         {
//             Console.WriteLine($"| {i,5} | {groceryItems[i],13} | {itemPrices[i],9:C2} | {itemQuantities[i],10} |");
//         }

//         Console.WriteLine("====================================================");
//     }
      
//      static void ComputeTotalCost()
//     {
//         decimal totalCost = 0;

//         for (int i = 0; i < groceryItems.Count; i++)
//         {
//             decimal itemTotal = itemPrices[i] * itemQuantities[i];
//             totalCost += itemTotal;
//         }
//         Console.WriteLine($"Total Cost: {totalCost}");
//         Console.WriteLine(); 
//         }

//      static void RemoveItem()
//     {
//         Console.Write("Enter the index of the item to remove: ");
//         int index = Convert.ToInt32(Console.ReadLine());

//         if (index >= 0 && index < groceryItems.Count)
//         {
//             groceryItems.RemoveAt(index);
//             itemPrices.RemoveAt(index);
//             itemQuantities.RemoveAt(index);
//             Console.WriteLine("Item removed from the grocery list!");
//         }
//         else
//         {
//             Console.WriteLine("Invalid item index!");
//         }
//     }
   
//        static void SaveGroceryListToFile()
// {
//     if (!isCustomerLoggedIn)
//     {
//         Console.WriteLine("Customer not logged in. Please log in first.");
//         return;
//     }

//     Console.WriteLine("Enter your phone number: ");
//     string phoneNumber = Console.ReadLine();

//     string folderPath = "Customer";
//     string fileName = $"{phoneNumber}.txt";
//     string filePath = Path.Combine(folderPath, fileName);

//     try
//     {
//         Directory.CreateDirectory(folderPath);

//         using (StreamWriter writer = new StreamWriter(filePath))
//         {
//             // Write the grocery item list to the file
//             writer.WriteLine("=========================== Item List =========================");
//             writer.WriteLine("| Index |   Item Name   |   Price   |  Quantity  |  Subtotal  |");
//             writer.WriteLine("|-------|---------------|-----------|------------|------------|");

//             decimal totalCost = 0;

//             for (int i = 0; i < groceryItems.Count; i++)
//             {
//                 decimal subtotal = itemPrices[i] * itemQuantities[i];
//                 totalCost += subtotal;

//                 writer.WriteLine($"| {i,5} | {groceryItems[i],13} | {itemPrices[i],9:C2} | {itemQuantities[i],10} | {subtotal,10:C2} |");
//             }

//             writer.WriteLine("================================================================");

//             writer.WriteLine($"Total cost of the grocery list: {totalCost:C2}");

//             Console.WriteLine("Grocery list saved to file successfully!");
//         }
//     }
//     catch (Exception ex)
//     {
//         Console.WriteLine($"Error occurred while saving the grocery list: {ex.Message}");
//     }
// }
//     static void LoadDataFromFiles()
// {
//     string filePath = "Customer/Customer_Details.txt";
    
//     if (File.Exists(filePath))
//     {
//         customers = new List<string>(File.ReadAllLines(filePath));
//     }
//     else
//     {
//         Console.WriteLine("Customer data file not found.");
//     }
// }

// }
