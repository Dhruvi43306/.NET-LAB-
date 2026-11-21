using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.Models
{
    internal class Item
    {
        public int ItemId;
        public string ItemName;
        public decimal Price;
        public int Quantity;


    }
}

//using ConsoleApp2.Models;

//namespace ConsoleApp1
//{
//    internal class Program
//    {
//        static List<Item> items = new List<Item>();
//        static void Main(string[] args)
//        {
//            while (true)
//            {
//                Console.WriteLine("\n===== SHOPPING CART SYSTEM =====");
//                Console.WriteLine("1. Add Item");
//                Console.WriteLine("2. Remove Item");
//                Console.WriteLine("3. View Cart");
//                Console.WriteLine("4. Calculate Total with Discount");
//                Console.WriteLine("5. Exit");

//                Console.Write("\nEnter your choice: ");
//                int choice = Convert.ToInt32(Console.ReadLine());

//                switch (choice)
//                {
//                    case 1:
//                        AddItem();
//                        break;

//                    case 2:
//                        RemoveItem();
//                        break;

//                    case 3:
//                        viewCart();
//                        break;

//                    case 4:
//                        CalculateTotal();
//                        break;

//                    case 5:
//                        Console.WriteLine("Thank you for shopping!");
//                        return;

//                    default:
//                        Console.WriteLine("Invalid choice!");
//                        break;

//                }
//                static void AddItem()
//                {
//                    Item item = new Item();

//                    Console.Write("\nEnter Item ID: ");
//                    item.ItemId = Convert.ToInt32(Console.ReadLine());


//                    Console.Write("Enter Item Name: ");
//                    item.ItemName = Console.ReadLine();

//                    Console.Write("Enter Item Price: ");
//                    item.Price = Convert.ToDecimal(Console.ReadLine());
//                    Console.Write("Enter Quantity: ");
//                    item.Quantity = Convert.ToInt32(Console.ReadLine());

//                    items.Add(item);
//                    Console.WriteLine("Item added successfully!");

//                }


//                static void RemoveItem()
//                {
//                    Console.Write("\nEnter Item ID to Remove: ");
//                    int id = Convert.ToInt32(Console.ReadLine());

//                    int index = FindIndex(id);
//                    if (index == -1)
//                    {
//                        Console.WriteLine("Item not found!");
//                    }
//                    else
//                    {
//                        items.RemoveAt(index);
//                        Console.WriteLine("Item removed successfully!");
//                    }
//                }



//                static void viewCart()
//                {
//                    decimal subtotal = 0;

//                    Console.WriteLine("\n========== SHOPPING CART ==========");

//                    foreach (Item item in items)
//                    {
//                        decimal itemTotal = item.Price * item.Quantity;

//                        Console.WriteLine("-----------------------------------");
//                        Console.WriteLine("Item ID   : " + item.ItemId);
//                        Console.WriteLine("Item Name : " + item.ItemName);
//                        Console.WriteLine("Price     : " + item.Price);
//                        Console.WriteLine("Quantity  : " + item.Quantity);
//                        Console.WriteLine("Total     : " + itemTotal);

//                        subtotal = subtotal + itemTotal;
//                    }
//                    Console.WriteLine("-----------------------------------");
//                    Console.WriteLine("Subtotal: " + subtotal);

//                }
//                static void CalculateTotal()
//                {
//                    if (items.Count == 0)
//                    {
//                        Console.WriteLine("\nYour cart is empty!");
//                        return;
//                    }

//                    decimal subtotal = 0;

//                    foreach (Item item in items)
//                    {
//                        subtotal = subtotal + (item.Price * item.Quantity);
//                    }

//                    Console.Write("\nEnter Discount Percentage (0-100): ");
//                    decimal discountPercentage =
//                        Convert.ToDecimal(Console.ReadLine());

//                    if (discountPercentage < 0 || discountPercentage > 100)
//                    {
//                        Console.WriteLine("Invalid discount percentage!");
//                        return;
//                    }

//                    decimal discountAmount =
//                        subtotal * discountPercentage / 100;

//                    decimal finalAmount = subtotal - discountAmount;

//                    Console.WriteLine("\n========== BILL DETAILS ==========");
//                    Console.WriteLine("Subtotal        : " + subtotal);
//                    Console.WriteLine("Discount        : " + discountAmount);
//                    Console.WriteLine("Final Amount    : " + finalAmount);
//                    Console.WriteLine("==================================");
//                }



//                static int FindIndex(int id)
//                {
//                    for(int i = 0; i < items.Count; i++)
//                    {
//                        if (items[i].ItemId == id)
//                        {
//                            return i;
//                        }
//                    }
//                    return -1;
//                }
//            }

//        }
//    }
//}