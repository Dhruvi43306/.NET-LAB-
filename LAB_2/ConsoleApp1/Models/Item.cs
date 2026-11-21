using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Models
{
    internal class Item
    {
        int Item_Code;
        string Item_Name;
        int Stock_Quantity;

        public Item(int Item_Code, string Item_Name, int Stock_Quantity)
        {
            this.Item_Code = Item_Code;
            this.Item_Name = Item_Name;
            this.Stock_Quantity = Stock_Quantity;
        }

        public void Display()
        {
            Console.WriteLine("ItemCode = " + Item_Code);
            Console.WriteLine("ItemName = " + Item_Name);
            Console.WriteLine("Stock_Quantity = " + Stock_Quantity);
        }
    }
}
