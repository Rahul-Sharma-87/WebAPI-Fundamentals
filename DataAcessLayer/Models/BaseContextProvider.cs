using System;

namespace DataAcessLayer.Models {
    public abstract class BaseContextProvider: IDisposable {
        protected BaseContextProvider(PracticeSQLEntities dbContext) {
            DbContext = dbContext;
        }

        protected PracticeSQLEntities DbContext { get; private set; }

        ~BaseContextProvider() {
            Dispose(false);
        }

        public void Dispose() {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                DbContext.Dispose();
            }
        }
    }
}
