# Doccure — Healthcare Management System

A full-stack healthcare management platform built with ASP.NET Core 9 microservices. Doccure provides an admin panel for managing doctors, patients, appointments, branches, prescriptions, medicines, and a pharmacy shop — with a public-facing shop for logged-in users.

---

## Architecture

```
┌─────────────────────────────────────────────────────────┐
│                  Browser / Web UI (:7101)               │
│         ASP.NET Core MVC — Areas: Admin + Public        │
└──────────────────────┬──────────────────────────────────┘
                       │ HTTPS
              ┌────────▼────────┐
              │  Ocelot Gateway │  :5000
              └────────┬────────┘
        ┌──────────────┼──────────────────────────┐
        │              │                          │
   ┌────▼────┐   ┌─────▼─────┐   ┌───────────────▼─────────┐
   │Identity │   │  Doctor   │   │  Appointment  │  ...     │
   │  :7151  │   │  :7002    │   │    :7089      │          │
   └─────────┘   └───────────┘   └─────────────────────────┘
```

### Microservices

| Service | Port (HTTPS) | Database | Description |
|---|---|---|---|
| **IdentityService** | 7186 | SQL Server | User registration, JWT token issuance |
| **DoctorService** | 7002 | SQL Server | Doctor CRUD with education, experience, awards |
| **PatientService** | 7210 | SQL Server | Patient profiles linked to Identity |
| **AppointmentService** | 7280 | SQL Server | Appointment scheduling and status |
| **BranchService** | 7001 | SQL Server | Hospital departments / branches |
| **NurseService** | 7081 | SQL Server | Nurse management |
| **PrescriptionService** | 7106 | SQL Server | Doctor prescriptions |
| **PharmacyService** | 7268 | SQL Server | Medicine catalogue with image upload |
| **MarketService** | 7241 | SQL Server + Redis | Product catalogue + Redis cart |
| **QueueService** | 7263 | SQL Server + SignalR | Patient queue with real-time hub |
| **OrderService** | 7056 | MySQL | Order management |
| **ReviewService** | 7147 | SQL Server | Patient reviews for doctors |
| **API Gateway** | 5000 | — | Ocelot reverse proxy |
| **Web UI** | 7101 | — | Admin + public MVC frontend |

> Ports above are sourced from each service's `Properties/launchSettings.json` (`https` profile). Update this table if a service's launch profile changes.

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 9.0 |
| ORM | Entity Framework Core 9.0 |
| API Gateway | Ocelot 23.x |
| Mapping | AutoMapper 12.0 |
| Auth | JWT Bearer + Session |
| Cache | Redis (StackExchange.Redis) |
| Real-time | SignalR |
| DB (most services) | SQL Server 2022 (Docker, port 1443) |
| DB (orders) | MySQL via Pomelo |
| API Docs | Swagger / Swashbuckle 6.6 |
| Frontend | Bootstrap 5 + Bootstrap Icons |
| Serialization | Newtonsoft.Json |

---

## Features

### Admin Panel (`/Admin`)
- **Dashboard** — live stats: doctors, patients, appointments, branches; recent appointments table; top doctors list; recent patients; branch overview
- **Doctor Management** — full CRUD with image upload, education, experience, awards, locations
- **Patient Management** — patient profiles with identity + appointment data
- **Appointment Management** — view/create/update/delete appointments, status tracking
- **Branch Management** — department CRUD with theme colour, rating, capacity
- **Nurse Management** — nurse profiles
- **Prescription Management** — write and manage prescriptions
- **Medicine Management** — pharmacy catalogue with image upload
- **Product Management** — shop product catalogue with image upload and stock status
- **Order Management** — order list with patient, block/floor/room, total, status
- **Queue Management** — real-time patient queue with SignalR

### User-Facing Shop (`/Shop`)
- Product listing with search and category filter (green theme)
- Add to cart via AJAX (session-authenticated)
- Cart page with item management, quantity display, order summary
- Redis-backed cart (persisted per user session)

