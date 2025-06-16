## Project Description

A Mini Learning Platform web application that allows users to select their learning topics by categories and subcategories. Users can send prompts to an AI to receive dynamically generated lessons and view their learning history. The platform features a REST API backend, a database, AI integration, and a basic web dashboard UI.
## Technologies Used
- **Frontend**: React (with TSX components)
- **Backend**: C#
- **Database**: PostgreSQL
- **ORM**: Entity Framework (EF)
- **Mapping**: AutoMapper
- **Containerization**: Docker

## Assumptions Made
- Users possess a basic understanding of command-line operations.
- Docker is installed and configured on the user's machine.
- Users have access to Visual Studio for backend development and execution.

## Setup Instructions

### Step 1: Clone the Repository
To clone the repository, open your terminal and run:

```bash
git clone https://github.com/DinaGoldberg1/AI-learning-platform.git
```

### Step 2: Install Docker
Ensure that Docker is installed on your machine. You can download it from:
👉 [Docker Desktop](https://www.docker.com/products/docker-desktop/)

### Step 3: Run the Database (PostgreSQL)

#### Change the connection string in the Appsettings file and in MyDbContext (in DAL) file to be:
```json
"DefaultConnection": "Host=db;Port=5432;Database=AIPlatformData;Username=postgres;Password=data4195"
```

Navigate to the project directory and run the following command to start the PostgreSQL container:

```bash
docker-compose up -d
```

- This command will run a container with PostgreSQL and expose port 5432 to localhost.
- **Username**: `postgres`
- **Password**: `data4195`
- **Database Name**: `AIPlatformData`

### Step 4: Configure Database Connection in C#
#### 🧵 Connection String – Update as per the source

#### If the C# code runs outside of Docker (e.g., from Visual Studio) change the connection strung to be:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=AIPlatformData;Username=postgres;Password=data4195"
}
```

#### If the C# code runs *inside* the Docker container:
```json
"DefaultConnection": "Host=db;Port=5432;Database=AIPlatformData;Username=postgres;Password=data4195"
```

### Step 5: Run the Backend
To run the backend, execute the following command in the `SERVER` directory:

```bash
dotnet run
```

Alternatively, you can run it through Visual Studio.

### Step 6: Access the Database with External Tools
To connect using pgAdmin, DBeaver, or DataGrip, use the following credentials:

```
Host: localhost
Port: 5432
User: postgres
Password: data4195
Database: AIPlatformData
```

### Important Notes
- If you are running the backend in Docker, the connection string should be with `Host=db`.
- If you are running it externally, do not change the connection string (`Host=localhost`).

## Running the Frontend
To run the frontend, navigate to the `client` directory and execute:

```bash
npm install
npm start
```

This will start the React application, and you can access it at `http://localhost:3000`.

## Accessing the Admin Dashboard
To log into the Admin Dashboard, use the following credentials:
- **Username**: `admin`
- **Phone**: `0502223333`
