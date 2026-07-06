# 📅 MVC-WebAPI-EventManagement

> A **Decoupled Event Management System** built with **ASP.NET Core MVC** and **ASP.NET Core Web API**. The frontend consumes backend services via **HttpClient**, following RESTful architecture principles.

---

## 📌 About Project

MVC-WebAPI-EventManagement is a decoupled web application where the **ASP.NET Core MVC frontend** communicates with the **ASP.NET Core Web API backend** instead of accessing the database directly.

The project demonstrates modern client-server architecture, API consumption, DTO usage, and RESTful communication while providing an event management platform.

---

## 📸 Project Screenshots

<p align="center">
<img width="554" height="404" alt="Ekran görüntüsü 2026-07-06 231206" src="https://github.com/user-attachments/assets/4107c926-847b-49f2-8ddc-06b1f34aa580" />

</p>

<p align="center">
<img width="583" height="182" alt="Ekran görüntüsü 2026-07-06 231230" src="https://github.com/user-attachments/assets/13b8e765-1cf4-4554-999f-4b0f0a0989ed" />

</p>

<p align="center">
<img width="550" height="223" alt="Ekran görüntüsü 2026-07-06 231623" src="https://github.com/user-attachments/assets/12df982b-c433-47d3-8512-fdcd6b569c29" />


</p>

<p align="center">
  <img width="562" height="260" alt="Ekran görüntüsü 2026-07-06 231636" src="https://github.com/user-attachments/assets/13e97242-9123-4793-81f9-5712e8917572" />



</p>

<p align="center">


<img width="554" height="404" alt="Ekran görüntüsü 2026-07-06 231206" src="https://github.com/user-attachments/assets/99224eda-8cf2-42e0-91c5-e22af54fbf87" />
</p>

---

## 🚀 Features

- 🌐 Decoupled MVC + Web API Architecture
- 📅 Event Management
- 🏢 Category & Venue Management
- 🔄 Full CRUD Operations
- 🔗 RESTful API Communication
- 📦 DTO (Data Transfer Object) Pattern
- ⚡ HttpClient Integration
- 📄 JSON Data Exchange
- 📱 Responsive Bootstrap Interface

---

## 🛠️ Technologies

- .NET 8
- C#
- ASP.NET Core MVC
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- HttpClient
- RESTful API
- JSON
- DTO Pattern
- Swagger (OpenAPI)
- Bootstrap 5

---

## 🏗️ System Architecture

```text
        User
          │
          ▼
 ASP.NET Core MVC
          │
     HttpClient
          │
          ▼
 ASP.NET Core Web API
          │
 Entity Framework Core
          │
          ▼
     SQL Server
```

---

## 📂 Project Structure

```text
MVC-WebAPI-EventManagement
│
├── EventManagement.API
│   ├── Controllers
│   ├── DTOs
│   ├── Models
│   └── Data
│
├── EventManagement.MVC
│   ├── Controllers
│   ├── Views
│   ├── Services
│   └── ViewModels
│
└── SQL Server
```

---

## ⚙️ Installation

Clone the repository:

```bash
git clone https://github.com/eldutberkay05/MVC-WebAPI-EventManagement.git
```

Configure the SQL Server connection string inside the **API project's appsettings.json** file.

Apply migrations:

```powershell
Update-Database
```

Configure **Multiple Startup Projects** in Visual Studio:

- EventManagement.API
- EventManagement.MVC

Run both projects simultaneously.

---

## 👨‍💻 Developer

**Berkay Eldut**

Backend Developer Candidate

ASP.NET Core MVC • ASP.NET Core Web API • Entity Framework Core • SQL Server • REST API
