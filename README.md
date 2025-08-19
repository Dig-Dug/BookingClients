# BookingClients

![Book Icon](https://s26162.pcdn.co/wp-content/uploads/sites/2/2022/08/Books.jpg)
# 📚 Book Management API

A simple ASP.NET Core Web API for managing books.  
Currently returns a list of hardcoded books via a REST endpoint. Future versions will support full CRUD operations and database persistence.

---

## 🚀 Tech Stack
- **C# / .NET 7** (ASP.NET Core Web API)  
- **Controller-based API structure**  
- **Dependency Injection (DI)** for services  
- **Book model & BookService** (in-memory for now)  

---

## 📂 Project Structure

- **/Controllers** → API controllers (e.g., `BooksController.cs`)  
- **/Services** → Business logic (e.g., `BookService.cs`)  
- **/Models** → Data models (e.g., `Book.cs`)  
- **Program.cs** → Application entry point & Dependency Injection setup  


## 🔧 How to Run
1. Clone this repository:
   ```bash
   git clone <your-repo-url>
   cd <your-project-folder>