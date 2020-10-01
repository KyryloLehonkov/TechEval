using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TechEval.Commands;
using TechEval.Commands.Results;
using TechEval.Core;

namespace TechEval.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICommandDispatcher _dispatcher;

        [BindProperty]
        public IFormFile Upload { get; set; }
        public IndexModel(ILogger<IndexModel> logger, ICommandDispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            CommandResult<FileUploadCommandResult> result;

            try
            {
                switch (Path.GetExtension(Upload.FileName)) //little bit ugly, actually in real world better to detemine filetype by its content
                                                            // and build call to dispatcher on the fly
                {
                    case ".xml":
                        result = await _dispatcher.Dispatch<XmlFileUploadCommand, FileUploadCommandResult>(new XmlFileUploadCommand { UploadedFile = Upload.OpenReadStream() });
                        break;
                    case ".csv":
                        result = await _dispatcher.Dispatch<CsvFileUploadCommand, FileUploadCommandResult>(new CsvFileUploadCommand { UploadedFile = Upload.OpenReadStream() });
                        break;
                    default: return BadRequest("Unknown format");
                }

            }
            catch (FormatUnknownException ex) // domain exceptions like this should be handled in a middleware
            {
                return BadRequest(ex.Message);
            }


            if (result.Value.IsOk) return StatusCode(200);

            else return BadRequest(result.Value.FailedRecords);




        }
    }
}
