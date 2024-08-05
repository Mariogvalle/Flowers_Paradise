namespace Flowers_Paradise.ViewModels
{
    internal class FirebaseAuthProvider
    {
        private FirebaseConfig firebaseConfig;

        public FirebaseAuthProvider(FirebaseConfig firebaseConfig)
        {
            this.firebaseConfig = firebaseConfig;
        }

        internal async Task CreateUserWithEmailAndPasswordAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        internal async Task SignInWithEmailAndPasswordAsync(string userName, string userPassword)
        {
            throw new NotImplementedException();
        }
    }
}