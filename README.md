# HTML Template to PDF - Creator
Front-end repository for this project: [Template-To-PDF-Creator (React + typescript)](https://github.com/supalapka/html-to-pdf-editor)

## Setup using docker
### Requirements
- Docker & Docker Compose installed
- Port 8080 free (or adjust in docker-compose.yaml)

#### Run `docker compose up `
- This will start SQL Server and the API
- Open in browser: http://localhost:8080/swagger/index.html

#
#
## Setup default

1. Change connection string
(Go to appsettings.json and write your database connection string)

2. Create database by running EF migrations
(Open Tools -> NuGet Package Manager -> PM Console. Run 'Update-Database' command)