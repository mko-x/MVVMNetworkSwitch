using System;

namespace NetProfileSwitcher.Interfaces.Storage.Tasks
{
    public interface IWriteTask<T> : IStorageBaseTask
    {
        /// <summary>
        /// Do write data to the file
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fileName"></param>
        void Execute(T data, String fileName);
    }

    public interface IWriteTask<TData, TStore> : IWriteTask<TData>
    {
        
    }
}
