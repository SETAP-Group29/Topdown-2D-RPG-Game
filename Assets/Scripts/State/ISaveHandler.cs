namespace State
{
    public interface ISaveHandler<T>
    {
        public string GetSaveIdentifier();
        
        public T LoadFromSave(Buffer saveData);

        public bool ShouldSave(T instance);

        public Buffer Save(T instance);
    }
}