using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Web.Controllers
{
    [Authorize] // Ensures only logged-in users can upload files
    [ApiController] // Marks this as an API controller
    [Route("api/[controller]")] // Sets the route to /api/Attachment
    public class AttachmentController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AttachmentController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Handles the upload of a single file.
        /// </summary>
        /// <param name="file">The file uploaded by the client.</param>
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { message = "No file was selected for upload." });
            }

            // Define a path to save the files.
            // e.g., {YourProject}/wwwroot/attachments
            var uploadsFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "attachments");

            // Create the directory if it doesn't exist.
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            // Generate a unique filename to prevent overwriting existing files.
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

            try
            {
                // Save the file to the server.
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Create a public URL for the file that the client can use.
                var fileUrl = $"{Request.Scheme}://{Request.Host}/attachments/{uniqueFileName}";

                // Return the URL and original filename to the client.
                return Ok(new
                {
                    url = fileUrl,
                    fileName = file.FileName
                });
            }
            catch (Exception ex)
            {
                // Return an error if the file could not be saved.
                return StatusCode(500, new { message = "An error occurred while uploading the file.", details = ex.Message });
            }
        }
    }
}
