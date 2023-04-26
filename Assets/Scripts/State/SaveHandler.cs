using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace State
{
    /// <summary>
    /// Used to facilitate and standardise the handling of in-game instances.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SaveHandler<T>
    {
        private string DataPath => Application.persistentDataPath;

        private string _typeName;

        protected SaveHandler(string type)
        {
            _typeName = type;
        }

        /// <summary>
        /// Used to deserialize a serialized save.
        /// </summary>
        /// <param name="saveData">the buffer to deserialize</param>
        /// <returns>an instance of the object</returns>
        public abstract T LoadFromSave(Buffer saveData);

        /// <summary>
        /// Used to serialize an instance to a buffer.
        /// </summary>
        /// <param name="instance">the object instance.</param>
        /// <returns>the object serialized as a buffer.</returns>
        public abstract Buffer Serialize(T instance);

        private string CreatePath(string type, string identifier)
        {
            return Path.Join(DataPath, "Data", Path.Join(type, identifier));
        }
        
        private string CreatePath(string type)
        {
            return Path.Join(DataPath, "Data", type);
        }

        private Dictionary<string, byte[]> GetInstancesByIdentifier(string identifier)
        {
            return Directory.GetFiles(CreatePath(identifier)).ToDictionary(dir => dir, File.ReadAllBytes);
        }
        
        /// <summary>
        /// Finds and serializes all files based on the type of  
        /// </summary>
        /// <returns>A list of the target types.</returns>
        public List<T> Load()
        {
            Dictionary<string, byte[]> files = GetInstancesByIdentifier(_typeName);

            return files.Keys
                .Select((identifier) => LoadFromSave(new Buffer(files[identifier])))
                .ToList();
        }
        
        /// <summary>
        /// Serializes an instance of an object, given an identifier.
        /// and writes it to a file
        /// </summary>
        /// <param name="instance">object instance</param>
        /// <param name="identifier">a unique identifier</param>
        public void Save(T instance, string identifier)
        {
            File.WriteAllBytes(CreatePath(this._typeName, identifier),this.Serialize(instance).GetBuffer());
        }

        /// <summary>
        /// Deletes an identifier from the list of files.
        /// </summary>
        /// <param name="identifier">The identifier of the object to delete.</param>
        public void Delete(string identifier)
        {
            File.Delete(CreatePath(this._typeName, identifier));
        }
    }
}