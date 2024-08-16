using Firebase.Auth;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseDemoApp
{
    public class FireStorage
    {
        private readonly FirebaseStorage _storage;

        public FireStorage()
        {
            string projectId = "demoproject-6d382";



            _storage = new FirebaseStorage(
           $"{projectId}.appspot.com",
           new FirebaseStorageOptions
           {
               AuthTokenAsyncFactory = async () =>
               {
                   var authService = new FirebaseService();
                   var auth = await authService.LoginUserAsync("demo@gmail.com", "123456789");
                   return auth.FirebaseToken;
               }
           }
            );
        }
        public async Task<string> SaveFileToStorageAsync(string folder, string fileName, Stream fileStream)
        {
            try
            {

                var storageReference = _storage.Child(folder).Child(fileName);
                var downloadUrl = await storageReference.PutAsync(fileStream);

                return downloadUrl;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
