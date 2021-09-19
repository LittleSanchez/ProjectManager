using System;
using System.Diagnostics;
using System.IO;
using Avalonia;
using ExpBag.Infrastructure.Environment;
using ExpBag.UI.Store;
using ExpBag.UI.Views;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ExpBag.UI.Services
{
    public static class StoreLoader
    {
        private static object locker = 43278534;

        private const string DbFileName = "data.mtf";
        private static readonly string DbFilePath = Path.Combine(AppdataController.AppDataFolder, DbFileName);
        private const string StoreFileName = "store.data";
        private static readonly string StoreFilePath = Path.Combine(AppdataController.AppDataFolder, StoreFileName);
        private const string StoreFileId = "0";

        public static ApplicationStore LoadStore()
        {
            try
            {
                //var db = new LiteDatabase(DbFilePath);
                //var storage = db.FileStorage;
                //storage.Download(StoreFileId, StoreFilePath, true);
                if (File.Exists(StoreFilePath))
                {
                    var store = JsonConvert.DeserializeObject<ApplicationStore>(File.ReadAllText(StoreFilePath)) ?? new ApplicationStore();
                    //File.Delete(StoreFilePath);
                    //db.Dispose();
                    return store;
                }
                throw new FileNotFoundException();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new ApplicationStore();
            }
        }

        public static void SaveStore(ApplicationStore store)
        {
            if (!Directory.Exists(Path.GetDirectoryName(DbFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(DbFilePath));
            }
            //var db = new LiteDatabase(DbFilePath);
            //var storage = db.FileStorage;
            if (File.Exists(StoreFilePath))
            {
                File.Delete(StoreFilePath);
            }
            File.WriteAllText(StoreFilePath, JsonConvert.SerializeObject(store));
            //storage.Upload(StoreFileId, StoreFilePath);
            //db.Dispose();
        }
    }
}