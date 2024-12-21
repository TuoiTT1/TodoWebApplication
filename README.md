Code backend Web API CRUD
Tech:  .NET 8
SQL : SQL Server
ORM: Dapper
Pattern: CQRS, Mediator
Architecture: DDD

sample structure src:
src/
├── WebAPI/                     # Lớp API chính
│   ├── Controllers/            # Controllers xử lý yêu cầu HTTP
│   ├── Filters/                # Lớp Middleware, Exception Filters
│   ├── Models/                 # DTOs cho API (request/response)
│   ├── Program.cs              # Điểm khởi động chính
│   └── appsettings.json        # Tệp cấu hình
│
├── Application/                # Lớp ứng dụng (CQRS, Mediator)
│   ├── Commands/               # Command và Command Handlers
│   │   ├── Employees/
│   │   │   ├── CreateEmployeeCommand.cs
│   │   │   ├── UpdateEmployeeCommand.cs
│   │   │   └── DeleteEmployeeCommand.cs
│   ├── Queries/                # Query và Query Handlers
│   │   ├── Employees/
│   │   │   ├── GetEmployeesQuery.cs.cs
│   │   │   └── GetEmployeesDTO.cs
│   ├── Interfaces/             # Các Interface dùng trong Application Layer
│   └── Behaviors/              # Middleware CQRS như Logging, Validation
│
├── Domain/                     # Lớp Domain (DDD)
│   ├── Entities/               # Các Entity (Aggregate Roots)
│   │   ├── Employee.cs
│   ├── ValueObjects/           # Value Objects
│   ├── Exceptions/             # Domain Exceptions
│   ├── Services/               # Domain Services
│   └── Interfaces/             # Interfaces cho các lớp bên dưới (Repository, UnitOfWork)
│   │      └── IEmployeeRepository.cs
│   │
├── Infrastructure/             # Lớp Infrastructure (Dapper, SQL, External Services)
│   ├── Persistence/            # Giao tiếp cơ sở dữ liệu
│   │   ├── Repositories/       # Các Repository (Dapper)
│   │   │   └── EmployeeRepository.cs
│   ├── Configurations/         # Cấu hình (Dapper, DB Connection)
│   ├── Migrations/             # Các script migration cho SQL Server
│   ├── Services/               # Tích hợp với các dịch vụ bên ngoài (ví dụ: Email, Payment)
│   └── Logging/                # Tích hợp logging framework (Serilog, NLog)
│
└── Tests/                      # Kiểm thử (Unit Tests, Integration Tests)
├── UnitTests/              # Unit Tests cho từng lớp riêng biệt
│   ├── Application/
│   ├── Domain/
│   ├── Infrastructure/
├── IntegrationTests/       # Integration Tests (API + Database)
│   ├── WebAPI/
└── TestUtilities/          # Tiện ích hỗ trợ kiểm thử (mock, stub)
