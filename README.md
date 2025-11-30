#    LeaseManager API

Une API REST compl�te et professionnelle pour la gestion des contrats de location immobili�re, construite avec **.NET 8** en suivant les meilleures pratiques de l'industrie.

##    Table des mati�res

- [Caract�ristiques](#-caract�ristiques)
- [Architecture](#-architecture)
- [Installation](#-installation)
- [Utilisation](#-utilisation)
- [API Endpoints](#-api-endpoints)
- [Tests](#-tests)
- [Bonnes Pratiques](#-bonnes-pratiques)

##   Caract�ristiques

### Fonctionnalit�s Core
-   Gestion compl�te des contrats de location (CRUD)
-   Gestion des propri�t�s et propri�taires
-   Suivi des paiements et transactions
-   Demandes de maintenance
-   Galerie d'images pour propri�t�s
-   Gestion de documents

### Qualit� du Code
-   Architecture Clean (Domain-Driven Design)
-   Injection de d�pendances
-   Repository Pattern
-   Unit of Work Pattern
-   Logging structur�

### API REST
-   CORS configurable multi-environnement
-   Versioning (v1)
-   Documentation Swagger/OpenAPI
-   R�ponses standardis�es
-   Gestion globale des exceptions
-   Compression de r�ponses
-   Health checks

### Tests
-   Tests unitaires complets (xUnit + Moq)
-   Tests par service organis�s
-   Pattern AAA (Arrange-Act-Assert)
-   TestDataBuilder pour consistance

### S�curit�
-   HTTPS redirection
-   Validation d'entr�e
-   CORS s�curis�
-   Logging des erreurs
-   Exception handling globale

##  Architecture

```
LeaseManager/
 LeaseManager.Core.Domain/
     Entities/   # Mod�les m�tier
     Enums/       # �num�rations
     Interfaces/        # Contrats
 LeaseManager.Infrastructure/
     Data/     # Base de donn�es
     Repositories/      # Acc�s aux donn�es
     UnitOfWork/ # Orchestration
 LeaseManager.WebAPI/
     Controllers/       # Endpoints REST
     Application/       # Services m�tier
     Middleware/        # Handlers
     Program.cs         # Configuration
 ProjectTest/     # Tests unitaires
```

##    Installation

### Pr�requis

- .NET 8.0 ou ult�rieur
- SQL Server 2019+

### Setup Local

```bash
# 1. Cloner le repository
git clone https://github.com/tswague2000/LeaseManager.WebAPI.git
cd LeaseManager.WebAPI

# 2. Restaurer les d�pendances
dotnet restore

# 3. Mettre � jour la base de donn�es
dotnet ef database update --project LeaseManager.Infrastructure

# 4. Lancer les tests
dotnet test

# 5. D�marrer l'application
dotnet run --project LeaseManager.WebAPI
```

##    Utilisation

### Lancer l'Application

```bash
# En d�veloppement
cd LeaseManager.WebAPI
dotnet run

# En production
dotnet run --configuration Release
```

### Acc�der � l'API

- **Base URL** : http://localhost:5000
- **Swagger UI** : http://localhost:5000/
- **Health Check** : http://localhost:5000/api/health

##    API Endpoints

### Lease (Contrats de Location)
```http
GET    /api/v1/lease         # Tous les baux
GET    /api/v1/lease/{id}  # Bail sp�cifique
POST   /api/v1/lease# Cr�er un bail
PUT    /api/v1/lease/{id}      # Mettre � jour
DELETE /api/v1/lease/{id}      # Supprimer
```

### Property (Propri�t�s)
```http
GET    /api/v1/property        # Toutes les propri�t�s
GET    /api/v1/property/{id}   # Propri�t� sp�cifique
POST   /api/v1/property    # Cr�er une propri�t�
PUT    /api/v1/property/{id}   # Mettre � jour
DELETE /api/v1/property/{id}   # Supprimer
```

### Owner (Propri�taires)
```http
GET    /api/v1/owner           # Tous les propri�taires
GET    /api/v1/owner/{id}      # Propri�taire sp�cifique
POST   /api/v1/owner           # Cr�er un propri�taire
PUT    /api/v1/owner/{id}      # Mettre � jour
DELETE /api/v1/owner/{id}  # Supprimer
```

*Et ainsi de suite pour Tenant, Payment, MaintenanceRequest, PropertyImage, Document*

### Exemple de Requ�te

```bash
# R�cup�rer tous les baux
curl -X GET http://localhost:5000/api/v1/lease \
  -H "Content-Type: application/json"

# Cr�er un bail
curl -X POST http://localhost:5000/api/v1/lease \
  -H "Content-Type: application/json" \
  -d '{
    "startDate": "2024-01-01",
  "endDate": "2025-01-01",
    "monthlyRent": 1000,
  "tenantId": 1,
    "propertyId": 1
  }'
```

##    Tests

### Ex�cuter les Tests

```bash
# Tous les tests
dotnet test

# Tests sp�cifiques
dotnet test --filter "LeaseService"

# Avec couverture de code
dotnet test /p:CollectCoverage=true

# D�tails verbeux
dotnet test --verbosity detailed
```

### Structure des Tests

Les tests sont organis�s par service dans `ProjectTest/Services/` :

```
ProjectTest/Services/
 LeaseServiceGetAllAsyncTests.cs
 LeaseServiceGetByIdAsyncTests.cs
 LeaseServiceCreateAsyncTests.cs
 LeaseServiceUpdateAsyncTests.cs
 LeaseServiceDeleteAsyncTests.cs
 OwnerServiceGetAllAsyncTests.cs
 OwnerServiceCreateAsyncTests.cs
 OwnerServiceUpdateAsyncTests.cs
 OwnerServiceDeleteAsyncTests.cs
 TestDataBuilder.cs
```

### Exemple de Test

```csharp
[Fact]
public async Task GetAllAsync_WithMultipleLeases_ShouldReturnAllLeases()
{
    // Arrange
    var leases = new List<Lease>
    {
    TestDataBuilder.CreateTestLease(1),
        TestDataBuilder.CreateTestLease(2)
    };
    _mockUnitOfWork.Setup(x => x.LeaseRepository.GetAllAsync())
     .ReturnsAsync(leases);

    // Act
    var result = await _leaseService.GetAllAsync();

  // Assert
    Assert.NotNull(result);
    Assert.Equal(2, result.Count());
}
```

##    Bonnes Pratiques

### CORS
Configuration multi-environnement :
- Development : localhost:3000, localhost:4200
- Production : domaine configurable

### API Versioning
- Route : `/api/v1/[controller]`
- Version par d�faut : 1.0
- Reporting dans headers

### Response Format
```json
{
  "success": true,
  "message": "Op�ration r�ussie",
  "data": { },
  "errors": null
}
```

### Error Handling
```json
{
  "statusCode": 400,
  "message": "Op�ration invalide",
  "details": "D�tail de l'erreur",
  "timestamp": "2024-01-10T10:30:00Z"
}
```

### Logging
```csharp
_logger.LogInformation("R�cup�ration de tous les baux");
_logger.LogError(ex, "Erreur lors de la r�cup�ration");
```

### Codes HTTP Appropri�s
- 200 OK
- 201 Created
- 204 No Content
- 400 Bad Request
- 404 Not Found
- 500 Internal Server Error

##    Configuration

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Initial Catalog=LeaseManagerDB;..."
  },
  "CorsSettings": {
    "AllowedOrigins": ["http://localhost:3000"]
  }
}
```

### Environnements

- **Development** : CORS permissif, Swagger activ�
- **Production** : CORS restrictif, Swagger d�sactiv�

##    S�curit�

### Impl�ment�e
-   HTTPS redirection
-   CORS configurable
-   Input validation
-   Global exception handling
-   Logging centralis�

### � Impl�menter
- [ ] JWT Authentication
- [ ] API Key validation
- [ ] Rate limiting
- [ ] Input sanitization

##    Statistiques

| M�trique | Nombre |
|----------|--------|
| Services | 8 |
| Contr�leurs | 8 |
| Tests Unitaires | 20+ |
| Endpoints | 55+ |
| Codes HTTP G�r�s | 7 |

##    Documentation

- [BEST_PRACTICES_REST_API.md](./BEST_PRACTICES_REST_API.md) - Bonnes pratiques
- [TESTS_ORGANIZATION.md](./ProjectTest/TESTS_ORGANIZATION.md) - Organisation tests
- [FINAL_SUMMARY.md](./FINAL_SUMMARY.md) - R�sum� final


**Version** : 1.0.0  
