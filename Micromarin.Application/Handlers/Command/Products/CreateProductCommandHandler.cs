using AutoMapper;
using MediatR;
using Micromarin.Application.Commands.Products;
using Micromarin.Application.Interfaces.Repositories;
using Micromarin.Domain.Interfaces;


namespace Micromarin.Application.Handlers.Command.Products;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand.Request, CreateProductCommand.Response>
{
    private readonly IUnitOfWork<IProductRepository> _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IUnitOfWork<IProductRepository> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CreateProductCommand.Response> Handle(CreateProductCommand.Request request, CancellationToken cancellationToken)
    {
        var result = new CreateProductCommand.Response(false);

        try
        {
            var product = _mapper.Map<Entities.Product>(request);
            await _unitOfWork.Repository.AddAsync(product);
            await _unitOfWork.CompleteAsync();

            result = new CreateProductCommand.Response(true);
        }
        catch (Exception ex)
        {
            // Log the exception (optional)
            result = new CreateProductCommand.Response(false);
        }

        return result;
    }
}
