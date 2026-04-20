#!/bin/bash

# Deploy script for dotNet Shopping List application
# This script automates: git pull, docker-compose up --build, and database migrations

set -e  # Exit on any error

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Logging functions
log_info() {
    echo -e "${BLUE}[INFO]${NC} $1"
}

log_success() {
    echo -e "${GREEN}[SUCCESS]${NC} $1"
}

log_warning() {
    echo -e "${YELLOW}[WARNING]${NC} $1"
}

log_error() {
    echo -e "${RED}[ERROR]${NC} $1"
}

# Determine docker compose command (use new 'docker compose' if available, fallback to 'docker-compose')
determine_compose_cmd() {
    if docker compose version &> /dev/null; then
        echo "docker compose"
    elif docker-compose --version &> /dev/null; then
        echo "docker-compose"
    else
        log_error "Neither 'docker compose' nor 'docker-compose' command found. Please install Docker."
        exit 1
    fi
}

# Script directory
SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
PROJECT_DIR="$SCRIPT_DIR"
APP_DIR="$PROJECT_DIR/dotNet-projektni"
COMPOSE_CMD=$(determine_compose_cmd)

log_info "Using command: $COMPOSE_CMD"
log_info "Starting deployment process..."
log_info "Project directory: $PROJECT_DIR"

# Step 1: Git pull
log_info "Step 1: Pulling latest changes from git repository..."
if git -C "$PROJECT_DIR" pull origin main 2>/dev/null || git -C "$PROJECT_DIR" pull origin master 2>/dev/null; then
    log_success "Git pull completed successfully"
else
    log_warning "Git pull skipped (not a git repository or no remote configured)"
fi

# Step 2: Stop existing containers
log_info "Step 2: Stopping existing containers..."
if $COMPOSE_CMD -f "$PROJECT_DIR/docker-compose.yml" down --remove-orphans 2>/dev/null; then
    log_success "Existing containers stopped"
else
    log_warning "No existing containers to stop"
fi

# Step 3: Build and start containers
log_info "Step 3: Building and starting Docker containers..."
cd "$PROJECT_DIR"

if ! $COMPOSE_CMD up --build -d; then
    log_error "Failed to start Docker containers"
    exit 1
fi

log_success "Docker containers started successfully"

# Step 4: Wait for app to be healthy
log_info "Step 4: Waiting for application to be healthy..."
MAX_ATTEMPTS=30
ATTEMPT=0

while [ $ATTEMPT -lt $MAX_ATTEMPTS ]; do
    if curl -f http://localhost:5109/health > /dev/null 2>&1; then
        log_success "Application is healthy"
        break
    fi
    
    ATTEMPT=$((ATTEMPT + 1))
    if [ $ATTEMPT -lt $MAX_ATTEMPTS ]; then
        echo -n "."
        sleep 1
    else
        log_error "Application failed to become healthy after $MAX_ATTEMPTS attempts"
        log_error "Check logs with: $COMPOSE_CMD logs app"
        exit 1
    fi
done

echo ""

# Step 5: Run database migrations
log_info "Step 5: Running database migrations..."
cd "$APP_DIR"

if $COMPOSE_CMD -f "$PROJECT_DIR/docker-compose.yml" exec -T app dotnet ef database update; then
    log_success "Database migrations completed successfully"
else
    log_warning "Database migrations completed (may already be up to date)"
fi

# Step 6: Verification
log_info "Step 6: Verifying deployment..."

log_info "Running containers:"
$COMPOSE_CMD -f "$PROJECT_DIR/docker-compose.yml" ps

log_info ""
log_success "Deployment completed successfully!"
log_info ""
log_info "Application is running at: http://localhost:5109"
log_info "To view logs: $COMPOSE_CMD -f $PROJECT_DIR/docker-compose.yml logs -f app"
log_info "To stop containers: $COMPOSE_CMD -f $PROJECT_DIR/docker-compose.yml down"



