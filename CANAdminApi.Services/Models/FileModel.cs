using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CANAdminApi.Services.Models
{
    public class FileModel : IDisposable
    {
        public string Filename { get; set; }
        public string Extension { get; set; }
        public Stream Data { get; set; }

        // Flag: Has Dispose already been called?
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                Data.Dispose();
            }

            disposed = true;
        }

        ~FileModel()
        {
            Dispose(false);
        }
    }

    public class FileData
    {
        public string Path { get; set; }
        public string FileName { get; set; }
    }
}
