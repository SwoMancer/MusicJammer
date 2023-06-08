using MusicJammer.PseudoModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MusicJammer.Data
{
    /// <summary>
    /// Defines the <see cref="FileHandler" />.<br/>
    /// It is used to retrieve data from a local device.
    /// <para>
    /// Resources:<br/>
    /// <see href="//https://docs.microsoft.com/en-us/xamarin/essentials/media-picker?tabs=android">docs.microsoft.com: media picker for android</see>
    /// </para>
    /// </summary>
    public static class FileHandler
    {
        #region Methods
        /// <summary>
        /// <c>PickAndShow()</c> lets the user select audio files from the device.
        /// Saves the files temporarily in TemporarilySoundVM.
        /// </summary>
        public static async Task PickAndShow()
        {
            try
            {
                //Apply the filter and wait for the user to pick an audio file.
                var result = await FilePicker.PickAsync(Gets());

                if (result != null)
                {
                    //A typical audio file format that works with Xam.Plugin.SimpleAudioPlayer is mp3 and wav.
                    //So only get that type of audio file.
                    if (result.FileName.EndsWith("mp3", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("wav", StringComparison.OrdinalIgnoreCase))
                    {
                        var stream = await result.OpenReadAsync();
                        byte[] soundBytes = ReadFully(stream);

                        SaveFile(soundBytes, result.FileName);

                        //Add to the Temporarily Sound View Model.
                        App.TemporarilySoundVM.AddSoundItemAsync(new Model.SoundItem(result.FileName, result.FullPath, result.FileName)
                        {
                            NormalColor = App.Setting.HighlightColor.Hex,
                            SelectedColor = App.Setting.ComplimentaryHighlightColor.Hex
                        });
                    }
                }

            }
            catch (FeatureNotSupportedException)
            {
                // Feature is not supported on the device
                Console.WriteLine("Feature is not supported on the device");
            }
            catch (PermissionException)
            {
                // Permissions not granted
                Console.WriteLine("Permissions not granted");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }
        /// <summary>
        /// <c>ReadFully()</c> convert a Stream to a byte array.
        /// </summary>
        /// <param name="input">Input a file in stream format.</param>
        /// <returns>File in a byte array format.</returns>
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        /// <summary>
        /// <c>SaveFile()</c> Saves a file in a byte array format to a local device.
        /// </summary>
        /// <param name="data">Input a file in a byte array format.</param>
        /// <param name="fileName">Input a Name in a String format.</param>
        public static void SaveFile(byte[] data, string fileName)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
            File.WriteAllBytes(filePath, data);
        }
        /// <summary>
        /// <c>SaveFile()</c> Saves a file in a byte array format to a local device.
        /// </summary>
        /// <param name="data">Input a file in a byte array format.</param>
        /// <param name="fileName">Input a Name in a String format.</param>
        public static async Task SaveFileAsync(byte[] data, string fileName)
        {
            await Task.Run(() => SaveFile(data, fileName));
        }
        /// <summary>
        /// <c>LoadFile()</c> finds a file on the <c>Environment.SpecialFolder.LocalApplicationData</c> to load.
        /// </summary>
        /// <param name="fileName">Input a file name in a string format.</param>
        /// <returns>A file in byte array format.</returns>
        public static byte[] LoadFile(string fileName)
        {
            string fileExactLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
            byte[] data = File.ReadAllBytes(fileExactLocation);
            return data;
        }
        /// <summary>
        /// <c>Gets()</c> is a static filter to get only audio files to return with <c>Xamarin.Essentials.FilePicker</c>.
        /// </summary>
        /// <returns><c>PickOptions</c> that is a filter for the <c>FilePicker</c>.</returns>
        private static PickOptions Gets()
        {
            var customFileType =
                new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { "public.my.mp3.extension" } }, // or general UTType values
                    { DevicePlatform.Android, new[] { "audio/*" } },      //Hitta string om det finns tid. 
                    { DevicePlatform.UWP, new[] { "mp3", ".av" } },
                    { DevicePlatform.Tizen, new[] { "*mp3*" } },
                    { DevicePlatform.macOS, new[] { "mp3", "wav" } }, // or general UTType values
                });
            var options = new PickOptions
            {
                PickerTitle = "Please select a audio file",
                FileTypes = customFileType,
            };

            return options;
        }
        /// <summary>
        /// See if a file already exists with that name on the device.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns><see cref="Boolean"/></returns>
        public static bool AlreadyExists(string filename)
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), filename);
            
            //Finns den redan?
            return File.Exists(fileName);
        }
        /// <summary>
        /// <c>LoadFile()</c> finds a file on the <c>Environment.SpecialFolder.LocalApplicationData</c> to load.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns><typeparamref name="T"/> <see cref="object"/></returns>
        public static T LoadFile<T>(string fileName)
        {
            //Find out by checking how JsonConvert.DeserializeObject T works

            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);

            string jsonText = File.ReadAllText(filePath);
            T tItem = JsonConvert.DeserializeObject<T>(jsonText);
            return tItem;
        }
        /// <summary>
        /// Saves a file on the <c>Environment.SpecialFolder.LocalApplicationData</c> to be load later.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="t"></param>
        public static void SaveFile<T>(string fileName, T t)
        {
            string filePath= Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);

            try
            {
                string jsonString = JsonConvert.SerializeObject(t, Formatting.Indented);
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        #endregion
    }
}
