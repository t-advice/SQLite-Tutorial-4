# .NET MAUI + SQLite Tutorials: MediaPicker, Camera, Save & Delete Images

This tutorial shows how to:
- pick images with MediaPicker
- capture photos with the device camera
- save image files into app storage and record metadata in SQLite
- delete images from both storage and the database

Targets .NET 9 and .NET MAUI in Visual Studio 2022.
# SQLiteTutorial4

Simple .NET MAUI image gallery that uses SQLite to store file paths for images picked or captured using MediaPicker.

Features:
- Pick photo from library
- Capture photo with camera
- Save file locally and record in SQLite
- Grid view of thumbnails
- View full image and delete (removes file + DB record)

Platform notes:
- Add camera/photo usage descriptions in iOS Info.plist.
- Add camera/storage permissions in AndroidManifest and handle runtime permissions when needed.


## Prerequisites
- Visual Studio 2022 with .NET Multi-platform App UI development workload
- .NET 9 SDK
- Emulators/simulators or devices for Android/iOS/Windows

## Install NuGet packages
- Open __Manage NuGet Packages...__ for the MAUI project and install:
  - `sqlite-net-pcl` (SQLite ORM)


