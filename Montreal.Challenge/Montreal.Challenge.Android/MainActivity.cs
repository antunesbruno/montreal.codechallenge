using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using FFImageLoading.Forms.Droid;
using Montreal.Challenge.Shared;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Permission = Plugin.Permissions.Abstractions.Permission;

namespace Montreal.Challenge.Droid
{    
    [Activity(Label = "Montreal.Challenge", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            //set absolute path
            FilePlatformOS.OS_ABSOLUTE_PATH = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;

            //FFImageLoading
            CachedImageRenderer.Init(true);
            
            //ACR Dialogs
            UserDialogs.Init(this);

            //Cross Permissions
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);

            //Request Permissions
            CheckRequestPermissions();

            LoadApplication(new App(new AndroidInitializer()));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void CheckRequestPermissions()
        {
            Permission[] arrPermission = {Permission.Storage, Permission.Photos, Permission.MediaLibrary, Permission.Camera, Permission.Location };

            Task.Run(async () =>
            {
                foreach (Permission permission in arrPermission)
                {                 
                    var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
                    if (permissionStatus != PermissionStatus.Granted)
                    {
                        var response = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                        var userResponse = response[permission];
                    }
                }              
            });
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }   
}

