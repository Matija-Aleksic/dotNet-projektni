# Shopping List Application

A modern web application for managing shopping lists, built with .NET 8.0 and containerized with Docker.

## Quick Start

### Prerequisites
- Docker Desktop
- Git

### Start the App

```bash
git clone <repo-url>
cd dotNet-projektni
docker-compose up -d
```

Open **http://localhost:5109**

---

## Tech Stack

| Layer | Technology |
|-------|-----------|
| **Backend** | ASP.NET Core 8.0 + C# 12 |
| **ORM** | Entity Framework Core 8.0 |
| **Database** | SQLite (with EF Core migrations) |
| **Auth** | ASP.NET Identity |
| **Frontend** | Razor Views + Bootstrap 5 + jQuery |
| **Containerization** | Docker + Docker Compose |

---

## Core Features

- ✅ **CRUD Operations** - Create, read, update, delete shopping lists and items
- ✅ **Item Categorization** - Organize items by categories
- ✅ **User Authentication** - Secure login with ASP.NET Identity
- ✅ **Database Migrations** - Version-controlled schema changes via EF Core
- ✅ **Persistent Storage** - SQLite database with Docker volume mounts
- ✅ **Health Checks** - Container and database monitoring
- ✅ **Responsive Design** - Works on all devices

---

## Advanced Technologies

### Entity Framework Core
- **Automatic Migrations**: Schema versioning and rollback capabilities
- **Relationship Management**: 1-to-Many relationships with cascade delete
- **LINQ Queries**: Type-safe database queries with `.Include()` for eager loading
- **Data Seeding**: Automatic demo data population on first run

### ASP.NET Core Architecture
- **Dependency Injection**: Service registration and constructor injection in `Program.cs`
- **Middleware Pipeline**: Static files → Routing → Authorization → Endpoint mapping
- **Model Binding & Validation**: Automatic form data binding with CSRF protection
- **Configuration Management**: Environment-based settings via `appsettings.json`

### Docker Containerization
- **Multi-Stage Dockerfile**: Optimized Alpine Linux build for minimal image size (~80MB)
- **Docker Compose**: Service orchestration with automatic startup and health checks
- **Volume Persistence**: SQLite database persists across container restarts
- **Networking**: Isolated bridge network for secure inter-service communication

### Security
- ✅ ASP.NET Identity with PBKDF2 password hashing
- ✅ CSRF protection with `[ValidateAntiForgeryToken]`
- ✅ Foreign key constraints and cascade delete relationships
- ✅ Entity validation for data integrity

---

## Database Schema

**Entity Relationships:**
```
User → ShoppingList (1-to-Many, Cascade Delete)
ShoppingList → Item (1-to-Many, Cascade Delete)
Category → Item (1-to-Many)
```

**Key Tables:**
- `AspNetUsers` - User accounts
- `ShoppingLists` - Shopping list records
- `Items` - Individual items in lists
- `Categories` - Item categories

---

## Docker Setup

### Start Services
```bash
docker-compose up --build -d
```

### Check Status
```bash
docker-compose ps
docker-compose logs -f app
```

### Health Check
```bash
curl http://localhost:5109/health
```

### Stop Services
```bash
docker-compose down
```

---

## Development

### Local Setup (Without Docker)
```bash
dotnet restore
dotnet ef database update
dotnet run
```

### Running Migrations
```bash
# Add migration
dotnet ef migrations add MigrationName

# Apply migration
dotnet ef database update

# Remove last migration
dotnet ef migrations remove
```

---

## Project Structure

```
dotNet-projektni/
├── Program.cs                      # DI setup, middleware, health endpoint
├── Dockerfile                      # Multi-stage Alpine Linux build
├── docker-compose.yml              # Container orchestration
├── deploy.ps1 / deploy.sh          # Automated deployment scripts
├── DOCKER-DEPLOYMENT.md            # Deployment documentation
│
├── Controllers/                    # MVC request handlers
│   ├── HomeController.cs
│   ├── ListController.cs
│   ├── ItemsController.cs
│   └── CategoriesController.cs
│
├── Data/                           # EF Core context & migrations
│   ├── ApplicationDbContext.cs
│   └── Migrations/
│
├── Models/                         # Data entities
│   ├── ShoppingList.cs
│   ├── Item.cs
│   ├── Category.cs
│   └── User.cs
│
└── Views/                          # Razor templates
    ├── List/
    ├── Items/
    ├── Categories/
    └── Shared/
```

---

## Deployment

### Automated Deployment (Recommended)

**Windows (PowerShell):**
```powershell
.\deploy.ps1
```

**Linux/macOS (Bash):**
```bash
chmod +x deploy.sh
./deploy.sh
```

**Quick Deploy (Linux - with Docker Compose detection):**
```bash
chmod +x quick-deploy.sh
./quick-deploy.sh
```

**Features:**
- Automated git pull (optional with `--SkipGit`)
- Auto-detects Docker Compose V2 or V1
- Build and start containers
- Wait for application health check
- Run database migrations
- Display container status

### Manual Deployment
```bash
docker compose up --build -d        # Modern syntax (V2)
# OR
docker-compose up --build -d        # Legacy syntax (V1)

docker compose logs -f app
```

### Troubleshooting Deployment

If deployment fails, check the troubleshooting guide:
```bash
cat DEPLOYMENT-TROUBLESHOOTING.md
```

**Common Issues:**
- Docker daemon not running
- Port 5109 already in use
- Docker Compose Python 3.12 compatibility (`distutils` missing)
- Health check timeout
- Database connection issues

See `DEPLOYMENT-TROUBLESHOOTING.md` for detailed solutions.

---

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/` | Home page |
| GET | `/health` | Health check |
| GET | `/List` | View all lists |
| POST | `/List/Create` | Create new list |
| GET | `/List/Details/{id}` | View list details |
| POST | `/List/Edit/{id}` | Update list |
| POST | `/List/Delete/{id}` | Delete list |

---

## Environment Variables

**In `Program.cs`:**
```csharp
ASPNETCORE_ENVIRONMENT=Production    # Environment mode
ASPNETCORE_URLS=http://+:5109        # Binding address
```

**Connection String:**
```
Data Source=app.db                   # SQLite file in container
```

---

## Troubleshooting

| Issue | Solution |
|-------|----------|
| Docker daemon not running | Start Docker Desktop |
| Port 5109 in use | Modify `docker-compose.yml` ports |
| Database errors | Check container logs: `docker-compose logs app` |
| Health check failing | Verify app logs and database connectivity |
| Container won't start | Rebuild: `docker-compose up --build -d` |

---

## Version Info

- **.NET** 8.0 (LTS)
- **Entity Framework Core** 8.0
- **Docker** Latest
- **Base Image** `mcr.microsoft.com/dotnet/aspnet:8.0-alpine`

---

**Status**: ✅ Production Ready  
**Last Updated**: 2026-04-20  
**Version**: 2.0.0 

