# YouTube MP3 Downloader

**YouTube MP3 Downloader** est une application de bureau .NET 10 construite avec Avalonia UI pour extraire l’audio d’une vidéo YouTube et l’enregistrer au format MP3.

## Fonctionnalités

- Interface graphique moderne avec Avalonia
- Recherche d’informations vidéo à partir d’une URL YouTube
- Téléchargement et conversion en MP3
- Configuration du dossier de sortie via un fichier `.env`

## Architecture du projet

- `Views/` : interface utilisateur Avalonia
- `ViewModels/` : logique de présentation MVVM
- `Services/` : service de récupération et conversion YouTube
- `Models/` : modèle de données vidéo

## Technologies utilisées

- .NET 10
- Avalonia 12
- YoutubeExplode 6
- YoutubeExplode.Converter
- FFmpeg.AutoGen
- CommunityToolkit.Mvvm
- DotNetEnv

## Prérequis

- .NET 10 SDK installé
- Windows (l’application utilise `ffmpeg.exe`)
- Télécharger `ffmpeg.exe` séparément et le placer dans le dossier du projet ou l’ajouter au `PATH`

## Télécharger FFmpeg

Téléchargez une version Windows de FFmpeg depuis l’un des liens suivants :

- https://github.com/Tyrrrz/FFmpegBin/releases

## Installation

1. Cloner le dépôt :

```powershell
git clone <url-du-repo>
cd YoutubeMP3
```

2. Installer les dépendances :

```powershell
dotnet restore
```

3. Placer `ffmpeg.exe` :

- soit dans le dossier racine du projet,

4. Configurer le dossier de sortie :

Créez un fichier `.env` à la racine du projet et ajoutez la variable `PATH` vers le dossier de destination des MP3 :

```text
PATH=C:\Users\<votre-utilisateur>\Downloads\
```

> Important : terminez le chemin avec un `\`.

## Utilisation

1. Démarrez l’application :

```powershell
dotnet run
```

2. Collez l’URL YouTube dans le champ prévu.
3. Cliquez sur `Search` pour récupérer les informations de la vidéo.
4. Cliquez sur `Download MP3` pour lancer la conversion.
5. Le MP3 sera enregistré dans le dossier configuré dans `.env`.

## Points d’attention

- `ffmpeg.exe` n’est pas inclus dans le dépôt.
- Si le téléchargement échoue, vérifiez l’URL, le fichier `.env`, et la présence de `ffmpeg.exe`.
