Ce dépôt contient le serveur backend développé en .NET Core. Il propose **deux API distinctes** :

- **API** avec contrôleurs → utilisée par `Frontend-Eleve-Projet-Formation`.
- **API minimale** sans contrôleurs → utilisée par `Frontend-Prof-Projet-Formation`.

## 🚀 Dépendances avec les autres dépôts

Ce projet fonctionne en conjonction avec **3 autres dépôts** :

- [BDD-Projet-Formation](https://github.com/giantprolu/BDD-Projet-Formation) (Base de données)
- [Frontend-Prof-Projet-Formation](https://github.com/giantprolu/Frontend-Prof-Projet-Formation) (Application des professeurs)
- [Frontend-Eleve-Projet-Formation](https://github.com/giantprolu/Frontend-Eleve-Projet-Formation) (Application des élèves)

Chaque frontend **doit se connecter à l'API correspondante**.

## 🛠️ Installation

1. **Clonez le dépôt** :
   ```bash
   git clone https://github.com/giantprolu/Backend-Projet-Formation.git
   cd Backend-Projet-Formation
2. **Restaurez les dépendances** :
   ```bash
   dotnet restore 

## 🚀 Démarrage des APIs
- **API (pour Frontend Élève)** :
    ```bash
    cd API
    dotnet run
- **API minimale (pour Frontend Prof)** :
    ```bash
    cd Minimal
    dotnet run