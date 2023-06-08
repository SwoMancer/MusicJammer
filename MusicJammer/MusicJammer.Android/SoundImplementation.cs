using Android.Media;
using MusicJammer.Data.Interface;
using MusicJammer.Droid;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using MusicJammer.Data;
using Xamarin.Forms;
using Android.Content;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.Dependency(typeof(SoundImplementation))]
namespace MusicJammer.Droid
{

    /// <summary>
    /// Defines the <see cref="SoundImplementation" />.
    /// </summary>
    public class SoundImplementation : IAudioPlayEx
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SoundImplementation"/> class.
        /// </summary>
        public SoundImplementation()
        {
        }

        /// <summary>
        /// The PlayAudioFile.
        /// </summary>
        /// <param name="fileName">The fileName<see cref="string"/>.</param>
        public void PlayAudioFile(string fileName)
        {
            //string androidFileName = SaveAndView(fileName);

            var player = new MediaPlayer();
            var fd = global::Android.App.Application.Context.Assets.OpenFd(fileName);
            player.Prepared += (s, e) =>
            {
                player.Start();
            };
            player.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);
            player.Prepare();
        }

        /// <summary>
        /// The SaveAndView.
        /// </summary>
        /// <param name="fileName">The fileName<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private string SaveAndView(string fileName)
        {
            try
            {
                //string rootPath = Android.OS.Environment.IsExternalStorageEmulated ? Android.OS.Environment.ExternalStorageDirectory.ToString() : System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
                string rootPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
                var filePathDir = Path.Combine(rootPath, "meusarquivos");
                if (!System.IO.File.Exists(filePathDir))
                {
                    Directory.CreateDirectory(filePathDir);
                }
                string filePath = Path.Combine(filePathDir, fileName);



                return "";

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
                return "";
            }
        }
    }
}
