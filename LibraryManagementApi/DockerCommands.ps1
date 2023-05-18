# Run postgresql docker 
docker run --name postgres-library-db -p 5455:5432 -e POSTGRES_USER=postgresUser -e POSTGRES_PASSWORD=	 -e POSTGRES_DB=library-db -d postgres


