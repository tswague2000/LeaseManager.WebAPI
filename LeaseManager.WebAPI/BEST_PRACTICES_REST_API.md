# ?? Bonnes Pratiques API REST Implémentées

## 1. CORS (Cross-Origin Resource Sharing)
? Configuration multi-environnements :
- **Development** : localhost:3000, localhost:4200
- **Production** : domaine spécifique
- Support des credentials

**Code :**
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:4200")
      .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});
```

## 2. API Versioning
? Versioning intégré :
- Version par défaut : v1
- Route : `/api/v{version}/[controller]`
- Header de version dans réponse

**Code :**
```csharp
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

[Route("api/v{version:apiVersion}/[controller]")]
```

## 3. Réponses Standardisées
? Format cohérent pour toutes les réponses :
```json
{
  "success": true,
  "message": "Opération réussie",
  "data": { ... },
  "errors": null
}
```

## 4. Compression de Réponse
? Activation de la compression HTTP :
- GZIP pour les réponses volumineuses
- Activation pour HTTPS aussi

## 5. Gestion Globale des Exceptions
? Middleware central qui :
- Capture toutes les exceptions
- Retourne des codes HTTP appropriés
- Format de réponse standardisé
- Logging centralisé

```csharp
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
```

## 6. Documentation API avec Swagger
? Swagger/OpenAPI complet :
- Description de l'API
- Versioning visible
- Commentaires XML des endpoints
- Try-it-out fonctionnelle

## 7. Logging Structuré
? Logging à chaque niveau :
- Information : opérations normales
- Warning : validations échouées
- Error : exceptions

**Utilisation :**
```csharp
_logger.LogInformation("Récupération de tous les baux");
_logger.LogError(ex, "Erreur lors de la récupération");
```

## 8. Health Checks
? Point d'accès pour monitoring :
- `/api/health` - Vérifie la santé de l'application
- Utile pour les load balancers

## 9. Codes HTTP Appropriés
? Utilisation correcte des codes :
- **200 OK** - Succès avec contenu
- **201 Created** - Ressource créée
- **204 No Content** - Succès sans contenu
- **400 Bad Request** - Erreur client
- **404 Not Found** - Ressource inexistante
- **500 Internal Server Error** - Erreur serveur

## 10. Validations d'Entrée
? Contrôles stricts :
```csharp
if (id <= 0)
    return BadRequest(ApiResponse<LeaseReadDto>
    .ErrorResponse("L'ID doit être supérieur à 0"));
```

## 11. Annotations Swagger
? Documentation des endpoints :
```csharp
[HttpGet("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<ActionResult<ApiResponse<LeaseReadDto>>> GetById(int id)
```

## 12. Sécurité HTTPS
? Redirection HTTPS automatique :
```csharp
app.UseHttpsRedirection();
```

## 13. Configuration Centralisée
? appsettings.json pour :
- Connection strings
- CORS origins
- Logging levels
- API settings

```json
{
  "CorsSettings": {
    "AllowedOrigins": ["http://localhost:3000"]
  }
}
```

## 14. Routes Propres et Cohérentes
? Convention de nommage :
- Pluriel pour ressources : `/api/leases`
- Hiérarchie claire : `/api/leases/{id}/payments`
- Verbes HTTP standards

## 15. Pagination et Filtrage (À implémenter)
- Ajouter skip/take pour les listes
- Filtres par query parameters
- Tri configurable

## Structure des Réponses

### Succès avec données
```json
{
  "success": true,
  "message": "Baux récupérés avec succès",
  "data": [{ ... }],
  "errors": null
}
```

### Succès sans données
```json
{
  "success": true,
  "message": "Bail créé avec succès",
  "data": { ... }
}
```

### Erreur
```json
{
  "success": false,
  "message": "Bail non trouvé",
  "data": null,
  "errors": null
}
```

### Erreur d'exception
```json
{
  "statusCode": 400,
  "message": "Opération invalide",
  "details": "Une propriété avec cet ID n'existe pas",
  "timestamp": "2024-01-10T10:30:00Z"
}
```

## Commandes Utiles

```bash
# Run en développement
dotnet run

# Accéder à Swagger
# http://localhost:5000/

# Health Check
# http://localhost:5000/api/health
```

## Configuration par Environnement

- **Development** : CORS permissif, Swagger activé
- **Production** : CORS restrictif, Swagger désactivé

## Points de Monitoring

- `/api/health` - Santé globale
- Logs structurés dans console
- Swagger UI pour tester
- Réponses standardisées pour client

## Sécurité Implémentée

- [x] CORS configurable
- [x] HTTPS enforced
- [x] Exception handling robuste
- [x] Input validation
- [x] Logging centralisé

## Sécurité Supplémentaire (À faire)

- [ ] JWT Authentication
- [ ] Rate limiting
- [ ] API Key validation
- [ ] Input sanitization
- [ ] HTTPS-only cookies
- [ ] CSRF protection