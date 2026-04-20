#  Shopping List Management Application

A modern web application for creating and managing shopping lists with real-time categorization, built with ASP.NET Core and containerized with Docker.

---

##  Quick Start

### Prerequisites
- Docker Desktop installed ([Download](https://www.docker.com/products/docker-desktop))
- Git (for cloning)

### Launch the App (30 seconds)

```bash
# Clone the repository
git clone <your-repo-url>
cd dotNet-projektni

# Start all services
docker-compose up -d

# Visit the app
open http://localhost
```

**Done!** The app is ready with demo data pre-loaded.

---

##  Demo Login Credentials

The app comes pre-loaded with a demo account for immediate testing:

| Field | Value |
|-------|-------|
| **Email** | `demo@shopping.local` |
| **Password** | `DemoPassword123!` |

### Demo Data Included

✅ **5 Pre-loaded Shopping Lists:**
1. Weekly Groceries (9 items)
2. Party Supplies (9 items)
3. Office Supplies (9 items)
4. Gym & Fitness (6 items)
5. Home Maintenance (7 items)

✅ **8 Pre-populated Categories:**
- Fruits & Vegetables
- Dairy & Eggs
- Bakery
- Meat & Fish
- Beverages
- Snacks
- Frozen Foods
- Household

---

## ️ Architecture

### Tech Stack

**Backend Framework:**
- **ASP.NET Core 8.0** - Modern, high-performance web framework
- **C# 12** - Latest language features and improvements

**Database:**
- **Entity Framework Core 8.0** - ORM for database access
- **Database Migrations** - Version control for database schema
- **SQL Server 2022 Express** - Reliable relational database

**Authentication & Authorization:**
- **ASP.NET Identity** - User authentication and management
- **Role-Based Access Control** - Secure authorization

**Web Server & Containerization:**
- **Docker** - Containerization for consistent deployment
- **Docker Compose** - Multi-container orchestration
- **Nginx** - Reverse proxy and load balancing

**Frontend:**
- **ASP.NET MVC** - Server-side rendering
- **Razor Views** - Template engine
- **Bootstrap 5** - Responsive UI framework
- **jQuery** - Client-side interactions

---

## ️ Database Architecture

### Entity Framework Core Features

**Database Migrations:**
```
Automatic schema versioning and updates
Rollback capabilities
Development-friendly change tracking
```

**Migrations Workflow:**
- Creates migrations automatically on app startup
- Applies schema changes without manual intervention
- Demo data seeds automatically on first run
- Idempotent seeding (safe to restart)

**Entity Relationships:**
```
User → ShoppingList (1-to-Many, Cascade Delete)
ShoppingList → Item (1-to-Many, Cascade Delete)
Category → Item (1-to-Many, Set Null on Delete)
```

**Connection String:**
```
Server=sqlserver,1433;Database=ShoppingListDB;User Id=sa;Password=<from .env>;
```

---

##  Docker & Containerization

### Multi-Container Architecture

**Services:**
1. **Nginx Reverse Proxy** (Port 80)
   - Routes traffic to application
   - Domain-based routing (test.mydomain.com)
   - Load balancing

2. **ASP.NET Core Application**
   - Auto-applies migrations on startup
   - Auto-seeds demo data on first run
   - Health checks implemented
   - Auto-restart on failure

3. **SQL Server 2022 Express** (Port 1433)
   - Persistent volume storage
   - Health checks configured
   - Pre-configured authentication

### Docker Compose Features

**Auto-Management:**
```yaml
Automatic service startup order
Health checks and dependencies
Container restart policies
Data persistence across restarts
```

**Environment Configuration:**
```bash
docker-compose up -d          # Start all services
docker-compose down           # Stop and remove containers
docker-compose logs -f web    # View app logs
docker-compose ps            # Check status
```

**Volume Persistence:**
```
Database data persists in named volume
Survives container restarts
Ready for production backups
```

---

##  Application Features

### CRUD Operations

| Operation | Endpoint | Description |
|-----------|----------|-------------|
| **Read All** | `GET /List` | View all shopping lists |
| **Read One** | `GET /List/Details/{id}` | View specific list with items |
| **Create** | `POST /List/Create` | Add new shopping list |
| **Update** | `POST /List/Edit/{id}` | Modify list details |
| **Delete** | `POST /List/Delete/{id}` | Remove shopping list |

### Additional Features

- ✅ Item categorization
- ✅ Quantity tracking (with units)
- ✅ Purchase status marking
- ✅ Responsive design
- ✅ User authentication
- ✅ Automatic timestamps

---

##  Technology Details

### ASP.NET Core Features Used

**Dependency Injection:**
- Service registration in `Program.cs`
- Constructor injection in controllers
- Scoped service lifetime for DbContext

**Configuration Management:**
```csharp
builder.Configuration.GetConnectionString("DefaultConnection")
builder.Configuration["DB_PASSWORD"]
Environment-based appsettings
```

**Middleware Pipeline:**
```
Static Files → Routing → Authorization → Endpoint Mapping
```

**Model Binding & Validation:**
```csharp
[Bind("Name,Description,UserId")]
[ValidateAntiForgeryToken]
```

### Entity Framework Core

**DbContext Configuration:**
```csharp
public DbSet<ShoppingList> ShoppingLists { get; set; }
public DbSet<Item> Items { get; set; }
public DbSet<Category> Categories { get; set; }
public DbSet<User> ApplicationUsers { get; set; }
```

**Relationship Configuration:**
```csharp
builder.Entity<ShoppingList>()
    .HasOne(s => s.User)
    .WithMany(u => u.ShoppingLists)
    .HasForeignKey(s => s.UserId)
    .OnDelete(DeleteBehavior.Cascade);
```

**Query Operations:**
```csharp
_context.ShoppingLists
    .Include(s => s.ShoppingListItems)
    .Include(s => s.User)
    .ToListAsync()
```

### ASP.NET Identity

**User Management:**
```csharp
UserManager<IdentityUser> - Create, update, delete users
SignInManager<IdentityUser> - Authentication
Role-based authorization
```

**Authentication Features:**
```
Email confirmation (configurable)
Password hashing (PBKDF2)
Account lockout after failed attempts
```

---

## ️ Development Setup

### Local Development (Without Docker)

```bash
# Restore packages
dotnet restore

# Apply migrations
dotnet ef database update

# Run the app
dotnet run

# Access at: https://localhost:7xxx
```

### Running Migrations

```bash
# Add a new migration
dotnet ef migrations add MigrationName

# Apply to database
dotnet ef database update

# Revert last migration
dotnet ef migrations remove
```

---

##  Project Structure

```
dotNet-projektni/
├── Program.cs                      ← DI setup, migrations, seeding
├── docker-compose.yml              ← Multi-container orchestration
├── Dockerfile                      ← App container definition
├── nginx.conf                      ← Reverse proxy configuration
├── .env                            ← Environment variables (secrets)
├── .env.example                    ← Safe template
│
├── Controllers/
│   ├── HomeController.cs           ← Landing page
│   └── ListController.cs           ← CRUD operations
│
├── Data/
│   ├── ApplicationDbContext.cs     ← EF Core context
│   └── Migrations/                 ← Database schema versions
│
├── Models/
│   ├── ShoppingList.cs
│   ├── Item.cs
│   ├── Category.cs
│   └── User.cs
│
├── Services/
│   └── DemoDataSeedingService.cs   ← Auto-population logic
│
└── Views/
    ├── Home/
    ├── List/
    └── Shared/
```

---

##  Security Features

**Environment Variables:**
- ✅ Database password in `.env` (not in code)
- ✅ `.env` excluded from git via `.gitignore`
- ✅ `.env.example` as safe template

**Authentication:**
- ✅ ASP.NET Identity for secure user management
- ✅ Password hashing with PBKDF2
- ✅ CSRF protection with `[ValidateAntiForgeryToken]`

**Data Protection:**
- ✅ Entity validation
- ✅ Cascade delete relationships
- ✅ Foreign key constraints

---

##  Dependencies

### NuGet Packages

```xml
Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore 8.0.11
Microsoft.AspNetCore.Identity.EntityFrameworkCore 8.0.11
Microsoft.AspNetCore.Identity.UI 8.0.11
Microsoft.EntityFrameworkCore.SqlServer 8.0.11
Microsoft.EntityFrameworkCore.Tools 8.0.11
```

### Docker Images

```
nginx:alpine                              ← Lightweight web server
mcr.microsoft.com/mssql/server:2022      ← SQL Server database
<local image>                             ← ASP.NET Core app
```

---

##  Deployment

### Docker Deployment (Production-Ready)

```bash
# Build image
docker build -t shoppinglist-app .

# Run container
docker run -d -p 80:80 shoppinglist-app

# Or use docker-compose
docker-compose -f docker-compose.yml up -d
```

### Deployment Checklist

- [ ] Change database password in `.env`
- [ ] Configure custom domain
- [ ] Enable HTTPS/SSL
- [ ] Set up database backups
- [ ] Configure monitoring and logging
- [ ] Review security settings
- [ ] Test all functionality

---

##  Documentation

| Document | Purpose |
|----------|---------|
| **README.md** | This file - Overview and tech stack |
| **START-HERE-DEMO.md** | Quick start guide |
| **DEMO-SETUP.md** | Detailed setup instructions |
| **ENV-REFERENCE.md** | Environment variables guide |
| **DEPLOYMENT.md** | Production deployment guide |
| **AGENTS.md** | Developer reference |

---

##  Contributing

1. Clone the repository
2. Create a feature branch
3. Make your changes
4. Test locally with Docker
5. Submit a pull request

---

##  Troubleshooting

### Common Issues

**Docker daemon not running:**
```bash
# Start Docker Desktop from Start menu or:
docker ps
```

**Port 80 already in use:**
```bash
# Modify docker-compose.yml:
ports:
  - "8080:80"
```

**Database connection failed:**
```bash
# Restart database
docker-compose restart sqlserver
docker-compose restart web
```

---

##  Version Information

| Component | Version |
|-----------|---------|
| .NET Framework | 8.0 |
| Entity Framework Core | 8.0.11 |
| ASP.NET Core | 8.0 |
| SQL Server | 2022 Express |
| Docker | Latest |
| Nginx | Alpine |

---

##  License

This project is provided as-is for educational and demonstration purposes.

---

## ✨ Key Features Summary

✅ **Database Migrations** - Automatic schema versioning  
✅ **Entity Framework Core** - Powerful ORM for data access  
✅ **Docker Containerization** - Consistent deployment  
✅ **ASP.NET Identity** - Secure authentication  
✅ **MVC Architecture** - Clean code organization  
✅ **Pre-loaded Demo Data** - Immediate testing capability  
✅ **Responsive UI** - Works on all devices  
✅ **Environment Configuration** - Secure secret management  

---

##  Quick Commands

```bash
# Start demo
docker-compose up -d

# View logs
docker-compose logs -f web

# Check status
docker-compose ps

# Stop services
docker-compose stop

# Rebuild
docker-compose up --build -d

# Access app
open http://localhost

# Login with
# Email: demo@shopping.local
# Password: DemoPassword123!
```

---

**Status**: ✅ Production Ready  
**Last Updated**: 2026-04-19  
**Version**: 1.0.0  

 **Ready to use immediately with demo data!** 

