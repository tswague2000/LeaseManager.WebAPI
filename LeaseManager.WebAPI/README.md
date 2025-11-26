# ?? LeaseManager API

Une API REST complète et professionnelle pour la gestion des contrats de location immobilière, construite avec **.NET 8** en suivant les meilleures pratiques de l'industrie.

## ?? Table des matières

- [Caractéristiques](#-caractéristiques)
- [Architecture](#-architecture)
- [Installation](#-installation)
- [Utilisation](#-utilisation)
- [API Endpoints](#-api-endpoints)
- [Tests](#-tests)
- [Déploiement](#-déploiement)
- [Bonnes Pratiques](#-bonnes-pratiques)
- [Contribution](#-contribution)

## ? Caractéristiques

### Fonctionnalités Core
- ? Gestion complète des contrats de location (CRUD)
- ? Gestion des propriétés et propriétaires
- ? Suivi des paiements et transactions
- ? Demandes de maintenance
- ? Galerie d'images pour propriétés
- ? Gestion de documents

### Qualité du Code
- ? Architecture Clean (Domain-Driven Design)
- ? Injection de dépendances
- ? Repository Pattern
- ? Unit of Work Pattern
- ? Logging structuré

### API REST
- ? CORS configurable multi-environnement
- ? Versioning (v1)
- ? Documentation Swagger/OpenAPI
- ? Réponses standardisées
- ? Gestion globale des exceptions
- ? Compression de réponses
- ? Health checks

### Tests
- ? Tests unitaires complets (xUnit + Moq)
- ? Tests par service organisés
- ? Pattern AAA (Arrange-Act-Assert)
- ? TestDataBuilder pour consistance

### Sécurité
- ? HTTPS redirection
- ? Validation d'entrée
- ? CORS sécurisé
- ? Logging des erreurs
- ? Exception handling globale

## ??? Architecture

```
LeaseManager/
??? LeaseManager.Core.Domain/
?   ??? Entities/   # Modèles métier
?   ??? Enums/       # Énumérations
?   ??? Interfaces/        # Contrats
??? LeaseManager.Infrastructure/
?   ??? Data/     # Base de données
?   ??? Repositories/      # Accès aux données
?   ??? UnitOfWork/ # Orchestration
??? LeaseManager.WebAPI/
?   ??? Controllers/       # Endpoints REST
?   ??? Application/       # Services métier
?   ??? Middleware/        # Handlers
?   ??? Program.cs         # Configuration
??? ProjectTest/     # Tests unitaires
```

## ?? Installation

### Prérequis

- .NET 8.0 ou ultérieur
- SQL Server 2019+
- Docker & Docker Compose (optionnel)

### Local Setup

```bash
# 1. Cloner le repository
git clone https://github.com/tswague2000/LeaseManager.WebAPI.git
cd LeaseManager.WebAPI

# 2. Restaurer les dépendances
dotnet restore

# 3. Mettre à jour la base de données
dotnet ef database update --project LeaseManager.Infrastructure

# 4. Lancer les tests
dotnet test

# 5. Démarrer l'application
dotnet run --project LeaseManager.WebAPI
```

### Docker Setup

```bash
# Avec docker-compose
docker-compose up -d

# L'API sera disponible sur http://localhost:5000
# Swagger sur http://localhost:5000/
```

## ?? Utilisation

### Lancer l'Application

```bash
# En développement
cd LeaseManager.WebAPI
dotnet run

# En production
dotnet run --configuration Release
```

### Accéder à l'API

- **Base URL** : http://localhost:5000
- **Swagger UI** : http://localhost:5000/
- **Health Check** : http://localhost:5000/api/health

## ?? API Endpoints

### Lease (Contrats de Location)
```http
GET    /api/v1/lease         # Tous les baux
GET    /api/v1/lease/{id}  # Bail spécifique
POST   /api/v1/lease# Créer un bail
PUT    /api/v1/lease/{id}      # Mettre à jour
DELETE /api/v1/lease/{id}      # Supprimer
```

### Property (Propriétés)
```http
GET    /api/v1/property        # Toutes les propriétés
GET    /api/v1/property/{id}   # Propriété spécifique
POST   /api/v1/property    # Créer une propriété
PUT    /api/v1/property/{id}   # Mettre à jour
DELETE /api/v1/property/{id}   # Supprimer
```

### Owner (Propriétaires)
```http
GET    /api/v1/owner           # Tous les propriétaires
GET    /api/v1/owner/{id}      # Propriétaire spécifique
POST   /api/v1/owner           # Créer un propriétaire
PUT    /api/v1/owner/{id}      # Mettre à jour
DELETE /api/v1/owner/{id}  # Supprimer
```

*Et ainsi de suite pour Tenant, Payment, MaintenanceRequest, PropertyImage, Document*

### Exemple de Requête

```bash
# Récupérer tous les baux
curl -X GET http://localhost:5000/api/v1/lease \
  -H "Content-Type: application/json"

# Créer un bail
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

## ?? Tests

### Exécuter les Tests

```bash
# Tous les tests
dotnet test

# Tests spécifiques
dotnet test --filter "LeaseService"

# Avec couverture de code
dotnet test /p:CollectCoverage=true

# Détails verbeux
dotnet test --verbosity detailed
```

### Structure des Tests

Les tests sont organisés par service dans `ProjectTest/Services/` :

```
ProjectTest/Services/
??? LeaseServiceGetAllAsyncTests.cs
??? LeaseServiceGetByIdAsyncTests.cs
??? LeaseServiceCreateAsyncTests.cs
??? LeaseServiceUpdateAsyncTests.cs
??? LeaseServiceDeleteAsyncTests.cs
??? OwnerServiceGetAllAsyncTests.cs
??? OwnerServiceCreateAsyncTests.cs
??? OwnerServiceUpdateAsyncTests.cs
??? OwnerServiceDeleteAsyncTests.cs
??? TestDataBuilder.cs
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

## ?? Déploiement

### Docker

```bash
# Build l'image
docker build -t leasemanager-api .

# Run le conteneur
docker run -p 5000:5000 \
  -e ConnectionStrings__DefaultConnection="..." \
  leasemanager-api
```

### Script de Déploiement

```bash
# Rendre le script exécutable
chmod +x deploy.sh

# Exécuter le déploiement
./deploy.sh Release
```

### CI/CD

Utiliser `deploy.sh` pour automatiser :
1. Restauration des dépendances
2. Nettoyage des builds
3. Compilation
4. Tests
5. Publication

## ?? Bonnes Pratiques

### CORS
Configuration multi-environnement :
- Development : localhost:3000, localhost:4200
- Production : domaine configurable

### Response Format
```json
{
  "success": true,
  "message": "Opération réussie",
  "data": { },
  "errors": null
}
```

### Error Handling
```json
{
  "statusCode": 400,
"message": "Opération invalide",
  "details": "Détail de l'erreur",
  "timestamp": "2024-01-10T10:30:00Z"
}
```

### Logging
```csharp
_logger.LogInformation("Récupération de tous les baux");
_logger.LogError(ex, "Erreur lors de la récupération");
```

### API Versioning
- Route : `/api/v1/[controller]`
- Version par défaut : 1.0
- Reporting dans headers

## ?? Configuration

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

- **Development** : CORS permissif, Swagger activé
- **Production** : CORS restrictif, Swagger désactivé

## ?? Statistiques

| Métrique | Nombre |
|----------|--------|
| Services | 8 |
| Contrôleurs | 8 |
| Tests Unitaires | 20+ |
| Endpoints | 55+ |
| Codes HTTP Gérés | 7 |

## ?? Sécurité

### Implémentée
- ? HTTPS redirection
- ? CORS configurable
- ? Input validation
- ? Global exception handling
- ? Logging centralisé

### À Implémenter
- [ ] JWT Authentication
- [ ] API Key validation
- [ ] Rate limiting
- [ ] Input sanitization

## ?? Documentation

- [IMPLEMENTATION_SUMMARY.md](./IMPLEMENTATION_SUMMARY.md) - Résumé
- [BEST_PRACTICES_REST_API.md](./BEST_PRACTICES_REST_API.md) - Bonnes pratiques
- [TESTS_ORGANIZATION.md](./ProjectTest/TESTS_ORGANIZATION.md) - Organisation tests
- [FINAL_SUMMARY.md](./FINAL_SUMMARY.md) - Résumé final

## ?? Contribution

1. Fork le repository
2. Créer une branche (`git checkout -b feature/amazing-feature`)
3. Commit les changements (`git commit -m 'Add amazing feature'`)
4. Push vers la branche (`git push origin feature/amazing-feature`)
5. Ouvrir une Pull Request

## ?? Support

Pour les questions ou problèmes :
- Créer une issue
- Email : support@leasemanager.com

## ?? Licence

Ce projet est sous licence MIT. Voir le fichier [LICENSE](LICENSE) pour plus de détails.

## ?? Remerciements

Construit avec :
- .NET 8
- xUnit & Moq pour les tests
- Swagger/OpenAPI pour la documentation
- SQL Server pour la base de données

---

**Version** : 1.0.0  
**Dernière mise à jour** : 2024-01-10  
**Statut** : ? Production Ready