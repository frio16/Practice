# Database Scripts

This folder contains SQL scripts for database schema changes and migrations.

## Structure

- `Scripts/` - Contains numbered SQL migration scripts

## Script Naming Convention

Scripts are named with a number prefix followed by a descriptive name:
- `001_CreateTrainMasterTable.sql`
- `002_AddTrainStatusColumn.sql`
- etc.

## Running Scripts

To run a script against the PostgreSQL database:

```bash
docker exec -i selflearning-postgres psql -U postgres -d selflearningdb < Database/Scripts/001_CreateTrainMasterTable.sql
```

Or interactively:
```bash
docker exec -it selflearning-postgres psql -U postgres -d selflearningdb
\i Database/Scripts/001_CreateTrainMasterTable.sql
```

## Notes

- Always test scripts in a development environment first
- Keep scripts idempotent (use IF NOT EXISTS where possible)
- Document any breaking changes in the script comments




