psql -U postgres -f CreateDatabase.sql -h localhost -p 5400 -d postgres
psql -U postgres -f CreateTable.sql -h localhost -p 5400 -d blog