### Authentication
- JWT issued by IdentityService, stored in ASP.NET session
- `SessionAuthFilter` global filter protects all non-Auth routes
- Logout clears session

---

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- Docker Desktop (for SQL Server and Redis)

### Start infrastructure with Docker

```bash
# SQL Server (port 1443 — non-default to avoid conflicts)
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrongPassword!" \
  -p 1443:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest

# Redis
docker run -p 6379:6379 --name redis -d redis:alpine

# MySQL (for OrderService)
docker run -e "MYSQL_ROOT_PASSWORD=root" -e "MYSQL_DATABASE=DoccureOrders" \
  -p 3306:3306 --name mysql -d mysql:8
```

---

## Getting Started

### 1. Clone the repository

```bash
git clone <repo-url>
cd Doccure
```

### 2. Apply database migrations

Each service has its own database. Run EF migrations per service:

```bash
cd MicroServices/Doccure.IdentityService && dotnet ef database update && cd ../..
cd MicroServices/Doccure.DoctorService && dotnet ef database update && cd ../..
cd MicroServices/Doccure.PatientService && dotnet ef database update && cd ../..
cd MicroServices/Doccure.AppointmentService && dotnet ef database update && cd ../..
cd MicroServices/Doccure.BranchService && dotnet ef database update && cd ../..
cd MicroServices/Doccure.NurseService && dotnet ef database update && cd ../..
cd MicroServices/Doccure.PrescriptionService && dotnet ef database update && cd ../..
cd MicroServices/Doccure.PharmacyService && dotnet ef database update && cd ../..
cd MicroServices/Doccure.MarketService && dotnet ef database update && cd ../..
cd MicroServices/Doccure.QueueService && dotnet ef database update && cd ../..
cd MicroServices/OrderService && dotnet ef database update && cd ../..
```

### 3. Trust HTTPS developer certificate

```bash
dotnet dev-certs https --trust
```

### 4. Start all services

Open each project in a separate terminal, or configure Visual Studio's **multiple startup projects**:

```bash
# 1. API Gateway (start first)
cd ApiGetaway/Doccure.Getaway && dotnet run

# 2. Microservices (any order)
cd MicroServices/Doccure.IdentityService && dotnet run
cd MicroServices/Doccure.DoctorService && dotnet run
cd MicroServices/Doccure.PatientService && dotnet run
cd MicroServices/Doccure.AppointmentService && dotnet run
cd MicroServices/Doccure.BranchService && dotnet run
cd MicroServices/Doccure.NurseService && dotnet run
cd MicroServices/Doccure.PrescriptionService && dotnet run
cd MicroServices/Doccure.PharmacyService && dotnet run
cd MicroServices/Doccure.MarketService && dotnet run
cd MicroServices/Doccure.QueueService && dotnet run
cd MicroServices/OrderService && dotnet run

# 3. Web UI (last)
cd Frontends/Doccure.Web.UI && dotnet run
```

### 5. Open the app

| URL | Description |
|---|---|
| `https://localhost:7101` | Web UI (redirects to login) |
| `https://localhost:7101/Admin` | Admin panel (requires login) |
| `https://localhost:7101/Shop` | Pharmacy shop (requires login) |
| `https://localhost:5000` | API Gateway |
| `https://localhost:7186/swagger` | Identity API docs |
| `https://localhost:7002/swagger` | Doctor API docs |

---

## API Gateway Routes

All routes are defined in `ApiGetaway/Doccure.Getaway/ocelot.json`.

