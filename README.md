# 🍞 Bakery Solutions

**Bakery Solutions** is an entry-level web application designed to demonstrate a software solution for managing orders in a bakery. The app is built using ASP.NET MVC 4.7.2 and follows a layered architecture: `Web`, `BusinessLogic`, and `Domain`, with data persistence handled via Entity Framework and SQL Server.

---

## 🔧 Features

1. **Authentication (Login/Register)**
   - Users can create new accounts and log in.
   - User sessions are maintained after login.
   - Basic data validation is implemented.

2. **Product Selection**
   - Available products are displayed in a simple list.
   - Users can add products to a virtual cart.

3. **Order Placement**
   - Orders are finalized and saved to the database.
   - Each order is linked to the corresponding user.
   - Users can view their past orders.

---

## 🧱 Technologies Used

- ASP.NET MVC 4.7.2
- C#
- HTML/CSS/JavaScript
- Entity Framework 6
- SQL Server LocalDB

---

## 🗃 Project Structure

- `eUseControl.Web` – User interface (Views + Controllers)
- `eUseControl.BusinessLogic` – Business logic layer (validations, rules)
- `eUseControl.Domain` – Data access layer (Entity Framework + Migrations)

---

## 💾 Database

The application uses Entity Framework Code First to manage the database. Upon running the application, required tables (`Users`, `Products`, `Orders`, etc.) are automatically generated.

---

## ▶️ How to Run the Application

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/Solutions-For-Bakeries.git
