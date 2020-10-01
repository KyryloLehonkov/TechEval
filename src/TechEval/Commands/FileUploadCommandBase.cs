using System.IO;

namespace TechEval.Commands
{
    public class FileUploadCommandBase
    {
        public Stream UploadedFile { get; set; }
    }
}
