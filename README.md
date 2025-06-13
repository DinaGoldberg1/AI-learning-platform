# README

## Setup Instructions
1. **Install Dependencies**: Ensure you have Docker and Docker Compose installed on your machine. You can download them from the [Docker website](https://www.docker.com/get-started).
2. **Clone the Repository**: Clone this repository to your local machine using:
   ```bash
   git clone <repository-url>
   ```
3. **Set Up PostgreSQL**: Use Docker Compose to spin up the PostgreSQL database by following these steps:

   - Create a `docker-compose.yml` file in the root directory with the following content:
     ```yaml
version: '3.8'
services:
  db:
    image: postgres:15
    restart: always
    environment:
      POSTGRES_USER: user
      POSTGRES_PASSWORD: password
      POSTGRES_DB: database
    ports:
      - "5432:5432"
    volumes:
      - db_data:/var/lib/postgresql/data
volumes:
  db_data:

     volumes:
       db_data:
     ```

   - Run the following command to start the PostgreSQL service:
     ```bash
     docker-compose up -d
     ```

   - Verify that the PostgreSQL container is running by executing:
     ```bash
     docker ps
     ```

## Technologies Used
- C#
- React (with TSX components)
- PostgreSQL
- Docker
- Docker Compose

## Assumptions Made
- Docker and Docker Compose are installed on the local machine.
- The user has basic knowledge of using the command line.
- The user has access to the internet to pull Docker images.

## How to Run Locally
### Frontend
1. Navigate to the frontend directory:
   ```bash
   cd frontend
   ```
2. Install dependencies using:
   ```bash
   npm install
   ```
3. Start the frontend application:
   ```bash
   npm start
   ```

### Backend
1. Navigate to the backend directory:
   ```bash
   cd backend
   ```
2. Install dependencies using:
   ```bash
   dotnet restore
   ```
3. Start the backend application:
   ```bash
   dotnet run
   ```

### Database Connection
1. Ensure your connection string in `appsettings.json` is set correctly:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": ""Host=localhost;Port=5432;Database=AIPlatformData;Username=postgres;Password=data4195""
   }
   ```

### Accessing the Application
- The backend will be accessible at:
  ```
  https://localhost:7194
  http://localhost:5287
  ```

- The frontend will typically run at:
  ```
  http://localhost:3000
  ```

### Important Notes
- Ensure that Docker is running before starting the backend and frontend applications.
- If you encounter any issues connecting to the database, verify that the PostgreSQL container is running by executing:
  ```bash
  docker ps
  ```

### Stopping the Database
To stop the PostgreSQL database, run:
```bash
docker-compose down
```
