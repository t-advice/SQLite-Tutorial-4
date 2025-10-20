# .NET MAUI + SQLite Tutorials: MediaPicker, Camera, Save & Delete Images

This tutorial shows how to:
- pick images with MediaPicker
- capture photos with the device camera
- save image files into app storage and record metadata in SQLite
- delete images from both storage and the database

Targets .NET 9 and .NET MAUI in Visual Studio 2022.

## Features
- Local SQLite database (`sqlite-net-pcl`)
- Image metadata persisted (file name, path, timestamp)
- Files stored under `FileSystem.AppDataDirectory/photos`
- Cross-platform MediaPicker for pick/capture
- Simple gallery UI to view and delete images

## Prerequisites
- Visual Studio 2022 with .NET Multi-platform App UI development workload
- .NET 9 SDK
- Emulators/simulators or devices for Android/iOS/Windows

## Install NuGet packages
- Open __Manage NuGet Packages...__ for the MAUI project and install:
  - `sqlite-net-pcl` (SQLite ORM)


