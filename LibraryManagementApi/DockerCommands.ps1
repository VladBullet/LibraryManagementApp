# Run postgresql docker 
docker run --name postgres-library-db -p 5455:5432 -e POSTGRES_USER=postgresUser -e POSTGRES_PASSWORD=my5ecr3tqa55w0rd -e POSTGRES_DB=library-db -d postgres

# Build app
docker build -f LibraryManagementApi/Dockerfile .
