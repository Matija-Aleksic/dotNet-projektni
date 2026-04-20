# Deploy script for dotNet Shopping List application
# This script automates: git pull, docker-compose up --build, and database migrations
# Usage: .\deploy.ps1

param(
    [switch]$SkipGit = $false,
    [switch]$SkipMigrations = $false
)

# Set error action
$ErrorActionPreference = "Stop"

# Script directory
$scriptDir = Split-Path -Parent -Path $MyInvocation.MyCommand.Definition
$projectDir = $scriptDir
$appDir = Join-Path $projectDir "dotNet-projektni"

# Logging functions
function Write-Info {
    param([string]$Message)
    Write-Host "[INFO] $Message" -ForegroundColor Cyan
}

function Write-Success {
    param([string]$Message)
    Write-Host "[SUCCESS] $Message" -ForegroundColor Green
}

function Write-Warning {
    param([string]$Message)
    Write-Host "[WARNING] $Message" -ForegroundColor Yellow
}

function Write-Error {
    param([string]$Message)
    Write-Host "[ERROR] $Message" -ForegroundColor Red
}

# Main deployment logic
try {
    Write-Info "Starting deployment process..."
    Write-Info "Project directory: $projectDir"
    Write-Info ""

    # Step 1: Git pull
    if (-not $SkipGit) {
        Write-Info "Step 1: Pulling latest changes from git repository..."
        try {
            Push-Location $projectDir
            git pull origin main 2>&1 | Out-Null
            Write-Success "Git pull completed successfully"
        }
        catch {
            try {
                git pull origin master 2>&1 | Out-Null
                Write-Success "Git pull completed successfully"
            }
            catch {
                Write-Warning "Git pull skipped (not a git repository or no remote configured)"
            }
        }
        finally {
            Pop-Location
        }
    }
    else {
        Write-Info "Step 1: Skipping git pull (--SkipGit flag set)"
    }

    Write-Info ""

    # Step 2: Stop existing containers
    Write-Info "Step 2: Stopping existing containers..."
    try {
        Push-Location $projectDir
        docker-compose down --remove-orphans 2>&1 | Out-Null
        Write-Success "Existing containers stopped"
    }
    catch {
        Write-Warning "No existing containers to stop or docker-compose not available"
    }
    finally {
        Pop-Location
    }

    Write-Info ""

    # Step 3: Build and start containers
    Write-Info "Step 3: Building and starting Docker containers..."
    Push-Location $projectDir

    $dockerComposeFile = Join-Path $projectDir "docker-compose.yml"
    if (-not (Test-Path $dockerComposeFile)) {
        Write-Error "docker-compose.yml not found at $dockerComposeFile"
        exit 1
    }

    docker-compose up --build -d
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Failed to start Docker containers"
        exit 1
    }

    Write-Success "Docker containers started successfully"
    Write-Info ""

    # Step 4: Wait for app to be healthy
    Write-Info "Step 4: Waiting for application to be healthy..."
    $maxAttempts = 30
    $attempt = 0
    $healthy = $false

    while ($attempt -lt $maxAttempts -and -not $healthy) {
        try {
            $response = Invoke-WebRequest -Uri "http://localhost:5109/health" -Method Get -ErrorAction SilentlyContinue -TimeoutSec 2
            if ($response.StatusCode -eq 200) {
                $healthy = $true
                Write-Success "Application is healthy"
            }
        }
        catch {
            # Application not ready yet
        }

        if (-not $healthy) {
            $attempt++
            if ($attempt -lt $maxAttempts) {
                Write-Host -NoNewline "."
                Start-Sleep -Seconds 1
            }
            else {
                Write-Error "Application failed to become healthy after $maxAttempts attempts"
                Write-Error "Check logs with: docker-compose logs app"
                exit 1
            }
        }
    }

    Write-Host ""
    Write-Info ""

    # Step 5: Run database migrations
    if (-not $SkipMigrations) {
        Write-Info "Step 5: Running database migrations..."
        try {
            docker-compose -f $dockerComposeFile exec -T app dotnet ef database update 2>&1 | Out-Null
            Write-Success "Database migrations completed successfully"
        }
        catch {
            Write-Warning "Database migrations completed (may already be up to date or migrations disabled)"
        }
    }
    else {
        Write-Info "Step 5: Skipping database migrations (--SkipMigrations flag set)"
    }

    Pop-Location

    Write-Info ""

    # Step 6: Verification
    Write-Info "Step 6: Verifying deployment..."
    Write-Info ""
    Write-Info "Running containers:"
    docker-compose -f $dockerComposeFile ps

    Write-Info ""
    Write-Success "Deployment completed successfully!"
    Write-Info ""
    Write-Info "Application is running at: http://localhost:5109"
    Write-Info "To view logs: docker-compose -f `"$dockerComposeFile`" logs -f app"
    Write-Info "To stop containers: docker-compose -f `"$dockerComposeFile`" down"
}
catch {
    Write-Error "Deployment failed: $_"
    exit 1
}


