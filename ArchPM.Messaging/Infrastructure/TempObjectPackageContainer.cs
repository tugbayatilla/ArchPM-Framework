using System;
using System.Collections.Generic;
using System.Linq;

namespace ArchPM.Messaging.Infrastructure
{
    /// <summary>
    /// Holds temprorary packages by guid
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class TempObjectPackageContainer<T> where T: class
    {
        /// <summary>
        /// The packages
        /// </summary>
        readonly List<TempObjectPackage<T>> packages;

        /// <summary>
        /// Initializes a new instance of the <see cref="TempObjectPackageContainer{T}" /> class.
        /// </summary>
        public TempObjectPackageContainer()
        {
            this.packages = new List<TempObjectPackage<T>>();
        }

        /// <summary>
        /// Adds the package to the list. if not exist, creates a new one. Thread Safe.
        /// </summary>
        /// <param name="packageId">The package identifier.</param>
        /// <param name="tempObject">The temporary object.</param>
        public void AddPackage(Guid packageId, T tempObject)
        {
            lock (this)
            {
                var package = CreatePackageIfNotExist(packageId);
                package.Data.Add(tempObject);
            }
        }

        /// <summary>
        /// Removes the expired packages. Thread Safe
        /// </summary>
        /// <param name="expirationSpan">The expiration span.</param>
        /// <returns></returns>
        public Int32 RemoveExpiredPackages(TimeSpan expirationSpan)
        {
            Int32 removedItemsCount = 0;
            lock (this)
            {
                var list = packages.ToList();
                foreach (var item in list)
                {
                    //expiration span defined in config and container create time + expiration span time less then now
                    if (expirationSpan != null &&
                        item.CreateTime.Add(expirationSpan) <= DateTime.Now)
                    {
                        //invalid situation: expiration occurs
                        this.packages.Remove(item);
                        removedItemsCount++;
                    }
                }
            }
            return removedItemsCount;
        }

        /// <summary>
        /// Gets the package. Thread Safe.
        /// </summary>
        /// <param name="packageId">The package identifier.</param>
        /// <returns></returns>
        public TempObjectPackage<T> GetPackage(Guid packageId)
        {
            TempObjectPackage<T> result = new TempObjectPackage<T>();
            lock (this)
            {
                var package = this.packages.FirstOrDefault(p => p.Id == packageId);
                if (package != null)
                    result = package;
            }
            return result;
        }

        /// <summary>
        /// Remove the package accordng to given id. It is thread safe
        /// </summary>
        /// <param name="packageId">The package identifier.</param>
        /// <returns></returns>
        public Int32 RemovePackage(Guid packageId)
        {
            Int32 removedItemsCount = 0;
            lock (this)
            {
                var package = this.packages.FirstOrDefault(p => p.Id == packageId);
                if (package != null)
                {
                    this.packages.Remove(package);
                    removedItemsCount++;
                }
            }
            return removedItemsCount;
        }

        /// <summary>
        /// Creates the package if not exist.
        /// </summary>
        /// <param name="packageId">The package identifier.</param>
        /// <returns></returns>
        TempObjectPackage<T> CreatePackageIfNotExist(Guid packageId)
        {
            lock (this)
            {
                TempObjectPackage<T> package = this.packages.FirstOrDefault(p => p.Id == packageId);
                if (package == null) //not exist
                {
                    package = new TempObjectPackage<T>() { Id = packageId };
                    this.packages.Add(package);
                }
                return package;
            }
        }
    }

}
