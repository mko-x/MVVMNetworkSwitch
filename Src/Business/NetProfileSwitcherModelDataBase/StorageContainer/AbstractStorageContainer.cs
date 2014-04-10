using System;
using NetProfileSwitcher.Interfaces;
using NetProfileSwitcher.Interfaces.Storage.Tasks;
using NetProfileSwitcher.Logs;
using NetProfileSwitcher.Logs.Info;
using NetProfileSwitcher.Logs.Interfaces;
using NetProfileSwitcher.Util;

namespace NetProfileSwitcher.ModelDataStore.StorageContainer
{
    public abstract class AbstractStorageContainer<T> : IStorageContainerProfiles<T>, INeedInjection<Logger>
    {
        #region - private member
        private IDeleteTask _deleteTask;
        private IWriteTask<IProfiles> _writeTask;
        private IReadTask<IProfiles> _readTask;

        private bool _initialized;

        private string _targetFile;
        ILog logger;
        #endregion - private member

        #region - constructors
        /// <summary>
        /// This empty constructor requires to override :initTasksInternal()
        /// </summary>
        public AbstractStorageContainer()
        {
            this.internalInit();
        }
        #endregion - constructors

        #region - public interface
        /// <summary>
        /// Read the current fullName from the storage
        /// </summary>
        /// <returns></returns>
        public IProfiles Read()
        {
            if (isInitialized())
            {
                return this.executeRead();
            }
            return null;
        }

        /// <summary>
        /// Write new fullName to the storage
        /// </summary>
        /// <param name="fullName">Data to write</param>
        public void Write(IProfiles data)
        {
            if (isInitialized())
            {
                this.executeWrite(data);
            }            
        }

        /// <summary>
        /// Delete the current fullName from the storage
        /// </summary>
        public void Delete()
        {
            if (isInitialized())
            {
                this.executeDelete();
            }
        }

        /// <summary>
        /// Create HashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return typeof(T).GetHashCode() + this.GetHashCode();
        }
        #endregion - public interface

        #region - protected/abstract 
        /// <summary>
        /// Override this method if you don't want to inject the tasks in constructor
        /// </summary>
        protected abstract void initTasksInternal();

        /// <summary>
        /// Initialize the tasks
        /// </summary>
        /// <param name="deleteTask"></param>
        /// <param name="writeTask"></param>
        /// <param name="readTask"></param>
        protected void initTasks(IDeleteTask deleteTask, IWriteTask<IProfiles> writeTask, IReadTask<IProfiles> readTask)
        {
            this._deleteTask = deleteTask;
            this._writeTask = writeTask;
            this._readTask = readTask;

            this.ensureTasks();
        }
        #endregion - protected/abstract

        #region - private methods
        /// <summary>
        /// Start of the member ensurance routine
        /// </summary>
        private void internalInit()
        {            
            _initialized = false;
            this._targetFile = StoragePath.FilePathWithFileName;
            initTasksInternal();
            ensureInitialization();
        }

        /// <summary>
        /// Checks all necessary memmbers for existence
        /// </summary>
        /// <returns>true if class instance is ready</returns>
        private bool ensureInitialization()
        {
            return _initialized = ensureTasks() && !String.IsNullOrWhiteSpace(_targetFile);
        }

        /// <summary>
        /// Ensures that all tasks exist
        /// </summary>
        /// <returns>true if tasks are ready</returns>
        private bool ensureTasks()
        {
            if (this._deleteTask == null || this._writeTask == null || this._readTask == null)
            {
                this.throwInitializationException();
            }
            return true;
        }

        /// <summary>
        /// call execute
        /// </summary>
        /// <returns></returns>
        private IProfiles executeRead()
        {
            logger.Log("Reading");
            return _readTask.Execute(_targetFile);
        }

        /// <summary>
        /// call execute
        /// </summary>
        private void executeDelete()
        {
            logger.Log("Deleting");
             _deleteTask.Execute(_targetFile);
        }

        /// <summary>
        /// call execute
        /// </summary>
        /// <param name="fullName"></param>
        private void executeWrite(IProfiles data)
        {
            logger.Log("Writing");
            _writeTask.Execute(data, _targetFile);
        }

        /// <summary>
        /// Is the instance ready
        /// </summary>
        /// <returns></returns>
        public bool isInitialized()
        {
            logger.Debug("Storage inititalized: ", _initialized);
            return this._initialized;
        }

        /// <summary>
        /// Throws an exception that shows errors at initialization
        /// </summary>
        private void throwInitializationException()
        {
            FieldAccessException exc = new FieldAccessException("Tasks were not initialized");
            logger.Fatal(exc);
            throw new TypeInitializationException(typeof(AbstractStorageContainer<T>).FullName, exc);
        }
        #endregion - private methods


        public void Inject(Logger injection)
        {
            this.logger = injection;
        }
    }
}
