using MediatR;

namespace Angular_Crud_C_.Services.Commands.SaveImageCommand
{
	public class SaveImageCommand : IRequest<string>
	{
		public FormFile File { get; set; }
		public string BaseUrl { get; set; }
	}
}
