using System;
using System.Diagnostics;
using System.IO;
using Avalonia;
using ExpBag.UI.Store;
using ExpBag.UI.Views;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ExpBag.UI.Services
{
    public class StoreLoader
    {
        private const string DbFileName = "data.mtf";
        private const string StoreFileName = "store.data";
        private const string StoreFileId = "0";

        public static ApplicationStore LoadStore()
        {
            try
            {

                using (var db = new LiteDatabase(Path.Combine(Path.GetTempPath(), DbFileName)))
                {
                    var storage = db.FileStorage;
                    storage.Download(StoreFileId, StoreFileName, true);
                    var store = JsonConvert.DeserializeObject<ApplicationStore>(File.ReadAllText(StoreFileName)) ?? new ApplicationStore();
                    File.Delete(StoreFileName);
                    return store;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new ApplicationStore();
            }
        }

        public static void SaveStore(ApplicationStore store)
        {
            using (var db = new LiteDatabase(Path.Combine(Path.GetTempPath(), DbFileName)))
            {
                var storage = db.FileStorage;
                File.WriteAllText(StoreFileName, JsonConvert.SerializeObject(store));
                storage.Upload(StoreFileId, StoreFileName);
                File.Delete(StoreFileName);
            }
        }
    }
}