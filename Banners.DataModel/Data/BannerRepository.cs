using Banners.DataModel.Models;
using Banners.DataModel.Properties;
using MongoDB.Driver;
using System;
using System.Linq;

namespace Banners.DataModel.Data
{
    public class BannerRepository
    {

        private IMongoDatabase database;
        private IMongoCollection<Banner> collection;


        public BannerRepository()
        {
            var client = new MongoClient(Settings.Default.BannerflowConnectionString);
            database = client.GetDatabase(Settings.Default.BannerflowDatabaseName);
            collection = database.GetCollection<Banner>("banners");

        }

        public Banner Get(int id)
        {
            var banner = collection
                .Find(document => document.Id == id)
                .FirstOrDefault();
            return banner;
        }

        public RepositoryActionResult<Banner> Add(Banner banner)
        {
            try
            {
                banner.Created = DateTime.Now;
                banner.Modified = null;
                collection.InsertOne(banner);

                return new RepositoryActionResult<Banner>(banner, RepositoryActionStatus.Created);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Banner>(null, RepositoryActionStatus.Error, ex);
                
            }
        }

        public RepositoryActionResult<Banner> Update(Banner banner)
        {
            try
            {
                var modification = Builders<Banner>.Update
                .Set(b => b.Html, banner.Html)
                .Set(b => b.Modified, DateTime.Now);
                var result = collection.FindOneAndUpdate(b => b.Id == banner.Id, modification); 

                if (result != null)
                {
                    return new RepositoryActionResult<Banner>(banner, RepositoryActionStatus.Updated);
                } 
                else
                {
                    return new RepositoryActionResult<Banner>(banner, RepositoryActionStatus.NotFound);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Banner>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Banner> Delete(int id)
        {
            try
            {
               var result = collection.FindOneAndDelete(b => b.Id == id); 

               if (result == null)
                {
                    return new RepositoryActionResult<Banner>(null, RepositoryActionStatus.NotFound); 
                } 
               else
                {
                    return new RepositoryActionResult<Banner>(null, RepositoryActionStatus.Deleted);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Banner>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}
