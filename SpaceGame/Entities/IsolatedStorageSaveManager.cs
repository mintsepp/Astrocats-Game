using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;
using FlatRedBall.Math.Geometry;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Xml.Serialization;

namespace MonoGameSaveManager
{
    /// <summary>
    /// Uses the .NET IsolatedStorage to load/save game data.
    /// Saves end up on "C:\Users\...\AppData\Local\IsolatedStorage\..." on Windows. 
    /// </summary>
    public class IsolatedStorageSaveManager : SaveManager
    {
        /// <summary>
        /// /// <summary>
        /// Creates a new save game manager based on .NET IsolatedStorage.
        /// </summary>
        /// <param name="folderName">Name of the folder containing the save.</param>
        /// <param name="fileName">Name of the save file.</param>
        public IsolatedStorageSaveManager(string folderName, string fileName)
            : base(folderName, fileName)
        { }

        public override void Load()
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                // Ignore if directory doesn't exist.
                if (!isf.DirectoryExists(folderName))
                    return;

                string filePath = Path.Combine(folderName, fileName);

                // Ignore if save doesn't exist.
                if (!isf.FileExists(filePath))
                    return;

                // Open the save file.
                using (IsolatedStorageFileStream stream = isf.OpenFile(filePath, FileMode.Open))
                {
                    // Get the XML data from the stream and convert it to object.
                    XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                    Data = (SaveData)serializer.Deserialize(stream);
                }
            }
        }

        public override void Save()
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                // Create directory if it doesn't exist.
                if (!isf.DirectoryExists(folderName))
                    isf.CreateDirectory(folderName);

                string filePath = Path.Combine(folderName, fileName);

                // Create new save file.
                using (IsolatedStorageFileStream stream = isf.CreateFile(filePath))
                {
                    // Convert the object to XML data and put it in the stream.
                    XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                    serializer.Serialize(stream, Data);
                }
            }
        }
    }
}



namespace SpaceGame.Entities
{
	public partial class IsolatedStorageSaveManager
	{
        /// <summary>
        /// Initialization logic which is execute only one time for this Entity (unless the Entity is pooled).
        /// This method is called when the Entity is added to managers. Entities which are instantiated but not
        /// added to managers will not have this method called.
        /// </summary>
		private void CustomInitialize()
		{


		}

		private void CustomActivity()
		{


		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}
