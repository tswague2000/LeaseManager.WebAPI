# 🧪 Tests Unitaires - Architecture et Organisation

## Structure des Tests

Les tests sont organisés par service dans le dossier `ProjectTest/Services/` avec une classe par méthode.

### Organisation par Service

#### LeaseService
```
LeaseServiceGetAllAsyncTests.cs
├── GetAllAsync_WithMultipleLeases_ShouldReturnAllLeases()
├── GetAllAsync_WithEmptyList_ShouldReturnEmptyCollection()
└── GetAllAsync_ShouldCallRepositoryGetAllAsyncOnce()

LeaseServiceGetByIdAsyncTests.cs
├── GetByIdAsync_WithValidId_ShouldReturnLease()
├── GetByIdAsync_WithInvalidId_ShouldReturnNull()
└── GetByIdAsync_ShouldCallRepositoryWithCorrectId()

LeaseServiceCreateAsyncTests.cs
├── CreateAsync_WithValidData_ShouldCreateLease()
├── CreateAsync_WithInvalidProperty_ShouldThrowException()
├── CreateAsync_WithInvalidTenant_ShouldThrowException()
└── CreateAsync_ShouldSaveChanges()

LeaseServiceUpdateAsyncTests.cs
├── UpdateAsync_WithValidData_ShouldUpdateLease()
├── UpdateAsync_WithInvalidId_ShouldReturnFalse()
├── UpdateAsync_WithAllFields_ShouldUpdateAll()
└── UpdateAsync_WithPartialData_ShouldUpdateOnlyProvidedFields()

LeaseServiceDeleteAsyncTests.cs
├── DeleteAsync_WithValidId_ShouldDeleteLease()
├── DeleteAsync_WithInvalidId_ShouldReturnFalse()
└── DeleteAsync_ShouldSaveChanges()
```

#### OwnerService
```
OwnerServiceGetAllAsyncTests.cs
├── GetAllAsync_WithMultipleOwners_ShouldReturnAllOwners()
├── GetAllAsync_WithEmptyList_ShouldReturnEmptyCollection()
└── GetAllAsync_ShouldCallRepositoryOnce()

OwnerServiceCreateAsyncTests.cs
├── CreateAsync_WithValidData_ShouldCreateOwner()
├── CreateAsync_WithDuplicateEmail_ShouldThrowException()
└── CreateAsync_ShouldSaveChanges()

OwnerServiceUpdateAsyncTests.cs
├── UpdateAsync_WithValidData_ShouldUpdateOwner()
├── UpdateAsync_WithInvalidId_ShouldReturnFalse()
└── UpdateAsync_ShouldCallUpdate()

OwnerServiceDeleteAsyncTests.cs
├── DeleteAsync_WithValidId_ShouldDeleteOwner()
└── DeleteAsync_WithInvalidId_ShouldReturnFalse()
```

## Pattern AAA (Arrange-Act-Assert)

Tous les tests suivent le pattern AAA :

```csharp
[Fact]
public async Task GetAllAsync_WithMultipleLeases_ShouldReturnAllLeases()
{
    // Arrange - Préparation des données
    var leases = new List<Lease>
    {
 TestDataBuilder.CreateTestLease(1),
        TestDataBuilder.CreateTestLease(2)
    };
    
    _mockUnitOfWork.Setup(x => x.LeaseRepository.GetAllAsync())
   .ReturnsAsync(leases);

  // Act - Exécution de l'action
    var result = await _leaseService.GetAllAsync();

    // Assert - Vérification du résultat
    Assert.NotNull(result);
    Assert.Equal(2, result.Count());
}
```

## TestDataBuilder

Classe utilitaire pour créer des données de test cohérentes :

```csharp
public static class TestDataBuilder
{
    public static Property CreateTestProperty(int id = 1) { ... }
    public static Owner CreateTestOwner(int id = 1) { ... }
    public static Tenant CreateTestTenant(int id = 1) { ... }
    public static Lease CreateTestLease(int id = 1, int propertyId = 1, int tenantId = 1) { ... }
  public static Payment CreateTestPayment(int id = 1, int leaseId = 1) { ... }
 public static MaintenanceRequest CreateTestMaintenanceRequest(...) { ... }
    public static PropertyImage CreateTestPropertyImage(...) { ... }
    public static Document CreateTestDocument(...) { ... }
}
```

### Utilisation du Builder

```csharp
var lease = TestDataBuilder.CreateTestLease(1);
var owner = TestDataBuilder.CreateTestOwner(5);
var payment = TestDataBuilder.CreateTestPayment(id: 1, leaseId: 1);
```

## Frameworks Utilisés

### xUnit
- Framework de test moderne
- Exécution parallèle par défaut
- Approche basée sur les [Fact] et [Theory]

### Moq
- Framework pour les mocks
- Syntaxe fluide pour setup
- Vérification des appels

## Conventions de Nommage des Tests

Format : `MethodName_Scenario_ExpectedResult`

```csharp
// Bon
GetByIdAsync_WithValidId_ShouldReturnLease()
CreateAsync_WithInvalidProperty_ShouldThrowException()
DeleteAsync_WithInvalidId_ShouldReturnFalse()

// Mauvais
GetByIdTest()
TestCreateWithInvalid()
DeleteFail()
```

