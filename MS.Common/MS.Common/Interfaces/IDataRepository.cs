using System;
using System.Threading.Tasks;

namespace MS.Common.Interfaces
{
    /// <summary>
    /// IDataRepository
    /// </summary>
    public interface IDataRepository
    {
        /// <summary>
        ///
        /// </summary>
        void CommitTransaction();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Task UpdateDatabase();

        /// <summary>
        ///
        /// </summary>
        /// <param name="isolationLevel"></param>
        void BeginTransaction(int isolationLevel);

        /// <summary>
        ///
        /// </summary>
        void BeginTransaction();

        /// <summary>
        ///
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Object OpenConnection();

        /// <summary>
        ///
        /// </summary>
        void CloseConnection();

        /// <summary>
        ///
        /// </summary>
        /// <param name="dbConnection"></param>
        void OpenConnection(Object dbConnection);

        /// <summary>
        ///
        /// </summary>
        /// <param name="connectionString"></param>
        void OpenConnection(string connectionString);
    }
}