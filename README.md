# business-management-persistence

### Implement Migrations and Update Database

**Create migration**

```bash
cd BusinessPersistance

dotnet ef migrations add InitialCreate --project ../Persistence --startup-project .
```

**Apply migration and update database**

```bash
cd BusinessPersistance

dotnet ef database update --project ../Persistence --startup-project .
```