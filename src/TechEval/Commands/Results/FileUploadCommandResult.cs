using System.Collections.Generic;
using TechEval.DataContext;

namespace TechEval.Commands.Results
{
    public class FileUploadCommandResult 
    {
        public bool IsOk { get; set; }
        public  List<ImportLog> FailedRecords;
    }
}
