using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace State
{
    public abstract class SaveHandler<T>
    {
        private string DataPath => Application.persistentDataPath;

        private string _typeName;

        public SaveHandler(string type)
        {
            _typeName = type;
        }

        public abstract T LoadFromSave(Buffer saveData);

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
        
        public List<T> Load()
        {
            Dictionary<string, byte[]> files = GetInstancesByIdentifier(this._typeName);

            return files.Keys
                .Select((identifier) => LoadFromSave(new Buffer(files[identifier])))
                .ToList();
        }
        
        public void Save(T instance, string identifier)
        {
            File.WriteAllBytes(CreatePath(this._typeName, identifier),this.Serialize(instance).GetBuffer());
        }

        public void Delete(string identifier)
        {
            File.Delete(CreatePath(this._typeName, identifier));
        }
    }
}