## Types de Tests

### 1. Cas de Succès (Happy Path)
```csharp
[Fact]
public async Task GetAllAsync_WithMultipleLeases_ShouldReturnAllLeases()
{
    // Vérifie le comportement normal
}
```

### 2. Cas d'Erreur
```csharp
[Fact]
public async Task CreateAsync_WithInvalidProperty_ShouldThrowException()
{
    // Vérifie la gestion des erreurs
}
```

### 3. Cas Limites
```csharp
[Fact]
public async Task GetAllAsync_WithEmptyList_ShouldReturnEmptyCollection()
{
    // Vérifie les cas limites
}
```

### 4. Vérification des Interactions
```csharp
[Fact]
public async Task CreateAsync_ShouldSaveChanges()
{
    // Vérifie que SaveChanges est appelé
    _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
}
```

## Couverture de Test

Chaque service devrait avoir :
- ✅ Tests pour GetAll (avec données, vide, vérification d'appel)
- ✅ Tests pour GetById (valide, invalide, vérification d'appel)
- ✅ Tests pour Create (valide, erreurs, sauvegarde)
- ✅ Tests pour Update (valide, invalide, partiels)
- ✅ Tests pour Delete (valide, invalide)

## Configuration des Mocks

### Setup Simple
```csharp
_mockUnitOfWork.Setup(x => x.LeaseRepository.GetAllAsync())
    .ReturnsAsync(leases);
```

### Setup avec Arguments
```csharp
_mockUnitOfWork.Setup(x => x.LeaseRepository.GetByIdAsync(1))
    .ReturnsAsync(lease);

_mockUnitOfWork.Setup(x => x.LeaseRepository.GetByIdAsync(It.IsAny<int>()))
    .ReturnsAsync(lease);
```

### Setup pour Lever une Exception
```csharp
_mockUnitOfWork.Setup(x => x.LeaseRepository.GetByIdAsync(999))
    .ReturnsAsync((Lease)null);
```

### Setup pour les Méthodes Async
```csharp
_mockUnitOfWork.Setup(x => x.SaveChangesAsync())
 .ReturnsAsync(1);
```

## Assertions Communes

```csharp
// Null checks
Assert.Null(result);
Assert.NotNull(result);

// Equality
Assert.Equal(expected, actual);
Assert.NotEqual(expected, actual);

// Truthiness
Assert.True(result);
Assert.False(result);

// Collections
Assert.Empty(result);
Assert.NotEmpty(result);
Assert.Contains(item, collection);
Assert.Single(collection);

// Exceptions
await Assert.ThrowsAsync<InvalidOperationException>(() => service.Method());
```

## Exécution des Tests

```bash
# Tous les tests
dotnet test

# Tests d'un projet spécifique
dotnet test ProjectTest

# Tests avec le pattern de nom
dotnet test --filter "LeaseService"

# Tests avec détails verbeux
dotnet test --verbosity detailed

# Tests avec couverture de code
dotnet test /p:CollectCoverage=true

dotnet clean
  dotnet build
  dotnet run
```

## Bonnes Pratiques

1. **Un test = une assertion principale**
   - Peut y avoir plusieurs assertions pour contexte
   - Mais une assertion clé

2. **Tests indépendants**
   - Pas de dépendance entre tests
   - Ordre d'exécution n'importe pas

3. **Noms descriptifs**
   - Lire le test pour comprendre ce qui est testé
   - Pas besoin de commentaires

4. **Setup minimal**
   - Initialiser que ce qui est nécessaire
   - Garder les tests courts et lisibles

5. **Pas de logique de test**
   - Pas de boucles ou conditions
   - Chaque scénario = test distinct

6. **Vérifier les interactions**
   - `.Verify()` pour s'assurer que les méthodes sont appelées
   - `.Times.Once`, `.Times.Never`, `.Times.AtLeast(2)`

## Cas de Test à Ajouter

### Pour PaymentService
```
PaymentServiceGetAllAsyncTests
PaymentServiceGetByIdAsyncTests
PaymentServiceCreateAsyncTests
PaymentServiceUpdateAsyncTests
PaymentServiceDeleteAsyncTests
```

### Pour PropertyService
```
PropertyServiceGetAllAsyncTests
PropertyServiceGetByIdAsyncTests
PropertyServiceCreateAsyncTests
PropertyServiceUpdateAsyncTests
PropertyServiceDeleteAsyncTests
```

### Pour autres services
```
TenantServiceTests
MaintenanceRequestServiceTests
PropertyImageServiceTests
DocumentServiceTests
```

## Exécution Recommandée

1. Avant commit : `dotnet test`
2. Avant push : Vérifier tous les tests passent
3. En CI/CD : Automatiser les tests

## Ressources

- [xUnit Documentation](https://xunit.net/docs/getting-started/netcore)
  - [Moq Documentation](https://github.com/Moq/moq4/wiki/Quickstart)
      - [Microsoft Testing Best Practices](https://docs.microsoft.com/en-us/dotnet/core/testing/)