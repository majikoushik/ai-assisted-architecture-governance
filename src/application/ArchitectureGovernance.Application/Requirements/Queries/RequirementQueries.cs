using ArchitectureGovernance.Application.Requirements.DTOs;
using ArchitectureGovernance.Application.Requirements.Mappers;
using ArchitectureGovernance.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ArchitectureGovernance.Application.Requirements.Queries;

public record GetRequirementByIdQuery(Guid Id) : IRequest<RequirementDto?>;

public class GetRequirementByIdQueryHandler : IRequestHandler<GetRequirementByIdQuery, RequirementDto?>
{
    private readonly IAppDbContext _context;

    public GetRequirementByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<RequirementDto?> Handle(GetRequirementByIdQuery request, CancellationToken cancellationToken)
    {
        var requirement = await _context.Requirements
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
            
        return requirement?.ToDto();
    }
}

public record GetRequirementsByProjectIdQuery(Guid ProjectId) : IRequest<List<RequirementDto>>;

public class GetRequirementsByProjectIdQueryHandler : IRequestHandler<GetRequirementsByProjectIdQuery, List<RequirementDto>>
{
    private readonly IAppDbContext _context;

    public GetRequirementsByProjectIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<RequirementDto>> Handle(GetRequirementsByProjectIdQuery request, CancellationToken cancellationToken)
    {
        var requirements = await _context.Requirements
            .AsNoTracking()
            .Where(r => r.ProjectId == request.ProjectId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);

        return requirements.Select(r => r.ToDto()).ToList();
    }
}

public record GetAllRequirementsQuery() : IRequest<List<RequirementDto>>;

public class GetAllRequirementsQueryHandler : IRequestHandler<GetAllRequirementsQuery, List<RequirementDto>>
{
    private readonly IAppDbContext _context;

    public GetAllRequirementsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<RequirementDto>> Handle(GetAllRequirementsQuery request, CancellationToken cancellationToken)
    {
        var requirements = await _context.Requirements
            .AsNoTracking()
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);

        return requirements.Select(r => r.ToDto()).ToList();
    }
}
