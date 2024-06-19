using Microsoft.AspNetCore.Mvc;
using Angular_Crud_C_.Services.Commands.SaveEditorContentCommand;
using Angular_Crud_C_.Services.Commands.SaveImageCommand;
using Angular_Crud_C_.Services.Commands.UpdateContentByIdCommand;
using Angular_Crud_C_.Services.Queries.GetAllContentQueries;
using Angular_Crud_C_.Services.Queries.GetAllProductQueries;
using Angular_Crud_C_.Services.Queries.GetContentById;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections;
using Angular_Crud_C_.Services.Commands.DeleteContentByIdCommand;
using GemBox.Pdf.Content;
using GemBox.Pdf;
using System.IO;
using Angular_Crud_C_.Models;
using GemBox.Document;
using Microsoft.IdentityModel.Tokens;

namespace Angular_Crud_C_.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FroalaController : ControllerBase
	{
		//this is froala controller
		private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
		private readonly ISender _mediator;

		public FroalaController(ISender mediator, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
		{
			_mediator = mediator;
			_hostingEnvironment = hostingEnvironment;
		}

		[HttpPost]
		public async Task<IActionResult> SaveEditorContent(SaveEditorContentCommand command)
		{
			var contentId = await _mediator.Send(command);
			return Ok(contentId);
		}

		[HttpGet]
		public async Task<IActionResult> GetContents()
		{
			var contents = await _mediator.Send(new GetAllContentQuery());
			return Ok(contents);
		}

		[HttpGet("GetById/{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			var resp = await _mediator.Send(new GetContentByIdQuery(id));
			return Ok(resp);
		}

        [HttpGet("getstring")]
        public string Getstring()
        {
            return "abc";
        }

        [HttpDelete("DeleteById/{id}")]
		public async Task<IActionResult> DeleteById(string id)
		{
			var resp = await _mediator.Send(new DeleteContentByIdCommand(id));
			return Ok(resp);
		}

		[HttpPut("updateEditorContentById")]
		public async Task<IActionResult> updateEditorContentById([FromBody] UpdateContentByIdCommand updatedContent)
		{
			var command = new UpdateContentByIdCommand(updatedContent.Id, updatedContent.UpdatedContent);
			var updatedFroala = await _mediator.Send(command);

			if (updatedFroala != null)
			{
				return Ok(updatedFroala);
			}

			return BadRequest("Failed to update content.");
		}

		[HttpPost("UploadFiles")]
		[Produces("application/json")]
		public async Task<IActionResult> Post(List<IFormFile> files)
		{
			var theFile = HttpContext.Request.Form.Files.GetFile("file");

			string webRootPath = _hostingEnvironment.WebRootPath; ;

			var fileRoute = Path.Combine(webRootPath, "uploads");

			var mimeType = HttpContext.Request.Form.Files.GetFile("file").ContentType;

			string extension = System.IO.Path.GetExtension(theFile.FileName);

			string name = Guid.NewGuid().ToString().Substring(0, 8) + extension;


			string link = Path.Combine(fileRoute, name);

			FileInfo dir = new FileInfo(fileRoute);
			dir.Directory.Create();

			string[] imageMimetypes = { "image/gif", "image/jpeg", "image/pjpeg", "image/x-png", "image/png", "image/svg+xml" };
			string[] imageExt = { ".gif", ".jpeg", ".jpg", ".png", ".svg", ".blob" };

			try
			{
				if (Array.IndexOf(imageMimetypes, mimeType) >= 0 && (Array.IndexOf(imageExt, extension) >= 0))
				{
					Stream stream;
					stream = new MemoryStream();
					theFile.CopyTo(stream);
					stream.Position = 0;
					String serverPath = link;

					using (FileStream writerFileStream = System.IO.File.Create(serverPath))
					{
						await stream.CopyToAsync(writerFileStream);
						writerFileStream.Dispose();
					}

					Hashtable imageUrl = new Hashtable();
					imageUrl.Add("link", "https://localhost:7220/uploads/" + name);

					return new JsonResult(imageUrl);
				}
				throw new ArgumentException("The image did not pass the validation");
			}

			catch (ArgumentException ex)
			{
				return new JsonResult(ex.Message);
			}
		}
		
		[HttpPost("GeneratePdf")]
		public IActionResult GeneratePdf([FromBody] PdfRequest request)
		{
			if (request == null || string.IsNullOrEmpty(request.Content))
			{
				return BadRequest("Invalid request data: Content is null or empty.");
			}
            GemBox.Pdf.ComponentInfo.SetLicense("FREE-LIMITED-KEY");

			PdfDocument document = new PdfDocument();

			PdfPage page = document.Pages.Add();

			PdfFormattedText formattedText = new PdfFormattedText();

			formattedText.Append(request.Content);

			page.Content.DrawText(formattedText, new PdfPoint(100, 700));

			MemoryStream stream = new MemoryStream();
			document.Save(stream);

			stream.Position = 0;

			return Ok(stream);
		}

		[HttpPost("GenerateWordFile")]
		public IActionResult GenerateWordFile([FromBody] PdfRequest editorContent)
		{
			if (string.IsNullOrEmpty(editorContent.Content))
			{
				return BadRequest("Invalid request data: Editor content is null or empty.");
			}

            GemBox.Document.ComponentInfo.SetLicense("FREE-LIMITED-KEY");

			DocumentModel document = new DocumentModel();
			
			Section section = new Section(document);
			document.Sections.Add(section);

			Paragraph paragraph = new Paragraph(document);
			section.Blocks.Add(paragraph);

			Run run = new Run(document, editorContent.Content);
			paragraph.Inlines.Add(run);

			var fileName = "GeneratedDocument.docx";
			var filePath = Path.Combine(Path.GetTempPath(), fileName);

			try
			{
				document.Save(filePath);
				byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
				document = DocumentModel.Load(filePath);
				var acceptRevisions = true;

				if (acceptRevisions)
					document.Revisions.AcceptAll();
				else
					document.Revisions.RejectAll();
				return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Error generating Word file: {ex.Message}");
			}
			finally
			{
				if (System.IO.File.Exists(filePath))
				{
					System.IO.File.Delete(filePath);
				}
			}
		}
	}
}
