# Montreal/Ferrous - Code Challenge
This repo contains the source code for the Montreal Mobile Xamarin Code Challenge

## Main objective
Fetch from the TMDb REST API Now Playing Movies (In Theaters) movies and informations then display the results in a user friendly application

## Development
Only the android layer was developed (but has the another platforms (IOS and UWP) in the project for future use, using Xamarin.Forms with the following versions:

* Microsoft Visual Studio 2017
* Xamarin 4.12.3.80

## Third-Party
The following nuget libraries were added to the project seeking code scalability and a better user experience:

### FFImageLoading
Caches the images on ListViews preventing image flickering during the scroll

### Autofac
Creates a dependency injection container, increasing code flexibility for customization and scalability.

### Microsoft.Net.Http
Delivers a Http client for direct and simple REST calls

### Newtonsoft.Json
Deserializes strings containing Json formatted objects

### Refit
Rest API Library to minimize code using Xamarin

### Acr User Dialogs
Is a library to show alerts, toasts, progress bar seeking a better user experience

### Plugin Permissions
Manage permissions of the mobile phone resources for the user

### Compiling
A direct build targeting a developer enabled Android device should do the trick, it's expected that the nuget packages to be restored automatically during the build.
