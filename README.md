# 🏠 LeaseManager API

## 🧩 Présentation générale

**LeaseManager** est une **API de gestion de contrats de location immobilière**.  
Elle constitue le **module backend** d’un site web ou d’un système interne, permettant d’administrer les **propriétés**, les **propriétaires**, les **locataires**, les **baux** et les **paiements** associés.

L’API ne contient **aucun système d’authentification** : elle est conçue pour être utilisée par une application front-end (par exemple un site web) et sera **sécurisée par une clé d’accès (API Key)**.

---

## 🎯 Objectif principal

Offrir une base solide pour gérer l’ensemble du **cycle de vie d’un bail locatif**, depuis la création d’une propriété jusqu’à la fin du contrat et le suivi des paiements.

---

## 🏗️ Structure des entités principales

### 👤 Owner (Propriétaire)
Représente la personne ou l’entreprise qui possède une ou plusieurs propriétés.

**Champs typiques :**
- `Id` — identifiant unique  
- `FullName` — nom complet  
- `Email` — adresse courriel  
- `PhoneNumber` — numéro de téléphone  
- `Properties` — liste des propriétés associées  

**Rôle :**
- Enregistre et gère ses propriétés.  
- Consulte les baux et paiements liés à ses biens.

---

### 🧑‍💼 Tenant (Locataire)
Représente la personne qui loue une propriété.

**Champs typiques :**
- `Id` — identifiant unique  
- `FullName` — nom complet  
- `Email` — adresse courriel  
- `PhoneNumber` — numéro de téléphone  
- `Leases` — liste des baux signés  

**Rôle :**
- Loue une propriété.  
- Effectue les paiements selon le contrat de bail.

---

### 🏡 Property (Propriété)
Représente un bien immobilier disponible à la location.

**Champs typiques :**
- `Id` — identifiant unique  
- `Title` — titre du bien (ex : *Appartement 2 pièces à Montréal*)  
- `Description` — détails sur la propriété  
- `Address` — adresse complète  
- `PricePerMonth` — prix mensuel  
- `OwnerId` — identifiant du propriétaire  
- `Images` — liste des images associées (`PropertyImage`)  
- `Leases` — baux liés à la propriété  

**Rôle :**
- Support principal des contrats de location.  
- Peut être libre ou occupée selon les `Leases` actifs.

---

### 📄 Lease (Contrat de location)
Entité centrale du système.  
Représente le contrat signé entre un propriétaire et un locataire pour une propriété donnée.

**Champs typiques :**
- `Id` — identifiant unique  
- `PropertyId` — propriété concernée  
- `OwnerId` — propriétaire  
- `TenantId` — locataire  
- `StartDate` / `EndDate` — durée du bail  
- `MonthlyRent` — montant du loyer  
- `Status` — *Active*, *Pending*, *Terminated*, *Expired*  
- `Payments` — liste des paiements effectués  

**Rôle :**
- Relie le propriétaire, le locataire et la propriété.  
- Sert de base au suivi des paiements et de la durée du contrat.

---

### 💰 Payment (Paiement)
Représente une transaction effectuée dans le cadre d’un bail.

**Champs typiques :**
- `Id` — identifiant unique  
- `LeaseId` — contrat lié  
- `Amount` — montant du paiement  
- `Date` — date du paiement  
- `Status` — *Pending*, *Completed*, *Late*, etc.  
- `PaymentMethod` — *CreditCard*, *BankTransfer*, *Cash*, etc.  

**Rôle :**
- Permet de suivre les paiements mensuels du locataire.  
- Gère les retards ou paiements incomplets.

---

### 🖼️ PropertyImage (Image de propriété)
Contient les images liées à une propriété.

**Champs typiques :**
- `Id` — identifiant unique  
- `PropertyId` — identifiant de la propriété  
- `ImageUrl` — lien de l’image  
- `Description` — texte descriptif  

**Rôle :**
- Permet d’illustrer les propriétés sur le front-end.

---

## ⚙️ Fonctionnement global

1. Le **propriétaire (Owner)** enregistre une ou plusieurs **propriétés (Property)**.  
2. Chaque propriété peut avoir plusieurs **images (PropertyImage)**.  
3. Un **locataire (Tenant)** loue une propriété via un **bail (Lease)**.  
4. Ce bail lie la propriété, le propriétaire et le locataire.  
5. Les **paiements (Payment)** sont ensuite enregistrés pour chaque bail.  
6. L’API fournit des **endpoints REST** pour :
   - Ajouter / modifier / supprimer des propriétés  
   - Créer ou mettre à jour un bail  
   - Enregistrer un paiement  
   - Lister les paiements par bail  
   - Consulter les propriétés disponibles ou louées  

---

## 🔐 Sécurité et architecture

- **Aucune authentification directe** (gérée côté front-end).  
- **Accès sécurisé par une clé d’API (API Key)** dans les requêtes HTTP.  
- **Architecture en couches (Clean Architecture)** :
  - **Domain** — Entités métier (`Owner`, `Tenant`, `Property`, etc.)  
  - **Infrastructure** — Base de données, repositories (EF Core)  
  - **Application** — Logique métier (services, règles de gestion)  
  - **API** — Endpoints REST (controllers)  

---

## 🧠 Bénéfices du modèle

- Séparation claire entre les rôles (Owner / Tenant).  
- Cohérence et traçabilité des données.  
- Architecture évolutive et maintenable.  
- Peut être intégré à tout type d’application (site web, mobile, SaaS).  

---

## 🧾 Exemple de relations

```text
Owner ───< Property ───< Lease >─── Tenant
                     │
                     └──< PropertyImage
Lease ───< Payment