| Upstream Path | Downstream | Service |
|---|---|---|
| `/api/auth/{everything}` | `:7186` | IdentityService (`AuthController`) |
| `/api/users/{everything}` | `:7186` | IdentityService (`UsersController`) |
| `/api/roles/{everything}` | `:7186` | IdentityService (`RoleController`) |
| `/api/doctors/{everything}` | `:7002` | DoctorService |
| `/api/patients/{everything}` | `:7210` | PatientService |
| `/api/appointments/{everything}` | `:7280` | AppointmentService (`AppointmentController`) |
| `/api/appointmentdetails/{everything}` | `:7280` | AppointmentService (`AppointmentDetailsController`) |
| `/api/branches/{everything}` | `:7001` | BranchService |
| `/api/nurses/{everything}` | `:7081` | NurseService |
| `/api/prescriptions/{everything}` | `:7106` | PrescriptionService |
| `/api/medicines/upload-image` | `:7268` | PharmacyService (image upload) |
| `/api/medicines/{everything}` | `:7268` | PharmacyService |
| `/api/products/upload-image` | `:7241` | MarketService (image upload) |
| `/api/products/{everything}` | `:7241` | MarketService |
| `/api/cart/{everything}` | `:7241` | MarketService (Redis cart) |
| `/api/queues/{everything}` | `:7263` | QueueService (REST only — the `/queuehub` SignalR hub is called directly at `:7263`, not through the gateway) |
| `/api/orders/{everything}` | `:7056` | OrderService (`OrdersController`) |
| `/api/orderdetails/{everything}` | `:7056` | OrderService (`OrderDetailsController`) |
| `/api/reviews/{everything}` | `:7147` | ReviewService |

> **Note:** Specific routes (e.g. `/upload-image`) must appear before generic `{everything}` catchall routes in `ocelot.json`.
>
> **Known gap:** the Web UI's `LoginService`, `RegisterService` and `QueueService` currently call their microservices **directly** (`:7186`, `:7263`) instead of going through the gateway at `:5000` — inconsistent with every other frontend service. The gateway routes above exist and work; only those three HTTP clients still bypass it.

---

## Project Structure

```
Doccure/
├── ApiGetaway/
│   └── Doccure.Getaway/              # Ocelot API Gateway
├── Frontends/
│   └── Doccure.Web.UI/               # ASP.NET Core MVC frontend
│       ├── Areas/
│       │   └── Admin/                # Admin area
│       │       ├── Controllers/      # Doctor, Patient, Branch, Medicine, Product, ...
│       │       └── Views/            # Admin Razor views
│       ├── Controllers/              # Auth, Shop, Cart
│       ├── Services/                 # HTTP clients per microservice
│       ├── Dtos/                     # Response/request DTOs
│       ├── ViewModels/               # DashboardViewModel, AuthViewModels
│       └── Filters/                  # SessionAuthFilter
└── MicroServices/
    ├── Doccure.IdentityService/
    ├── Doccure.DoctorService/
    ├── Doccure.PatientService/
    ├── Doccure.AppointmentService/
    ├── Doccure.BranchService/
    ├── Doccure.NurseService/
    ├── Doccure.PrescriptionService/
    ├── Doccure.PharmacyService/
    ├── Doccure.MarketService/        # Products + Redis cart
    ├── Doccure.QueueService/         # Patient queue + SignalR
    ├── Doccure.ReviewService/
    └── OrderService/                 # MySQL-backed orders
```

---

## Notes

- SQL Server is mapped to port **1443** (non-standard) to avoid conflicts — configured in each service's `appsettings.json`
- Images are saved to each microservice's `wwwroot/uploads/` folder and proxied to the Web UI via `PhysicalFileProvider`
- Cart data is stored in Redis under keys `cart:{userId}` where `userId` is the JWT `sub` claim
- Most HTTP communication from the Web UI goes through the Ocelot gateway at `https://localhost:5000`, except `LoginService`, `RegisterService` and `QueueService`, which call IdentityService (`:7186`) and QueueService (`:7263`) directly — see the Known gap note above

---

## Author

**Murad Agamedov**  
Built with ASP.NET Core 9, Entity Framework Core, Ocelot, Redis, SignalR, and Bootstrap 5.
