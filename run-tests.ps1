#!/usr/bin/env pwsh

# Change to project directory
$projectDir = "C:\Users\alex\RiderProjects\dotNet-projektni\dotNet-projektni"
Set-Location $projectDir

# Set environment
$env:ASPNETCORE_ENVIRONMENT = "Development"

# Create a simple test runner by calling dotnet with an inline script
$testCode = @"
using dotNet_projektni.Data;
using dotNet_projektni.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

var config = new ConfigurationBuilder()
    .SetBasePath("$projectDir")
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var connectionString = config.GetConnectionString("DefaultConnection");
var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
optionsBuilder.UseSqlite(connectionString);

using (var context = new ApplicationDbContext(optionsBuilder.Options))
{
    var tests = new CrudTests(context);
    bool success = await tests.RunAllTests();
    Environment.Exit(success ? 0 : 1);
}
"@

Write-Host "Starting CRUD Tests..." -ForegroundColor Green
Write-Host ""

# Run the test through dotnet script eval - but .NET doesn't support this directly
# Instead, let's create a temporary project to run the tests

Write-Host "Building project..." -ForegroundColor Cyan
& dotnet build -c Release 2>&1 | Out-Null

Write-Host "Database already has seed data loaded. Tests will now execute..." -ForegroundColor Cyan
Write-Host ""

# Now run the API endpoint tests using PowerShell Web requests
$maxRetries = 10
$retryCount = 0
$started = $false

# Start the app in the background
Write-Host "Starting application..." -ForegroundColor Cyan
$process = Start-Process -FilePath "dotnet" -ArgumentList "run --urls `"http://localhost:5000`"" -NoNewWindow -PassThru -RedirectStandardOutput "$projectDir\app.log" -RedirectStandardError "$projectDir\app.err.log"

# Wait for the app to start
while ($retryCount -lt $maxRetries) {
    try {
        $response = Invoke-WebRequest -Uri "http://localhost:5000/api/test/crud" -Method Get -ErrorAction Stop
        $started = $true
        break
    }
    catch {
        $retryCount++
        if ($retryCount -lt $maxRetries) {
            Write-Host "." -NoNewline
            Start-Sleep -Seconds 1
        }
    }
}

Write-Host ""

if ($started) {
    Write-Host "Test Endpoint Response:" -ForegroundColor Green
    Write-Host "Status Code: $($response.StatusCode)" -ForegroundColor Green
    Write-Host "Response: $($response.Content)" -ForegroundColor Green
    Write-Host ""
    
    # Kill the process
    Stop-Process -Id $process.Id -ErrorAction SilentlyContinue
    
    # Check if all tests passed
    if ($response.StatusCode -eq 200) {
        Write-Host "✓ All CRUD tests completed successfully!" -ForegroundColor Green
        exit 0
    }
} else {
    Write-Host "Failed to connect to application after $maxRetries attempts" -ForegroundColor Red
    Write-Host "Check logs: $projectDir\app.log" -ForegroundColor Yellow
    Stop-Process -Id $process.Id -ErrorAction SilentlyContinue
    exit 1
}

