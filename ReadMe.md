# 🧾 InventoryApp

A simple, console-based inventory management system built with C#.  
It allows users to add, view, delete, and export product information to CSV or text files in a structured format.

---

## 🚀 Features

- Add new products with serial number, name, quantity, and price
- View products in a neatly tabulated format
- Update or delete existing products
- Export product list to a `.csv` or `txt` file
- Automatically load saved products on startup
- Currency formatting for ₦ (Naira)

---

## 💻 Technologies

- Language: C#
- Platform: .NET 6+ / .NET Core
- Environment: Visual Studio Code

---

## 📂 File Structure
InventoryApp/ 
  ├── Program.cs # Main application logic 
  ├── products.txt # Auto-generated file for product data 
  ├── README.md # Project documentation 
  |── InventoryApp.csproj # Project config file


---

## ⚙️ How to Run

1. **Clone the repo**:
   ```bash
   git clone https://https://github.com/IyanuCode/InventoryApp
cd InventoryApp
dotnet run

When exported, the CSV will look like this:
Serial          | Name                 | Qty   | Price     
------------------------------------------------------------
SN-1001         | maggi                | 38    | ₦938.00    
SN-1000         | mouse                | 8     | ₦1,000.00  
SN-1004         | key                  | 9     | ₦50.00     
SN-1002         | pot                  | 9     | ₦300.00    


Author
Adeusi Iyanu : https://github.com/IyanuCode/InventoryApp


---

License
This project is open-source and available under the MIT License.
