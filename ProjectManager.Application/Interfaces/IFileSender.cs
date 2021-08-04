using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Interfaces
{

    public enum FileSenderResult
    {
        Ok = 0,
        Aborted = 1,
        Error = 2
    }

    public interface IFileSender
    {
        void SendFile(string url, string filePath);
        void SendDataAsFile(string url, byte[] fileData);
    }
}
